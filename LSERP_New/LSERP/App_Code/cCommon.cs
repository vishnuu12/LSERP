using System;
using System.Data;
using eplus.data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.HtmlControls;
using QRCodeEncoderDecoderLibrary;
using System.Linq;
using System.Drawing;
using System.IO;
using SelectPdf;
using System.Data.OleDb;
using System.Configuration;
using GemBox.Spreadsheet;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace eplus.core
{
    public class cCommon
    {

        #region "Declaration"

        DataSet ds = new DataSet();
        cDataAccess DAL = new cDataAccess();
        SqlCommand c = new SqlCommand();

        #endregion

        #region "Properties"

        public string AlertType { get; set; }
        public string EntryMode { get; set; }
        public string reciverID { get; set; }
        public string reciverType { get; set; }
        public string ReciverName { get; set; }
        public string EmailID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public int userID { get; set; }
        public string file { get; set; }
        public DataTable dtSettings { get; set; }

        #endregion


        #region "Common Methods"

        #region "Excel"

        public void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
        {
            try
            {
                if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
                {
                    string _key = gemBoxKey;
                    SpreadsheetInfo.SetLicense(_key);

                    var workbook = new ExcelFile();
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    worksheet.Rows[startRow].Style.Font.Weight = ExcelFont.BoldWeight;

                    columnCount = columnCount - 1;

                    worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);


                    if (schoolName != "")
                    {
                        worksheet.Cells[0, 0].Value = schoolName;
                        worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Merged = true;
                        worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Color = SpreadsheetColor.FromName(ColorName.Blue);
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

                    // Insert DataTable to an Excel worksheet.
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
                    var options = SaveOptions.XlsxDefault;

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

        public DataTable importExcel(string ConnectionString, string filePath, string isHDR)
        {
            DataTable dtExcelSchema = new DataTable();
            try
            {
                // conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                string conStr = String.Format(ConnectionString, filePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
            }
            catch (Exception)
            {

            }
            return dtExcelSchema;
        }

        #endregion

        #region "QR Code"

        public string QRcodeGeneration(string code)
        {
            string qrImage = "";
            try
            {
                QREncoder QRCodeEncoder = new QREncoder();
                QRCodeEncoder.Encode(ErrorCorrection.M, code);
                // create bitmap image
                // each module will be 4 by 4 pixels
                // the quiet zone around the QR Code will be 8 pixels
                Bitmap QRCodeImage = QRCodeToBitmap.CreateBitmap(QRCodeEncoder, 4, 8);
                //converting bitmap to image
                Image img = (Image)QRCodeImage;
                //convert image to bytes
                ImageConverter converter = new ImageConverter();
                byte[] filedata = (byte[])converter.ConvertTo(img, typeof(byte[]));
                //convert bytes to base 64 string
                qrImage = "data:image/png;base64," + Convert.ToBase64String(filedata);
            }
            catch (Exception) { }
            return qrImage;

        }

        public string getDisplayQRNumber(string QRNumber, int length, char comchar)
        {
            string disQRNumber = "";
            try
            {
                if (QRNumber.Length % length == 0)
                {
                    for (int i = 0; i < length; i++)
                    {
                        disQRNumber = disQRNumber + QRNumber.Substring(length * i, length) + comchar;
                    }
                    disQRNumber = disQRNumber.Trim(comchar);
                }
            }
            catch (Exception)
            {

            }
            return disQRNumber;
        }

        #endregion

        #region "PDF"

        public void GenerateAndSavePDF(string LetterPath, string strFile, string pdffile, string URL)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = new PdfDocument();
            try
            {
                if (!Directory.Exists(LetterPath))
                    Directory.CreateDirectory(LetterPath);
                if (File.Exists(strFile))
                    File.Delete(strFile);

                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                converter.Options.MarginLeft = 25;
                converter.Options.MarginRight = 20;
                converter.Options.MarginTop = 50;
                converter.Options.MarginBottom = 40;
                converter.Options.WebPageWidth = 700;
                converter.Options.WebPageHeight = 0;
                converter.Options.WebPageFixedSize = false;

                doc = converter.ConvertUrl(URL);
                doc.Save(strFile);

                var ms = new System.IO.MemoryStream();

                HttpContext.Current.Response.ContentType = "Application/pdf";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdffile);
                HttpContext.Current.Response.TransmitFile(strFile);
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                doc.Close();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            finally
            {
                converter = null;
                doc = null;
                LetterPath = null;
                strFile = null;
                URL = null;
            }
        }

        public static void DownLoad(string FileName, string StrFile)
        {
            try
            {
                string extension = FileName.Split('.')[1].ToUpper();

                if (extension == "PDF")
                    HttpContext.Current.Response.ContentType = "Application/pdf";
                else if (extension == "JPG")
                    HttpContext.Current.Response.ContentType = "Application/JPG";
                else if (extension == "JPEG")
                    HttpContext.Current.Response.ContentType = "Application/JPEG";
                else if (extension == "XLSX")
                    HttpContext.Current.Response.ContentType = "Application/XLSX";
                else if (extension == "XLS")
                    HttpContext.Current.Response.ContentType = "Application/XLS";
                else if (extension == "DOC")
                    HttpContext.Current.Response.ContentType = "Application/DOC";
                else if (extension == "DOCX")
                    HttpContext.Current.Response.ContentType = "Application/DOCX";
                else if(extension=="PNG")
                    HttpContext.Current.Response.ContentType = "Application/PNG";

                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                HttpContext.Current.Response.TransmitFile(StrFile);
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        #endregion

        #region "Common"

        public string RandomAlphaNumericNumber(int length)
        {
            Random random = new Random();
            string RNumber = "";
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                RNumber = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception)
            {

            }
            finally
            {
                random = null;
            }
            return RNumber;
        }

        public Int64 RandomNumericNumber(int length)
        {
            Random random = new Random();
            Int64 RNumber = 0;
            try
            {
                const string chars = "0123456789";
                RNumber = Convert.ToInt64(new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()));
            }
            catch (Exception) { }
            finally
            {
                random = null;
            }
            return RNumber;
        }


        #endregion

        #region "Document Path"

        public DataSet getDocPathDetails(string DocumentType)
        {

            try
            {

                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "SMS_GetDocumentPathDetails";
                c.Parameters.AddWithValue("@DocumentType", DocumentType);
                DAL.GetDataset(c, ref ds);

            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        #endregion

        public string GetMaximumNumberExcel()
        {
            string MaxEXID = "";
            try
            {
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetMAXExidFromExcelFiles";
                MaxEXID = DAL.GetScalar(c);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }

            return MaxEXID;
        }

        public void SaveExcelFile(string RefPage, string FileName, string Createdby)
        {
            try
            {
                //  cSession _objsess = (cSession)HttpContext.Current.Session["LoginDetails"];
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_SaveExcelFile";
                c.Parameters.AddWithValue("@Createdby", Createdby);
                c.Parameters.AddWithValue("@RefPage", RefPage);
                c.Parameters.AddWithValue("@FileName", FileName);

                string identity = DAL.GetScalar(c);

            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        public string GetMaximumNumberPDF()
        {
            string MaxPDFID = "";
            try
            {
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetMAXPdfIdFromPDFFiles";
                MaxPDFID = DAL.GetScalar(c);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }

            return MaxPDFID;
        }

        public void SavePDFFile(string RefPage, string FileName, string Createdby)
        {
            try
            {
                // cSession _objsess = (cSession)HttpContext.Current.Session["LoginDetails"];
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_SavePDFFile";
                c.Parameters.AddWithValue("@Createdby", Createdby);
                c.Parameters.AddWithValue("@RefPage", RefPage);
                c.Parameters.AddWithValue("@FileName", FileName);
                DAL.GetScalar(c);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }

        }

        public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
        {
            try
            {
                StreamWriter w;
                w = File.CreateText(URL);
                w.WriteLine("<html><head><title>");
                w.WriteLine(HeaderTitle);
                w.WriteLine("</title>");
             //   w.WriteLine("<link rel='stylesheet' type='text/css' href='http://localhost:59987/LSERP_New/Assets/css/ep-style.css'/>");
              //  w.WriteLine("<link rel='stylesheet' href='http://localhost:59987/LSERP_New/Assets/css/style.css' type='text/css'/>");

                w.WriteLine("<link rel='stylesheet' type='text/css' href='https://innovasphere.com/LSERP/Assets/css/ep-style.css'/>");
                w.WriteLine("<link rel='stylesheet' href='https://innovasphere.com/LSERP/Assets/css/style.css' type='text/css'/>");

                w.WriteLine("</head><body>");
                w.WriteLine("<div style='text-align:center;padding-top:10px;font-size:20px;color:#00BCD4;'>");
                w.WriteLine("LoneStar");
                w.WriteLine("</div>");
                w.WriteLine("<div class='col-sm-12' style='text-align:center;padding-top:10px;font-size:20px;font-weight:bold;'>");
                w.WriteLine(lbltitle);
                w.WriteLine("<div>");
                w.WriteLine("<div class='col-sm-12' style='padding-top:10px;'>");
                w.WriteLine(div);
                w.WriteLine("<div>");
                w.WriteLine("</body></html>");
                w.Flush();
                w.Close();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        public void ShowInputSection(HtmlGenericControl divAdd, HtmlGenericControl divInput, HtmlGenericControl divOutput)
        {
            try
            {
                divAdd.Style.Add("display", "none");
                divInput.Style.Add("display", "block");
                divOutput.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        public void ShowOutputSection(HtmlGenericControl divAdd, HtmlGenericControl divInput, HtmlGenericControl divOutput)
        {
            try
            {
                divAdd.Style.Add("display", "block");
                divInput.Style.Add("display", "none");
                divOutput.Style.Add("display", "block");
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        #endregion

    }

}
