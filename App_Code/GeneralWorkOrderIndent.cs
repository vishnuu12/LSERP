using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;

/// <summary>
/// Summary description for GeneralWorkOrderIndent
/// </summary>
public class GeneralWorkOrderIndent
{
    public GeneralWorkOrderIndent()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();

    #endregion

    #region "Properties"
    public string GWIId { get; set; }
    public int GWOID { get; set; }
    public int GWOIDA { get; set; }
    public int SGWOID { get; set; }
    public int EmpID { get; set; }

    public string PageNameFlag { get; set; }
    public string txtsdescription { get; set; }
    public string txtsremark { get; set; }
    public string txtquantity { get; set; }
    public string documentUpload { get; set; }
    public int ddlUOM { get; set; }
    public int UOMID { get; set; }
    public int LiJobList { get; set; }
    public int ddlIndentTo { get; set; }
    public int employeeid { get; set; }

    public string GWONO { get; set; }
    public string txtsdescription1 { get; set; }
    public string txtsremark1 { get; set; }
    public string txtquantity1 { get; set; }
    public string FileUpload1 { get; set; }
    public int LiJobList1 { get; set; }
    public int ddlUOM1 { get; set; }

    #endregion


    #region "Dropdownlists"

    public void GetUnitsDetails(DropDownList ddlCategoryName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetUnitCategoryDetails();

            ddlCategoryName.DataSource = ds.Tables[0];
            ddlCategoryName.DataTextField = "ClassificationNom";
            ddlCategoryName.DataValueField = "CMID";
            ddlCategoryName.DataBind();
            ddlCategoryName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    public DataSet GetUnitCategoryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQuantityInUnits";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public void GetJobOperationListDetails(DropDownList ddlCategoryName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetJobListDetails();

            ddlCategoryName.DataSource = ds.Tables[0];
            ddlCategoryName.DataTextField = "ProcessName";
            ddlCategoryName.DataValueField = "WOJOLID";
            ddlCategoryName.DataBind();
            ddlCategoryName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetGWIndentDetailsByGWOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGWIndentDetailsByGWOID";
            c.Parameters.AddWithValue("@GWOID", GWOID);
            c.Parameters.AddWithValue("@Flag", PageNameFlag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void GetIndentToDetails(DropDownList ddlCategoryName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetIndentToListDetails();

            ddlCategoryName.DataSource = ds.Tables[0];
            ddlCategoryName.DataTextField = "IndentTo";
            ddlCategoryName.DataValueField = "IndentId";
            ddlCategoryName.DataBind();
            ddlCategoryName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetIndentToListDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetIndentToListDetailsForGeneralWO";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetJobListDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartNameDetailsForGeneralWO";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
    #endregion

    public DataSet SaveGeneralWorkOrderIndent()
    {
        DataSet dsa = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralWorkOrderIndent";
            c.Parameters.Add("@GWOID", GWOID);
            c.Parameters.Add("@txtsdescription", txtsdescription);
            c.Parameters.Add("@txtsremark", txtsremark);
            c.Parameters.Add("@txtquantity", txtquantity);
          c.Parameters.Add("@ddlUOM", ddlUOM);
            c.Parameters.Add("@LiJobList", LiJobList);
            c.Parameters.Add("@ddlIndentTo", ddlIndentTo);
            //   c.Parameters.Add("@AttachmentID", documentUpload);
            c.Parameters.Add("@employeeid", employeeid);

              c.Parameters.AddWithValue("@AttachementID", documentUpload);
            DAL.GetDataset(c, ref dsa);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsa;
    }
    public DataSet SaveGeneralWorkOrderIndentforGWOID()
    {
        DataSet dsa = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
           c.CommandText = "LS_SaveGeneralWorkOrderIndentforGWOID";
            c.Parameters.Add("@SGWOID", SGWOID);
            c.Parameters.Add("@GWIId", GWIId);
            c.Parameters.Add("@lblGWOI", GWONO);
            c.Parameters.Add("@txtsdescription1", txtsdescription1);
            c.Parameters.Add("@txtsremark1", txtsremark1);
            c.Parameters.Add("@txtquantity1", txtquantity1);
            c.Parameters.Add("@ddlUOM1", ddlUOM1);
            c.Parameters.Add("@LiJobList1", LiJobList1);
            c.Parameters.AddWithValue("@AttachementID1", FileUpload1);
            c.Parameters.Add("@employeeid", employeeid);
            DAL.GetDataset(c, ref dsa);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsa;
    }

    public DataSet GetEditGeneralWorkOrderDetailsByGWOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEditGeneralWorkOrderDetailsByGWOID";
            c.Parameters.Add("@GWOID", GWOID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetGeneralWOIndent()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETGeneralWorkOrderIndent";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteGeneralWorkOrderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteGeneralWorkOrderDetails";
            c.Parameters.AddWithValue("@GWOID", GWOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet DeleteGeneralWorkOrderDetailsforSGWOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteGeneralWorkOrderDetailsforSGWOID";
            c.Parameters.AddWithValue("@SGWOID", SGWOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void getddlUOM(DropDownList ddlUOM)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            string[] paramNames = { "@ddlUOM" };
            object[] paramValue = { ddlUOM };

            DAL.GetDataset("LS_GetUOM", paramValue, paramNames, ref ds);
            ddlUOM.DataSource = ds.Tables[0];
            ddlUOM.DataTextField = "Name";
            ddlUOM.DataValueField = "CMID";
            ddlUOM.DataBind();
            ddlUOM.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

}