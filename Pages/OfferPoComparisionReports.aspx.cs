using eplus.core;
using GemBox.Spreadsheet;
using JqDatatablesWebForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_OfferPoComparisionReports : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cReports objR;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;

            DateTimeOffset d1 = DateTimeOffset.UtcNow;
            long d11 = d1.ToUnixTimeMilliseconds();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                HttpContext.Current.Session["OfferPOComparision"] = null;
            }
            if (target == "ViewPOCopy")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewPOCopy(index);
            }
            if (target == "ViewOfferCopy")
            {
                string index = arg.ToString();
                OfferPrintDetails(index);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable ds = new DataTable();
            databind();
            ds = (DataTable)ViewState["OfferPOComparision"];

            ds.Columns.Remove("EODID");
            ds.Columns.Remove("POCopy");

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "Offer PO Comparision Reports" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, rowcount, ColumnCount, strFile, LetterName, "", "Offer PO Comparision Reports", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            GeneratePDDF();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"


    private void OfferPrintDetails(string EODID)
    {
        DataSet ds = new DataSet();
        cSales objSales = new cSales();
        try
        {
            objSales.EODID = Convert.ToInt32(EODID);
            ds = objSales.GetOfferPrintDetailsByEODID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMDDesignationName.Text = ds.Tables[0].Rows[0]["MD_Designation"].ToString();
                lblCustomerAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                lblPhoneNumber.Text = ds.Tables[0].Rows[0]["CustomerphoneNo"].ToString();
                lblFaxNumber.Text = ds.Tables[0].Rows[0]["Faxno"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["CustomerEmail"].ToString();
                lblCustomerContactName.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                lblCustomerPhoneNumber.Text = ds.Tables[0].Rows[0]["CustomerphoneNo"].ToString();
                lblCustomerMobileNumber.Text = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();
                lblCustomerEmail.Text = ds.Tables[0].Rows[0]["CustomerEmail"].ToString();
                lblOfferNo.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblOfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();
                lblsubjectItems.Text = ds.Tables[0].Rows[0]["SubJectItem"].ToString();
                lblReference.Text = ds.Tables[0].Rows[0]["Reference"].ToString();
                lblProjectDescription.Text = ds.Tables[0].Rows[0]["Projectname"].ToString();
                lblfrontpageres.Text = ds.Tables[0].Rows[0]["Frontpage"].ToString();

                lbl_offertype.InnerText = ds.Tables[0].Rows[0]["Offertype"].ToString() + " ";

                lblMarketingEngineer.Text = ds.Tables[0].Rows[0]["MarketingPersonName"].ToString();
                lblSalesPhoneNumber.Text = ds.Tables[0].Rows[0]["MarketingOfficePhoneNo"].ToString();
                lblSalesMobileNumber.Text = ds.Tables[0].Rows[0]["MarketingPersonMobileNo"].ToString();

                lblAnnexure1PopUpColumnHead.Text = "Annexure 1";
                lblAnnexure1OfferNumber.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblAnneure1OfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvAnnexure1_p.DataSource = ds.Tables[1];
                    gvAnnexure1_p.DataBind();
                }
                else
                {
                    gvAnnexure1_p.DataSource = "";
                    gvAnnexure1_p.DataBind();
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    gvAnnexure3_p.DataSource = ds.Tables[2];
                    gvAnnexure3_p.DataBind();
                }
                else
                {
                    gvAnnexure3_p.DataSource = "";
                    gvAnnexure3_p.DataBind();
                }


                lblAnnexure2PopColumnHead.Text = "Annexure 2";
                lblAnnexure2OfferNo.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblAnnexure2OfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();

                lblNote_p.Text = ds.Tables[4].Rows[0]["Note"].ToString();

                if (ds.Tables[3].Rows.Count > 0)
                {
                    gvAnnexure2_p.DataSource = ds.Tables[3];
                    gvAnnexure2_p.DataBind();
                }
                else
                {
                    gvAnnexure2_p.DataSource = "";
                    gvAnnexure2_p.DataBind();
                }

                lblCurrencySymbol_p.Text = ds.Tables[3].Rows[0]["CurrencySymbol"].ToString();

                if (ds.Tables[8].Rows[0]["ExForBasis"].ToString() == "0")
                    lblExForBasis_p.Text = "EX-Works";
                else
                    lblExForBasis_p.Text = "For Basis";

                if (ds.Tables[8].Rows[0]["QuotedPrice"].ToString() == "0")
                    lblsumofprice_p.Text = ds.Tables[7].Rows[0]["TotalPriceOfExWorks"].ToString();
                else
                    lblsumofprice_p.Text = "Quoted";

                lblAnnexure2MarketingHead.Text = ds.Tables[0].Rows[0]["MarketingHeadName"].ToString();
                lblFooterHeadDesignationWihMobNo.Text = ds.Tables[0].Rows[0]["MarketingHeadDesignation"].ToString();

                lblAnnnexure2MD.Text = ds.Tables[0].Rows[0]["MDName"].ToString();

                hdnAddress.Value = ds.Tables[5].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[5].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[5].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[5].Rows[0]["WebSite"].ToString();
                hdnCompanyName.Value = ds.Tables[5].Rows[0]["CompanyName"].ToString();
                hdnFormarlyCompanyName.Value = ds.Tables[5].Rows[0]["FormalCompanyName"].ToString();

                lblLeftfootercompanyName.Text = lblrightfootercompanyName.Text = ds.Tables[5].Rows[0]["Footer"].ToString();

                hdnAnnexure1CheckedLength.Value = ds.Tables[1].Rows.Count.ToString();
                hdnAnnexure3CheckedLength.Value = ds.Tables[2].Rows.Count.ToString();

                lblAnnexure3PopUpColumnHead.Text = "Annexure 3";
                lblAnnexure3offerNo.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblAnnexure3OfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvOfferChargesDetails_p.DataSource = ds.Tables[6];
                    gvOfferChargesDetails_p.DataBind();
                }
                else
                {
                    gvOfferChargesDetails_p.DataSource = "";
                    gvOfferChargesDetails_p.DataBind();
                }

                lblAnnexure1HeaderName.Text = "Annexure 1";

                //if (ds.Tables[8].Rows.Count > 0)



                string Annnexure1 = "";

                foreach (GridViewRow row in gvAnnexure1_p.Rows)
                {
                    Label lblheader = (Label)row.FindControl("lblSOWHeader");
                    if (Annnexure1 == "")
                        Annnexure1 = lblheader.Text;
                    else
                        Annnexure1 = Annnexure1 + ',' + lblheader.Text;
                }
                lblAnnexure1Header.Text = Annnexure1;

                lblAnnexure2HeaderName.Text = "Annexure 2";
                if (ds.Tables[0].Rows[0]["Offertype"].ToString().Trim() == "Technical")
                    lblAnnexure2Header.Text = "Unprice Schedule";
                else
                    lblAnnexure2Header.Text = "Price Schedule";

                lblAnnexure3HeaderName.Text = "Annexure 3";
                lblAnnexure3Header.Text = "Commercial Terms And Conditions";

                if (Convert.ToInt32(hdnAnnexure1CheckedLength.Value) > 0)
                {
                    lblAnnexure1PopUpColumnHead.Text = "Annexure 1";
                    lblAnnexure2PopColumnHead.Text = "Annexure 2";
                    lblAnnexure3PopUpColumnHead.Text = "Annexure 3";

                    divAnnexure1HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                    divAnnexure2HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                    divAnnexure3HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");

                    lblAnnexure1HeaderName.Text = "Annexure 1";
                    lblAnnexure2HeaderName.Text = "Annexure 2";
                    lblAnnexure3HeaderName.Text = "Annexure 3";
                }
                else
                {
                    lblAnnexure2PopColumnHead.Text = "Annexure 1";
                    lblAnnexure3PopUpColumnHead.Text = "Annexure 2";

                    divAnnexure1HeaderName.Attributes.Add("style", "display:none;padding-left: 15px; padding-right: 15px;");
                    divAnnexure2HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                    divAnnexure3HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");

                    lblAnnexure2HeaderName.Text = "Annexure 1";
                    lblAnnexure3HeaderName.Text = "Annexure 2";
                }

                cQRcode objQr = new cQRcode();

                string QrNumber = objQr.generateQRNumber(9);
                string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                string QrCode = objQr.QRcodeGeneration(Code);

                if (QrCode != "")
                {
                    imgQrcode.Attributes.Add("style", "display:block;");
                    imgQrcode.ImageUrl = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = "";
                    objQr.createdBy = objSession.employeeid;
                    objQr.saveQRNumber();
                }
                else
                    imgQrcode.Attributes.Add("style", "display:none;");

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();
                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

                string Main = url.Replace(Replacevalue, "Assets/css/main.css");
                string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
                string style = url.Replace(Replacevalue, "Assets/css/style.css");
                string Print = url.Replace(Replacevalue, "Assets/css/print.css");
                string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "PrintGenerateOffer('" + epstyleurl + "','" + style + "','" + Print + "','" + Main + "','" + topstrip + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Offer Is Not Available Contact System Adminstrator');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewPOCopy(int index)
    {
        string FileName;
        string EnquiryNumber;
        cReports objR = new cReports();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            //FileName = gvCustomerPo.DataKeys[index].Values[2].ToString();
            //EnquiryNumber = gvCustomerPo.DataKeys[index].Values[1].ToString();
            //objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);

            //EnquiryNumber = index.ToString();
            objR.PoCopy = Convert.ToInt32(index);
            ds = objR.GetFileName();
            EnquiryNumber = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
            FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
            objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void GeneratePDDF()
    {
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

        //    DataTable dtAddress = new DataTable();
        //    dtAddress = (DataTable)ViewState["Address"];

        //    hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
        //    hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
        //    hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
        //    hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();
        //    hdnCompanyName.Value = dtAddress.Rows[0]["CompanyName"].ToString();
        //    hdnFormalCompanyname.Value = dtAddress.Rows[0]["FormalCompanyName"].ToString();
        //    hdnCompanyNameFooter.Value = dtAddress.Rows[0]["CompanyNameFooter"].ToString();

        //    hdnLonestarLogo.Value = dtAddress.Rows[0]["LonestarLOGO"].ToString();
        //    hdnISOLogo.Value = dtAddress.Rows[0]["ISOLogo"].ToString();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintWorkOrderPO('" + ViewState["ApprovedTime"].ToString() + "','" + ViewState["POStatus"].ToString() + "','" + ViewState["ApprovedBy"].ToString() + "');", true);
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
                var worksheet = workbook.Worksheets.Add("OfferPOComparision");

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
                    worksheet.Cells[1, 0].Row.Height = 500;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (startRow == 0)
                    startRow = 2;

                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Name = "Times New Roman";
                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;

                worksheet.InsertDataTable(dt,
                   new InsertDataTableOptions()
                   {
                       ColumnHeaders = true,
                       StartRow = 2
                   });


                for (int i = 0; i < worksheet.CalculateMaxUsedColumns(); i++)
                {
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
                    worksheet.Rows[startRow + 1 + i].Style.Font.Weight = ExcelFont.MinWeight;
                    worksheet.Rows[startRow + 1 + i].Style.Font.Name = "Times New Roman";
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

    #endregion

    #region"Web Method"

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetData()
    {
        // Initialization.         
        DataTables da = new DataTables();
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

        //  Dictionary<string, object> data;
        string Jsonstring;
        string sessionname = "GeneralWorkOrder";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            // Initialization.    
            string search = HttpContext.Current.Request.Params["search[value]"];

            string column0 = HttpContext.Current.Request.Params["columns[0][search][value]"];
            string column1 = HttpContext.Current.Request.Params["columns[1][search][value]"];
            string column2 = HttpContext.Current.Request.Params["columns[2][search][value]"];
            string column3 = HttpContext.Current.Request.Params["columns[3][search][value]"];
            string column4 = HttpContext.Current.Request.Params["columns[4][search][value]"];
            string column5 = HttpContext.Current.Request.Params["columns[5][search][value]"];
            string column6 = HttpContext.Current.Request.Params["columns[6][search][value]"];
            string column7 = HttpContext.Current.Request.Params["columns[7][search][value]"];
            string column8 = HttpContext.Current.Request.Params["columns[8][search][value]"];
            string column9 = HttpContext.Current.Request.Params["columns[9][search][value]"];

            string[] strcol = { column0, column1, column2, column3, column4, column5, column6, column7, column9 };

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.    

            if (HttpContext.Current.Session["OfferPOComparision"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["OfferPOComparision"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["OfferPOComparision"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["EnquiryID"].ToString().ToLower().Contains(search.ToLower())
                          || p["CustomerName"].ToString().ToLower().Contains(search.ToLower())
                          || p["OfferNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["CompletedDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["PORefNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["PODate"].ToString().ToLower().Contains(search.ToLower())
                          || p["OfferCost"].ToString().ToLower().Contains(search.ToLower())
                          || p["POPrice"].ToString().ToLower().Contains(search.ToLower())
                          || p["EnquiryAlloted"].ToString().ToLower().Contains(search.ToLower())

                          ).ToList();
            }

            string[] strKey = { "EnquiryID","CustomerName","OfferNo", "CompletedDate", "PORefNo", "PODate", "OfferCost", "POPrice", "EnquiryAlloted",};

            for (int i = 0; i < strcol.Length; i++)
            {
                if (!string.IsNullOrEmpty(strcol[i]) && !string.IsNullOrWhiteSpace(strcol[i]) && strcol[i].ToString() != "select")
                {
                    dataList = dataList.Where(p =>
                             p[strKey[i]].ToString().ToLower().Equals(strcol[i].ToLower())).ToList();
                }
            }

            // Sorting.    
            //  data = SortByColumnWithOrder(order, orderDir, data);
            // Filter record count.    

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            int recFilter = dataList.Count;
            // Apply pagination. 

            //EnquiryID	ProspectName	Staff	DesignStart	BomApproved	RFPStatus

            dataList = dataList.Skip(startRec).Take(pageSize).ToList();
            // Loading drop down lists.    
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.Ldata = dataList;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        // Return info.    
        return da;
        //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        //js.MaxJsonLength = int.MaxValue;
        //return js.Serialize(da);
    }

    protected void databind()
    {

        // Initialization. 
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        try
        {
            ds = objR.GetOfferPOComparisionReports();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["OfferPOComparision"] = ds.Tables[0];
            }

        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
    }

    private static List<Dictionary<string, object>> LoadData()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            ds = objR.GetOfferPOComparisionReports();
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
            //Console.WriteLine(t1);
            //Console.WriteLine(t2);
            //Console.WriteLine(t3);
            //Console.ReadKey();
        }
        catch (Exception ex)
        {

        }
        // info.    
        return rows;
    }

    #endregion
}