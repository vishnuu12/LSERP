using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ActualItemExpensesSheet : System.Web.UI.Page
{
    #region "Declaration"

    cReports objRp;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["RFPDID"] = Request.QueryString["RFPDID"].ToString();
            BindItemExpensesDetails();
        }
    }

    #endregion

    #region"Common Methods"

    private void BindItemExpensesDetails()
    {
        DataSet ds = new DataSet();
        objRp = new cReports();
        try
        {
            objRp.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objRp.GetItemExpensesDetailsByRFPDID();

            lblrfpnoitemname.Text = ds.Tables[3].Rows[0]["ItemName"].ToString() + "  /  " + ds.Tables[3].Rows[0]["RFPNo"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobCardDetails.DataSource = ds.Tables[0];
                gvJobCardDetails.DataBind();
            }
            else
            {
                gvJobCardDetails.DataSource = "";
                gvJobCardDetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvworkorderpo.DataSource = ds.Tables[1];
                gvworkorderpo.DataBind();
            }
            else
            {
                gvworkorderpo.DataSource = "";
                gvworkorderpo.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvContractorexpensesdetails.DataSource = ds.Tables[2];
                gvContractorexpensesdetails.DataBind();
            }
            else
            {
                gvContractorexpensesdetails.DataSource = "";
                gvContractorexpensesdetails.DataBind();
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