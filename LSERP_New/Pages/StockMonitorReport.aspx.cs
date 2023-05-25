using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using JqDatatablesWebForm.Models;

public partial class Pages_StockMonitorReport : System.Web.UI.Page
{
    #region"Declaration"

    cStores objSt;
    cSession objSession = new cSession();

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
                HttpContext.Current.Session["SMR"] = null;
                HttpContext.Current.Session["ModeName"] = null;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

    protected void rblMRN_OnSelectIndexChanged(object sender, EventArgs e)
    {
        BindStockMonitorDetails();
    }

    #endregion

    #region"Common methods"

    private void BindStockMonitorDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            objSt = new cStores();
            objSt.Mode = rblMRN.SelectedValue;
            ds = objSt.GetStockMonitorReportDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"Gridview Events"

    protected void gvStockMonitorDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string SIID = "0";

            objSt.SIID = Convert.ToInt32(SIID);

            ds = objSt.GetStockInwardCertificatesDetailsBySIID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCertificates.DataSource = ds.Tables[0];
                gvCertificates.DataBind();
            }
            else
            {
                gvCertificates.DataSource = "";
                gvCertificates.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
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
        string sessionname = "SMR";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            // Initialization.    

            string ModeName = HttpContext.Current.Request.Params["Mode"];

            string search = HttpContext.Current.Request.Params["search[value]"];

            string column0 = HttpContext.Current.Request.Params["columns[0][search][value]"];
            string column1 = HttpContext.Current.Request.Params["CategoryName"];
            string column2 = HttpContext.Current.Request.Params["MRNNo"];
            string column3 = HttpContext.Current.Request.Params["Type"];
            string column4 = HttpContext.Current.Request.Params["Grade"];
            string column5 = HttpContext.Current.Request.Params["columns[5][search][value]"];
            string column6 = HttpContext.Current.Request.Params["THK"];
            string column7 = HttpContext.Current.Request.Params["columns[7][search][value]"];
            string column8 = HttpContext.Current.Request.Params["Location"];
            string column9 = HttpContext.Current.Request.Params["columns[9][search][value]"];
            string column10 = HttpContext.Current.Request.Params["columns[10][search][value]"];
            string column11 = HttpContext.Current.Request.Params["columns[11][search][value]"];
            string column12 = HttpContext.Current.Request.Params["columns[12][search][value]"];
            string column13 = HttpContext.Current.Request.Params["columns[13][search][value]"];
            string column14 = HttpContext.Current.Request.Params["columns[14][search][value]"];
            string column15 = HttpContext.Current.Request.Params["columns[15][search][value]"];

            string[] strcol = { column0, column1, column2, column3, column4, column5, column6, column7, column8, column9, column10, column11, column12, column13, column14, column15 };

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.    

            if (HttpContext.Current.Session["sessionname"] == null || HttpContext.Current.Session["ModeName"].ToString() != ModeName)
            {
                dataList = LoadData(ModeName);
                HttpContext.Current.Session["SMR"] = dataList;
                HttpContext.Current.Session["ModeName"] = ModeName;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["SMR"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["CategoryName"].ToString().ToLower().Contains(search.ToLower())
                          || p["MRNNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["MaterialTypeName"].ToString().ToLower().Contains(search.ToLower())
                          || p["GradeName"].ToString().ToLower().Contains(search.ToLower())
                          || p["Measurment"].ToString().ToLower().Contains(search.ToLower())
                          || p["THKValue"].ToString().ToLower().Contains(search.ToLower())
                          || p["RFPNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["LocationName"].ToString().ToLower().Contains(search.ToLower())
                          || p["InwardedQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["UOM"].ToString().ToLower().Contains(search.ToLower())
                          || p["BlockedQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["ConsumedQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["ActualStock"].ToString().ToLower().Contains(search.ToLower())
                          || p["InStockQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["UnitQuoteCost"].ToString().ToLower().Contains(search.ToLower())
                          || p["Cost"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
            }

            string[] colname = { "CertificateName","CategoryName", "MRNNumber", "MaterialTypeName", "GradeName", "Measurment", "THKValue", "RFPNo", "LocationName", "InwardedQty",
            "UOM","BlockedQty","ConsumedQty","ActualStock","InStockQty","UnitQuoteCost","Cost"};

            for (int i = 0; i < strcol.Length; i++)
            {
                if (!string.IsNullOrEmpty(strcol[i]) && !string.IsNullOrWhiteSpace(strcol[i]) && strcol[i].ToString() != "select")
                {
                    dataList = dataList.Where(p =>
                             p[colname[i]].ToString().ToLower().Equals(strcol[i].ToLower())).ToList();
                }
            }

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
    }

    private static List<Dictionary<string, object>> LoadData(string Modename)
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cStores objSt = new cStores();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objSt = new cStores();
            objSt.Mode = Modename;
            ds = objSt.GetStockMonitorReportDetails();

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
            Log.Message(ex.ToString());
        }
        // info.    
        return rows;
    }

    #endregion

}