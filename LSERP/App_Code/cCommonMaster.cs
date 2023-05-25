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


public class cCommonMaster
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
    public string Type { get; set; }
    public string fromDate { get; set; }
    public string toDate { get; set; }

    public int DocumentTypeId { get; set; }
    public string DocumentType { get; set; }
    public string Description { get; set; }
    public string Extension { get; set; }

    public int EnquiryTypeId { get; set; }
    public string EnquiryTypeName { get; set; }

    public int CustomerTypeId { get; set; }
    public string CustomerTypeName { get; set; }

    public int MGID { get; set; }
    public string MateriyalGroupName { get; set; }
    public string Prefix { get; set; }

    public int THKId { get; set; }
    public decimal THKValue { get; set; }

    public int PartId { get; set; }
    public string PartName { get; set; }

    public int TaxId { get; set; }
    public string TaxName { get; set; }

    public int CMID { get; set; }
    public string ClassificationNomName { get; set; }

    public string MaterialName { get; set; }
    public string MaterialPartNumber { get; set; }
    public int MID { get; set; }

    public string EFirstName { get; set; }
    public string EMiddleName { get; set; }
    public string ELastName { get; set; }
    public DateTime EDob { get; set; }
    public DateTime EDoj { get; set; }
    public string EGender { get; set; }
    public string EInitials { get; set; }
    public string ETitle { get; set; }
    public int EmpType { get; set; }
    public string EPhoto { get; set; }
    public string CreatedBy { get; set; }
    public int Designation { get; set; }
    public int Role { get; set; }
    public int ERPRole { get; set; }
    public int EmpID { get; set; }


    public string EPerPostBoxNo { get; set; }
    public string EPerStreet { get; set; }
    public Int32 EPerCountry { get; set; }
    public Int32 EPerState { get; set; }
    public Int32 EPerCity { get; set; }
    public DateTime ValidityTill { get; set; }
    public string EEmailID { get; set; }
    public Int64 EMobileNo { get; set; }
    public Int64 EPhoneNo { get; set; }
    public Int64 EComZipCode { get; set; }
    public Int64 EPerZipCode { get; set; }
    public string EComPostBoxNo { get; set; }
    public string EComStreet { get; set; }
    public Int32 EComCity { get; set; }
    public Int32 EComState { get; set; }
    public Int32 EComCountry { get; set; }
    public Int64 PhoneNo { get; set; }
    public string ECorporateEmail { get; set; }
    public Int16 EPrimaryEmail { get; set; }
    public Int16 EPrimaryMobileNo { get; set; }
    public Int64 StateID { get; set; }
    public Int64 CountryID { get; set; }

    public int Print_Per { get; set; }
    public int Excel_Per { get; set; }
    public int PDF_Per { get; set; }
    public int UserId { get; set; }

    public int EmployeeID { get; set; }
    public string Staff { get; set; }
    public int EnquiryID { get; set; }
    public int SAID { get; set; }

    #endregion

    #region "Common Methods"

    public bool URLExists(string url)
    {
        bool result = false;

        WebRequest webRequest = WebRequest.Create(url);
        webRequest.Timeout = 1200;
        webRequest.Method = "HEAD";

        HttpWebResponse response = null;

        try
        {
            response = (HttpWebResponse)webRequest.GetResponse();
            result = true;
        }
        catch (WebException ex)
        {
            Log.Message(ex);
        }
        finally
        {
            if (response != null)
            {
                response.Close();
            }
        }
        return result;
    }

    public DataSet getMenuPages()
    {
        ds = new DataSet();
        DAL = new cDataAccess();
        try
        {
            DAL.GetDataset("LS_GetMenuPages", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet getMenuPagesByUserType()
    {
        ds = new DataSet();
        DAL = new cDataAccess();
        try
        {
            string[] paramNames = { "@UserType" };
            object[] paramValue = new object[1];
            paramValue[0] = userType;
            DAL.GetDataset("LS_GetMenuPagesbyUserType", paramValue, paramNames, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet getNotifications()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            //string[] paramNames = { "@SchoolMasterID", "@Type" };
            //object[] paramValue = new object[2];
            //paramValue[0] = SchoolMasterID;
            //paramValue[1] = Type;
            //DAL.GetDataset("Sms_GetNotificationDetails", paramValue, paramNames, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet getEvents()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            //string[] paramNames = { "@SchoolMasterID", "@SchoolAcaID", "@Type" };
            //object[] paramValue = new object[3];
            //paramValue[0] = SchoolMasterID;
            //paramValue[1] = SchoolAcaID;
            //paramValue[2] = Type;
            //DAL.GetDataset("Sms_GetEventDetail", paramValue, paramNames, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    #endregion

    #region "Masters"

    public DataSet getdashboardcolor()
    {
        DataSet dsFinance = new DataSet();
        try
        {
            DAL = new cDataAccess();
            ds = new DataSet();
            SqlCommand c = new SqlCommand();
            DAL.GetDataset("LS_GetDashboardColor", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet getUserLoginData()
    {
        ds = new DataSet();
        DAL = new cDataAccess();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUserLoginDetails";
            c.Parameters.AddWithValue("@FromDate", fromDate);
            c.Parameters.AddWithValue("@ToDate", toDate);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet saveDocumentType()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDocumentTypeDetails";
            c.Parameters.AddWithValue("@DocumentTypeId", DocumentTypeId);
            c.Parameters.AddWithValue("@DocumentTypeName", DocumentType.ToUpper());
            c.Parameters.AddWithValue("@Extension", Extension);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDocumnetType()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetDocumentTypeDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deleteDocumentType(Int32 DocumentTypeId)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteDocumentType";
            c.Parameters.AddWithValue("@DocumentTypeId", DocumentTypeId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPrintPermissionDetails()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetPrintPermisionDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet SavePrintPermissions()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePrintPermissions";
            c.Parameters.AddWithValue("@EmpID", EmpID);
            c.Parameters.AddWithValue("@Print_Per", Print_Per);
            c.Parameters.AddWithValue("@Excel_Per", Excel_Per);
            c.Parameters.AddWithValue("@PDF_Per", PDF_Per);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStaffAssignmentEnquiryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEmployeeListByDepartmetHeadWise";
            c.Parameters.AddWithValue("@HeadID", EmployeeID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateStaffAssignMentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStaffAssignmentDetails";
            c.Parameters.AddWithValue("@SAID", SAID);
            c.Parameters.AddWithValue("@EnquiryID", EnquiryID);
            c.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            c.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            DAL.GetDataset(c, ref ds);
        }
        catch(Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;

    }


    #endregion

    #region "Dropdownlists"

    public void GetDepartmentName(DropDownList ddlDepName)
    {
        try
        {
            DAL.GetDataset("LS_GetDepartments", ref ds);
            {
                ddlDepName.DataSource = ds.Tables[0];
                ddlDepName.DataTextField = "DepartmentName";
                ddlDepName.DataValueField = "DepartmentID";
                ddlDepName.DataBind();
                ddlDepName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DropDownList GetEmployeeName(DropDownList ddlEmployee)
    {
        try
        {
            DAL.GetDataset("LS_EmployeeList", ref ds);
            {
                ddlEmployee.DataSource = ds.Tables[0];
                ddlEmployee.DataTextField = "Name";
                ddlEmployee.DataValueField = "EmployeeID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return ddlEmployee;
    }

    public void GetEmploymentType(DropDownList ddlEmpType)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetEmploymentType", ref ds);
            ddlEmpType.DataSource = ds.Tables[0];
            ddlEmpType.DataTextField = "EmploymentType";
            ddlEmpType.DataValueField = "ETID";
            ddlEmpType.DataBind();
            ddlEmpType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetDesignation(DropDownList ddlDesignation)
    {
        try
        {
            DAL.GetDataset("LS_GetDesignation", ref ds);
            ddlDesignation.DataSource = ds.Tables[0];
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetRoles(DropDownList ddlGetRoles)
    {
        try
        {
            DAL.GetDataset("LS_GetRoles", ref ds);
            ddlGetRoles.DataSource = ds.Tables[0];
            ddlGetRoles.DataTextField = "UserType";
            ddlGetRoles.DataValueField = "UserTypeID";
            ddlGetRoles.DataBind();
            ddlGetRoles.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetERPRoles(DropDownList ddlERPRoles)
    {
        try
        {
            DAL.GetDataset("LS_GetERPRoles", ref ds);
            ddlERPRoles.DataSource = ds.Tables[0];
            ddlERPRoles.DataTextField = "ERPUserType";
            ddlERPRoles.DataValueField = "EUID";
            ddlERPRoles.DataBind();
            ddlERPRoles.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void getCountry(DropDownList ddlCountry)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            ds = new DataSet();

            DAL.GetDataset("LS_GetCountry", ref ds);
            ddlCountry.DataSource = ds.Tables[0];
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void getState(DropDownList ddlState)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            string[] paramNames = { "@CountryID" };
            object[] paramValue = { CountryID };

            DAL.GetDataset("LS_GetState", paramValue, paramNames, ref ds);
            ddlState.DataSource = ds.Tables[0];
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "StateID";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void getCity(DropDownList ddlCity)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            string[] paramNames = { "@StateID" };
            object[] paramValue = { StateID };

            DAL.GetDataset("LS_GetCity", paramValue, paramNames, ref ds);
            ddlCity.DataSource = ds.Tables[0];
            ddlCity.DataTextField = "CityName";
            ddlCity.DataValueField = "CityID";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void getReligion(DropDownList ddlReligionName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            string[] paramNames = { };
            object[] paramValue = { };
            DAL.GetDataset("LS_getReligion", paramValue, paramNames, ref ds);
            ddlReligionName.DataSource = ds.Tables[0];
            ddlReligionName.DataTextField = "ReligionName";
            ddlReligionName.DataValueField = "RelID";
            ddlReligionName.DataBind();
            ddlReligionName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetBloodGroup(DropDownList ddlBloodGroupName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c.CommandText = "LS_GetBloodGroup";
            DAL.GetDataset(c, ref ds);
            ddlBloodGroupName.DataSource = ds.Tables[0];
            ddlBloodGroupName.DataTextField = "BloodGroupName";
            ddlBloodGroupName.DataValueField = "BGID";
            ddlBloodGroupName.DataBind();
            ddlBloodGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void getNationality(DropDownList ddlname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetCountry", ref ds);
            ddlname.DataSource = ds.Tables[0];
            ddlname.DataTextField = "Nationality";
            ddlname.DataValueField = "CountryID";
            ddlname.DataBind();
            ddlname.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetRegion(DropDownList ddlRegion)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetRegion", ref ds);
            ddlRegion.DataSource = ds.Tables[0];
            ddlRegion.DataTextField = "Region";
            ddlRegion.DataValueField = "RegionId";
            ddlRegion.DataBind();
            ddlRegion.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet AccessPrintPermissions(LinkButton btnprint, LinkButton imgExcel, LinkButton imgPdf, string UserId)
    {
        DataSet dsFinance = new DataSet();
        try
        {
            DAL = new cDataAccess();
            ds = new DataSet();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPrintPermissionsByUserId";
            c.Parameters.AddWithValue("@UserId", UserId);
            DAL.GetDataset(c, ref ds);
            btnprint.Visible = ds.Tables[0].Rows[0]["Per_Print"].ToString() == "1" ? true : false;
            imgExcel.Visible = ds.Tables[0].Rows[0]["Per_Excel"].ToString() == "1" ? true : false;
            imgPdf.Visible = ds.Tables[0].Rows[0]["Per_PDF"].ToString() == "1" ? true : false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

}

