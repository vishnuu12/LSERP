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

    public string PCDID { get; set; }

    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    public string currencyExchangerate;

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
	
				
    public string GuaranteeTerms { get; set; }
    public string GuaranteeRemarks { get; set; }

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
    public Int64 AlternateContactNumber { get; set; }
    public int Drawingoffer { get; set; }
    public int notinterested { get; set; }
    public int budgetaryoffer { get; set; }
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

    //Design Po review
    public string UStamp { get; set; }
    public string UStampRemarks { get; set; }
    public string U2Stamp { get; set; }
    public string U2StampRemarks { get; set; }
    public string PED { get; set; }
    public string PEDRemarks { get; set; }
    public string IBR { get; set; }
    public string IBRRemarks { get; set; }
    public string ISO { get; set; }
    public string ISORemarks { get; set; }
    public string SpecialInformation { get; set; }
    public string SpecialInformationRemarks { get; set; }
    public string Units { get; set; }
    public string UnitsRemarks { get; set; }
    public string CheckedwithOffer { get; set; }
    public string CheckedwithOfferRemarks { get; set; }
    public string TechnicalFeasibility { get; set; }
    public string TechnicalFeasibilityRemarks { get; set; }
    public string Information { get; set; }
    public string InformationRemarks { get; set; }
    public string Statutory { get; set; }
    public string StatutoryRemarks { get; set; }
    public string AdditionalNote { get; set; }
    public int Poreviwecompletionstatus { get; set; }

    //sales PO review
    public string Payment { get; set; }
    public string PaymentRemarks { get; set; }
    public string Packingcharges { get; set; }
    public string PackingchargesRemarks { get; set; }
    public string Inspection { get; set; }
    public string InspectionRemarks { get; set; }
    public string LDclause { get; set; }
    public string LDclauseRemarks { get; set; }
    public string Insurance { get; set; }
    public string InsuranceRemarks { get; set; }
    public string Transporter { get; set; }
    public string TransporterRemarks { get; set; }
    public string Roadpermit { get; set; }
    public string RoadpermitRemarks { get; set; }
    public string Deliveryschedule { get; set; }
    public string DeliveryscheduleRemarks { get; set; }
    public string GST { get; set; }
    public string GSTRemarks { get; set; }
    public string Additionalnote { get; set; }
    public DateTime PRDate { get; set; }
    public int SalesPoreviwecompletionstatus { get; set; }

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

    public int DDID { get; set; }
    public int SalesItemid { get; set; }
    public int ProductionOverhead { get; set; }
    public int ConsumableCost { get; set; }
    public decimal PackagingCost { get; set; }
    public decimal MakingOverhead { get; set; }
    public int MiscExpense { get; set; }
    public decimal Totalcost { get; set; }
    public decimal Recommendedcost { get; set; }
    public int Revisedcost { get; set; }
    public int SheadApproval { get; set; }
    public string RevisedReason { get; set; }
    public int Finalcost { get; set; }

    public int ESRID { get; set; }

    public int Itemid { get; set; }
    public int Sizeid { get; set; }
    public int Revisionno { get; set; }
    public int Quantity { get; set; }
    public string Tagno { get; set; }
    public string ItemDescription { get; set; }
    public string ItemPressure { get; set; }
    public string ItemTemperature { get; set; }
    public string ItemMovement { get; set; }
    public string Matrlwarning { get; set; }
    public DateTime Createdon { get; set; }

    public int CID { get; set; }
    public decimal INRvalue { get; set; }
    public string Currencyname { get; set; }
    public string Currencysymbol { get; set; }

    public string POEnquirynumber { get; set; }
    public string POHID { get; set; }
    public string PORefgNo { get; set; }
    public string POCopyWithoutPrice { get; set; }
    public string POCopy { get; set; }
    public DateTime PODate { get; set; }
    public Decimal UnitPrice { get; set; }
    public int EDID { get; set; }

    public DataTable POTable { get; set; }

    // public int UnitPrice { get; set; }
    public int PODID { get; set; }
    public int RFPQTY { get; set; }
    public string date { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public DateTime dateOfDelivery { get; set; }
    public int RFPHID { get; set; }

    public DateTime OfferSubmissionDate { get; set; }


    public string SizeOfEJ { get; set; }
    public string TypeOfEJ { get; set; }


    public string SizeOfEJRemarks { get; set; }
    public string TypeOfEJRemarks { get; set; }

    public int OfferTypeID { get; set; }

    //public int Flag { get; set; }
    public int MPID { get; set; }

    public string Mode { get; set; }

    public string EnquiryApplication { get; set; }
    public string LocationID { get; set; }

    public int MarketingPercentage { get; set; }
    public decimal PackagingPercentage { get; set; }
    public decimal UnitCost { get; set; }


    public decimal MarketingCost { get; set; }
    public decimal MarketingCostPercentage { get; set; }

    public decimal MarketingOverHeadPercentage { get; set; }
    public decimal ApprovedCost { get; set; }

    public string itemCodeType { get; set; }

    public string ItemMovementType { get; set; }

    public DataTable dt { get; set; }

    public int EODID { get; set; }
    public int CommercialOffer { get; set; }

    public int ProductId { get; set; }

    public string CommodityID { get; set; }
    public DateTime InVoiceDate { get; set; }
    public string Consignee { get; set; }
    public string BuyerOtherThanConsignee { get; set; }
    public string CountryOfOrginOfGoods { get; set; }
    public string CountryOfFinalDestination { get; set; }
    public string PreCarriedBy { get; set; }
    public string PlaceOfReceiptByPrecarrier { get; set; }
    public string VesselFlightNo { get; set; }
    public string PortOfDischarge { get; set; }
    public string PortOfLanding { get; set; }
    public string FinalDestination { get; set; }
    public string TermsOfDelivery { get; set; }
    public string ModeOfDispatch { get; set; }
    public string MarksAndNos { get; set; }
    public string NumberOfPackages { get; set; }
    public string KindOfPackages { get; set; }
    public string LUTARNNo { get; set; }
    public string EPCGLicenseNo { get; set; }
    public string FactoryAddress { get; set; }
    public string Cureency { get; set; }
    public string Declaration { get; set; }
    public string ItemQty { get; set; }


    //Dispatch And Invoice Domestic

    public string Email { get; set; }
    public string GSTINNo { get; set; }
    public string PANNo { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string TransportationMode { get; set; }
    public string VechileNo { get; set; }
    public string PlaceofSupply { get; set; }
    public string TransporterName { get; set; }
    public string Freight { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverAddress { get; set; }
    public string State { get; set; }
    public string ReceiverGSTINNo { get; set; }
    public string ReceiverStateCode { get; set; }
    public string ConsigneeName { get; set; }
    public string ConsigneeAddress { get; set; }
    public string ConsigneeGSTINNo { get; set; }
    public string FactoryAddrss { get; set; }
    public string StateCode { get; set; }
    public string PackingCharges { get; set; }
    public string InspectionCharges { get; set; }
    public string FreightCharges { get; set; }
    public string IGSTPerCentage { get; set; }
    public string ElectronicReferenceNo { get; set; }
    public string OtherCharges { get; set; }


    public string InVoiceType { get; set; }
    public int InVoiceID { get; set; }
    public string ApprovalStatus { get; set; }
    public string InVoiceRemarks { get; set; }

    public int CostVersion { get; set; }

    public decimal unitcostForignCurrency { get; set; }
    public string SalesCostNote { get; set; }

    public int PUID { get; set; }

    public string PageFlag { get; set; }
    public int SIEHID { get; set; }
    public int ETCID { get; set; }

    public int OSTID { get; set; }
    public int OLDID { get; set; }
    public DateTime ReviewDate { get; set; }
    public string Remarks { get; set; }

    public int OSHID { get; set; }
    public string CurrentStatus { get; set; }
    public string ReScheduledSubmissiondate { get; set; }
    public DateTime ReScheduledDate { get; set; }
    public string RescheduledateReason { get; set; }
    public string OfferNameID { get; set; }
    public string FaxNo { get; set; }
    public string ContactPersonPhoneNo { get; set; }
    public string CustomerEmail { get; set; }
    public string ContactPersonEmail { get; set; }
    public string MarketingPersonName { get; set; }

    public string MarketingPersonDesignation { get; set; }
    public string MarketingPersonMobileNo { get; set; }
    public string MarketingPersonEmail { get; set; }
    public string MarketingOfficePhoneNo { get; set; }
    public string MarketingHeadName { get; set; }
    public string MarketingHeadDesignation { get; set; }
    public string MarketingHeadMobileNo { get; set; }
    public string MarketingHeadEmail { get; set; }
    public string SubJectItem { get; set; }
    public string Projectname { get; set; }
    public string Reference { get; set; }
    public string Frontpage { get; set; }


    public string TransitInsurance { get; set; }
    public string Delivery { get; set; }
    public string Guarantee { get; set; }
    public string PaymentTerms { get; set; }
    public string Validity { get; set; }
    public string LDClause { get; set; }
    public string Settlement { get; set; }
    public string Legalization { get; set; }
    public string ForceMajeure { get; set; }
    public string CountryofOrigin { get; set; }
    public string PortandLocationofShipment { get; set; }
    public string DrawingGivenByCustomer { get; set; }
    public string UStampValue { get; set; }
    public string IBRValue { get; set; }
    public string SupervisionCharges { get; set; }
    public string IIIPartyInspectionCharges { get; set; }
    public string SeaFreight { get; set; }
    public string AirFreight { get; set; }
    public string FOBCharges { get; set; }
    public string ReplaceExWorks { get; set; }
    public string ReplacePackingChargesOnRate { get; set; }
    public string SeaWorthCharges { get; set; }
    public string ASMECodeCharges { get; set; }
    public string OceanFreightCharges { get; set; }
    public string Note { get; set; }
    public string QuotedPrice { get; set; }
    public string ExForBasis { get; set; }
    public string CustomermobileNo { get; set; }
    public string InvoiceAmount { get; set; }
    public string InvoiceNo { get; set; }
    public string CollectionAmount { get; set; }
    public string PaymentMode { get; set; }
    public string IDID { get; set; }
    public string TDSDeducted { get; set; }
    public string BasicValue { get; set; }
    public string InvoiceValue { get; set; }
    public string RefNo { get; set; }
    public DateTime PaymentDate { get; set; }
    public string InvoiceRFPHID { get; set; }
    public int EmployeeID { get; set; }
    public string TargetValue { get; set; }
    public int SEFYTG { get; set; }
    public string FYID { get; set; }
    public string IPDID { get; set; }
    public string EPPEID { get; set; }
    public string FileName { get; set; }
    public int CompanyID { get; set; }
    public string RFPDAID { get; set; }

    /// <summary>
    /// ///
    /// </summary>
    /// <returns></returns>

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

    public DataSet GetCustomerslist()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetCustomer", ref ds);
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
    public DataSet getCostsheet(int enqid, string Spname)
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = Spname;
            c.Parameters.AddWithValue("@Enquiryid", enqid);
            DAL.GetDataset(c, ref ds);
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

            c.Parameters.Add("@ProspectID", ProspectID);

            c.Parameters.Add("@ProjectDescription", ProjectDescription);
            c.Parameters.Add("@EMDAmount", EMDAmount);
            c.Parameters.Add("@ReceivedDate", ReceivedDate);
            c.Parameters.Add("@ClosingDate", ClosingDate);

            c.Parameters.Add("@Drawingoffer", DBNull.Value);
            c.Parameters.Add("@budgetaryoffer", budgetaryoffer);

            c.Parameters.Add("@EnquiryLocation", EnquiryLocation);
            c.Parameters.Add("@EnquiryNewType", Type);

            c.Parameters.Add("@AttachmentName", AttachementName);

            c.Parameters.Add("@Source", Source);
            c.Parameters.Add("@SalesResource", SalesResource);
            c.Parameters.Add("@RfpGroupID", RfpGroupID);
            c.Parameters.Add("@CompanyID", CompanyID);

            if (CommercialOffer == -1)
            {
                c.Parameters.Add("@CommercialOffer", DBNull.Value);
                c.Parameters.Add("@OfferSubmissionDate", OfferSubmissionDate);
                c.Parameters.Add("@LseNumber", LseNumber);
                c.Parameters.Add("@ContactPerson", ContactPerson);
                c.Parameters.Add("@ContactNumber", ContactNumber);
                c.Parameters.Add("@Email", EmailId);
                c.Parameters.Add("@AlternateContactNumber", AlternateContactNumber);
                c.Parameters.Add("@notinterested", notinterested);
                c.Parameters.Add("@UserID", UserID);
                c.Parameters.Add("@AttachementTypeName", AttachementTypeName);
                c.Parameters.Add("@AttachementDescription", AttachementDescription);
                c.Parameters.Add("@ItemDescription", ItemDescription);
                c.Parameters.Add("@ProductId", ProductId);
            }
            else
            {
                c.Parameters.Add("@CommercialOffer", CommercialOffer);
                c.Parameters.Add("@OfferSubmissionDate", DBNull.Value);
                c.Parameters.Add("@LseNumber", DBNull.Value);
                c.Parameters.Add("@ContactPerson", DBNull.Value);
                c.Parameters.Add("@ContactNumber", DBNull.Value);
                c.Parameters.Add("@Email", DBNull.Value);
                c.Parameters.Add("@AlternateContactNumber", DBNull.Value);
                c.Parameters.Add("@notinterested", DBNull.Value);
                c.Parameters.Add("@UserID", DBNull.Value);
                c.Parameters.Add("@AttachementTypeName", DBNull.Value);
                c.Parameters.Add("@AttachementDescription", DBNull.Value);
                c.Parameters.Add("@ItemDescription", DBNull.Value);
                c.Parameters.Add("@ProductId", DBNull.Value);
            }

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

    public DataSet GetEnquiryprocessDetails(string EnquiryID, string spname, bool SIFlag)
    {
        DataSet ds = new DataSet();
        int SI;
        if (SIFlag)
            SI = 1;
        else
            SI = 0;
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@EnquiryID", EnquiryID);//EnquiryID);            
            c.Parameters.Add("@SIFlag", SI);
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
    public DataSet GetSalesCostDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesCostDetailsByEnquiryID";
            c.Parameters.Add("@DDID", DDID);
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
            //   c.Parameters.Add("@Budgetary", BudgetaryEnquiry);

            c.Parameters.Add("@AttachementName", DBNull.Value);

            c.Parameters.Add("@SizeOfEJ", SizeOfEJ);
            c.Parameters.Add("@SizeOfEJRemarks", SizeOfEJRemarks);
            c.Parameters.Add("@TypeOfEJ", TypeOfEJ);
            c.Parameters.Add("@TypeOfEJRemarks", TypeOfEJRemarks);

            c.Parameters.Add("@OfferTypeID", OfferTypeID);

            DAL.GetDataset(c, ref ds);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveDesignPOReviewClarrification()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePOReviewCheckListDetails";
            c.Parameters.AddWithValue("@POHID", POHID);
            c.Parameters.AddWithValue("@UStamp", UStamp);
            c.Parameters.AddWithValue("@UStampRemarks", UStampRemarks);
            c.Parameters.AddWithValue("@U2Stamp", U2Stamp);
            c.Parameters.AddWithValue("@U2StampRemarks", U2StampRemarks);
            c.Parameters.AddWithValue("@PED", PED);
            c.Parameters.AddWithValue("@PEDRemarks", PEDRemarks);
            c.Parameters.AddWithValue("@IBR", IBR);
            c.Parameters.AddWithValue("@IBRRemarks", IBRRemarks);
            c.Parameters.AddWithValue("@ISO", ISO);
            c.Parameters.AddWithValue("@ISORemarks", ISORemarks);
            c.Parameters.AddWithValue("@SpecialInformation", SpecialInformation);
            c.Parameters.AddWithValue("@SpecialInformationRemarks", SpecialInformationRemarks);
            c.Parameters.AddWithValue("@Units", Units);
            c.Parameters.AddWithValue("@UnitsRemarks", UnitsRemarks);
            c.Parameters.AddWithValue("@CheckedwithOffer", CheckedwithOffer);
            c.Parameters.AddWithValue("@CheckedwithOfferRemarks", CheckedwithOfferRemarks);
            c.Parameters.AddWithValue("@TechnicalFeasibility", TechnicalFeasibility);
            c.Parameters.AddWithValue("@TechnicalFeasibilityRemarks", TechnicalFeasibilityRemarks);
            c.Parameters.AddWithValue("@Information", Information);
            c.Parameters.AddWithValue("@InformationRemarks", InformationRemarks);
            c.Parameters.AddWithValue("@Statutory", Statutory);
            c.Parameters.AddWithValue("@StatutoryRemarks", StatutoryRemarks);
            c.Parameters.AddWithValue("@AdditionalNote", AdditionalNote);
            c.Parameters.AddWithValue("@Poreviwecompletionstatus", Poreviwecompletionstatus);
            DAL.GetDataset(c, ref ds);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
    public DataSet SaveSalesPOReviewClarrification()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSalesPOReviewCheckListDetails";
            c.Parameters.AddWithValue("@POHID", POHID);
            c.Parameters.AddWithValue("@Payment", Payment);
            c.Parameters.AddWithValue("@PaymentRemarks", PaymentRemarks);
            c.Parameters.AddWithValue("@Packingcharges", Packingcharges);
            c.Parameters.AddWithValue("@PackingchargesRemarks", PackingchargesRemarks);
            c.Parameters.AddWithValue("@Inspection", Inspection);
            c.Parameters.AddWithValue("@InspectionRemarks", InspectionRemarks);
            c.Parameters.AddWithValue("@LDclause", LDclause);
            c.Parameters.AddWithValue("@LDclauseRemarks", LDclauseRemarks);
            c.Parameters.AddWithValue("@Insurance", Insurance);
            c.Parameters.AddWithValue("@InsuranceRemarks", InsuranceRemarks);
            c.Parameters.AddWithValue("@Transporter", Transporter);
            c.Parameters.AddWithValue("@TransporterRemarks", TransporterRemarks);
            c.Parameters.AddWithValue("@Roadpermit", Roadpermit);
            c.Parameters.AddWithValue("@RoadpermitRemarks", RoadpermitRemarks);
            c.Parameters.AddWithValue("@Deliveryschedule", Deliveryschedule);
            c.Parameters.AddWithValue("@DeliveryscheduleRemarks ", DeliveryscheduleRemarks);
            c.Parameters.AddWithValue("@GuaranteeTerms", GuaranteeTerms);
            c.Parameters.AddWithValue("@GuaranteeRemarks", GuaranteeRemarks);
            c.Parameters.AddWithValue("@GST", GST);
            c.Parameters.AddWithValue("@GSTRemarks", GSTRemarks);
            c.Parameters.AddWithValue("@Additionalnote", Additionalnote);
            c.Parameters.AddWithValue("@PRDate", PRDate);
            c.Parameters.AddWithValue("@SalesPoreviwecompletionstatus", SalesPoreviwecompletionstatus);
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
            c.Parameters.Add("@Status", CurrentStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPOReviewDetails(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPOReviewCheckListDetails(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@POhid", POHID);
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
            c.Parameters.Add("@Type", Type);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    public DataSet GetEnquiryCommunicationMessageBySenderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "GetEnquiryCommunicationMessageBySenderDetails";
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

    public DataSet GetEnquiryProjectAssignmentStatusReportByUserID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryProjectAssignmentStatusReportByUserID";
            c.Parameters.Add("@UserID", UserID);
            if (fromDate == "")
                c.Parameters.Add("@FromDate", DBNull.Value);
            else
                c.Parameters.Add("@FromDate", FromDate);
            if (toDate == "")
                c.Parameters.Add("@ToDate", DBNull.Value);
            else
                c.Parameters.Add("@ToDate", ToDate);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetApprovalPendingStatusReportByUserID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetApprovalPendingStatusReportByUserID";
            c.Parameters.Add("@UserID", UserID);
            if (fromDate == "")
                c.Parameters.Add("@FromDate", DBNull.Value);
            else
                c.Parameters.Add("@FromDate", FromDate);
            if (toDate == "")
                c.Parameters.Add("@ToDate", DBNull.Value);
            else
                c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryStatusReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryStatusReport";
            if (fromDate == "")
                c.Parameters.Add("@FromDate", DBNull.Value);
            else
                c.Parameters.Add("@FromDate", FromDate);
            if (toDate == "")
                c.Parameters.Add("@ToDate", DBNull.Value);
            else
                c.Parameters.Add("@ToDate", ToDate);
            // c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveCustomerPO()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCustomerPO";
            c.Parameters.Add("@POHID", POHID);
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            if (date != "")
                c.Parameters.Add("@PODate", PODate);
            else
                c.Parameters.Add("@PODate", DBNull.Value);
            c.Parameters.Add("@PORefgNo", PORefgNo);
            c.Parameters.Add("@POCopy", POCopy);
            c.Parameters.Add("@POCopyWithoutPrice", POCopyWithoutPrice);
            c.Parameters.Add("@EODID", EODID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerPO()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerPO";
            c.Parameters.Add("@EmployeeID", EmpID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveCustomerPODetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCustomerPODetails";
            c.Parameters.AddWithValue("@tblPODetails", POTable);
            //c.Parameters.Add("@POHID", POHID);
            //c.Parameters.Add("@EDID", EDID);
            //c.Parameters.Add("@Quantity", Quantity);
            //c.Parameters.Add("@UnitPrice", UnitPrice);
            //c.Parameters.Add("@dateOfDelivery", dateOfDelivery);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetCustomerPODetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerPODetails";
            c.Parameters.Add("@EODID", EODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet DeleteCustomerPODetailsByPODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCustomerPODetails";
            c.Parameters.AddWithValue("@PODID", PODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveRFPAttachements()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRFPAttachements";
            //   c.Parameters.Add("@AttachementID", AttachementID);
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@AttachementTypeID", AttachementTypeName);
            c.Parameters.Add("@Filename", AttachementName);
            c.Parameters.Add("@AttachementDescription", Description);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMaterialPlanningAttachements()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialPlanningAttachements";
            //   c.Parameters.Add("@AttachementID", AttachementID);
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@AttachementTypeID", AttachementTypeName);
            c.Parameters.Add("@Filename", AttachementName);
            c.Parameters.Add("@AttachementDescription", Description);
            c.Parameters.Add("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetAttachementDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.CommandText = "LS_GetAttachementsDetailsByRFPHID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPItemdetails(string RFPHID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemdetails";
            c.Parameters.Add("@RFPHID", RFPHID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet DeleteRFPItemdetails(string RFPDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteRFPItemdetails";
            c.Parameters.Add("@RFPDID", RFPDID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAttachementDetailsByMPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;

            c.Parameters.Add("@MPID", MPID);
            c.CommandText = "LS_GetAttachementsDetailsByMPID";


            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet InsertRFPItemdetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_InertRFPITEMdetails";
            c.Parameters.AddWithValue("@PODID", PODID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPQTY", RFPQTY);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet UpdateRFPItemdetails(string RFPDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateRFPITEMdetails";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@PODID", PODID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPQTY", RFPQTY);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet getBellowsDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBellowsDetailsByRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
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

    public DataSet GetEnquiryTradingReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryTradingReport";
            c.Parameters.AddWithValue("@Mode", Mode);
            if (Mode == "Custom")
            {
                c.Parameters.AddWithValue("@FromDate", FromDate);
                c.Parameters.AddWithValue("@ToDate", ToDate);
            }
            else
            {
                c.Parameters.AddWithValue("@FromDate", DBNull.Value);
                c.Parameters.AddWithValue("@ToDate", DBNull.Value);
            }
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryOfferDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryOfferDetailsByEnquiryID";
            c.Parameters.AddWithValue("@EnquiryID", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOfferNumberByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferNumberByEnquiryID";
            c.Parameters.AddWithValue("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateOfferGeneratedStatusbyEODID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateOfferGeneratedStatusByEODID";
            c.Parameters.AddWithValue("@EODID", EODID);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateOfferDispatchedStatus()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateOfferDispatchedStatus";
            c.Parameters.AddWithValue("@tblOfferDispatchedStatus", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetPoOrderedItemDetailsByPOHID(string spname)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@POHID", POHID);
            c.Parameters.AddWithValue("@ProspectID", ProspectID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveInternationalInvoiceDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveInternationalInvoiceDetails";
            c.Parameters.AddWithValue("@POHID ", POHID);
            c.Parameters.AddWithValue("@CommodityID ", CommodityID);
            c.Parameters.AddWithValue("@InVoiceDate ", InVoiceDate);
            c.Parameters.AddWithValue("@Consignee ", Consignee);
            c.Parameters.AddWithValue("@BuyerOtherThanConsignee ", BuyerOtherThanConsignee);
            c.Parameters.AddWithValue("@CountryOfOrginOfGoods ", CountryOfOrginOfGoods);
            c.Parameters.AddWithValue("@CountryOfFinalDestination ", CountryOfFinalDestination);
            c.Parameters.AddWithValue("@PreCarriedBy ", PreCarriedBy);
            c.Parameters.AddWithValue("@PlaceOfReceiptByPrecarrier", PlaceOfReceiptByPrecarrier);
            c.Parameters.AddWithValue("@VesselFlightNo ", VesselFlightNo);
            c.Parameters.AddWithValue("@PortOfDischarge ", PortOfDischarge);
            c.Parameters.AddWithValue("@PortOfLanding ", PortOfLanding);
            c.Parameters.AddWithValue("@FinalDestination ", FinalDestination);
            c.Parameters.AddWithValue("@TermsOfDelivery ", TermsOfDelivery);
            c.Parameters.AddWithValue("@Payment ", Payment);
            c.Parameters.AddWithValue("@ModeOfDispatch ", ModeOfDispatch);
            c.Parameters.AddWithValue("@MarksAndNos ", MarksAndNos);
            c.Parameters.AddWithValue("@NumberOfPackages ", NumberOfPackages);
            c.Parameters.AddWithValue("@KindOfPackages ", KindOfPackages);
            c.Parameters.AddWithValue("@LUTARNNo ", LUTARNNo);
            c.Parameters.AddWithValue("@EPCGLicenseNo ", EPCGLicenseNo);
            c.Parameters.AddWithValue("@FactoryAddress ", FactoryAddress);
            c.Parameters.AddWithValue("@LocationID ", LocationID);
            c.Parameters.AddWithValue("@Cureency ", Cureency);
            c.Parameters.AddWithValue("@Declaration ", Declaration);
            c.Parameters.AddWithValue("@tblInterInvoiceItemDetails", dt);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveDomesticInvoiceDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDomesticInvoiceDetails";
            c.Parameters.AddWithValue("@POHID ", POHID);
            c.Parameters.AddWithValue("@CommodityID", CommodityID);
            c.Parameters.AddWithValue("@PhoneNo ", PhoneNo);
            c.Parameters.AddWithValue("@Email ", Email);
            c.Parameters.AddWithValue("@GSTINNo ", GSTINNo);
            c.Parameters.AddWithValue("@PANNo ", PANNo);
            c.Parameters.AddWithValue("@InvoiceDate  ", InvoiceDate);
            c.Parameters.AddWithValue("@TransportationMode ", TransportationMode);
            c.Parameters.AddWithValue("@VechileNo ", VechileNo);
            c.Parameters.AddWithValue("@PlaceofSupply ", PlaceofSupply);
            c.Parameters.AddWithValue("@TransporterName ", TransporterName);
            c.Parameters.AddWithValue("@Freight  ", Freight);
            c.Parameters.AddWithValue("@Payment", Payment);
            c.Parameters.AddWithValue("@ReceiverName ", ReceiverName);
            c.Parameters.AddWithValue("@ReceiverAddress ", ReceiverAddress);
            c.Parameters.AddWithValue("@State ", State);
            c.Parameters.AddWithValue("@ReceiverGSTINNo ", ReceiverGSTINNo);
            c.Parameters.AddWithValue("@ReceiverStateCode ", ReceiverStateCode);
            c.Parameters.AddWithValue("@ConsigneeName ", ConsigneeName);
            c.Parameters.AddWithValue("@ConsigneeAddress ", ConsigneeAddress);
            c.Parameters.AddWithValue("@ConsigneeGSTINNo  ", ConsigneeGSTINNo);
            c.Parameters.AddWithValue("@FactoryAddrss ", FactoryAddrss);
            c.Parameters.AddWithValue("@LocationID ", LocationID);
            c.Parameters.AddWithValue("@StateCode ", StateCode);
            c.Parameters.AddWithValue("@PackingCharges ", PackingCharges);
            c.Parameters.AddWithValue("@InspectionCharges ", InspectionCharges);
            c.Parameters.AddWithValue("@FreightCharges ", FreightCharges);
            c.Parameters.AddWithValue("@IGSTPerCentage ", IGSTPerCentage);
            c.Parameters.AddWithValue("@ElectronicReferenceNo ", ElectronicReferenceNo);
            c.Parameters.AddWithValue("@OtherCharges ", OtherCharges);
            c.Parameters.AddWithValue("@tblDomesticInvoiceItemDetails", dt);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet getInVoiceApprovalDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInVoiceApprovalDetails";

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet UpdateInvoiceApproval()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateInVoiceApproval";
            c.Parameters.AddWithValue("@InVoiceType", InVoiceType);
            c.Parameters.AddWithValue("@InVoiceID", InVoiceID);
            c.Parameters.AddWithValue("@ApprovalStatus", ApprovalStatus);
            c.Parameters.AddWithValue("@InVoiceRemarks", InVoiceRemarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceApprovedDetailsForMakeDispatchByPOHID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceApprovedDetailsForMakeDispatchbyPOHID";
            c.Parameters.AddWithValue("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInvoiceDispatchedDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateInvoiceDispatchedDetails";
            c.Parameters.AddWithValue("@tblInvoiceDispatchedDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet ShareRFPStatusByPOHID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateRFPStatusByPOHID";
            c.Parameters.AddWithValue("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSalesPoReviewCheckListdetailsByPOHIDForPDf()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesPORevieCheckListDetailsForPDF";
            c.Parameters.AddWithValue("@POhid", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdatePOSharedStatus()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateCustomerPOSharedStatus";
            c.Parameters.AddWithValue("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateSalesCostreviewStatus()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSalesCostReviewStatus";
            c.Parameters.AddWithValue("@ETCID", ETCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveOfferStatusHistoryDetails(string msg)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveOfferHistoryDetails";
            c.Parameters.AddWithValue("@EODID", EODID);
            c.Parameters.AddWithValue("@OSHID", OSHID);
            c.Parameters.AddWithValue("@EnquiryNumber", EnquiryNumber);
            c.Parameters.AddWithValue("@OSTID", OSTID);
            if (msg == "1")
            {
                c.Parameters.AddWithValue("@OLDID", OLDID);
                c.Parameters.AddWithValue("@ReviewDate", DBNull.Value);
            }
            else if (msg == "2")
            {
                c.Parameters.AddWithValue("@OLDID", DBNull.Value);
                c.Parameters.AddWithValue("@ReviewDate", ReviewDate);
            }
            else
            {
                c.Parameters.AddWithValue("@OLDID", DBNull.Value);
                c.Parameters.AddWithValue("@ReviewDate", DBNull.Value);
            }

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

    public DataSet GetOfferStatusDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferStatusDetails";

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOfferStatusDetailsByOSHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferStatusDetailsByOSHID";
            c.Parameters.AddWithValue("@OSHID", OSHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSalesActivitiesEnquiryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDailyActivitiesSalesEnquiryDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveDailyActivitiesSalesEnquiryDetails(bool rsdate)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDailyActivitiesSalesEnquiryDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@CurrentStatus", CurrentStatus);
            c.Parameters.Add("@ReScheduledSubmissiondate", ReScheduledSubmissiondate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", UserID);
            c.Parameters.Add("@RescheduledateReason", RescheduledateReason);
            if (rsdate)
                c.Parameters.Add("@ReScheduledDate", ReScheduledDate);
            else
                c.Parameters.Add("@ReScheduledDate", DBNull.Value);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    //EODID
    //UserID
    //EnquiryNumber
    //OfferNameID
    //EnquiryNumber
    //FaxNo
    //ContactPersonPhoneNo
    //CustomerEmail
    //ContactPerson
    //ContactPersonEmail
    //MarketingPersonName
    //MarketingPersonDesignation
    //MarketingPersonMobileNo
    //MarketingPersonEmail
    //MarketingOfficePhoneNo
    //MarketingHeadName
    //MarketingHeadDesignation
    //MarketingHeadMobileNo
    //MarketingHeadEmail
    //SubJectItem
    //Projectname
    //Reference
    //Frontpage
    //CreatedBy
    //GST
    //Freight
    //Insurance
    //Inspection
    //TransitInsurance
    //Delivery
    //Guarantee
    //PaymentTerms
    //Validity
    //LDClause
    //Settlement
    //Legalization
    //ForceMajeure
    //CountryofOrigin
    //PortandLocationofShipment
    //DrawingGivenByCustomer
    //UStampValue
    //IBRValue
    //SupervisionCharges
    //IIIPartyInspectionCharges
    //SeaFreight
    //AirFreight
    //FOBCharges
    //ReplaceExWorks
    //ReplacePackingChargesOnRate
    //SeaWorthCharges
    //ASMECodeCharges
    //OceanFreightCharges
    //Note

    public DataSet SaveGenerateOfferDetails(string SOWPID, string TACPID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGenerateOfferDetails";
            c.Parameters.Add("@EODID", EODID);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@OfferNameID", OfferNameID);
            c.Parameters.Add("@FaxNo", FaxNo);
            c.Parameters.Add("@ContactPersonPhoneNo", ContactPersonPhoneNo);
            c.Parameters.Add("@CustomerEmail", CustomerEmail);
            c.Parameters.Add("@ContactPerson", ContactPerson);
            c.Parameters.Add("@ContactPersonEmail", ContactPersonEmail);
            c.Parameters.Add("@MarketingPersonName", MarketingPersonName);
            c.Parameters.Add("@MarketingPersonDesignation", MarketingPersonDesignation);
            c.Parameters.Add("@MarketingPersonMobileNo", MarketingPersonMobileNo);
            c.Parameters.Add("@MarketingPersonEmail", MarketingPersonEmail);
            c.Parameters.Add("@MarketingOfficePhoneNo", MarketingOfficePhoneNo);
            c.Parameters.Add("@MarketingHeadName", MarketingHeadName);
            c.Parameters.Add("@MarketingHeadDesignation", MarketingHeadDesignation);
            c.Parameters.Add("@MarketingHeadMobileNo", MarketingHeadMobileNo);
            c.Parameters.Add("@MarketingHeadEmail", MarketingHeadEmail);
            c.Parameters.Add("@SubJectItem", SubJectItem);
            c.Parameters.Add("@Projectname", Projectname);
            c.Parameters.Add("@Reference", Reference);
            c.Parameters.Add("@Frontpage", Frontpage);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            if (string.IsNullOrEmpty(GST))
                c.Parameters.Add("@GST", DBNull.Value);
            else
                c.Parameters.Add("@GST", GST);

            if (string.IsNullOrEmpty(Freight))
                c.Parameters.Add("@Freight", DBNull.Value);
            else
                c.Parameters.Add("@Freight", Freight);

            if (string.IsNullOrEmpty(Insurance))
                c.Parameters.Add("@Insurance", DBNull.Value);
            else
                c.Parameters.Add("@Insurance", Insurance);

            if (string.IsNullOrEmpty(Inspection))
                c.Parameters.Add("@Inspection", DBNull.Value);
            else
                c.Parameters.Add("@Inspection", Inspection);

            if (string.IsNullOrEmpty(TransitInsurance))
                c.Parameters.Add("@TransitInsurance", DBNull.Value);
            else
                c.Parameters.Add("@TransitInsurance", TransitInsurance);

            if (string.IsNullOrEmpty(Delivery))
                c.Parameters.Add("@Delivery", DBNull.Value);
            else
                c.Parameters.Add("@Delivery", Delivery);

            if (string.IsNullOrEmpty(Guarantee))
                c.Parameters.Add("@Guarantee", DBNull.Value);
            else
                c.Parameters.Add("@Guarantee", Guarantee);

            if (string.IsNullOrEmpty(PaymentTerms))
                c.Parameters.Add("@PaymentTerms", DBNull.Value);
            else
                c.Parameters.Add("@PaymentTerms", PaymentTerms);

            if (string.IsNullOrEmpty(Validity))
                c.Parameters.Add("@Validity", DBNull.Value);
            else
                c.Parameters.Add("@Validity", Validity);

            if (string.IsNullOrEmpty(LDClause))
                c.Parameters.Add("@LDClause", DBNull.Value);
            else
                c.Parameters.Add("@LDClause", LDClause);

            if (string.IsNullOrEmpty(Settlement))
                c.Parameters.Add("@Settlement", DBNull.Value);
            else
                c.Parameters.Add("@Settlement", Settlement);

            if (string.IsNullOrEmpty(Legalization))
                c.Parameters.Add("@Legalization", DBNull.Value);
            else
                c.Parameters.Add("@Legalization", Legalization);

            if (string.IsNullOrEmpty(ForceMajeure))
                c.Parameters.Add("@ForceMajeure", DBNull.Value);
            else
                c.Parameters.Add("@ForceMajeure", ForceMajeure);

            if (string.IsNullOrEmpty(CountryofOrigin))
                c.Parameters.Add("@CountryofOrigin", DBNull.Value);
            else
                c.Parameters.Add("@CountryofOrigin", CountryofOrigin);

            if (string.IsNullOrEmpty(PortandLocationofShipment))
                c.Parameters.Add("@PortandLocationofShipment", DBNull.Value);
            else
                c.Parameters.Add("@PortandLocationofShipment", PortandLocationofShipment);

            if (string.IsNullOrEmpty(DrawingGivenByCustomer))
                c.Parameters.Add("@DrawingGivenByCustomer", DBNull.Value);
            else
                c.Parameters.Add("@DrawingGivenByCustomer", DrawingGivenByCustomer);

            if (string.IsNullOrEmpty(UStampValue))
                c.Parameters.Add("@UStampValue", DBNull.Value);
            else
                c.Parameters.Add("@UStampValue", UStampValue);

            if (string.IsNullOrEmpty(IBRValue))
                c.Parameters.Add("@IBRValue", DBNull.Value);
            else
                c.Parameters.Add("@IBRValue", IBRValue);

            if (string.IsNullOrEmpty(SupervisionCharges))
                c.Parameters.Add("@SupervisionCharges", DBNull.Value);
            else
                c.Parameters.Add("@SupervisionCharges", SupervisionCharges);

            if (string.IsNullOrEmpty(IIIPartyInspectionCharges))
                c.Parameters.Add("@IIIPartyInspectionCharges", DBNull.Value);
            else
                c.Parameters.Add("@IIIPartyInspectionCharges", IIIPartyInspectionCharges);

            if (string.IsNullOrEmpty(SeaFreight))
                c.Parameters.Add("@SeaFreight", DBNull.Value);
            else
                c.Parameters.Add("@SeaFreight", SeaFreight);

            if (string.IsNullOrEmpty(AirFreight))
                c.Parameters.Add("@AirFreight", DBNull.Value);
            else
                c.Parameters.Add("@AirFreight", AirFreight);

            if (string.IsNullOrEmpty(FOBCharges))
                c.Parameters.Add("@FOBCharges", DBNull.Value);
            else
                c.Parameters.Add("@FOBCharges", FOBCharges);

            if (string.IsNullOrEmpty(ReplaceExWorks))
                c.Parameters.Add("@ReplaceExWorks", DBNull.Value);
            else
                c.Parameters.Add("@ReplaceExWorks", ReplaceExWorks);

            if (string.IsNullOrEmpty(ReplacePackingChargesOnRate))
                c.Parameters.Add("@ReplacePackingChargesOnRate", DBNull.Value);
            else
                c.Parameters.Add("@ReplacePackingChargesOnRate", ReplacePackingChargesOnRate);

            if (string.IsNullOrEmpty(SeaWorthCharges))
                c.Parameters.Add("@SeaWorthCharges", DBNull.Value);
            else
                c.Parameters.Add("@SeaWorthCharges", SeaWorthCharges);

            if (string.IsNullOrEmpty(ASMECodeCharges))
                c.Parameters.Add("@ASMECodeCharges", DBNull.Value);
            else
                c.Parameters.Add("@ASMECodeCharges", ASMECodeCharges);

            if (string.IsNullOrEmpty(OceanFreightCharges))
                c.Parameters.Add("@OceanFreightCharges", DBNull.Value);
            else
                c.Parameters.Add("@OceanFreightCharges", OceanFreightCharges);

            if (string.IsNullOrEmpty(Note))
                c.Parameters.Add("@Note", DBNull.Value);
            else
                c.Parameters.Add("@Note", Note);

            if (string.IsNullOrEmpty(SOWPID))
                c.Parameters.Add("@SOWPID", DBNull.Value);
            else
                c.Parameters.Add("@SOWPID", SOWPID);

            if (string.IsNullOrEmpty(TACPID))
                c.Parameters.Add("@TACPID", DBNull.Value);
            else
                c.Parameters.Add("@TACPID", TACPID);

            c.Parameters.Add("@QuotedPrice", QuotedPrice);
            c.Parameters.Add("@ExForBasis", ExForBasis);
            c.Parameters.Add("@CustomermobileNo", CustomermobileNo);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOfferPrintDetailsByEODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferPrintDetailsByEODID";
            c.Parameters.Add("@EODID", EODID);
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

    public void GetEnquiryList(DropDownList ddlName, string SPname)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset(SPname, ref ds);
            ddlName.DataSource = ds.Tables[0];
            ddlName.DataTextField = "CustomerEnquiryNumber";
            ddlName.DataValueField = "EnquiryID";
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
    public void GetProductreq(DropDownList ddlproductreq)
    {
        try
        {
            DataSet ds = new DataSet();
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetProductrequirementForEnquiryProcess", ref ds);
            ddlproductreq.DataSource = ds.Tables[0];
            ddlproductreq.DataTextField = "ProductRequirement";
            ddlproductreq.DataValueField = "ProductID";
            ddlproductreq.DataBind();
            ddlproductreq.Items.Insert(0, new ListItem("--Select--", "0"));
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

    public DataSet GetEmployeeListForEnquiryCommunication(int EnquiryID, DropDownList ddlReceiverGroup)
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
    public DataSet GetAnnexure(int i)
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            if (i == 1) DAL.GetDataset("LS_GetAnnexure1", ref ds);
            else if (i == 3) DAL.GetDataset("LS_GetAnnexure3", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet GetddlData(string paramname, string paramvalue, string spname)
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            if (paramname == "Headername") c.Parameters.AddWithValue("@Headername", paramvalue);
            else if (paramname == "Customerid")
            {
                c.Parameters.AddWithValue("@Customerid", Convert.ToInt32(paramvalue));
            }
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }
    public DataSet saveSalesCost()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SalescostSave";
            c.Parameters.AddWithValue("@DDID", DDID);

            c.Parameters.AddWithValue("@PackagingPercentage", PackagingPercentage);
            c.Parameters.AddWithValue("@MarketingCostPercentage", MarketingCostPercentage);

            //  c.Parameters.AddWithValue("@ProductionOverhead", ProductionOverhead);
            //   c.Parameters.AddWithValue("@ConsumableCost", ConsumableCost);
            c.Parameters.AddWithValue("@PackagingCost", PackagingCost);
            c.Parameters.AddWithValue("@MarketingCost", MarketingCost);
            //  c.Parameters.AddWithValue("@MiscExpense", MiscExpense);
            c.Parameters.AddWithValue("@Totalcost ", Totalcost);
            c.Parameters.AddWithValue("@Recommendedcost ", Recommendedcost);
            c.Parameters.AddWithValue("@UnitCost", UnitCost);
            c.Parameters.AddWithValue("@CostVersion", CostVersion);
            c.Parameters.AddWithValue("@ItemQty", ItemQty);
            c.Parameters.AddWithValue("@unitcostForignCurrency", unitcostForignCurrency);
            c.Parameters.AddWithValue("@CID", CID);
            c.Parameters.AddWithValue("@Note", SalesCostNote);
            c.Parameters.AddWithValue("@INRvalue", INRvalue);
            c.Parameters.AddWithValue("@PageFlag", PageFlag);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet saveSalescostApproval()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SalescostApproval";
            c.Parameters.AddWithValue("@DDID", DDID);
            c.Parameters.AddWithValue("@Revisedcost", Revisedcost);
            c.Parameters.AddWithValue("@RevisedReason", RevisedReason);
            c.Parameters.AddWithValue("@SheadApproval", SheadApproval);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet UpdateSalescostApproval()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSalescostApproval";
            c.Parameters.AddWithValue("@DDID", DDID);
            c.Parameters.AddWithValue("@MakingOverhead", MakingOverhead);
            c.Parameters.AddWithValue("@MarketingOverHeadPercentage", MarketingOverHeadPercentage);
            c.Parameters.AddWithValue("@ApprovedCost", ApprovedCost);
            c.Parameters.AddWithValue("@RevisedReason", RevisedReason);
            c.Parameters.AddWithValue("@CostRevision", CostVersion);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet updateenquiryprocess(string Enqid, string spname)
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@Userid", UserID);
            c.Parameters.AddWithValue("@Createdon", Createdon);
            c.Parameters.AddWithValue("@Enquiryid", Enqid);
            c.Parameters.AddWithValue("@Itemid", Itemid);
            c.Parameters.AddWithValue("@Sizeid", Sizeid);
            c.Parameters.AddWithValue("@Revisionno", Revisionno);
            c.Parameters.AddWithValue("@Quantity", Quantity);
            if (Tagno == null)
                c.Parameters.AddWithValue("@ItemCodeValue", DBNull.Value);
            else
                c.Parameters.AddWithValue("@ItemCodeValue", Tagno);

            c.Parameters.AddWithValue("@ItemDescription", ItemDescription);
            c.Parameters.AddWithValue("@ItemPressure", ItemPressure);
            c.Parameters.AddWithValue("@ItemTemperature", ItemTemperature);
            c.Parameters.AddWithValue("@ItemMovement", ItemMovement);
            c.Parameters.AddWithValue("@Matrlwarning", Matrlwarning);

            c.Parameters.AddWithValue("@EnquiryApplication", EnquiryApplication);
            c.Parameters.AddWithValue("@Location", LocationID);
            if (itemCodeType == null)
                c.Parameters.AddWithValue("@itemCodeType", DBNull.Value);
            else
                c.Parameters.AddWithValue("@itemCodeType", itemCodeType);

            c.Parameters.AddWithValue("@ItemMovementType", DBNull.Value);
            c.Parameters.AddWithValue("@PUID", PUID);

            c.Parameters.AddWithValue("@SIEHID", SIEHID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet deleteitemfromenquiry(string EDID, string spname)
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@EDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet UpdateSalesCost()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_Updatesalescost";
            c.Parameters.AddWithValue("@DDID", DDID);
            c.Parameters.AddWithValue("@Totalcost ", Totalcost);
            c.Parameters.AddWithValue("@Recommendedcost ", Recommendedcost);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet UpdateOfferCost()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateOfferCost";
            //c.Parameters.AddWithValue("@DDID", DDID);
            //c.Parameters.AddWithValue("@EnquiryNumber", EnquiryNumber);
            //c.Parameters.AddWithValue("@Finalcost", Finalcost);
            c.Parameters.AddWithValue("@tblOfferCost", dt);
            c.Parameters.AddWithValue("@CostRevision", CostVersion);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetEnquiryStatusReportsByESRID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryStatusreportsByESRID";
            c.Parameters.AddWithValue("@UserID", UserID);
            c.Parameters.AddWithValue("@ESRID", ESRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetStatusReportsName(DropDownList ddlStatusReports)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetEnquiryStatusReports", ref ds);
            ddlStatusReports.DataSource = ds.Tables[0];
            ddlStatusReports.DataTextField = "ReportsName";
            ddlStatusReports.DataValueField = "ESRID";
            ddlStatusReports.DataBind();
            ddlStatusReports.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
    public DataSet CurrencyDetails(int CurrencyID, DropDownList ddlCurrency = null)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCurrencyDetails";
            //  c.Parameters.AddWithValue("@CID", CurrencyID);
            DAL.GetDataset(c, ref ds);

            ddlCurrency.DataSource = ds.Tables[0];
            ddlCurrency.DataTextField = "CurrencyName";
            ddlCurrency.DataValueField = "INRvalue";
            ddlCurrency.DataBind();
            //ddlCurrency.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet SaveCurrencyDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_CurrencyDetails";
            c.Parameters.AddWithValue("@CID", CID);
            c.Parameters.AddWithValue("@Currencyname", Currencyname);
            c.Parameters.AddWithValue("@Currencysymbol", Currencysymbol);
            c.Parameters.AddWithValue("@INRvalue", INRvalue);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet DeleteCurrencydetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCurrency";
            c.Parameters.AddWithValue("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetEnquiryNumberForSalesCost(int UserID, DropDownList ddlEnquiryNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDForSalesCost";
            c.Parameters.AddWithValue("@EmployeeID", UserID);
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
    public DataSet getCustomerNameForSalesCost(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerNameForSalesCost";
            c.Parameters.AddWithValue("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
            ddlCustomerName.DataSource = ds.Tables[0];
            ddlCustomerName.DataTextField = "ProspectName";
            ddlCustomerName.DataValueField = "ProspectID";
            ddlCustomerName.DataBind();
            ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet getPOItemnameforRFP()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPOItemnameforRFP";
            c.Parameters.AddWithValue("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }



    public DataSet GetOfferTypeDetails(DropDownList ddlOfferType)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferTypeDetails";
            DAL.GetDataset(c, ref ds);

            DAL.GetDataset(c, ref ds);
            ddlOfferType.DataSource = ds.Tables[0];
            ddlOfferType.DataTextField = "OfferType";
            ddlOfferType.DataValueField = "OfferTypeID";
            ddlOfferType.DataBind();
            ddlOfferType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetHowSourcedEnquiry(DropDownList ddlHowSourcedEnquiry)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetHowSourcedEnquiry";
            DAL.GetDataset(c, ref ds);

            ddlHowSourcedEnquiry.DataSource = ds.Tables[0];
            ddlHowSourcedEnquiry.DataTextField = "SourceName";
            ddlHowSourcedEnquiry.DataValueField = "SourceID";
            ddlHowSourcedEnquiry.DataBind();
            ddlHowSourcedEnquiry.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPGroupName(DropDownList ddlRFPGroupName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPGroupName";
            DAL.GetDataset(c, ref ds);

            ddlRFPGroupName.DataSource = ds.Tables[0];
            ddlRFPGroupName.DataTextField = "RFPGroupName";
            ddlRFPGroupName.DataValueField = "RFPGroupID";
            ddlRFPGroupName.DataBind();
            ddlRFPGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryLocation(DropDownList ddlEnquiryLocation)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryLocation";
            DAL.GetDataset(c, ref ds);

            ddlEnquiryLocation.DataSource = ds.Tables[0];
            ddlEnquiryLocation.DataTextField = "LocationName";
            ddlEnquiryLocation.DataValueField = "LocationID";
            ddlEnquiryLocation.DataBind();
            ddlEnquiryLocation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProductName(DropDownList ddlProductName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductNameDetails";
            DAL.GetDataset(c, ref ds);

            ddlProductName.DataSource = ds.Tables[0];
            ddlProductName.DataTextField = "ProductName";
            ddlProductName.DataValueField = "ProductID";
            ddlProductName.DataBind();
            ddlProductName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetDispatchedOfferDetailsByEnquiryNumber(DropDownList ddlOffer)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDispatchedOfferDetailsByEnquiryNumber";
            c.Parameters.AddWithValue("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);

            ddlOffer.DataSource = ds.Tables[0];
            ddlOffer.DataTextField = "OfferNo";
            ddlOffer.DataValueField = "EODID";
            ddlOffer.DataBind();
            ddlOffer.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPDetailsByPOHID(DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByPOHID";
            c.Parameters.AddWithValue("@POHID", POHID);
            DAL.GetDataset(c, ref ds);

            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOfferName(DropDownList ddlOfferName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferName";
            DAL.GetDataset(c, ref ds);

            ddlOfferName.DataSource = ds.Tables[0];
            ddlOfferName.DataTextField = "OfferName";
            ddlOfferName.DataValueField = "ID";
            ddlOfferName.DataBind();
            ddlOfferName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetSalesAttachementTypeName(DropDownList ddlTypename)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesAttachementTypeName";
            DAL.GetDataset(c, ref ds);

            ddlTypename.DataSource = ds.Tables[0];
            ddlTypename.DataTextField = "TypeName";
            ddlTypename.DataValueField = "SATID";
            ddlTypename.DataBind();
            ddlTypename.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOfferNoDetailsByEnquiryNumber(DropDownList ddlOfferNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferNoDetailsByEnquiryNumber";
            c.Parameters.AddWithValue("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);

            ddlOfferNo.DataSource = ds.Tables[0];
            ddlOfferNo.DataTextField = "OfferNo";
            ddlOfferNo.DataValueField = "EODID";
            ddlOfferNo.DataBind();
            ddlOfferNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerNameByUserIDAndUnRaisedPO(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerNameByEmployeeIDAndUnraisedPO";
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
            ddlCustomerName.DataSource = ds.Tables[0];
            ddlCustomerName.DataTextField = "ProspectName";
            ddlCustomerName.DataValueField = "ProspectID";
            ddlCustomerName.DataBind();
            ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryNumberByUserIDAndUnRaisedPO(int UserID, DropDownList ddlEnquiryNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDByUserIDAndUnraisedPO";
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


    public DataSet GetOfferStatusType(RadioButtonList rblOfferType)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferStatusType";
            DAL.GetDataset(c, ref ds);
            rblOfferType.DataSource = ds.Tables[0];
            rblOfferType.DataTextField = "TypeName";
            rblOfferType.DataValueField = "OSTID";
            rblOfferType.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOrderLostDetails(DropDownList ddlOfferLost)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOrderLostDetails";
            DAL.GetDataset(c, ref ds);
            ddlOfferLost.DataSource = ds.Tables[0];
            ddlOfferLost.DataTextField = "Name";
            ddlOfferLost.DataValueField = "OLDID";
            ddlOfferLost.DataBind();
            ddlOfferLost.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDailyActivitiesSalesEnquiryDetailsByEmployeeID(int EmployeeID, DropDownList ddlEnquiryNumber, DropDownList ddlprospect)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDailyActivitiesSalesEnquiryNumberByEmployeeID";
            c.Parameters.Add("@EmployeeID", EmployeeID);
            DAL.GetDataset(c, ref ds);

            ddlEnquiryNumber.DataSource = ds.Tables[0];
            ddlEnquiryNumber.DataTextField = "EnquiryNumber";
            ddlEnquiryNumber.DataValueField = "EnquiryID";
            ddlEnquiryNumber.DataBind();
            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlprospect.DataSource = ds.Tables[0];
            ddlprospect.DataTextField = "ProspectName";
            ddlprospect.DataValueField = "ProspectID";
            ddlprospect.DataBind();
            ddlprospect.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPheaderDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPHeaderDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteCustomerPOHeaderByPOHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCustomerPOHeaderByPOHID";
            c.Parameters.Add("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerPODetailsByPaymentStatusUpdate(DropDownList ddlPONo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerPODetailsByPaymentStatusUpdate";
            DAL.GetDataset(c, ref ds);

            ddlPONo.DataSource = ds.Tables[0];
            ddlPONo.DataTextField = "ProspectName";
            ddlPONo.DataValueField = "ProspectID";
            ddlPONo.DataBind();
            ddlPONo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceDetailsByPOHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            if (InvoiceRFPHID == "G")
                c.Parameters.Add("@RFPHID", DBNull.Value);
            else
                c.Parameters.Add("@RFPHID", InvoiceRFPHID);
            c.Parameters.Add("@InVoiceType", InVoiceType);
            c.CommandText = "LS_GetInvoiceDetailsByPOHID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPaymentModeDetails(DropDownList ddlPaymentMode)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_BankAccountDetails";
            DAL.GetDataset(c, ref ds);

            ddlPaymentMode.DataSource = ds.Tables[0];
            ddlPaymentMode.DataTextField = "AccountName";
            ddlPaymentMode.DataValueField = "BADID";
            ddlPaymentMode.DataBind();
            ddlPaymentMode.Items.Insert(0, new ListItem("--Select--", "0"));
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
            c.Parameters.Add("@InvoiceNo", InvoiceNo);
            if (InVoiceType == "1")
                c.Parameters.Add("@RFPHID", InvoiceRFPHID);
            else
                c.Parameters.Add("@RFPHID", DBNull.Value);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@InvoiceDate", InvoiceDate);
            c.Parameters.Add("@LocationID", LocationID);
            c.Parameters.Add("@InvoiceValue", InvoiceValue);
            c.Parameters.Add("@BasicValue", BasicValue);
            c.Parameters.Add("@InVoiceType", InVoiceType);
            c.Parameters.Add("@PaymentRemarks", PaymentRemarks);
            c.Parameters.Add("@IPDID", IPDID);
            c.Parameters.Add("@IDID", IDID);
            c.Parameters.Add("@CompanyID", CompanyID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePaymentCollectionDetails(string type)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCollectionDetails";
            c.Parameters.Add("@PaymentMode", PaymentMode);
            if (InvoiceRFPHID == "G")
                c.Parameters.Add("@RFPHID", DBNull.Value);
            else
                c.Parameters.Add("@RFPHID", InvoiceRFPHID);
            c.Parameters.Add("@CollectionAmount", CollectionAmount);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            if (type == "Domestic")
            {
                c.Parameters.Add("@TDSDeducted", TDSDeducted);
                c.Parameters.Add("@currencyExchangerate", DBNull.Value);
            }
            else
            {
                c.Parameters.Add("@TDSDeducted", DBNull.Value);
                c.Parameters.Add("@currencyExchangerate", currencyExchangerate);
            }
            c.Parameters.Add("@IDID", IDID);
            c.Parameters.Add("@InVoiceType", type);
            c.Parameters.Add("@RefNo", RefNo);
            c.Parameters.Add("@PaymentDate", PaymentDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNODetails(DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoDetailsByInvoicePaymentStatusUpdate";
            DAL.GetDataset(c, ref ds);

            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindInvoiceNo(DropDownList ddlInvoiceNo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPInvoiceNoDetails";
            if (InvoiceRFPHID == "G")
                c.Parameters.Add("@RFPHID", DBNull.Value);
            else
                c.Parameters.Add("@RFPHID", InvoiceRFPHID);
            c.Parameters.Add("@InVoiceType", InVoiceType);
            DAL.GetDataset(c, ref ds);

            ddlInvoiceNo.DataSource = ds.Tables[0];
            ddlInvoiceNo.DataTextField = "InvoiceNo";
            ddlInvoiceNo.DataValueField = "IDID";
            ddlInvoiceNo.DataBind();
            ddlInvoiceNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceAmountDetailsbyInvoiceID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceAmountDetailsByInvoiceID";
            c.Parameters.Add("@IDID", IDID);
            if (InvoiceRFPHID == "G")
                c.Parameters.Add("@RFPHID", DBNull.Value);
            else
                c.Parameters.Add("@RFPHID", InvoiceRFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceNoListDetailsByRFPHID(DropDownList ddlAddInvoiceNo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceNoListDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);

            ddlAddInvoiceNo.DataSource = ds.Tables[0];
            ddlAddInvoiceNo.DataTextField = "InvoiceNo";
            ddlAddInvoiceNo.DataValueField = "IDID";
            ddlAddInvoiceNo.DataBind();
            ddlAddInvoiceNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInvoiceNoDetailsByPCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateInvoiceNoDetailsByPCDID";
            c.Parameters.Add("@PCDID", PCDID);
            c.Parameters.Add("@IDID", IDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteInvoiceDetailsByIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteInvoiceDetailsByIDID";
            c.Parameters.Add("@IDID", IDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteCollectionPaymentDetailsByPCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCollectionPaymentDetailsByPCDID";
            c.Parameters.Add("@PCDID", PCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceTypeName(DropDownList ddlInvoiceType)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceTypeNameDetails";
            DAL.GetDataset(c, ref ds);

            ddlInvoiceType.DataSource = ds.Tables[0];
            ddlInvoiceType.DataTextField = "TypeName";
            ddlInvoiceType.DataValueField = "ITID";
            ddlInvoiceType.DataBind();
            ddlInvoiceType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerNameByRFPHIDPaymentStatusUpdate()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPHID", InvoiceRFPHID);
            c.CommandText = "LS_GetCustomerNameByRFPHIDForPaymentStatusUpdate";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindSalaesEmployeeName(DropDownList ddlEmpName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesDepartmentEmployeeNameList";
            DAL.GetDataset(c, ref ds);

            ddlEmpName.DataSource = ds.Tables[0];
            ddlEmpName.DataTextField = "EmployeeName";
            ddlEmpName.DataValueField = "EmployeeID";
            ddlEmpName.DataBind();
            ddlEmpName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSalesRevenueTargetValuesDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@EmployeeID", EmployeeID);
            c.Parameters.Add("@TargetValue", TargetValue);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.CommandText = "LS_SaveSalesEmployeeRevenueTargetValueDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteSalesRevenueDetailsByFYID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@SEFYTG", SEFYTG);
            c.CommandText = "LS_DeleteRevenueDetailsBySEFYTG";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetFinanceYearDetails(DropDownList ddlFinanceYear)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetFinanceYaer";
            DAL.GetDataset(c, ref ds);

            ddlFinanceYear.DataSource = ds.Tables[0];
            ddlFinanceYear.DataTextField = "FinanceYear";
            ddlFinanceYear.DataValueField = "FYID";
            ddlFinanceYear.DataBind();
            ddlFinanceYear.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSalesKPIReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@FYID", FYID);
            c.Parameters.Add("@EmployeeID", EmployeeID);
            c.CommandText = "LS_GetSalesKPIReports_OverAll";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPaymentDays(DropDownList ddlPaymentDays)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoicePaymentDays";
            DAL.GetDataset(c, ref ds);

            ddlPaymentDays.DataSource = ds.Tables[0];
            ddlPaymentDays.DataTextField = "PaymentDays";
            ddlPaymentDays.DataValueField = "IPDID";
            ddlPaymentDays.DataBind();
            ddlPaymentDays.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInvoiceDetailsByIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@IDID", IDID);
            c.CommandText = "LS_GetInvoiceDetailsByIDID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveEnquiryPurchasePriceEstimation()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@EPPEID", EPPEID);
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@Description", Description);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.CommandText = "LS_SaveEnquiryPurchasePriceEstimation";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryNoDetails(DropDownList ddlEnquiryNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryNoDetailsByPriceEstimation";
            DAL.GetDataset(c, ref ds);

            ddlEnquiryNo.DataSource = ds.Tables[0];
            ddlEnquiryNo.DataTextField = "CustomerEnquiryNumber";
            ddlEnquiryNo.DataValueField = "EnquiryID";
            ddlEnquiryNo.DataBind();
            ddlEnquiryNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerNameDetails(DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerNameDetailsByPriceEstimation";
            DAL.GetDataset(c, ref ds);

            ddlCustomerName.DataSource = ds.Tables[0];
            ddlCustomerName.DataTextField = "ProspectName";
            ddlCustomerName.DataValueField = "ProspectID";
            ddlCustomerName.DataBind();
            ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPriceEstimationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPriceEstimationDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPriceEstimationDetailsbyEPPEID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@EPPEID", EPPEID);
            c.CommandText = "LS_GetPriceEstimationDetailsByEPPEID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeletePriceEstimationDetailsByEPPEID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@EPPEID", EPPEID);
            c.CommandText = "LS_DeletePriceEstimationDetailsByEPPEID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveRFPDocumentArchiveDetails()
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@EnquiryID", EnquiryID);
            c.Parameters.Add("@EODID", EODID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@DepartmentID", DepartmentID);
            c.Parameters.Add("@FileName", FileName);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.CommandText = "LS_SaveRFPDocumentArchiveDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPDocumentArchiveDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDocumentArchiveDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteRFPDocumentArchiveDetailsByRFPDAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteRFPDocumentArchiveDetailsBYRFPDAID";
            c.Parameters.Add("@RFPDAID", RFPDAID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
		    public DataSet getEnquiryProcessDetailsnew()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            DAL.GetDataset("LS_GetCustomerEnquiryHeaderDetails_new", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet BindInvoiceNoForPayment(DropDownList ddlInvoiceNo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPInvoiceNoDetailsForPayment";
            DAL.GetDataset(c, ref ds);

            ddlInvoiceNo.DataSource = ds.Tables[0];
            ddlInvoiceNo.DataTextField = "InvoiceNo";
            ddlInvoiceNo.DataValueField = "IDID";
            ddlInvoiceNo.DataBind();
            ddlInvoiceNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet GetInvoiceAmountDetailsbyInvoiceIDPayment()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceAmountDetailsbyInvoiceIDPayment";
            c.Parameters.Add("@IDID", IDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
    public DataSet SavePaymentCollectionDetailsPayment(string type)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCollectionDetailsPayment";
            c.Parameters.Add("@PaymentMode", PaymentMode);
            c.Parameters.Add("@CollectionAmount", CollectionAmount);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            if (type == "Domestic")
            {
                c.Parameters.Add("@TDSDeducted", TDSDeducted);
                c.Parameters.Add("@currencyExchangerate", DBNull.Value);
            }
            else
            {
                c.Parameters.Add("@TDSDeducted", DBNull.Value);
                c.Parameters.Add("@currencyExchangerate", currencyExchangerate);
            }
            c.Parameters.Add("@IDID", IDID);
            c.Parameters.Add("@InVoiceType", type);
            c.Parameters.Add("@RefNo", RefNo);
            c.Parameters.Add("@PaymentDate", PaymentDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet BindInvoiceDatas()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInvoiceDetailsByPayment";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	  public DataSet getEnquiryProcessDetails_Old()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerEnquiryHeaderDetails_OldEnquiry";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

}

