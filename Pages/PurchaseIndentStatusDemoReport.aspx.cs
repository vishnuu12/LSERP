using JqDatatablesWebForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PurchaseIndentStatusDemoReport : System.Web.UI.Page
{
    #region "Declarations"

    cSession _objSess = new cSession();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            HttpContext.Current.Session["datalspi"] = null;
        }
    }

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
        string sessionname = "pi";

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

            if (HttpContext.Current.Session["datals" + sessionname + ""] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["datals" + sessionname + ""] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["datals" + sessionname + ""];

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
                          //|| p.INDNo.ToString().ToLower().Contains(search.ToLower())
                          //|| p.PartName.ToString().ToLower().Contains(search.ToLower())
                          //|| p.PONo.ToString().ToLower().Contains(search.ToLower())
                          //|| p.PODate.ToString().ToLower().Contains(search.ToLower())
                          //|| p.POQty.ToString().ToLower().Contains(search.ToLower())
                          //|| p.PODeliveryDate.ToString().ToLower().Contains(search.ToLower())
                          //|| p.SupplierName.ToString().ToLower().Contains(search.ToLower())
                          //|| p.MRNNo.ToString().ToLower().Contains(search.ToLower())
                          //|| p.DCQty.ToString().ToLower().Contains(search.ToLower())
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
        cReports objRpt = new cReports();
        List<cReports> lst = new List<cReports>();
        List<String> Users = new List<String>();
        // Dictionary<string, string> dt = new Dictionary<string, string>();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

        try
        {
            ds = objRpt.GetPurchaseIndentMaterialInwardStatusReportDetails();

            //Users = ds.Tables[0].AsEnumerable()
            //.Select(r => r.Field<string>("RFPNo"))
            //.ToList();

            DataTable dt = new DataTable();
            dt = (DataTable)ds.Tables[0];

            Dictionary<string, object> row = new Dictionary<string, object>();

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            //lst = (from DataRow dr in ds.Tables[0].Rows
            //               select new cReports()
            //               {
            //                   SPODID = dr["SPODID"].ToString(),
            //                   RFPNo = dr["RFPNo"].ToString(),
            //                   INDNo = dr["INDNo"].ToString(),
            //                   PartName = dr["PartName"].ToString()
            //               }).ToList();

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    objRpt = new cReports();
            //    objRpt.SPODID = ds.Tables[0].Rows[i]["SPODID"].ToString();
            //    objRpt.RFPNo = ds.Tables[0].Rows[i]["RFPNo"].ToString();
            //    objRpt.INDNo = ds.Tables[0].Rows[i]["INDNo"].ToString();
            //    objRpt.PartName = ds.Tables[0].Rows[i]["PartName"].ToString();
            //    objRpt.PONo = ds.Tables[0].Rows[i]["PONo"].ToString();
            //    objRpt.PODate = ds.Tables[0].Rows[i]["PODate"].ToString();
            //    objRpt.POQty = ds.Tables[0].Rows[i]["POQty"].ToString();
            //    objRpt.PODeliveryDate = ds.Tables[0].Rows[i]["PODeliveryDate"].ToString();
            //    objRpt.SupplierName = ds.Tables[0].Rows[i]["SupplierName"].ToString();
            //    objRpt.MRNNo = ds.Tables[0].Rows[i]["MRNNo"].ToString();
            //    objRpt.DCQty = ds.Tables[0].Rows[i]["DCQty"].ToString();

            //    lst.Add(objRpt);
            //}

            //  dt=JsonConvert.SerializeObject(ds.Tables[0]);
        }
        catch (Exception ex)
        {

        }
        // info.    
        return rows;
    }


    private static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    private static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
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