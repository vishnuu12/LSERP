using GemBox.Spreadsheet;
using JqDatatablesWebForm.Models;
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

public partial class Pages_ProductionAndQualityKPIReports : System.Web.UI.Page
{
    #region "Declaration"

    cReports objRpt;
    cSession objSesion = new cSession();

    #endregion

    #region"Page Init Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSesion = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetFinanaceYear();
                HttpContext.Current.Session["RFPKPIDISPATCH"] = null;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"Radio Events"

    protected void rblKPIChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblKPIChange.SelectedValue == "0")
            {
                divFinanceYear.Visible = true;
                divspecficdate.Visible = false;
            }
            else if (rblKPIChange.SelectedValue == "1")
            {
                divFinanceYear.Visible = false;
                divspecficdate.Visible = true;
            }
            txtfromDate.Text = "";
            txtToDate.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnGetReports_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["RFPKPIReports"];

            dt.Columns.Remove("Month");
            dt.Columns.Remove("Year");

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";
            string LetterName = "RFPKPIMainReport" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, strFile, LetterName, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"

    private void BindDesignKPIReports()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.FYID = ddlFinanceYear.SelectedValue;

            ds = objRpt.GetRFPKPIReports();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["RFPKPIReports"] = ds.Tables[0];
                gvRFPKpiReports.DataSource = ds.Tables[0];
                gvRFPKpiReports.DataBind();
            }
            else
            {
                gvRFPKpiReports.DataSource = "";
                gvRFPKpiReports.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetFinanaceYear()
    {
        DataSet ds = new DataSet();
        cSales objSales = new cSales();
        try
        {
            ds = objSales.GetFinanceYearDetails(ddlFinanceYear);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDesignKPIReportDetails(string Month, string Year)
    {
        DataSet ds = new DataSet();
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "RFPKPIDetails.aspx?Month=" + Month + "&&Year=" + Year + "&&FYID=" + ddlFinanceYear.SelectedValue + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
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
            if ((dt.Rows.Count > 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                ExcelFile workbook = new ExcelFile();
                ExcelWorksheet SheetLocation1;

                SheetLocation1 = workbook.Worksheets.Add("RFPKPI");

                if (dt.Rows.Count > 0)
                {
                    SheetLocation1 = exportex(dt, dt.Rows.Count, dt.Columns.Count, LetterName,
                        "RFP KPI " + ddlFinanceYear.SelectedItem.Text, "", 2, SheetLocation1);
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

    #region"GridView Events"

    protected void gvRFPKpiReports_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string Month = gvRFPKpiReports.DataKeys[index].Values[0].ToString();
            string Year = gvRFPKpiReports.DataKeys[index].Values[1].ToString();

            if (e.CommandName.ToString() == "viewNoOfRFPReleased")
                BindDesignKPIReportDetails(Month, Year);
            else
                BindRFPDispatchedDetails(Month, Year);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Methods"

    protected void ddlFinanceYear_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFinanceYear.SelectedIndex > 0)
            {
                BindDesignKPIReports();
                divOutput.Visible = true;
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindRFPDispatchedDetails(string Month, string Year)
    {
        DataSet ds = new DataSet();
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "RFPDispatchDetails.aspx?Month=" + Month + "&&Year=" + Year + "&&FYID=" + ddlFinanceYear.SelectedValue + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
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

            string FromDate = HttpContext.Current.Request.Params["FromDate"];
            string ToDate = HttpContext.Current.Request.Params["ToDate"];

            if (HttpContext.Current.Session["RFPKPIDISPATCH"] == null ||
                HttpContext.Current.Session["FromDate"] != FromDate ||
                HttpContext.Current.Session["ToDate"] != ToDate)
            {
                data = GetRFPDispatchDetails(FromDate, ToDate);
                HttpContext.Current.Session["RFPKPIDISPATCH"] = data;
                HttpContext.Current.Session["FromDate"] = FromDate;
                HttpContext.Current.Session["ToDate"] = ToDate;
            }
            else
                data = (List<Dictionary<string, object>>)HttpContext.Current.Session["RFPKPIDISPATCH"];

            int totalRecords = data.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(p =>
                              p["ProspectName"].ToString().ToLower().Contains(search.ToLower())
                           || p["RFPNo"].ToString().ToLower().Contains(search.ToLower())
                           || p["RFPApprovedOn"].ToString().ToLower().Contains(search.ToLower())
                           || p["DeliveryOn"].ToString().ToLower().Contains(search.ToLower())
                           || p["InvoiceEntryDate"].ToString().ToLower().Contains(search.ToLower())
                           || p["RFPDispatchedOn"].ToString().ToLower().Contains(search.ToLower())
                           || p["RFPDispatchedBy"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
            }

            int recFilter = data.Count;
            data = data.Skip(startRec).Take(pageSize).ToList();
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
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
    public static List<Dictionary<string, object>> GetRFPDispatchDetails(string FromDate, string ToDate)
    {
        DataSet ds = new DataSet();
        cReports objRpt = new cReports();
        List<cReports> lst = new List<cReports>();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objRpt.FromDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objRpt.ToDate = DateTime.ParseExact(ToDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objRpt.GetRFPKPIDispatchDetailsByFromDateAndToDate();

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
        string LetterName = "RFPKPIDispatchDetails.xls";
        try
        {
            objRpt.FromDate = DateTime.ParseExact(HttpContext.Current.Session["FromDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objRpt.ToDate = DateTime.ParseExact(HttpContext.Current.Session["ToDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objRpt.GetRFPKPIDispatchDetailsByFromDateAndToDate();

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

            dtL1 = ds.Tables[0];

            dtL1.Columns["RFPNo"].SetOrdinal(0);
            dtL1.Columns["ProspectName"].SetOrdinal(1);
            dtL1.Columns["DeliveryOn"].SetOrdinal(2);
            dtL1.Columns["RFPApprovedOn"].SetOrdinal(3);
            dtL1.Columns["RFPDispatchedOn"].SetOrdinal(4);

            dtL1.Columns["ProspectName"].ColumnName = "Customer Name";
            dtL1.Columns["DeliveryOn"].ColumnName = "Dispatch Committed Date";
            dtL1.Columns["RFPApprovedOn"].ColumnName = "RFP Released Date";
            dtL1.Columns["RFPDispatchedOn"].ColumnName = "Actual Dispatch Date";
            dtL1.Columns["RFPDispatchedBy"].ColumnName = "RFP Dispatched By";
            dtL1.Columns["InvoiceEntryDate"].ColumnName = "Invoice Entry Date";

            string _key = gemBoxKey;
            SpreadsheetInfo.SetLicense(_key);

            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet SheetLocation1;

            SheetLocation1 = workbook.Worksheets.Add("RFP KPI");

            if (dtL1.Rows.Count > 0)
            {
                SheetLocation1 = exportexDispatch(dtL1, dtL1.Rows.Count, dtL1.Columns.Count, LetterName,
                    " RFP DISPATCH DETAILS FROM THE DATE BETWEEN " + HttpContext.Current.Session["FromDate"].ToString() + "And" + HttpContext.Current.Session["ToDate"].ToString(), "", 2, SheetLocation1);
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

    public static ExcelWorksheet exportexDispatch(DataTable dt, int rowCount, int columnCount, string LetterName, string schoolName, string reportName, int startRow, ExcelWorksheet worksheet)
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