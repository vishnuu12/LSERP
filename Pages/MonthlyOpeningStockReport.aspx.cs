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

public partial class Pages_MonthlyOpeningStockReport : System.Web.UI.Page
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
            BindMonthlyOpeningStockReportDetails();
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

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "MonthlyOpeningStock" + "-" + ViewState["Month"].ToString() + "-" + ViewState["Year"].ToString() + "-" + MAXEXID + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, strFile, LetterName, GemBoxKey);

            _objc.SaveExcelFile("MonthlyOpeningStockReport.aspx", LetterName, objSession.employeeid);

            //bindMaterialMaster();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMonthlyOpeningStockReportDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objR.Year = Convert.ToInt32(ViewState["Year"].ToString());

            ds = objR.GetMonthlyOpeningStockReportDetailsByMonthAndYear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyOpeningStockreportDetails.DataSource = ds.Tables[0];
                gvMonthlyOpeningStockreportDetails.DataBind();
            }
            else
            {
                gvMonthlyOpeningStockreportDetails.DataSource = "";
                gvMonthlyOpeningStockreportDetails.DataBind();
            }

            ViewState["DataTable"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void exportExcel(DataTable dt, string fileName, string LetterName, string gemBoxKey)
    {
        try
        {
            DataTable dtL1 = new DataTable();
            DataTable dtL2 = new DataTable();
            DataTable dtL3 = new DataTable();
            DataTable dtL4 = new DataTable();

            var Location1 = from myRow in dt.AsEnumerable()
                            where myRow.Field<int>("LocationID") == 1
                            select myRow;
            var Location2 = from myRow in dt.AsEnumerable()
                            where myRow.Field<int>("LocationID") == 2
                            select myRow;
            var Location3 = from myRow in dt.AsEnumerable()
                            where myRow.Field<int>("LocationID") == 3
                            select myRow;
            var Location4 = from myRow in dt.AsEnumerable()
                            where myRow.Field<int>("LocationID") == 4
                            select myRow;

            dtL1 = Location1.CopyToDataTable();
            dtL2 = Location2.CopyToDataTable();
            dtL3 = Location3.CopyToDataTable();
            dtL4 = Location4.CopyToDataTable();

            if ((dt.Rows.Count > 0))
            {

                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                ExcelFile workbook = new ExcelFile();
                ExcelWorksheet SheetLocation1;
                ExcelWorksheet SheetLocation2;
                ExcelWorksheet SheetLocation3;
                ExcelWorksheet SheetLocation4;

                SheetLocation1 = workbook.Worksheets.Add("CHEMMENCHERRY");
                SheetLocation2 = workbook.Worksheets.Add("CHEYYAR");
                SheetLocation3 = workbook.Worksheets.Add("CHEMMANCHERRY SUB STORE (B)");
                SheetLocation4 = workbook.Worksheets.Add("CHEYYAR SUB STORE (B)");

                if (dtL1.Rows.Count > 0)
                {
                    SheetLocation1 = exportex(dtL1, dtL1.Rows.Count, dtL1.Columns.Count, LetterName,
                        "Opening Stock CHEMMENCHERRY" + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, SheetLocation1);
                }

                SheetLocation1.Columns["A"].Cells[dtL1.Rows.Count + 6].Calculate();

                if (dtL2.Rows.Count > 0)
                {
                    SheetLocation2 = exportex(dtL2, dtL2.Rows.Count, dtL2.Columns.Count, LetterName,
                       "Opening Stock CHEYYAR" + " / " + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, SheetLocation2);
                }
                if (dtL3.Rows.Count > 0)
                {
                    SheetLocation3 = exportex(dtL3, dtL1.Rows.Count, dtL3.Columns.Count, LetterName,
                     "Opening Stock CHEMMANCHERRY SUB STORE (B)" + " / " + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, SheetLocation3);
                }
                if (dtL4.Rows.Count > 0)
                {
                    SheetLocation4 = exportex(dtL4, dtL1.Rows.Count, dtL4.Columns.Count, LetterName,
                  "Opening Stock CHEYYAR SUB STORE (B)" + " / " + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, SheetLocation4);
                }

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

            worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Font.Name = "Times New Roman";

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

    #endregion


    #region"PageLoad Events"  

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvMonthlyOpeningStockreportDetails.Rows.Count > 0)
        {
            gvMonthlyOpeningStockreportDetails.UseAccessibleHeader = true;
            gvMonthlyOpeningStockreportDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}