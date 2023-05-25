using GemBox.Spreadsheet;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPPaymentCollectionValue : System.Web.UI.Page
{
    #region "Declaration"

    cSales objSales;
    cDesign objDesign;
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
                BindRFPPaymentCollectionReports();
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
        try
        {
            string MAXEXID = "";
            DataTable ds = new DataTable();
            ds = (DataTable)ViewState["RFPPaymentCollectionReports"];

            ds.Columns.Remove("IDID");

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "RFPPaymentCollectionReports" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, rowcount, ColumnCount, strFile, LetterName, "", "RFP Payment Collection Reports", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"
    private void BindRFPPaymentCollectionReports()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {

            ds = objDesign.GetRFPPaymentCollectionReports();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["RFPPaymentCollectionReports"] = ds.Tables[0];
                gvRFPPaymentCollectionReports.DataSource = ds.Tables[0];
                gvRFPPaymentCollectionReports.DataBind();
            }
            else
            {
                gvRFPPaymentCollectionReports.DataSource = "";
                gvRFPPaymentCollectionReports.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
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
                var worksheet = workbook.Worksheets.Add("RFPPaymentCollectionReports");

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


    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvRFPPaymentCollectionReports.Rows.Count > 0)
        {
            gvRFPPaymentCollectionReports.UseAccessibleHeader = true;
            gvRFPPaymentCollectionReports.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion

}