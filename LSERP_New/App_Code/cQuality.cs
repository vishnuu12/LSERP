using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using eplus.data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for cQuality
/// </summary>
public class cQuality
{
    #region "Declaration"

    DataSet ds = new DataSet();
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    cSession objSession = new cSession();
    public DateTime DueOn;
    public string customerpono;
    public string tagNo;
    public string other;
    public string WPRHID;

    #endregion

    #region"Properties"

    public int MID { get; set; }
    public int STID { get; set; }
    public string Sequence { get; set; }

    public string Stage { get; set; }
    public string TypeCheck { get; set; }

    public int QPHID { get; set; }
    public int QPDID { get; set; }

    public int BOMID { get; set; }

    public int LSIQCAccepted { get; set; }
    public int LSIQCApplicable { get; set; }

    public int ClientTPIAAccepted { get; set; }
    public int ClientTPIAApplicable { get; set; }

    public int DocumentMandatory { get; set; }

    public int RFPDID { get; set; }
    public int RFPHID { get; set; }
    public int RFPQPDID { get; set; }
    public int EDID { get; set; }

    public int BOMID1 { get; set; }
    public int BOMID2 { get; set; }
    public int WPSID { get; set; }

    public string Remarks { get; set; }
    public int PAPDID { get; set; }
    public int PRIDID { get; set; }
    public int PRPDID1 { get; set; }
    public int PRPDID2 { get; set; }

    public DataTable dt { get; set; }

    public int CMID { get; set; }
    public int CTDID { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string JobOrderRemarks { get; set; }
    public string AssemplyJobOrderID { get; set; }

    public int MIDID { get; set; }
    public int SPODID { get; set; }
    public int GRNIDID  { get; set; }

    public int LOcationID { get; set; }


    //LPIReport 

    public string LPIRID { get; set; }
    public string ControlOfRecords { get; set; }
    public DateTime ReportDate { get; set; }
    public string JobSize { get; set; }
    public string IfSpecifyItemName { get; set; }
    public string ProjectName { get; set; }
    public string BellowSNo { get; set; }
    public string StageOfTest { get; set; }
    public string DrawingNo { get; set; }
    public DateTime TestDate { get; set; }
    public string MaterialSpecification { get; set; }
    public string OtherReference { get; set; }
    public string PenetrantBrandID { get; set; }
    public string PenetrantBatchNo { get; set; }
    public string Thickness { get; set; }
    public string CleanRemoverBrandID { get; set; }
    public string CleanRemoverBatchNo { get; set; }
    public string ProcedureAndRevNo { get; set; }
    public string DwellTime { get; set; }
    public string SurfaceCondition { get; set; }
    public string DeveloperBrandID { get; set; }
    public string DeveloperBatchNo { get; set; }
    public string SurfaceTemprature { get; set; }
    public string DevelopementTime { get; set; }
    public string PenetrateSystem { get; set; }
    public string LightningEquipment { get; set; }
    public string Technique { get; set; }
    public string LightIntensity { get; set; }
    public string SheetOfIndications { get; set; }
    public string InspectionQty { get; set; }
    public string AcceptedQty { get; set; }
    public string UserID { get; set; }



    //Visual Examnination Report
    public string VERID { get; set; }
    public string Customername { get; set; }
    public string JobDescription { get; set; }
    public string Material { get; set; }
    public string AcceptanceCriteria { get; set; }
    public string EquipmentUsed { get; set; }
    public string QtmOfInspection { get; set; }
    public string BSLNo { get; set; }
    public string PreparedName { get; set; }
    public string PreparedLevel { get; set; }
    public string EvaluvatedName { get; set; }
    public string EvaluvatedDesignation { get; set; }
    public string CustomerTPIAName { get; set; }
    public string CustomerTPIADesignation { get; set; }
    public string AIName { get; set; }
    public string AIDesignation { get; set; }
    public string CreatedBy { get; set; }

    //DTReport
    public string DTRID { get; set; }
    public string PONo { get; set; }
    public string QAPNo { get; set; }
    public string ItemQty { get; set; }
    public string Size { get; set; }
    public string RequiredMoveMent { get; set; }
    public string Actual { get; set; }

    //Heat Treatment Cycle report

    public string HTCRID { get; set; }
    public string EQUIPNo { get; set; }
    public string MethodOfHeatTreatment { get; set; }
    public string JobSizeAndMaterial { get; set; }
    public string TypeOfHeatTreatment { get; set; }
    public string LoadingTemprature { get; set; }
    public string RateOfHeading { get; set; }
    public string SoakingTemprature { get; set; }
    public string SoakingTimeDuration { get; set; }
    public string RateOfCooling { get; set; }
    public string UnLoadinfTemprature { get; set; }
    public string MethodOfThermoCoubleAttachement { get; set; }
    public string MethodOfThermoCoubleRemoval { get; set; }
    public string MethodOfSupport { get; set; }
    public string Identification { get; set; }
    public string ThermocoubleLOcationSketch { get; set; }
    public string Note { get; set; }
    public string PrefaredEnginearname { get; set; }
    public string QualityInchargename { get; set; }

    //MT report

    public string MPERID { get; set; }
    public string RFPNo { get; set; }
    public string MagneticParticle { get; set; }
    public string BrandNameAndColor { get; set; }
    public string TempratureOfThePart { get; set; }
    public string Equipment { get; set; }
    public string LightingEquipment { get; set; }

    public string LightIntensityMethod { get; set; }
    public string CurrentUsed { get; set; }
    public string TestSensitivity { get; set; }
    public string AcceptanceStandard { get; set; }
    public string RecordingOfINdication { get; set; }
    public string PerformedName { get; set; }
    public string PerformedLevel { get; set; }
    public string InspectionAuthorityName { get; set; }
    public string InspectionAuthorityDesignation { get; set; }

    //Ultra Sonic Examination Report

    public string USERID { get; set; }
    public string CalibrationBlock { get; set; }
    public string ReferenceBlock { get; set; }
    public string CalibrationRangeNormal { get; set; }
    public string CalibrationRangeAngle { get; set; }
    public string SearchUnitNormal { get; set; }
    public string SearchUnitAngle { get; set; }
    public string Model { get; set; }
    public string SizeAndIdentification { get; set; }
    public string SerielNo { get; set; }
    public string Frequency { get; set; }
    public string GainReference { get; set; }
    public string GainScanning { get; set; }
    public string ExtentOfTest { get; set; }
    public string Coublent { get; set; }
    public string SearchUnitCableLength { get; set; }
    public string RejectionLevel { get; set; }
    public string ScanningSketch { get; set; }


    public string AttachementName { get; set; }
    public string CertficateNo { get; set; }
    public DateTime ReceivedDate { get; set; }
    public int MIQCDID { get; set; }

    public int CFID { get; set; }

    public Decimal Quantity { get; set; }
    public string MaterialQtyStatus { get; set; }
    public string MTR { get; set; }
    public string TPSNo { get; set; }
    public string Visual { get; set; }
    public string CheckTest { get; set; }
    public string AddtionalRequirtment { get; set; }
    public string MeasuredDimension { get; set; }
    public string OriginalMarking { get; set; }

    public Decimal ReworkedQty { get; set; }
    public string ReworkedQtyStatus { get; set; }


    //RadioGraphic Examination Sheet

    public string RGETSID { get; set; }
    public string ItemName { get; set; }
    public string ItemSerielNo { get; set; }
    public string NoOfExposures { get; set; }
    public string IQI { get; set; }
    public string IQIPlacement { get; set; }
    public string LeadScreensFront { get; set; }
    public string LeadScreensBack { get; set; }
    public string Film { get; set; }
    public string BaseMaterial { get; set; }
    public string BaseMaterialSizeAndThickness { get; set; }
    public string PlacementOfBackScatter { get; set; }
    public string weldthickness { get; set; }
    public string Sensitivity { get; set; }
    public string WeldReinforcementthickness { get; set; }
    public string NoofFilmsFilmCassette { get; set; }
    public string Density { get; set; }
    public string DevelopingTempratureAndTime { get; set; }
    public string XRayKV { get; set; }
    public string XRayMa { get; set; }
    public string XRayTime { get; set; }
    public string XRayFSSize { get; set; }
    public string XRayModel { get; set; }
    public string XRaySrNo { get; set; }
    public string MinimumFocustoobjectdistance { get; set; }
    public string GameRaySource { get; set; }
    public string GameRayStrength { get; set; }
    public string GameRaySize { get; set; }
    public string MaximumSourceSideOftoobjectToFilmdistance { get; set; }
    public string ShootingSketch { get; set; }
    public string PreparedBy { get; set; }
    public DateTime PreparedDate { get; set; }
    public string ReveiwedBy { get; set; }
    public DateTime ReveiwedDate { get; set; }

    //Radio Graphic Examination Report

    public string SODFOD { get; set; }
    public string FilmManufacture { get; set; }
    public string SSOFD { get; set; }
    public string FilmTypeDesignation { get; set; }
    public string FocalSpot { get; set; }
    public string SizeOfBLetter { get; set; }
    public string PlacingOfBLetter { get; set; }
    public string EvaluvationOfBLettter { get; set; }
    public string RTPerformer { get; set; }
    public string LSEPreparedBy { get; set; }
    public DateTime LSEPreparedDate { get; set; }
    public string LSEPreapredLevel { get; set; }
    public string LSEReviewedBy { get; set; }
    public DateTime LSEReviewdDate { get; set; }
    public string LSEReviewedDesignation { get; set; }
    public string CustomerReviwedBy { get; set; }
    public DateTime CustomerReviwedDate { get; set; }
    public string CustomerDesignation { get; set; }
    public string AIReveiwedBy { get; set; }
    public DateTime AIReviewedDate { get; set; }
    public string TechniqueSheetReferenceNo { get; set; }
    public string ExposureTechnique { get; set; }
    public string StageOfInspection { get; set; }
    public string Viewingtechnique { get; set; }
    public string RGERID { get; set; }

    //PMI 
    public string PMIRID { get; set; }
    public string WTRID { get; set; }

    public string PPIRID { get; set; }

    public string observed { get; set; }
    public string Packing { get; set; }
    public string Marking { get; set; }

    public string MTRID { get; set; }

    public DataTable dtDimension { get; set; }

    public string TotalAverage { get; set; }
    public string DrawingSpringRateValue { get; set; }
    public string SpringRateActualGraphValue { get; set; }
    public string Percentage { get; set; }

    public string SRMTRID { get; set; }
    public string InstrumentRefNo { get; set; }
    public string CalreportNo { get; set; }
    public DateTime DoneOn { get; set; }
    public string AWQPDID { get; set; }

    public string FAQPDID { get; set; }

    public string CertificateQCStatus { get; set; }

    public string InstrumentRefNo1 { get; set; }
    public string CalreportNo1 { get; set; }
    public DateTime DoneOn1 { get; set; }
    public DateTime DueOn1 { get; set; }
    public string PlanningType { get; set; }
    public int WOIHID { get; set; }
    public string Status { get; set; }
    public string WOQCLMID { get; set; }
    public int WOIQCDID { get; set; }

    public int DIRDID { get; set; }
    public string ITPNo { get; set; }
    public string FTRHID { get; set; }
    public string EILSpecification { get; set; }
    public DateTime DateOfCal { get; set; }
    public DateTime DueForcal { get; set; }
    public string Instrument { get; set; }
    public string ReportReferenceNo { get; set; }
    public string DRGPartNo { get; set; }
    public string RMIRHID { get; set; }
    public string CustomerName { get; set; }
    public string Other { get; set; }
    public string DevelopementTemprature { get; set; }
    public string PTRHID { get; set; }
    public string ApprovedTestProcedureNo { get; set; }
    public string TypeOfTest { get; set; }
    public string Medium { get; set; }
    public string TestPressure { get; set; }
    public string HoldingTime { get; set; }
    public string DesignPressure { get; set; }
    public string TestTemprature { get; set; }
    public string CodeNo { get; set; }
    public string Range { get; set; }
    public string CalibrationRef { get; set; }
    public DateTime CalibrationDoneOn { get; set; }
    public DateTime CalibrationDueOn { get; set; }
    public string Result { get; set; }
    public string TestProcedureNo { get; set; }
    public string CustomerSpecification { get; set; }
    public string MethodOfCleaning { get; set; }
    public string VisualInspection { get; set; }
    public string PIRHID { get; set; }
    public string WorkOrderNo { get; set; }
    public string Part { get; set; }

    public DateTime DatePC { get; set; }
    public string StartTimePC { get; set; }
    public string CompletionTimePC { get; set; }
    public string ProcedureAndResultsPC { get; set; }
    public DateTime DateAPC { get; set; }
    public string StartTimeAPC { get; set; }
    public string CompletionTimeAPC { get; set; }
    public string MethodologyAPC { get; set; }
    public string ProcedureAndResultsAPC { get; set; }
    public DateTime DatePAC { get; set; }
    public string StartTimePAC { get; set; }
    public string CompletionTimePAC { get; set; }
    public string MethodologyPAC { get; set; }
    public string ProcedureAndResultsPAC { get; set; }
    public string Results { get; set; }
    public string PPRHID { get; set; }
    public string PECPLTDCNo { get; set; }
    public string Type { get; set; }
    public string HTRHID { get; set; }
    public string Inspection { get; set; }
    public string WeldPlan { get; set; }
    public string PartName1 { get; set; }
    public string PartName2 { get; set; }
    public string PMI { get; set; }

    public int JCHID { get; set; }
    public string StageInspection { get; set; }
    public string RaisedBy { get; set; }
    public int PMID { get; set; }
    public string mrnchangestatus { get; set; }
    public string AssemplyStatus { get; set; }
    public string AttachementDescription { get; set; }
    public string AttachementID { get; set; }
    public string QCDimensionName { get; set; }
	
	
	
    public string SPODIDS { get; set; }
    public string MIDIDS { get; set; }
    public string MRNNumbers { get; set; }
    public string MRNIDS { get; set; }
    public int employeeid { get; set; }


    #endregion

    #region"Common Methods"

    public DataSet GetQualityPlanningHeaderDetailsByMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQualityPlanningHeaderDetailsByMID";
            c.Parameters.AddWithValue("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveQualityPlanningHeaderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQualityPlanningHeaderDetails";
            c.Parameters.AddWithValue("@QPHID", QPHID);
            c.Parameters.AddWithValue("@sequence", Sequence);
            c.Parameters.AddWithValue("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveQPDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQPDetails";
            c.Parameters.AddWithValue("@QPHID", QPHID);
            c.Parameters.AddWithValue("@QPDID", QPDID);
            c.Parameters.AddWithValue("@Stage", Stage);
            c.Parameters.AddWithValue("@TypeCheck", TypeCheck);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQPDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQPDetailsByQPHID";
            c.Parameters.AddWithValue("@QPHID", QPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteQPHeader()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteQPHeaderDetails";
            c.Parameters.AddWithValue("@QPHID", QPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteQPDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteQPDetails";
            c.Parameters.AddWithValue("@QPDID", QPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQualityPlanningDetailsByBOMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQualityPlanningDetailsByBOMID";
            c.Parameters.AddWithValue("@BOMID", BOMID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@Stage", Stage);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveRFPQualityPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRFPQualityPlanningDetails";
            c.Parameters.AddWithValue("@RFPQPDID", RFPQPDID);
            c.Parameters.AddWithValue("@QPDID", QPDID);
            c.Parameters.AddWithValue("@BOMID", BOMID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@LSIQCApplicable", LSIQCApplicable);
            c.Parameters.AddWithValue("@WPSID", WPSID);
            //   c.Parameters.AddWithValue("@LSIQCAccepted", LSIQCAccepted);
            c.Parameters.AddWithValue("@ClientTPIAApplicable", ClientTPIAApplicable);
            // c.Parameters.AddWithValue("@ClientTPIAAccepted", ClientTPIAAccepted);
            c.Parameters.AddWithValue("@DocumentMandatory", DocumentMandatory);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@Stage", Stage);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateQPStatusByRFPDIDAndEDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpDateQPStatusByRFPDID";
            c.Parameters.AddWithValue("@EDID", EDID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateRFPQPStatusByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateRFPQPStatus";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePartAssemplyPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartAssemplyPlanningDetails";
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@PAPDID", PAPDID);
            c.Parameters.AddWithValue("@PRPDID1", PRPDID1);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@PRPDID2", PRPDID2);
            c.Parameters.AddWithValue("@WPSID", WPSID);
            c.Parameters.AddWithValue("@BOMID1", BOMID1);
            c.Parameters.AddWithValue("@BOMID2", BOMID2);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            if (AWQPDID == "")
                c.Parameters.AddWithValue("@AWQPDID", DBNull.Value);
            else
                c.Parameters.AddWithValue("@AWQPDID", AWQPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartAssemplyPlanningDetailsByRFPDID(DropDownList ddlWPSNumber)
    {
        DataSet ds = new DataSet();
        cProduction objP = new cProduction();
        try
        {
            objP.GetWPSDetailsbyWPSNumber(ddlWPSNumber);
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyPlanningDetailsByRFPDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            // c.Parameters.AddWithValue("@PRIDID", PRIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBlockedMRNDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBlockedMRNDetailsByRFPDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveQualityClearence()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQualityClearence";
            c.Parameters.AddWithValue("@tblQUalityClearence", dt);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet deletePartAssemplyPlanningDetailsByPAPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeletePartAssemplyPlanningDetailsByPAPDID";
            c.Parameters.AddWithValue("@PAPDID", PAPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateMRNBlockingQualityClearenceStatusbyRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateMRNBlockingQualityClearenceStatusbyRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetAssemplyPlanningDetailsbyRFPDIDAndPRIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetAssemplyPlanningDetailsByRFPDIDAndPRIDIDforAssemplyjobCard1";
            c.CommandText = "LS_GetAssemplyPlanningdetailsByRFPDIDAndPRIDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@PRIDID", PRIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardNameByPAPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardNameByPAPDID";
            c.Parameters.AddWithValue("@PAPDID", PAPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveMaterialInwardQualityClearebnce()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialInwardQualityClearanceHeaderDetails";
            c.Parameters.AddWithValue("@MIDID", MIDID);
            c.Parameters.AddWithValue("@MTR", MTR);
            c.Parameters.AddWithValue("@TPSNo", TPSNo);
            c.Parameters.AddWithValue("@Visual", Visual);
            c.Parameters.AddWithValue("@CheckTest", CheckTest);
            c.Parameters.AddWithValue("@AddtionalRequirtment", AddtionalRequirtment);
            c.Parameters.AddWithValue("@MeasuredDimension", MeasuredDimension);
            c.Parameters.AddWithValue("@OriginalMarking", OriginalMarking);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@LOcationID", LOcationID);
            c.Parameters.AddWithValue("@InstrumentRefNo", InstrumentRefNo);
            c.Parameters.AddWithValue("@CalreportNo", CalreportNo);
            c.Parameters.AddWithValue("@DoneOn", DoneOn);
            c.Parameters.AddWithValue("@DueOn", DueOn);

            c.Parameters.AddWithValue("@InstrumentRefNo1", InstrumentRefNo1);
            c.Parameters.AddWithValue("@CalreportNo1", CalreportNo1);
            c.Parameters.AddWithValue("@DoneOn1", DoneOn1);
            c.Parameters.AddWithValue("@DueOn1", DueOn1);

            c.Parameters.AddWithValue("@PMI", PMI);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCertficateDetailsBySPODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCertificateDetailsBySPODID";
            c.Parameters.AddWithValue("@SPODID", SPODID);
            c.Parameters.AddWithValue("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInwardedMaterialDetailsByLocationID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInwardedMaterialDetailsByLOcationID";
            c.Parameters.AddWithValue("@LocationID", LOcationID);
            c.Parameters.AddWithValue("@mrnchangestatus", mrnchangestatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	   public DataSet GetInwardedMaterialDetailsAllLocation()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInwardedMaterialDetailsAllLocation";
            c.Parameters.AddWithValue("@mrnchangestatus", mrnchangestatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetLPIReportDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLPIReportDetails";

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetTestReportDetailsByRFPHID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveLPIReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveLPIReportDetails";

            c.Parameters.AddWithValue("@RFPHID               ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID               ", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);

            c.Parameters.AddWithValue("@LPIRID               ", LPIRID);
            c.Parameters.AddWithValue("@ControlOfRecords     ", ControlOfRecords);
            c.Parameters.AddWithValue("@ReportDate           ", ReportDate);
            c.Parameters.AddWithValue("@JobSize              ", JobSize);
            c.Parameters.AddWithValue("@IfSpecifyItemName    ", IfSpecifyItemName);
            c.Parameters.AddWithValue("@ProjectName          ", ProjectName);
            c.Parameters.AddWithValue("@BellowSNo            ", BellowSNo);
            c.Parameters.AddWithValue("@StageOfTest          ", StageOfTest);
            c.Parameters.AddWithValue("@DrawingNo            ", DrawingNo);
            c.Parameters.AddWithValue("@TestDate             ", TestDate);
            c.Parameters.AddWithValue("@MaterialSpecification", MaterialSpecification);
            c.Parameters.AddWithValue("@OtherReference       ", OtherReference);
            c.Parameters.AddWithValue("@PenetrantBrandID     ", PenetrantBrandID);
            c.Parameters.AddWithValue("@PenetrantBatchNo     ", PenetrantBatchNo);
            c.Parameters.AddWithValue("@Thickness            ", Thickness);
            c.Parameters.AddWithValue("@CleanRemoverBrandID  ", CleanRemoverBrandID);
            c.Parameters.AddWithValue("@CleanRemoverBatchNo  ", CleanRemoverBatchNo);
            c.Parameters.AddWithValue("@ProcedureAndRevNo    ", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@DwellTime            ", DwellTime);
            c.Parameters.AddWithValue("@SurfaceCondition     ", SurfaceCondition);
            c.Parameters.AddWithValue("@DeveloperBrandID     ", DeveloperBrandID);
            c.Parameters.AddWithValue("@DeveloperBatchNo     ", DeveloperBatchNo);
            c.Parameters.AddWithValue("@SurfaceTemprature    ", SurfaceTemprature);
            c.Parameters.AddWithValue("@DevelopementTime     ", DevelopementTime);
            c.Parameters.AddWithValue("@PenetrateSystem      ", PenetrateSystem);
            c.Parameters.AddWithValue("@LightningEquipment   ", LightningEquipment);
            c.Parameters.AddWithValue("@Technique            ", Technique);
            c.Parameters.AddWithValue("@LightIntensity       ", LightIntensity);
            c.Parameters.AddWithValue("@SheetOfIndications   ", SheetOfIndications);
            c.Parameters.AddWithValue("@InspectionQty        ", InspectionQty);
            c.Parameters.AddWithValue("@AcceptedQty          ", AcceptedQty);
            c.Parameters.AddWithValue("@UserID", UserID);
            c.Parameters.AddWithValue("@tblLPIDetails", dt);

            c.Parameters.AddWithValue("@Customername", Customername);
            c.Parameters.AddWithValue("@RFPNo", RFPNo);
            c.Parameters.AddWithValue("@customerpono", customerpono);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteQualityTestReportDetailsByPrimaryID(string spname, string PrimaryID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@PrimaryID", PrimaryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    //RFPHID
    //RFPDID
    //VERID
    //ReportDate
    //Customername
    //DrawingNo
    //ProcedureAndRevNo
    //EquipmentUsed
    //LightIntensity
    //Technique
    //BSLNo
    //CreatedBy
    //IfSpecifyItemName
    //ITPNo
    //SurfaceCondition
    //AcceptedQty
    //ProjectName
    //PONo
    //RFPNo

    public DataSet SaveVisualExaminationReport()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveVisualExaminationReportHeader";

            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);

            c.Parameters.AddWithValue("@VERID", VERID);
            c.Parameters.AddWithValue("@ReportDate", ReportDate);
            c.Parameters.AddWithValue("@Customername", Customername);
            c.Parameters.AddWithValue("@DrawingNo", DrawingNo);
            c.Parameters.AddWithValue("@ProcedureAndRevNo", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@EquipmentUsed", EquipmentUsed);
            c.Parameters.AddWithValue("@LightIntensity", LightIntensity);
            c.Parameters.AddWithValue("@Technique", Technique);
            c.Parameters.AddWithValue("@BSLNo", BSLNo);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@IfSpecifyItemName", IfSpecifyItemName);
            c.Parameters.AddWithValue("@ITPNo", ITPNo);
            c.Parameters.AddWithValue("@SurfaceCondition", SurfaceCondition);
            c.Parameters.AddWithValue("@AcceptedQty", AcceptedQty);
            c.Parameters.AddWithValue("@ProjectName", ProjectName);
            c.Parameters.AddWithValue("@PONo", PONo);
            c.Parameters.AddWithValue("@RFPNo", RFPNo);

            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@other", other);

            c.Parameters.AddWithValue("@tblVERDetails", dt);

            //c.Parameters.AddWithValue("@RFPHID                   ", RFPHID);
            //c.Parameters.AddWithValue("@RFPDID                   ", RFPDID);
            //c.Parameters.AddWithValue("@VERID                    ", VERID);
            //c.Parameters.AddWithValue("@ReportDate               ", ReportDate);
            //c.Parameters.AddWithValue("@Customername             ", Customername);
            //c.Parameters.AddWithValue("@JobDescription           ", JobDescription);
            //c.Parameters.AddWithValue("@DrawingNo                ", DrawingNo);
            //c.Parameters.AddWithValue("@TestDate                 ", TestDate);
            //c.Parameters.AddWithValue("@Material                 ", Material);
            //c.Parameters.AddWithValue("@Thickness                ", Thickness);
            //c.Parameters.AddWithValue("@ProcedureAndRevNo        ", ProcedureAndRevNo);
            //c.Parameters.AddWithValue("@AcceptanceCriteria       ", AcceptanceCriteria);
            //c.Parameters.AddWithValue("@EquipmentUsed            ", EquipmentUsed);
            //c.Parameters.AddWithValue("@LightIntensity           ", LightIntensity);
            //c.Parameters.AddWithValue("@Technique                ", Technique);
            //c.Parameters.AddWithValue("@QtmOfInspection          ", QtmOfInspection);
            //c.Parameters.AddWithValue("@BSLNo                    ", BSLNo);
            //c.Parameters.AddWithValue("@PreparedName             ", PreparedName);
            //c.Parameters.AddWithValue("@PreparedLevel            ", PreparedLevel);
            //c.Parameters.AddWithValue("@EvaluvatedName           ", EvaluvatedName);
            //c.Parameters.AddWithValue("@EvaluvatedDesignation    ", EvaluvatedDesignation);
            //c.Parameters.AddWithValue("@CustomerTPIAName         ", CustomerTPIAName);
            //c.Parameters.AddWithValue("@CustomerTPIADesignation  ", CustomerTPIADesignation);
            //c.Parameters.AddWithValue("@AIName                   ", AIName);
            //c.Parameters.AddWithValue("@AIDesignation            ", AIDesignation);
            //c.Parameters.AddWithValue("@CreatedBy                ", CreatedBy);
            //c.Parameters.AddWithValue("@ItemName", IfSpecifyItemName);
            //c.Parameters.AddWithValue("@ControlOfRecords", ControlOfRecords);
            //c.Parameters.AddWithValue("@tblVERDetails", dt);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }




    public DataSet SaveDeflectionTestReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDeflectionTestReportDetails";
            c.Parameters.AddWithValue("@RFPHID ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID ", RFPDID);
            c.Parameters.AddWithValue("@DTRID ", DTRID);
            c.Parameters.AddWithValue("@ReportDate ", ReportDate);
            c.Parameters.AddWithValue("@ControlOfRecords", ControlOfRecords);
            c.Parameters.AddWithValue("@Customername ", Customername);
            c.Parameters.AddWithValue("@PONo ", PONo);
            c.Parameters.AddWithValue("@DrawingNo ", DrawingNo);
            c.Parameters.AddWithValue("@JobDescription ", JobDescription);
            c.Parameters.AddWithValue("@QAPNo ", QAPNo);
            c.Parameters.AddWithValue("@BellowSNo ", BellowSNo);
            c.Parameters.AddWithValue("@ItemQty ", ItemQty);
            c.Parameters.AddWithValue("@Size ", Size);
            c.Parameters.AddWithValue("@RequiredMoveMent", RequiredMoveMent);
            c.Parameters.AddWithValue("@Actual ", Actual);
            c.Parameters.AddWithValue("@Remarks ", Remarks);
            c.Parameters.AddWithValue("@CreatedBy ", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SavePositiveMaterialIdentificationReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePositiveMaterialIdentificationReportDetails";

            c.Parameters.AddWithValue("@RFPHID  ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID  ", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);
            c.Parameters.AddWithValue("@PMIRID  ", PMIRID);
            c.Parameters.AddWithValue("@ReportDate  ", ReportDate);
            c.Parameters.AddWithValue("@IfSpecifyItemName ", IfSpecifyItemName);
            c.Parameters.AddWithValue("@DrawingNo  ", DrawingNo);
            c.Parameters.AddWithValue("@JobDescription  ", JobDescription);
            c.Parameters.AddWithValue("@PONo  ", PONo);
            c.Parameters.AddWithValue("@ItemQty  ", ItemQty);
            c.Parameters.AddWithValue("@QAPNo  ", QAPNo);
            c.Parameters.AddWithValue("@BSLNo  ", BSLNo);
            c.Parameters.AddWithValue("@UserID  ", UserID);

            c.Parameters.AddWithValue("@ITPNo", ITPNo);
            c.Parameters.AddWithValue("@tagNo", tagNo);
            c.Parameters.AddWithValue("@EILSpecification", EILSpecification);
            c.Parameters.AddWithValue("@DateOfCal", DateOfCal);
            c.Parameters.AddWithValue("@DueForcal", DueForcal);
            c.Parameters.AddWithValue("@Instrument", Instrument);
            c.Parameters.AddWithValue("@ReportReferenceNo", ReportReferenceNo);
            c.Parameters.AddWithValue("@RFPNo", RFPNo);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@Customername", Customername);
            c.Parameters.AddWithValue("@ProjectName", ProjectName);
            c.Parameters.AddWithValue("@Size", Size);
            c.Parameters.AddWithValue("@DRGPartNo", DRGPartNo);

            c.Parameters.AddWithValue("@tblPMIPartDetails", dt);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetVERDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVERDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQualityTestReportNo(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveHeattreatmentCycleReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveHeatTreatmentCycleReportDetails";
            c.Parameters.AddWithValue("@RFPHID  ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID  ", RFPDID);

            c.Parameters.AddWithValue("@EDID", EDID);

            c.Parameters.AddWithValue("@HTCRID  ", HTCRID);
            c.Parameters.AddWithValue("@RFPNo", RFPNo);
            c.Parameters.AddWithValue("@ReportDate  ", ReportDate);
            c.Parameters.AddWithValue("@DrawingNo  ", DrawingNo);
            c.Parameters.AddWithValue("@ControlOfRecords  ", ControlOfRecords);
            c.Parameters.AddWithValue("@ItemQty  ", ItemQty);
            c.Parameters.AddWithValue("@EQUIPNo  ", EQUIPNo);
            c.Parameters.AddWithValue("@IfSpecifyItemName  ", IfSpecifyItemName);
            c.Parameters.AddWithValue("@MethodOfHeatTreatment  ", MethodOfHeatTreatment);
            c.Parameters.AddWithValue("@JobSizeAndMaterial  ", JobSizeAndMaterial);
            c.Parameters.AddWithValue("@TypeOfHeatTreatment  ", TypeOfHeatTreatment);
            c.Parameters.AddWithValue("@LoadingTemprature  ", LoadingTemprature);
            c.Parameters.AddWithValue("@RateOfHeading  ", RateOfHeading);
            c.Parameters.AddWithValue("@SoakingTemprature  ", SoakingTemprature);
            c.Parameters.AddWithValue("@SoakingTimeDuration  ", SoakingTimeDuration);
            c.Parameters.AddWithValue("@RateOfCooling  ", RateOfCooling);
            c.Parameters.AddWithValue("@UnLoadinfTemprature  ", UnLoadinfTemprature);
            c.Parameters.AddWithValue("@MethodOfThermoCoubleAttachement ", MethodOfThermoCoubleAttachement);
            c.Parameters.AddWithValue("@MethodOfThermoCoubleRemoval  ", MethodOfThermoCoubleRemoval);
            c.Parameters.AddWithValue("@MethodOfSupport  ", MethodOfSupport);
            c.Parameters.AddWithValue("@Identification  ", Identification);
            c.Parameters.AddWithValue("@ThermocoubleLOcationSketch  ", ThermocoubleLOcationSketch);
            c.Parameters.AddWithValue("@Note  ", Note);
            c.Parameters.AddWithValue("@PrefaredEnginearname  ", PrefaredEnginearname);
            c.Parameters.AddWithValue("@QualityInchargename  ", QualityInchargename);
            c.Parameters.AddWithValue("@CreatedBy  ", CreatedBy);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMTReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMTReportDetails";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@MPERID     ", MPERID);
            c.Parameters.AddWithValue("@ControlOfRecords", ControlOfRecords);
            c.Parameters.AddWithValue("@Customername", Customername);
            c.Parameters.AddWithValue("@ReportDate", ReportDate);
            c.Parameters.AddWithValue("@RFPNo", RFPNo);
            c.Parameters.AddWithValue("@PONo", PONo);
            c.Parameters.AddWithValue("@JobDescription", JobDescription);
            c.Parameters.AddWithValue("@DrawingNo", DrawingNo);
            c.Parameters.AddWithValue("@TestDate", TestDate);
            c.Parameters.AddWithValue("@Material", Material);
            c.Parameters.AddWithValue("@MagneticParticle", MagneticParticle);
            c.Parameters.AddWithValue("@Thickness", Thickness);
            c.Parameters.AddWithValue("@BrandNameAndColor", BrandNameAndColor);
            c.Parameters.AddWithValue("@ProcedureAndRevNo", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@TempratureOfThePart", TempratureOfThePart);
            c.Parameters.AddWithValue("@Equipment", Equipment);
            c.Parameters.AddWithValue("@LightingEquipment", LightingEquipment);
            c.Parameters.AddWithValue("@Technique", Technique);
            c.Parameters.AddWithValue("@LightIntensity", LightIntensity);
            c.Parameters.AddWithValue("@LightIntensityMethod", LightIntensityMethod);
            c.Parameters.AddWithValue("@CurrentUsed", CurrentUsed);
            c.Parameters.AddWithValue("@TestSensitivity", TestSensitivity);
            c.Parameters.AddWithValue("@AcceptanceStandard", AcceptanceStandard);
            c.Parameters.AddWithValue("@RecordingOfINdication", RecordingOfINdication);
            c.Parameters.AddWithValue("@PerformedName", PerformedName);
            c.Parameters.AddWithValue("@PerformedLevel", PerformedLevel);
            c.Parameters.AddWithValue("@EvaluvatedName", EvaluvatedName);
            c.Parameters.AddWithValue("@EvaluvatedDesignation", EvaluvatedDesignation);
            c.Parameters.AddWithValue("@InspectionAuthorityName", InspectionAuthorityName);
            c.Parameters.AddWithValue("@InspectionAuthorityDesignation", InspectionAuthorityDesignation);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@Itemname", IfSpecifyItemName);
            c.Parameters.AddWithValue("@tblMTReportDetails", dt);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveUltraSonicReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveUltraSonicReportDetails";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);
            c.Parameters.AddWithValue("@USERID", USERID);
            c.Parameters.AddWithValue("@ReportDate", ReportDate);
            c.Parameters.AddWithValue("@DrawingNo", DrawingNo);
            c.Parameters.AddWithValue("@TestDate", TestDate);
            c.Parameters.AddWithValue("@Material", Material);
            c.Parameters.AddWithValue("@ControlOfRecords   ", ControlOfRecords);
            c.Parameters.AddWithValue("@Customername   ", Customername);
            c.Parameters.AddWithValue("@JobDescription   ", JobDescription);
            c.Parameters.AddWithValue("@IfSpecifyItemName   ", IfSpecifyItemName);
            c.Parameters.AddWithValue("@CalibrationBlock   ", CalibrationBlock);
            c.Parameters.AddWithValue("@Thickness   ", Thickness);
            c.Parameters.AddWithValue("@ProcedureAndRevNo   ", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@ReferenceBlock   ", ReferenceBlock);
            c.Parameters.AddWithValue("@SurfaceCondition   ", SurfaceCondition);
            c.Parameters.AddWithValue("@CalibrationRangeNormal   ", CalibrationRangeNormal);
            c.Parameters.AddWithValue("@CalibrationRangeAngle   ", CalibrationRangeAngle);
            c.Parameters.AddWithValue("@Equipment   ", Equipment);
            c.Parameters.AddWithValue("@SearchUnitNormal   ", SearchUnitNormal);
            c.Parameters.AddWithValue("@SearchUnitAngle   ", SearchUnitAngle);
            c.Parameters.AddWithValue("@Model   ", Model);
            c.Parameters.AddWithValue("@SizeAndIdentification   ", SizeAndIdentification);
            c.Parameters.AddWithValue("@SerielNo   ", SerielNo);
            c.Parameters.AddWithValue("@Frequency   ", Frequency);
            c.Parameters.AddWithValue("@GainReference   ", GainReference);
            c.Parameters.AddWithValue("@GainScanning   ", GainScanning);
            c.Parameters.AddWithValue("@ExtentOfTest   ", ExtentOfTest);
            c.Parameters.AddWithValue("@Coublent   ", Coublent);
            c.Parameters.AddWithValue("@SearchUnitCableLength   ", SearchUnitCableLength);
            c.Parameters.AddWithValue("@RejectionLevel   ", RejectionLevel);
            c.Parameters.AddWithValue("@AcceptanceStandard   ", AcceptanceStandard);
            c.Parameters.AddWithValue("@ScanningSketch   ", ScanningSketch);
            c.Parameters.AddWithValue("@PerformedName   ", PerformedName);
            c.Parameters.AddWithValue("@PerformedLevel   ", PerformedLevel);
            c.Parameters.AddWithValue("@EvaluvatedName   ", EvaluvatedName);
            c.Parameters.AddWithValue("@EvaluvatedDesignation   ", EvaluvatedDesignation);
            c.Parameters.AddWithValue("@InspectionAuthorityName   ", InspectionAuthorityName);
            c.Parameters.AddWithValue("@InspectionAuthorityDesignation   ", InspectionAuthorityDesignation);
            c.Parameters.AddWithValue("@CreatedBy   ", CreatedBy);
            c.Parameters.AddWithValue("@tblUTReportDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveRadioGraphicExaminationTechniqueSheet()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRadioGraphicExaminationTechiqueSheet";
            c.Parameters.AddWithValue("@RFPHID                                       ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID                                       ", RFPDID);
            c.Parameters.AddWithValue("@ReportDate                                   ", ReportDate);
            c.Parameters.AddWithValue("@Customername                                 ", Customername);
            c.Parameters.AddWithValue("@ControlOfRecords   ", ControlOfRecords);
            c.Parameters.AddWithValue("@DrawingNo                                    ", DrawingNo);
            c.Parameters.AddWithValue("@JobDescription                               ", JobDescription);
            c.Parameters.AddWithValue("@Technique                                    ", Technique);
            c.Parameters.AddWithValue("@ProcedureAndRevNo                            ", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@SurfaceCondition                             ", SurfaceCondition);
            c.Parameters.AddWithValue("@RGETSID                                      ", RGETSID);
            c.Parameters.AddWithValue("@ItemName                                     ", ItemName);
            c.Parameters.AddWithValue("@ItemSerielNo                                 ", ItemSerielNo);
            c.Parameters.AddWithValue("@NoOfExposures                                ", NoOfExposures);
            c.Parameters.AddWithValue("@IQI                                          ", IQI);
            c.Parameters.AddWithValue("@IQIPlacement                                 ", IQIPlacement);
            c.Parameters.AddWithValue("@LeadScreensFront                             ", LeadScreensFront);
            c.Parameters.AddWithValue("@LeadScreensBack                              ", LeadScreensBack);
            c.Parameters.AddWithValue("@Film                                         ", Film);
            c.Parameters.AddWithValue("@BaseMaterial                                 ", BaseMaterial);
            c.Parameters.AddWithValue("@BaseMaterialSizeAndThickness                 ", BaseMaterialSizeAndThickness);
            c.Parameters.AddWithValue("@PlacementOfBackScatter                       ", PlacementOfBackScatter);
            c.Parameters.AddWithValue("@weldthickness                                ", weldthickness);
            c.Parameters.AddWithValue("@Sensitivity                                  ", Sensitivity);
            c.Parameters.AddWithValue("@WeldReinforcementthickness                   ", WeldReinforcementthickness);
            c.Parameters.AddWithValue("@NoofFilmsFilmCassette                        ", NoofFilmsFilmCassette);
            c.Parameters.AddWithValue("@Density                                      ", Density);
            c.Parameters.AddWithValue("@DevelopingTempratureAndTime                  ", DevelopingTempratureAndTime);
            c.Parameters.AddWithValue("@XRayKV                                       ", XRayKV);
            c.Parameters.AddWithValue("@XRayMa                                       ", XRayMa);
            c.Parameters.AddWithValue("@XRayTime                                     ", XRayTime);
            c.Parameters.AddWithValue("@XRayFSSize                                   ", XRayFSSize);
            c.Parameters.AddWithValue("@XRayModel                                    ", XRayModel);
            c.Parameters.AddWithValue("@XRaySrNo                                     ", XRaySrNo);
            c.Parameters.AddWithValue("@MinimumFocustoobjectdistance                 ", MinimumFocustoobjectdistance);
            c.Parameters.AddWithValue("@GameRaySource                                ", GameRaySource);
            c.Parameters.AddWithValue("@GameRayStrength                              ", GameRayStrength);
            c.Parameters.AddWithValue("@GameRaySize                                  ", GameRaySize);
            c.Parameters.AddWithValue("@MaximumSourceSideOftoobjectToFilmdistance    ", MaximumSourceSideOftoobjectToFilmdistance);
            c.Parameters.AddWithValue("@ShootingSketch                               ", ShootingSketch);
            c.Parameters.AddWithValue("@PreparedBy                                   ", PreparedBy);
            c.Parameters.AddWithValue("@PreparedDate                                 ", PreparedDate);
            c.Parameters.AddWithValue("@ReveiwedBy                                   ", ReveiwedBy);
            c.Parameters.AddWithValue("@ReveiwedDate                                 ", ReveiwedDate);
            c.Parameters.AddWithValue("@CreatedBy                                    ", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }



    public DataSet SaveRadioGraphicExaminationReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRadioGraphicExaminationReport";
            c.Parameters.AddWithValue("@RFPHID                      ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID                      ", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);
            c.Parameters.AddWithValue("@ReportDate                  ", ReportDate);
            c.Parameters.AddWithValue("@RGERID                      ", RGERID);
            c.Parameters.AddWithValue("@PONo                        ", PONo);
            c.Parameters.AddWithValue("@RFPNo                       ", RFPNo);
            c.Parameters.AddWithValue("@ControlOfRecords            ", ControlOfRecords);
            c.Parameters.AddWithValue("@TestDate                    ", TestDate);
            c.Parameters.AddWithValue("@Customername                ", Customername);
            c.Parameters.AddWithValue("@DrawingNo                   ", DrawingNo);
            c.Parameters.AddWithValue("@JobDescription              ", JobDescription);
            c.Parameters.AddWithValue("@ItemName                    ", ItemName);
            c.Parameters.AddWithValue("@ItemSerielNo                ", ItemSerielNo);
            c.Parameters.AddWithValue("@NoOfExposures               ", NoOfExposures);
            c.Parameters.AddWithValue("@IQI                         ", IQI);
            c.Parameters.AddWithValue("@TechniqueSheetReferenceNo   ", TechniqueSheetReferenceNo);
            c.Parameters.AddWithValue("@ExposureTechnique           ", ExposureTechnique);
            c.Parameters.AddWithValue("@StageOfInspection           ", StageOfInspection);
            c.Parameters.AddWithValue("@Viewingtechnique            ", Viewingtechnique);
            c.Parameters.AddWithValue("@ProcedureAndRevNo           ", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@WeldReinforcementthickness  ", WeldReinforcementthickness);
            c.Parameters.AddWithValue("@LeadScreensFront            ", LeadScreensFront);
            c.Parameters.AddWithValue("@LeadScreensBack             ", LeadScreensBack);
            c.Parameters.AddWithValue("@NoofFilmsFilmCassette       ", NoofFilmsFilmCassette);
            c.Parameters.AddWithValue("@SODFOD                      ", SODFOD);
            c.Parameters.AddWithValue("@FilmManufacture             ", FilmManufacture);
            c.Parameters.AddWithValue("@SSOFD                       ", SSOFD);
            c.Parameters.AddWithValue("@FilmTypeDesignation         ", FilmTypeDesignation);
            c.Parameters.AddWithValue("@XRayKV                      ", XRayKV);
            c.Parameters.AddWithValue("@XRayMa                      ", XRayMa);
            c.Parameters.AddWithValue("@Sensitivity                 ", Sensitivity);
            c.Parameters.AddWithValue("@FocalSpot                   ", FocalSpot);
            c.Parameters.AddWithValue("@GameRaySource               ", GameRaySource);
            c.Parameters.AddWithValue("@GameRayStrength             ", GameRayStrength);
            c.Parameters.AddWithValue("@GameRaySize                 ", GameRaySize);
            //   c.Parameters.AddWithValue("@DevelopingTempratureAndTime ", DevelopingTempratureAndTime);
            c.Parameters.AddWithValue("@DevelopementTemprature ", DevelopementTemprature);
            c.Parameters.AddWithValue("@DevelopementTime ", DevelopementTime);

            c.Parameters.AddWithValue("@SizeOfBLetter               ", SizeOfBLetter);
            c.Parameters.AddWithValue("@PlacingOfBLetter            ", PlacingOfBLetter);
            c.Parameters.AddWithValue("@EvaluvationOfBLettter       ", EvaluvationOfBLettter);
            c.Parameters.AddWithValue("@RTPerformer                 ", RTPerformer);
            c.Parameters.AddWithValue("@LSEPreparedBy               ", LSEPreparedBy);
            c.Parameters.AddWithValue("@LSEPreparedDate             ", LSEPreparedDate);
            c.Parameters.AddWithValue("@LSEPreapredLevel            ", LSEPreapredLevel);
            c.Parameters.AddWithValue("@LSEReviewedBy               ", LSEReviewedBy);
            c.Parameters.AddWithValue("@LSEReviewdDate              ", LSEReviewdDate);
            c.Parameters.AddWithValue("@LSEReviewedDesignation      ", LSEReviewedDesignation);
            c.Parameters.AddWithValue("@CustomerReviwedBy           ", CustomerReviwedBy);
            c.Parameters.AddWithValue("@CustomerReviwedDate         ", CustomerReviwedDate);
            c.Parameters.AddWithValue("@CustomerDesignation         ", CustomerDesignation);
            c.Parameters.AddWithValue("@AIReveiwedBy                ", AIReveiwedBy);
            c.Parameters.AddWithValue("@AIReviewedDate              ", AIReviewedDate);
            c.Parameters.AddWithValue("@AIDesignation               ", AIDesignation);
            c.Parameters.AddWithValue("@CreatedBy                   ", CreatedBy);
            c.Parameters.AddWithValue("@Other", Other);

            c.Parameters.AddWithValue("@BaseMaterial", BaseMaterial);
            c.Parameters.AddWithValue("@Film", Film);
            c.Parameters.AddWithValue("@Technique", Technique);
            c.Parameters.AddWithValue("@BaseMaterialSizeAndThickness", BaseMaterialSizeAndThickness);
            c.Parameters.AddWithValue("@PlacementOfBackScatter", PlacementOfBackScatter);
            c.Parameters.AddWithValue("@weldthickness", weldthickness);
            c.Parameters.AddWithValue("@Density", Density);

            //BaseMaterial
            //Film
            //Technique
            //BaseMaterialSizeAndThickness
            //PlacementOfBackScatter
            //weldthickness
            //Density

            c.Parameters.AddWithValue("@tblRadioGraphicExaminationDetails", dt);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveWallThinningReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWallThinningReportDetails";
            c.Parameters.AddWithValue("@RFPHID           ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID           ", RFPDID);
            c.Parameters.AddWithValue("@WTRID            ", WTRID);
            c.Parameters.AddWithValue("@ReportDate       ", ReportDate);
            c.Parameters.AddWithValue("@ItemName", IfSpecifyItemName);
            c.Parameters.AddWithValue("@DrawingNo        ", DrawingNo);
            c.Parameters.AddWithValue("@JobDescription   ", JobDescription);
            c.Parameters.AddWithValue("@PONo             ", PONo);
            c.Parameters.AddWithValue("@QAPNo            ", QAPNo);
            c.Parameters.AddWithValue("@BSLNo            ", BSLNo);
            c.Parameters.AddWithValue("@CreatedBy        ", CreatedBy);
            c.Parameters.AddWithValue("@tblWallThinningItemDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveMaterialInwardQCCertficates()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialInwardQCCertificateDetails";
            c.Parameters.AddWithValue("@AttachementName", AttachementName);
            c.Parameters.AddWithValue("@MIDID ", MIDID);
            c.Parameters.AddWithValue("@CFID ", CFID);
            c.Parameters.AddWithValue("@CertficateNo ", CertficateNo);
            c.Parameters.AddWithValue("@ReceivedDate ", ReceivedDate);
            c.Parameters.AddWithValue("@CreatedBy ", CreatedBy);

            c.Parameters.AddWithValue("@Remarks ", Remarks);
            c.Parameters.AddWithValue("@CertificateQCStatus ", CertificateQCStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }



    public DataSet SavePreservationAndPackingInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePreservationAndPackingInspectionReportDetails";
            c.Parameters.AddWithValue("@RFPHID   ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID   ", RFPDID);
            c.Parameters.AddWithValue("@PPIRID   ", PPIRID);
            c.Parameters.AddWithValue("@ReportDate   ", ReportDate);
            c.Parameters.AddWithValue("@ItemName   ", ItemName);
            c.Parameters.AddWithValue("@DrawingNo   ", DrawingNo);
            c.Parameters.AddWithValue("@Customername   ", Customername);
            c.Parameters.AddWithValue("@JobDescription  ", JobDescription);
            c.Parameters.AddWithValue("@PONo   ", PONo);
            c.Parameters.AddWithValue("@QAPNo   ", QAPNo);
            c.Parameters.AddWithValue("@CreatedBy   ", CreatedBy);
            c.Parameters.AddWithValue("@RFPNo   ", RFPNo);
            c.Parameters.AddWithValue("@tblPPItemDetails", dt);
            c.Parameters.AddWithValue("@observed   ", observed);
            c.Parameters.AddWithValue("@Packing    ", Packing);
            c.Parameters.AddWithValue("@Marking    ", Marking);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMovementTestReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMovementTestReportDetails";
            c.Parameters.AddWithValue("@RFPHID ", RFPHID);
            c.Parameters.AddWithValue("@RFPDID ", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);

            c.Parameters.AddWithValue("@MTRID ", MTRID);
            c.Parameters.AddWithValue("@ReportDate ", ReportDate);
            c.Parameters.AddWithValue("@RFPNo ", RFPNo);
            c.Parameters.AddWithValue("@ItemName ", ItemName);
            c.Parameters.AddWithValue("@DrawingNo ", DrawingNo);
            c.Parameters.AddWithValue("@Customername ", Customername);
            c.Parameters.AddWithValue("@JobDescription", JobDescription);
            c.Parameters.AddWithValue("@PONo ", PONo);
            c.Parameters.AddWithValue("@QAPNo ", QAPNo);
            c.Parameters.AddWithValue("@Remarks ", Remarks);
            c.Parameters.AddWithValue("@CreatedBy ", CreatedBy);
            c.Parameters.AddWithValue("@tblMovementtestItemDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSpringRateAndMovementTestReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSpringRateAndMovementTestReportDetails";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);

            c.Parameters.AddWithValue("@SRMTRID", SRMTRID);
            c.Parameters.AddWithValue("@RFPNO", RFPNo);
            c.Parameters.AddWithValue("@ReportDate", ReportDate);
            c.Parameters.AddWithValue("@IfSpecifyItemName", IfSpecifyItemName);
            c.Parameters.AddWithValue("@DrawingNo", DrawingNo);
            c.Parameters.AddWithValue("@JobDescription", JobDescription);
            c.Parameters.AddWithValue("@PONo", PONo);
            c.Parameters.AddWithValue("@QAPNo", QAPNo);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@TotalAverage", TotalAverage);
            c.Parameters.AddWithValue("@DrawingSpringRateValue", DrawingSpringRateValue);
            c.Parameters.AddWithValue("@SpringRateActualGraphValue", SpringRateActualGraphValue);
            c.Parameters.AddWithValue("@Percentage", Percentage);
            //c.Parameters.AddWithValue("@tblBellowsDetails", dt);

            c.Parameters.AddWithValue("@Size", Size);
            c.Parameters.AddWithValue("@BSLNo", BSLNo);
            c.Parameters.AddWithValue("@AcceptedQty", AcceptedQty);
            c.Parameters.AddWithValue("@OtherReference", OtherReference);
            c.Parameters.AddWithValue("@TestProcedureNo", TestProcedureNo);
            c.Parameters.AddWithValue("@Type", Type);

            c.Parameters.AddWithValue("@tblDimensionDetails", dtDimension);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMaterialInwardQCClearanceDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialInwardQcClearanceQuantityDetails";
            c.Parameters.AddWithValue("@MIDID", MIDID);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@Quantity", Quantity);
            c.Parameters.AddWithValue("@MaterialQtyStatus", MaterialQtyStatus);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@LOcationID", LOcationID);
            //if (ReworkedQty == 0)
            //    c.Parameters.AddWithValue("@ReworkedQty", DBNull.Value);
            //else
            //    c.Parameters.AddWithValue("@ReworkedQty", ReworkedQty);
            //c.Parameters.AddWithValue("@ReworkedQtyStatus", ReworkedQtyStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialInwardQCClearanceQuantityDetailsByMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialInwardQCClearanceQuantityDetails";
            c.Parameters.AddWithValue("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteMaterialInwardQCClearanceDetailsByMIDID(int MIQCDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialInwardQCClearanceDetailsBYMIQCDID";
            c.Parameters.AddWithValue("@MIQCDID", MIQCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet CheckQualityPlanningAndAssemplyPlanningDetailsCompletedOrNot()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_CheckQualityPlanningAndAssemplyPlanningStatus";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveFinalAssemblyQCListDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveFinalAssemblyQCListDetails";
            if (FAQPDID == "")
                c.Parameters.AddWithValue("@FAQPDID", DBNull.Value);
            else
                c.Parameters.AddWithValue("@FAQPDID", FAQPDID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMRNDetailsByMRNNumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNDetailsByMRNNumber";
            c.Parameters.AddWithValue("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderInwarddedIndentDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderInwardedIndentDetails";
            c.Parameters.AddWithValue("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQCListDetailsByWorkOrderIndentNo()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQCListDetailsByWorkOrderIndentNo";
            c.Parameters.AddWithValue("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkOrderInwardQCClearanceDetails(string WOIDIDs, string WOQCLMIDs)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderInwardQCClearanceDetails";
            c.Parameters.AddWithValue("@WOIDIDs", WOIDIDs);
            c.Parameters.AddWithValue("@WOQCLMIDs", WOQCLMIDs);
            c.Parameters.AddWithValue("@AttachementName", AttachementName);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@Status", Status);
            c.Parameters.AddWithValue("@WOIHID", WOIHID);
            if (string.IsNullOrEmpty(CreatedBy))
                c.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
            else
                c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyWeldingQCClearanceDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyWeldingQCClearanceDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@AssemplyStatus", AssemplyStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyPlanningQCDetailsByPAPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyPlanningQCDetailsByPAPDID";
            c.Parameters.Add("@PAPDID", PAPDID);
            // c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePartAssemplyWeldingQCClearanceDetails(string PRIDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartAssemplyWeldingQCClearanceDetails";
            c.Parameters.Add("@PAPDID", PAPDID);
            //c.Parameters.Add("@QPDID", QPDID);
            //c.Parameters.Add("@AttachementName", AttachementName);
            //c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@PRIDID", PRIDID);
            c.Parameters.Add("@Status", Status);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@tblAssemplyQCDetails", dt);
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderQCListMasterDetailsByQCOprationName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderQCListDetailsByQCOperationName";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkOrderQCPlanningDetauils()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderQCPlanningDetails";
            c.Parameters.Add("@WOIHID", WOIHID);
            c.Parameters.Add("@WOQCLMID", WOQCLMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteWorkOrderIndentQCListDetailsByWOIQCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderIndentQCListDetailsByWOIQCDID";
            c.Parameters.Add("@WOIQCDID", WOIQCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateWorkOrderShareQCStatusByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateWorkOrderShareQCStatusByWOIHID";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateMaterialInwardQCCompletedStatusByMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateQCCompletedStatusByMIDID";
            c.Parameters.Add("@MIDID", MIDID);
            c.Parameters.Add("@LOcationID", LOcationID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialInwardQCAvailQtyDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_MaterialinwardDCAvailQtyDetailsByMIDID";
            c.Parameters.Add("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMIQCHeaderDetailsByMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_MIQCHeaderDetailsByMIDID";
            c.Parameters.Add("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBellowSnoByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBellowSnoByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDimensionInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDimensionsInspectionDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteDimensionInspectionReportdetailsByDIRDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteDimensionInspectionReportdetailsByDIRDID";
            c.Parameters.Add("@DIRDID", DIRDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDimensionInspectionReportdetailsPrint()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDimensionInspectionReportdetails_print";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMovementTestReportdetailsByMTRID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMovementTestReportDetailsByMTRID_PRINT";
            c.Parameters.Add("@MTRID", MTRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetLPIPrintdetailsByLPIRID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLPIDetailsByLPIRID_Print";
            c.Parameters.Add("@LPIRID", LPIRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveFerritetestReportdetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveFerriteTestReportdetails";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@FTRHID", FTRHID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@IfSpecifyItemName", IfSpecifyItemName);
            c.Parameters.Add("@Customername", Customername);
            c.Parameters.Add("@JobDescription", JobDescription);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@BSLNo", BSLNo);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@TestDate", TestDate);
            c.Parameters.Add("@ITPNo", ITPNo);
            c.Parameters.Add("@tagNo", tagNo);
            c.Parameters.Add("@Size", Size);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@AcceptedQty", AcceptedQty);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@tblFerritetestReportDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetFerriteTestReportDetailsByFTRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetFerriteTestReportDetailsByFTRHID_PRINT";
            c.Parameters.Add("@USERID", USERID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetPMIReportDetailsByPMIRID_PRINT()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPMIReportDetailsByPMIRID_PRINT";
            c.Parameters.Add("@PMIRID", PMIRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }




    public DataSet SaveRawMaterialInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRawMaterialInspectionReportDetails";
            c.Parameters.Add("@RMIRHID", RMIRHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@CustomerName", CustomerName);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@ITPNo", ITPNo);
            c.Parameters.Add("@Size", Size);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@BSLNo", BSLNo);
            c.Parameters.Add("@AcceptedQty", AcceptedQty);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@Other", Other);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@tblRawMaterialinspectionReportDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRawMaterialInspectionReportByMIRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRawMaterialInspectionReportByRMIRHID_PRINT";
            c.Parameters.Add("@RMIRHID", RMIRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRadioGraphicExaminationDetailsByRGERID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRadioGraphicExaminationDetailsByRGERID_PRINT";
            c.Parameters.Add("@RGERID", RGERID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePressureTestReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePressureTestReportDetails";
            c.Parameters.Add("@PTRHID", PTRHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@ITPNo", ITPNo);
            c.Parameters.Add("@Customername", Customername);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@Size", Size);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@BSLNo", BSLNo);
            c.Parameters.Add("@AcceptedQty", AcceptedQty);
            c.Parameters.Add("@ApprovedTestProcedureNo", ApprovedTestProcedureNo);
            c.Parameters.Add("@TypeOfTest", TypeOfTest);
            c.Parameters.Add("@Medium", Medium);
            c.Parameters.Add("@TestPressure", TestPressure);
            c.Parameters.Add("@HoldingTime", HoldingTime);
            c.Parameters.Add("@DesignPressure", DesignPressure);
            c.Parameters.Add("@TestTemprature", TestTemprature);
            c.Parameters.Add("@CodeNo", CodeNo);
            c.Parameters.Add("@Range", Range);
            c.Parameters.Add("@CalibrationRef", CalibrationRef);
            c.Parameters.Add("@CalibrationDoneOn", CalibrationDoneOn);
            c.Parameters.Add("@CalibrationDueOn", CalibrationDueOn);
            c.Parameters.Add("@Result", Result);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPressureTestReportDetailsByPTRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPressureTestReportDetailsByPTRHID_PRINT";
            c.Parameters.Add("@PTRHID", PTRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPaintingObservationDescriptionDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPaintingObservationDescriptionDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePaintingInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePaintingInspectionReportDetails";
            c.Parameters.Add("@PIRHID", PIRHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@ITPNo", ITPNo);
            c.Parameters.Add("@Customername", Customername);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@Size", Size);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@BSLNo", BSLNo);
            c.Parameters.Add("@AcceptedQty", AcceptedQty);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            c.Parameters.Add("@TestProcedureNo", TestProcedureNo);
            c.Parameters.Add("@CustomerSpecification", CustomerSpecification);
            c.Parameters.Add("@MethodOfCleaning", MethodOfCleaning);
            c.Parameters.Add("@VisualInspection", VisualInspection);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@tblPaintingObservationDetails", dt);

            //TestProcedureNo
            //CustomerSpecification
            //MethodOfCleaning
            //VisualInspection
            //Remarks
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPaintingInspectionReportDetailsByPIRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPaintingInspectionReportDetailsByPIRHID_PRINT";
            c.Parameters.Add("@PIRHID", PIRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetHeatTreatmentCycleReportDetailsByHTCRID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetHeatTreatmentCycleReportDetailsByHTCRID_PRINT";
            c.Parameters.Add("@HTCRID", HTCRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePicklingAndPassivationReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePicklingAndPassivationReportDetails";
            c.Parameters.Add("@PPRHID", PPRHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@ITPNo", ITPNo);
            c.Parameters.Add("@Customername", Customername);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@Size", Size);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@BSLNo", BSLNo);
            c.Parameters.Add("@AcceptedQty", AcceptedQty);
            c.Parameters.Add("@WorkOrderNo", WorkOrderNo);
            c.Parameters.Add("@Part", Part);
            c.Parameters.Add("@DatePC", DatePC);
            c.Parameters.Add("@StartTimePC", StartTimePC);
            c.Parameters.Add("@CompletionTimePC", CompletionTimePC);
            c.Parameters.Add("@ProcedureAndResultsPC", ProcedureAndResultsPC);
            c.Parameters.Add("@DateAPC", DateAPC);
            c.Parameters.Add("@StartTimeAPC", StartTimeAPC);
            c.Parameters.Add("@CompletionTimeAPC", CompletionTimeAPC);
            c.Parameters.Add("@MethodologyAPC", MethodologyAPC);
            c.Parameters.Add("@ProcedureAndResultsAPC", ProcedureAndResultsAPC);
            c.Parameters.Add("@DatePAC", DatePAC);
            c.Parameters.Add("@StartTimePAC", StartTimePAC);
            c.Parameters.Add("@CompletionTimePAC", CompletionTimePAC);
            c.Parameters.Add("@MethodologyPAC", MethodologyPAC);
            c.Parameters.Add("@ProcedureAndResultsPAC", ProcedureAndResultsPAC);
            c.Parameters.Add("@Results", Results);
            c.Parameters.Add("@PECPLTDCNo", PECPLTDCNo);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPicklingAndPassivationReportDetailsByPPRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPicklingAndPassivationReportDetailsByPPRHID_PRINT";
            c.Parameters.Add("@PPRHID", PPRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }



    public DataSet SaveWeldPlanDetailsReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWeldPlanDetailsReport";
            c.Parameters.Add("@WPRHID", WPRHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@Customername", Customername);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@Technique", Technique);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@ITPNo", ITPNo);
            c.Parameters.Add("@AcceptedQty", AcceptedQty);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@tblWeldPlanDetails", dt);
            c.Parameters.Add("@Size", Size);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWelPlanReportDetailsByWPRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWeldPlanReportDetailsByWPRHID_PRINT";
            c.Parameters.Add("@WPRHID", WPRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSpringrateAndMovementTestReportDetailsBySRMTRID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSpringRateTestReportDetailsBySRMTRID_PRINT";
            c.Parameters.Add("@SRMTRID", SRMTRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetHardNessTestReportDetailsByHTRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetHardNessTestReportDetailsByHTRHID_PRINT";
            c.Parameters.Add("@HTRHID", HTRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveHardNessTestReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveHardNessTestReportDetails";
            c.Parameters.Add("@HTRHID", HTRHID);
            c.Parameters.Add("@ReportDate", ReportDate);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@Customername", Customername);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@PONo", PONo);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@QAPNo", QAPNo);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@BSLNo", BSLNo);
            c.Parameters.Add("@Size", Size);
            c.Parameters.Add("@Inspection", Inspection);
            c.Parameters.Add("@WeldPlan", WeldPlan);
            c.Parameters.Add("@PartName1", PartName1);
            c.Parameters.Add("@PartName2", PartName2);
            c.Parameters.Add("@WorkOrderNo", WorkOrderNo);
            c.Parameters.Add("@Instrument", Instrument);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            c.Parameters.Add("@tblHardNessTestReportDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetUltraSonicReportDetailsByUSERID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUltraSonicReportDetailsByUSERID_PRINT";
            c.Parameters.Add("@USERID", USERID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyPlanningJobcardItemSnoByPAPDIDAndJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyPlanningJobcardItemSnoByPAPDIDAndJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@PAPDID", PAPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveQualityStageInspectionReportDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQualityStageInspectionReportDetails";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@StageInspection", StageInspection);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", UserID);
            c.Parameters.Add("@RaisedBy", RaisedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQualityStageInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQualityStageInspectionReportDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardQCStageDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardNoProcessQCStageDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJObCardNOQCStageDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardNoMarkingAndCuttingQCStageDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@PMID", PMID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@tblJobCardNoQCStageDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInwardQtyEntireIndentByWOIHID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateInwardQtyEntireIndentByWOIHID";
            c.Parameters.Add("@WOIHID", WOIHID);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateShareAssemplyPlanningStatusByRFPDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateShareAssemplyPlanningStatusByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveAssemplyJobcardQCStageDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveAssemplyJobCardQCStageActivityDetails";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@PAPDID", PAPDID);
            c.Parameters.Add("@PMID", PMID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@tblJobCardNoQCStageDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyJobCardQCStageDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyJobCardNoProcessQCStageDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@PAPDID", PAPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardQCAttachements()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardQCAttachementDetails";
            c.Parameters.Add("@AttachementDescription", AttachementDescription);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetAttachementDetailsByJobCardNo()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAttachementDetailsByJobCardNo";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteJobCardAttachementDetailsByAttachementIDAndJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteJobCardAttachByJCHIDAndAttachementID";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@AttachementID", AttachementID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateQCDimensionDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateQCDimensionDetailsByJCHID";
            c.Parameters.Add("@QCDimensionName", QCDimensionName);
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@Actual", Actual);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet ReverseAssemplyPlanningStatusByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateReverseAssemplyPlanningStatusByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateAssemplyPlanningStatusByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAssemplyPlanningStatusByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

    #region"DropDown Events"

    public DataSet GetPartSeriwlNoDetailsByEDID(DropDownList ddlPartName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartSerielNumberByRFPDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);

            ddlPartName.DataSource = ds.Tables[0];
            ddlPartName.DataTextField = "PartName";
            ddlPartName.DataValueField = "BOMID";
            ddlPartName.DataBind();
            ddlPartName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetItemSpecificationDetailsByRFPDIDAndEDID(DropDownList ddlPenetrantBrand, DropDownList ddlDeveloperBrand, DropDownList ddlCleanerBrand)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemSpecificationDetailsByRFPDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@EDID", EDID);
            DAL.GetDataset(c, ref ds);

            ddlPenetrantBrand.DataSource = ddlDeveloperBrand.DataSource = ddlCleanerBrand.DataSource = ds.Tables[1];
            ddlPenetrantBrand.DataTextField = ddlDeveloperBrand.DataTextField = ddlCleanerBrand.DataTextField = "BrandNo";
            ddlPenetrantBrand.DataValueField = ddlDeveloperBrand.DataValueField = ddlCleanerBrand.DataValueField = "PID";
            ddlPenetrantBrand.DataBind();
            ddlDeveloperBrand.DataBind();
            ddlCleanerBrand.DataBind();

            ddlPenetrantBrand.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDeveloperBrand.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCleanerBrand.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindAssemplyWeldingAndFinalAssemplyQCDetails(ListBox liFinalAssemblyQC, ListBox liPartAssemblyQC)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemblyWeldingAndFinalAssemblyQCDetails";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);

            liPartAssemblyQC.DataSource = ds.Tables[0];
            liPartAssemblyQC.DataTextField = "TypeOfCheck";
            liPartAssemblyQC.DataValueField = "QPDID";
            liPartAssemblyQC.DataBind();
            liPartAssemblyQC.Items.Insert(0, new ListItem("--Select--", "0"));

            liFinalAssemblyQC.DataSource = ds.Tables[1];
            liFinalAssemblyQC.DataTextField = "TypeOfCheck";
            liFinalAssemblyQC.DataValueField = "QPDID";
            liFinalAssemblyQC.DataBind();
            liFinalAssemblyQC.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getRFPApprovedCustomerNameByUserID(DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPApprovedCustomerNameByUserID";
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

    public DataSet GetRFPDetailsByUserIDAndRFPStatusCompleted(DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByUserIDAndRFPStatusApproved";
            c.Parameters.Add("@EmployeeID", UserID);
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

    public DataSet GetRFPDetailsByJobcardQC(int UserID, DropDownList ddlRFPNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsJobCardQC";
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);
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

    public DataSet GetDailyActivitiesQualityRFPDetailsByEmployeeID(int EmployeeID, DropDownList ddlRFPNo, DropDownList ddlRaisedBy)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDailyActivitiesQualityRFPDetailsByEmployeeID";
            c.Parameters.Add("@EmployeeID", EmployeeID);
            DAL.GetDataset(c, ref ds);

            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlRaisedBy.DataSource = ds.Tables[1];
            ddlRaisedBy.DataTextField = "EmployeeName";
            ddlRaisedBy.DataValueField = "EmployeeID";
            ddlRaisedBy.DataBind();
            ddlRaisedBy.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getRFPCustomerNameByJobCardQCByUserID(int UserID, DropDownList ddlRFPNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetRFPCustomerNameByJobCardQCByUserID";
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);
            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "ProspectName";
            ddlRFPNo.DataValueField = "ProspectID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPCustomerNameByBlockingMRNQualityClearance(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetRFPCustomerNameByBlockingMRNQualityClearance";
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "ProspectName";
            ddlRFPNo.DataValueField = "ProspectID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoDetailsByBlockingMRNQualityClearance(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetRFPNoDetailsByBlockingMRNQualityClearance";
            c.Parameters.Add("@EmployeeID", UserID);
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

    public DataSet GetRFPCustomerNameByAsemplyWeldingQCClearance(int UserID, DropDownList ddlRFPNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetRFPCustomerNameByAssemplyWeldingQCClearance";
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);
            ddlRFPNo.DataSource = ds.Tables[0];
            ddlRFPNo.DataTextField = "ProspectName";
            ddlRFPNo.DataValueField = "ProspectID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoDetailsByAssemplyweldingQCClearance(int UserID, DropDownList ddlRFPNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetRFPNoDetailsByAssemplyweldingQCClearance";
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);
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


    public DataSet GetCertficateDetailsByMRNQC()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetCertificateDetailsByMRNQC";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetRFPCustomerNameByRFPQualityPlanning(int UserID, DropDownList ddlCustomerName, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_RFPCustomerNameByRFPQualityPlanning";
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);

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

    public DataSet GetRFPNoDetailsByRFPQualityPlanning(int UserID, DropDownList ddlRFPNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetRFPCustomerNameByQualityUserID";
            c.CommandText = "LS_GetRFPNoDetailsByRFPQualityPlanning";
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);
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

    public DataSet UpdateCertificateMandatoryStatusByMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateCertificatesMandatoryStatusByMIDID";
            c.Parameters.Add("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	public DataSet GetGRNDetailsByGRNNumber()
		{
			DataSet ds = new DataSet();
			try
			{
				DAL = new cDataAccess();
				c = new SqlCommand();
				c.CommandType = CommandType.StoredProcedure;
				c.CommandText = "LS_GetGRNDetailsByGRNNumber";
				c.Parameters.AddWithValue("@MIDID", MIDID);
				DAL.GetDataset(c, ref ds);
			}
			catch (Exception ex)
			{
				Log.Message(ex.ToString());
			}
			return ds;
		}
		
		    public DataSet CheckTCAddedOrNot(int MIDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_CheckTCAddedOrNotByMIDID";
            c.Parameters.Add("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet UpdateGeneralStockStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateGeneralStockStatus";
            c.Parameters.Add("@MIDID", STID);
            c.Parameters.Add("@UserID", JCHID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet GetInwardedMaterialDetailsByLocationIDForgeneral()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInwardedMaterialDetailsByLOcationIDForgeneral";
            c.Parameters.AddWithValue("@SPOID", SPODID);
            c.Parameters.AddWithValue("@mrnchangestatus", mrnchangestatus);
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