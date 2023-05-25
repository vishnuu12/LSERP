using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AllDepartMentApprovalPendingStatusReport : System.Web.UI.Page
{

    #region"pageLoad Events"

    cReports objR;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlApprovalname_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlApprovalname.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                BindPendingStatusReportDetails();
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

    #region"Common Methods"

    private void BindPendingStatusReportDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.ApprovalName = ddlApprovalname.SelectedValue;
            ds = objR.GetApprovalPendingStatusReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAllDepartMentApprovalPendingStatus.DataSource = ds.Tables[0];
                gvAllDepartMentApprovalPendingStatus.DataBind();
            }
            else
            {
                gvAllDepartMentApprovalPendingStatus.DataSource = "";
                gvAllDepartMentApprovalPendingStatus.DataBind();
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
        if (gvAllDepartMentApprovalPendingStatus.Rows.Count > 0)
            gvAllDepartMentApprovalPendingStatus.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}