using common;
using GemBox.Spreadsheet;
using JqDatatablesWebForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralWorkOrderReport : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cReports objR;

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
                HttpContext.Current.Session["GeneralWorkOrder"] = null;
            }
            if (target == "POPrint")
            {
                DataSet ds = new DataSet();
                GeneralWorkOrderIndentApproval gwoia = new GeneralWorkOrderIndentApproval();
                string Tax = "";
                string OtherCharges = "";
                int index = Convert.ToInt32(arg);
                gwoia.WPOID = index;
                ds = gwoia.GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompletedReports();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ApprovedTime"] = ds.Tables[0].Rows[0]["GWPOL1ApprovedDate"].ToString();
                    ViewState["POStatus"] = ds.Tables[0].Rows[0]["POStatus"].ToString();
                    ViewState["ApprovedBy"] = ds.Tables[0].Rows[0]["GWPOL1ApprovedBy"].ToString();

                    lblNote_p.Text = ds.Tables[0].Rows[0]["Note"].ToString();
                    lblSupplierChainVendorName_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                    lblReceiverAddress_p.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    lblConsigneeAddress_p.Text = lblConsigneeAddressRev1_p.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                    lblGSTNumber_p.Text = "<label> GST No: </label>" + ds.Tables[0].Rows[0]["GSTNO"].ToString();
                    lblRange_p.Text = ds.Tables[0].Rows[0]["Range"].ToString();

                    lblWoNo_p.Text = ds.Tables[0].Rows[0]["GWonumber"].ToString();
                    ViewState["WPONo"] = ds.Tables[0].Rows[0]["GWonumber"].ToString();
                    ViewState["PORevision"] = ds.Tables[0].Rows[0]["PORevision"].ToString();

                    lblWODate_p.Text = ds.Tables[0].Rows[0]["POCreated_On"].ToString();
                    lblQuoteRefNo_p.Text = ds.Tables[0].Rows[0]["QuateReferenceNumber"].ToString();
                    lblDeliveryDate_p.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                    lblPayment_p.Text = ds.Tables[0].Rows[0]["PaymentDays"].ToString();

                    lblSubTotal_p.Text = ds.Tables[5].Rows[0]["SubTotal"].ToString();
                    lblTotalAmount_p.Text = ds.Tables[5].Rows[0]["TotalAmount"].ToString();

                    lblRupeesInwords_p.Text = ds.Tables[0].Rows[0]["AmountInRupees"].ToString();

                    gvWorkOrderPOItemDetails_p.DataSource = ds.Tables[6];
                    gvWorkOrderPOItemDetails_p.DataBind();

                    if (ds.Tables[1].Rows[0]["CompanyID"].ToString() == "1")
                    {
                        divGSTREV0.Visible = true;
                        divGSTREV1.Visible = false;

                        lblTNGSTNo.Text = "<label> TNGST No: </label>" + ds.Tables[2].Rows[0]["TNGSTNo"].ToString();
                        lblCSTNo.Text = "<label> CST No: </label>" + ds.Tables[2].Rows[0]["CSTNo"].ToString();
                        lblECCNo.Text = "<label> ECC No: </label>" + ds.Tables[2].Rows[0]["ECCNo"].ToString();
                        lblTINNo.Text = "<label> TIN No: </label>" + ds.Tables[2].Rows[0]["TINNo"].ToString();
                        lblGSTNo.Text = "<label> GST No: </label>" + ds.Tables[2].Rows[0]["GSTNumber"].ToString();
                    }
                    else
                    {
                        divGSTREV0.Visible = false;
                        divGSTREV1.Visible = true;
                        lblCINNo_P.Text = "<label> CIN No: </label>" + ds.Tables[2].Rows[0]["CINNo"].ToString();
                        lblPANNo_p.Text = "<label> PAN No: </label>" + ds.Tables[2].Rows[0]["PANNumber"].ToString();
                        lblTANNo_p.Text = "<label> TAN No: </label>" + ds.Tables[2].Rows[0]["TANNo"].ToString();
                        lblGSTIN_p.Text = "<label> GSTIN No: </label>" + ds.Tables[2].Rows[0]["GSTNumber"].ToString();
                    }

                    ViewState["Address"] = ds.Tables[2];

                    StringBuilder sbCharges;
                    string sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                    sbCharges = new StringBuilder();
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                        sb = sb.Replace("lblchargesname", dr["ChargesName"].ToString());
                        sb = sb.Replace("textname", dr["Value"].ToString());
                        sbCharges.Append(sb);
                    }
                    divothercharges_p.InnerHtml = sbCharges.ToString();

                    sbCharges = new StringBuilder();
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                        sb = sb.Replace("lblchargesname", dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % ");
                        sb = sb.Replace("textname", dr["Amount"].ToString());
                        sbCharges.Append(sb);
                    }
                    divtax_p.InnerHtml = sbCharges.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPoPrintPopUp();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Please Add  Item  Details');", true);


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
            ds = (DataTable)ViewState["GeneralWorkOrder"];

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "General WorkOrder Reports" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, rowcount, ColumnCount, strFile, LetterName, "", "General WorkOrder Reports", 2, GemBoxKey);
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

            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
            hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();
            hdnCompanyName.Value = dtAddress.Rows[0]["CompanyName"].ToString();
            hdnFormalCompanyname.Value = dtAddress.Rows[0]["FormalCompanyName"].ToString();
            hdnCompanyNameFooter.Value = dtAddress.Rows[0]["CompanyNameFooter"].ToString();

            hdnLonestarLogo.Value = dtAddress.Rows[0]["LonestarLOGO"].ToString();
            hdnISOLogo.Value = dtAddress.Rows[0]["ISOLogo"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintWorkOrderPO('" + ViewState["ApprovedTime"].ToString() + "','" + ViewState["POStatus"].ToString() + "','" + ViewState["ApprovedBy"].ToString() + "');", true);
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
                var worksheet = workbook.Worksheets.Add("GeneralWorkOrder");

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

            if (HttpContext.Current.Session["GeneralWorkOrder"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["GeneralWorkOrder"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["GeneralWorkOrder"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["EnquiryID"].ToString().ToLower().Contains(search.ToLower())
                          || p["CustomerEnquiryNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["LseNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["ProspectName"].ToString().ToLower().Contains(search.ToLower())
                          || p["EnquiryTypeName"].ToString().ToLower().Contains(search.ToLower())
                          || p["ConTactPerson"].ToString().ToLower().Contains(search.ToLower())
                          || p["ContactNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["Email"].ToString().ToLower().Contains(search.ToLower())
                          || p["ReceivedDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["ClosingDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["ProjectDescription"].ToString().ToLower().Contains(search.ToLower())
                          || p["EnquiryLocation"].ToString().ToLower().Contains(search.ToLower())
                          || p["SourceName"].ToString().ToLower().Contains(search.ToLower())
                          || p["EmployeeName"].ToString().ToLower().Contains(search.ToLower())
                          || p["OfferSubmissionDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["Staff"].ToString().ToLower().Contains(search.ToLower())
                          || p["Offer No"].ToString().ToLower().Contains(search.ToLower())
                          || p["Offer Date"].ToString().ToLower().Contains(search.ToLower())
                          || p["Order No"].ToString().ToLower().Contains(search.ToLower())
                          || p["Order Date"].ToString().ToLower().Contains(search.ToLower())
                          || p["ItemCount"].ToString().ToLower().Contains(search.ToLower())
                          || p["RFPNo"].ToString().ToLower().Contains(search.ToLower())

                          ).ToList();
            }

            string[] strKey = { "EnquiryID","CustomerEnquiryNumber","LseNumber", "ProspectName", "EnquiryTypeName", "ConTactPerson", "ContactNumber",
            "Email","ReceivedDate","ClosingDate","ProjectDescription", "EnquiryLocation","SourceName","EmployeeName", "OfferSubmissionDate",
            "Staff", "Offer No", "Offer Date", "Order No","Order Date","ItemCount","RFPNo",};

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
            ds = objR.GetGeneralWorkOrderDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["GeneralWorkOrder"] = ds.Tables[0];
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
            ds = objR.GetGeneralWorkOrderDetails();
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