using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_DesignPOReviewStatusForMarketingAndDesign : System.Web.UI.Page
{
    #region"DecLaration"

    cSession objSession = new cSession();
    cCommon objc;

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
                BindEnquiryProjectAssignmentDetails();
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
            BindEnquiryProjectAssignmentDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvEnquiryProjectAssignmentReport_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (string.IsNullOrEmpty(dr["Design PO Date"].ToString()))
                {
                    e.Row.Cells[5].Text = "NA";
                    e.Row.CssClass = "warning";
                }
                if (string.IsNullOrEmpty(dr["Sales PO Date"].ToString()))
                {
                    e.Row.Cells[6].Text = "NA";
                    e.Row.CssClass = "warning";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindEnquiryProjectAssignmentDetails()
    {
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            objc.UserID = Convert.ToInt32(objSession.employeeid);
            if (txtFromDate.Text == "")
                objc.fromDate = "";
            else
                objc.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (txtToDate.Text == "")
                objc.toDate = "";
            else
                objc.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            objc.UserTypeID = objSession.type;

            ds = objc.GetEnquiryClarrificationForMarketingAndDesign("LS_GetPOContractReviewStatusForSalesAndMarketing");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEnquiryProjectAssignmentReport.DataSource = ds.Tables[0];
                gvEnquiryProjectAssignmentReport.DataBind();
            }
            else
            {
                gvEnquiryProjectAssignmentReport.DataSource = "";
                gvEnquiryProjectAssignmentReport.DataBind();
            }

            lblMenuHeadeing.Text = lblHeadeing.Text = ds.Tables[1].Rows[0]["Heading"].ToString();
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
        if (gvEnquiryProjectAssignmentReport.Rows.Count > 0)
        {
            gvEnquiryProjectAssignmentReport.UseAccessibleHeader = true;
            gvEnquiryProjectAssignmentReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }

    #endregion
}