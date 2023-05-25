using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eplus.data;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public class cDesign
{
    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();

    #endregion

    #region"Properties"

    public int UserID { get; set; }
    public int EnquiryNumber { get; set; }
    public string AttachementName { get; set; }
    public int VersionNumber { get; set; }
    public string Remarks { get; set; }

    public int DDID { get; set; }
    public string ChangesRequested { get; set; }
    public int CommercialImpact { get; set; }
    public int StateAmount { get; set; }
    public string DelayInImplementation { get; set; }
    public DateTime ReScheduledDate { get; set; }
    public string RejectReason { get; set; }

    public string Datetime { get; set; }

    #endregion

    #region"Masters"



    #endregion

    #region"Common Methods"

    public DataSet GetDesignDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveDesignDocumentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDesignDocumentDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@VersionNumber", VersionNumber);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetDesignDetailsByEnquiryIDForApproval()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignDetailsByEnquiryIDForApproval";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateForwardToCustomerStatusInDesignDocumentDetails(int DDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSharedWithCustomerStatusInDesignDocument";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateApproveDesignStatus(int DDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateCustomerDrawingApprovalStatus";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetApprovedDesignDetailsByDDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetReopenApprovedDesignDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void UpdateCustomerApprovedDrawingVersionStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateCustomerApprovedDrawingVersionStatus";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet SaveReopenApprovedDrawingDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveReopenApprovalDrawingDetails";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@ChangesRequested", ChangesRequested);
            c.Parameters.Add("@CommercialImpact", CommercialImpact);
            c.Parameters.Add("@StateAmount", StateAmount);
            c.Parameters.Add("@DelayInImplementation", DelayInImplementation);
            if (Datetime != "")
                c.Parameters.Add("@ReScheduledDate", ReScheduledDate);
            else
                c.Parameters.Add("@ReScheduledDate", DBNull.Value);

            c.Parameters.Add("@NotApprovedReason", RejectReason);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

    #region"DropDownList"

    public DataSet GetEnquiryNumberByUserID(int UserID, DropDownList ddlEnquiryNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDByUserID";
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
            ddlEnquiryNumber.DataSource = ds.Tables[0];
            ddlEnquiryNumber.DataTextField = "EnquiryName";
            ddlEnquiryNumber.DataValueField = "EnquiryID";
            ddlEnquiryNumber.DataBind();
            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void GetEnquiryNumberBySalesUserID(int SalesUserID, DropDownList ddlEnquiryNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDBySalesUserID";
            c.Parameters.Add("@SalesEmployeeID", SalesUserID);
            DAL.GetDataset(c, ref ds);
            ddlEnquiryNumber.DataSource = ds.Tables[0];
            ddlEnquiryNumber.DataTextField = "EnquiryName";
            ddlEnquiryNumber.DataValueField = "EnquiryID";
            ddlEnquiryNumber.DataBind();
            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetCustomerApprovedDrawings(int SalesUserID, DropDownList ddlEnquiryNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerApprovedDesignDrawings";
            c.Parameters.Add("@SalesHOD", SalesUserID);
            DAL.GetDataset(c, ref ds);
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "DetailsAvailable")
            {
                ddlEnquiryNumber.DataSource = ds.Tables[1];
                ddlEnquiryNumber.DataTextField = "EnquiryName";
                ddlEnquiryNumber.DataValueField = "DDID";
                ddlEnquiryNumber.DataBind();
                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }



    #endregion
}