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

public partial class Pages_MonthWiseStockReportDetails : System.Web.UI.Page
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
            if (IsPostBack == false)
            {
                BindYear();
                // BindCurrentMonthYearDetails();
                divOutput.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnStockPending_Click(object sender, EventArgs e)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "StockInwardPendingMRNDetailsReport.aspx");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnMonthlyConsumption_Click(object sender, EventArgs e)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "MonthlyConsumptionReport.aspx?Month=" + ddlMonthYear.SelectedValue.Split('/')[0].ToString() + "&&Year=" + ddlMonthYear.SelectedValue.Split('/')[1].ToString() + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnMonthlyInward_Click(object sender, EventArgs e)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "MonthlyMaterialInwardDetailsReport.aspx?Month=" + ddlMonthYear.SelectedValue.Split('/')[0].ToString() + "&&Year=" + ddlMonthYear.SelectedValue.Split('/')[1].ToString() + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnMonthlyOpenningStock_Click(object sender, EventArgs e)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "MonthlyOpeningStockReport.aspx?Month=" + ddlMonthYear.SelectedValue.Split('/')[0].ToString() + "&&Year=" + ddlMonthYear.SelectedValue.Split('/')[1].ToString() + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        cCommon _objc = new cCommon();
        List<int> liCID = new List<int>();
        List<int> liLocationID = new List<int>();
        DataTable dtnew = new DataTable();
        try
        {
            //dtt.AsEnumerable().Where(row => row.Field<string>("FProductID") == ddl_FinishProductName.SelectedValue && row.Field<string>("FColorID") == ddl_Color.SelectedValue)
            //.Select(b => b["FQuantity"] = Convert.ToString(tot)).ToList();
            string MAXEXID = "";
            int CID;
            int LocationID;

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];

            foreach (GridViewRow row in gvMonthlyStockReportCostDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    CID = Convert.ToInt32(gvMonthlyStockReportCostDetails.DataKeys[row.RowIndex].Values[0].ToString());
                    LocationID = Convert.ToInt32(gvMonthlyStockReportCostDetails.DataKeys[row.RowIndex].Values[1].ToString());
                    liCID.Add(CID);
                    liLocationID.Add(LocationID);
                }
            }

            dtnew.Columns.Add("LocationID");
            dtnew.Columns.Add("CID");
            dtnew.Columns.Add("Location");
            dtnew.Columns.Add("CategoryName");
            dtnew.Columns.Add("OpeningStock");
            dtnew.Columns.Add("InwardCost");
            dtnew.Columns.Add("ConsumptionCost");
            dtnew.Columns.Add("ClosingStock");

            for (int i = 0; i < liCID.Count; i++)
            {
                var rows = dt.AsEnumerable().Where(row => row.Field<int>("CID") == liCID[i] && row.Field<int>("LocationID") == liLocationID[i]);
                dtnew.ImportRow(rows.CopyToDataTable().Rows[0]);
            }
            dtnew.Columns.Remove("LocationID");
            dtnew.Columns.Remove("CID");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dtnew.Rows.Count);
            int ColumnCount = Convert.ToInt32(dtnew.Columns.Count);

            string strFile = "";

            string LetterName = "MonthlyStock" + "-" + ddlMonthYear.SelectedValue.Split('/')[0].ToString() + "-" + ddlMonthYear.SelectedValue.Split('/')[1].ToString() + "-" + MAXEXID + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dtnew, rowcount, ColumnCount, strFile, LetterName, "Stock Report" + " For " + ddlMonthYear.SelectedItem.Text, "", 2, GemBoxKey);

            _objc.SaveExcelFile("MonthWiseStockReportDetails.aspx", LetterName, objSession.employeeid);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlYear_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divOutput.Visible = false;
            if (ddlYear.SelectedIndex > 0)
            {
                BindCurrentMonthYearDetails();
            }
            else
            {
                cCommon objc = new cCommon();
                objc.EmptyDropDownList(ddlMonthYear);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMonthYear_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMonthYear.SelectedIndex > 0)
            {
                BindMonthlyWiseStockReportCostDetails();
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

    private void BindYear()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.GetYearDetails(ddlYear);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
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
                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Size = 12 * 20;
                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Name = "Times New Roman";

                // worksheet.Cells.GetSubrangeAbsolute(3, 0, rowCount + 3, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                worksheet.Cells.GetSubrangeAbsolute(3, 0, rowCount + 3, columnCount).Style.Font.Size = 12 * 20;
                worksheet.Cells.GetSubrangeAbsolute(3, 0, rowCount + 3, columnCount).Style.Font.Name = "Times New Roman";

                //  worksheet.Cells.GetSubrangeAbsolute(3, 3, rowCount + 3, columnCount).Style.NumberFormat = "@";
                // worksheet.Cells["D4"].Style.NumberFormat = "@";

                //worksheet.DefaultRowHeight = 25 * 20;

                //Insert DataTable to an Excel worksheet . //
                worksheet.InsertDataTable(dt,
                   new InsertDataTableOptions()
                   {
                       ColumnHeaders = true,
                       StartRow = 2
                   });

                foreach (var cell in worksheet.Cells.GetSubrangeAbsolute(3, 2, rowCount + 2, columnCount))
                {
                    cell.Value = Convert.ToDecimal(cell.Value);
                }

                worksheet.Cells.GetSubrangeAbsolute(2, 1, 2, 1).Value = "Category Name";
                worksheet.Cells.GetSubrangeAbsolute(2, 2, 2, 2).Value = "Opening Stock";
                worksheet.Cells.GetSubrangeAbsolute(2, 3, 2, 3).Value = "Inward Cost";
                worksheet.Cells.GetSubrangeAbsolute(2, 4, 2, 4).Value = "Consumption Cost";
                worksheet.Cells.GetSubrangeAbsolute(2, 5, 2, 5).Value = "Closing Stock";

                int lastrow = rowCount + 3;

                worksheet.Cells[rowCount + 3, 2].Formula = "=SUM(C4:C" + lastrow.ToString() + ")";
                worksheet.Cells[rowCount + 3, 3].Formula = "=SUM(D4:D" + lastrow.ToString() + ")";
                worksheet.Cells[rowCount + 3, 4].Formula = "=SUM(E4:E" + lastrow.ToString() + ")";
                worksheet.Cells[rowCount + 3, 5].Formula = "=SUM(F4:F" + lastrow.ToString() + ")";

                worksheet.Cells[rowCount + 3, 2].Calculate();

                worksheet.Rows[rowCount + 3].Style.Font.Weight = ExcelFont.BoldWeight;

                worksheet.Cells.GetSubrangeAbsolute(3, 2, rowCount + 3, columnCount).Style.NumberFormat = "#,##0.00";

                for (int i = 0; i < worksheet.CalculateMaxUsedColumns(); i++)
                {
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
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

    private void BindCurrentMonthYearDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objR.GetCurrentMonthYearDetails(ddlMonthYear);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMonthlyWiseStockReportCostDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ddlMonthYear.SelectedValue.Split('/')[0].ToString());
            objR.Year = Convert.ToInt32(ddlMonthYear.SelectedValue.Split('/')[1].ToString());
            ds = objR.GetMonthlyWiseStockReportCostrDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMonthlyStockReportCostDetails.DataSource = ds.Tables[0];
                gvMonthlyStockReportCostDetails.DataBind();
            }
            else
            {
                gvMonthlyStockReportCostDetails.DataSource = "";
                gvMonthlyStockReportCostDetails.DataBind();
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
        if (gvMonthlyStockReportCostDetails.Rows.Count > 0)
        {
            gvMonthlyStockReportCostDetails.UseAccessibleHeader = true;
            gvMonthlyStockReportCostDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}