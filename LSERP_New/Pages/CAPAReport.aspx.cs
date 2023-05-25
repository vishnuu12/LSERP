using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

public partial class Pages_CAPAReport : System.Web.UI.Page
{
    #region "Declaration"

    cReports objReport;
    cSession objSession = new cSession();

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
            {
                BindCustomername();
                BindRFPNoDetails();

                // BindCAPAReportDetails();
            }
            if (target == "CAPAPrint")
            {
                string CAPAID = arg.ToString();
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "CAPAReportPrint.aspx?CAPAID=" + CAPAID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }

            if (target == "CAPADelete")
            {
                try
                {
                    string CAPAID = arg.ToString();
                    DataSet ds = new DataSet();
                    objReport = new cReports();

                    objReport.CAPAID = CAPAID.ToString();

                    ds = objReport.DeleteCAPAReportByCAPAID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','CAPA Report Deleted Succesfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                    BindCAPAReportDetails();
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            if (target == "ShareRaisedProblem")
            {
                if (ddlNCStage.SelectedIndex > 0)
                {
                    objReport = new cReports();
                    DataSet ds = new DataSet();

                    objReport.QCStatus = 2;
                    objReport.CAPAID = hdnCAPAID.Value;
                    objReport.UserID = objSession.employeeid;

                    string AttachmentName = "";
                    string[] extension;

                    if (fpProblemraisedAttach.HasFile)
                    {
                        string extn = Path.GetExtension(fpProblemraisedAttach.PostedFile.FileName).ToUpper();
                        string Attchname = "";
                        AttachmentName = Path.GetFileName(fpProblemraisedAttach.PostedFile.FileName);
                        extension = AttachmentName.Split('.');

                        Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");
                        AttachmentName = Attchname + '.' + extension[extension.Length - 1];
                    }
                    objReport.AttachementName = AttachmentName;
                    objReport.ISOPDID = ddlNCStage.SelectedValue;
                    objReport.BellowSno = txtJobSno.Text;

                    ds = objReport.UpdateProblemRaisedQCStatusByCAPAID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Problem Raised to Production Successfully');", true);

                        string NCReportDocsSavePath = ConfigurationManager.AppSettings["NCReportDocsSavePath"].ToString();
                        string NCReportDocsHttpPath = ConfigurationManager.AppSettings["NCReportDocsHTTPPath"].ToString();

                        string StrStaffDocumentPath = NCReportDocsSavePath + "\\";

                        if (!Directory.Exists(StrStaffDocumentPath))
                            Directory.CreateDirectory(StrStaffDocumentPath);
                        if (AttachmentName != "")
                            fpProblemraisedAttach.SaveAs(StrStaffDocumentPath + AttachmentName);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "ErrorMessage('Error','Please Select NCR Stage Doc No');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        cCommon objc = new cCommon();
        try
        {
            if (ddlCustomerName.SelectedIndex > 0)
            {
                objc.ProspectID = Convert.ToInt32(ddlCustomerName.SelectedValue);
                //objc.userID = Convert.ToInt32(objSession.employeeid);
                objc.userID = 0;
                objc.GetRFPNoDetailsByProspectID(ddlRFPNo);
            }
            else
            {
                objReport = new cReports();
                objReport.GetRFPNoDetailsByCAPAReport(ddlRFPNo);
            }

            objc.EmptyDropDownList(ddlItemName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            hdnCAPAID.Value = "0";
            ddlCustomerName.SelectedIndex = 0;
            if (ddlRFPNo.SelectedIndex > 0)
            {
                cCommon objc = new cCommon();
                objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objc.GetCustomerNameDetailsByRFPHID();
                ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

                BindCAPAReportDetails();
                BindItemDetailsByRFPHID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    //NCRNo
    //NCRDate
    //InspectionFromDate
    //InspectionToDate
    //InspectionOfWelLengthInMeterPerQty
    //RepeatedReworkLocation
    //WeldDefectsFoundInMeter
    //DateFrom
    //RepeatedReworks

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objReport = new cReports();
        DataSet ds = new DataSet();
        try
        {
            objReport.CAPAID = hdnCAPAID.Value;
            //objReport.NCRNo = Convert.ToInt32(txtNCRNo.Text);
            //objReport.NCRDate = DateTime.ParseExact(txtNCRDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objReport.InspectionFromDate = DateTime.ParseExact(txtinspectionperiodfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objReport.txtinspectionperiodtodate = DateTime.ParseExact(txtinspectionperiodtodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objReport.InspectionOfWelLengthInMeterPerQty = txtweldlengthmeter.Text;
            objReport.RepeatedReworkLocation = txtrepeatedreworklocation.Text;
            objReport.WeldDefectsFoundInMeter = txtwelddefectsfoundinmeterinqty.Text;
            objReport.DataFrom = txtdatafrom.Text;
            objReport.RepeatedReworks = txtrepeatedreworks.Text;
            objReport.CreatedBy = objSession.employeeid;
            objReport.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objReport.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());

            ds = objReport.SaveBasicCapaReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','CAPA Data Added Successfullly');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','CAPA Data Updated Successfullly');", true);
            BindCAPAReportDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.CAPAID = hdnCAPAID.Value;
            objReport.CAPAASID = Convert.ToInt32(ddlCAPAActionState.SelectedValue);
            objReport.Description = txtDescription.Text;
            //objReport.ActionDate = DateTime.ParseExact(txtActionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objReport.InChargeName = txtinchargename.Text;
            objReport.CreatedBy = objSession.employeeid;

            ds = objReport.SaveCAPAActionDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','CAPA Data Added Successfullly');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','CAPA Data Updated Successfullly');", true);
            BindCAPAActionStateDetailsByCAPAID();
            BindCAPAActionState();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        hdnCAPAID.Value = "0";
        // txtActionDate.Text = "";
        txtdatafrom.Text = "";
        ddlItemName.SelectedIndex = 0;
        txtinchargename.Text = "";
        txtwelddefectsfoundinmeterinqty.Text = "";
        txtweldlengthmeter.Text = "";
    }

    #endregion

    #region"GridView Events"

    protected void gvCAPAReport_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnCAPAID.Value = gvCAPAReport.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "AddCAPA")
            {
                lblHeadeing_h.Text = ddlRFPNo.SelectedItem.Text + " / " + ddlItemName.SelectedItem.Text;

                BindCAPAActionState();
                BindCAPAActionStateDetailsByCAPAID();
                BindNCRDocNoList();

                DataSet ds = new DataSet();
                objReport = new cReports();
                objReport.CAPAID = hdnCAPAID.Value;
                ds = objReport.GetCAPAReportDetailsByCAPAID();

                lblweldlengthmeter_pp.Text = ds.Tables[0].Rows[0]["InspectionOfWelLengthInMeterPerQty"].ToString();
                lblinspectionfromdate_pp.Text = ds.Tables[0].Rows[0]["InspectionFromDate"].ToString();
                lblinspectiontodate_pp.Text = ds.Tables[0].Rows[0]["InspectionToDate"].ToString();
                lblrepeatedreworklocation_pp.Text = ds.Tables[0].Rows[0]["RepeatedReworkLocation"].ToString();
                lblwelddefectsfoundinmeter_pp.Text = ds.Tables[0].Rows[0]["WeldDefectsFoundInMeter"].ToString();
                lbldatafrom_pp.Text = ds.Tables[0].Rows[0]["DataFrom"].ToString();
                lblrepeatedreworks_pp.Text = ds.Tables[0].Rows[0]["RepeatedReworks"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "ShowActionStatePopUp();", true);
            }
            if (e.CommandName == "EditCAPAReport" || e.CommandName == "PrintCAPA")
            {
                if (e.CommandName == "EditCAPAReport")
                {
                    DataSet ds = new DataSet();
                    objReport = new cReports();

                    objReport.CAPAID = hdnCAPAID.Value;
                    ds = objReport.GetCAPAReportDetailsByCAPAID();

                    ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString();
                    txtweldlengthmeter.Text = ds.Tables[0].Rows[0]["InspectionOfWelLengthInMeterPerQty"].ToString();
                    txtinspectionperiodfromdate.Text = ds.Tables[0].Rows[0]["InspectionFromDate"].ToString();
                    txtinspectionperiodtodate.Text = ds.Tables[0].Rows[0]["InspectionToDate"].ToString();
                    txtrepeatedreworklocation.Text = ds.Tables[0].Rows[0]["RepeatedReworkLocation"].ToString();
                    txtwelddefectsfoundinmeterinqty.Text = ds.Tables[0].Rows[0]["WeldDefectsFoundInMeter"].ToString();
                    txtdatafrom.Text = ds.Tables[0].Rows[0]["DataFrom"].ToString();
                    txtrepeatedreworks.Text = ds.Tables[0].Rows[0]["RepeatedReworks"].ToString();
                }
                else if (e.CommandName == "PrintCAPA")
                {

                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCAPADetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objReport = new cReports();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int CAPADID = Convert.ToInt32(gvCAPADetails.DataKeys[index].Values[0].ToString());
            objReport.CAPADID = CAPADID;
            objReport.CAPAID = hdnCAPAID.Value;
            ds = objReport.DeleteCAPAActionReportDetailsByCAPADID("LS_DeleteCAPAActionReportDetailsByProcees1");

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','CAPA Data Deleted Successfullly');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            BindCAPAActionStateDetailsByCAPAID();
            BindCAPAActionState();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCAPADetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                if (dr["CAPAASID"].ToString() == ViewState["RowCount"].ToString())
                    btnDelete.Visible = dr["ProcessStep"].ToString() == "P1" ? true : false;
                else
                    btnDelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"
    private void BindCAPAReportDetails()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objReport.GetCAPAReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCAPAReport.DataSource = ds.Tables[0];
                gvCAPAReport.DataBind();
            }
            else
            {
                gvCAPAReport.DataSource = "";
                gvCAPAReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCAPAActionState()
    {
        objReport = new cReports();
        try
        {
            objReport.CAPAID = hdnCAPAID.Value;
            objReport.ProcessStep = "P1";
            objReport.GetCAPAActionState(ddlCAPAActionState);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindItemDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objReport.GetItemNameDetailsByRFPHIDCAPAReport(ddlItemName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCustomername()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            ds = objReport.GetCustomerNameDetailsByCAPAReport(ddlCustomerName);
            ViewState["CustomerDetails"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPNoDetails()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            ds = objReport.GetRFPNoDetailsByCAPAReport(ddlRFPNo);
            ViewState["RFPDetails"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCAPAActionStateDetailsByCAPAID()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.CAPAID = hdnCAPAID.Value;
            ds = objReport.GetCAPAActionStateDetailsByCAPAID();

            ViewState["RowCount"] = ds.Tables[0].Rows.Count;

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCAPADetails.DataSource = ds.Tables[0];
                gvCAPADetails.DataBind();
            }
            else
            {
                gvCAPADetails.DataSource = "";
                gvCAPADetails.DataBind();
            }


        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindNCRDocNoList()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = objReport.GetNCRDocNoList(ddlNCStage);

            ViewState["NCRStageDocList"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}