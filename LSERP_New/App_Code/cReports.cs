using eplus.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for cReports
/// </summary>
public class cReports
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
    public DateTime txtinspectionperiodtodate;

    #endregion

    #region"Properties"

    public string InwardQty { get; set; }
    public string MRNNo { get; set; }
    public string InwardedDate { get; set; }
    public string QCProcessedQty { get; set; }
    public string Status { get; set; }
    public string QCDoneOn { get; set; }
    public string QCDoneBy { get; set; }
    public string StockDate { get; set; }

    public string SPODID { get; set; }
    public string RFPNo { get; set; }
    public string INDNo { get; set; }
    public string PartName { get; set; }
    public string PONo { get; set; }
    public string PODate { get; set; }
    public string POQty { get; set; }
    public string PODeliveryDate { get; set; }
    public string SupplierName { get; set; }
    public string DCQty { get; set; }
    public int RFPHID { get; set; }

    public int RFPDID { get; set; }

    public string MID { get; set; }
    public string MaterialName { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string ApprovalName { get; set; }
    public int EnquiryID { get; set; }
    public int PoCopy { get; set; }

    public int EODID { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public int SUPID { get; set; }
    public string CID { get; set; }
    public string MRNID { get; set; }

    public int SPOID { get; set; }
    public string PartType { get; set; }
    public string FYID { get; set; }
    public string Studentname { get; set; }
    public string Address { get; set; }
    public string PhoneNo { get; set; }
    public int NCRNo { get; set; }
    public DateTime NCRDate { get; set; }
    public DateTime InspectionFromDate { get; set; }
    public string RepeatedReworkLocation { get; set; }
    public string WeldDefectsFoundInMeter { get; set; }
    public string DataFrom { get; set; }
    public string RepeatedReworks { get; set; }
    public string InspectionOfWelLengthInMeterPerQty { get; set; }
    public string CreatedBy { get; set; }
    public string CAPAID { get; set; }
    public int CAPAASID { get; set; }
    public string Description { get; set; }
    public DateTime ActionDate { get; set; }
    public string InChargeName { get; set; }
    public int CAPADID { get; set; }

    public string ProcessName { get; set; }
    public int PMID { get; set; }
    public string UserID { get; set; }
    public int QCStatus { get; set; }
    public string AttachementName { get; set; }
    public string ProcessStep { get; set; }
    public string ISOPDID { get; set; }
    public string BellowSno { get; set; }
    public string MGMID { get; set; }
    public string Remarks { get; set; }
    public int ProspectID { get; set; }

    #endregion

    #region"Common methods"

    public DataSet GetPurchaseIndentMaterialInwardStatusReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentStatusReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCompanySummaryReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCompanyCompensationSummaryStatusSheet";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSalesSummaryReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesSummaryStatusReport";
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCompanyDetailsReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCompanyDetailsReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignSummaryReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DesignSummaryReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPSummarySheetDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPSummarySheetDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialPlanningStatusDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlanningStatusDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignDetailsReport()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignChartDetailsSheetReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSalesDetailsReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesChartDetailsSheetReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemStatusDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemStatusDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    //LS_GetPartStatusDetailsByRFPDID
    public DataSet GetPartStatusDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartStatusDetailsByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndentSTatusReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentStatusReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartWiseRFPStatusReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobOrderRFPPartDetailsByMID";
            c.Parameters.Add("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyWiseStockReportCostrDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlyClosingInStockDetails";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyConsumptionStockReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlyConsumtionStockReportsDetailsByDateWise";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyMaterialInwardMaterialReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlyInwardMRNReportDetails";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyOrderDetailsByMonthAndYear()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlyOfferOrderDetailsByMonthYear";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyEarningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@Year", Year);
            c.CommandText = "LS_GetMonthlyEarningDetailsByYear";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyCompletedRFPReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyCompletedRFPListDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyOfferCostDetailsByMonthAndYear()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlyOfferCostDetailsReport";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetApprovalPendingStatusReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetApprovalPendingStatusReport";
            c.Parameters.Add("@ApprovalName", ApprovalName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyOpeningStockReportDetailsByMonthAndYear()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_MonthlyOpeningStockReportDetailsByMonthAndYear";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryHeaderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryHeaderForCrashReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemDetailsbyEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemDetailsByEnquiryIDForCrashReport";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDepartmentDocumentDetailsByEnquiryID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOfferStatusUpdateHistoryDetailsByEODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOfferStatusupdateHistoryDetailsByEODID";
            c.Parameters.Add("@EODID", EODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockInwardPendingMRNReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockInwardPendingMRNReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyClosingStockReportDetailsByMonthAndYear()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlyClosingStockReportDetailsByMonthAndYear";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseIndentDetailsByCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentReportByCID";
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMarketingReportDetailsByFromDateAndToDate(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMRNLifeCycleReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNLifeCycleReportDetailsByMRNID";
            c.Parameters.Add("@MRNID", MRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMrnNoDetails(DropDownList ddlMRNNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNNoDetails";
            DAL.GetDataset(c, ref ds);
            ddlMRNNo.DataSource = ds.Tables[0];
            ddlMRNNo.DataTextField = "MRNNumber";
            ddlMRNNo.DataValueField = "MRNID";
            ddlMRNNo.DataBind();
            ddlMRNNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSalesKPICollectionDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesKPICollectionDetails";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateProblemRaisedQCStatusByCAPAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateProblemRaisedQCStatusByCAPAID";
            c.Parameters.Add("@QCStatus", QCStatus);
            c.Parameters.Add("@CAPAID", CAPAID);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@ISOPDID", ISOPDID);
            c.Parameters.Add("@BellowSno", BellowSno);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialnameOfPreviousPriceReportByMGMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialNamePriviousPriceReportDetailsByMGMID";
            c.Parameters.Add("@MGMID", MGMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetViewCustomerOrderDetailsByProspectID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerOrderDetailsByPrspectID";
            c.Parameters.Add("@ProspectID", ProspectID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

    #region"DropDown Methods"

    public DataSet GetCurrentMonthYearDetails(DropDownList ddlMonthYear)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCurrentMonthYearDetails";
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
            //MonthYear	MonthYearName
            ddlMonthYear.DataSource = ds.Tables[0];
            ddlMonthYear.DataTextField = "MonthYearName";
            ddlMonthYear.DataValueField = "MonthYear";
            ddlMonthYear.DataBind();
            ddlMonthYear.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetYearDetails(DropDownList ddlyear)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetYearDetails";
            DAL.GetDataset(c, ref ds);

            ddlyear.DataSource = ds.Tables[0];
            ddlyear.DataTextField = "Year";
            ddlyear.DataValueField = "Year";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierNameDetails(DropDownList ddlSuppliername)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierDetails";
            DAL.GetDataset(c, ref ds);

            ddlSuppliername.DataSource = ds.Tables[0];
            ddlSuppliername.DataTextField = "SupplierName";
            ddlSuppliername.DataValueField = "SUPID";
            ddlSuppliername.DataBind();
            ddlSuppliername.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierEvaluvationReportBySUPIDAndDate()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPOEvaluvationReportBySUPIDAndDate";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierEvaluvationReportByFormDateAndTodate()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOverAllPOEvaluvationReportByFromDateAndToDate";
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet POAndInwardStatusReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPOAndInwardStatusReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCategoryNameDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCategroyNameDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierPOItemDetailsBySPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPOItemDetailsBySPOIDToPOInwardStatusReport";
            c.Parameters.Add("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInwardDetailsBySPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInwardDetailsBySPOID";
            c.Parameters.Add("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoDetails(DropDownList ddlRFPNo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoDetailsByIndentStatusReport";
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

    public DataSet GetItemNameDetailsByRFPHID(DropDownList ddlItemName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemDetailsByRFPHIDByIndentStatusReport";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);

            ddlItemName.DataSource = ds.Tables[0];
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "RFPDID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartDetailsByRFPHIDAndRFPDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartDetailsIndentStatusReportByItemWise";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPExpensesReportDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPExpensesReport";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPItemExpensesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemExpensesDetails";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemExpensesDetailsByRFPDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemExpensesDetailsByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAddtionalPartExpensesDetailsByFromdateandtodate()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAddtionalPartExpensesDetailsByFromDateAndToDate";
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            c.Parameters.Add("@PartType", PartType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMonthlyKPIDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMonthlySalesKPIDetailsByMonthAndYear";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQuotationApprovalPendingStatusReportDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQuotationApprovalPendingStatusReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignKPIReportDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignKPIReportDetails";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            c.Parameters.Add("@FYID", FYID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerPOAndOfferEnquiryNoDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerPOAndOfferDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDrawingItemDetailsDesignKPIByEnquiryID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingDetailsByEnquiryNo_DesignKPI";
            c.Parameters.Add("@EnquiryNo", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetStudentListDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStudentDetails";
            c.Parameters.Add("@Studentname", Studentname);
            c.Parameters.Add("@Address", Address);
            c.Parameters.Add("@PhoneNo", PhoneNo);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCustomerNameDetailsByCAPAReport(DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerNameDetailsByCAPAReport";
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

    public DataSet GetRFPNoDetailsByCAPAReport(DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoDetilsByCAPAReport";
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

    public DataSet GetItemNameDetailsByRFPHIDCAPAReport(DropDownList ddlItemName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.CommandText = "LS_GetItemNameDetailsByRFPHIDByCAPAReport";
            DAL.GetDataset(c, ref ds);

            ddlItemName.DataSource = ds.Tables[0];
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "RFPDID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCAPAActionState(DropDownList ddlCAPAActionState)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPAID", CAPAID);
            c.Parameters.Add("@ProcessStep", ProcessStep);
            c.CommandText = "LS_GetCAPAActionState";
            DAL.GetDataset(c, ref ds);

            ddlCAPAActionState.DataSource = ds.Tables[0];
            ddlCAPAActionState.DataTextField = "Statename";
            ddlCAPAActionState.DataValueField = "CAPAASID";
            ddlCAPAActionState.DataBind();
            ddlCAPAActionState.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveBasicCapaReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPAID", CAPAID);
            //c.Parameters.Add("@NCRNo", NCRNo);
            //c.Parameters.Add("@NCRDate", NCRDate);
            c.Parameters.Add("@InspectionFromDate", InspectionFromDate);
            c.Parameters.Add("@InspectionToDate", txtinspectionperiodtodate);
            c.Parameters.Add("@RepeatedReworkLocation", RepeatedReworkLocation);
            c.Parameters.Add("@InspectionOfWelLengthInMeterPerQty", InspectionOfWelLengthInMeterPerQty);
            c.Parameters.Add("@WeldDefectsFoundInMeter", WeldDefectsFoundInMeter);
            c.Parameters.Add("@DataFrom", DataFrom);
            c.Parameters.Add("@RepeatedReworks", RepeatedReworks);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);

            c.CommandText = "LS_SaveBasicCAPAReportDetails";
            DAL.GetDataset(c, ref ds);

            //NCRDate
            //InspectionFromDate
            //txtinspectionperiodtodate
            //RepeatedReworkLocation
            //WeldDefectsFoundInMeter
            //DataFrom
            //RepeatedReworks
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCAPAReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.CommandText = "LS_GetCAPAReportDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet SaveCAPAActionDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;

            c.Parameters.Add("@CAPAID", CAPAID);
            c.Parameters.Add("@CAPAASID", CAPAASID);
            c.Parameters.Add("@Description", Description);
            // c.Parameters.Add("@ActionDate", ActionDate);
            c.Parameters.Add("@InChargeName", InChargeName);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.CommandText = "LS_SaveCAPAActionDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCAPAReportDetailsByCAPAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPAID", CAPAID);
            c.CommandText = "LS_GetCAPAReportDetailsByCAPAID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCAPAActionStateDetailsByCAPAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPAID", CAPAID);
            c.CommandText = "LS_GetCAPAActionStateDetailsByCAPAID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteCAPAActionReportDetailsByCAPADID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPADID", CAPADID);
            c.Parameters.Add("@CAPAID", CAPAID);
            c.CommandText = spname;
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCAPAReportDetailsByCAPAIDForPrint()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPAID", CAPAID);
            c.CommandText = "LS_GetCAPAReportDetails_Print";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoList()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoListDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPItemDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.CommandText = "LS_GetRFPItemDetailsByRFPHID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPQualityPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPDID", RFPDID);// LS_GetPartNameByEDID
            c.CommandText = "LS_GetViewRFPQualityPlanningDetailsByRFPDID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPAssemplyPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPDID", RFPDID);// LS_GetPartNameByEDID
            c.CommandText = "LS_GetViewRFPAssemplyPlanningDetailsByRFPDID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPMaterialPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetViewRFPmaterialPlanningDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetRFPItemJobCardStatusReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemJobCardDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemNameByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemNameByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPQCItemStatusCard()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemStatusCard";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPQCItemStatusCardPrintDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPQCItemStatusCardPrintByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPItemAssemplyWeldingQCClearanceStatusCardByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemAssemplyWeldingQCClearanceStatusCardByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPItemRawmatQCClearedByAndDateByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemRawMatQCClearedByAndDateByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@PMID", PMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCAPARequestPendingDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCAPARequestPendingDetails";
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateProductionCorrectiveActionQCStatusByCAPAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateProductionCorrectiveActionQCStatusByCAPAID";
            c.Parameters.Add("@QCStatus", QCStatus);
            c.Parameters.Add("@CAPAID", CAPAID);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@AttachementName", AttachementName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetCAPARequestFinalApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCAPARequestFinalApprovalDetails";
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateFinalNCQCStatusByCAPAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateFinalNCQCStatusByCAPAID";
            c.Parameters.Add("@QCStatus", QCStatus);
            c.Parameters.Add("@CAPAID", CAPAID);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@AttachementName", AttachementName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryStatusReportByFromDateAndToDate()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryStatusReportByFromDateAndToDate";
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetNCRDocNoList(DropDownList ddlNCDocNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetNCRDocNoListForNCReport";
            DAL.GetDataset(c, ref ds);

            ddlNCDocNo.DataSource = ds.Tables[0];
            ddlNCDocNo.DataTextField = "DocNo";
            ddlNCDocNo.DataValueField = "ISOPDID";
            ddlNCDocNo.DataBind();
            ddlNCDocNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEnquiryInformationDetailsByEnquiryNumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryInformationDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialNameDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialNameDetailsByMaterialPriceReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateRFPDispatchDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateRFPDispatchDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@AttachementName", AttachementName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPDispatchDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDispatchDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNOForDispatch(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoDispatchDetails";
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);
            //,
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

    public DataSet GetCustomernameForDispatch(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerNameDispatchDetails";
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

    public DataSet GetRFPKPIReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPKPIReports";
            c.Parameters.Add("@FYID", FYID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPKPIDetailsByMonthAndYear()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPKPIDetailsByMonthAndYear";
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPKPIItemDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.CommandText = "LS_GetRFPKPIItemDetailsByRFPHID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPDispatchedDetailsByMonthAndYear()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@Month", Month);
            c.Parameters.Add("@Year", Year);
            c.CommandText = "GetRFPDispatchedDetailsByMonthAndYear";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteCAPAReportByCAPAID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@CAPAID", CAPAID);
            c.CommandText = "LS_DeleteCAPAReportByCAPAID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPKPIDispatchDetailsByFromDateAndToDate()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            c.CommandText = "LS_RFPKPIDispatchDetailsByFromDateAndToDate";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRepeatedOrderAnalysisReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRepeatedOrderAnalysisReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	public DataSet GetSupplierEvaluvationReportDetailsByFormDateAndTodate()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPOEvaluvationReportDetailsBySUPIDAndDate";
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

	
    public DataSet GetRFPExpensesReportDetailsSupplier()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPExpensesReportSupplier";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	
    public DataSet GetRFPEnquiryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_EnquiryToRFPReports";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet POAndInwardStatusReportDetails_New()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPOAndInwardStatusReportDetails_New";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
    public DataSet GetGeneralWorkOrderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GeneralWorkOrderReports";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
		
    public DataSet GetOfferPOComparisionReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_OfferPOComparisionReports";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	
    public DataSet GetFileName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetFileName";
            c.Parameters.AddWithValue("@PoCopy", PoCopy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderReports";
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