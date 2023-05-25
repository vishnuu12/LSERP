using JqDatatablesWebForm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPItemJobCardStatusReport : System.Web.UI.Page
{
    #region "Declaration"

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
                HttpContext.Current.Session["RFPItemStatusCard"] = null;
                HttpContext.Current.Session["RFPDID"] = Request.QueryString["RFPDID"].ToString();
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
        DataTables da = new DataTables();
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

        string Jsonstring;
        string sessionname = "RFPItemStatusCard";
        string ItemName = "";
        Jsonstring = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            // Initialization.    
            string search = HttpContext.Current.Request.Params["search[value]"];

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.    
            if (HttpContext.Current.Session["RFPItemStatusCard"] == null)
            {
                dataList = GetRFPNoList();
                ItemName = GetItemName();
                HttpContext.Current.Session["RFPItemStatusCard"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["RFPItemStatusCard"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["PartName"].ToString().ToLower().Contains(search.ToLower())
                          || p["JobCardNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["ProcessName"].ToString().ToLower().Contains(search.ToLower())
                          || p["PARTQTY"].ToString().ToLower().Contains(search.ToLower())
                          || p["QCStatus"].ToString().ToLower().Contains(search.ToLower())
                          || p["Jobstatus"].ToString().ToLower().Contains(search.ToLower())
                          || p["JobCardDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["JobCardRaisedBy"].ToString().ToLower().Contains(search.ToLower())
                          || p["CategoryName"].ToString().ToLower().Contains(search.ToLower())
                          || p["GradeNameProduction"].ToString().ToLower().Contains(search.ToLower())
                          || p["JobOrderRemarks"].ToString().ToLower().Contains(search.ToLower())
                          || p["JobCardQCRequestBy"].ToString().ToLower().Contains(search.ToLower())
                          || p["JobCardQCRequestOn"].ToString().ToLower().Contains(search.ToLower())
                          || p["QCDoneOn"].ToString().ToLower().Contains(search.ToLower())
                          || p["QCDoneBy"].ToString().ToLower().Contains(search.ToLower())

                          ).ToList();
            }

            int recFilter = dataList.Count;

            dataList = dataList.Skip(startRec).Take(pageSize).ToList();

            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.ItemName = ItemName;
            da.Ldata = dataList;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        return da;
    }

    private static List<Dictionary<string, object>> GetRFPNoList()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objR.RFPDID = Convert.ToInt32(HttpContext.Current.Session["RFPDID"]);
            ds = objR.GetRFPItemJobCardStatusReport();

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
        return rows;
    }

    private static string GetItemName()
    {
        cReports objRpt = new cReports();
        DataSet ds = new DataSet();
        string ItemName = "";
        try
        {
            objRpt.RFPDID = Convert.ToInt32(HttpContext.Current.Session["RFPDID"]);
            ds = objRpt.GetItemNameByRFPDID();

            ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ItemName;
    }

    #endregion

}