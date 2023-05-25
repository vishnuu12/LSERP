using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_NCQCApproval : System.Web.UI.Page
{
    #region "Declaration"

    cReports objReport;
    cSession objSession = new cSession();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region "Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                BindCAPARequestPendingDetails();
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

            if (target == "ShareRaisedProblem")
            {
                objReport = new cReports();
                DataSet ds = new DataSet();

                objReport.QCStatus = Convert.ToInt32(ddlQCStatus.SelectedValue);
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

                ds = objReport.UpdateFinalNCQCStatusByCAPAID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','QC Approved Successfully');", true);

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
                BindCAPARequestPendingDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"
    protected void btnAction_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.CAPAID = hdnCAPAID.Value;
            objReport.CAPAASID = Convert.ToInt32(ddlCAPAActionState.SelectedValue);
            objReport.Description = txtDescription.Text;
            // objReport.ActionDate = DateTime.ParseExact(txtActionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
    #endregion

    #region "Common Methods"

    private void BindCAPARequestPendingDetails()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.UserID = objSession.employeeid;
            ds = objReport.GetCAPARequestFinalApprovalDetails();

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
                BindCAPAActionState();
                BindCAPAActionStateDetailsByCAPAID();

                DataSet ds = new DataSet();
                objReport = new cReports();
                objReport.CAPAID = hdnCAPAID.Value;

                ds = objReport.GetCAPAReportDetailsByCAPAID();

                lblHeadeing_h.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString() + " / " + ds.Tables[0].Rows[0]["ItemName"].ToString() + " / " + ds.Tables[0].Rows[0]["DrawingName"].ToString();

                lblweldlengthmeter_pp.Text = ds.Tables[0].Rows[0]["InspectionOfWelLengthInMeterPerQty"].ToString();
                lblinspectionfromdate_pp.Text = ds.Tables[0].Rows[0]["InspectionFromDate"].ToString();
                lblinspectiontodate_pp.Text = ds.Tables[0].Rows[0]["InspectionToDate"].ToString();
                lblrepeatedreworklocation_pp.Text = ds.Tables[0].Rows[0]["RepeatedReworkLocation"].ToString();
                lblwelddefectsfoundinmeter_pp.Text = ds.Tables[0].Rows[0]["WeldDefectsFoundInMeter"].ToString();
                lbldatafrom_pp.Text = ds.Tables[0].Rows[0]["DataFrom"].ToString();
                lblrepeatedreworks_pp.Text = ds.Tables[0].Rows[0]["RepeatedReworks"].ToString();
                lblQCRequestOn_pp.Text = ds.Tables[0].Rows[0]["QCRequestOn"].ToString();
                lblQCRequestBy_pp.Text = ds.Tables[0].Rows[0]["QCNCRequestedBy"].ToString();

                lblproductionDoneOn_pp.Text = ds.Tables[0].Rows[0]["ProductionDoneOn"].ToString();
                lblproductionDoneBy_pp.Text = ds.Tables[0].Rows[0]["ProductionNCDoneBy"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "ShowActionStatePopUp();", true);
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

            ds = objReport.DeleteCAPAActionReportDetailsByCAPADID("LS_DeleteCAPAActionReportDetailsByProcees3");

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
                    btnDelete.Visible = dr["ProcessStep"].ToString() == "P3" ? true : false;
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

    #region "Common Methods"

    private void BindCAPAActionState()
    {
        objReport = new cReports();
        try
        {
            objReport.CAPAID = hdnCAPAID.Value;
            objReport.ProcessStep = "P3";
            objReport.GetCAPAActionState(ddlCAPAActionState);
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

    #endregion
}