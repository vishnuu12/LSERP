using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_WorkOrderQCPlanning : System.Web.UI.Page
{
    #region"PageInit Events"

    cSession objSession = new cSession();
    cCommon objc;
    cProduction objP;
    cQuality objQ;

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
                BindWorkOrderIndentDetailsByItemID();
            }
            if (target == "ShareQC")
                ShareWorkOrderQC();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    #endregion

    #region"Button Events"

    protected void btnSaveQC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        string WOQCLMID = "";
        try
        {
            foreach (GridViewRow row in gvWorkOrderQC.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    if (WOQCLMID == "")
                        WOQCLMID = gvWorkOrderQC.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        WOQCLMID = WOQCLMID + "," + gvWorkOrderQC.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            objQ.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            objQ.WOQCLMID = WOQCLMID;

            ds = objQ.SaveWorkOrderQCPlanningDetauils();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Planning Details Saved Successfully');ShowQCPlanningPopUp();", true);
            BindWorkOrderQCListMasterDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvWorkOrderIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnWOIHID.Value = gvWorkOrderIndentDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "AddQCPlanning")
            {
                Label lblRFPNo = (Label)gvWorkOrderIndentDetails.Rows[index].FindControl("lblRFPNo");
                Label lblWONumber = (Label)gvWorkOrderIndentDetails.Rows[index].FindControl("lblWONumber");

                ViewState["RFPNo"] = lblRFPNo.Text;
                ViewState["WONumber"] = lblWONumber.Text;

                BindWorkOrderQCListMasterDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowQCPlanningPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvWorkOrderIndentQCListDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnWOIQCDID.Value = gvWorkOrderIndentQCListDetails.DataKeys[index].Values[0].ToString();
            if (e.CommandName == "deleteQCDetails")
            {
                DeleteWorkOrderIndentQCListDetails();
                BindWorkOrderQCListMasterDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowQCPlanningPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderQC_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");
                if (string.IsNullOrEmpty(dr["Status"].ToString()))
                    chkQC.Visible = true;
                else
                    chkQC.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindWorkOrderIndentDetailsByItemID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            ds = objP.GetWorkOrderIndentDetailsByQCPlanning();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderIndentDetails.DataSource = ds.Tables[0];
                gvWorkOrderIndentDetails.DataBind();
            }
            else
            {
                gvWorkOrderIndentDetails.DataSource = "";
                gvWorkOrderIndentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderQCListMasterDetails()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            ds = objQ.GetWorkOrderQCListMasterDetailsByQCOprationName();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderQC.DataSource = ds.Tables[0];
                gvWorkOrderQC.DataBind();
            }
            else
            {
                gvWorkOrderQC.DataSource = "";
                gvWorkOrderQC.DataBind();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderIndentQCListDetails.DataSource = ds.Tables[1];
                gvWorkOrderIndentQCListDetails.DataBind();
            }
            else
            {
                gvWorkOrderIndentQCListDetails.DataSource = "";
                gvWorkOrderIndentQCListDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteWorkOrderIndentQCListDetails()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.WOIQCDID = Convert.ToInt32(hdnWOIQCDID.Value);
            ds = objQ.DeleteWorkOrderIndentQCListDetailsByWOIQCDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','QC Details Deleted Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShareWorkOrderQC()
    {
        objQ = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            if (gvWorkOrderIndentQCListDetails.Rows.Count > 0)
            {
                objQ.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
                ds = objQ.UpdateWorkOrderShareQCStatusByWOIHID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Indent QC Shared Successfully');", true);
                    BindWorkOrderIndentDetailsByItemID();
                    SaveAlertDetails();
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No QC Added');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            ds = objc.GetEmployeeIDDetailsByUserTypeIDSANDErpUserType("8", 3);

            string[] str = ds.Tables[0].Rows[0]["EmployeeID"].ToString().Split(',');

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
                objAlerts.Subject = "Work Order Indent PO Alert";
                objAlerts.Message = "Work Order Indent Raised From RFP No" + ViewState["RFPNo"].ToString() + "And Indent No" + ViewState["WONumber"] + " Add Work Order PO";
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

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvWorkOrderIndentDetails.Rows.Count > 0)
            gvWorkOrderIndentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}