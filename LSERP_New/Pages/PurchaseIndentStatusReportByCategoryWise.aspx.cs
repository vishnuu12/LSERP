using eplus.core;
using GemBox.Spreadsheet;
using JqDatatablesWebForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PurchaseIndentStatusReportByCategoryWise : System.Web.UI.Page
{
    #region"Declaration"

    cStores objSt;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //objSt = new cStores();
            //objSt.Mode = "InStock";
            //BindStockMonitorDetails();
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                HttpContext.Current.Session["PICW"] = null;
                HttpContext.Current.Session["CID"] = null;
                HttpContext.Current.Request.Params["FromDate"] = null;
                HttpContext.Current.Request.Params["ToDate"] = null;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #region"Button Events"

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #endregion

    #region"Web Method"
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetData()
    {
        DataTables da = new DataTables();
        List<Dictionary<string, object>> data;
        data = null;
        DataSet ds = new DataSet();
        try
        {
            string search = HttpContext.Current.Request.Params["search[value]"];
            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            string CID = HttpContext.Current.Request.Params["CID"];
            string FromDate = HttpContext.Current.Request.Params["FromDate"];
            string ToDate = HttpContext.Current.Request.Params["ToDate"];

            if (HttpContext.Current.Session["PICW"] == null ||
                HttpContext.Current.Session["CID"].ToString() != CID ||
                HttpContext.Current.Session["FromDate"] != FromDate ||
                HttpContext.Current.Session["ToDate"] != ToDate)
            {
                data = GetIndentDetailsByCID(CID, FromDate, ToDate);
                HttpContext.Current.Session["PICW"] = data;
                HttpContext.Current.Session["CID"] = CID;
                HttpContext.Current.Session["FromDate"] = FromDate;
                HttpContext.Current.Session["ToDate"] = ToDate;
            }
            else
                data = (List<Dictionary<string, object>>)HttpContext.Current.Session["PICW"];

            int totalRecords = data.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(p =>
                              p["PID"].ToString().ToLower().Contains(search.ToLower())
                           || p["IndentBy"].ToString().ToLower().Contains(search.ToLower())
                           || p["IndentQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["GradeName"].ToString().ToLower().Contains(search.ToLower())
                           || p["SupplierName"].ToString().ToLower().Contains(search.ToLower())
                           || p["UnitCost"].ToString().ToLower().Contains(search.ToLower())
                           || p["POEnterQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["SupplierName"].ToString().ToLower().Contains(search.ToLower())
                           || p["InwardQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["IndentDate"].ToString().ToLower().Contains(search.ToLower())
                           || p["POYear"].ToString().ToLower().Contains(search.ToLower())
                           || p["POQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["MRNNo"].ToString().ToLower().Contains(search.ToLower())

                           || p["InwardDate"].ToString().ToLower().Contains(search.ToLower())
                           || p["InwardBy"].ToString().ToLower().Contains(search.ToLower())
                           || p["POIssuedDate"].ToString().ToLower().Contains(search.ToLower())
                           || p["IndentNo"].ToString().ToLower().Contains(search.ToLower())

                          ).ToList();
            }

            int recFilter = data.Count;
            data = data.Skip(startRec).Take(pageSize).ToList();
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.TotalInwardCost = HttpContext.Current.Session["TotalInwardCost"].ToString();
            da.data = data;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return da;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string GetCategoryNameListDetails()
    {
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        string JsonString = "";
        try
        {
            objR = new cReports();
            ds = objR.GetCategoryNameDetails();

            DataTable dt = new DataTable();
            dt = (DataTable)ds.Tables[0];

            JsonString = dt.Rows[0]["Categoryname"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JsonString;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static List<Dictionary<string, object>> GetIndentDetailsByCID(string CID, string FromDate, string ToDate)
    {
        DataSet ds = new DataSet();
        cReports objRpt = new cReports();
        List<cReports> lst = new List<cReports>();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objRpt.CID = CID;
            objRpt.FromDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objRpt.ToDate = DateTime.ParseExact(ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objRpt.GetPurchaseIndentDetailsByCID();

            HttpContext.Current.Session["TotalInwardCost"] = ds.Tables[1].Rows[0]["TotalInwardCost"].ToString();

            DataTable dt = new DataTable();
            dt = (DataTable)ds.Tables[0];

            Dictionary<string, object> row = new Dictionary<string, object>();

            DateTimeOffset d1 = DateTimeOffset.Now;
            long t1 = d1.ToUnixTimeMilliseconds();
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            DateTimeOffset d2 = DateTimeOffset.Now;
            long t2 = d2.ToUnixTimeMilliseconds();

            long t3 = t2 - t1;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return rows;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string ExcelDownload()
    {
        DataSet ds = new DataSet();
        cReports objRpt = new cReports();
        string strFile = "";
        string ExcelHttpPath = ConfigurationManager.AppSettings["ExcelPath"].ToString();
        string LetterName = "CategoryWiseIndentReport.xls";
        try
        {
            objRpt.CID = HttpContext.Current.Session["CID"].ToString();
            objRpt.FromDate = DateTime.ParseExact(HttpContext.Current.Session["FromDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objRpt.ToDate = DateTime.ParseExact(HttpContext.Current.Session["ToDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objRpt.GetPurchaseIndentDetailsByCID();

            int rowcount = Convert.ToInt32(ds.Tables[0].Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Tables[0].Columns.Count);

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, strFile, LetterName, GemBoxKey);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ExcelHttpPath + LetterName;
    }

    public static void exportExcel(DataSet ds, string FilePath, string LetterName, string gemBoxKey)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dtL1 = new DataTable();
            DataTable dtL2 = new DataTable();
            DataTable dtL3 = new DataTable();

            dt = ds.Tables[0];

            var rows = ds.Tables[0].AsEnumerable()
                        .Where(r => r.IsNull("MRNNo"));

            var row1 = ds.Tables[0].AsEnumerable()
                        .Where(r => r["MRNNo"].ToString() != "");

            dtL1 = rows.CopyToDataTable();
            dtL2 = row1.CopyToDataTable();

            string _key = gemBoxKey;
            SpreadsheetInfo.SetLicense(_key);

            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet SheetLocation1;
            ExcelWorksheet SheetLocation2;

            SheetLocation1 = workbook.Worksheets.Add("Pending Indent");
            SheetLocation2 = workbook.Worksheets.Add("Inward Details");

            if (dtL1.Rows.Count > 0)
            {
                SheetLocation1 = exportex(dtL1, dtL1.Rows.Count, dtL1.Columns.Count, LetterName,
                    "Pending Indent Details From The Date Between " + HttpContext.Current.Session["FromDate"].ToString() + "And" + HttpContext.Current.Session["ToDate"].ToString(), "", 2, SheetLocation1);
            }

            if (dtL2.Rows.Count > 0)
            {
                SheetLocation2 = exportex(dtL2, dtL2.Rows.Count, dtL2.Columns.Count, LetterName,
                    "Inward Indent Details The Date Between " + HttpContext.Current.Session["FromDate"].ToString() + "And" + HttpContext.Current.Session["ToDate"].ToString(), "", 2, SheetLocation2);
            }

            workbook.Save(FilePath);

            //var response = HttpContext.Current.Response;
            //var options = SaveOptions.XlsDefault;

            //response.Clear();
            //response.ContentType = options.ContentType;
            //response.AddHeader("Content-Disposition", "attachment; filename=" + LetterName);

            //var ms = new System.IO.MemoryStream();
            //workbook.Save(ms, options);
            //ms.WriteTo(response.OutputStream);

            //response.Flush();
            //response.SuppressContent = true;
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public static ExcelWorksheet exportex(DataTable dt, int rowCount, int columnCount, string LetterName, string schoolName, string reportName, int startRow, ExcelWorksheet worksheet)
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
}