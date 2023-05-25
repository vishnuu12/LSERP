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

public partial class Pages_WorkOrderIndentStatusReport : System.Web.UI.Page
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
                HttpContext.Current.Session["DesignSummaryReport"] = null;
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
        string sessionname = "WOIStatusReport";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            // Initialization.    
            string search = HttpContext.Current.Request.Params["search[value]"];

            //string column1 = HttpContext.Current.Request.Params["columns[0][search][value]"];
            //string column2 = HttpContext.Current.Request.Params["columns[1][search][value]"];
            //string column3 = HttpContext.Current.Request.Params["columns[10][search][value]"];
            //string column4 = HttpContext.Current.Request.Params["columns[3][search][value]"];

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.    

            if (HttpContext.Current.Session["WOIStatusReport"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["WOIStatusReport"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["WOIStatusReport"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                             p["RFPNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["INDNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["INDDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["IndentBy"].ToString().ToLower().Contains(search.ToLower())
                          || p["INDQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["ItemName"].ToString().ToLower().Contains(search.ToLower())
                          || p["PartName"].ToString().ToLower().Contains(search.ToLower())
                          || p["WONo"].ToString().ToLower().Contains(search.ToLower())
                          || p["WODate"].ToString().ToLower().Contains(search.ToLower())
                          || p["POQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["DeliveryDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["DCNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["DCDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["IssuedQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["VendorName"].ToString().ToLower().Contains(search.ToLower())
                          || p["InwardQty"].ToString().ToLower().Contains(search.ToLower())
                          || p["WOINoSuffix"].ToString().ToLower().Contains(search.ToLower())
                          || p["QCStatus"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
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

    private static List<Dictionary<string, object>> LoadData()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            ds = objR.GetWorkOrderIndentSTatusReportDetails();

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

    private static List<cReports> SortByColumnWithOrder(string order, string orderDir, List<cReports> data)
    {
        // Initialization.    
        List<cReports> lst = new List<cReports>();
        try
        {
            // Sorting    
            switch (order)
            {
                case "0":
                    // Setting.    
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                                                 data.OrderByDescending(p => p.RFPNo).ToList()
                                                         : data.OrderBy(p => p.INDNo).ToList();
                    break;
                case "1":
                    // Setting.    
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(
                                                                        p => p.RFPNo).ToList()
                                                         : data.OrderBy(p => p.INDNo).ToList();
                    break;
            }
        }
        catch (Exception ex)
        {

        }
        // info.    
        return lst;
    }

    #endregion
}