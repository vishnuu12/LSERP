using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class Pages_GRNStockReports : System.Web.UI.Page
{
    #region"Declaration"

    cPurchase objPc;
    cCommon objc;
    cStores objSt;
    cSession objSession = new cSession();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;

            DateTimeOffset d1 = DateTimeOffset.UtcNow;
            long d11 = d1.ToUnixTimeMilliseconds();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
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
                GetGRNReports();

            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void GetGRNReports()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            ds = objSt.GetGRNReportDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGRNReports.DataSource = ds.Tables[0];
                gvGRNReports.DataBind();
            }
            else
            {
                gvGRNReports.DataSource = "";
                gvGRNReports.DataBind();
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
        if (gvGRNReports.Rows.Count > 0)
            gvGRNReports.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion




}