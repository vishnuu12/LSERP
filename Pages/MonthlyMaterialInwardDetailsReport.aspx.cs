using eplus.core;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MonthlyMaterialInwardDetailsReport : System.Web.UI.Page
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
            ViewState["Month"] = Request.QueryString["Month"].ToString();
            ViewState["Year"] = Request.QueryString["Year"].ToString();
            BindMonthlyStockConsumptionDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        cCommon _objc = new cCommon();
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];

            dt.Columns.Remove("MRNID");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "MonthlyStockInward" + "-" + ViewState["Month"].ToString() + "-" + ViewState["Year"].ToString() + "-" + MAXEXID + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            _objc.exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "Monthly Stock MRN Inward Report", "", 2, GemBoxKey);

            _objc.SaveExcelFile("MonthlyMaterialInwardDetailsReport.aspx", LetterName, objSession.employeeid);

            //bindMaterialMaster();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMonthlyStockConsumptionDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objR.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objR.GetMonthlyMaterialInwardMaterialReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyMaterialInwardReportDetails.DataSource = ds.Tables[0];
                gvMonthlyMaterialInwardReportDetails.DataBind();
            }
            else
            {
                gvMonthlyMaterialInwardReportDetails.DataSource = "";
                gvMonthlyMaterialInwardReportDetails.DataBind();
            }

            ViewState["DataTable"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"  

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvMonthlyMaterialInwardReportDetails.Rows.Count > 0)
        {
            gvMonthlyMaterialInwardReportDetails.UseAccessibleHeader = true;
            gvMonthlyMaterialInwardReportDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}