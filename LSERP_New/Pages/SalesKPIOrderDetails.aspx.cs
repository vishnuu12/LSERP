using eplus.core;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Pages_SalesKPIOrderDetails : System.Web.UI.Page
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

    protected void gvMonthlyOrderPODetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "viewCustomerPODetails")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string EnquiryNumber = gvMonthlyOrderPODetails.DataKeys[index].Values[0].ToString();
                BindCustomerPOAndOfferDetailsByEnquiryNumber(EnquiryNumber);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

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

    protected void gvMonthlyOrderPODetails_OnRowDataBounds(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOfferDispatchedon = (Label)e.Row.FindControl("lblOfferDispatchedon");
                if (dr["OfferNoRevision"].ToString() != "0")
                    lblOfferDispatchedon.Attributes.Add("style", "color:mediumvioletred;");
            }
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
            dt = (DataTable)ViewState["KPIORDER"];

            dt.Columns.Remove("PO_CreatedBy");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "SalesKPIOrder" + "-" + ViewState["Month"].ToString() + "-" + ViewState["Year"].ToString() + "-" + MAXEXID + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, strFile, LetterName, GemBoxKey);

            //_objc.SaveExcelFile("MonthlyClosingStockReport.aspx", LetterName, objSession.employeeid);
            //bindMaterialMaster();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindCustomerPOAndOfferDetailsByEnquiryNumber(string EnquiryNumber)
    {
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "CustomerPOAndOfferDetailsDetailsByEnquiryNo.aspx?EnquiryID=" + EnquiryNumber + "");

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

            ds = objR.GetMonthlyOrderDetailsByMonthAndYear();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["KPIORDER"] = ds.Tables[0];

                gvMonthlyOrderPODetails.DataSource = ds.Tables[0];
                gvMonthlyOrderPODetails.DataBind();
            }
            else
            {
                gvMonthlyOrderPODetails.DataSource = "";
                gvMonthlyOrderPODetails.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
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
                        "Sales KPI Order" + ViewState["Month"].ToString() + " / " + ViewState["Year"].ToString() + "", "", 2, SheetLocation1);
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
        if (gvMonthlyOrderPODetails.Rows.Count > 0)
        {
            gvMonthlyOrderPODetails.UseAccessibleHeader = true;
            gvMonthlyOrderPODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}