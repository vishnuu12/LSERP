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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eplus.core
{
    public class cCommon : System.Web.UI.MasterPage
    {

        #region "Declaration"

        DataSet ds = new DataSet();
        cDataAccess DAL = new cDataAccess();
        SqlCommand c = new SqlCommand();
        cSession objSession = new cSession();
        string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
        string BaseHtttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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

        public string Foldername { get; set; }
        public string FileName { get; set; }
        public string PID { get; set; }
        public FileUpload AttachementControl { get; set; }
        public int ProspectID { get; set; }

        public string url { get; set; }
        public string epstyleurl { get; set; }
        public string style { get; set; }
        public string Print { get; set; }
        public string Main { get; set; }
        public string topstrip { get; set; }

        public string fromDate { get; set; }
        public string toDate { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int UserID { get; set; }
        public int UserTypeID { get; set; }

        public int LocationID { get; set; }
        public string EnquiryID { get; set; }

        public int RFPHID { get; set; }
        public int JCHID { get; set; }
        public string ISORecordsCodeNo { get; set; }

        #endregion

        #region "Common Methods"

        public void ViewFileName(string SavePath, string HttpPath, string FileName, string ID, HtmlGenericControl ifrm)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            try
            {
                string BaseHttpPath = HttpPath + ID + "//";

                ifrm.Attributes.Add("src", BaseHttpPath + FileName);

                if (File.Exists(SavePath + ID + "//" + FileName))
                {
                    // ScriptManager.RegisterStartupScript(page, page.GetType(), "Loader", "ShowViewPopUp();", true);
                    string s = "window.open('" + BaseHttpPath + FileName + "','_blank');";
                    page.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else
                {
                    //  ifrm.Attributes.Add("src", "");
                    string s = "window.open('" + BaseHttpPath + FileName + "','_blank');";
                    page.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    // ScriptManager.RegisterStartupScript(page, page.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                }
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
		
        public void GeneralViewFileName(string SavePath, string HttpPath, string FileName, string ID, HtmlGenericControl ifrm)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            try
            {
                string BaseHttpPath = HttpPath + "//";

                ifrm.Attributes.Add("src", BaseHttpPath + FileName);

                if (File.Exists(SavePath + "//" + FileName))
                {
                    // ScriptManager.RegisterStartupScript(page, page.GetType(), "Loader", "ShowViewPopUp();", true);
                    string s = "window.open('" + BaseHttpPath + FileName + "' ,'', '_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=500,left=500,width=900,height=900');";
                    page.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else
                {
                    //  ifrm.Attributes.Add("src", "");
                    string s = "window.open('" + BaseHttpPath + FileName + "','_blank');";
                    page.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    // ScriptManager.RegisterStartupScript(page, page.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                }
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        public string SaveFiles()
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string reslt = "";
            try
            {
                string path = Foldername + PID + "\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!string.IsNullOrEmpty(FileName))
                    AttachementControl.SaveAs(path + FileName);

                reslt = "saved";
            }
            catch (Exception ex)
            {
                reslt = "failed";
                Log.Message(ex.ToString());
            }
            return reslt;
        }

        #region "Excel"

        public void ReportExcelDownload(DataTable dt, string UserID)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();
                string[] pagename = url.Split('/');

                string MAXEXID = "";
                MAXEXID = GetMaximumNumberExcel();

                int rowcount = Convert.ToInt32(dt.Rows.Count);
                int ColumnCount = Convert.ToInt32(dt.Columns.Count);

                string strFile = "";

                string LetterName = MAXEXID + ".xlsx";

                string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

                string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

                strFile = LetterPath + LetterName;

                if (!Directory.Exists(LetterPath))
                    Directory.CreateDirectory(LetterPath);

                if (File.Exists(strFile))
                    File.Delete(strFile);
                exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "LoneStar", "", 2, GemBoxKey);
                SaveExcelFile(pagename[pagename.Length - 1], LetterName, UserID);
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
                    var worksheet = workbook.Worksheets.Add("Sheet1");

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
                System.Drawing.Image img = (System.Drawing.Image)QRCodeImage;
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
                converter.Options.MarginLeft = 0;
                converter.Options.MarginRight = 0;
                converter.Options.MarginTop = 0;
                converter.Options.MarginBottom = 0;
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
                else if (extension == "PNG")
                    HttpContext.Current.Response.ContentType = "Application/PNG";
                else if (extension == "ZIP")
                    HttpContext.Current.Response.ContentType = "Application/ZIP";
                else if (extension == "RAR")
                    HttpContext.Current.Response.ContentType = "Application/RAR";

                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                HttpContext.Current.Response.TransmitFile(StrFile);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                //HttpContext.Current.Response.WriteFile(StrFile);
                //HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        public static void DownLoad(string FileNameAndEnquiryNumber)
        {
            try
            {
                string FileName = FileNameAndEnquiryNumber.Split('/')[0];
                string EnquiryNumber = FileNameAndEnquiryNumber.Split('/')[1];

                string BaseHtttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
                string strFile = BaseHtttpPath + EnquiryNumber + "//" + FileName;

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
                else if (extension == "PNG")
                    HttpContext.Current.Response.ContentType = "Application/PNG";

                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                HttpContext.Current.Response.TransmitFile(strFile);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        #endregion

        #region "Common"

        public void customerddlchnage(DropDownList ddlCustomerName, DropDownList ddlEnquiryNumber, DataTable dcustomr, DataTable denquiry)
        {
            DataView dv;
            try
            {
                if (ddlCustomerName.SelectedIndex > 0)
                {
                    dv = new DataView(dcustomr);
                    dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                    dcustomr = dv.ToTable();

                    ddlEnquiryNumber.DataSource = dcustomr;
                    ddlEnquiryNumber.DataTextField = "EnquiryName";
                    ddlEnquiryNumber.DataValueField = "EnquiryID";
                    ddlEnquiryNumber.DataBind();
                }
                else
                {
                    ddlEnquiryNumber.DataSource = denquiry;
                    ddlEnquiryNumber.DataTextField = "EnquiryName";
                    ddlEnquiryNumber.DataValueField = "EnquiryID";
                    ddlEnquiryNumber.DataBind();
                }

                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

                //divInfo.Visible = false;
                //divGrid.Visible = false;

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
        public void enquiryddlchange(DropDownList ddlEnquiryNumber, DropDownList ddlCustomerName, DataTable dcustomr, DataTable denquiry)
        {
            try
            {
                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dcustomr);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dcustomr = dv.ToTable();
                    ddlCustomerName.SelectedValue = dcustomr.Rows[0]["ProspectID"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

        public DataSet getCustomerNameByUserID(int UserID, DropDownList ddlCustomerName, string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "ProspectName";
                ddlCustomerName.DataValueField = "ProspectID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet getEnquiryNumberByProspectID(DropDownList ddlEnquiryNumber)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetEnquiryNumberByProspectID";
                c.Parameters.Add("@ProspectID", ProspectID);
                c.Parameters.Add("@EmployeeID", objSession.employeeid);
                DAL.GetDataset(c, ref ds);
                ddlEnquiryNumber.DataSource = ds.Tables[0];
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }
        public DataSet GetEnquiryNumberByUserID(int UserID, DropDownList ddlEnquiryNumber, string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlEnquiryNumber.DataSource = ds.Tables[0];
                ddlEnquiryNumber.DataTextField = "EnquiryName";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }
        public DataSet GetCustomerPODetailsByUserID(int UserID, DropDownList ddlCustomerPO, string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                //c.CommandText = "LS_GetPOHIDByUserID";
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlCustomerPO.DataSource = ds.Tables[0];
                ddlCustomerPO.DataTextField = "PORefNo";
                ddlCustomerPO.DataValueField = "POHID";
                ddlCustomerPO.DataBind();
                ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetRFPDetailsByUserID(int UserID, DropDownList ddlRFPNo)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetRFPDetailsByUserID";
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlRFPNo.DataSource = ds.Tables[0];
                ddlRFPNo.DataTextField = "RFPNo";
                ddlRFPNo.DataValueField = "RFPHID";
                ddlRFPNo.DataBind();
                ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetRFPDetailsByUserIDAndRFPtatusCompleted(int UserID, DropDownList ddlRFPNo, string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlRFPNo.DataSource = ds.Tables[0];
                ddlRFPNo.DataTextField = "RFPNo";
                ddlRFPNo.DataValueField = "RFPHID";
                ddlRFPNo.DataBind();
                ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetLocationDetails(DropDownList ddlLocation)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetLocationDetails";
                DAL.GetDataset(c, ref ds);

                ddlLocation.DataSource = ds.Tables[0];
                ddlLocation.DataTextField = "Location";
                ddlLocation.DataValueField = "LocationID";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetPINumberByUserID(DropDownList ddlPIIndentNumber, DropDownList ddlRFNo, int UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetPurchaseIndentNumberByUserID";
                c.Parameters.Add("@UserID", UserID);
                DAL.GetDataset(c, ref ds);

                ddlPIIndentNumber.DataSource = ds.Tables[0];
                ddlPIIndentNumber.DataTextField = "PINumber";
                ddlPIIndentNumber.DataValueField = "QHID";
                ddlPIIndentNumber.DataBind();
                ddlPIIndentNumber.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlRFNo.DataSource = ds.Tables[1];
                ddlRFNo.DataTextField = "RFPNo";
                ddlRFNo.DataValueField = "RFPHID";
                ddlRFNo.DataBind();
                ddlRFNo.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlRFNo.Items.Insert(Convert.ToInt32(ds.Tables[1].Rows.Count) + 1, new ListItem("GENERAL", "G"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetRFPDetailsByUserIDInAddtionalPartBOMRequest(int UserID, DropDownList ddlRFPNo)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetRFPDetailsByUserIDInAddtionalPartRequest";
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlRFPNo.DataSource = ds.Tables[0];
                ddlRFPNo.DataTextField = "RFPNo";
                ddlRFPNo.DataValueField = "RFPHID";
                ddlRFPNo.DataBind();
                ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }


        public DataSet getRFPCustomerNameByUserID(int UserID, DropDownList ddlCustomerName)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetRFPCustomerNameByUserID";
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "ProspectName";
                ddlCustomerName.DataValueField = "ProspectID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet getRFPCustomerNameByQualityUserID(int UserID, DropDownList ddlCustomerName)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);
                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "ProspectName";
                ddlCustomerName.DataValueField = "ProspectID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }



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
                w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
                w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
                w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
                w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
                w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
                //w.WriteLine("<link rel='stylesheet' type='text/css' href='https://innovasphere.com/LSERP/Assets/css/ep-style.css'/>");
                //w.WriteLine("<link rel='stylesheet' href='https://innovasphere.com/LSERP/Assets/css/style.css' type='text/css'/>");
                w.WriteLine("</head><body>");
                w.WriteLine("<div style='text-align:center;font-size:20px;color:#00BCD4;'>");
                //w.WriteLine("LoneStar");
                w.WriteLine("</div>");
                w.WriteLine("<div class='col-sm-12' style='font-size:20px;font-weight:bold;'>");
                //w.WriteLine(lbltitle);
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

        public DataSet GetContractorDetails()
        {
            DAL = new cDataAccess();
            ds = new DataSet();
            try
            {
                DAL.GetDataset("LS_GetContractorDetails", ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex);
            }
            return ds;
        }

        public bool Validate(HtmlGenericControl div)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string error = "";
            bool msg = true;
            foreach (Control c in div.Controls)
            {
                if (c is TextBox)
                {

                    TextBox txt = (TextBox)c;

                    if (txt.CssClass != null || txt.CssClass != "")
                    {
                        if (txt.CssClass.Contains("mandatoryfield"))
                        {
                            if (txt.Text == "")
                            {
                                if (error == "")
                                    error = txt.ClientID;
                                else
                                    error = error + '/' + txt.ClientID;
                            }
                        }
                    }
                }
                if (c is DropDownList)
                {
                    DropDownList ddl = (DropDownList)c;

                    if (ddl.CssClass != null || ddl.CssClass != "")
                    {
                        if (ddl.CssClass.Contains("mandatoryfield"))
                        {
                            if (ddl.SelectedIndex == 0)
                            {
                                if (error == "")
                                    error = ddl.ClientID;
                                else
                                    error = error + '/' + ddl.ClientID;
                            }
                        }
                    }
                }
            }

            if (error != "")
            {
                msg = false;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "Validate", "ServerValidation('" + error + "');", true);
            }

            return msg;
        }

        public void EmptyDropDownList(DropDownList ddl)
        {
            ddl.DataSource = "";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public DataSet GetReportsyUserID(string spName)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spName;
                c.Parameters.Add("@UserID", UserID);
                if (fromDate == "")
                    c.Parameters.Add("@FromDate", DBNull.Value);
                else
                    c.Parameters.Add("@FromDate", FromDate);
                if (toDate == "")
                    c.Parameters.Add("@ToDate", DBNull.Value);
                else
                    c.Parameters.Add("@ToDate", ToDate);

                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEnquiryOfferOrderStatus(string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                if (fromDate == "")
                    c.Parameters.Add("@FromDate", DBNull.Value);
                else
                    c.Parameters.Add("@FromDate", FromDate);
                if (toDate == "")
                    c.Parameters.Add("@ToDate", DBNull.Value);
                else
                    c.Parameters.Add("@ToDate", ToDate);

                c.Parameters.Add("@UserID", UserID);
                c.Parameters.Add("@UserTypeID", UserTypeID);

                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetOfferPendingPriceApproval(string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                if (fromDate == "")
                    c.Parameters.Add("@FromDate", DBNull.Value);
                else
                    c.Parameters.Add("@FromDate", FromDate);
                if (toDate == "")
                    c.Parameters.Add("@ToDate", DBNull.Value);
                else
                    c.Parameters.Add("@ToDate", ToDate);

                c.Parameters.Add("@UserTypeID", UserTypeID);
                c.Parameters.Add("@UserID", UserID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEnquiryClarrificationForMarketingAndDesign(string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                if (fromDate == "")
                    c.Parameters.Add("@FromDate", DBNull.Value);
                else
                    c.Parameters.Add("@FromDate", FromDate);
                if (toDate == "")
                    c.Parameters.Add("@ToDate", DBNull.Value);
                else
                    c.Parameters.Add("@ToDate", ToDate);

                c.Parameters.Add("@UserTypeID", UserTypeID);
                c.Parameters.Add("@UserID", UserID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetReceivedPOFromCustomerToRFPReleaseDate(string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                if (fromDate == "")
                    c.Parameters.Add("@FromDate", DBNull.Value);
                else
                    c.Parameters.Add("@FromDate", FromDate);
                if (toDate == "")
                    c.Parameters.Add("@ToDate", DBNull.Value);
                else
                    c.Parameters.Add("@ToDate", ToDate);

                c.Parameters.Add("@UserTypeID", UserTypeID);
                c.Parameters.Add("@UserID", UserID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetLOcationAddressByLOcationID()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetLocationDetailsByLocationID";
                c.Parameters.AddWithValue("@LocationID", LocationID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }


        public void ReadhtmlFile(string pdffile, HiddenField hdnpdfContent)
        {
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string PDFhttpPath = ConfigurationManager.AppSettings["PDFPath"].ToString();
            string pdfpath = LetterPath + pdffile;
            var page = HttpContext.Current.CurrentHandler as Page;
            if (File.Exists(pdfpath))
            {
                //string fileName = PDFhttpPath + pdffile;

                StreamReader sr = new StreamReader(pdfpath);
                string htmlString = sr.ReadToEnd();
                htmlString = htmlString.Replace(Environment.NewLine, "");
                htmlString = htmlString.Replace("\n", String.Empty);
                htmlString = htmlString.Replace("\r", String.Empty);
                htmlString = htmlString.Replace("\t", String.Empty);

                sr.Close();
                sr.Dispose();

                hdnpdfContent.Value = htmlString;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "();", true);
                ScriptManager.RegisterStartupScript(page, page.GetType(), "Loader", "PrintHtmlFile();", true);
            }
            else
                ScriptManager.RegisterStartupScript(page, page.GetType(), "PopUp", "InfoMessage('Information','File Not Found');", true);
        }

        public DataSet GetAddress()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetAddress";
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetQualityRFPDetailsByUserID(int UserID, DropDownList ddlRFPNo)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetQualityRFPDetailsByUserID";
                c.Parameters.Add("@EmployeeID", UserID);
                DAL.GetDataset(c, ref ds);

                ddlRFPNo.DataSource = ds.Tables[0];
                ddlRFPNo.DataTextField = "RFPNo";
                ddlRFPNo.DataValueField = "RFPHID";
                ddlRFPNo.DataBind();
                ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetCurrencyDetails(DropDownList ddlCurrency)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetCurrencyDetails";
                DAL.GetDataset(c, ref ds);

                ddlCurrency.DataSource = ds.Tables[0];
                ddlCurrency.DataTextField = "";
                ddlCurrency.DataValueField = "";
                ddlCurrency.DataBind();
                ddlCurrency.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }


        public DataSet deleteDailyActivitiesReportDetailsDetailsByID(int id, string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@id", id);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEmployeeIDDetailsByUserTypeIDSANDErpUserType(string UserTypeIDS, int ERPUserTypeID)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetEmployeeIDSDetailsByUserTypeIDSAndERPUserTypeID";
                c.Parameters.Add("@UserTypeIDS", UserTypeIDS);
                c.Parameters.Add("@ERPUserTypeID", ERPUserTypeID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEmployeeIDByEnquiryID(string spname)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EnquiryID", EnquiryID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet getCustomerNameByUserIDAndType(int UserID, DropDownList ddlCustomerName, string spname, string Type)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                c.Parameters.Add("@Type", Type);
                DAL.GetDataset(c, ref ds);
                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "ProspectName";
                ddlCustomerName.DataValueField = "ProspectID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEnquiryNumberByUserIDAndType(int UserID, DropDownList ddlEnquiryNumber, string spname, string Type)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                c.Parameters.Add("@Type", Type);
                DAL.GetDataset(c, ref ds);
                ddlEnquiryNumber.DataSource = ds.Tables[0];
                ddlEnquiryNumber.DataTextField = "EnquiryName";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetStaffNameDetailsByEnquiryID()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetStaffNameDetailsByEnquiryID";
                c.Parameters.Add("@EnquiryID", EnquiryID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetStaffDetailsByRFPHIDAndUserType()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetStaffDetailsByRFPHID";
                c.Parameters.Add("@RFPHID", RFPHID);
                c.Parameters.Add("@JCHID", JCHID);
                c.Parameters.Add("@UserTypeID", UserTypeID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetCustomerNameByPendingList(int UserID, DropDownList ddlCustomerName, string spname, string status)
        {
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                c.Parameters.Add("@status", status);
                DAL.GetDataset(c, ref ds);

                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "ProspectName";
                ddlCustomerName.DataValueField = "ProspectID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEnquiryNumberByPendingList(int UserID, DropDownList ddlCustomerName, string spname, string status)
        {
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                c.Parameters.Add("@status", status);
                DAL.GetDataset(c, ref ds);

                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "EnquiryNumber";
                ddlCustomerName.DataValueField = "EnquiryID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetRFPNoByPendingList(int UserID, DropDownList ddlCustomerPO, string spname, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                c.Parameters.Add("@EmployeeID", UserID);
                c.Parameters.Add("@status", status);
                DAL.GetDataset(c, ref ds);
                ddlCustomerPO.DataSource = ds.Tables[0];
                ddlCustomerPO.DataTextField = "PORefNo";
                ddlCustomerPO.DataValueField = "POHID";
                ddlCustomerPO.DataBind();
                ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }


        public DataSet GetAutomatedAlertdetails()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetAutomatedAlertDetails";
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetISODocNoDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.Add("@CodeNo", ISORecordsCodeNo);
                c.CommandText = "LS_GetQMSListDetails";
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetRFPNoDetailsByProspectID(DropDownList ddlRFPNo)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetRFPNoDetailsByProspectIDAndUserID";
                c.Parameters.Add("@ProspectID", ProspectID);
                c.Parameters.Add("@userID", userID);
                DAL.GetDataset(c, ref ds);

                ddlRFPNo.DataSource = ds.Tables[0];
                ddlRFPNo.DataTextField = "RFPNo";
                ddlRFPNo.DataValueField = "RFPHID";
                ddlRFPNo.DataBind();
                ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetCustomerNameDetailsByRFPHID()
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetCustomerNameByRFPHID";
                c.Parameters.Add("@RFPHID", RFPHID);
                DAL.GetDataset(c, ref ds);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        #endregion

        public DataTable ExcelToDataTable(string path)
        {
            // If using Professional version, put your serial key below.
            string Key = ConfigurationManager.AppSettings["GemBoxKey"].ToString();
            SpreadsheetInfo.SetLicense(Key);

            var workbook = ExcelFile.Load(path);

            // Select the first worksheet from the file.
            var worksheet = workbook.Worksheets[0];

            // Create DataTable from an Excel worksheet.
            var dataTable = worksheet.CreateDataTable(new CreateDataTableOptions()
            {
                ColumnHeaders = true,
                StartRow = 0,
                NumberOfColumns = worksheet.Columns.Count + 4,
                NumberOfRows = worksheet.Rows.Count,
                Resolution = ColumnTypeResolution.AutoPreferStringCurrentCulture
            });

            return dataTable;
        }

        public DataSet GetCustomerNameDetailsByQCReport(DropDownList ddlCustomerName)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetCustomerNameDetailsByQCReport";
                DAL.GetDataset(c, ref ds);

                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "ProspectName";
                ddlCustomerName.DataValueField = "ProspectID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetRFPNoDetailsByQCReport(DropDownList ddlRFPNo)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetRFPNoDetilsByQCReport";
                DAL.GetDataset(c, ref ds);

                ddlRFPNo.DataSource = ds.Tables[0];
                ddlRFPNo.DataTextField = "RFPNo";
                ddlRFPNo.DataValueField = "RFPHID";
                ddlRFPNo.DataBind();
                ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetItemNameDetailsByRFPHIDQCReport(DropDownList ddlItemName)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.Parameters.Add("@RFPHID", RFPHID);
                c.CommandText = "LS_GetItemNameDetailsByQCReport";
                DAL.GetDataset(c, ref ds);

                ddlItemName.DataSource = ds.Tables[0];
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "RFPDID";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }	
        public DataSet GetEnquiryNumberForReports(DropDownList ddlCustomerName, string spname)
        {
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = spname;
                DAL.GetDataset(c, ref ds);

                ddlCustomerName.DataSource = ds.Tables[0];
                ddlCustomerName.DataTextField = "EnquiryNumber";
                ddlCustomerName.DataValueField = "EnquiryID";
                ddlCustomerName.DataBind();
                ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }

        public DataSet GetPODetails(DropDownList ddlPO,string mrnchangestatus)
        {
            DataSet ds = new DataSet();
            try
            {
                DAL = new cDataAccess();
                c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetPODetails";
                c.Parameters.Add("@mrnchangestatus", mrnchangestatus);
                DAL.GetDataset(c, ref ds);

                ddlPO.DataSource = ds.Tables[0];
                ddlPO.DataTextField = "SPONumber";
                ddlPO.DataValueField = "SPOID";
                ddlPO.DataBind();
                ddlPO.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
            return ds;
        }


    }



}
