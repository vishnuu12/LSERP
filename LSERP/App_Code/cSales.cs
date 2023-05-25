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


public class cSales
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

    public string AnnexureName { get; set; }
    public int AMID { get; set; }

    public string HeaderName { get; set; }
    public int SOWID { get; set; }
    public int TACID { get; set; }
    public int SOWPID { get; set; }
    public int TACPID { get; set; }
    public string Points { get; set; }

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

    public int EnquiryID { get; set; }
    public string EnquiryNumber { get; set; }
    public Int64 LseNumber { get; set; }
    public int ProspectID { get; set; }
    public string ContactPerson { get; set; }
    public string EmailId { get; set; }
    public Int64 ContactNumber { get; set; }
    public string ProjectDescription { get; set; }
    public int CommercialOffer { get; set; }
    public int EnquiryTypeID { get; set; }
    public Decimal EMDAmount { get; set; }
    public DateTime ReceivedDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public int EnquiryLocation { get; set; }
    public string BudgetaryEnquiry { get; set; }
    public int UserID { get; set; }

    public int AttachementID { get; set; }
    public string AttachementName { get; set; }
    public int AttachementTypeName { get; set; }
    public string AttachementDescription { get; set; }
    public int Source { get; set; }
    public int SalesResource { get; set; }
    public int RfpGroupID { get; set; }


    //Enquiry Review Check List Model

    public int ClarifyID { get; set; }
    public int CustomerID { get; set; }
    public string DaedlineDate { get; set; }
    public string Material { get; set; }
    public string MaterialRemarks { get; set; }
    public string Pressure { get; set; }
    public string PressureRemarks { get; set; }
    public string Temprature { get; set; }
    public string TempratureRemarks { get; set; }
    public string OverAllLength { get; set; }
    public string OverAllLengthRemarks { get; set; }
    public string Movements { get; set; }
    public string MovementsRemarks { get; set; }
    public string FlowMedium { get; set; }
    public string FlowMediumRemarks { get; set; }
    public string Application { get; set; }
    public string ApplicationRemarks { get; set; }
    public string PipingLayout { get; set; }
    public string PipingLayoutRemarks { get; set; }
    public string Painting { get; set; }
    public string PaintingRemarks { get; set; }
    public string Statuary { get; set; }
    public string StatuaryRemarks { get; set; }
    public string InspectionAndTesting { get; set; }
    public string InspectionAndTestingRemarks { get; set; }
    public string Clarrification { get; set; }
    public string ClarrificationRemarks { get; set; }
    public string Stamping { get; set; }
    public string StampingRemarks { get; set; }
    public string AddtionalNote { get; set; }
    public string Connection { get; set; }
    public string ConnectionRemarks { get; set; }


    public string completionStatus { get; set; }
    public int sender { get; set; }
    public string ReceiverGroup { get; set; }
    public int Receiver { get; set; }
    public string ReplyRequired { get; set; }
    public string Header { get; set; }
    public string Message { get; set; }
    public int replyStatus { get; set; }
    public int ECID { get; set; }
    public string AlertType { get; set; }
    public int MessageID { get; set; }

    public char ReceiverType { get; set; }

    #endregion

    #region "Common Methods"

    #endregion

    #region "Masters"


    public DataSet saveEnquiryType()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEnquiryTypeDetails";
            c.Parameters.AddWithValue("@EnquiryTypeId", EnquiryTypeId);
            c.Parameters.AddWithValue("@EnquiryTypeName", EnquiryTypeName.ToUpper());
            c.Parameters.AddWithValue("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryType()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetEnquiryTypeDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deleteEnquiryType(int EnquiryTypeId)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteEnquiryType";
            c.Parameters.AddWithValue("@EnquiryTypeId", EnquiryTypeId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet saveCustomerType()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCustomerTypeDetails";
            c.Parameters.AddWithValue("@CustomerTypeId", CustomerTypeId);
            c.Parameters.AddWithValue("@CustomerTypeName", CustomerTypeName.ToUpper());
            c.Parameters.AddWithValue("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerType()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetCustomerTypeDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deleteCustomerType(int CustomerTypeId)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCustomerType";
            c.Parameters.AddWithValue("@CustomerTypeId", CustomerTypeId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getAnnexure()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetAnnexure", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveAnnexure()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveAnnexure";
            c.Parameters.AddWithValue("@AMID", AMID);
            c.Parameters.AddWithValue("@AnnexureName", AnnexureName);
            DAL.GetDataset(c, ref ds);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteAnnexure(Int32 AMID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteAnnexure";
            c.Parameters.AddWithValue("@AMID", AMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getSOW()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetSOW", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveSOW()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSOW";
            c.Parameters.AddWithValue("@SOWID", SOWID);
            c.Parameters.AddWithValue("@HeaderName", HeaderName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteSOW(Int32 SOWID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSOW";
            c.Parameters.AddWithValue("@SOWID", SOWID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getTC()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetTC", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveTC()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveTC";
            c.Parameters.AddWithValue("@TACID", TACID);
            c.Parameters.AddWithValue("@HeaderName", HeaderName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteTC(Int32 TACID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteTC";
            c.Parameters.AddWithValue("@TACID", TACID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getSOWPoints()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetSOWPoints", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveSOWPoints()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSOWPoints";
            c.Parameters.AddWithValue("@SOWPID", SOWPID);
            c.Parameters.AddWithValue("@SOWID", SOWID);
            c.Parameters.AddWithValue("@Point", Points);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteSOWPoints(Int32 SOWPID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSOWPoints";
            c.Parameters.AddWithValue("@SOWPID", SOWPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getTCPoints()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetTCPoints", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveTCPoints()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveTCPoints";
            c.Parameters.AddWithValue("@TACPID", TACPID);
            c.Parameters.AddWithValue("@TACID", TACID);
            c.Parameters.AddWithValue("@Point", Points);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteTCPoints(Int32 TACPID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteTCPoints";
            c.Parameters.AddWithValue("@TACPID", TACPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getEnquiryProcessDetails()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            DAL.GetDataset("LS_GetCustomerEnquiryHeaderDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveCustomerEnquiryDetails()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();

            SqlCommand c = new SqlCommand();

            c.CommandType = CommandType.StoredProcedure;

            c.CommandText = "LS_SaveCustomerEnquiryDetails";

            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@EnquiryTypeID", EnquiryTypeId);
            c.Parameters.Add("@CustomerEnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@LseNumber", LseNumber);
            c.Parameters.Add("@ProspectID", ProspectID);
            c.Parameters.Add("@ContactPerson", ContactPerson);
            c.Parameters.Add("@ContactNumber", ContactNumber);
            c.Parameters.Add("@Email", EmailId);
            c.Parameters.Add("@ProjectDescription", ProjectDescription);
            c.Parameters.Add("@EMDAmount", EMDAmount);
            c.Parameters.Add("@ReceivedDate", ReceivedDate);
            c.Parameters.Add("@ClosingDate", ClosingDate);
            c.Parameters.Add("@CommercialOffer", CommercialOffer);
            c.Parameters.Add("@EnquiryLocation", EnquiryLocation);
            c.Parameters.Add("@Budgetary", BudgetaryEnquiry);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@AttachmentName", AttachementName);
            c.Parameters.Add("@AttachementTypeName", AttachementTypeName);
            c.Parameters.Add("@AttachementDescription", AttachementDescription);
            c.Parameters.Add("@Source", Source);
            c.Parameters.Add("@SalesResource", SalesResource);
            c.Parameters.Add("@RfpGroupID", RfpGroupID);

            DAL.GetDataset(c, ref ds);

            //SqlConnection con = new SqlConnection();

            //con.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}


            //c.CommandText = "LS_SaveCustomerEnquiryDetails";
            //c.CommandType = CommandType.StoredProcedure;
            //c.Connection = con;

            //SqlDataAdapter da = new SqlDataAdapter();

            //da = new SqlDataAdapter(c);
            //da.Fill(ds);

            //if (con.State == ConnectionState.Open)
            //{
            //    con.Close();
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet DeleteCustomerEnquiry(Int32 EnquiryID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCustomerEnquiry";
            c.Parameters.AddWithValue("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaximumEnquiryID()
    {
        DataSet s = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaxEnquiryID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string GetMaximumAttachementID()
    {
        string MaxAttachementID = "";
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaxAttachementID";
            MaxAttachementID = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return MaxAttachementID;
    }

    public DataSet saveEnquiryAttachements()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEnquiryAttachements";
            c.Parameters.Add("@AttachementID", AttachementID);
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@AttachementTypeID", AttachementTypeName);
            c.Parameters.Add("@Filename", AttachementName);
            c.Parameters.Add("@AttachementDescription", Description);
            DAL.GetDataset(c, ref ds);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetAttachementsDetails(string EnquiryID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.CommandText = "LS_GetAttachementsDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string DeleteAttachement()
    {
        string Attachemnent = "";
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteAttachements";
            c.Parameters.AddWithValue("@AttachementID", AttachementID);
            Attachemnent = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return Attachemnent;
    }

    public DataSet GetCustomerEnquiryOrderDetailsByProspectID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerEnquiryOrderDetailsByProspectID";
            c.Parameters.Add("@ProspectID", ProspectID);
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetCustomerOrderDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerEnquiryOrderDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveEnquiryReviewClarrification()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEnquiryReviewClarrificationDetails";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@ClarifyID", ClarifyID);
            c.Parameters.Add("@CustomerID", CustomerID);
            c.Parameters.Add("@DaedlineDate", DaedlineDate);
            c.Parameters.Add("@Material", Material);
            c.Parameters.Add("@MaterialRemarks", MaterialRemarks);
            c.Parameters.Add("@Pressure", Pressure);
            c.Parameters.Add("@PressureRemarks", PressureRemarks);
            c.Parameters.Add("@Temprature", Temprature);
            c.Parameters.Add("@TempratureRemarks", TempratureRemarks);

            c.Parameters.Add("@Connection", Connection);
            c.Parameters.Add("@ConnectionRemarks", ConnectionRemarks);

            c.Parameters.Add("@OverAllLength", OverAllLength);
            c.Parameters.Add("@OverAllLengthRemarks", OverAllLengthRemarks);
            c.Parameters.Add("@Movements", Movements);
            c.Parameters.Add("@MovementsRemarks", MovementsRemarks);
            c.Parameters.Add("@FlowMedium", FlowMedium);
            c.Parameters.Add("@FlowMediumRemarks", FlowMediumRemarks);
            c.Parameters.Add("@Application", Application);
            c.Parameters.Add("@ApplicationRemarks", ApplicationRemarks);
            c.Parameters.Add("@PipingLayout", PipingLayout);
            c.Parameters.Add("@PipingLayoutRemarks", PipingLayoutRemarks);
            c.Parameters.Add("@Painting", Painting);
            c.Parameters.Add("@PaintingRemarks", PaintingRemarks);
            c.Parameters.Add("@Statuary", Statuary);
            c.Parameters.Add("@StatuaryRemarks", StatuaryRemarks);
            c.Parameters.Add("@InspectionAndTesting", InspectionAndTesting);
            c.Parameters.Add("@InspectionAndTestingRemarks", InspectionAndTestingRemarks);
            c.Parameters.Add("@Clarrification", Clarrification);
            c.Parameters.Add("@ClarrificationRemarks", ClarrificationRemarks);
            c.Parameters.Add("@Stamping", Stamping);
            c.Parameters.Add("@StampingRemarks", StampingRemarks);
            c.Parameters.Add("@AddtionalNote", AddtionalNote);
            c.Parameters.Add("@CreatedBy", UserID);

            c.Parameters.Add("@CompletionStatus", completionStatus);

            DAL.GetDataset(c, ref ds);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetEnquiryReviewCheckListDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryReviewCheckListDetails";
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void saveEnquiryCommunication()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEnquiryCommunication";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@sender", sender);
            c.Parameters.Add("@ReceiverGroup", ReceiverType);
            c.Parameters.Add("@Receiver", Receiver);
            c.Parameters.Add("@Header", Header);
            c.Parameters.Add("@Message", Message);
            c.Parameters.Add("@ReplyRequired", ReplyRequired);
            c.Parameters.Add("@AlertType", AlertType);         
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    public DataSet GetEnquiryCommunicationMessage()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryCommunicationMessage";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetEnquiryDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveEnquiryCommunicationReplyMessage()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEnquiryCommunicationReplyMessage";
            c.Parameters.Add("@ECID", ECID);
            c.Parameters.Add("@Header", Header);
            c.Parameters.Add("@Message", Message);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    //public DataSet GetEnquiryCommunicationDetailsByEnquiryNumber()
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        DAL = new cDataAccess();
    //        c = new SqlCommand();
    //        c.CommandType = CommandType.StoredProcedure;
    //        c.CommandText = "LS_GetEnquiryCommunicationDetailsByEnquiryNumber";
    //        c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
    //        c.Parameters.Add("@UserID", UserID); 
    //        DAL.GetDataset(c, ref ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //    return ds;
    //}

    #endregion


    #region "Dropdownlists"

    public void GetSOWList(DropDownList ddlSOW)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetSOWList", ref ds);
            ddlSOW.DataSource = ds.Tables[0];
            ddlSOW.DataTextField = "HeaderName";
            ddlSOW.DataValueField = "SOWID";
            ddlSOW.DataBind();
            ddlSOW.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetTACList(DropDownList ddlName)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetTACList", ref ds);
            ddlName.DataSource = ds.Tables[0];
            ddlName.DataTextField = "HeaderName";
            ddlName.DataValueField = "TACID";
            ddlName.DataBind();
            ddlName.Items.Insert(0, new ListItem("--Select--", "0"));
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
            DAL.GetDataset("LS_GetProspectDetailsForEnquiryProcess", ref ds);
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

    public void GetEnquiryTypeName(DropDownList ddlEnquiryType)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetEnquiryTypeName", ref ds);
            ddlEnquiryType.DataSource = ds.Tables[0];
            ddlEnquiryType.DataTextField = "TypeName";
            ddlEnquiryType.DataValueField = "EnquiryTypeID";
            ddlEnquiryType.DataBind();
            ddlEnquiryType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetSalesAndMarketingEmployee(DropDownList ddlSalesResource)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetSalesAndMarketingDepartmentEmployee", ref ds);
            ddlSalesResource.DataSource = ds.Tables[0];
            ddlSalesResource.DataTextField = "EmployeeName";
            ddlSalesResource.DataValueField = "EmployeeID";
            ddlSalesResource.DataBind();
            ddlSalesResource.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //public void GetEnquiryIDWithReceivedDate(DropDownList ddlEnquiryID, string EmployeeID)
    //{
    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        DAL = new cDataAccess();
    //        SqlCommand c = new SqlCommand();
    //        c.CommandType = CommandType.StoredProcedure;
    //        c.CommandText = "LS_GetEnquiryIDWithReceivedDateFromEnquiryHeader";
    //        c.Parameters.Add("@EmployeeID", EmployeeID);

    //        DAL.GetDataset(c, ref ds);
    //        ddlEnquiryID.DataSource = ds.Tables[0];
    //        ddlEnquiryID.DataTextField = "EnquiryNumber";
    //        ddlEnquiryID.DataValueField = "EnquiryID";
    //        ddlEnquiryID.DataBind();
    //        ddlEnquiryID.Items.Insert(0, new ListItem("--Select--", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    public DataSet GetEmployeeListForEnquiryCommunication(int EnquiryID,DropDownList ddlReceiverGroup)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_EmployeeListByEnquiryID";
            c.Parameters.AddWithValue("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
            ddlReceiverGroup.DataSource = ds.Tables[0];
            ddlReceiverGroup.DataTextField = "EmployeeName";
            ddlReceiverGroup.DataValueField = "EmployeeID";
            ddlReceiverGroup.DataBind();
            ddlReceiverGroup.Items.Insert(0, new ListItem("All", "All"));
            ddlReceiverGroup.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetEnquiryCustOrderNumber(DropDownList ddlEnquiryCustOrderNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetEnquiryCustomerOrderNumber", ref ds);
            ddlEnquiryCustOrderNumber.DataSource = ds.Tables[0];
            ddlEnquiryCustOrderNumber.DataTextField = "EnquiryNumber";
            ddlEnquiryCustOrderNumber.DataValueField = "EnquiryNumber";
            ddlEnquiryCustOrderNumber.DataBind();
            ddlEnquiryCustOrderNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

}

