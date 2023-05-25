using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MonthlyKPIDetails : System.Web.UI.Page
{
    #region "Declaration"

    cReports objRpt;
    cSession objSesion = new cSession();

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["Month"] = Request.QueryString["Month"].ToString();
                ViewState["Year"] = Request.QueryString["Year"].ToString();
                BindMonthlyKPIDetails();

                lblmonthname.Text = "KPI For The Month Of " + ViewState["Month"].ToString() + "/" + ViewState["Year"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMonthlyKPIDetails()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objRpt.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objRpt.GetMonthlyKPIDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSalesKPIReports.DataSource = ds.Tables[0];
                gvSalesKPIReports.DataBind();
            }
            else
            {
                gvSalesKPIReports.DataSource = "";
                gvSalesKPIReports.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}