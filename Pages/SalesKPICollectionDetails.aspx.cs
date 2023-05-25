using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SalesKPICollectionDetails : System.Web.UI.Page
{
    #region"Declaration"

    cReports objRpt;
    cSession objSesion = new cSession();

    #endregion

    #region"Page Load Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["Month"] = Request.QueryString["Month"].ToString();
                ViewState["Year"] = Request.QueryString["Year"].ToString();
                BindSalesKPICollectionDetails();
                lblMonthYearName.Text = ViewState["Month"].ToString() + "/" + ViewState["Year"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindSalesKPICollectionDetails()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objRpt.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objRpt.GetSalesKPICollectionDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSalesKPICollectionDetails.DataSource = ds.Tables[0];
                gvSalesKPICollectionDetails.DataBind();
            }
            else
            {
                gvSalesKPICollectionDetails.DataSource = "";
                gvSalesKPICollectionDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}