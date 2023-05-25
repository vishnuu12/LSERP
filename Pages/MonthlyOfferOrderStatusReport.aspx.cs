using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MonthlyOfferOrderStatusReport : System.Web.UI.Page
{
    #region"DecLaration"

    cSales objSales;
    cSession objSession = new cSession();
    cReports objR;

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
            {
                // BindCurrentMonthYearDetails();
                divOutput.Visible = true;
                divInput.Visible = false;

                ViewState["Month"] = Request.QueryString["Month"].ToString();
                ViewState["Year"] = Request.QueryString["Year"].ToString();
                BindMonthlyOrderDetails();

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMonthYear_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonthYear.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                BindMonthlyOrderDetails();
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

    #region"GridView Events"

    protected void gvEnquiryProjectAssignmentReport_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  e.Row.Cells[0].Attributes.Add("style", "text-align:center;");

                if (string.IsNullOrEmpty(dr["Offer No"].ToString()))
                {
                    e.Row.Cells[3].Text = "NA";
                    e.Row.CssClass = "warning";
                }

                if (string.IsNullOrEmpty(dr["Order No"].ToString()))
                    e.Row.Cells[5].Text = "NA";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMonthlyOrderDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objR.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objR.GetMonthlyOrderDetailsByMonthAndYear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyOrderPODetails.DataSource = ds.Tables[0];
                gvMonthlyOrderPODetails.DataBind();
            }
            else
            {
                gvMonthlyOrderPODetails.DataSource = "";
                gvMonthlyOrderPODetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCurrentMonthYearDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.GetCurrentMonthYearDetails(ddlMonthYear);
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
        if (gvMonthlyOrderPODetails.Rows.Count > 0)
        {
            gvMonthlyOrderPODetails.UseAccessibleHeader = true;
            gvMonthlyOrderPODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}