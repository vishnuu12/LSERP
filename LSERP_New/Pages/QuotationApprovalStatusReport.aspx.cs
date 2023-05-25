using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_QuotationApprovalStatusReport : System.Web.UI.Page
{
    #region"Reports"

    cReports objRpt;

    #endregion

    #region"Page Load Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
                BindQuotationApprovalPendingStatusReport();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindQuotationApprovalPendingStatusReport()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            ds = objRpt.GetQuotationApprovalPendingStatusReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQuotationApprovalStatusReport.DataSource = ds.Tables[0];
                gvQuotationApprovalStatusReport.DataBind();
            }
            else
            {
                gvQuotationApprovalStatusReport.DataSource = "";
                gvQuotationApprovalStatusReport.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvApprovedQuotationList.DataSource = ds.Tables[1];
                gvApprovedQuotationList.DataBind();
            }
            else
            {
                gvApprovedQuotationList.DataSource = "";
                gvApprovedQuotationList.DataBind();
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
        if (gvQuotationApprovalStatusReport.Rows.Count > 0)
            gvQuotationApprovalStatusReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvApprovedQuotationList.Rows.Count > 0)
            gvApprovedQuotationList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}