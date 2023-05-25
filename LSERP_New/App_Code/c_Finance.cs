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


public class c_Finance
{

    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();

    #endregion

    #region "Properties"

    public string DepartmentName { get; set; }
    public Int32 DepartmentID { get; set; }
    public string DepartmentPrefix { get; set; }
    public Int32 HOD { get; set; }
    public string userType { get; set; }
    public string fromDate { get; set; }
    public string toDate { get; set; }
    public string Description { get; set; }

    public int TaxId { get; set; }
    public string TaxName { get; set; }

    public int CustomerID { get; set; }
    public string customerName { get; set; }
    public string address { get; set; }
    public string contactName { get; set; }
    public Int64 phone { get; set; }
    public string email { get; set; }
    public string CustomerPANNo { get; set; }
    public string CustomerTANNo { get; set; }
    public string CustomerGSTNo { get; set; }
    public int Country { get; set; }
    public int state { get; set; }
    public string bankname { get; set; }
    public string bankAccount { get; set; }
    public string bank_IFSCDetails { get; set; }
    public string openingBalance { get; set; }
    public string returnValue { get; set; }
    public int CustomerTypeID { get; set; }
    public int CreatedBy { get; set; }
    public int CityID { get; set; }

    public string ProsPectName { get; set; }
    public int ProsPectID { get; set; }
    public int Source { get; set; }
    public int Region { get; set; }
    public string FaxNo { get; set; }
    public string StateName { get; set; }
    public string CityName { get; set; }

    #endregion

    #region "Common Methods"


    #endregion

    #region "Masters"


    public DataSet saveTaxMaster()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveTaxNameDetails";
            c.Parameters.AddWithValue("@TaxId", TaxId);
            c.Parameters.AddWithValue("@TaxName", TaxName.ToUpper());
            c.Parameters.AddWithValue("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetTaxMasterDetails()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetTaxNameDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deleteTaxMaster(int TaxId)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteTaxNameDetails";
            c.Parameters.AddWithValue("@TaxId", TaxId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getCustomers()
    {
        DataSet dsFinance = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetCustomer", ref dsFinance);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsFinance;
    }

    public DataSet saveCustomer()
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCustomer";
            c.Parameters.Add("@CustomerName", customerName.ToUpper());
            c.Parameters.Add("@CustomerID", CustomerID);

            c.Parameters.Add("@CityID", CityID);
            c.Parameters.Add("@ContactPerson", contactName.ToUpper());

            c.Parameters.Add("@CustomerTypeID", CustomerTypeID);
            c.Parameters.Add("@Address", address);
            c.Parameters.Add("@Phone", phone);
            c.Parameters.Add("@Email", email);
            c.Parameters.Add("@CustomerPANNo", CustomerPANNo.ToUpper());
            c.Parameters.Add("@CustomerTANNo", CustomerTANNo.ToUpper());
            c.Parameters.Add("@CustomerGSTNo", CustomerGSTNo.ToUpper());
            c.Parameters.Add("@Country", Country);
            c.Parameters.Add("@State", state);
            c.Parameters.Add("@BankName", bankname.ToUpper());
            c.Parameters.Add("@BankAccountNo", bankAccount);
            c.Parameters.Add("@BankIFSCDetails", bank_IFSCDetails.ToUpper());
            c.Parameters.Add("@OpeningBalance", openingBalance);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            c.Parameters.Add("@Region", Region);
            c.Parameters.Add("@ProsPectID", ProsPectID);
            c.Parameters.Add("@FaxNo", FaxNo);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet DeleteCustomer(int CustomerID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCustomer";
            c.Parameters.AddWithValue("@CustomerID", CustomerID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string saveProsPect()
    {
        string ReturnValue = "";
        try
        {

            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveProsPect";
            c.Parameters.Add("@ProsPectName", ProsPectName.ToUpper());
            c.Parameters.Add("@ProsPectID", ProsPectID);

            c.Parameters.Add("@CityID", CityID);
            c.Parameters.Add("@ContactPerson", contactName.ToUpper());

            c.Parameters.Add("@Address", address);
            c.Parameters.Add("@Phone", phone);
            c.Parameters.Add("@Email", email);

            c.Parameters.Add("@Country", Country);
            c.Parameters.Add("@State", state);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@Source", Source);
            c.Parameters.Add("@RegionId", Region);
            c.Parameters.Add("@FaxNo", FaxNo);

            c.Parameters.Add("@StateName", StateName);
            c.Parameters.Add("@CityName", CityName);

            ReturnValue = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ReturnValue;
    }

    public DataSet GetProsPect()
    {
        DataSet dsFinance = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetProsPect", ref dsFinance);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsFinance;
    }

    public DataSet GetProspctDetailsByProspectID(int ProspectID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProspectDetailsByProspectID";
            c.Parameters.Add("@ProspectID", ProspectID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getTasks(string userid, string status)
    {
        DataSet dsFinance = new DataSet();
        try
        {

            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMyTasks";
            c.Parameters.AddWithValue("@userid", userid);
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref dsFinance);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsFinance;
    }

    #endregion

    #region "Dropdownlists"

    public void GetCustomerType(DropDownList ddlCustomerTypeID)
    {

        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetCustomerTypeList", ref ds);
            ddlCustomerTypeID.DataSource = ds.Tables[0];
            ddlCustomerTypeID.DataTextField = "TypeName";
            ddlCustomerTypeID.DataValueField = "CustomerTypeId";
            ddlCustomerTypeID.DataBind();
            ddlCustomerTypeID.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    public void GetProspectDetails(DropDownList ddlProspect)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetProspectNameCity", ref ds);
            ddlProspect.DataSource = ds.Tables[0];
            ddlProspect.DataTextField = "ProspectName";
            ddlProspect.DataValueField = "ProspectID";
            ddlProspect.DataBind();
            ddlProspect.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    public DataSet GetTaxNameDetails(DropDownList ddlTaxName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetTaxMasterDetails();

            ddlTaxName.DataSource = ds.Tables[0];
            ddlTaxName.DataTextField = "TaxName";
            ddlTaxName.DataValueField = "TaxId";
            ddlTaxName.DataBind();
            ddlTaxName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion



}

