using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InterDevelopementApproval : System.Web.UI.Page
{

    #region"Declarartion"

    cProduction objP;
    cSession objSession = new cSession();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                bindinternalDevelopementApprovalDetails();
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
        string BOMIDs = "";
        string Remarks = "";
        objP = new cProduction();
        try
        {
            string CommandName = ((Button)sender).CommandName;

            foreach (GridViewRow row in gvInternalDevelopementApprovalDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                if (chkitems.Checked)
                {
                    if (BOMIDs == "")
                        BOMIDs = gvInternalDevelopementApprovalDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    else
                        BOMIDs = BOMIDs + ',' + gvInternalDevelopementApprovalDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    if (Remarks == "")
                        Remarks = txtRemarks.Text;
                    else
                        Remarks = Remarks + '#' + txtRemarks.Text;
                }
            }

            if (CommandName == "Approve")
                objP.Flag = "Approve";
            else if (CommandName == "Reject")
                objP.Flag = "Reject";

            objP.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objP.UpdateInternalDevelopementMaterialRequestByBOMIDs(BOMIDs, Remarks);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Internal Developement Approved Successfully');", true);
                //  bindinternalDevelopementApprovalDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        bindinternalDevelopementApprovalDetails();
    }

    #endregion

    #region"Common Methods"

    private void bindinternalDevelopementApprovalDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            ds = objP.GetInternalDevelopementApprovalDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvInternalDevelopementApprovalDetails.DataSource = ds.Tables[0];
                gvInternalDevelopementApprovalDetails.DataBind();
            }
            else
            {
                gvInternalDevelopementApprovalDetails.DataSource = "";
                gvInternalDevelopementApprovalDetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvInternalDevelopementApprovedDetails.DataSource = ds.Tables[1];
                gvInternalDevelopementApprovedDetails.DataBind();
            }
            else
            {
                gvInternalDevelopementApprovedDetails.DataSource = "";
                gvInternalDevelopementApprovedDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvInternalDevelopementApprovalDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

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

    #endregion

    #region"PageLoad_Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvInternalDevelopementApprovedDetails.Rows.Count > 0)
            gvInternalDevelopementApprovedDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}