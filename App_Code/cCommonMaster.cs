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
    cSession _objSession = new cSession();

    public string stage;
    public string status;
    #endregion

    #region "Properties"

    public string DepartmentName { get; set; }
    public Int32 DepartmentID { get; set; }
    public string DepartmentPrefix { get; set; }
    public Int32 HOD { get; set; }
    public string userType { get; set; }
    public string Type { get; set; }
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }

    public string PageName { get; set; }
    public Int32 PID { get; set; }
    public Int32 UserTypeID { get; set; }
    public Int32 AID { get; set; }
    public string Remark { get; set; }
    public string DisplayOrder { get; set; }
    public string UserID { get; set; }
    public Int32 Check { get; set; }
    public Int32 Count { get; set; }

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
    public int PageMID { get; set; }

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

    public DateTime DesignerDeadLineDate { get; set; }

    public int admin_Empid { get; set; }

    public string Designationname { get; set; }
    public int AttachementTypeId { get; set; }
    public int SPOID { get; set; }
    public string AttachementName { get; set; }

    public int ID { get; set; }
    public string primarykey { get; set; }
    public string table { get; set; }

    public int AttachementID { get; set; }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string confirmpassword { get; set; }
    public string newpassword { get; set; }
    public DateTime QualityAndProductionDeadLineDate { get; set; }

    public bool PQDeadLineDate { get; set; }

    public string WpsId { get; set; }
    public string MaterialGrade { get; set; }
    public string Thickness { get; set; }
    public string Process { get; set; }
    public string FillerGrade { get; set; }
    public string Amps { get; set; }
    public string Polarity { get; set; }
    public string Gaslevel { get; set; }
    public string WPSNumber { get; set; }

    public int DIDID { get; set; }
    public string InVoiceType { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string HSNno { get; set; }
    public string Payment { get; set; }
    public string TransportationMode { get; set; }
    public int LocationID { get; set; }
    public string TransporterName { get; set; }
    public string Freight { get; set; }
    public string LRNo { get; set; }
    public string vechileNo { get; set; }
    public string TimeOfSupply { get; set; }
    public string Placeofsupply { get; set; }
    public string Remarks { get; set; }
    public string BuyerOrderNo { get; set; }
    public string BuyerDate { get; set; }
    public string PreCarriagedBy { get; set; }
    public string PlaceOfReceipt { get; set; }
    public string PortLoading { get; set; }
    public string PortOfDischarge { get; set; }
    public string VesselFlightNo { get; set; }
    public string CountryOfOrginGoods { get; set; }
    public string CountryOfFinalDestination { get; set; }
    public string KindPackage { get; set; }
    public int CurrencyID { get; set; }
    public string BillingName { get; set; }
    public string BillingAddress { get; set; }
    public string BillingState { get; set; }
    public string BillingStateCode { get; set; }
    public string BillingGSTINNo { get; set; }
    public string BillingPONO { get; set; }
    public string Consigneename { get; set; }
    public string ConsigneeAddress { get; set; }
    public string Consigneestate { get; set; }
    public string Consigneestatecode { get; set; }
    public string ConsigneeGSTINNo { get; set; }
    public string ConsigneePONo { get; set; }

    public string InvoiceNoPrefix { get; set; }

    public string DespatchType { get; set; }

    public int RFPHID { get; set; }

    public int RFPDID { get; set; }

    public string invoiceQty { get; set; }
    public int RIIDID { get; set; }
    public int ChargesID { get; set; }
    public decimal ChargesValue { get; set; }
    public string ChargesType { get; set; }
    public string PageNameFlag { get; set; }
    public decimal Value { get; set; }
    public string EmployeeIds { get; set; }
    public int PMID { get; set; }
    public string JQMID { get; set; }
    public string InvoiceNo { get; set; }
    public string ElectronicReferenceNo { get; set; }
    public string ConsigneePANNo { get; set; }
    public string BiilingPANNo { get; set; }
    public string TargetValue { get; set; }
    public string LocationName { get; set; }
    public string EODID { get; set; }
    public string Voltage { get; set; }

    #endregion

    #region "Common Methods"

    public int saveNavigation(cCommonMaster objcommon)
    {
        DataSet dsFinance = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveNavigationDetails";
            c.Parameters.AddWithValue("@PageName", objcommon.PageName);
            c.Parameters.AddWithValue("@UserID", objcommon.UserID);
            PID = Convert.ToInt32(DAL.GetScalar(c));
        }
        catch (Exception ex)
        {

        }
        return PID;
    }
    public void SaveUserModules(cCommonMaster objA)
    {
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveUserModules";
            c.Parameters.AddWithValue("@UserTypeID", objA.UserTypeID);
            c.Parameters.AddWithValue("@MID", objA.MID);
            c.Parameters.AddWithValue("@Check", objA.Check);
            c.Parameters.AddWithValue("@DisplayOrder", objA.Count);
            DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
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
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMenuPagesbyUserType";
            c.Parameters.AddWithValue("@UserType", userType);
            c.Parameters.AddWithValue("@Designation", Designationname);
            DAL.GetDataset(c, ref ds);
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

    public DataSet getuserRoleMaster()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUserRoles";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetUserModuleWithRoles(cCommonMaster objA)
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUserModuleWithRoles";
            c.Parameters.AddWithValue("@UserTypeID", objA.UserTypeID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetPagesWithModule(cCommonMaster objA)
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPageWithModules";
            c.Parameters.AddWithValue("@MID", objA.PageMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetDesignertask(string spname)
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            //SqlConnection.ClearAllPools();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@Emplid", admin_Empid);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
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
            // c.CommandText = "LS_GetProductionEmployeeDetails"; 
            c.Parameters.AddWithValue("@HeadID", EmployeeID);
            c.Parameters.AddWithValue("@status", status);
            c.Parameters.AddWithValue("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateStaffAssignMentDetails(bool designerdeadline)
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
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            if (designerdeadline == false)
                c.Parameters.AddWithValue("@DesignerDeadLineDate", DBNull.Value);
            else
                c.Parameters.AddWithValue("@DesignerDeadLineDate", DesignerDeadLineDate);
            if (PQDeadLineDate == false)
                c.Parameters.AddWithValue("@QualityAndProductionDeadLineDate", DBNull.Value);
            else
                c.Parameters.AddWithValue("@QualityAndProductionDeadLineDate", QualityAndProductionDeadLineDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;

    }


    public DataSet SaveAttachements()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "SaveAttachements";
            if (EnquiryID == 0)
                c.Parameters.AddWithValue("@EnquiryID", DBNull.Value);
            else
                c.Parameters.AddWithValue("@EnquiryID", EnquiryID);

            c.Parameters.AddWithValue("@AttachementDescription", Description);
            c.Parameters.AddWithValue("@AttachementTypeID", AttachementTypeId);
            c.Parameters.AddWithValue("@FileName", AttachementName);
            c.Parameters.AddWithValue("@ID", ID);
            c.Parameters.AddWithValue("@PrimaryKey", primarykey);
            c.Parameters.AddWithValue("@table", table);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAttachementDetailsByPrimaryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAttachementsDetailsByPrimaryID";
            c.Parameters.Add("@ID", ID);
            c.Parameters.Add("@table", table);
            c.Parameters.Add("@PrimaryKey", primarykey);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteAttachements()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteAttachements";
            c.Parameters.AddWithValue("@AttachementID", AttachementID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet changepassword()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_Change_Password";
            c.Parameters.Add("@uid", UserName);
            c.Parameters.Add("@old", Password);
            c.Parameters.Add("@new", confirmpassword);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getuserdetails()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_getUserDetails";
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet getEmployeeLoginDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEmployeeLoginDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateUserLoginDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateUserLoginDetailsByEmployeeID";
            c.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            c.Parameters.AddWithValue("@UserTypeID", UserTypeID);
            c.Parameters.AddWithValue("@UserName ", UserName);
            c.Parameters.AddWithValue("@Password ", Password);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateStaffRFPAllocation(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@SAID", SAID);
            c.Parameters.AddWithValue("@EmployeeID", EmployeeIds);
            c.Parameters.AddWithValue("@QualityAndProductionDeadLineDate", QualityAndProductionDeadLineDate);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@DepartmentName", DepartmentName);
            c.Parameters.AddWithValue("@LocationName", LocationName);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPStaffAllocationDetails(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@status", status);
            c.Parameters.AddWithValue("@HeadID", EmployeeID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetRFPViewDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPViewDetailsByUserID";
            // c.CommandText = "LS_GetProductionEmployeeDetails"; 
            c.Parameters.AddWithValue("@UserID", EmployeeID);
            c.Parameters.AddWithValue("@Type", Type);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWPSDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWPSDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWPSDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWPSDetails";
            c.Parameters.AddWithValue("@WpsId", WpsId);
            c.Parameters.AddWithValue("@MaterialGrade", MaterialGrade);
            c.Parameters.AddWithValue("@Thickness", Thickness);
            c.Parameters.AddWithValue("@Process", Process);
            c.Parameters.AddWithValue("@FillerGrade", FillerGrade);
            c.Parameters.AddWithValue("@Amps", Amps);
            c.Parameters.AddWithValue("@Polarity", Polarity);
            c.Parameters.AddWithValue("@Gaslevel", Gaslevel);
            c.Parameters.AddWithValue("@WPSNumber", WPSNumber);
            c.Parameters.AddWithValue("@AttachementName", AttachementName);
            c.Parameters.AddWithValue("@Voltage", Voltage); 
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWpsDetailsByWPSID()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWPSDetailsByWPSID";
            c.Parameters.AddWithValue("@WPSID", WpsId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveDispatchAndInvoiceDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDispatchAndInvoiceDetails";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            c.Parameters.AddWithValue("@InvoiceNoPrefix", InvoiceNoPrefix);
            c.Parameters.AddWithValue("@InVoiceType", InVoiceType);
            c.Parameters.AddWithValue("@InvoiceDate", InvoiceDate);
            c.Parameters.AddWithValue("@HSNno", HSNno);
            c.Parameters.AddWithValue("@Payment", Payment);
            c.Parameters.AddWithValue("@TransportationMode", TransportationMode);
            c.Parameters.AddWithValue("@LocationID", LocationID);
            c.Parameters.AddWithValue("@TransporterName", TransporterName);
            c.Parameters.AddWithValue("@Freight", Freight);
            c.Parameters.AddWithValue("@LRNo", LRNo);
            c.Parameters.AddWithValue("@vechileNo", vechileNo);
            c.Parameters.AddWithValue("@TimeOfSupply", TimeOfSupply);
            c.Parameters.AddWithValue("@Placeofsupply", Placeofsupply);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@BuyerOrderNo", BuyerOrderNo);
            c.Parameters.AddWithValue("@BuyerDate", BuyerDate);
            c.Parameters.AddWithValue("@PreCarriagedBy", PreCarriagedBy);
            c.Parameters.AddWithValue("@PlaceOfReceipt", PlaceOfReceipt);
            c.Parameters.AddWithValue("@PortLoading", PortLoading);
            c.Parameters.AddWithValue("@PortOfDischarge", PortOfDischarge);
            c.Parameters.AddWithValue("@VesselFlightNo", VesselFlightNo);
            c.Parameters.AddWithValue("@CountryOfOrginGoods", CountryOfOrginGoods);
            c.Parameters.AddWithValue("@CountryOfFinalDestination", CountryOfFinalDestination);
            c.Parameters.AddWithValue("@KindPackage", KindPackage);
            c.Parameters.AddWithValue("@CurrencyID", CurrencyID);
            c.Parameters.AddWithValue("@BillingName", BillingName);
            c.Parameters.AddWithValue("@BillingAddress", BillingAddress);
            c.Parameters.AddWithValue("@BillingState", BillingState);
            c.Parameters.AddWithValue("@BillingStateCode", BillingStateCode);
            c.Parameters.AddWithValue("@BillingGSTINNo", BillingGSTINNo);
            c.Parameters.AddWithValue("@BillingPONO", BillingPONO);
            c.Parameters.AddWithValue("@Consigneename", Consigneename);
            c.Parameters.AddWithValue("@ConsigneeAddress", ConsigneeAddress);
            c.Parameters.AddWithValue("@Consigneestate", Consigneestate);
            c.Parameters.AddWithValue("@Consigneestatecode", Consigneestatecode);
            c.Parameters.AddWithValue("@ConsigneeGSTINNo", ConsigneeGSTINNo);
            c.Parameters.AddWithValue("@ConsigneePONo", ConsigneePONo);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@DespatchType", DespatchType);
            c.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);

            c.Parameters.AddWithValue("@ElectronicReferenceNo", ElectronicReferenceNo);
            c.Parameters.AddWithValue("@ConsigneePANNo", ConsigneePANNo);
            c.Parameters.AddWithValue("@BiilingPANNo", BiilingPANNo);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetDespatchAndInvoiceDetailsByDIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDespatchAndInvoiceDetailsByDIDID";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDespatchAndInvoiceDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDespatchAndInvoiceDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceItemQtyDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceItemQtyDetailsByRFPDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveRFPInvoiceItemDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRFPInvoiceItemDetails";
            c.Parameters.AddWithValue("@RIIDID", RIIDID);
            c.Parameters.AddWithValue("@DIDID", DIDID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@invoiceQty", invoiceQty);
            c.Parameters.AddWithValue("@Description", Description);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteDispatchAndInvoiceItemDetailsByRIIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteDispatchAndInvoiceItemDetailsByRIIDID";
            c.Parameters.AddWithValue("@RIIDID", RIIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceItemDetailsByDIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceItemDetailsByDIDID";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceTaxAndOtherChargesDetails(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@DIDID", DIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveInvoiceTaxAndOtherChargesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveInvoiceChargesDetails";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            c.Parameters.AddWithValue("@ChargesID", ChargesID);
            c.Parameters.AddWithValue("@ChargesValue", ChargesValue);
            c.Parameters.AddWithValue("@ChargesType", ChargesType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteInvoiceChargesDetailsByChargesID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteInvoiceChargesDetailsByChargesID";
            c.Parameters.AddWithValue("@ChargesID", ChargesID);
            c.Parameters.AddWithValue("@PageNameFlag", PageNameFlag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoicePaymentDetailsByDIDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoicePaymentDetailsByDIDID";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveInvoicePaymentDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveInvoicePaymentDetails";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            c.Parameters.AddWithValue("@Value", Value);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteInvoiceByDIDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteInvoiceByDIDID";
            c.Parameters.AddWithValue("@DIDID", DIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoicePDFDetailsByDIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@DIDID", DIDID);
            c.CommandText = "LS_GetInvoicePDFDetailsByDIDID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardProcessNameQCStageDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@JQMID", JQMID);
            c.Parameters.Add("@PMID", PMID);
            c.Parameters.Add("@stage", stage);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.CommandText = "LS_SaveJobCardProcessQCStageMaster";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardProcessQCStageMaster()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@PMID", PMID);
            c.CommandText = "LS_GetJobCardProcessQCStageMasterByPMID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteJobCardProcessQCStageDetailsByJQMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@JQMID", JQMID);
            c.CommandText = "LS_DeleteJobCardProcessQCStageDetailsByJQMID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignStaffAssignmentEnquiryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetEmployeeListByDepartmetHeadWise";
            c.CommandText = "LS_GetDesignStaffAssignmentEnquiryDetails";
            // c.CommandText = "LS_GetProductionEmployeeDetails"; 
            c.Parameters.AddWithValue("@HeadID", EmployeeID);
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateStaffAllocationDesign(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@SAID", SAID);
            c.Parameters.AddWithValue("@EmployeeIds", EmployeeIds);
            c.Parameters.AddWithValue("@DesignerDeadLineDate", DesignerDeadLineDate);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@EnquiryID", EnquiryID);
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

    public DataSet GetCurrencyName(DropDownList ddlCurrency)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetCurrencyDetails", ref ds);
            ddlCurrency.DataSource = ds.Tables[0];
            ddlCurrency.DataTextField = "CurrencyName";
            ddlCurrency.DataValueField = "CID";
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetReadyToDispatchRFPNODetails(DropDownList ddlRFPNo, DropDownList ddlcustomername)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetReadyToDispatchRFPNODetails";
            DAL.GetDataset(c, ref ds);

            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));

            //ProspectID	ProspectName
            ddlcustomername.DataSource = ds.Tables[1];
            ddlcustomername.DataTextField = "ProspectName";
            ddlcustomername.DataValueField = "ProspectID";
            ddlcustomername.DataBind();
            ddlcustomername.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceItemDetailsByRFPHID(DropDownList ddlitemname)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceItemDetailsByRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);

            ddlitemname.DataSource = ds.Tables[0];
            ddlitemname.DataTextField = "ItemName";
            ddlitemname.DataValueField = "RFPDID";
            ddlitemname.DataBind();
            ddlitemname.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceNoDetailsByRFPHID(DropDownList ddlInvoiceNo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceNoDetailsByRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);

            ddlInvoiceNo.DataSource = ds.Tables[0];
            ddlInvoiceNo.DataTextField = "InvoiceNo";
            ddlInvoiceNo.DataValueField = "DIDID";
            ddlInvoiceNo.DataBind();
            ddlInvoiceNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobProcessNameMaster(DropDownList ddlProcessName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProcessNameMaster";
            DAL.GetDataset(c, ref ds);

            ddlProcessName.DataSource = ds.Tables[0];
            ddlProcessName.DataTextField = "ProcessName";
            ddlProcessName.DataValueField = "PMID";
            ddlProcessName.DataBind();
            ddlProcessName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoDetailsAndOfferNoDetailsByRFPDocumentArchive(DropDownList ddlRFPNo, DropDownList ddlOfferNo, DropDownList ddldepartment)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.AddWithValue("@EnquiryID", EnquiryID);
            c.CommandText = "LS_GetRFPNoAndOfferNoDetails";
            DAL.GetDataset(c, ref ds);

            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlOfferNo.DataSource = ds.Tables[1];
            ddlOfferNo.DataTextField = "OfferNo";
            ddlOfferNo.DataValueField = "EODID";
            ddlOfferNo.DataBind();
            ddlOfferNo.Items.Insert(0, new ListItem("--Select--", "0"));

            ddldepartment.DataSource = ds.Tables[2];
            ddldepartment.DataTextField = "DepartmentName";
            ddldepartment.DataValueField = "DepartmentId";
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteSupplierPOBySPOID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSupplierPOBySPOID";
            c.Parameters.AddWithValue("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryNoAndCustomerNameByEODID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryNoAndCustomerNameByEODID";
            c.Parameters.AddWithValue("@EODID", EODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryNoAndOfferNoByRFPHID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryNoAndOfferNoByRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public void GetEmployeeLocation(DropDownList ddlEmployeeLocation)
    {
        try
        {
            DAL.GetDataset("LS_GetEmployeeLocation", ref ds);
            ddlEmployeeLocation.DataSource = ds.Tables[0];
            ddlEmployeeLocation.DataTextField = "Location";
            ddlEmployeeLocation.DataValueField = "LocationID";
            ddlEmployeeLocation.DataBind();
            ddlEmployeeLocation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}

