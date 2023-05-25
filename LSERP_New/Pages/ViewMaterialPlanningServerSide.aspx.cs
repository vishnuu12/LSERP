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

public partial class Pages_ViewMaterialPlanningServerSide : System.Web.UI.Page
{
    #region"Declaration"

    cProduction objProd;
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
                HttpContext.Current.Session["viewMP"] = null;
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
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetData()
    {
        // Initialization.         
        DataTables da = new DataTables();
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

        //  Dictionary<string, object> data;
        string Jsonstring;
        string sessionname = "viewMP";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            // Initialization.    
            string search = HttpContext.Current.Request.Params["search[value]"];

            string column1 = HttpContext.Current.Request.Params["columns[0][search][value]"];
            string column2 = HttpContext.Current.Request.Params["columns[1][search][value]"];
            string column3 = HttpContext.Current.Request.Params["columns[10][search][value]"];
            string column4 = HttpContext.Current.Request.Params["columns[3][search][value]"];

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);
            //Loading.    

            if (HttpContext.Current.Session["viewMP"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["viewMP"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["viewMP"];

            //ds = _objCommon.GetDocumnetType();
            // Total record count.    
            int totalRecords = dataList.Count;
            // Verification.    

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["RFPNo"].ToString().ToLower().Contains(search.ToLower())
                          || p["BlockedWeight"].ToString().ToLower().Contains(search.ToLower())
                          || p["PartName"].ToString().ToLower().Contains(search.ToLower())
                          || p["GradeNameDesign"].ToString().ToLower().Contains(search.ToLower())
                          || p["GradeNameProduction"].ToString().ToLower().Contains(search.ToLower())
                          || p["Username"].ToString().ToLower().Contains(search.ToLower())
                          || p["BlockedDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["MRNNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["THKValue"].ToString().ToLower().Contains(search.ToLower())
                          || p["UOM"].ToString().ToLower().Contains(search.ToLower())
                          || p["ItemName"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
                //||
                //p.Extension.ToString().ToLower().Contains(search.ToLower()))
            }

            // Sorting.    
            //  data = SortByColumnWithOrder(order, orderDir, data);
            // Filter record count.    

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            int recFilter = dataList.Count;
            // Apply pagination. 


            dataList = dataList.Skip(startRec).Take(pageSize).ToList();
            // Loading drop down lists.    
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.Ldata = dataList;
        }
        catch (Exception ex)
        {
            // Info    
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
        cProduction objProd = new cProduction();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            ds = objProd.GetBlockingMRNMaterialPlanningDetails();

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