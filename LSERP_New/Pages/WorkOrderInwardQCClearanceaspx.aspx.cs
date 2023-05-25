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

public partial class Pages_WorkOrderInwardQCClearanceaspx : System.Web.UI.Page
{

    #region"PageInit Events"

    cCommon objc;
    cQuality objQ;
    cProduction objP;
    cSession objSession = new cSession();
    string QualityReportSavePath = ConfigurationManager.AppSettings["QualityReportSavePath"].ToString();
    string QualityReportHttpPath = ConfigurationManager.AppSettings["QualityReportHttpPath"].ToString();

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
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (!IsPostBack)
                bindWorkOrderInwardedIndentDetails();

            else
            {
                if (target == "ViewWODrawingFile")
                    ViewWorkOrderDrawingFileName(Convert.ToInt32(arg.ToString()));
                if (target == "QCApprove")
                    SaveWorkOrderQCApprove(arg.ToString());

                if (target == "UpdateInwardQty")
                    UpdateInwardQtyEntirelyTheIndentByWOIHID(arg.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveQCApprove_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvWorkOrderInwardedIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "AddQC")
            {
                Label lblIndentNo = (Label)gvWorkOrderInwardedIndentDetails.Rows[index].FindControl("lblIndentNo");
                Label lblTotalIndenQty = (Label)gvWorkOrderInwardedIndentDetails.Rows[index].FindControl("lblTotalIndenQty");
                Label lblPart = (Label)gvWorkOrderInwardedIndentDetails.Rows[index].FindControl("lblPartName");

                lblheadername_p.Text = lblIndentNo.Text + " " + lblPart.Text;
                lblIndentQty.Text = lblTotalIndenQty.Text;

                ViewState["WOIHID"] = gvWorkOrderInwardedIndentDetails.DataKeys[index].Values[2].ToString();
                BindPartSnoByWorkOrderIndentNo();
                BindQCListDetailsByWorkOrderIndentNo();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowQCClearancePopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPartSnoDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");
                if (dr["QCCheckedStatus"].ToString() == "7" || dr["QCCheckedStatus"].ToString() == "0")
                    chkQC.Visible = true;
                else if (dr["QCCheckedStatus"].ToString() == "9")
                    chkQC.Visible = false;
                else if (dr["QCCheckedStatus"].ToString() == "1" && dr["JobAvailQty"].ToString() == "0")
                    chkQC.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvQCApplicableList_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderInwardedIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnUpdateAll = (LinkButton)e.Row.FindControl("btnUpdateAll");
                if (objSession.type == 1)
                    btnUpdateAll.Visible = true;
                else
                    btnUpdateAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindWorkOrderInwardedIndentDetails()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.UserID = objSession.employeeid;
            ds = objQ.GetWorkOrderInwarddedIndentDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderInwardedIndentDetails.DataSource = ds.Tables[0];
                gvWorkOrderInwardedIndentDetails.DataBind();
            }
            else
            {
                gvWorkOrderInwardedIndentDetails.DataSource = "";
                gvWorkOrderInwardedIndentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void ViewWorkOrderDrawingFileName(int index)
    {
        objc = new cCommon();
        try
        {
            string FileName = gvWorkOrderInwardedIndentDetails.DataKeys[index].Values[0].ToString();
            string WONo = gvWorkOrderInwardedIndentDetails.DataKeys[Convert.ToInt32(index)].Values[1].ToString();
            objc.ViewFileName(Session["WorkOrderIndentSavePath"].ToString(), Session["WorkOrderIndentHttpPath"].ToString(), FileName, WONo, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartSnoByWorkOrderIndentNo()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.WOIHID = Convert.ToInt32(ViewState["WOIHID"].ToString());
            ds = objP.GetPartSnoByWorkOrderIndentNo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderPartSnoDetails.DataSource = ds.Tables[0];
                gvWorkOrderPartSnoDetails.DataBind();
            }
            else
            {
                gvWorkOrderPartSnoDetails.DataSource = "";
                gvWorkOrderPartSnoDetails.DataBind();
            }

            lblInwardQty.Text = ds.Tables[1].Rows[0]["InwardQty"].ToString();
            lblProcessedQty.Text = ds.Tables[1].Rows[0]["ProcessedQty"].ToString();
            lblAvailableQty.Text = hdnQCAvailQty.Value = ds.Tables[1].Rows[0]["AvailQty"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindQCListDetailsByWorkOrderIndentNo()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.WOIHID = Convert.ToInt32(ViewState["WOIHID"].ToString());
            ds = objQ.GetQCListDetailsByWorkOrderIndentNo();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQCApplicableList.DataSource = ds.Tables[0];
                gvQCApplicableList.DataBind();
            }
            else
            {
                gvQCApplicableList.DataSource = "";
                gvQCApplicableList.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void SaveWorkOrderQCApprove(string Status)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        cSession objS = new cSession();
        string WOIDIDs = "";
        string WOQCLMIDs = "";
        string AttachmentName = "";
        try
        {
            foreach (GridViewRow row in gvWorkOrderPartSnoDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                TextBox txtInwardQty = (TextBox)row.FindControl("txtInwardQty");

                if (chk.Checked && Convert.ToInt32(txtInwardQty.Text) > 0)
                {
                    if (WOIDIDs == "")
                        WOIDIDs = gvWorkOrderPartSnoDetails.DataKeys[row.RowIndex].Values[0].ToString() + "-" + txtInwardQty.Text;
                    else
                        WOIDIDs = WOIDIDs + "," + gvWorkOrderPartSnoDetails.DataKeys[row.RowIndex].Values[0].ToString() + "-" + txtInwardQty.Text;
                }
            }

            foreach (GridViewRow row in gvQCApplicableList.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    if (WOQCLMIDs == "")
                        WOQCLMIDs = gvQCApplicableList.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        WOQCLMIDs = WOQCLMIDs + "," + gvQCApplicableList.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            objQ.Remarks = txtQCRemarks.Text;
            objQ.Status = Status;

            if (fpQCReport.HasFile)
            {
                string extn = Path.GetExtension(fpQCReport.PostedFile.FileName).ToUpper();

                AttachmentName = Path.GetFileName(fpQCReport.PostedFile.FileName);
                string[] extension = AttachmentName.Split('.');
                AttachmentName = extension[0] + '_' + "WOQC" + '.' + extension[1];
            }
            objQ.AttachementName = AttachmentName;
            objQ.CreatedBy = objSession.employeeid;
            objQ.WOIHID = Convert.ToInt32(ViewState["WOIHID"].ToString());

            ds = objQ.SaveWorkOrderInwardQCClearanceDetails(WOIDIDs, WOQCLMIDs);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Saved Succesfully');HideQCClearancePopUp();", true);
                BindPartSnoByWorkOrderIndentNo();
                BindQCListDetailsByWorkOrderIndentNo();
                objc = new cCommon();
                objc.Foldername = QualityReportSavePath;
                objc.FileName = AttachmentName;
                objc.PID = "WorkOrderQCReport";
                objc.AttachementControl = fpQCReport;
                objc.SaveFiles();

                BindPartSnoByWorkOrderIndentNo();
                BindQCListDetailsByWorkOrderIndentNo();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');HideQCClearancePopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        bindWorkOrderInwardedIndentDetails();
    }

    private void UpdateInwardQtyEntirelyTheIndentByWOIHID(string WOIHID)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.UserID = objSession.employeeid;
            objQ.WOIHID = Convert.ToInt32(WOIHID);
            ds = objQ.UpdateInwardQtyEntireIndentByWOIHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Everything Equally Updated   Succesfully');", true);
                bindWorkOrderInwardedIndentDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvWorkOrderInwardedIndentDetails.Rows.Count > 0)
            gvWorkOrderInwardedIndentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}