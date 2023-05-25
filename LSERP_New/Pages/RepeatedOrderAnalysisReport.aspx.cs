using eplus.core;
using GemBox.Spreadsheet;
using JqDatatablesWebForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RepeatedOrderAnalysisReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cReports objR;

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
            if (!IsPostBack)
            {
                HttpContext.Current.Session["ROA"] = null;

                //if (Request.QueryString.ToString().Length > 0)
                //{
                //    string FileName = Request.QueryString["FileName"].ToString();

                //    cCommon.DownLoad(FileName, ConfigurationManager.AppSettings["ExcelSavePath"].ToString() + FileName);
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion   

    #region"Web Method"

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetData()
    {
        // Initialization.         
        DataTables da = new DataTables();
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

        //Dictionary<string, object> data;
        string Jsonstring;
        string sessionname = "POInwardStatusReport";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            // Initialization.    
            string search = HttpContext.Current.Request.Params["search[value]"];

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.    

            if (HttpContext.Current.Session["ROA"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["ROA"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["ROA"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                             p["ProspectName"].ToString().ToLower().Contains(search.ToLower())
                          || p["OrderCount"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            int recFilter = dataList.Count;
            // Apply pagination. 

            //EnquiryID	ProspectName	Staff	DesignStart	BomApproved	RFPStatus

            dataList = dataList.Skip(startRec).Take(pageSize).ToList();
            // Loading drop down lists.    
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.Ldata = dataList;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        // Return info.    
        return da;
        //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        //js.MaxJsonLength = int.MaxValue;
        //return js.Serialize(da);
    }

    private static List<Dictionary<string, object>> LoadData()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            ds = objR.GetRepeatedOrderAnalysisReport();

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
            //Console.WriteLine(t1);
            //Console.WriteLine(t2);
            //Console.WriteLine(t3);
            //Console.ReadKey();
        }
        catch (Exception ex)
        {

        }
        // info.    
        return rows;
    }

    private static List<cReports> SortByColumnWithOrder(string order, string orderDir, List<cReports> data)
    {
        // Initialization.    
        List<cReports> lst = new List<cReports>();
        try
        {
            // Sorting    
            switch (order)
            {
                case "0":
                    // Setting.    
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                                                 data.OrderByDescending(p => p.RFPNo).ToList()
                                                         : data.OrderBy(p => p.INDNo).ToList();
                    break;
                case "1":
                    // Setting.    
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(
                                                                        p => p.RFPNo).ToList()
                                                         : data.OrderBy(p => p.INDNo).ToList();
                    break;
            }
        }
        catch (Exception ex)
        {

        }
        // info.    
        return lst;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object ExcelDownLoad()
    {
        cCommon _objc = new cCommon();
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();

            ds = objR.GetRepeatedOrderAnalysisReport();

            dt = (DataTable)ds.Tables[0];

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "RepeatOrderAnalysis.xlsx";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "", "POInwardStatusReport", 2, GemBoxKey);

            //_objc.SaveExcelFile("MonthWiseStockReportDetails.aspx", LetterName, "1001");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return new
        {
            Data = new { FileName = "RepeatOrderAnalysis.xlsx" }
        };
    }

    public static void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("Stock Report");

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
                {
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
                }

                workbook.Save(fileName);

                //var response = HttpContext.Current.Response;
                //var options = SaveOptions.XlsxDefault;

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
        }
        catch (Exception ex)
        {

            Log.Message(ex.ToString());
        }
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object Download()
    {
        try
        {
            //var response = HttpContext.Current.Response;
            //var options = SaveOptions.XlsxDefault;

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

        return "";
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetSupplierPODetailsBySPOID(string SPOID)
    {
        cReports objR = new cReports();
        DataSet ds = new DataSet();
        DataTable dt;
        string JSONString = string.Empty;
        try
        {
            objR.SPOID = Convert.ToInt32(SPOID);
            ds = objR.GetSupplierPOItemDetailsBySPOID();

            dt = (DataTable)ds.Tables[0];

            if (dt.Rows.Count > 0)
                JSONString = JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetInwardDetails(string SPOID)
    {
        cReports objR = new cReports();
        DataSet ds = new DataSet();
        DataTable dt;
        string JSONString = string.Empty;
        try
        {
            objR.SPOID = Convert.ToInt32(SPOID);
            ds = objR.GetInwardDetailsBySPOID();

            dt = (DataTable)ds.Tables[0];

            if (dt.Rows.Count > 0)
                JSONString = JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    #endregion
}