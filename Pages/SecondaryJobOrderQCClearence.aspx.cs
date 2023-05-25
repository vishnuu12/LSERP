using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.IO;
using System.Configuration;
using System.Text;
using SelectPdf;
using System.Text.RegularExpressions;

public partial class Pages_SecondaryJobOrderQCClearence : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cProduction objP;
    cQuality objQ;
    int Temp;
    string QualityReportSavePath = ConfigurationManager.AppSettings["QualityReportSavePath"].ToString();
    string QualityReportHttpPath = ConfigurationManager.AppSettings["QualityReportHttpPath"].ToString();

    string JobCardStatusAttachementSavePath = ConfigurationManager.AppSettings["JobCardStatusAttachementSavePath"].ToString();
    string JobCardStatusAttachementHttpPath = ConfigurationManager.AppSettings["JobCardStatusAttachementHTTPPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
                ddlrfpNoAndCustomerNameLoad();
            if (target == "QCApprove")
                SaveQCApprove(arg.ToString());
            if (target == "printjobcard")
                JobCardPrintDetails(arg.ToString().Split('/')[0].ToString(), Convert.ToInt32(arg.ToString().Split('/')[1].ToString()));
            if (target == "viewjobcardAttach")
                ViewJobCardAttachName(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlrfpNoAndCustomerNameLoad();
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
                objP.rfpchangestatus = rblRFPChange.SelectedValue;

                ds = objP.GetJobCardHeaderDetailsByRFPHID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvJobCardHeaderDetails.DataSource = ds.Tables[0];
                    gvJobCardHeaderDetails.DataBind();
                }
                else
                {
                    gvJobCardHeaderDetails.DataSource = "";
                    gvJobCardHeaderDetails.DataBind();
                }
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"

    //protected void btnSaveQCJobCard_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objP = new cProduction();
    //    try
    //    {
    //        foreach (GridViewRow row in gvJobCardPartDetails.Rows)
    //        {
    //            CheckBox chkitems = (CheckBox)row.FindControl("chkQC");
    //            RadioButtonList rbl = (RadioButtonList)gvJobCardPartDetails.Rows[row.RowIndex].FindControl("rblQC");
    //            TextBox txtRemarks = (TextBox)gvJobCardPartDetails.Rows[row.RowIndex].FindControl("txtRemarks");

    //            if (chkitems.Checked)
    //            {
    //                if (chkitems.CssClass != "aspNetDisabled")
    //                {
    //                    objP.JCDIDs = Convert.ToInt32(gvJobCardPartDetails.DataKeys[row.RowIndex].Values[0].ToString());
    //                    objP.QCStatus = rbl.SelectedValue;
    //                    objP.Remarks = txtRemarks.Text;
    //                    ds = objP.SaveJobCardQCClearenceDetails();
    //                }
    //            }
    //        }
    //        if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Clearence  Saved Successfully');hidePartDetailsPopUp();", true);
    //            ddlRFPNo_SelectIndexChanged(null, null);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void btnSaveQCApprove_Click(object sender, EventArgs e)
    {

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

            if (btn.CommandName == "MC")
                gv = gvQCStageDetails;
            else if (btn.CommandName == "BW")
            {
                ViewState["PMID"] = "7";
                gv = gvBeforWelding;
            }
            else if (btn.CommandName == "DW")
            {
                ViewState["PMID"] = "8";
                gv = gvDuringwelding;
            }
            else if (btn.CommandName == "FW")
            {
                ViewState["PMID"] = "9";
                gv = gvfinalwelding;
            }
            else
                gv = gvbellowformingtangentcutting;

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
            objQ.JCHID = Convert.ToInt32(hdnJCHID.Value);
            objQ.PMID = Convert.ToInt32(ViewState["PMID"].ToString());

            ds = objQ.SaveJObCardNOQCStageDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Clearence  Saved Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveAttchement_Click(object sender, EventArgs e)
    {
        cQuality objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.AttachementDescription = txtDescription.Text;
            objQt.JCHID = Convert.ToInt32(hdnJCHID.Value);
            string AttachmentName = "";

            if (fAttachment.HasFile)
            {
                string Attchname = "";

                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();

                string[] extension = extn.Split('.');

                Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");

                AttachmentName = Attchname + '.' + extension[extension.Length - 1];
            }

            objQt.AttachementName = AttachmentName;

            objQt.CreatedBy = objSession.employeeid;

            ds = objQt.SaveJobCardQCAttachements();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachement Details Saved successfully');", true);

                string JobCardNo = ds.Tables[0].Rows[0]["JobOrderNo"].ToString();

                string StrStaffDocumentPath = JobCardStatusAttachementSavePath + JobCardNo + "\\";

                if (!Directory.Exists(StrStaffDocumentPath))
                    Directory.CreateDirectory(StrStaffDocumentPath);

                if (AttachmentName != "")
                    fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);
            }

            BindAttachements(hdnJCHID.Value);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objQ = new cQuality();
            DataSet ds = new DataSet();
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "DeleteAttachement")
            {
                string AttachementID = gvAttachments.DataKeys[index].Values[0].ToString();

                objQ.AttachementID = AttachementID;
                objQ.JCHID = Convert.ToInt32(hdnJCHID.Value);

                ds = objQ.DeleteJobCardAttachementDetailsByAttachementIDAndJCHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','Attachements Deleted Succesfully');", true);
                BindAttachements(hdnJCHID.Value);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvJobCardHeaderDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnJCHID.Value = gvJobCardHeaderDetails.DataKeys[index].Values[0].ToString();

            Label lblProcessName = (Label)gvJobCardHeaderDetails.Rows[index].FindControl("lblProcessName");
            Label lblJobCardNumber = (Label)gvJobCardHeaderDetails.Rows[index].FindControl("lblJobOrderNumber");

            ViewState["JobCardNo"] = lblJobCardNumber.Text;

            if (e.CommandName.ToString() == "ADDQC")
            {
                BindJobCardPartDetails();

                if (lblProcessName.Text == "Marking & Cutting" || lblProcessName.Text == "Sheet Marking & Cutting")
                {
                    divFabricationAndWelding_QC.Visible = false;
                    divmarkingAndCuttingqCStage_QC.Visible = true;
                    divBellowFormingAndTangentCutting_QC.Visible = false;
                }
                else if (lblProcessName.Text == "Fabrication & Welding" || lblProcessName.Text == "Sheet Welding")
                {
                    divFabricationAndWelding_QC.Visible = true;
                    divmarkingAndCuttingqCStage_QC.Visible = false;
                    divBellowFormingAndTangentCutting_QC.Visible = false;
                }
                else if (lblProcessName.Text == "Bellow Forming & Tangent Cutting")
                {
                    divFabricationAndWelding_QC.Visible = false;
                    divmarkingAndCuttingqCStage_QC.Visible = false;
                    divBellowFormingAndTangentCutting_QC.Visible = true;
                }

                ViewState["JID"] = gvJobCardHeaderDetails.DataKeys[index].Values[2].ToString();
                string PartName = gvJobCardHeaderDetails.DataKeys[index].Values[1].ToString();

                lblJobCardNumber_P.Text = lblJobCardNumber.Text + "/" + PartName + " / " + lblProcessName.Text;


                bindQualityHeaderDetailsByJCHID();
                BindJobCardQCStageDetailsByJCHID();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddJobCardPartPopUp();", true);
            }
            else if (e.CommandName.ToString() == "AddStage")
            {
                // lblprocessname_QC.Text = ddlRFPNo.SelectedItem.Text + "/" + lblProcessName.Text + "/" + lblJobCardNumber.Text;
                BindJobCardQCStageDetailsByJCHID();
                if (ViewState["PMID"].ToString() == "1" || ViewState["PMID"].ToString() == "4")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowMarkingAndCuttingQCStagePopup();", true);
                else if (ViewState["PMID"].ToString() == "2" || ViewState["PMID"].ToString() == "5")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SHowFabricationWeldingPopUp();", true);
                else if (ViewState["PMID"].ToString() == "3")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowBellowFormingPopUp();", true);
            }

            else if (e.CommandName.ToString() == "addjobattch")
            {
                lbljobcardattchheader_h.Text = lblJobCardNumber.Text + " / " + lblProcessName.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowJobCardAttachementPopUp();", true);
                BindAttachements(hdnJCHID.Value);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvJobCardHeaderDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnAddQC");
                if (dr["ButtonStatus"].ToString() == "Disabled")
                {
                    btn.CssClass = "aspNetDisabled";
                    btn.ToolTip = "QC Not Requested";
                }
                else
                    btn.CssClass = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvJobCardPartDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");
                if (dr["QCCheckedStatus"].ToString() == "0" || dr["QCCheckedStatus"].ToString() == "8")
                    chkQC.Visible = true;
                else if (dr["QCCheckedStatus"].ToString() == "9" || dr["QCCheckedStatus"].ToString() == "1" || dr["QCCheckedStatus"].ToString() == "7")
                    chkQC.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void gvApproveQC_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument.ToString());

    //        hdnJCDID.Value = gvJobCardPartDetails.DataKeys[index].Values[0].ToString();
    //        Label lblPartSNo = (Label)gvJobCardPartDetails.Rows[index].FindControl("lblPartSNO");
    //        lblPartSNO_Q.Text = lblPartSNo.Text + "/" + lblJobCardNumber_P.Text;
    //        bindQualityHeaderDetailsByJCHID();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowApproveQCPopUp();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void gvApproveQC_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FileUpload fp = (FileUpload)e.Row.FindControl("fpQCReport");
                if (dr["DOCUMENTMANDATORY"].ToString() == "0")
                    fp.CssClass = "form-control";
                else
                    fp.CssClass = "form-control md";
                //CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");
                //RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("rblQC");
                //if (dr["QCStatus"].ToString() == "1")
                //{
                //    chkQC.CssClass = "aspNetDisabled";
                //    rbl.SelectedValue = "A";
                //    chkQC.Checked = true;
                //}
                //else if (dr["QCStatus"].ToString() == "9")
                //{
                //    chkQC.CssClass = "";
                //    chkQC.Checked = true;
                //    rbl.SelectedValue = "R";
                //}
                //else
                //{
                //    chkQC.CssClass = "";
                //    rbl.ClearSelection();
                //    chkQC.Checked = false;
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvJobCardQCDimensionDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //Set the edit index.
            gvJobCardQCDimensionDetails.EditIndex = e.NewEditIndex;
            bindQualityHeaderDetailsByJCHID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvJobCardQCDimensionDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvJobCardQCDimensionDetails.EditIndex = -1;
            bindQualityHeaderDetailsByJCHID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvJobCardQCDimensionDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            string DimensionName = gvJobCardQCDimensionDetails.DataKeys[e.RowIndex].Values[0].ToString();
            objQ.QCDimensionName = gvJobCardQCDimensionDetails.DataKeys[e.RowIndex].Values[0].ToString();
            objQ.JCHID = Convert.ToInt32(hdnJCHID.Value);
            TextBox txtActual = (TextBox)gvJobCardQCDimensionDetails.Rows[e.RowIndex].FindControl("txtActual");
            objQ.Actual = txtActual.Text;

            ds = objQ.UpdateQCDimensionDetailsByJCHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','" + DimensionName + " Updated Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','" + DimensionName + " Records Saved Succesfully');", true);

            gvJobCardQCDimensionDetails.EditIndex = -1;
            bindQualityHeaderDetailsByJCHID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"
    private void ViewJobCardAttachName(Int32 index)
    {
        objc = new cCommon();
        try
        {
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            string BasehttpPath = JobCardStatusAttachementHttpPath + ViewState["JobCardNo"].ToString() + "/";
            string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
            ViewState["FileName"] = FileName;
            ifrm.Attributes.Add("src", BasehttpPath + FileName);

            objc.ViewFileName(JobCardStatusAttachementSavePath, JobCardStatusAttachementHttpPath, FileName, ViewState["JobCardNo"].ToString(), ifrm);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAttachements(string JCHID)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.JCHID = Convert.ToInt32(JCHID);
            ds = objQ.GetAttachementDetailsByJobCardNo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAttachments.DataSource = ds.Tables[0];
                gvAttachments.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlrfpNoAndCustomerNameLoad()
    {
        DataSet ds = new DataSet();
        try
        {
            objc = new cCommon();
            objQ = new cQuality();
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objQ.getRFPCustomerNameByJobCardQCByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, rblRFPChange.SelectedValue);
            dsRFPHID = objQ.GetRFPDetailsByJobcardQC(Convert.ToInt32(objSession.employeeid), ddlRFPNo, rblRFPChange.SelectedValue);
            ViewState["RFPDetails"] = dsRFPHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindJobCardQCStageDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.JCHID = Convert.ToInt32(hdnJCHID.Value);
            ds = objQ.GetJobCardQCStageDetailsByJCHID();

            if (ds.Tables[0].Rows[0]["PMID"].ToString() == "4" || ds.Tables[0].Rows[0]["PMID"].ToString() == "1")
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvQCStageDetails.DataSource = ds.Tables[1];
                    gvQCStageDetails.DataBind();
                }
                else
                {
                    gvQCStageDetails.DataSource = "";
                    gvQCStageDetails.DataBind();
                }
            }
            else if (ds.Tables[0].Rows[0]["PMID"].ToString() == "2" || ds.Tables[0].Rows[0]["PMID"].ToString() == "5")
            {
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
            }

            else if (ds.Tables[0].Rows[0]["PMID"].ToString() == "3")
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvbellowformingtangentcutting.DataSource = ds.Tables[1];
                    gvbellowformingtangentcutting.DataBind();
                }
                else
                {
                    gvbellowformingtangentcutting.DataSource = "";
                    gvbellowformingtangentcutting.DataBind();
                }
            }

            ViewState["PMID"] = ds.Tables[0].Rows[0]["PMID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindJobCardPartDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
            ds = objP.getJobCardPartDetailsByJCHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobCardPartDetails.DataSource = ds.Tables[0];
                gvJobCardPartDetails.DataBind();
                ViewState["ProcessName"] = ds.Tables[1].Rows[0]["ProcessName"].ToString();
            }
            else
            {
                gvJobCardPartDetails.DataSource = "";
                gvJobCardPartDetails.DataBind();
            }

            //gvJobCardPartDetails.Columns[3].Visible = false;
            //gvJobCardPartDetails.Columns[5].Visible = false;
            //gvJobCardPartDetails.Columns[0].Visible = false;
            //gvJobCardPartDetails.Columns[4].Visible = false;

            if ((ds.Tables[1].Rows[0]["ProcessName"].ToString() == "Sheet Welding") || (ds.Tables[1].Rows[0]["ProcessName"].ToString() == "Fabrication & Welding"))
            {
                //gvJobCardPartDetails.Columns[0].Visible = false;
                //gvJobCardPartDetails.Columns[3].Visible = false;
                //gvJobCardPartDetails.Columns[4].Visible = false;
                //gvJobCardPartDetails.Columns[5].Visible = true;
            }
            else
            {
                //gvJobCardPartDetails.Columns[0].Visible = true;
                //gvJobCardPartDetails.Columns[3].Visible = true;
                //gvJobCardPartDetails.Columns[4].Visible = true;
                //gvJobCardPartDetails.Columns[5].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindQualityHeaderDetailsByJCHID()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            divDimensionDetails.Visible = false;

            objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
            objP.JCDID = Convert.ToInt32(hdnJCDID.Value);
            objP.ProcessName = ViewState["ProcessName"].ToString();
            ds = objP.GetQualityProcessNameDetailsByJCHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvApproveQC.DataSource = ds.Tables[0];
                gvApproveQC.DataBind();
            }
            else
            {
                gvApproveQC.DataSource = "";
                gvApproveQC.DataBind();
            }

            if (ViewState["ProcessName"].ToString() == "Marking & Cutting")
            {
                divMarkingAndCutting_pv_mc.Visible = true;
                divMarkingAndCutting_pv_smc.Visible = false;
                divBellowFormingAndTangentCutting_pv_BFTC.Visible = false;
                divfabricationwelding_pv_fw.Visible = false;

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblItemname_pv_mc.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
                    lblsize_pv_mc.Text = ds.Tables[1].Rows[0]["Size"].ToString();
                    lblRFPNo_pv_mc.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    lblpartName_pv_mc.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
                    lblcategory_pv_mc.Text = ds.Tables[2].Rows[0]["CategoryName"].ToString();
                    lblgrade_pv_mc.Text = ds.Tables[2].Rows[0]["GradeName"].ToString();
                    lblthk_pv_mc.Text = ds.Tables[2].Rows[0]["THKValue"].ToString();
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    lbldrawingname_pv_mc.Text = ds.Tables[3].Rows[0]["DrawingName"].ToString();
                    lblprocessname_pv_mc.Text = ds.Tables[3].Rows[0]["ProcessName"].ToString();
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    lblstageofactivity_pv_mc.Text = ds.Tables[4].Rows[0]["JobOrderRemarks"].ToString();
                    lblTubeID_pv_mc.Text = ds.Tables[4].Rows[0]["TubeID"].ToString();
                    lblIssueDate_pv_mc.Text = ds.Tables[4].Rows[0]["IssueDate"].ToString();
                    lblTargetdate_pv_mc.Text = ds.Tables[4].Rows[0]["TargetDate"].ToString();
                    lbljobcardraisedby_pv_mc.Text = ds.Tables[4].Rows[0]["JobCardRaisedBy"].ToString();
                }

                divDimensionDetails.Visible = true;

                if (ds.Tables[5].Rows.Count > 0)
                {
                    gvJobCardQCDimensionDetails.DataSource = ds.Tables[5];
                    gvJobCardQCDimensionDetails.DataBind();
                }
                else
                {
                    gvJobCardQCDimensionDetails.DataSource = "";
                    gvJobCardQCDimensionDetails.DataBind();
                }
            }

            if (ViewState["ProcessName"].ToString() == "Sheet Marking & Cutting")
            {
                try
                {
                    divMarkingAndCutting_pv_mc.Visible = false;
                    divMarkingAndCutting_pv_smc.Visible = true;
                    divBellowFormingAndTangentCutting_pv_BFTC.Visible = false;
                    divfabricationwelding_pv_fw.Visible = false;

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblItemname_pv_smc.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
                        lblsize_pv_smc.Text = ds.Tables[1].Rows[0]["Size"].ToString();
                        lblRFPNo_pv_smc.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblpartName_pv_smc.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
                        lblcategory_pv_smc.Text = ds.Tables[2].Rows[0]["CategoryName"].ToString();
                        lblgrade_pv_smc.Text = ds.Tables[2].Rows[0]["GradeName"].ToString();
                        lblthk_pv_smc.Text = ds.Tables[2].Rows[0]["THKValue"].ToString();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        lbldrawingname_pv_smc.Text = ds.Tables[3].Rows[0]["DrawingName"].ToString();
                        lblprocessname_pv_smc.Text = ds.Tables[3].Rows[0]["ProcessName"].ToString();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        lblstageofactivity_pv_smc.Text = ds.Tables[4].Rows[0]["JobOrderRemarks"].ToString();
                        lblTubeID_pv_smc.Text = ds.Tables[4].Rows[0]["TubeID"].ToString();
                        lblIssueDate_pv_smc.Text = ds.Tables[4].Rows[0]["IssueDate"].ToString();
                        lblTargetdate_pv_smc.Text = ds.Tables[4].Rows[0]["TargetDate"].ToString();
                        lbljobcardraisedby_pv_smc.Text = ds.Tables[4].Rows[0]["JobCardRaisedBy"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            if (ViewState["ProcessName"].ToString() == "Fabrication & Welding")
            {
                divMarkingAndCutting_pv_mc.Visible = false;
                divMarkingAndCutting_pv_smc.Visible = false;
                divBellowFormingAndTangentCutting_pv_BFTC.Visible = false;
                divfabricationwelding_pv_fw.Visible = true;
                try
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblItemname_pv_fw.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
                        lblsize_pv_fw.Text = ds.Tables[1].Rows[0]["Size"].ToString();
                        lblRFPNo_pv_fw.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblpartName_pv_fw.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
                        lblcategory_pv_fw.Text = ds.Tables[2].Rows[0]["CategoryName"].ToString();
                        lblgrade_pv_fw.Text = ds.Tables[2].Rows[0]["GradeName"].ToString();
                        lblthk_pv_fw.Text = ds.Tables[2].Rows[0]["THKValue"].ToString();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        lbldrawingname_pv_fw.Text = ds.Tables[3].Rows[0]["DrawingName"].ToString();
                        lblprocessname_pv_fw.Text = ds.Tables[3].Rows[0]["ProcessName"].ToString();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        lblstageofactivity_pv_fw.Text = ds.Tables[4].Rows[0]["JobOrderRemarks"].ToString();
                        lblIssueDate_pv_fw.Text = ds.Tables[4].Rows[0]["IssueDate"].ToString();
                        lblTargetdate_pv_fw.Text = ds.Tables[4].Rows[0]["TargetDate"].ToString();
                        lbljobcardraisedby_pv_fw.Text = ds.Tables[4].Rows[0]["JobCardRaisedBy"].ToString();
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                        lblNOP_pv_fw.Text = ds.Tables[5].Rows[0]["NOP"].ToString();
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            if (ViewState["ProcessName"].ToString() == "Sheet Welding")
            {
                divMarkingAndCutting_pv_mc.Visible = false;
                divMarkingAndCutting_pv_smc.Visible = false;
                divfabricationwelding_pv_fw.Visible = false;
                divBellowFormingAndTangentCutting_pv_BFTC.Visible = false;
                divfabricationwelding_pv_sw.Visible = true;
                try
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblItemname_pv_sw.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
                        lblsize_pv_sw.Text = ds.Tables[1].Rows[0]["Size"].ToString();
                        lblRFPNo_pv_sw.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblpartName_pv_sw.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
                        lblcategory_pv_sw.Text = ds.Tables[2].Rows[0]["CategoryName"].ToString();
                        lblgrade_pv_sw.Text = ds.Tables[2].Rows[0]["GradeName"].ToString();
                        lblthk_pv_sw.Text = ds.Tables[2].Rows[0]["THKValue"].ToString();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        lbldrawingname_pv_sw.Text = ds.Tables[3].Rows[0]["DrawingName"].ToString();
                        lblprocessname_pv_sw.Text = ds.Tables[3].Rows[0]["ProcessName"].ToString();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        lblstageofactivity_pv_sw.Text = ds.Tables[4].Rows[0]["JobOrderRemarks"].ToString();
                        lblIssueDate_pv_sw.Text = ds.Tables[4].Rows[0]["IssueDate"].ToString();
                        lblTargetdate_pv_sw.Text = ds.Tables[4].Rows[0]["TargetDate"].ToString();
                        lbljobcardraisedby_pv_sw.Text = ds.Tables[4].Rows[0]["JobCardRaisedBy"].ToString();
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                        lblNOP_pv_sw.Text = ds.Tables[5].Rows[0]["NOP"].ToString();
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            if (ViewState["ProcessName"].ToString() == "Bellow Forming & Tangent Cutting")
            {
                divMarkingAndCutting_pv_mc.Visible = false;
                divMarkingAndCutting_pv_smc.Visible = false;
                divfabricationwelding_pv_fw.Visible = false;
                divfabricationwelding_pv_sw.Visible = false;
                divBellowFormingAndTangentCutting_pv_BFTC.Visible = true;
                try
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblItemname_pv_BFTC.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
                        lblsize_pv_BFTC.Text = ds.Tables[1].Rows[0]["Size"].ToString();
                        lblRFPNo_pv_BFTC.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        lblpartName_pv_BFTC.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
                        lblcategory_pv_BFTC.Text = ds.Tables[2].Rows[0]["CategoryName"].ToString();
                        lblgrade_pv_BFTC.Text = ds.Tables[2].Rows[0]["GradeName"].ToString();
                        lblthk_pv_BFTC.Text = ds.Tables[2].Rows[0]["THKValue"].ToString();
                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        lbldrawingname_pv_BFTC.Text = ds.Tables[3].Rows[0]["DrawingName"].ToString();
                        lblprocessname_pv_BFTC.Text = ds.Tables[3].Rows[0]["ProcessName"].ToString();
                    }
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        lblstageofactivity_pv_BFTC.Text = ds.Tables[4].Rows[0]["JobOrderRemarks"].ToString();
                        lblIssueDate_pv_BFTC.Text = ds.Tables[4].Rows[0]["IssueDate"].ToString();
                        lblTargetdate_pv_BFTC.Text = ds.Tables[4].Rows[0]["TargetDate"].ToString();
                        lbljobcardraisedby_pv_BFTC.Text = ds.Tables[4].Rows[0]["JobCardRaisedBy"].ToString();
                    }
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        lblNOP_pv_BFTC.Text = ds.Tables[5].Rows[0]["NOP"].ToString();
                        lblformingmethod_pv_BFTC.Text = ds.Tables[5].Rows[0]["FormingMethod"].ToString();
                    }
                    if (ds.Tables[5].Rows[0]["FormingMethod"].ToString() == "Expandal")
                    {
                        divDimensionDetails.Visible = true;

                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            gvJobCardQCDimensionDetails.DataSource = ds.Tables[6];
                            gvJobCardQCDimensionDetails.DataBind();
                        }
                        else
                        {
                            gvJobCardQCDimensionDetails.DataSource = "";
                            gvJobCardQCDimensionDetails.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
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
        cProduction objP = new cProduction();
        string AttachementName = "";
        string JCDID = "";
        try
        {
            DataRow dr;
            dt.Columns.Add("QCPDID");
            dt.Columns.Add("JCDID");
            dt.Columns.Add("RFPQPDID");
            dt.Columns.Add("QCRemarks");
            dt.Columns.Add("QCReport");
            dt.Columns.Add("QcType");
            foreach (GridViewRow row in gvApproveQC.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkQC");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                FileUpload fpQualityReport = (FileUpload)row.FindControl("fpQCReport");
                //    DropDownList ddlQcType = (DropDownList)row.FindControl("ddlQCType");
                if (chkitems.Checked)
                {
                    if (fpQualityReport.HasFile)
                    {
                        cSales ojSales = new cSales();
                        cCommon objc = new cCommon();
                        objc.Foldername = QualityReportSavePath;
                        AttachementName = Path.GetFileName(fpQualityReport.PostedFile.FileName);
                        string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                        string[] extension = AttachementName.Split('.');
                        AttachementName = extension[0].Trim().Replace("/", "") + '.' + extension[1];
                        objc.FileName = AttachementName;
                        objc.PID = ViewState["JID"].ToString();
                        objc.AttachementControl = fpQualityReport;
                        objc.SaveFiles();
                    }
                    else
                        AttachementName = "";

                    dr = dt.NewRow();
                    dr["QCPDID"] = 0;
                    dr["JCDID"] = 0;
                    dr["RFPQPDID"] = Convert.ToInt32(gvApproveQC.DataKeys[row.RowIndex].Values[1].ToString());
                    dr["QCRemarks"] = txtRemarks.Text;
                    dr["QCReport"] = AttachementName;
                    dr["QcType"] = 0;
                    dt.Rows.Add(dr);
                }
            }
            foreach (GridViewRow row in gvJobCardPartDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkQC");
                if (chkitems.Checked)
                {
                    if (JCDID == "")
                        JCDID = gvJobCardPartDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        JCDID = JCDID + "," + gvJobCardPartDetails.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            objP.JCDIDs = JCDID;
            objP.Flag = QCStatus;
            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objP.dt = dt;
            objP.Remarks = txtOverallRemarks.Text;

            ds = objP.SaveJobCardQCClearenceProcessStagesDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                string status = "";
                if (QCStatus == "A")
                    status = "Accepted";
                else if (QCStatus == "RJ")
                    status = "Rejected";
                else if (QCStatus == "RW")
                    status = "Reworked";
                else if (status == "HOLD")
                    status = "Holded";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC " + status + " Successfully');hidePartDetailsPopUp();", true);
                BindJobCardPartDetails();
                ddlRFPNo_SelectIndexChanged(null, null);
                SaveAlertDetails(QCStatus);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Your Transaction Was Not Successful');hidePartDetailsPopUp();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    private void JobCardPrintDetails(string JCHID, int index)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            ViewState["ProcessName"] = gvJobCardHeaderDetails.DataKeys[index].Values[3].ToString();

            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            if (ViewState["ProcessName"].ToString() == "Sheet Marking & Cutting")
            {
                string Page = url.Replace(Replacevalue, "SheetmarkingAndCuttingProcessCardPrint.aspx?JCHID=" + JCHID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                //try
                //{
                //    objP.JCHID = Convert.ToInt32(JCHID);
                //    ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetMarkingAndCuttingDetailsByJCHID_PRINT");
                //    lblProcessName_SMC_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                //    lblJobOrderID_SMC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                //    lblProcessname_SMC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                //    ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                //    lblDate_SMC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                //    lblRFPNo_SMC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                //    lblContractorName_SMC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                //    lblContractorTeamname_SMC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                //    lblItemNameSize_SMC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                //    lblDrawingName_SMC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                //    lblPartname_SMC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //    //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                //    lblMaterialCategory_SMC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                //    lblMaterialGrade_SMC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                //    lblThickness_SMC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                //    lblMRNNumber_SMC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                //    lblJobOrderRemarks_SMC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();
                //    lblTubeID_SMC_P.Text = ds.Tables[7].Rows[0]["TubeID"].ToString();
                //    lblTubeLength_SMC_P.Text = ds.Tables[7].Rows[0]["TubeLength"].ToString();

                //    lblOverAllRemarks_SMC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                //    lbldeadlineDate_SMC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

                //    lbljobcardstatus_SMC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                //    if (ds.Tables[6].Rows.Count > 0)
                //    {
                //        gvPartSno_SMC_P.DataSource = ds.Tables[6];
                //        gvPartSno_SMC_P.DataBind();
                //    }

                //    gvPLYDetails_SMC_P.DataSource = ds.Tables[5];
                //    gvPLYDetails_SMC_P.DataBind();

                //    if (ds.Tables[9].Rows.Count > 0)
                //    {
                //        gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                //        gvMRNIssueDetails_SMC_P.DataBind();
                //    }

                //    if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                //        lblOfferQCTest_SMC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                //    else
                //        lblOfferQCTest_SMC_P.Text = "";

                //    lblTotalCost_SMC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                //    lblPartQty_SMC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

                //    ViewState["Address"] = ds.Tables[10];

                //    gvQCObservationdetails_SMC_P.DataSource = ds.Tables[11];
                //    gvQCObservationdetails_SMC_P.DataBind();

                //    cQRcode objQr = new cQRcode();

                //    string QrNumber = objQr.generateQRNumber(9);
                //    string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                //    string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                //    string QrCode = objQr.QRcodeGeneration(Code);
                //    if (QrCode != "")
                //    {
                //        //imgQrcode.Attributes.Add("style", "display:block;");
                //        //imgQrcode.ImageUrl = QrCode;
                //        ViewState["QrCode"] = QrCode;
                //        objQr.QRNumber = displayQrnumber;
                //        objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                //        objQr.createdBy = objSession.employeeid;
                //        string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                //        objQr.Pagename = pageName;
                //        objQr.saveQRNumber();
                //    }
                //    else
                //        ViewState["QrCode"] = "";

                //    DataTable dtAddress = new DataTable();
                //    dtAddress = (DataTable)ViewState["Address"];

                //    hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                //    hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                //    hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                //    hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                //    //gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                //    //gvQCObservationDetails_MC_P.DataBind();

                //    hdnDocNo.Value = ds.Tables[12].Rows[0]["DocNo"].ToString();
                //    hdnRevNo.Value = ds.Tables[12].Rows[0]["RevNo"].ToString();
                //    hdnRevDate.Value = ds.Tables[12].Rows[0]["RevDate"].ToString();

                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','SMC');", true);

                //    // GeneratePDFFile("SheetMarkingAndCutting");
                //}
                //catch (Exception ex)
                //{
                //    Log.Message(ex.ToString());
                //}
            }

            else if (ViewState["ProcessName"].ToString() == "Marking & Cutting")
            {
                try
                {
                    string Page = url.Replace(Replacevalue, "MarkingAndCuttingPrint.aspx?JCHID=" + JCHID + "");

                    string s = "window.open('" + Page + "','_blank');";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                    //objP.JCHID = Convert.ToInt32(JCHID);
                    //ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetMarkingAndCuttingDetailsByJCHID_PRINT");

                    //lblProcessNameHeader_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //lblJobOrderID_MC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                    //lblProcessName_MC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                    //lblDate_MC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                    //lblRFPNo_MC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                    //lblContractorName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                    //lblContractorTeamMemberName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                    //lblItemNameSize_MC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    //lblDrawingname_MC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    //lblPartName_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                    ////  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                    //lblMaterialCategory_MC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    //lblmaterialGrade_MC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    //lblThickness_MC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                    //lblMRNNumber_MC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                    //lblJobOrderRemarks_MC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                    //lblOverallremarks_MC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                    //lblDeadlineDate_MC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

                    //lbljobcardStatus_MC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                    //if (ds.Tables[6].Rows.Count > 0)
                    //{
                    //    gvPartSerialNo_MC_P.DataSource = ds.Tables[6];
                    //    gvPartSerialNo_MC_P.DataBind();
                    //}

                    //string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                    //string result = "";

                    //lblFabricationType_MC_P.Text = ds.Tables[5].Rows[0]["FabricationTypeName"].ToString();

                    //foreach (DataRow dr in ds.Tables[5].Rows)
                    //{
                    //    result = "";
                    //    result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                    //    sb.Append(result);
                    //}

                    //divfabrication_MC_P.InnerHtml = sb.ToString();

                    //if (ds.Tables[9].Rows.Count > 0)
                    //{
                    //    gvIssueDetails_MC_P.DataSource = ds.Tables[9];
                    //    gvIssueDetails_MC_P.DataBind();
                    //}

                    //if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    //    lblOfferQC_MC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                    //else
                    //    lblOfferQC_MC_P.Text = "";

                    //lblTotalCost_MC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                    //lblPartQty_MC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

                    //ViewState["Address"] = ds.Tables[10];

                    //// ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('information','Functionality Moved Into Another Page');", true);
                    //// Response.Redirect("JobCardExpensesSheet.aspx");
                    ////Session["JCHID"] = hdnJCHID.Value;
                    ////Session["RFPHID"] = ddlRFPNo.SelectedValue;
                    ////Response.Redirect("JobCardExpensesSheet.aspx", true);
                    ////GeneratePDFFile("FabricationMarkingAndCutting");

                    //gvQCObservationDetails_MC_P.DataSource = ds.Tables[11];
                    //gvQCObservationDetails_MC_P.DataBind();

                    //cQRcode objQr = new cQRcode();

                    //string QrNumber = objQr.generateQRNumber(9);
                    //string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                    //string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                    //string QrCode = objQr.QRcodeGeneration(Code);
                    //if (QrCode != "")
                    //{
                    //    //imgQrcode.Attributes.Add("style", "display:block;");
                    //    //imgQrcode.ImageUrl = QrCode;
                    //    ViewState["QrCode"] = QrCode;
                    //    objQr.QRNumber = displayQrnumber;
                    //    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                    //    objQr.createdBy = objSession.employeeid;
                    //    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    //    objQr.Pagename = pageName;
                    //    objQr.saveQRNumber();
                    //}
                    //else
                    //    ViewState["QrCode"] = "";

                    //DataTable dtAddress = new DataTable();
                    //dtAddress = (DataTable)ViewState["Address"];

                    //hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                    //hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                    //hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                    //hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                    ////DNLID DocNo RevNo RevDate
                    //hdnDocNo.Value = ds.Tables[12].Rows[0]["DocNo"].ToString();
                    //hdnRevNo.Value = ds.Tables[12].Rows[0]["RevNo"].ToString();
                    //hdnRevDate.Value = ds.Tables[12].Rows[0]["RevDate"].ToString();

                    ////gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                    ////gvQCObservationDetails_MC_P.DataBind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','MC');", true);
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            else if (ViewState["ProcessName"].ToString() == "Sheet Welding")
            {
                try
                {
                    string Page = url.Replace(Replacevalue, "SheetWeldingCardPrint.aspx?JCHID=" + JCHID + "");

                    string s = "window.open('" + Page + "','_blank');";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                    //objP.JCHID = Convert.ToInt32(JCHID);
                    //ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetWeldingDetailsByJCHID_PRINT");

                    //lblProcessName_SW_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //lblJobOrderID_SW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                    //lblProcessname_SW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                    //lblDate_SW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                    //lblRFPNo_SW_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                    //lblContractorName_SW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                    //lblContractorTeamname_SW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                    //lblItemNameSize_SW_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    //lblDrawingName_SW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    //lblPartname_SW_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                    ////  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                    //lblMaterialCategory_SW_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    //lblMaterialGrade_SW_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    //lblThickness_SW_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                    //lblMRNNumber_SW_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                    //lblJobOrderRemarks_SW_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                    //lblRemarks_SW_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                    //lblDeadlineDate_SW_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

                    //lbljobcardstatus_SW_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                    //if (ds.Tables[6].Rows.Count > 0)
                    //{
                    //    gvPartSno_SW_P.DataSource = ds.Tables[6];
                    //    gvPartSno_SW_P.DataBind();
                    //}

                    //gvWPSDetails_SW_P.DataSource = ds.Tables[5];
                    //gvWPSDetails_SW_P.DataBind();

                    ////if (ds.Tables[9].Rows.Count > 0)
                    ////{
                    ////    gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                    ////    gvMRNIssueDetails_SMC_P.DataBind();
                    ////}

                    //if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    //    lblOfferQCTest_SW_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                    //else
                    //    lblOfferQCTest_SW_P.Text = "";

                    //lblTotalCost_SW_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                    //lblPartQty_SW_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
                    //lblNOP_SW_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();
                    //ViewState["Address"] = ds.Tables[10];

                    //if (ds.Tables[11].Rows.Count > 0)
                    //{
                    //    gvbeforewelding_SW_P.DataSource = ds.Tables[11];
                    //    gvbeforewelding_SW_P.DataBind();
                    //}
                    //else
                    //{
                    //    gvbeforewelding_SW_P.DataSource = "";
                    //    gvbeforewelding_SW_P.DataBind();
                    //}

                    //if (ds.Tables[12].Rows.Count > 0)
                    //{
                    //    gvduringwelding_SW_P.DataSource = ds.Tables[12];
                    //    gvduringwelding_SW_P.DataBind();
                    //}
                    //else
                    //{
                    //    gvduringwelding_SW_P.DataSource = "";
                    //    gvduringwelding_SW_P.DataBind();
                    //}

                    //if (ds.Tables[13].Rows.Count > 0)
                    //{
                    //    gvfinalwelding_SW_P.DataSource = ds.Tables[13];
                    //    gvfinalwelding_SW_P.DataBind();
                    //}
                    //else
                    //{
                    //    gvfinalwelding_SW_P.DataSource = "";
                    //    gvfinalwelding_SW_P.DataBind();
                    //}

                    //cQRcode objQr = new cQRcode();

                    //string QrNumber = objQr.generateQRNumber(9);
                    //string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                    //string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                    //string QrCode = objQr.QRcodeGeneration(Code);
                    //if (QrCode != "")
                    //{
                    //    //imgQrcode.Attributes.Add("style", "display:block;");
                    //    //imgQrcode.ImageUrl = QrCode;
                    //    ViewState["QrCode"] = QrCode;
                    //    objQr.QRNumber = displayQrnumber;
                    //    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                    //    objQr.createdBy = objSession.employeeid;
                    //    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    //    objQr.Pagename = pageName;
                    //    objQr.saveQRNumber();
                    //}
                    //else
                    //    ViewState["QrCode"] = "";

                    //DataTable dtAddress = new DataTable();
                    //dtAddress = (DataTable)ViewState["Address"];

                    //hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                    //hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                    //hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                    //hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                    //hdnDocNo.Value = ds.Tables[14].Rows[0]["DocNo"].ToString();
                    //hdnRevNo.Value = ds.Tables[14].Rows[0]["RevNo"].ToString();
                    //hdnRevDate.Value = ds.Tables[14].Rows[0]["RevDate"].ToString();

                    ////gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                    ////gvQCObservationDetails_MC_P.DataBind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','SW');", true);

                    // GeneratePDFFile("SheetWelding");
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            else if (ViewState["ProcessName"].ToString() == "Fabrication & Welding")
            {
                try
                {
                    string Page = url.Replace(Replacevalue, "FabricationAndWelding.aspx?JCHID=" + JCHID + "");

                    string s = "window.open('" + Page + "','_blank');";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                    //objP.JCHID = Convert.ToInt32(JCHID);
                    //ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetFabricationAndWeldingDetailsByJCHID_PRINT");

                    //lblProcessName_FW_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //lblJobOrderID_FW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                    //lblProcessname_FW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                    //lblDate_FW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                    //lblRFPNo_FW_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                    //lblContractorName_FW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                    //lblContractorTeamname_FW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                    //lblItemNameSize_FW_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    //lblDrawingName_FW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    //lblPartname_FW_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                    ////  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                    //lblMaterialCategory_FW_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    //lblMaterialGrade_FW_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    //lblThickness_FW_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                    //lblMRNNumber_FW_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                    //lblJobOrderRemarks_FW_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                    //lblRemarks_FW_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                    //lblDeadLineDate_FW_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

                    //lbljobcardStatus_FW_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                    //if (ds.Tables[6].Rows.Count > 0)
                    //{
                    //    gvPartSno_FW_P.DataSource = ds.Tables[6];
                    //    gvPartSno_FW_P.DataBind();
                    //}

                    //gvWPSDetails_FW_P.DataSource = ds.Tables[5];
                    //gvWPSDetails_FW_P.DataBind();

                    ////if (ds.Tables[9].Rows.Count > 0)
                    ////{
                    ////    gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                    ////    gvMRNIssueDetails_SMC_P.DataBind();
                    ////}

                    //if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    //    lblOfferQCTest_FW_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                    //else
                    //    lblOfferQCTest_FW_P.Text = "";

                    //lblTotalCost_FW_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                    //lblPartQty_FW_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
                    //lblNOP_FW_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();
                    //ViewState["Address"] = ds.Tables[10];

                    //if (ds.Tables[9].Rows.Count > 0)
                    //{
                    //    string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                    //    string result = "";

                    //    lblFabricationType_FW_P.Text = ds.Tables[9].Rows[0]["FabricationTypeName"].ToString();

                    //    foreach (DataRow dr in ds.Tables[9].Rows)
                    //    {
                    //        result = "";
                    //        result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                    //        sb.Append(result);
                    //    }

                    //    divfabrication_FW_P.InnerHtml = sb.ToString();
                    //}
                    //else
                    //    divfabrication_FW_P.InnerHtml = "";

                    //// GeneratePDFFile("FabricationAndWelding");

                    //if (ds.Tables[11].Rows.Count > 0)
                    //{
                    //    gvBeforeWelding_FW_P.DataSource = ds.Tables[11];
                    //    gvBeforeWelding_FW_P.DataBind();
                    //}
                    //else
                    //{
                    //    gvBeforeWelding_FW_P.DataSource = "";
                    //    gvBeforeWelding_FW_P.DataBind();
                    //}

                    //if (ds.Tables[12].Rows.Count > 0)
                    //{
                    //    gvDuringWelding_FW_P.DataSource = ds.Tables[12];
                    //    gvDuringWelding_FW_P.DataBind();
                    //}
                    //else
                    //{
                    //    gvDuringWelding_FW_P.DataSource = "";
                    //    gvDuringWelding_FW_P.DataBind();
                    //}

                    //if (ds.Tables[13].Rows.Count > 0)
                    //{
                    //    gvFinalWelding_FW_P.DataSource = ds.Tables[13];
                    //    gvFinalWelding_FW_P.DataBind();
                    //}
                    //else
                    //{
                    //    gvFinalWelding_FW_P.DataSource = "";
                    //    gvFinalWelding_FW_P.DataBind();
                    //}

                    //cQRcode objQr = new cQRcode();

                    //string QrNumber = objQr.generateQRNumber(9);
                    //string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                    //string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                    //string QrCode = objQr.QRcodeGeneration(Code);
                    //if (QrCode != "")
                    //{
                    //    //imgQrcode.Attributes.Add("style", "display:block;");
                    //    //imgQrcode.ImageUrl = QrCode;
                    //    ViewState["QrCode"] = QrCode;
                    //    objQr.QRNumber = displayQrnumber;
                    //    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                    //    objQr.createdBy = objSession.employeeid;
                    //    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    //    objQr.Pagename = pageName;
                    //    objQr.saveQRNumber();
                    //}
                    //else
                    //    ViewState["QrCode"] = "";

                    //DataTable dtAddress = new DataTable();
                    //dtAddress = (DataTable)ViewState["Address"];

                    //hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                    //hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                    //hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                    //hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                    //hdnDocNo.Value = ds.Tables[14].Rows[0]["DocNo"].ToString();
                    //hdnRevNo.Value = ds.Tables[14].Rows[0]["RevNo"].ToString();
                    //hdnRevDate.Value = ds.Tables[14].Rows[0]["RevDate"].ToString();

                    ////gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                    ////gvQCObservationDetails_MC_P.DataBind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','FW');", true);
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            else if (ViewState["ProcessName"].ToString() == "Bellow Forming & Tangent Cutting")
            {
                try
                {
                    string Page = url.Replace(Replacevalue, "BellowFormingAndTangentCuttingPrint.aspx?JCHID=" + JCHID + "");

                    string s = "window.open('" + Page + "','_blank');";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                    //objP.JCHID = Convert.ToInt32(JCHID);
                    //ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetBellowFormingAndTangentCuttingDetailsByJCHID_PRINT");

                    //lblProcessName_BFTC_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //lblJobOrderID_BFTC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                    //lblProcessname_BFTC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                    //ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                    //lblDate_BFTC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                    //lblRFPNo_BFTC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                    //lblContractorName_BFTC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                    //lblContractorTeamname_BFTC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                    //lblItemNameSize_BFTC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    //lblDrawingName_BFTC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    //lblPartname_BFTC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                    ////  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                    //lblMaterialCategory_BFTC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    //lblMaterialGrade_BFTC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    //lblThickness_BFTC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                    //lblMRNNumber_BFTC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                    //lblJobOrderRemarks_BFTC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                    //lblremarks_BETC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                    //lbldeadlineDate_BFTC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();
                    //lbljobcardstatus_BFTC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                    //if (ds.Tables[6].Rows.Count > 0)
                    //{
                    //    gvPartSno_BFTC_P.DataSource = ds.Tables[6];
                    //    gvPartSno_BFTC_P.DataBind();
                    //}

                    //lblBellowDetails_BFTC_P.Text = "Bellow Details" + " ( " + ds.Tables[5].Rows[0]["FabricationTypeName"].ToString() + " )";

                    //if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    //    lblOfferQCTest_BFTC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                    //else
                    //    lblOfferQCTest_BFTC_P.Text = "";

                    //lblTotalCost_BFTC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                    //lblPartQty_BFTC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
                    //lblNOP_BFTC_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();

                    //ViewState["Address"] = ds.Tables[9];

                    //if (ds.Tables[5].Rows.Count > 0)
                    //{
                    //    string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:60%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                    //    string result = "";

                    //    foreach (DataRow dr in ds.Tables[5].Rows)
                    //    {
                    //        result = "";
                    //        result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                    //        sb.Append(result);
                    //    }

                    //    divBellowDetails_BFTC_P.InnerHtml = sb.ToString();
                    //}

                    //if (ds.Tables[5].Rows[0]["FabricationTypeName"].ToString() == "Roll")
                    //{
                    //    divNumberOfStages_BFTC_P.Attributes.Add("style", "display:block;");
                    //    gvNumberOfStages_BFTC_P.DataSource = ds.Tables[10];
                    //    gvNumberOfStages_BFTC_P.DataBind();
                    //}
                    //else
                    //    divNumberOfStages_BFTC_P.Attributes.Add("style", "display:none;");

                    //gvStageActivity_BFTC_P.DataSource = ds.Tables[11];
                    //gvStageActivity_BFTC_P.DataBind();

                    //cQRcode objQr = new cQRcode();

                    //string QrNumber = objQr.generateQRNumber(9);
                    //string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                    //string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                    //string QrCode = objQr.QRcodeGeneration(Code);
                    //if (QrCode != "")
                    //{
                    //    //imgQrcode.Attributes.Add("style", "display:block;");
                    //    //imgQrcode.ImageUrl = QrCode;
                    //    ViewState["QrCode"] = QrCode;
                    //    objQr.QRNumber = displayQrnumber;
                    //    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                    //    objQr.createdBy = objSession.employeeid;
                    //    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    //    objQr.Pagename = pageName;
                    //    objQr.saveQRNumber();
                    //}
                    //else
                    //    ViewState["QrCode"] = "";

                    //DataTable dtAddress = new DataTable();
                    //dtAddress = (DataTable)ViewState["Address"];

                    //hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                    //hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                    //hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                    //hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                    //hdnDocNo.Value = ds.Tables[12].Rows[0]["DocNo"].ToString();
                    //hdnRevNo.Value = ds.Tables[12].Rows[0]["RevNo"].ToString();
                    //hdnRevDate.Value = ds.Tables[12].Rows[0]["RevDate"].ToString();

                    ////gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                    ////gvQCObservationDetails_MC_P.DataBind();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','BFTC');", true);

                    //// GeneratePDFFile("BellowFormingAndTangentCutting");
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GeneratePDFFile(string ProcessName)
    {
        objc = new cCommon();
        try
        {
            string MAXPDFID = "";
            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));           

            //divLSERPLogo.Visible = false;

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                // imgQrcode.Attributes.Add("style", "display:block;");
                //  imgQrcode.ImageUrl = QrCode;
                ViewState["QrCode"] = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = ProcessName + "/" + ViewState["JobOrderID"].ToString();
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                ViewState["QrCode"] = "";



            //if (ProcessName == "SheetMarkingAndCutting")
            //{
            //    divSheetMarkingAndCuttingPDF.Visible = true;
            //    divSheetMarkingAndCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            //    divSheetMarkingAndCuttingPDF.Visible = false;
            //}
            //else if (ProcessName == "FabricationMarkingAndCutting")
            //{
            //    divMarkingAndCuttingPDF.Visible = true;
            //    divMarkingAndCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            //    divMarkingAndCuttingPDF.Visible = false;
            //}
            //else if (ProcessName == "SheetWelding")
            //{
            //    divSheetWeldingPDF.Visible = true;
            //    divSheetWeldingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            //    divSheetWeldingPDF.Visible = false;
            //}

            //else if (ProcessName == "FabricationAndWelding")
            //{
            //    divFabricationAndWeldingPDF.Visible = true;
            //    divFabricationAndWeldingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            //    divFabricationAndWeldingPDF.Visible = false;
            //}
            //else if (ProcessName == "BellowFormingAndTangentCutting")
            //{
            //    divBellowFormingAndTangentCuttingPDF.Visible = true;
            //    divBellowFormingAndTangentCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            //    divBellowFormingAndTangentCuttingPDF.Visible = false;
            //}

            //string div = sb.ToString();

            //MAXPDFID = objc.GetMaximumNumberPDF();
            //string htmlfile = ProcessName + "_" + ViewState["JobOrderID"].ToString() + ".html";
            //string pdffile = ProcessName + "_" + ViewState["JobOrderID"].ToString() + ".pdf";
            //string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            //string strFile = LetterPath + pdffile;

            //string URL = LetterPath + htmlfile;

            // SaveHtmlFile(URL, "Secondary Job Order Details", "", div);
            //GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            //objc.ReadhtmlFile(htmlfile, hdnpdfContent);

            ////divLSERPLogo.Visible = true;

            //objc.SavePDFFile("SecondaryJobOrderDetails.aspx", pdffile, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; }   .col-sm-12.contractorborder { padding-top:5px; border-left: 2px solid;border-right: 2px solid; border-bottom: 2px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");
            w.WriteLine("</head><body>");

            w.WriteLine("<div class='print-page'>");
            w.WriteLine("<table><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            w.WriteLine("<div class='header' style='border-bottom:1px solid;'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;'>");
            //  winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-6 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR </h3>");
            w.WriteLine("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("<p style='font-weight:500;color:#000;width: 103%;'>" + Address + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");
            w.WriteLine("<div class='col-sm-12 padding0' style='padding-top:0px;'>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot class='footer-space'><tr><td>");
            //
            w.WriteLine("<div class='col-sm-12'>");
            // w.WriteLine("<div class='footer' style='border: 0px solid #000 ! important;'>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:50px;'>");
            w.WriteLine("<div class='col-sm-6 p-t-20'><label style='color:black; font-weight:bolder;float:left;'>Quality Incharge</label></div>");
            w.WriteLine("<div class='col-sm-6' style='padding-left:16%;'><label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + ViewState["QrCode"].ToString() + "' class='Qrcode' /></div>");
            //  w.WriteLine("</div>");
            w.WriteLine("</div></div>");
            w.WriteLine("</td></tr></tfoot></table>");
            w.WriteLine("</div>");

            //w.WriteLine("<div style='text-align:center;padding-top:10px;font-size:20px;color:#00BCD4;'>");
            //w.WriteLine("</div>");
            //w.WriteLine("<div class='col-sm-12' style='text-align:center;padding-top:10px;font-size:20px;font-weight:bold;'>");
            //w.WriteLine("");
            //w.WriteLine("<div>");
            //w.WriteLine("<div class='col-sm-12' id='divLSERPLogo' style='padding-top: 30px;' runat='server'>");
            //w.WriteLine("<img src='" + topstrip + "' alt='' height='140px;'>");
            //w.WriteLine("</div>");
            //w.WriteLine(div);
            //w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GenerateAndSavePDF(string LetterPath, string strFile, string pdffile, string URL)
    {
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = new PdfDocument();
        try
        {
            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);
            if (File.Exists(strFile))
                File.Delete(strFile);

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 0;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 0;
            converter.Options.MarginBottom = 0;
            converter.Options.WebPageWidth = 700;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;

            doc = converter.ConvertUrl(URL);
            doc.Save(strFile);

            var ms = new System.IO.MemoryStream();

            //HttpContext.Current.Response.ContentType = "Application/pdf";
            //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdffile);
            //HttpContext.Current.Response.TransmitFile(strFile);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();

            doc.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            converter = null;
            doc = null;
            LetterPath = null;
            strFile = null;
            URL = null;
        }
    }

    public void SaveAlertDetails(string QCStatus)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 6;
            objc.JCHID = Convert.ToInt32(hdnJCHID.Value);
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

            if (QCStatus == "A")
                QCStatus = "Accepted";
            else if (QCStatus == "RW")
                QCStatus = "Reworked";
            else
                QCStatus = "Rejected";

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
                objAlerts.Subject = "Alert For Job Card QC Status";
                objAlerts.Message = "Job Card QC " + QCStatus + " From RFP No" + ds.Tables[0].Rows[0]["RFPNo"].ToString() + "And Job card No" + hdnJCHID.Value;
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

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvJobCardHeaderDetails.Rows.Count > 0)
            gvJobCardHeaderDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvJobCardPartDetails.Rows.Count > 0)
            gvJobCardPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}