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

public partial class Pages_DesignKPIReportDetails : System.Web.UI.Page
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
                // BindCurrentMonthYearDetails();
                divOutput.Visible = true;
                divInput.Visible = true;

                ViewState["Month"] = Request.QueryString["Month"].ToString();
                ViewState["Year"] = Request.QueryString["Year"].ToString();
                ViewState["FYID"] = Request.QueryString["FYID"].ToString();
                BindMonthlyOrderDetails();

                lblMonthYearName.Text = ViewState["Month"].ToString() + "/" + ViewState["Year"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvEnquiryProjectAssignmentReport_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  e.Row.Cells[0].Attributes.Add("style", "text-align:center;");

                if (string.IsNullOrEmpty(dr["Offer No"].ToString()))
                {
                    e.Row.Cells[3].Text = "NA";
                    e.Row.CssClass = "warning";
                }

                if (string.IsNullOrEmpty(dr["Order No"].ToString()))
                    e.Row.Cells[5].Text = "NA";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvdesignkpireportdetails_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string EnquiryID = gvdesignkpireportdetails.DataKeys[index].Values[0].ToString();
            BindDrawingItemDetails(EnquiryID);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DesignKPIReportsDetails"];

            // dt.Columns.Remove("SUPID");

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";
            string LetterName = "DesignKPIReports" + ".xls";
            string LetterPath = System.Configuration.ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
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

    #region"Common Methods"

    private void BindDrawingItemDetails(string EnquiryID)
    {
        DataSet ds = new DataSet();
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "DrawingItemDetailsByDesignKPI.aspx?EnquiryID=" + EnquiryID + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMonthlyOrderDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.Month = Convert.ToInt32(ViewState["Month"].ToString());
            objR.Year = Convert.ToInt32(ViewState["Year"].ToString());
            objR.FYID = ViewState["FYID"].ToString();

            ds = objR.GetDesignKPIReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["DesignKPIReportsDetails"] = ds.Tables[0];

                gvdesignkpireportdetails.DataSource = ds.Tables[0];
                gvdesignkpireportdetails.DataBind();
            }
            else
            {
                gvdesignkpireportdetails.DataSource = "";
                gvdesignkpireportdetails.DataBind();
            }
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
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

                SheetLocation1 = workbook.Worksheets.Add("SalesKPIOrder");

                if (dt.Rows.Count > 0)
                {
                    SheetLocation1 = exportex(dt, dt.Rows.Count, dt.Columns.Count, LetterName,
                        "Design KPI Details " + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, SheetLocation1);
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

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvdesignkpireportdetails.Rows.Count > 0)
        {
            gvdesignkpireportdetails.UseAccessibleHeader = true;
            gvdesignkpireportdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}