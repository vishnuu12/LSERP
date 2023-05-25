using JqDatatablesWebForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DcReports : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cStores objSt;
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
            if (!IsPostBack)
            {
                HttpContext.Current.Session["DCReportSheet"] = null;
            }
            if (target == "PrintVendorDC")
                PrintVendorDC(Convert.ToInt32(arg.ToString()));
            if (target == "PrintPO")
                PrintWorkOrderPODetails(Convert.ToInt32(arg.ToString()));
            if (target == "PrintDC")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "InternalDCPrint.aspx?IDCID=" + arg.ToString() + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            if (target == "PrintODC")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "OTHERDCPrint.aspx?OTHID=" + arg.ToString() + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    protected void btnpost_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

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
        string sessionname = "DCReportSheet";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            // Initialization.    
            string ModeName = HttpContext.Current.Request.Params["Mode"];
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

            string[] strcol = { column0, column1, column2, column3, column4, column5, column6, column7 };

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.
            //
            if (HttpContext.Current.Session["sessionname"] == null || HttpContext.Current.Session["ModeName"].ToString() != ModeName)
            {
                dataList = LoadData(ModeName);
                HttpContext.Current.Session["DCReportSheet"] = dataList;
                HttpContext.Current.Session["ModeName"] = ModeName;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["DCReportSheet"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["DCNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["DCDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["FormJJNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["TariffClassification"].ToString().ToLower().Contains(search.ToLower())
                          || p["Duration"].ToString().ToLower().Contains(search.ToLower())
                          || p["DutyDetailsDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["Location"].ToString().ToLower().Contains(search.ToLower())
                          || p["RawMatQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["Value"].ToString().ToLower().Contains(search.ToLower())

                          ).ToList();
            }

            string[] strKey = { "DCNo", "DCDate" };

            for (int i = 0; i < strcol.Length; i++)
            {
                if (!string.IsNullOrEmpty(strcol[i]) && !string.IsNullOrWhiteSpace(strcol[i]))
                {
                    dataList = dataList.Where(p =>
                             p["DCNo"].ToString().ToLower().Contains(strcol[i].ToLower())).ToList();
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

    private static List<Dictionary<string, object>> LoadData(string Modename)
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cPurchase objPc = new cPurchase();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objPc = new cPurchase();
            ds = objPc.GetDetailsByVendorDCReports(Modename);

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
        }
        catch (Exception ex)
        {

        }
        // info.    
        return rows;
    }

    #endregion

    #region"Common Method"

    private void PrintWorkOrderPODetails(int index)
    {
        try
        {
            DataSet ds = new DataSet();
            objPc = new cPurchase();
            objPc.WPOID = index;
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "WorkorderPoPrint.aspx?WPOID=" + objPc.WPOID + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void PrintVendorDC(int index)
    {
        try
        {
            DataSet ds = new DataSet();
            objPc = new cPurchase();
            objPc.VDCID = index;
            ds = objPc.GetVendorDCDetailsByVDCIDForPDF();
            //VDCID	WPOID DCDateEdit TariffClassification DutyDetailsDate	DutyDetailsDateEdit	LocationID	Location	Address
            ViewState["Address"] = ds;

            DataTable dt2;
            dt2 = (DataTable)ds.Tables[2];

            hdnAddress.Value = ds.Tables[0].Rows[0]["Address"].ToString();

            hdnTINNo.Value = dt2.Rows[0]["TINNo"].ToString();
            hdnCodeNo.Value = dt2.Rows[0]["CodeNo"].ToString();
            hdnCSTNo.Value = dt2.Rows[0]["CSTNo"].ToString();
            hdnGSTNumber.Value = dt2.Rows[0]["GSTNumber"].ToString();
            hdnCompanyName.Value = dt2.Rows[0]["CompanyName"].ToString();

            lblWONo_p.Text = ds.Tables[0].Rows[0]["Wonumber"].ToString();
            lblFormJJno_p.Text = ds.Tables[0].Rows[0]["FormJJNo"].ToString();
            lblDCno_p.Text = ds.Tables[0].Rows[0]["DCNo"].ToString();
            lblDate_p.Text = ds.Tables[0].Rows[0]["DCDate"].ToString();
            lblSuppliername_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
            lblSupplierAddress_p.Text = ds.Tables[0].Rows[0]["SupplierAdddress"].ToString();
            lbltarrifClassification_p.Text = ds.Tables[0].Rows[0]["TariffClassification"].ToString();
            lblExpectedDurationofProcessing_p.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
            CompanyName_P.InnerText = dt2.Rows[0]["CompanyName"].ToString();

            gvWorkOrderPOItemDetails_p.DataSource = ds.Tables[1];
            gvWorkOrderPOItemDetails_p.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "PrintVendorDC();", true);
            // GeneratePDDF();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}