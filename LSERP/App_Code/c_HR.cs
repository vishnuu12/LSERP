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


public class c_HR
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
    public string DesignationName { get; set; }
    public Int32 DesignationID { get; set; }
    public Int32 HOD { get; set; }
    public string userType { get; set; }
    public string Type { get; set; }
    public string fromDate { get; set; }
    public string toDate { get; set; }

    public int DocumentTypeId { get; set; }
    public string DocumentType { get; set; }
    public string Description { get; set; }


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

    public Int32 EBloodGroup { get; set; }
    public Int32 EReligion { get; set; }
    public Int32 ERelMonth { get; set; }
    public Int32 ERelYear { get; set; }
    public Int32 ETotMonth { get; set; }
    public Int32 ETotYear { get; set; }
    public string EQualification { get; set; }
    public string ENationality { get; set; }
    public string EmployeeCode { get; set; }
    public string AccessCardNo { get; set; }
    public string EReportingTo { get; set; }
    public string ReferenceID { get; set; }

    public string EPANNo { get; set; }
    public string EPFNo { get; set; }
    public string EBankName { get; set; }
    public string EAccNo { get; set; }
    public string EBankIFSC { get; set; }
    public string EEmployeeESINo { get; set; }
    public Int64 EAadharNo { get; set; }
    public Int32 ESalaryPaymentMode { get; set; }
    public string EBankAcc { get; set; }


    public string ECertification { get; set; }
    public string ESubjects { get; set; }
    public string EMarks { get; set; }
    public Decimal EPercentage { get; set; }
    public string ELocation { get; set; }
    public Int32 EYOC { get; set; }
    public string EAttachments { get; set; }
    public Int32 EARID { get; set; }

    public Int64 EEID { get; set; }
    public Int64 EAutoNum { get; set; }
    public DateTime EEndYear { get; set; }
    public DateTime EStartYear { get; set; }
    public string EOrganisation { get; set; }
    public string EExpDesignation { get; set; }


    public string EPF { get; set; }
    public string EResume { get; set; }
    public string EEduPapers { get; set; }
    public string EMedicalReport { get; set; }
    public string EResignLetter { get; set; }
    public string EPAN { get; set; }
    public string EAadhar { get; set; }

    public string EAppointmentLetter { get; set; }
    public string EOther1Name { get; set; }
    public string EOther1Doc { get; set; }
    public string EOther2Name { get; set; }
    public string EOther2Doc { get; set; }
    public string EOther3Name { get; set; }
    public string EOther3Doc { get; set; }
    public string EOther4Name { get; set; }
    public string EOther4Doc { get; set; }
    public string EOther5Name { get; set; }
    public string EOther5Doc { get; set; }

    #endregion

    #region "Common Methods"

    public  DataSet GetEmployeeCommunicationDetailsEmployeeID(int EmployeeID)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEmployeeAlertByEmployeeID";
            c.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            DAL.GetDataset(c,ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

    #region "Masters"

    public DataSet getDepartment()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetDepartments", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveDepartment()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDepartment";
            c.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            c.Parameters.AddWithValue("@DepartmentName", DepartmentName);
            c.Parameters.AddWithValue("@HOD", HOD);
            c.Parameters.AddWithValue("@DepartmentPrefix", DepartmentPrefix);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteDepartment(Int32 DepartmentID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteDepartment";
            c.Parameters.AddWithValue("@DepartmentID", DepartmentID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getDesignation()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetDesignations", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveDesignation()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDesignation";
            c.Parameters.AddWithValue("@DesignationID", DesignationID);
            c.Parameters.AddWithValue("@Designation", DesignationName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteDesignation(Int32 DesignationID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteDesignation";
            c.Parameters.AddWithValue("@DesignationID", DesignationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveEmployeeExperience()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmployeeExperience";
            c.Parameters.Add("@Organisation", EOrganisation);
            c.Parameters.Add("@Designation", EExpDesignation);
            c.Parameters.Add("@StartYear", EStartYear);
            c.Parameters.Add("@EndYear", EEndYear);
            c.Parameters.Add("@AutoNum", EAutoNum);
            c.Parameters.Add("@EEID", EEID);
            c.Parameters.Add("@EmployeeID", EmpID);
            c.Parameters.Add("@TotExpmonth", ETotMonth);
            c.Parameters.Add("@TotExpyear", ETotYear);
            c.Parameters.Add("@RelExpmonth", ERelMonth);
            c.Parameters.Add("@RelExpYear", ERelYear);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveEmployeeBasicInfo()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmployeePersonalDetails";
            c.Parameters.Add("@FirstName", EFirstName);
            c.Parameters.Add("@MiddleName", EMiddleName);
            c.Parameters.Add("@LastName", ELastName);
            c.Parameters.Add("@Dob", EDob);
            c.Parameters.Add("@Doj", EDoj);
            c.Parameters.Add("@Gender", EGender);
            c.Parameters.Add("@Initials", EInitials);
            c.Parameters.Add("@Photo", EPhoto);
            c.Parameters.Add("@EmpType", EmpType);
            c.Parameters.Add("@Title", ETitle);
            c.Parameters.Add("@Department", DepartmentID);
            c.Parameters.Add("@Designation", Designation);
            c.Parameters.Add("@Roles", Role);
            c.Parameters.Add("@ERPRole", ERPRole);
            c.Parameters.Add("@EmpID", EmpID);
            c.Parameters.Add("@UserId", CreatedBy);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveEmployeeCommunication()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmployeeCommunication";
            c.Parameters.Add("@EmployeeID", EmpID);
            c.Parameters.Add("@PerPostBoxNo", EPerPostBoxNo);
            c.Parameters.Add("@PerStreet", EPerStreet);
            c.Parameters.Add("@PerState", EPerState);
            c.Parameters.Add("@PerCountry", EPerCountry);
            c.Parameters.Add("@PerCity", EPerCity);
            c.Parameters.Add("@perZipCode", EPerZipCode);
            c.Parameters.Add("@ComPostBoxNo", EComPostBoxNo);
            c.Parameters.Add("@ComStreet", EComStreet);
            c.Parameters.Add("@ComCountry", EComCountry);
            c.Parameters.Add("@ComState", EComState);
            c.Parameters.Add("@ComCity", EComCity);
            c.Parameters.Add("@ComZipCode", EComZipCode);
            c.Parameters.Add("@ComMobileNo", EMobileNo);
            c.Parameters.Add("@ComEmail", EEmailID);
            c.Parameters.Add("@ComPhoneNo", EPhoneNo);
            c.Parameters.Add("@CorporateEmail", ECorporateEmail);
            c.Parameters.Add("@PrimaryEmail", EPrimaryEmail);
            c.Parameters.Add("@PrimaryMobileNo", EPrimaryMobileNo);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveEmployeeProfileData()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmployeeProfileData";
            c.Parameters.Add("@EmployeeID", EmpID);
            c.Parameters.Add("@BloodGroup", EBloodGroup);
            c.Parameters.Add("@Qualification", EQualification);
            c.Parameters.Add("@Nationality", ENationality);
            c.Parameters.Add("@Religion", EReligion);
            c.Parameters.Add("@ReportingTo", EReportingTo);
            c.Parameters.Add("@AccessCardNo", AccessCardNo);
            c.Parameters.Add("@EmployeeCode", EmployeeCode);
            c.Parameters.Add("@ValidityTill", ValidityTill);
            c.Parameters.Add("@ReferenceID", ReferenceID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveEmployeeBankDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            //  c.CommandType = CommandType.StoredProcedure;
            //  c.CommandText = "LS_SaveEmployeeBankDetails";
            c.Parameters.Add("@PANNo", EPANNo);
            c.Parameters.Add("@PFNo", EPFNo);
            c.Parameters.Add("@BankName", EBankName);
            c.Parameters.Add("@BankAccountNo", EBankAcc);
            c.Parameters.Add("@BankIFSCCode", EBankIFSC);
            c.Parameters.Add("@EmployeeESINo", EEmployeeESINo);
            c.Parameters.Add("@AadharNo", EAadharNo);
            c.Parameters.Add("@SalaryPaymentMode", ESalaryPaymentMode);
            c.Parameters.Add("@EmployeeID", EmpID);

            // DAL.GetDataset(c, ref ds);

            SqlConnection con = new SqlConnection();

            con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }


            c.CommandText = "LS_SaveEmployeeBankDetails";
            c.CommandType = CommandType.StoredProcedure;
            c.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter();

            da = new SqlDataAdapter(c);
            da.Fill(ds);

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveEmployeeAcadRecords()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmployeeAcadRecords";
            c.Parameters.Add("@EmployeeID", EmpID);
            c.Parameters.Add("@Certification", ECertification);
            c.Parameters.Add("@Subject", ESubjects);
            c.Parameters.Add("@Marks", EMarks);
            c.Parameters.Add("@Percentage", EPercentage);
            c.Parameters.Add("@Location", ELocation);
            c.Parameters.Add("@YOC", EYOC);
            c.Parameters.Add("@Attachments", EAttachments);
            c.Parameters.Add("@UserID", CreatedBy);
            c.Parameters.Add("@EARID", EARID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void InsertEmployeeDocs()
    {
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmployeeDocs";
            c.Parameters.Add("@Appointment_Letter", EAppointmentLetter);
            c.Parameters.Add("@Empresume", EResume);
            c.Parameters.Add("@PanCard", EPAN);
            c.Parameters.Add("@Medicalreport", EMedicalReport);
            c.Parameters.Add("@ResignLetter", EResignLetter);
            c.Parameters.Add("@EduPapers", EEduPapers);
            c.Parameters.Add("@PFForm ", EPF);
            c.Parameters.Add("@Bank_Acc", EBankAcc);
            c.Parameters.Add("@Aadhar", EAadhar);
            c.Parameters.Add("@Other_1_Name", EOther1Name);
            c.Parameters.Add("@Other_1_DOC", EOther1Doc);
            c.Parameters.Add("@Other_2_Name", EOther2Name);
            c.Parameters.Add("@Other_2_DOC", EOther2Doc);
            c.Parameters.Add("@Other_3_Name", EOther3Name);
            c.Parameters.Add("@Other_3_DOC", EOther3Doc);
            c.Parameters.Add("@Other_4_Name", EOther4Name);
            c.Parameters.Add("@Other_4_DOC", EOther4Doc);
            c.Parameters.Add("@Other_5_Name", EOther5Name);
            c.Parameters.Add("@Other_5_DOC", EOther5Doc);
            c.Parameters.Add("@EmployeeID", EmpID);
            DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetStaffDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStaffDetails";
            c.Parameters.Add("@StaffID", EmpID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    #endregion

    #region "Dropdownlists"

    public void GetEmployeeList(DropDownList ddlName)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetEmployeeList", ref ds);
            ddlName.DataSource = ds.Tables[0];
            ddlName.DataTextField = "EmployeeName";
            ddlName.DataValueField = "EmployeeID";
            ddlName.DataBind();
            ddlName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetSalaryPaymentMode(DropDownList ddlSalaryPaymentMode)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c.CommandText = "LS_GetSalaryPaymentMode";
            DAL.GetDataset(c, ref ds);
            ddlSalaryPaymentMode.DataSource = ds.Tables[0];
            ddlSalaryPaymentMode.DataValueField = "SPMID";
            ddlSalaryPaymentMode.DataTextField = "SalaryPaymentMode";
            ddlSalaryPaymentMode.DataBind();
            ddlSalaryPaymentMode.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}

