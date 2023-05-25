using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AssemplyWeldingQCClearance : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession;
    cCommon objc;
    cMaterials objMat;
    cProduction objP;
    cQuality objQ;
    int Temp;
    string QualityReportSavePath = ConfigurationManager.AppSettings["QualityReportSavePath"].ToString();
    string QualityReportHttpPath = ConfigurationManager.AppSettings["QualityReportHttpPath"].ToString();

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                divOutput.Visible = false;

                ddlRFPNoAndCustomerLoad();
            }
            if (target == "QCApprove")
                SaveQCApprove(arg.ToString());
            if (target == "PrintAssemplyJobCard")
                PrintAssemplyJobCardDetails(arg.ToString());
            if (target == "PartToPartAssemplyJobCardPrint")
                PartToPartAssemplyJobCardDetails(arg.ToString());

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"radio Events"

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlRFPNoAndCustomerLoad();
            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["RFPDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlRFPNo.DataSource = dt;
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        cProduction objP = new cProduction();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

                BindAssemplyWeldingQCClearanceDetails();
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnQCStage_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        DataTable dt = new DataTable();
        GridView gv;
        try
        {
            DataRow dr;

            dt.Columns.Add("JQMID");
            dt.Columns.Add("VA");
            dt.Columns.Add("RO");
            dt.Columns.Add("VB");

            LinkButton btn = sender as LinkButton;
            btn.Text = "clicked!";

            if (btn.CommandName == "BW")
            {
                ViewState["PMID"] = "7";
                gv = gvBeforWelding;
            }
            else if (btn.CommandName == "DW")
            {
                ViewState["PMID"] = "8";
                gv = gvDuringwelding;
            }
            else
            {
                ViewState["PMID"] = "9";
                gv = gvfinalwelding;
            }

            foreach (GridViewRow row in gv.Rows)
            {
                //CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                string JQMID = gv.DataKeys[row.RowIndex].Values[0].ToString();

                Label lblstage = (Label)row.FindControl("lblstageactivity");
                TextBox txtverficationavailability = (TextBox)row.FindControl("txtverificationavailability");
                TextBox txtreferenceobservation = (TextBox)row.FindControl("txtreferenceobservation");
                TextBox txtverfiedby = (TextBox)row.FindControl("txtverifiedBy");

                dr = dt.NewRow();

                dr["JQMID"] = JQMID;
                dr["VA"] = txtverficationavailability.Text;
                dr["RO"] = txtreferenceobservation.Text;
                dr["VB"] = txtverfiedby.Text;

                dt.Rows.Add(dr);
            }
            objQ.CreatedBy = objSession.employeeid;
            objQ.dt = dt;
            objQ.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            objQ.PMID = Convert.ToInt32(ViewState["PMID"].ToString());

            objQ.PAPDID = Convert.ToInt32(ViewState["PAPDID"].ToString());

            ds = objQ.SaveAssemplyJobcardQCStageDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Clearence  Saved Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvAJDetails_AJD_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "ViewAssemplyQC")
            {
                Label lblPartToPart = (Label)gvAJDetails_AJD.Rows[index].FindControl("lblPartToPart");

                lblasqcheading_h.Text = lblJobNo_AJD.Text + "/" + lblPartToPart.Text;

                ViewState["PAPDID"] = gvAJDetails_AJD.DataKeys[index].Values[0].ToString();
                //ViewState["PRIDID"] = gvAJDetails_AJD.DataKeys[index].Values[0].ToString();               

                ViewAssemplyPlanningJobcardItemSnoByPAPDIDAndJCHID();
                ViewAssemplyPlanningQCDetailsByPAPDID();

                BindJobCardQCStageDetailsByJCHID();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAssemplyPlanningQCPopUP();", true);
            }

            if (e.CommandName == "")
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyJobCardHeaderDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["AssemplyJobJCHID"] = gvAssemplyJobCardHeaderDetails.DataKeys[index].Values[0].ToString();
            if (e.CommandName == "ADDQC")
            {
                Label lbl = (Label)gvAssemplyJobCardHeaderDetails.Rows[index].FindControl("lblJobOrderNumber");
                lblJobNo_AJD.Text = ddlRFPNo.SelectedItem.Text + "/" + lbl.Text;

                ViewPartAssemplyCardDetailsByJCHID();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAssemplyJobcardPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyJobCardHeaderDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void gvAssemplyPlanningQCDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvBellowSno_AJD_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");

                if (dr["QCStatus"].ToString() == "1" || dr["QCStatus"].ToString() == "9")
                    chk.Visible = false;
                else
                    chk.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void PartToPartAssemplyJobCardDetails(string PAPDID)
    {
        var page = HttpContext.Current.CurrentHandler as Page;
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.ToLower();

        string[] pagename = url.ToString().Split('/');
        string Replacevalue = pagename[pagename.Length - 1].ToString();

        string Page = url.Replace(Replacevalue, "PartToPartAssemplyJobCardPrint.aspx?JCHID=" + ViewState["AssemplyJobJCHID"].ToString() + "&&PAPDID=" + PAPDID + "");

        string s = "window.open('" + Page + "','_blank');";
        this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    private void PrintAssemplyJobCardDetails(string JCHID)
    {
        var page = HttpContext.Current.CurrentHandler as Page;
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.ToLower();

        string[] pagename = url.ToString().Split('/');
        string Replacevalue = pagename[pagename.Length - 1].ToString();

        string Page = url.Replace(Replacevalue, "AssemplyWeldingJobCardPrint.aspx?JCHID=" + JCHID + "");

        string s = "window.open('" + Page + "','_blank');";
        this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }

    private void BindJobCardQCStageDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            objQ.PAPDID = Convert.ToInt32(ViewState["PAPDID"].ToString());
            ds = objQ.GetAssemplyJobCardQCStageDetailsByJCHID();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvBeforWelding.DataSource = ds.Tables[1];
                gvBeforWelding.DataBind();
            }
            else
            {
                gvBeforWelding.DataSource = "";
                gvBeforWelding.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvDuringwelding.DataSource = ds.Tables[2];
                gvDuringwelding.DataBind();
            }
            else
            {
                gvDuringwelding.DataSource = "";
                gvDuringwelding.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                gvfinalwelding.DataSource = ds.Tables[3];
                gvfinalwelding.DataBind();
            }
            else
            {
                gvfinalwelding.DataSource = "";
                gvfinalwelding.DataBind();
            }


            ViewState["PMID"] = ds.Tables[0].Rows[0]["PMID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewAssemplyPlanningJobcardItemSnoByPAPDIDAndJCHID()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            objQ.PAPDID = Convert.ToInt32(ViewState["PAPDID"].ToString());

            ds = objQ.GetAssemplyPlanningJobcardItemSnoByPAPDIDAndJCHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBellowSno_AJD.DataSource = ds.Tables[0];
                gvBellowSno_AJD.DataBind();
            }
            else
            {
                gvBellowSno_AJD.DataSource = "";
                gvBellowSno_AJD.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyWeldingQCClearanceDetails()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.AssemplyStatus = rblRFPChange.SelectedValue;
            objQ.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQ.GetAssemplyWeldingQCClearanceDetailsByRFPHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyJobCardHeaderDetails.DataSource = ds.Tables[0];
                gvAssemplyJobCardHeaderDetails.DataBind();
            }
            else
            {
                gvAssemplyJobCardHeaderDetails.DataSource = "";
                gvAssemplyJobCardHeaderDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewPartAssemplyCardDetailsByJCHID()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            ds = objP.GetPartAssemplyPlanningJobCardDetailsByJCHID("LS_GetPartAssemplyPlanningJobCardDetailsByJCHIDAndQCApplicable");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAJDetails_AJD.DataSource = ds.Tables[0];
                gvAJDetails_AJD.DataBind();
            }
            else
            {
                gvAJDetails_AJD.DataSource = "";
                gvAJDetails_AJD.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewAssemplyPlanningQCDetailsByPAPDID()
    {
        objQ = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            //  objQ.PRIDID = Convert.ToInt32(ViewState["PRIDID"].ToString());
            objQ.PAPDID = Convert.ToInt32(ViewState["PAPDID"].ToString());
            //objQ.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());

            ds = objQ.GetAssemplyPlanningQCDetailsByPAPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyPlanningQCDetails.DataSource = ds.Tables[0];
                gvAssemplyPlanningQCDetails.DataBind();
            }
            else
            {
                gvAssemplyPlanningQCDetails.DataSource = "";
                gvAssemplyPlanningQCDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void SaveQCApprove(string QCStatus)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objQ = new cQuality();
        string AttachementName = "";
        string PRIDID = "";
        DataTable dtAssemplyQC = new DataTable();
        DataRow dr;
        try
        {
            foreach (GridViewRow row in gvBellowSno_AJD.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                if (chk.Checked)
                {
                    if (PRIDID == "")
                        PRIDID = gvBellowSno_AJD.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PRIDID = PRIDID + ',' + gvBellowSno_AJD.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            dtAssemplyQC.Columns.Add("QPDID");
            dtAssemplyQC.Columns.Add("AttachementName");
            dtAssemplyQC.Columns.Add("Remarks");
            dtAssemplyQC.Columns.Add("AttachementID");
            //  dtAssemplyQC.Columns.Add("Status");

            foreach (GridViewRow row in gvAssemplyPlanningQCDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                FileUpload fpQualityReport = (FileUpload)row.FindControl("fpQCReport");
                //    DropDownList ddlQcType = (DropDownList)row.FindControl("ddlQCType");
                if (chkitems.Checked)
                {
                    dr = dtAssemplyQC.NewRow();
                    if (fpQualityReport.HasFile)
                    {
                        cSales ojSales = new cSales();
                        cCommon objc = new cCommon();
                        objc.Foldername = QualityReportSavePath;
                        AttachementName = Path.GetFileName(fpQualityReport.PostedFile.FileName);
                        //   string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                        string[] extension = AttachementName.Split('.');
                        AttachementName = extension[0].Trim().Replace("/", "") + '.' + extension[1];
                        objc.FileName = AttachementName;
                        objc.PID = "AssemblyWeldingQCReport";
                        objc.AttachementControl = fpQualityReport;
                        objc.SaveFiles();
                    }
                    else
                        AttachementName = "";

                    dr["QPDID"] = Convert.ToInt32(gvAssemplyPlanningQCDetails.DataKeys[row.RowIndex].Values[0].ToString());
                    dr["AttachementName"] = AttachementName;
                    dr["Remarks"] = txtRemarks.Text;
                    //dr["Status"] = QCStatus;
                    // dr["CreatedBy"] = objSession.employeeid;
                    dtAssemplyQC.Rows.Add(dr);
                }
            }

            objQ.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            objQ.Status = QCStatus;
            objQ.PAPDID = Convert.ToInt32(ViewState["PAPDID"]);
            objQ.CreatedBy = objSession.employeeid;

            objQ.Remarks = txtOverallRemarks.Text;

            objQ.dt = dtAssemplyQC;

            ds = objQ.SavePartAssemplyWeldingQCClearanceDetails(PRIDID);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Clearence  Saved Successfully');ShowAssemplyPlanningQCPopUP();", true);
                //ddlRFPNo_SelectIndexChanged(null, null);
                ViewAssemplyPlanningQCDetailsByPAPDID();
                ViewAssemplyPlanningJobcardItemSnoByPAPDIDAndJCHID();
                ViewPartAssemplyCardDetailsByJCHID();
                BindAssemplyWeldingQCClearanceDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    private void ddlRFPNoAndCustomerLoad()
    {
        try
        {
            //objc = new cCommon();
            objQ = new cQuality();
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            //dsCustomer = objc.getRFPCustomerNameByQualityUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
            //dsRFPHID = objQ.GetRFPDetailsByJobcardQC(Convert.ToInt32(objSession.employeeid), ddlRFPNo);

            dsCustomer = objQ.GetRFPCustomerNameByAsemplyWeldingQCClearance(Convert.ToInt32(objSession.employeeid), ddlCustomerName, rblRFPChange.SelectedValue);
            dsRFPHID = objQ.GetRFPNoDetailsByAssemplyweldingQCClearance(Convert.ToInt32(objSession.employeeid), ddlRFPNo, rblRFPChange.SelectedValue);

            ViewState["RFPDetails"] = dsRFPHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAssemplyAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        DataSet dscheck = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 6;
            objc.JCHID = Convert.ToInt32(0);
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objc.GetStaffDetailsByRFPHIDAndUserType();

            string[] str = ds.Tables[0].Rows[0]["EmployeeIDS"].ToString().Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = str[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Alert For Assemply Welding Job Card QC Clearance";
                objAlerts.Message = "Assemply Welding Job Card QC Requested From RFP No" + " / " + ds.Tables[0].Rows[0]["RFPNo"].ToString() + "And Job card No " + ViewState["AWJCNo"].ToString();
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}