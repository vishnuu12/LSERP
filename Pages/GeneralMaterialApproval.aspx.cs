using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralMaterialApproval : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cStores objSt;

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
            if (!IsPostBack)
                bindGeneralmaterialIssueDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        bool msg = true;
        string returnmsg = "";
        try
        {
            objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
            foreach (GridViewRow row in gvGeneralMRNIssueDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    objSt.GMIDID = gvGeneralMRNIssueDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    objSt.Flag = "Approve";
                    ds = objSt.UpdateGeneralMaterialApprovalDetails();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                        msg = true;
                    else
                    {
                        msg = false;
                        returnmsg = ds.Tables[0].Rows[0]["Message"].ToString();
                        break;
                    }
                }
            }

            if (msg)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','General Material Issue Details Saved successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + returnmsg + "');", true);
            bindGeneralmaterialIssueDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
            foreach (GridViewRow row in gvGeneralMRNIssueDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    objSt.GMIDID = gvGeneralMRNIssueDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    objSt.Flag = "Reject";
                    ds = objSt.UpdateGeneralMaterialApprovalDetails();
                }
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','General MRN Issue Rejected successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvGeneralMRNIssueDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            objSt.MRNID = Convert.ToInt32(gvGeneralMRNIssueDetails.DataKeys[index].Values[1].ToString());
            objSt.LocationID = Convert.ToInt32(gvGeneralMRNIssueDetails.DataKeys[index].Values[2].ToString()); ;

            ds = objSt.GetStockMonitorReportDetailsbyMRNID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Remove("Cost");

                gvStockMonitorDetails.DataSource = ds.Tables[0];
                gvStockMonitorDetails.DataBind();
            }
            else
            {
                gvStockMonitorDetails.DataSource = "";
                gvStockMonitorDetails.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ShowStockPopup();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindGeneralmaterialIssueDetails()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            ds = objSt.BindGeneralMaterialRequestDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralMRNIssueDetails.DataSource = ds.Tables[0];
                gvGeneralMRNIssueDetails.DataBind();
            }
            else
            {
                gvGeneralMRNIssueDetails.DataSource = "";
                gvGeneralMRNIssueDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}