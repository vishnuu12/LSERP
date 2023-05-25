using eplus.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web;

/// <summary>
/// Summary description for DocumentType
/// </summary>
public class DocumentType
{
    #region "Declarations"

    DataSet dsa = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    public string itemCodeType;
    public int revisionNumber;
    public string pressure;
    public int SIEHID;
    public string ItemCodeValue;
    public string tabname;
    public DateTime DeadLineDate;

    #endregion

    public DocumentType()
    {

    }

    public string Type { get; set; }
    public string Extension { get; set; }
    public string EmployeeID { get; set; }

    public int SUPVENID { get; set; }
    public int SUPCHID { get; set; }
    public int SCVMID { get; set; }
    public string txtVendorname { get; set; }
    public string txtContactPerson { get; set; }
    public string txtAddress { get; set; }
    public string txtContactNo { get; set; }
    public string txtGSTNo { get; set; }
    public string txtEmailId { get; set; }
    public string fbUpload { get; set; }

    public DataSet VSupplierChainVendormaster()
    {
        DataSet dsa = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_VSupplierChainVendormaster";
            c.Parameters.Add("@SCVMID", SCVMID);
            c.Parameters.Add("@txtVendorname", txtVendorname);
            c.Parameters.Add("@txtContactPerson", txtContactPerson);
            c.Parameters.Add("@txtAddress", txtAddress);
            c.Parameters.Add("@txtContactNo", txtContactNo);
            c.Parameters.Add("@txtGSTNo", txtGSTNo);
            c.Parameters.Add("@txtEmailId", txtEmailId);
            c.Parameters.AddWithValue("@fGSTNAttach", fbUpload);
            DAL.GetDataset(c, ref dsa);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsa;
    }

    public DataSet VGETSupplierChainVendormaster()
    {
        DataSet dsa = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
           // c.CommandText = "LS_GETSupplierChainVendormaster";
            c.CommandText = "LSE_SupplierChainVendormaster";
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            DAL.GetDataset(c, ref dsa);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsa;
    }

    public DataSet GetSupllierChainDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETSupplierChainVendormaster";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupllierChainDetailsBySUPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETSupplierChainVendormasterSCVMID";
            c.Parameters.Add("@SCVMID", SCVMID);
            //  c.CommandText = "LS_GetSupplierChainDetailsBySCVMID";
            //  c.Parameters.Add("@SCVMID", SCVMID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void ShowInputSection(HtmlGenericControl divAdd, HtmlGenericControl divInput, HtmlGenericControl divOutput)
    {
        try
        {
            divAdd.Style.Add("display", "none");
            divInput.Style.Add("display", "block");
            divOutput.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet DeleteSupplierChainDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSupplierChain";
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

}