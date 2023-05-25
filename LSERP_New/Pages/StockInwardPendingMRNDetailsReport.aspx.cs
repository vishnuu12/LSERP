using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_StockInwardPendingMRNDetailsReport : System.Web.UI.Page
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
            dt.Columns.Remove("MID_CreatedDate");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";
            string LetterName = "StockInwardPending" + MAXEXID + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            _objc.exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "Stock Inward Pending MRN Report", "", 2, GemBoxKey);

            _objc.SaveExcelFile("StockInwardPendingMRNDetailsReport.aspx", LetterName, objSession.employeeid);

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

            ds = objR.GetStockInwardPendingMRNReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStockInwardPendingDetailsReport.DataSource = ds.Tables[0];
                gvStockInwardPendingDetailsReport.DataBind();

                lblstockinwardpendingmrnvalue.Text = ds.Tables[1].Rows[0]["IPC"].ToString();
            }
            else
            {
                gvStockInwardPendingDetailsReport.DataSource = "";
                gvStockInwardPendingDetailsReport.DataBind();

                lblstockinwardpendingmrnvalue.Text = "";
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
        if (gvStockInwardPendingDetailsReport.Rows.Count > 0)
        {
            gvStockInwardPendingDetailsReport.UseAccessibleHeader = true;
            gvStockInwardPendingDetailsReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}