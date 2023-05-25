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

public partial class Pages_CrashReport : System.Web.UI.Page
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
                HttpContext.Current.Session["CrashReport"] = null;
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
        string sessionname = "CrashReport";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            // Initialization.    
            string search = HttpContext.Current.Request.Params["search[value]"];

            string ItemName = HttpContext.Current.Request.Params["ItemName"];
            string Size = HttpContext.Current.Request.Params["Size"];
            string Temp = HttpContext.Current.Request.Params["Temp"];
            string Pressure = HttpContext.Current.Request.Params["Pressure"];
            string Application = HttpContext.Current.Request.Params["Application"];
            string TagNo = HttpContext.Current.Request.Params["TagNo"];
            string Movement = HttpContext.Current.Request.Params["Movement"];
            string Location = HttpContext.Current.Request.Params["Location"];

            string column0 = HttpContext.Current.Request.Params["columns[0][search][value]"];
            string column1 = HttpContext.Current.Request.Params["columns[1][search][value]"];
            string[] strcol = { ItemName, Size, Temp, Pressure, Application, TagNo, Movement, Location };

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.
            //CustomerEnquiryNumber,
            //ProspectName

            if (HttpContext.Current.Session["CrashReport"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["CrashReport"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["CrashReport"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["CustomerEnquiryNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["ProspectName"].ToString().ToLower().Contains(search.ToLower())
                          || p["OfferNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["PORefNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["RFPNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["ItemName"].ToString().ToLower().Contains(search.ToLower())
                          || p["Size"].ToString().ToLower().Contains(search.ToLower())
                          || p["Temperature"].ToString().ToLower().Contains(search.ToLower())
                          || p["Pressure"].ToString().ToLower().Contains(search.ToLower())
                          || p["EnquiryApplication"].ToString().ToLower().Contains(search.ToLower())
                          || p["TagNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["Movement"].ToString().ToLower().Contains(search.ToLower())
                          || p["Location"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
            }

            string[] strKey = { "ItemName", "Size", "Temperature", "Pressure", "EnquiryApplication", "TagNo", "Movement", "Location" };

            for (int i = 0; i < strcol.Length; i++)
            {
                if (!string.IsNullOrEmpty(strcol[i]) && !string.IsNullOrWhiteSpace(strcol[i]))
                {
                    dataList = dataList.Where(p =>
                             p[strKey[i]].ToString().ToLower().Contains(strcol[i].ToLower())).ToList();
                }
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            int recFilter = dataList.Count;

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
        return da;
    }

    private static List<Dictionary<string, object>> LoadData()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            ds = objR.GetEnquiryHeaderDetails();
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

    private static List<cReports> SortByColumnWithOrder(string order, string orderDir, List<cReports> data)
    {
        List<cReports> lst = new List<cReports>();
        try
        {
            switch (order)
            {
                case "0":
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ?
                                                 data.OrderByDescending(p => p.RFPNo).ToList()
                                                         : data.OrderBy(p => p.INDNo).ToList();
                    break;
                case "1":
                    lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(
                                                                        p => p.RFPNo).ToList()
                                                         : data.OrderBy(p => p.INDNo).ToList();
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return lst;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string[] GetItemStatusDetailsByEnquiryID(string EnquiryID)
    {
        cReports objR = new cReports();
        DataSet ds = new DataSet();
        DataTable dt;
        string[] JSONString = { "", "", "" };
        try
        {
            objR.EnquiryID = Convert.ToInt32(EnquiryID);
            ds = objR.GetItemDetailsbyEnquiryID();

            dt = (DataTable)ds.Tables[0];

            JSONString[0] = dt.Rows[0]["Res"].ToString();
            JSONString[1] = dt.Rows[0]["Res1"].ToString();
            JSONString[2] = dt.Rows[0]["Res2"].ToString();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetItemPartStatusDetailsByRFPDID(string RFPDID)
    {
        cReports objR = new cReports();
        string res = "";
        DataSet ds = new DataSet();
        try
        {
            objR.RFPDID = Convert.ToInt32(RFPDID);
            ds = objR.GetPartStatusDetailsByRFPDID();

            res = ds.Tables[0].Rows[0]["Res"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return res;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object ViewAllDocs(string EnquiryID)
    {
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "RFPQualityPlanning.aspx");

            string s = "window.open('" + Page + "','_blank');";
            page.ClientScript.RegisterStartupScript(page.GetType(), "script", s, true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return "";
    }

    #endregion
}