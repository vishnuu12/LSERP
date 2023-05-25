using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using eplus.data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for cStores
/// </summary>
public class cStores
{
    #region "Declaration"

    DataSet ds = new DataSet();
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    cSession objSession = new cSession();
    public string status;

    #endregion

    #region"Properties"

    public int SUPID { get; set; }
    public int LocationID { get; set; }

    public string DCNumber { get; set; }
    public DateTime DCdate { get; set; }

    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }

    public string DCCopy { get; set; }

    public string date { get; set; }
    public int MIHID { get; set; }
    public int SPODID { get; set; }
    public Decimal DcQty { get; set; }
    public int SPOID { get; set; }

    public int MRNID { get; set; }

    public int CMID { get; set; }
    public int Qty { get; set; }
    public int SIID { get; set; }

    public int CertificateID { get; set; }
    public string CertificateNo { get; set; }

    public DateTime ReceiptDate { get; set; }

    public int SICFID { get; set; }
    public string Mode { get; set; }

    public int MPID { get; set; }
    public string blockedweight { get; set; }
    public string RequiredShape { get; set; }
    public int MGMID { get; set; }
    public int THKID { get; set; }

    public decimal THKValue { get; set; }

    public int MPMD { get; set; }

    public int UserID { get; set; }
    public int MIDID { get; set; }

    public int VDCID { get; set; }
    public int WOIHID { get; set; }

    public DataTable dt { get; set; }

    public int DCSubTotalQty { get; set; }


    //Issue Material Properties
    public string SIWeight { get; set; }
    public string SILength { get; set; }
    public string SIWidth { get; set; }
    public string RtnQty { get; set; }
    public string RtnWidth { get; set; }
    public string RtnLength { get; set; }
    public DateTime MaterialRtnDate { get; set; }
    public int JCHID { get; set; }
    public string BalanceMRNLayOut { get; set; }

    //Internal DC

    public int IDCID { get; set; }
    public int FromUnit { get; set; }
    public int ToUnit { get; set; }
    public int Duration { get; set; }
    public string Remarks { get; set; }
    public int CreatedBy { get; set; }


    public int IDCMIDID { get; set; }
    public string ItemDescription { get; set; }
    public int IssuedQty { get; set; }
    public int ReturnableQty { get; set; }

    public int ReceiverID { get; set; }

    public int AttachementTypeName { get; set; }
    public string Description { get; set; }
    public string AttachementName { get; set; }
    public int CID { get; set; }
    public int MTID { get; set; }
    public string RequiredWeight { get; set; }
    public DateTime DeliveryDate { get; set; }
    public object MTFIDs { get; set; }
    public object MTFIDsValue { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int OldMRNID { get; set; }
    public string CustomerMaterial { get; set; }
    public int MaterialRequestQC { get; set; }

    public decimal StockQty { get; set; }
    public int QtyInNumbers { get; set; }
    public string DocumentName { get; set; }
    public decimal UnitCost { get; set; }
    public int TransferFromLocation { get; set; }
    public int TransferToLocation { get; set; }
    public string MRNCuttingLayout { get; set; }
    public DateTime TransferDate { get; set; }

    public Decimal Quantity { get; set; }
    public int MaterialTypeID { get; set; }
    public string Flag { get; set; }
    public string ApprovalStatus { get; set; }
    public int RJMIDID { get; set; }
    public DateTime DCDate { get; set; }
    public int SCVMID { get; set; }
    public int WorkOrderInwardHeaderID { get; set; }

    public int JCMRNID { get; set; }

    public int WOIDID { get; set; }
    public string IssuedBy { get; set; }

    public string EwayBillNo { get; set; }
    public int VDCDID { get; set; }
    public string RFPHID { get; set; }
    public decimal GeneralIssuedqty { get; set; }
    public int MRNLocationID { get; set; }
    public string GMIDID { get; set; }
    public int CompanyID { get; set; }
    public string ToAddress { get; set; }
    public int OTHID { get; set; }
    public int UMID { get; set; }
    public string ToUnitName { get; set; }
    public string ExpectedDuration { get; set; }
    public int OTDID { get; set; }

    #endregion

    #region"Common Methods"
    public DataSet GetAddedMaterials()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAddedMaterials";
            c.Parameters.AddWithValue("@MIHID", MIHID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetMaterialInwardBySUPIDAndLocationID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialInwardDetails";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInwardStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateInwardStatus";
            c.Parameters.Add("@MIHID", MIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet SaveMaterialInward()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialInward";

            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@LocationID", LocationID);

            c.Parameters.Add("@MIHID", MIHID);
            c.Parameters.Add("@DCNumber", DCNumber);
            c.Parameters.Add("@DCdate", DCdate);
            c.Parameters.Add("@InvoiceDate", InvoiceDate);
            c.Parameters.Add("@InvoiceNumber", InvoiceNumber);
            c.Parameters.Add("@DCCopy", DBNull.Value);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMIDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMIDetails";
            c.Parameters.Add("@MIHID", MIHID);
            c.Parameters.Add("@DCQty", DcQty);
            c.Parameters.Add("@SPODID", SPODID);

            if (AttachementName == "")
                c.Parameters.Add("@AttachementName", DBNull.Value);
            else
                c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@RJMIDID", RJMIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getSupplierPOItemDetailsBySPODIDs()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialInwardItemDetailsBySPODID";
            c.Parameters.Add("@SPOID", SPOID);
            c.Parameters.Add("@MIHID", MIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockInwardDetailsByMRNID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockInwardDetailsByMRNID";
            //c.Parameters.Add("@MRNID", MRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveStockInwardDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStockInwardDetails";
            c.Parameters.Add("@SIID", SIID);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@Qty", Qty);
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@LocationID", LocationID);
            c.Parameters.Add("@ReceiptDate", ReceiptDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveStockInwardCertificateDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStockInwardCertificates";
            c.Parameters.Add("@SIID", SIID);
            c.Parameters.Add("@CertificateID", CertificateID);
            c.Parameters.Add("@CertificateNo", CertificateNo);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockInwardCertificatesDetailsBySIID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockInwardCertificatesDetailsBySIID";
            c.Parameters.Add("@SIID", SIID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet CheckCertificatesForThisStockINward()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_CheckCertificatesForStockInward";
            c.Parameters.Add("@SIID", SIID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateSIStatusBySIID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSIStatusBySIID";
            c.Parameters.Add("@SIID", SIID);
            c.Parameters.Add("@ReceiptDate", DBNull.Value);
            c.Parameters.Add("@UserID", UserID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteCertificatesBySICFID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCertificatesBySICFID";
            c.Parameters.Add("@SICFID", SICFID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockMonitorReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockMonitorReportDetails";
            c.Parameters.Add("@Mode", Mode);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockMonitorReportDetailsbyMRNID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockMonitorReportDetailsbyMRNID";
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet saveMaterialPlanningMRNDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialPlanningMRNDetails";
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@blockedweight", blockedweight);
            if (RequiredShape != "")
                c.Parameters.Add("@RequiredShape", RequiredShape);
            else
                c.Parameters.Add("@RequiredShape", DBNull.Value);
            c.Parameters.Add("@UserID", UserID);
            if (LocationID == 0)
                c.Parameters.Add("@LocationID", DBNull.Value);
            else
                c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialPlanningMRNDetailsByMPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlaningMRNDetailsByMPID";
            c.Parameters.Add("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string DeleteMaterialPlanningMRNDetails()
    {
        string str = "";
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialPlanningMRNDetails";
            c.Parameters.Add("@MPMD", MPMD);
            str = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return str;
    }

    public DataSet SaveWorkOrderInward()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderInwardHeader";
            c.Parameters.Add("@VDCID", VDCID);
            c.Parameters.Add("@SCVMID", SCVMID);
            c.Parameters.Add("@InvoiceDate", InvoiceDate);
            c.Parameters.Add("@InvoiceNumber", InvoiceNumber);
            c.Parameters.Add("@WorkOrderInwardHeaderID", WorkOrderInwardHeaderID);
            c.Parameters.Add("@DCNumber", DCNumber);
            c.Parameters.Add("@DCDate", DCDate);
            c.Parameters.Add("@CreatedBy", UserID);
            c.Parameters.Add("@EwayBillNo", EwayBillNo);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetWorkOrderInwardByVDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderInwardHeaderDetailsByVDCID";
            c.Parameters.Add("@VDCID", VDCID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetIndentDetailsBYDCNumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetIndentDetailsByVDCID";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkOrderInwardDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderInwardDetails";
            c.Parameters.AddWithValue("@tblWorkOrderInwardDetails", dt);
            c.Parameters.AddWithValue("@VDCID", @VDCID);
            c.Parameters.AddWithValue("@WorkOrderInwardHeaderID", WorkOrderInwardHeaderID);
            //c.Parameters.AddWithValue("@DCSubTotalQty", DCSubTotalQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSheetMarkingAndCuttingJobCardDetailsByMaterialIssued";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMaterialIssueDetails(bool MtrnDate)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateMaterialIssueDetails";
            c.Parameters.AddWithValue("@JCHID", JCHID);
            c.Parameters.AddWithValue("@SIWeight", SIWeight);
            c.Parameters.AddWithValue("@SILength", SILength);
            c.Parameters.AddWithValue("@SIWidth", SIWidth);
            if (MtrnDate)
                c.Parameters.AddWithValue("@MaterialRtnDate", MaterialRtnDate);
            else
                c.Parameters.AddWithValue("@MaterialRtnDate", DBNull.Value);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet saveMaterialReturnDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateMaterialReturnDetailsByJCHID";
            //c.Parameters.AddWithValue("@JCHID", JCHID);
            c.Parameters.AddWithValue("@JCMRNID", JCMRNID);
            c.Parameters.AddWithValue("@IssuedBy", IssuedBy);
            //c.Parameters.AddWithValue("@RtnQty", RtnQty);
            //c.Parameters.AddWithValue("@RtnWidth", RtnWidth);
            //c.Parameters.AddWithValue("@RtnLength", RtnLength);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getInternalDCDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInternalDCDetails";
            c.Parameters.AddWithValue("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveInternalDCDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveInternalDCDetails";
            c.Parameters.AddWithValue("@IDCID     ", IDCID);
            c.Parameters.AddWithValue("@FromUnit  ", FromUnit);
            c.Parameters.AddWithValue("@ToUnit    ", ToUnit);
            c.Parameters.AddWithValue("@Duration  ", Duration);
            c.Parameters.AddWithValue("@Remarks   ", Remarks);
            c.Parameters.AddWithValue("@CreatedBy ", CreatedBy);
            c.Parameters.AddWithValue("@ReceiverID", ReceiverID);
            c.Parameters.AddWithValue("@CompanyID", CompanyID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveInternalDCmaterialIssueDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveInternalDCMaterialIssueDetails";
            c.Parameters.AddWithValue("@IDCID          ", IDCID);
            c.Parameters.AddWithValue("@IDCMIDID       ", IDCMIDID);
            c.Parameters.AddWithValue("@ItemDescription", ItemDescription);
            c.Parameters.AddWithValue("@IssuedQty      ", IssuedQty);
            c.Parameters.AddWithValue("@ReturnableQty  ", ReturnableQty);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getInternalDCmaterialIssueDetails(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@IDCID", IDCID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInternalDCStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateShareDCStatusByIDCID";
            c.Parameters.AddWithValue("@IDCID", IDCID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveReceivedDCDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveReceivedDCDetails";
            c.Parameters.AddWithValue("@tblReceivedDCDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateinternalDCMaterialIssueDetailsByIDCMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateinternalDCMaterialIssueDetailsByIDCMIDID";
            c.Parameters.AddWithValue("@IDCMIDID", IDCMIDID);
            c.Parameters.AddWithValue("@ReturnableQty", ReturnableQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet getInternalDCAndItemDetailsByDCSharedStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInternalDCAndItemDetailsByIDCID";
            c.Parameters.AddWithValue("@IDCID", IDCID);
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
        cSales objSales = new cSales();
        try
        {
            MaxAttachementID = objSales.GetMaximumAttachementID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return MaxAttachementID;
    }

    public DataSet SaveMaterialInwardAttachements()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialInwardAttachements";
            c.Parameters.Add("@MIHID", MIHID);
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

    public DataSet GetAttachementDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@MIHID", MIHID);
            c.CommandText = "LS_GetAttachementsDetailsByMIHID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteAttachementDetailsByAttachementID(int AttachementID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@AttachementID", AttachementID);
            c.CommandText = "LS_DeleteMaterialInwardAttachementDetailsByAttachementID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveIMStockDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveIMStockDetails";
            c.Parameters.Add("@SPODID", SPODID);
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@MGMID", MGMID);
            c.Parameters.Add("@THKID", THKID);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@MTID", MTID);
            c.Parameters.Add("@RequiredWeight", RequiredWeight);
            c.Parameters.Add("@ExpiryDate", ExpiryDate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@LocationID", LocationID);
            c.Parameters.Add("@CustomerMaterial", CustomerMaterial);
            c.Parameters.Add("@OldMRNID", OldMRNID);
            c.Parameters.Add("@MTFIDs", MTFIDs);
            c.Parameters.Add("@MTFIDsValue", MTFIDsValue);
            if (AttachementName == "")
                c.Parameters.Add("@AttachementName", AttachementName);
            else
                c.Parameters.Add("@AttachementName", DBNull.Value);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@MaterialRequestQC", MaterialRequestQC);

            c.Parameters.Add("@QtyInNumbers", QtyInNumbers);
            c.Parameters.Add("@DocumentName", DocumentName);
            c.Parameters.Add("@CompanyID", CompanyID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetIMStockDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetIMStockDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveStockAvailabilityDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStockAvailabilityDetails";
            c.Parameters.Add("@StockQty", StockQty);
            c.Parameters.Add("@UnitCost", UnitCost);
            //c.Parameters.Add("@QtyInNumbers", QtyInNumbers);
            //c.Parameters.Add("@DocumentName", DocumentName);
            //c.Parameters.Add("@ReceiptDate", ReceiptDate);
            //c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@LOcationID", LocationID);
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@SPODID", SPODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetIMStockAvailabilityDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetIMStockAvailabilityDetailsByMRNID";
            c.Parameters.Add("@MRNID", MRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockMonitorDetailsByLocationID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockMonitorDetailsByLocationID";
            c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveStockTransferApprovalDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStockTransferDetails";
            c.Parameters.Add("@TransferFromLocation", TransferFromLocation);
            c.Parameters.Add("@TransferToLocation", TransferToLocation);
            if (string.IsNullOrEmpty(MRNCuttingLayout))
                c.Parameters.Add("@MRNCuttingLayout", DBNull.Value);
            else
                c.Parameters.Add("@MRNCuttingLayout", MRNCuttingLayout);
            c.Parameters.Add("@Quantity", Quantity);
            c.Parameters.Add("@SPODID", SPODID);
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@MTID", MaterialTypeID);
            c.Parameters.Add("@MTFIDs", MTFIDs);
            c.Parameters.Add("@MTFIDsValue", MTFIDsValue);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet CheckStockTransferAprovalStatusByMRNNumberAndLocationID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@LocationID", TransferFromLocation);
            c.Parameters.Add("@MRNID", MRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMaterialReturnDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStockTransferMaterialReturnDetails";
            c.Parameters.Add("@TransferFromLocation", TransferFromLocation);
            if (string.IsNullOrEmpty(MRNCuttingLayout))
                c.Parameters.Add("@MRNCuttingLayout", DBNull.Value);
            else
                c.Parameters.Add("@MRNCuttingLayout", MRNCuttingLayout);
            c.Parameters.Add("@SPODID", SPODID);
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@MTID", MaterialTypeID);
            c.Parameters.Add("@MTFIDs", MTFIDs);
            c.Parameters.Add("@MTFIDsValue", MTFIDsValue);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockTransferDetailsByMRNIDAndLocationID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockTransferDetailsByMRNIDAndLoCationID";
            c.Parameters.Add("@LocationID", TransferFromLocation);
            c.Parameters.Add("@MRNID", MRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockReturnDetailsByMRNIDAndLocationID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockTransferReturnDetailsByMRNIDAndLocationID";
            c.Parameters.Add("@LocationID", TransferFromLocation);
            c.Parameters.Add("@MRNID", MRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteStockTransferDetailsBySIID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteStockTransferDetailsBySIID";
            c.Parameters.Add("@SIID", SIID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateStockTransferApprovalStatus(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@TransferFromLocation", TransferFromLocation);
            c.Parameters.Add("@Flag", Flag);
            if (SIID == 0)
                c.Parameters.Add("@SIID", DBNull.Value);
            else
                c.Parameters.Add("@SIID", SIID);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockTransferApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockTransferApprovalDetails";
            c.Parameters.Add("@ApprovalStatus", ApprovalStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStockTransferInwardDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStockTransferInwardDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorDCDetailsByVendorByVendorNumber()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCDetailsByVDCID";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderInwardedDetailsByVDCIDAndWOinwardHeaderID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderInwardedDetailsByVDCIDAndWOInwardHeaderID";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            c.Parameters.AddWithValue("@WorkOrderInwardHeaderID", WorkOrderInwardHeaderID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateVendorDCStatusByVDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateVendorDCStatusByVDCID";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteWorkOrderInwardDetailsByWOIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderInwardDetailsByWOIDID";
            c.Parameters.AddWithValue("@WOIDID", WOIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateShareWorkOrderInward()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateShareWorkorderInward";
            c.Parameters.AddWithValue("@WorkOrderInwardHeaderID", WorkOrderInwardHeaderID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetIMStockDetailsEdit()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetIMStockDetailsBySPODID";
            c.Parameters.AddWithValue("@SPODID", SPODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateIMSharedStatusBySPODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateIMSharedStatusBySPODID";
            c.Parameters.AddWithValue("@SPODID", SPODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInwardStatusByMRNID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateShareIMStockInwardStatusByMRNID";
            c.Parameters.AddWithValue("@MRNID", MRNID);
            c.Parameters.AddWithValue("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteStockAvailabilityDetailsBySIID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteStockAvailabilityDetailsBySIID";
            c.Parameters.AddWithValue("@SIID", SIID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteInternalDCDetailsByIDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteInterNalDCDetailsByIDCMIDID";
            c.Parameters.AddWithValue("@IDCMIDID", IDCMIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteInternalDCByIDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteInternalDCByIDCID";
            c.Parameters.AddWithValue("@IDCID", IDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveOTHERDCHeader()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveOTHERDCHeader";
            c.Parameters.AddWithValue("@OTHID", OTHID);

            c.Parameters.AddWithValue("@UMID", UMID);
            c.Parameters.AddWithValue("@ToUnitName", ToUnitName);

            c.Parameters.AddWithValue("@ToAddress", ToAddress);
            c.Parameters.AddWithValue("@DCDate", DCDate);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@CompanyID", CompanyID);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@ExpectedDuration", ExpectedDuration);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOtherDCHeader()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOTHERDCHeader";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetotherDCDetailsByOTHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOtherDCDetailsByOTHID";
            c.Parameters.AddWithValue("@OTHID", OTHID);
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

    public DataSet GetSPONumberBySUPIDAndLocationID(DropDownList ddlSPONumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSPONumberBySUPIDAndLocationID";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);

            ddlSPONumber.DataSource = ds.Tables[0];

            ddlSPONumber.DataTextField = "SPONumber";
            ddlSPONumber.DataValueField = "SPOID";
            ddlSPONumber.DataBind();
            ddlSPONumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetMRNNumberDetails(DropDownList ddlMRNNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNNumber";
            DAL.GetDataset(c, ref ds);
            ddlMRNNumber.DataSource = ds.Tables[0];

            ddlMRNNumber.DataTextField = "MRNNumber";
            ddlMRNNumber.DataValueField = "MRNID";
            ddlMRNNumber.DataBind();
            ddlMRNNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCertificateNameDetails(DropDownList ddlCertificates)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCertificateDetails";
            DAL.GetDataset(c, ref ds);

            ddlCertificates.DataSource = ds.Tables[0];
            ddlCertificates.DataTextField = "CertificateName";
            ddlCertificates.DataValueField = "CFID";
            ddlCertificates.DataBind();
            ddlCertificates.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMRNNumberByMGMIDAndTHKID(DropDownList ddlMRNNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNNumberByMGMIDAndTHKID";
            c.Parameters.Add("@MGMID", MGMID);
            c.Parameters.Add("@THKID", THKValue);
            DAL.GetDataset(c, ref ds);

            ddlMRNNumber.DataSource = ds.Tables[0];
            ddlMRNNumber.DataTextField = "MRNNumber";
            ddlMRNNumber.DataValueField = "MRNID";
            ddlMRNNumber.DataBind();

            ddlMRNNumber.Items.Insert(0, new ListItem("--Select--", "-1"));
            ddlMRNNumber.Items.Insert(1, new ListItem("PurchaseIndent", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet bindUnitDetails(DropDownList ddlFromUnit, DropDownList ddlToUnit, DropDownList ddlReceiverTo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUnitDetails";
            DAL.GetDataset(c, ref ds);

            ddlFromUnit.DataSource = ds.Tables[0];
            ddlFromUnit.DataTextField = "Unit";
            ddlFromUnit.DataValueField = "UMID";
            ddlFromUnit.DataBind();
            ddlFromUnit.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlToUnit.DataSource = ds.Tables[0];
            ddlToUnit.DataTextField = "Unit";
            ddlToUnit.DataValueField = "UMID";
            ddlToUnit.DataBind();
            ddlToUnit.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlReceiverTo.DataSource = ds.Tables[1];
            ddlReceiverTo.DataTextField = "EmployeeName";
            ddlReceiverTo.DataValueField = "EmployeeID";
            ddlReceiverTo.DataBind();
            ddlReceiverTo.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialInwardAttachementTypename(DropDownList ddlTypeName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialInwardAttachementTypeName";
            DAL.GetDataset(c, ref ds);

            ddlTypeName.DataSource = ds.Tables[0];
            ddlTypeName.DataTextField = "TypeName";
            ddlTypeName.DataValueField = "MIATID";
            ddlTypeName.DataBind();
            ddlTypeName.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteWorkorderinwardHeaderByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderInwardHeaderByWOHID";
            c.Parameters.Add("@WorkOrderInwardHeaderID", WorkOrderInwardHeaderID);
            DAL.GetDataset(c, ref ds);
        }

        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateAllowPermissionVendorDcQtyByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAllowPermissionVendorDcQtyByWOIHID";
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateAllowPermissionworkorderinwardQtyByVDCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAllowPermissionworkorderinwardQtyByVDCDID";
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@VDCDID", VDCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoDetailsByGeneralMRNIssue(DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoDetailsByGeneralMRNIssue";
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

    public DataSet BindMRNNoDetailsByGeneralMRNIssue(DropDownList ddlMRNNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNNoDetailsByGeneralMRNIssue";
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

    public DataSet SaveGeneralMRNIssueDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralMRNMaterialIssueDetails";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@MRNID", MRNID);
            c.Parameters.Add("@GeneralIssuedqty", GeneralIssuedqty);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@MRNLocationID", MRNLocationID);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindGeneralMRNMaterialIssueDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralMRNMaterialIssueDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteGeneralMaterialMRNIssueDetailsByGMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteGeneralMaterialMRNIssueDetailsByGMIDID";
            c.Parameters.Add("@GMIDID", GMIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindGeneralMaterialRequestDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralMaterialRequestDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet UpdateGeneralMaterialApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateGeneralMaterialApprovalDetails";
            c.Parameters.Add("@GMIDID", GMIDID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@Flag", Flag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveOtherDCDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveOtherDCDetails";
            c.Parameters.Add("@OTHID", OTHID);
            c.Parameters.Add("@ItemDescription", ItemDescription);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@IssuedQty", IssuedQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOTHERDCIssueDetailsByOTHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOtherDCIssueDetailsByOTHID";
            c.Parameters.Add("@OTHID", OTHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteOtherDCDetailsByOTDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteOtherDCDetailsByOTDID";
            c.Parameters.Add("@OTDID", OTDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOTHERDCDetailsPRINTByOTHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOTHERDCDetailsPRINT";
            c.Parameters.AddWithValue("@OTHID", OTHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteOtherDCByOTHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteOtherDCByOTHID";
            c.Parameters.AddWithValue("@OTHID", OTHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

	public DataSet GetGRNReportDetails()
		{
			DataSet ds = new DataSet();
			try
			{
				DAL = new cDataAccess();
				c = new SqlCommand();
				c.CommandType = CommandType.StoredProcedure;
				c.CommandText = "LS_GetGRNStoctReports";
				DAL.GetDataset(c, ref ds);
			}
				catch (Exception ex)
			{
				Log.Message(ex.ToString());
			}
			return ds;
		}

	public DataSet GetGRNReportDetailsNew()
		{
			DataSet ds = new DataSet();
			try
			{
				DAL = new cDataAccess();
				c = new SqlCommand();
				c.CommandType = CommandType.StoredProcedure;
				c.CommandText = "LS_GetGRNStoctReportsNew";
				DAL.GetDataset(c, ref ds);
			}
				catch (Exception ex)
			{
				Log.Message(ex.ToString());
			}
			return ds;
		}
	
    public DataSet GetGenaralDCDetailsPRINTByGDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralDCDetailsByGDCIDForPDF";
            c.Parameters.AddWithValue("@GDCID", OTHID);
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
