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

public partial class Pages_PurchaseIndentMaterialInwardStatusReport : System.Web.UI.Page
{
    #region "Declarations"

    cSession _objSess = new cSession();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            HttpContext.Current.Session["PIMS"] = null;
        }
    }

    #region"Web Method"

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetData()
    {
        // Initialization.         
        DataTables da = new DataTables();
        List<Dictionary<string, object>> data;
        data = null;
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

            if (HttpContext.Current.Session["PIMS"] == null)
            {
                data = LoadData();
                HttpContext.Current.Session["PIMS"] = data;
            }
            else
                data = (List<Dictionary<string, object>>)HttpContext.Current.Session["PIMS"];

            //ds = _objCommon.GetDocumnetType();
            // Total record count.    
            int totalRecords = data.Count;
            // Verification.    

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                data = data.Where(p =>
                              p["RFPNo"].ToString().ToLower().Contains(search.ToLower())
                           || p["INDNo"].ToString().ToLower().Contains(search.ToLower())
                           || p["PartName"].ToString().ToLower().Contains(search.ToLower())
                           || p["PONo"].ToString().ToLower().Contains(search.ToLower())
                           || p["PODate"].ToString().ToLower().Contains(search.ToLower())
                           || p["POQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["PODeliveryDate"].ToString().ToLower().Contains(search.ToLower())
                           || p["SupplierName"].ToString().ToLower().Contains(search.ToLower())
                           || p["MRNNo"].ToString().ToLower().Contains(search.ToLower())
                           || p["DCQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["InwardQty"].ToString().ToLower().Contains(search.ToLower())
                           || p["SI_InwardBy"].ToString().ToLower().Contains(search.ToLower())
                           || p["SI_InwardDate"].ToString().ToLower().Contains(search.ToLower())

                           || p["QCClearedDate"].ToString().ToLower().Contains(search.ToLower())
                           || p["QCClearedBy"].ToString().ToLower().Contains(search.ToLower())
                           || p["DCNo"].ToString().ToLower().Contains(search.ToLower())

                          ).ToList();
                //||
                //p.Extension.ToString().ToLower().Contains(search.ToLower()))
            }
            // Sorting. 
            //if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderDir))
            //    data = SortByColumnWithOrder(order, orderDir, data);

            // Filter record count.    
            int recFilter = data.Count;
            // Apply pagination.    
            data = data.Skip(startRec).Take(pageSize).ToList();
            // Loading drop down lists.    
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.data = data;
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

    //private static List<Dictionary<string, object>> LoadData()
    //{
    //    // Initialization. 
    //    DataSet ds = new DataSet();
    //    cReports objR = new cReports();
    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    try
    //    {
    //        ds = objR.GetRFPSummarySheetDetails();

    //        DataTable dt = new DataTable();
    //        dt = (DataTable)ds.Tables[0];

    //        Dictionary<string, object> row = new Dictionary<string, object>();

    //        DateTimeOffset d1 = DateTimeOffset.Now;
    //        long t1 = d1.ToUnixTimeMilliseconds();
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            row = new Dictionary<string, object>();
    //            foreach (DataColumn col in dt.Columns)
    //            {
    //                row.Add(col.ColumnName, dr[col]);
    //            }
    //            rows.Add(row);
    //        }

    //        DateTimeOffset d2 = DateTimeOffset.Now;
    //        long t2 = d2.ToUnixTimeMilliseconds();

    //        long t3 = t2 - t1;
    //        //Console.WriteLine(t1);
    //        //Console.WriteLine(t2);
    //        //Console.WriteLine(t3);
    //        //Console.ReadKey();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    // info.    
    //    return rows;
    //}

    private static List<Dictionary<string, object>> LoadData()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cReports objRpt = new cReports();
        List<cReports> lst = new List<cReports>();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            ds = objRpt.GetPurchaseIndentMaterialInwardStatusReportDetails();

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