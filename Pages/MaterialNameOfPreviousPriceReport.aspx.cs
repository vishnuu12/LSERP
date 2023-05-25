using JqDatatablesWebForm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MaterialNameOfPreviousPriceReport : System.Web.UI.Page
{
    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HttpContext.Current.Session["PICW"] = null;
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
        List<Dictionary<string, object>> data;
        data = null;
        DataSet ds = new DataSet();
        try
        {
            string search = HttpContext.Current.Request.Params["search[value]"];
            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            string MGMID = HttpContext.Current.Request.Params["MGMID"];

            if (HttpContext.Current.Session["PICW"] == null || HttpContext.Current.Session["MGMID"] != MGMID)
            {
                data = GetMaterialNamePriviousPriceReportDetails(MGMID);
                HttpContext.Current.Session["PICW"] = data;
                HttpContext.Current.Session["MGMID"] = MGMID;
            }
            else
            {
                data = (List<Dictionary<string, object>>)HttpContext.Current.Session["PICW"];
                MGMID = HttpContext.Current.Session["MGMID"].ToString();
            }

            int totalRecords = data.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(p =>
                              p["SupplierName"].ToString().ToLower().Contains(search.ToLower())
                           || p["UnitCost"].ToString().ToLower().Contains(search.ToLower())
                           || p["CategoryName"].ToString().ToLower().Contains(search.ToLower())
                           || p["THKValue"].ToString().ToLower().Contains(search.ToLower())
                           || p["QuoteApprovedBy"].ToString().ToLower().Contains(search.ToLower())
                           || p["QuotationApprovedOn"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
            }

            int recFilter = data.Count;
            data = data.Skip(startRec).Take(pageSize).ToList();
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.data = data;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return da;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static string GetMaterialNameDetails()
    {
        DataSet ds = new DataSet();
        cReports objR = new cReports();
        string JsonString = "";
        try
        {
            objR = new cReports();
            ds = objR.GetMaterialNameDetails();

            DataTable dt = new DataTable();
            dt = (DataTable)ds.Tables[0];

            JsonString = dt.Rows[0]["MaterialName"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JsonString;
    }

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static List<Dictionary<string, object>> GetMaterialNamePriviousPriceReportDetails(string MGMID)
    {
        DataSet ds = new DataSet();
        cReports objRpt = new cReports();
        List<cReports> lst = new List<cReports>();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objRpt.MGMID = MGMID;
            ds = objRpt.GetMaterialnameOfPreviousPriceReportByMGMID();

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

    #endregion
}