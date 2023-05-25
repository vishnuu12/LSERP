using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_NCReportDetailsByRFPHID : System.Web.UI.Page
{
    #region "Declaration"

    cReports objReport;
    cSession objSession = new cSession();

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                ViewState["RFPHID"] = Request.QueryString["RFPHID"].ToString();
                BindCAPAReportDetails();
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

    #region"Common Methods"
    private void BindCAPAReportDetails()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());

            ds = objReport.GetCAPAReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCAPAReport.DataSource = ds.Tables[0];
                gvCAPAReport.DataBind();
            }
            else
            {
                gvCAPAReport.DataSource = "";
                gvCAPAReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}