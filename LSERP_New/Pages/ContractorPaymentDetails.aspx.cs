using eplus.core;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ContractorPaymentDetails : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cProduction objP;
    string ContractorPaymentSavePath = ConfigurationManager.AppSettings["ContractorPaymentSavePath"].ToString();
    string ContractorPaymentHttpPath = ConfigurationManager.AppSettings["ContractorPaymentHttpPath"].ToString();

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                DataSet ds = new DataSet();
                objP = new cProduction();
                objP.GetContractorNameAndTeamNameDetails(ddlContractorName);
                BindContractorPaymentBillDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.ToDate = DateTime.ParseExact(txtTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.CTDID = Convert.ToInt32(ddlContractorName.SelectedValue);

            ds = objP.GetContractorMonthlyPaymentDetailsByContractorTeamName();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPrimaryJobOrderDetails.DataSource = ds.Tables[0];
                gvPrimaryJobOrderDetails.DataBind();

                ViewState["ConTractorPaymentDetails"] = ds.Tables[0];
            }
            else
            {
                gvPrimaryJobOrderDetails.DataSource = "";
                gvPrimaryJobOrderDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnBillCompleted_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        string BillDocs = "";
        string CPDID = "";
        try
        {
            decimal gc = Convert.ToDecimal(txtGeneralConsumable.Text);
            decimal billamount = Convert.ToDecimal(hdnBillAmount.Value);

            if (billamount > gc)
            {
                foreach (GridViewRow row in gvPrimaryJobOrderDetails.Rows)
                {
                    if (CPDID == "")
                        CPDID = gvPrimaryJobOrderDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        CPDID = CPDID + ',' + gvPrimaryJobOrderDetails.DataKeys[row.RowIndex].Values[0].ToString();
                }

                if (fbAttachFile.HasFile)
                {
                    cSales ojSales = new cSales();
                    cCommon objc = new cCommon();
                    objc.Foldername = ContractorPaymentSavePath;
                    BillDocs = Path.GetFileName(fbAttachFile.PostedFile.FileName);
                    string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                    string[] extension = BillDocs.Split('.');
                    BillDocs = MaximumAttacheID + txtBillNo.Text + extension[0].Trim().Replace("/", "") + '.' + extension[1];
                    objc.FileName = BillDocs;
                    objc.PID = "BillDocs";
                    objc.AttachementControl = fbAttachFile;
                    objc.SaveFiles();
                }
                else
                    BillDocs = "";
                objP.CPDIDs = CPDID;
                objP.GeneralConsumable = Convert.ToDecimal(txtGeneralConsumable.Text);
                objP.BillNo = txtBillNo.Text;
                objP.BillDate = DateTime.ParseExact(txtBillDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objP.Remarks = txtRemarks.Text;
                objP.AttachementName = BillDocs;
                objP.CreatedBy = Convert.ToInt32(objSession.employeeid);

                ds = objP.SaveContractorPaymentBillDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Bill details saved Succesfully');", true);
                    btnsubmit_Click(null, null);
                    BindContractorPaymentBillDetails();
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Consumable Amount Cannot Greater Bill Amount');", true);
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
            dt = (DataTable)ViewState["ConTractorPaymentDetails"];
            //CPDID,CPD.RFPDID

            dt.Columns.Remove("CPDID");
            dt.Columns.Remove("RFPDID");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "ContractorPaymentDetails" + ddlContractorName.SelectedItem.Text.Replace("/", "").ToString() + ".xls";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);
            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "Contractor Payment Report" + " For The Contractor Name Of "+ddlContractorName.SelectedItem.Text+" Date Between " + txtFromDate.Text + " And " + txtTodate.Text, "", 2, GemBoxKey);
            _objc.SaveExcelFile("ContractorPaymentDetails.aspx", LetterName, objSession.employeeid);
            //bindMaterialMaster();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPrimaryJobOrderDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "print")
                BindContractorPaymentPrintDetails(gvBillDetails.DataKeys[index].Values[0].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common methods"

    public void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("Contractor Payment Report");

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

    private void BindContractorPaymentBillDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            ds = objP.GetContractorPaymentBillDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBillDetails.DataSource = ds.Tables[0];
                gvBillDetails.DataBind();
            }
            else
            {
                gvBillDetails.DataSource = "";
                gvBillDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorPaymentPrintDetails(string CPBDID)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.CPBDID = Convert.ToInt32(CPBDID);
            ds = objP.GetContractorPaymentPrintDetailsByBillID();

            lblBillMonthYear_p.Text = "Bill Details For Month Of " + ds.Tables[0].Rows[0]["BillDate"].ToString();
            lblContractorName_p.Text = ds.Tables[1].Rows[0]["ContractorName"].ToString();
            lblteamname_p.Text = ds.Tables[1].Rows[0]["TeamName"].ToString();
            lblBillNo_p.Text = ds.Tables[0].Rows[0]["BillNo"].ToString();
            lblBillDate_p.Text = ds.Tables[0].Rows[0]["BillCreated"].ToString();
            lblBillAmount_p.Text = ds.Tables[0].Rows[0]["BillAmount"].ToString();
            lblRemarks_p.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            lblTotalAmount_p.Text = ds.Tables[0].Rows[0]["BillAmount"].ToString();
            lblGC_p.Text = ds.Tables[0].Rows[0]["GeneralCosumable"].ToString();

            gvContractorPaymentDetails_p.DataSource = ds.Tables[1];
            gvContractorPaymentDetails_p.DataBind();

            ViewState["Address"] = ds.Tables[2];
            GeneratePDF();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        string Flag;
        try
        {
            var sbPurchaseOrder = new StringBuilder();
            var sbGvItemAnnexure = new StringBuilder();
            var sbBellowAnnexure = new StringBuilder();

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                //imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "ContratorMonthlyBillDetaikls";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
            hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "PrintBillDetails('" + epstyleurl + "','" + Main + "','" + QrCode + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}