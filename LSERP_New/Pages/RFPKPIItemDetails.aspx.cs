using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPKPIItemDetails : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cReports objR;

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                ViewState["RFPHID"] = Request.QueryString["RFPHID"].ToString();
                BindRFPItemDetailsByRFPHID();
            }
            if (target == "CAPAPrint")
            {
                string CAPAID = arg.ToString();
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "CAPAReportPrint.aspx?CAPAID=" + CAPAID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvRFPItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string RFPDID = gvRFPItemDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName.ToString() == "QPStatus")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "ViewRFPQualityPlanning.aspx?RFPDID=" + RFPDID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }

            if (e.CommandName.ToString() == "MPStatus")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "ViewRFPMaterialPlanningDetails.aspx?RFPDID=" + RFPDID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }

            if (e.CommandName.ToString() == "JOBStatus")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "RFPItemJobCardStatusReport.aspx?RFPDID=" + RFPDID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }

            if (e.CommandName.ToString() == "QCStatusCard")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "RFPQCSItemStatusCard.aspx?RFPDID=" + RFPDID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvRFPItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnPrint = (LinkButton)e.Row.FindControl("btnPrint");

                if (string.IsNullOrEmpty(dr["CAPAID"].ToString()))
                    btnPrint.Visible = false;
                else
                    btnPrint.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"

    protected void BindRFPItemDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());

            ds = objR.GetRFPKPIItemDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPItemDetails.DataSource = ds.Tables[0];
                gvRFPItemDetails.DataBind();
            }
            else
            {
                gvRFPItemDetails.DataSource = "";
                gvRFPItemDetails.DataBind();
            }

            lblRFPNo.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}