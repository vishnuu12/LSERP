using eplus.core;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class AutomatedAlerts : System.Web.Services.WebService
{
    public AutomatedAlerts()
    {

    }

    [WebMethod]
    public void GetAutomatedAlertDetails()
    {
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            ds = objc.GetAutomatedAlertdetails();

            int rowcount = Convert.ToInt32(ds.Tables[0].Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Tables[0].Columns.Count);

            string strFile = "";

            string LetterName = ds.Tables[0].Rows[0]["FileName"].ToString();

            //string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string LetterPath = ConfigurationManager.AppSettings["CommunicationSavePath"].ToString();

            //string CommunicationHttpPath = ConfigurationManager.AppSettings["CommunicationHttpPath"].ToString();

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
    }

    public void exportExcel(DataSet ds, string FilePath, string LetterName, string gemBoxKey)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dtL1 = new DataTable();
            DataTable dtL2 = new DataTable();
            DataTable dtL3 = new DataTable();

            dt = ds.Tables[0];
            dtL1 = ds.Tables[1];
            dtL2 = ds.Tables[2];
            //   dtL3 = ds.Tables[2];

            string _key = gemBoxKey;
            SpreadsheetInfo.SetLicense(_key);

            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet SheetLocation1;
            ExcelWorksheet SheetLocation2;

            SheetLocation1 = workbook.Worksheets.Add("Design");
            SheetLocation2 = workbook.Worksheets.Add("Marketing");

            if (dtL1.Rows.Count > 0)
            {
                SheetLocation1 = exportex(dtL1, dtL1.Rows.Count, dtL1.Columns.Count, LetterName,
                    "BOM Approved Details For " + dt.Rows[0]["ActivityDate"].ToString(), "", 2, SheetLocation1);
            }

            if (dtL2.Rows.Count > 0)
            {
                SheetLocation2 = exportex(dtL2, dtL2.Rows.Count, dtL2.Columns.Count, LetterName,
                   "Offer Cost Accepted Details For " + dt.Rows[0]["ActivityDate"].ToString(), "", 2, SheetLocation2);
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
}
