using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Pages_ApprovalPendingStatusReport : System.Web.UI.Page
{
    #region"DecLaration"

    cSales objSales;
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
            if (IsPostBack == false)
                bindApprovalPendingStatusReportDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bindApprovalPendingStatusReportDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindApprovalPendingStatusReportDetails()
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.UserID = Convert.ToInt32(objSession.employeeid);
            if (txtFromDate.Text == "")
                objSales.fromDate = "";
            else
                objSales.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (txtToDate.Text == "")
                objSales.toDate = "";
            else
                objSales.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objSales.GetApprovalPendingStatusReportByUserID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvApprovalPendingStatusReport.DataSource = ds.Tables[0];
                gvApprovalPendingStatusReport.DataBind();

                gvApprovalPendingStatusReport.UseAccessibleHeader = true;
                gvApprovalPendingStatusReport.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gvApprovalPendingStatusReport.DataSource = "";
                gvApprovalPendingStatusReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "ShowDataTable();", true);
    }

    #endregion
}