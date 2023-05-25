using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPItemExpensesSheet : System.Web.UI.Page
{
    #region "Declaration"

    cReports objRp;

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["RFPHID"] = Request.QueryString["RFPHID"].ToString();
                BindRFPItemExpensesDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GriView Events"

    protected void gvItemRFPExpensesSheet_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "ViewItemExpenses")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string RFPDID = gvItemRFPExpensesSheet.DataKeys[index].Values[0].ToString();

                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();
               
                string Page = url.Replace(Replacevalue, "ActualItemExpensesSheet.aspx?RFPDID=" + RFPDID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"

    private void BindRFPItemExpensesDetails()
    {
        DataSet ds = new DataSet();
        objRp = new cReports();
        try
        {
            objRp.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());
            ds = objRp.GetRFPItemExpensesDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItemRFPExpensesSheet.DataSource = ds.Tables[0];
                gvItemRFPExpensesSheet.DataBind();
            }
            else
            {
                gvItemRFPExpensesSheet.DataSource = "";
                gvItemRFPExpensesSheet.DataBind();
            }

            lblrfpno.Text = "RFP No " + ds.Tables[1].Rows[0]["RFPNo"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}