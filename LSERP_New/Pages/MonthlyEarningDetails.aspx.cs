using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MonthlyEarningDetails : System.Web.UI.Page
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
                BindYear();
                divOutput.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlYear_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlYear.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                BindMonthlyEarningDetails();
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

    protected void gvMonthlyEarningDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string[] MonthYear;
            string Page = "";

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            if (e.CommandName.ToString() == "viewMonthlyOrder")
            {
                MonthYear = gvMonthlyEarningDetails.DataKeys[index].Values[0].ToString().Split('/');
                Page = url.Replace(Replacevalue, "MonthlyOfferOrderStatusReport.aspx?Month=" + MonthYear[0] + "&&Year=" + MonthYear[1] + "");
            }
            else if (e.CommandName.ToString() == "viewMonthlyOffer")
            {
                MonthYear = gvMonthlyOfferValues.DataKeys[index].Values[0].ToString().Split('/');
                Page = url.Replace(Replacevalue, "MonthlyOfferCostDetails.aspx?Month=" + MonthYear[0] + "&&Year=" + MonthYear[1] + "");
            }

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindYear()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.GetYearDetails(ddlYear);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMonthlyEarningDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Year = Convert.ToInt32(ddlYear.SelectedValue);
            ds = objR.GetMonthlyEarningDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyEarningDetails.DataSource = ds.Tables[0];
                gvMonthlyEarningDetails.DataBind();
            }
            else
            {
                gvMonthlyEarningDetails.DataSource = "";
                gvMonthlyEarningDetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvMonthlyOfferValues.DataSource = ds.Tables[1];
                gvMonthlyOfferValues.DataBind();
            }
            else
            {
                gvMonthlyOfferValues.DataSource = "";
                gvMonthlyOfferValues.DataBind();
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
        if (gvMonthlyEarningDetails.Rows.Count > 0)
        {
            gvMonthlyEarningDetails.UseAccessibleHeader = true;
            gvMonthlyEarningDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvMonthlyOfferValues.Rows.Count > 0)
        {
            gvMonthlyOfferValues.UseAccessibleHeader = true;
            gvMonthlyOfferValues.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}