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

public partial class Pages_NewCustomerAddtionListReport : System.Web.UI.Page
{
    #region"DecLaration"

    cSession objSession = new cSession();
    cReports objRt;

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
                divNewCustomer.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindEnquiryProjectAssignmentDetails();
            divNewCustomer.Visible = true;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        cCommon _objc = new cCommon();
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "NewCustomerAddtionList.xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, strFile, LetterName, GemBoxKey);

            _objc.SaveExcelFile("NewCustomerAddtionListReport.aspx", LetterName, objSession.employeeid);
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
            string _key = gemBoxKey;
            SpreadsheetInfo.SetLicense(_key);

            ExcelFile workbook = new ExcelFile();
            ExcelWorksheet SheetLocation1;

            SheetLocation1 = workbook.Worksheets.Add("New Customer List");

            SheetLocation1 = exportex(dt, dt.Rows.Count, dt.Columns.Count, LetterName,
                "New Customer List For The Date Between " + txtFromDate.Text + " And " + txtToDate.Text + "", "", 2, SheetLocation1);

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

    protected void btnprint_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objRt = new cReports();
        try
        {
            objRt.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objRt.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objRt.GetMarketingReportDetailsByFromDateAndToDate("LS_GetNewCustomerAddtionListReport");

            lblTotalNoOfCustomer_p.Text = ds.Tables[0].Rows[0]["TotalNewCustomer"].ToString();

            lbldate_p.Text = "Total No Of New Customer List From The Date Between " + txtFromDate.Text + " And " + txtToDate.Text + " = ";

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvCustomerEnquiryDetails_p.DataSource = ds.Tables[1];
                gvCustomerEnquiryDetails_p.DataBind();
            }
            else
            {
                gvCustomerEnquiryDetails_p.DataSource = "";
                gvCustomerEnquiryDetails_p.DataBind();
            }

            gvCustomerEnquiryDetails_p.UseAccessibleHeader = true;
            gvCustomerEnquiryDetails_p.HeaderRow.TableSection = TableRowSection.TableHeader;

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                ViewState["QrCode"] = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "NewCustomerAddtionList";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                ViewState["QrCode"] = "";

            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ds.Tables[2];

            hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
            hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

            hdnDocNo.Value = ds.Tables[3].Rows[0]["DocNo"].ToString();
            hdnRevNo.Value = ds.Tables[3].Rows[0]["RevNo"].ToString();
            hdnRevDate.Value = ds.Tables[3].Rows[0]["RevDate"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "PrintNewCustomerAddtionList('" + QrCode + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindEnquiryProjectAssignmentDetails()
    {
        objRt = new cReports();
        DataSet ds = new DataSet();
        try
        {
            objRt.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objRt.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objRt.GetMarketingReportDetailsByFromDateAndToDate("LS_GetNewCustomerAddtionListReport");

            lblNumberOfCustomer.Text = ds.Tables[0].Rows[0]["TotalNewCustomer"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                ViewState["DataTable"] = ds.Tables[1];

                gvcustomerEnquiryDetails.DataSource = ds.Tables[1];
                gvcustomerEnquiryDetails.DataBind();
            }
            else
            {
                gvcustomerEnquiryDetails.DataSource = "";
                gvcustomerEnquiryDetails.DataBind();
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
        if (gvcustomerEnquiryDetails.Rows.Count > 0)
        {
            gvcustomerEnquiryDetails.UseAccessibleHeader = true;
            gvcustomerEnquiryDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}