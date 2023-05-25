using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using eplus.data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public class cProduction
{
    #region "Declaration"

    DataSet ds = new DataSet();
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    cSession objSession = new cSession();
    public string TubeLength;
    public string certficates;
    public string rfpchangestatus;
    public string status;

    #endregion

    #region"Properties"

    public int RFPDID { get; set; }
    public int JCMRNID { get; set; }
    public int RFPHID { get; set; }
    public int CTPDID { get; set; }
    public int EDID { get; set; }



    public int RGMID { get; set; }
    public decimal UnitRate { get; set; }
    public decimal TotalRate { get; set; }

    public decimal UnitCalculatedWeight { get; set; }
    public decimal TotalCalculatedWeight { get; set; }

    public decimal UnitAWTWeight { get; set; }
    public decimal TotalAWTWeight { get; set; }

    public string Remarks { get; set; }
    public int PJOID { get; set; }

    public int MID { get; set; }
    public string ProcessName { get; set; }
    public decimal ProcessRate { get; set; }
    public int PMID { get; set; }

    public DataTable dt { get; set; }

    public int PRIDID { get; set; }
    public int PRPDID { get; set; }
    public int NextProcess { get; set; }

    public int MPID { get; set; }
    public int PartQty { get; set; }


    public string SecondaryJobOrderID { get; set; }
    public int CuttingProcessID { get; set; }
    public int CMID { get; set; }
    public int CTDID { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public string PRPDIDs { get; set; }

    public int PDID { get; set; }

    public DataTable MRNdt { get; set; }

    public int JCHID { get; set; }

    public int JCDID { get; set; }
    public string QCStatus { get; set; }

    public int procesOrder { get; set; }

    public string joborderRemarks { get; set; }

    public int WPSID { get; set; }

    public int BOMID { get; set; }
    public string BellowFormType { get; set; }
    public string TangetCuttingType { get; set; }

    public Decimal SheetMarkingCost { get; set; }
    public Decimal FormingCost { get; set; }
    public Decimal TangetCost { get; set; }
    public Decimal BellowCalculatedWeight { get; set; }

    public string Flag { get; set; }
    public int Stages { get; set; }

    public string RollFormingDevelopementPitch { get; set; }
    public string RollFormingInitialDepth { get; set; }

    public string ExpandalDevelopementPitch { get; set; }
    public string ExpandalFinalOver { get; set; }
    public int RFPQPDID { get; set; }

    public int WOID { get; set; }
    public string jobDescription { get; set; }
    public string AttachementName { get; set; }

    public int WPOID { get; set; }

    public int CreatedBy { get; set; }

    public int UserID { get; set; }

    public string RFPNoList { get; set; }
    public int PID { get; set; }
    public string TubeId { get; set; }
    public string MaterialissuedStatus { get; set; }

    public string JCDIDs { get; set; }
    public string ContractorName { get; set; }

    public string FabricationType { get; set; }
    public string FabricationTypeValues { get; set; }
    public decimal AmtInPercentage { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int CPDID { get; set; }
    public string CPDIDs { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal GeneralConsumable { get; set; }
    public string BillNo { get; set; }
    public DateTime BillDate { get; set; }
    public int CPBDID { get; set; }
    public int RawMaterialQuantity { get; set; }
    public string WOjobListID { get; set; }
    public int WOIHID { get; set; }
    public string JobOrderRemarks { get; set; }
    public string USERID { get; set; }
    public int PAPDID { get; set; }
    public int JobQty { get; set; }

    public decimal JobWeight { get; set; }

    public int PPDID { get; set; }

    public decimal CMPPercentage { get; set; }
    public decimal OSF { get; set; }
    public decimal CSF { get; set; }
    public decimal CurrentMonthDispatchValue { get; set; }
    public decimal OpenProjectExpensesValue { get; set; }

    public decimal ClosingProjectValue { get; set; }

    public int PEEID { get; set; }
    public int ItemQTY { get; set; }

    public int PRDID { get; set; }
    public int IndentBy { get; set; }
    public int MTID { get; set; }
    public string Type { get; set; }
    public string MTFIDs { get; set; }
    public string MTFIDsValue { get; set; }
    public string DrawingName { get; set; }
    public string PurchaseCopy { get; set; }
    public string UOM { get; set; }
    public string MPIDsAndReqQty { get; set; }
    public decimal ReqQty { get; set; }
    public string MPIDs { get; set; }
    public string CurrentStatus { get; set; }
    public string ReScheduledSubmissiondate { get; set; }
    public string RescheduledateReason { get; set; }
    public DateTime ReScheduledDate { get; set; }
    public int ETCID { get; set; }
    public int DDID { get; set; }
    public int JCPNMID { get; set; }
    public decimal Rate { get; set; }

    public int RCMID { get; set; }
    public decimal Qty { get; set; }

    public int CJPCDID { get; set; }
    public int AmountPercentage { get; set; }
    public string QCPlan { get; set; }
    public string LocationID { get; set; }
    public string txtAmountPercentage { get; set; }

    #endregion

    #region"Common Methods"

    public DataSet GetPartDetailsByEDIDAndRFPHIDAndRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartNameByEDIDAndRFPHIDAndRFPDIDInJobOrder";
            c.Parameters.Add("@EDID", EDID);
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

    public DataSet GetPrimaryJobOrderDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPrimaryJobOrderDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SavePrimaryJobOrderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePrimaryJobOrderDetails";
            c.Parameters.Add("@PJOID", PJOID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);

            c.Parameters.Add("@RGMID", RGMID);

            //c.Parameters.Add("@UnitRate", UnitRate);
            //c.Parameters.Add("@TotalRate", TotalRate);
            //c.Parameters.Add("@TotalCalculatedWeight", TotalCalculatedWeight);
            //c.Parameters.Add("@UnitCalculatedWeight", UnitCalculatedWeight);

            c.Parameters.Add("@UnitAWTWeight", UnitAWTWeight);
            c.Parameters.Add("@TotalAWTWeight", TotalAWTWeight);

            c.Parameters.Add("@Remarks", Remarks);

            c.Parameters.Add("@tblPartProcessDetails", dt);
            c.Parameters.Add("@ItemQTY", ItemQTY);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartProcessMasterDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartProcessMasterDetails";
            c.Parameters.Add("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SavePartProcessName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartProcessName";
            c.Parameters.Add("@ProcessName", ProcessName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePartProcessDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartProcessDetails";
            c.Parameters.Add("@tblProcessDetail", dt);
            c.Parameters.Add("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet DeletePartProcessNameByPMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeletePartProcessNameByPMID";
            c.Parameters.Add("@PMID", PMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProductionItemNameDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionItemNameDetailsByRFPHID";
            //c.Parameters.Add("@PMID", PMID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartDetailsByPRIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemPartSerielNoByPRIDID";
            c.Parameters.Add("@PRIDID", PRIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet bindJobCardDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobOrderDetails";
            c.Parameters.Add("@PRPDID", PRPDID);
            c.Parameters.Add("@NextProcess", NextProcess);
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMRNDetailsAndPartSNoDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMRNAndPartSNODetailsByMPIDAndPartQty";
            //c.Parameters.Add("@PartQty", PartQty);
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@NextProcess", NextProcess);
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardDetails";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@SecondaryJobOrderID", SecondaryJobOrderID);
            c.Parameters.Add("@CuttingProcessID", CuttingProcessID);
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@IssueDate", IssueDate);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@DeliveryDate", DeliveryDate);
            c.Parameters.Add("@PartQty", PartQty);
            c.Parameters.Add("@NextProcess", NextProcess);
            c.Parameters.Add("@PDID", PDID);
            c.Parameters.Add("@PRPDID", PRPDIDs);
            //  if (MRNdt.Rows.Count > 0)
            c.Parameters.Add("@MRNdt", MRNdt);
            //else
            //    c.Parameters.Add("@MRNdt", DBNull.Value);
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@joborderRemarks", joborderRemarks);
            c.Parameters.Add("@UserID", UserID);

            if (AttachementName == "")
                c.Parameters.Add("@AttachementName", DBNull.Value);
            else
                c.Parameters.Add("@AttachementName", AttachementName);

            if (FabricationType == "")
            {
                c.Parameters.Add("@FabricationType", DBNull.Value);
                c.Parameters.Add("@FabricationTypeValues", DBNull.Value);
            }
            else
            {
                c.Parameters.Add("@FabricationType", FabricationType);
                c.Parameters.Add("@FabricationTypeValues", FabricationTypeValues);
            }
            if (ProcessName == "Marking & Cutting")
            {
                c.Parameters.Add("@TubeId", DBNull.Value);
                c.Parameters.Add("@TubeLength", DBNull.Value);
            }
            else
            {
                c.Parameters.Add("@TubeId", TubeId);
                c.Parameters.Add("@TubeLength", TubeLength);
            }

            //c.Parameters.Add("@MaterialissuedStatus", MaterialissuedStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardHeaderDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardHeaderDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@rfpchangestatus", rfpchangestatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getEditJobCardPartDetailsByJCHID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardQCClearenceDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardQCClearenceDetails";
            c.Parameters.Add("@JCDID", JCDID);
            c.Parameters.Add("@QCStatus", QCStatus);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProductionPartDetailsByMPIDAndProcessOrder()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionPartDetailsByMPIDAndProcessOrder";
            c.Parameters.Add("@MPID", MPID);
            if (Flag == "WorkInProgrss")
            {
                c.Parameters.Add("@JCDID", DBNull.Value);
                c.Parameters.Add("@procesOrder", procesOrder);
            }
            else
            {
                c.Parameters.Add("@JCDID", JCDID);
                c.Parameters.Add("@procesOrder", DBNull.Value);
            }
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveQcRequestDetails(string PRPDIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQCRequest";
            c.Parameters.Add("@MPID", MPID);
            //c.Parameters.Add("@PRPDID", PRPDID);
            c.Parameters.Add("@procesOrder", procesOrder);
            c.Parameters.Add("@PRPDIDs", PRPDIDs);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardDetailsByMPIDANdProcessOrder()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardDetailsByMPIDAndProcessOrder";
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWPSDetailsByWPSID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWPSMasterDetailsByWPSID";
            c.Parameters.Add("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardSheetWeldingDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardSheetWeldingDetails";
            c.Parameters.Add("@WPSID", WPSID);
            c.Parameters.Add("@SecondaryJobOrderID", SecondaryJobOrderID);
            c.Parameters.Add("@IssueDate", IssueDate);
            c.Parameters.Add("@DeliveryDate", DeliveryDate);
            c.Parameters.Add("@PDID", PDID);
            c.Parameters.Add("@NextProcess", NextProcess);
            c.Parameters.Add("@joborderRemarks", joborderRemarks);
            c.Parameters.Add("@PRPDID", PRPDIDs);
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@PartQty", PartQty);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@MPID", MPID);
            if (AttachementName == "")
                c.Parameters.Add("@AttachementName", DBNull.Value);
            else
                c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@CreatedBy", CreatedBy);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBellowsCostDetailsByBOMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBellowsCostDetailsByBOMID";
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@FormType", BellowFormType);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBellowFormingCostDetailsbyFormType()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBellowFormingCostByRollOrExpandal";
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@FormType", BellowFormType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetTangetCuttingCostByCuttingType()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBellowTangetCuttingCostByCuttingType";
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@CuttingType", TangetCuttingType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveBellowCostDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveBellowCostDetails";
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@PJOID", PJOID);
            c.Parameters.Add("@SheetMarkingCost", SheetMarkingCost);
            c.Parameters.Add("@FormingCost", FormingCost);
            c.Parameters.Add("@TangetCost", TangetCost);
            c.Parameters.Add("@FormType", BellowFormType);
            c.Parameters.Add("@TangetCuttingType", TangetCuttingType);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@BellowCalculatedWeight", BellowCalculatedWeight);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStagesInBellowTangetCuttingRoll()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStagesInBellowTangetCuttingRoll";
            c.Parameters.Add("@Stages", Stages);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveBellowTangentCuttingDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveBellowsTangentCuttingDetails";
            c.Parameters.Add("@SecondaryJobOrderID", SecondaryJobOrderID);
            c.Parameters.Add("@IssueDate", IssueDate);
            c.Parameters.Add("@DeliveryDate", DeliveryDate);
            c.Parameters.Add("@PDID", PDID);
            c.Parameters.Add("@NextProcess", NextProcess);
            c.Parameters.Add("@joborderRemarks", joborderRemarks);
            c.Parameters.Add("@PRPDID", PRPDIDs);
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@PartQty", PartQty);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@MPID", MPID);

            if (BellowFormType == "Roll")
            {
                c.Parameters.Add("@RollFormingDevelopementPitch", RollFormingDevelopementPitch);
                c.Parameters.Add("@RollFormingInitialDepth", RollFormingInitialDepth);
                c.Parameters.Add("@ExpandalDevelopementPitch", DBNull.Value);
                c.Parameters.Add("@ExpandalFinalOver", DBNull.Value);
                c.Parameters.Add("@Stages", Stages);
            }
            else
            {
                c.Parameters.Add("@RollFormingDevelopementPitch", DBNull.Value);
                c.Parameters.Add("@RollFormingInitialDepth", DBNull.Value);
                c.Parameters.Add("@Stages", DBNull.Value);
                //c.Parameters.Add("@tblBellowStages", DBNull.Value);
                c.Parameters.Add("@ExpandalDevelopementPitch", ExpandalDevelopementPitch);
                c.Parameters.Add("@ExpandalFinalOver", ExpandalFinalOver);
            }
            c.Parameters.Add("@tblBellowStages", dt);
            c.Parameters.Add("@BellowFormType", BellowFormType);

            if (AttachementName == "")
                c.Parameters.Add("@AttachementName", DBNull.Value);
            else
                c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQualityProcessNameDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQualityPlanningProcessDetailsBYJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            //c.Parameters.Add("@JCDID", JCDID);
            c.Parameters.Add("@JCDID", DBNull.Value);
            c.Parameters.Add("@ProcessName", ProcessName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardQCClearenceProcessStagesDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardQcClearenceProcessStagesDetails";
            c.Parameters.Add("@tblQCProcessList", dt);
            c.Parameters.Add("@JCDIDs", JCDIDs);
            c.Parameters.Add("@Flag", Flag);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            //c.Parameters.Add("@RFPQPDID", RFPQPDID);
            //c.Parameters.Add("@QCStatus", QCStatus);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateQCStatusByJCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateQCStatusByJCDID";
            c.Parameters.Add("@JCDID", JCDID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetWorkOrderIndentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentDetails";
            c.Parameters.Add("@RFPHID", RFPHID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkOrderIndentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderIndentDetails";
            c.Parameters.Add("@WOIHID", WOIHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            //c.Parameters.Add("@RFPDID", RFPDID);
            // c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@JobDescription", jobDescription);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@RawMaterialQuantity", RawMaterialQuantity);
            c.Parameters.Add("@WOjobListID", WOjobListID);
            // c.Parameters.Add("@PRPDIDs", PRPDIDs);
            c.Parameters.Add("@MPIDs", MPIDs);
            // c.Parameters.Add("@JobQty", JobQty);
            c.Parameters.Add("@JobWeight", JobWeight);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@QCPlan", QCPlan);
            //QCPlan
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndentDetailsByWPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            //c.CommandText = "LS_GetWorkOrderPODetailsByWPOID";
            c.CommandText = "LS_GetWorkOrderPODetails";
            //   c.Parameters.Add("@", );
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateRFPNOListByPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateRFPNoListByPID";
            c.Parameters.Add("@PID", PID);
            c.Parameters.Add("@RFPNOList", RFPNoList);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBlockingMRNMaterialPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBlockedMRNMaterialPlanningDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCertificateAndBomFilesName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCertificateAndBomFilesName";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@Flag", Flag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateMaterialPlanningWeightApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateMaterialPlanningWeightApprovalStatusByMPID";
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@Flag", Flag);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialPlanningDeviationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlanningDeviationDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEditJobCardDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEditJobCardPartDetailsbyJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getJobCardPartDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardPartDetailsbyJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet bindJobCardDetailsByRFPDID(DropDownList ddlContractorName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardDetailsByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);

            ddlContractorName.DataSource = ds.Tables[1];
            ddlContractorName.DataTextField = "ContractorName";
            ddlContractorName.DataValueField = "ContractorID";
            ddlContractorName.DataBind();
            ddlContractorName.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet SaveContractorPaymentdetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveContractorPaymentdetails";
            c.Parameters.Add("@tblContractorPaymentDetails", dt);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteContractorPaymentDetailsByCPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteContractorPaymentDetailsByCPDID";
            c.Parameters.Add("@CPDID", CPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateContractorPaymentRequestStatusByPaymentID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            //c.CommandText = "LS_UpdateContractorPaymentApprovalStatusByPaymentID";
            c.CommandText = spname;
            c.Parameters.Add("@CPDIDs", CPDIDs);
            c.Parameters.Add("@Flag", Flag);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPrimaryJobOrderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPrimaryJobOrderDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorPaymentApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorPaymentApprovalDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateContractorPaymentApprovalStatusByPaymentID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateContractorPaymentApprovalStatusByPaymentID";
            c.Parameters.Add("@CPDID", CPDID);
            c.Parameters.Add("@Flag", Flag);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorMonthlyPaymentDetailsByContractorTeamName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorMonthlyPaymentDetailsByContractorTeamName";
            c.Parameters.Add("@FromDate", FromDate);
            c.Parameters.Add("@ToDate", ToDate);
            c.Parameters.Add("@CTDID", CTDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveContractorPaymentBillDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveContractorPaymentBillDetails";
            c.Parameters.Add("@CPDIDs", CPDIDs);
            c.Parameters.Add("@GeneralConsumable", GeneralConsumable);
            c.Parameters.Add("@BillNo", BillNo);
            c.Parameters.Add("@BillDate", BillDate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorPaymentBillDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorBillPaymentDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorPaymentPrintDetailsByBillID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorPaymentDetailsByBillID";
            c.Parameters.Add("@CPBDID", CPBDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartSnoByMPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartSnoByMPID";
            c.Parameters.Add("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndentDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentDetailsByRFPDID";
            //c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndentDetailsByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentDetailsByWOIHID";
            c.Parameters.Add("@WOIHID", WOIHID);
            //c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteWorkorderIndentDetailsByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderIndentDetailsByWOIHID";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndentItemNameDetailsWPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentItemNameDetailsByWPOID";
            c.Parameters.Add("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartSnoByWorkOrderIndentNo()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartSnoByWorkOrderIndentNo";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePartAssemplyJobCardDetails(string PAPDID, string PRIDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartAssemplyJobCardPartDetails";
            //c.Parameters.AddWithValue("@tblPartAssemplyJobCardDetails", dt);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@PRIDID", PRIDID);
            //c.Parameters.AddWithValue("@AssemplyJobOrderID", AssemplyJobOrderID);

            c.Parameters.AddWithValue("@CMID", CMID);
            c.Parameters.AddWithValue("@CTDID ", CTDID);
            c.Parameters.AddWithValue("@IssueDate", IssueDate);
            c.Parameters.AddWithValue("@DeliveryDate ", DeliveryDate);
            c.Parameters.AddWithValue("@JobOrderRemarks", JobOrderRemarks);
            c.Parameters.AddWithValue("@PAPDID", PAPDID);
            c.Parameters.AddWithValue("@USERID", USERID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyPlanningJobCardDetailsByPRIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyPlanningJobCardDetailsByPRIDID";
            // c.Parameters.Add("@PRIDID", PRIDID);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyJobCardDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyJobCardDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteAssemplyJobCardDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteAssemplyJobCardDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBoughtOutMRNBlockedDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOIMRNBlockedDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet deleteAssemplyMRNIssueDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteAssemplyMRNIssueDetailsByJCHID";
            c.Parameters.Add("@JCMRNID", JCMRNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyMRNissuedDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyMRNIssuedDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveAssemplyMRNIssueDetails(string MRN)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveAssemplyMRNIssueDetails";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@USERID", USERID);
            c.Parameters.Add("@MRN", MRN);
            //c.Parameters.Add("@PRIDID", PRIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAssemplyJobCardPDFDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyJobCardDetailsByJCHID_PRINT";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartAssemplyPlanningJobCardDetailsByJCHID(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UPdateAssemplyJobCardDetailsCompletedStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAssemplyJobCardCompletedStatusByPRIDIDAndJCHID";
            c.Parameters.Add("@PRIDID", PRIDID);
            c.Parameters.Add("@PAPDID", PAPDID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateShareIndentToQC(string WOIHID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateShareIndentToQC";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndentDetailsByQCPlanning()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentDetailsByQCPlanning";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBoughtOutItemMRNDetailsByMPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBoughtOutItemMRNDetailsByMPID";
            c.Parameters.Add("@WOIHID", WOIHID);
            c.Parameters.Add("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkOrderBoughtOutItemMRNIssueDetails(string MRNDetails)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderBoughtOutItemMRNIssueDetails";
            // c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@WPOID", WPOID);
            c.Parameters.Add("@WOIHID", WOIHID);
            c.Parameters.Add("@MRNDetails", MRNDetails);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SharePrimaryJoborderByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SharePrimaryJoborderByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SharePrimaryJoborderByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SharePrimaryJoborderByRFPHID";
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

    public DataSet DeletePurchaseIndentDetailsByPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeletePurchaseIndentDetailsByPID";
            c.Parameters.Add("@PID", PID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet ReviewMaterialPlanningDetailsByItemName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateReviewMaterialPlanningDetailsByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateItemCompletedStatusByPRIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateItemCompletedStatusByPRIDID";
            c.Parameters.Add("@PRIDID", PRIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderIndenPrintDetailsByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkorderIndentPrintDetailsByWOIHID";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateAmendmentStatusByUserID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAmendmentStatusByUserID";
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProcessPlanningDetailsByRFPDIDAndEDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProcessPlanningDetailsByRFPDIDAndEDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePartProcessPlanningDetails(string ProcessIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartProcessPlanningDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@ProcessIDs", ProcessIDs);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProcessPlanningByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProcessPlanningByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteProcessPlanningByPPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteProcessPlanningDetailsByPPDID";
            c.Parameters.Add("@PPDID", PPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateProcessPlanningSharedStatusByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateProcessPlanningSharedStatusByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByProcessingJobOrder";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveProjectExpensesEntry()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveProjectExpensesEntryDetails";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@CMPPercentage", CMPPercentage);
            c.Parameters.Add("@OSF", OSF);
            c.Parameters.Add("@CurrentMonthDispatchValue", CurrentMonthDispatchValue);
            c.Parameters.Add("@OpenProjectExpensesValue", OpenProjectExpensesValue);
            c.Parameters.Add("@ClosingProjectValue", ClosingProjectValue);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindProjectExpensesEntryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProjectExpensesEntryDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteProjectExpensesEntryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@PEEID", PEEID);
            c.CommandText = "LS_DeleteProjectExpensesEntryDetailsByPEEID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteJobcardDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@UserID", UserID);
            c.CommandText = "LS_DeleteJobCardDetailsByJCHID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBellosnoDetailsByRFPDID(string PAPDIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@PAPDIDs", PAPDIDs);
            c.CommandText = "LS_GetBellowSnoDetailsByRFPDIDAndPAPDIDs";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPrimaryJoborderPartCostDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPDID", RFPDID);
            c.CommandText = "LS_GetPrimaryJoborderPartCostDetailsByRFPDID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeletePrimaryJobOrderPartCostDetailsByRFPDIDAndBOMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@BOMID", BOMID);
            c.CommandText = "LS_DeletePrimaryJobOrderPartCostDetailsByRFPDIDAndBOMID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPrimaryJoborderAWTDeviationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPrimaryJoborderAWTDeviationDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdatePrimaryJoborderAWTApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAWTApprovalStatusByPRDID";
            c.Parameters.Add("@PRDID", PRDID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@Flag", Flag);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemSnoByRFPHIDAndAssemplyPlanningCompleted()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemSnoByRFPDIDAndAssemplyJobCardCompleted";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateItemSnoDetailsByPRIDIDs(string PRIDIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateItemSnoDetailsByPRIDIDs";
            c.Parameters.Add("@PRIDIDs", PRIDIDs);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateItemSnoCompletedStatusByPRIDIDs(string PRIDIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateItemCompletedStatusByPRIDID";
            c.Parameters.Add("@PRIDIDs", PRIDIDs);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialplanningIndentDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlanningIndentDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveRFPPurchaseIndentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRFPPurchaseIndentDetails";
            c.Parameters.Add("@PID", PID);

            c.Parameters.Add("@RFPHID", RFPHID);

            c.Parameters.Add("@IndentBy", IndentBy);

            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@MTID", MTID);

            c.Parameters.Add("@DeliveryDate", DeliveryDate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@certficates", certficates);

            c.Parameters.Add("@Type", Type);
            c.Parameters.Add("@MTFIDs", MTFIDs);
            c.Parameters.Add("@MTFIDsValue", MTFIDsValue);

            c.Parameters.Add("@MPIDsAndReqQty", MPIDsAndReqQty);

            if (string.IsNullOrEmpty(PurchaseCopy))
                c.Parameters.Add("@PurchaseCopy", DBNull.Value);
            else
                c.Parameters.Add("@PurchaseCopy", PurchaseCopy);

            c.Parameters.Add("@DrawingName", DrawingName);
            c.Parameters.Add("@ReqQty", ReqQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQCCertificatesByMPIDs(string MPIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQCCertificatesByMPIDs";
            c.Parameters.Add("@MPIDs", MPIDs);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPIndentDetailsByPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPIndentDetailsByPIDAndRFPHID";
            c.Parameters.Add("@PID", PID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBomGradenameAndThicknessClubbingWTDetails(int EnquiryID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMMaterialGradeAndThKClubbingDetailsByEnquiryNumber";
            c.Parameters.Add("@EnquiryID", EnquiryID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GePartFormulaDetailsByMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartFormulaDetailsByMID";
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetInterNalBOMDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInternalBOMDetails";
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInterBomDetailsByBOMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInterNalBOMDetailsByBOMID";
            c.Parameters.Add("@ETCID", ETCID);
            // c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@Flag", Flag.ToLower());
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateInternalDevelopementMaterialRequestByBOMIDs(string BOMIDs, string remarks)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateInternalDevelopementApprovalByBOMIDs";
            c.Parameters.Add("@BOMIDs", BOMIDs);
            c.Parameters.Add("@Status", Flag);
            c.Parameters.Add("@remarks", remarks);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInternalDevelopementApprovalDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInternalDevelopementApprovalDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorJobCardRateDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorJobCardRateDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveContractorJobCardRateDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveContractorJobCardRateDetails";
            c.Parameters.Add("@RCMID", RCMID);
            c.Parameters.Add("@JCPNMID", JCPNMID);
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@Rate", Rate);
            c.Parameters.Add("@UserID", UserID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorJobCardRateDetailsbyRCMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorJobCardRateDetailsbyRCMID";
            c.Parameters.Add("@RCMID", RCMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteJobCardRateDetailsbyRCMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteContractorJobCardRateDetailsByRCMID";
            c.Parameters.Add("@RCMID", RCMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRateDetailsByTeamNameAndProcessName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardRateDetailsByCTDIDAndJCPNMID";
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@JCPNMID", JCPNMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardNoProcessCostDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardNoProcessCostDetailsByJCHIDAndRFPHID";
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveJobCardNoProcessCostDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveJobCardNoProcessCostDetails";
            c.Parameters.Add("@CJPCDID", CJPCDID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@JCPNMID", JCPNMID);
            c.Parameters.Add("@Qty", Qty);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardProcessCostDetailsByCJPCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardProcessCostDetailsByCJPCDID";
            c.Parameters.Add("@CJPCDID", CJPCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteJObcardDetailsByCJPCDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteJobCardDetailsByCJPCDID";
            c.Parameters.Add("@CJPCDID", CJPCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardNoProcessCostPrintDetails(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@JCHID", JCHID);
            c.Parameters.Add("@CJPCDID", CJPCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoAndJobcardNoProcessnameDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoAndJobCardNoProcessNameDetailsByJCHID";
            c.Parameters.Add("@JCHID", JCHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }



    public DataSet GetDailyProductionActivitiesRFPDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDailyActivitiesProductionDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveDailyActivitiesProductionRFPDetails(bool rsdate)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDailyActivitiesProductionDetails";
            c.Parameters.Add("@RFPHID", RFPHID);
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

    public DataSet bindJobCardActualExpensesDetails(DropDownList ddlContractorName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            //c.CommandText = "LS_GetJobCardDetailsByRFPDID";
            c.CommandText = "LS_JobCardActualExpensesSheetDetailsByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);

            ddlContractorName.DataSource = ds.Tables[2];
            ddlContractorName.DataTextField = "ContractorName";
            ddlContractorName.DataValueField = "ContractorID";
            ddlContractorName.DataBind();
            ddlContractorName.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorPaymentDetailsByCMIDAndCTDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetContractorPaymentDetailsByCMIDAndCTDIDAndRFPDID";
            // c.CommandText = "LS_GetContractorJobCardPaymentDetailsByCTDIDAndRFPDID";
            c.CommandText = "LS_GetContractorJobCardPaymentDetailsByRFPDID";
            //c.Parameters.Add("@CMID", CMID);
            //c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProductionEmployeeList()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionEmployeeListDetails";
            c.Parameters.Add("@HeadID", USERID);
            c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQualityEmployeeList()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQualityEmployeeListDetails";
            c.Parameters.Add("@HeadID", USERID);
            c.Parameters.Add("@LocationID", LocationID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartTopartAssemplyJobCardPrintDetailsByJCHIDAndPAPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartToPartAssemplyJobCardPrintDetailsByJCHIDAndPAPDID";
            c.Parameters.Add("@JCHID",JCHID);
            c.Parameters.Add("@PAPDID",PAPDID);
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

    public DataSet GetRateGroupingMasterDetails(DropDownList ddlRateGroup)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRateGroupMasterDetails";
            DAL.GetDataset(c, ref ds);

            ddlRateGroup.DataSource = ds.Tables[0];
            ddlRateGroup.DataTextField = "GroupName";
            ddlRateGroup.DataValueField = "RGMID";
            ddlRateGroup.DataBind();
            ddlRateGroup.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }


    public DataSet GetRFPDetailsByUserIDAndMPStatusCompleted(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByUserIDAndMPStatusCompleted";
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


    public DataSet GetRFPDetailsByUserIDAndPJOCompleted(int UserID, DropDownList ddlRFPNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByUserIDAndPrimaryJobOrderCompleted";
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

    public DataSet GetContractorDetails(DropDownList ddlContractorName, DropDownList ddlContractorTeamMemberList, DropDownList ddlCutingProcess)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorDetails";
            DAL.GetDataset(c, ref ds);

            if (ddlContractorName != null)
            {
                ddlContractorName.DataSource = ds.Tables[0];
                ddlContractorName.DataTextField = "CONTRACTORNAME";
                ddlContractorName.DataValueField = "CMID";
                ddlContractorName.DataBind();
                ddlContractorName.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            ddlContractorTeamMemberList.DataSource = ds.Tables[1];
            ddlContractorTeamMemberList.DataTextField = "Name";
            ddlContractorTeamMemberList.DataValueField = "CTDID";
            ddlContractorTeamMemberList.DataBind();
            ddlContractorTeamMemberList.Items.Insert(0, new ListItem("--Select--", "0"));

            if (ddlCutingProcess != null)
            {
                ddlCutingProcess.DataSource = ds.Tables[2];
                ddlCutingProcess.DataTextField = "ProcessName";
                ddlCutingProcess.DataValueField = "CuttingProcessID";
                ddlCutingProcess.DataBind();
                ddlCutingProcess.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWPSDetailsbyWPSNumber(DropDownList ddlWPSNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWPSDetails";
            DAL.GetDataset(c, ref ds);
            ddlWPSNumber.DataSource = ds.Tables[0];
            ddlWPSNumber.DataTextField = "WPSNumber";
            ddlWPSNumber.DataValueField = "WPSId";
            ddlWPSNumber.DataBind();
            ddlWPSNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetContractorNameAndTeamNameDetails(DropDownList ddlContractorName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorNameAndTeamNameDetails";
            DAL.GetDataset(c, ref ds);
            ddlContractorName.DataSource = ds.Tables[0];
            ddlContractorName.DataTextField = "ContractorName";
            ddlContractorName.DataValueField = "ContractorID";
            ddlContractorName.DataBind();
            ddlContractorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPCustomerNameByMPStatusCompleted(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPCustomerNameByUserIDAndMPStatusCompleted";
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

    public DataSet GetBoughtOutItemPONumberByWOIHID(DropDownList ddlPONo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOIPONumberByWOIHID";
            c.Parameters.Add("@WOIHID", WOIHID);
            DAL.GetDataset(c, ref ds);

            ddlPONo.DataSource = ds.Tables[0];
            ddlPONo.DataTextField = "PONo";
            ddlPONo.DataValueField = "WPOID";
            ddlPONo.DataBind();
            ddlPONo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartDetailsByRFPDID(DropDownList ddlpartname, ListBox LijoboperationList)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartNameDetailsByRFPDID";
            //c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
            //ddlpartname.DataSource = ds.Tables[0];
            //ddlpartname.DataTextField = "PartName";
            //ddlpartname.DataValueField = "MPID";
            //ddlpartname.DataBind();
            //ddlpartname.Items.Insert(0, new ListItem("--Select--", "0"));

            LijoboperationList.DataSource = ds.Tables[1];
            LijoboperationList.DataTextField = "ProcessName";
            LijoboperationList.DataValueField = "WOJOLID";
            LijoboperationList.DataBind();
            LijoboperationList.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPDetailsByUserIDForPrimaryJoborder(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByUserIDForPrimaryJoborder";
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

    public DataSet getRFPCustomerNameByUserIDForPrimaryJoborder(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPCustomerNameByUserIDForPrimaryJoborder";
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

    public DataSet getRFPCustomerNameByUserIDAndPJOCompleted(int UserID, DropDownList ddlCustomerName, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPCustomerNameByUserIDAndPJOCompleted";
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

    public DataSet GetRFPCustomerNameByUserIDAndReadyToDispatch(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPCustomerNameByUserIDAndAssemplyJobcardCompleted";
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

    public DataSet GetRFPDetailsByUserIDAndReadyToDispatch(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_RFPDetailsByUserIDAndReadyToDispatch";
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

    public DataSet getCustomerNameByUserIDForProduction(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerNameByEmployeeIDForProduction";
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

    public DataSet GetEnquiryNumberByUserIDForProduction(int UserID, DropDownList ddlEnquiryNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDByUserIDForProduction";
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

    public DataSet getRFPCustomerNameByUserID(int UserID, DropDownList ddlCustomerName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPCustomerNameByUserID";
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

    public DataSet GetRFPDetailsByUserID(int UserID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetailsByUserID";
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


    public DataSet SaveDailyActivitiesProductionRFP()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDailyActivitiesProductionRFPDetails";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@CurrentStatus", CurrentStatus);
            c.Parameters.Add("@ReScheduledSubmissiondate", ReScheduledSubmissiondate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@CreatedBy", UserID);
            c.Parameters.Add("@RescheduledateReason", RescheduledateReason);
            c.Parameters.Add("@ReScheduledDate", ReScheduledDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetInternalItemDetails(DropDownList ddlItemName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetInterNalItemDetails";
            DAL.GetDataset(c, ref ds);

            ddlItemName.DataSource = ds.Tables[0];
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "ItemID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartNameDetails(DropDownList ddlMaterialName)
    {

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartNameDetailsForInterNalDevelopement";

            DAL.GetDataset(c, ref ds);
            ddlMaterialName.DataSource = ds.Tables[0];
            ddlMaterialName.DataTextField = "MaterialName";
            ddlMaterialName.DataValueField = "MID";
            ddlMaterialName.DataBind();
            ddlMaterialName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobCardProcessNameDetails(DropDownList ddlJobCardProcessName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobCardProcessNameDetails";

            DAL.GetDataset(c, ref ds);
            ddlJobCardProcessName.DataSource = ds.Tables[0];
            ddlJobCardProcessName.DataTextField = "JobName";
            ddlJobCardProcessName.DataValueField = "JCPNMID";
            ddlJobCardProcessName.DataBind();
            ddlJobCardProcessName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetJobOrderNoDetailsByRFPHID(DropDownList ddlJobCardNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetJobOrderNoDetailsByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);

            ddlJobCardNo.DataSource = ds.Tables[0];
            ddlJobCardNo.DataTextField = "JobNo";
            ddlJobCardNo.DataValueField = "JCHID";
            ddlJobCardNo.DataBind();
            ddlJobCardNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetDailyActivitiesProductionRFPDetailsByEmployeeID(int EmployeeID, DropDownList ddlRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDailyActivitiesProductionRFPDetailsByEmployeeID";
            c.Parameters.Add("@EmployeeID", EmployeeID);
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

    public DataSet getRFPCustomerNameByMaterialPlanning(int EmployeeID, DropDownList ddlCustomerName, string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPCustomerNameByMaterialplanning";
            c.Parameters.Add("@EmployeeID", EmployeeID);
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

    public DataSet GetRFPNoDetaisByMaterialPlanning(int EmployeeID, DropDownList ddlRFPNo, string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoDetailsByMaterialPlanning";
            c.Parameters.Add("@EmployeeID", EmployeeID);
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
	
	public DataSet GetJCHIDByPAPDID(string spname,string PAPDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@PAPDID", PAPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	public DataSet GetAssemplyPlanningJobCardDetailsForPartToPart()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAssemplyPlanningJobCardDetailsForPartToPart";
            // c.Parameters.Add("@PRIDID", PRIDID);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	
	
	    public DataSet GetPartDetailsByPRIDIDForContractor()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemPartSerielNoByPRIDIDForContractor";
            c.Parameters.Add("@PRIDID", PRIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	 public DataSet DeleteContractorJobOrderFormDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteContractorJobOrderFormDetails";
            c.Parameters.Add("@CTPDID", CTPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet GetContractorName(DropDownList ddContractorName, int RFPDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorNameForPayment";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);

            ddContractorName.DataSource = ds.Tables[0];
            ddContractorName.DataTextField = "ContractorName";
            ddContractorName.DataValueField = "CMID";
            ddContractorName.DataBind();
            ddContractorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
	
	    public DataSet SaveContractorPercentage()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveContractorPercentageForAllJob";
            c.Parameters.Add("@CTDID", CTDID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@AmountPercentage", AmountPercentage);
            c.Parameters.Add("@Amount", Amount);
            c.Parameters.Add("@PaymentDate", PaymentDate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@ContractorName", ContractorName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet BindContractorJobOrderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorJobOrderDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@CMID", CMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet BindContractorJobOrderFormDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure; 
            c.CommandText = "LS_GetContractorJobOrderFormDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@ContractorName", ContractorName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet GetBindContractorJobComplete()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorJobCompleteDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            //  c.Parameters.Add("@RFPDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBindContractorJobInComplete()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetContractorJobInCompleteDetails";
            c.Parameters.Add("@RFPDID", RFPDID);
            //  c.Parameters.Add("@RFPDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	
	// Contractor ListItem
	
	    public DataSet GetContractorJobListDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_ContractorJobListPercentage";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	    public DataSet GetMaterialPlanningDetailsByRFPHIDAndEDIDForContractor()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlanningDetailsByRFPHIDAndEDIDForContractor";
            c.Parameters.Add("@RFPDID", RFPDID);
            //  c.Parameters.Add("@RFPDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
		public DataSet GetMaterialPlanningDetailsByRFPHIDAndEDIDForIncomlete()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlanningDetailsByRFPHIDAndEDIDForIncomlete";
            c.Parameters.Add("@RFPDID", RFPDID);
            //  c.Parameters.Add("@RFPDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	
		public DataSet GetAWTValueForPartMasterByBOMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAWTValueForPartMasterByBOMID";
            c.Parameters.Add("@BOMID", BOMID);
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