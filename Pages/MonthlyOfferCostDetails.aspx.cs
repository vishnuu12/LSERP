using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MonthlyOfferCostDetails : System.Web.UI.Page
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
                BindMonthlyOfferCostDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvOfferStatusHistory_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "OfferHistory")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                hdnEODID.Value = gvMonthlyOfferCostdetails.DataKeys[index].Values[0].ToString();

                Label lblenquirynumber = (Label)gvMonthlyOfferCostdetails.Rows[index].FindControl("lblenquiryNo");
                Label lblprospectname = (Label)gvMonthlyOfferCostdetails.Rows[index].FindControl("lblprospectname");

                lblEnquiryNo_h.Text = lblenquirynumber.Text + " / " + lblprospectname.Text;

                bindOfferStatusUpdatedetails();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Show", "ShowOfferStatusHistory();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMonthlyOfferCostDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objR.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objR.GetMonthlyOfferCostDetailsByMonthAndYear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyOfferCostdetails.DataSource = ds.Tables[0];
                gvMonthlyOfferCostdetails.DataBind();
            }
            else
            {
                gvMonthlyOfferCostdetails.DataSource = "";
                gvMonthlyOfferCostdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindOfferStatusUpdatedetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.EODID = Convert.ToInt32(hdnEODID.Value);
            ds = objR.GetOfferStatusUpdateHistoryDetailsByEODID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOfferStatusHistory.DataSource = ds.Tables[0];
                gvOfferStatusHistory.DataBind();
            }
            else
            {
                gvOfferStatusHistory.DataSource = "";
                gvOfferStatusHistory.DataBind();
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
        if (gvMonthlyOfferCostdetails.Rows.Count > 0)
        {
            gvMonthlyOfferCostdetails.UseAccessibleHeader = true;
            gvMonthlyOfferCostdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}