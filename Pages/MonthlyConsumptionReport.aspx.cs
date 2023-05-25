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

public partial class Pages_MonthlyConsumptionReport : System.Web.UI.Page
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
            DataTable dt2 = new DataTable();
            dt = (DataTable)ViewState["Job"];
            dt2 = (DataTable)ViewState["RFP"];

            dt.Columns.Remove("JCHID");
            dt.Columns.Remove("SecondaryJidSuffix");

            MAXEXID = _objc.GetMaximumNumberExcel();

            //int rowcount = Convert.ToInt32(dt.Rows.Count);
            //int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "MonthlyConsumption" + "-" + ViewState["Month"].ToString() + "-" + ViewState["Year"].ToString() + "-" + MAXEXID + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            // exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "Monthly Consumption Report", "", 2, GemBoxKey);

            exportExcel(dt, dt2, strFile, LetterName, GemBoxKey);

            _objc.SaveExcelFile("MonthlyConsumptionReport.aspx", LetterName, objSession.employeeid);

            //bindMaterialMaster();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    public void exportExcel(DataTable dt, DataTable dt2, string fileName, string LetterName, string gemBoxKey)
    {
        try
        {

            int ColumnCount = Convert.ToInt32(dt.Columns.Count);
            int ColumnCount2 = Convert.ToInt32(dt2.Columns.Count);

            if ((dt.Rows.Count > 0) && (dt2.Rows.Count > 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                ExcelFile workbook = new ExcelFile();
                ExcelWorksheet worksheet;
                ExcelWorksheet worksheet2;
                worksheet2 = workbook.Worksheets.Add("RFP Consumption");
                worksheet = workbook.Worksheets.Add("Material Consumption");

                worksheet2 = exportex(dt2, dt2.Rows.Count, ColumnCount2, LetterName,
                    "RFP Consumption Details / " + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, worksheet2);
                worksheet = exportex(dt, dt.Rows.Count, ColumnCount, LetterName,
                    "Monthly Consumption Details" + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, worksheet);

                workbook.Save(fileName);

                var response = HttpContext.Current.Response;
                var options = SaveOptions.XlsDefault;

                response.Clear();
                response.ContentType = options.ContentType;
                response.AddHeader("Content-Disposition", "attachment; filename=" + LetterName);

                var ms = new System.IO.MemoryStream();
                workbook.Save(ms, options);
                ms.WriteTo(response.OutputStream);

                response.Flush();
                response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
        }
        catch (Exception ex)
        {

            Log.Message(ex.ToString());
        }
    }

    public ExcelWorksheet exportex(DataTable dt, int rowCount, int columnCount, string LetterName, string schoolName, string reportName, int startRow, ExcelWorksheet worksheet)
    {
        try
        {

            worksheet.Rows[startRow].Style.Font.Weight = ExcelFont.BoldWeight;

            columnCount = columnCount - 1;

            worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

            if (schoolName != "")
            {
                worksheet.Cells[0, 0].Value = schoolName;
                worksheet.Cells[0, 0].Style.Font.Name = "Times New Roman";
                worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Merged = true;
                worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Color = SpreadsheetColor.FromName(ColorName.DarkRed);
                worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Size = 18 * 20;
                worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            }

            if (reportName != "")
            {
                worksheet.Cells[1, 0].Value = reportName;
                worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Merged = true;
                worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Size = 18 * 20;
                worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            }

            if (startRow == 0)
                startRow = 2;

            worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;

            // Insert DataTable to an Excel worksheet . //
            worksheet.InsertDataTable(dt,
               new InsertDataTableOptions()
               {
                   ColumnHeaders = true,
                   StartRow = 2
               });

            for (int i = 0; i < worksheet.CalculateMaxUsedColumns(); i++)
                worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return worksheet;
    }

    private void BindMonthlyStockConsumptionDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objR.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objR.GetMonthlyConsumptionStockReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyConsumptionReportDetails.DataSource = ds.Tables[0];
                gvMonthlyConsumptionReportDetails.DataBind();
            }
            else
            {
                gvMonthlyConsumptionReportDetails.DataSource = "";
                gvMonthlyConsumptionReportDetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvRFPConsumptionReportDetails.DataSource = ds.Tables[1];
                gvRFPConsumptionReportDetails.DataBind();
            }
            else
            {
                gvRFPConsumptionReportDetails.DataSource = "";
                gvRFPConsumptionReportDetails.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                gvGeneralMaterialIssueDetails.DataSource = ds.Tables[3];
                gvGeneralMaterialIssueDetails.DataBind();
            }
            else
            {
                gvGeneralMaterialIssueDetails.DataSource = "";
                gvGeneralMaterialIssueDetails.DataBind();
            }

            ViewState["Job"] = ds.Tables[0];
            ViewState["RFP"] = ds.Tables[1];
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
        if (gvMonthlyConsumptionReportDetails.Rows.Count > 0)
        {
            gvMonthlyConsumptionReportDetails.UseAccessibleHeader = true;
            gvMonthlyConsumptionReportDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvRFPConsumptionReportDetails.Rows.Count > 0)
        {
            gvRFPConsumptionReportDetails.UseAccessibleHeader = true;
            gvRFPConsumptionReportDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}