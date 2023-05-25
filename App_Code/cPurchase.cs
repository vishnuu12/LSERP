using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using eplus.data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for cPurchase
/// </summary>
public class cPurchase
{
    #region "Declaration"

    DataSet ds = new DataSet();
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    cSession objSession = new cSession();
    public string VDCDID;
    public int txtRawMaterialQty;
    public int MIQCCID;

    #endregion

    #region"properties"

    public string QNSUPID { get; set; }
    public string QNPID { get; set; }
    public string QNUmber { get; set; }
    public int SPOID { get; set; }
    public int SUPID { get; set; }
    public string AttachmentID { get; set; }
    public Decimal Quotecost { get; set; }
    public DateTime IssueDate { get; set; }
    public string QuateReferenceNumber { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Note { get; set; }
    public string PaymentMode { get; set; }
    public string Enclosure { get; set; }
    public int LocationID { get; set; }
    public int CurrencyID { get; set; }
    public string CurrencyValueINR { get; set; }

    public int WPOID { get; set; }
    public Decimal handlingcharges { get; set; }
    public string woremarks { get; set; }

    public int TaxID { get; set; }
    public int Value { get; set; }

    public string type { get; set; }
    public int RFPHID { get; set; }

    public int PID { get; set; }
    public string Remarks { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }

    public int AttachementTypeName { get; set; }
    public string Description { get; set; }
    public int EnquiryID { get; set; }
    public string AttachementName { get; set; }

    public string PINumber { get; set; }
    public int PISUPID { get; set; }

    public string QNumber { get; set; }

    public int CID { get; set; }
    public int MGMID { get; set; }
    public int THKID { get; set; }

    public int MTID { get; set; }
    public string RequiredWeight { get; set; }


    public string MTFIDs { get; set; }
    public string MTFIDsValue { get; set; }

    public string QIDs { get; set; }

    public int CMID { get; set; }
    public int SPODID { get; set; }

    public decimal TotalCost { get; set; }

    public DataTable dt { get; set; }

    public int VDCID { get; set; }
    public DateTime DCDate { get; set; }
    public string FormJJNo { get; set; }
    public int TariffClassification { get; set; }
    public int Duration { get; set; }
    public DateTime DutyDetailsDate { get; set; }


    public Decimal CGST { get; set; }
    public Decimal SGST { get; set; }
    public Decimal IGST { get; set; }
    public Decimal FrightCharges { get; set; }
    public Decimal PackingAndForwardingCharges { get; set; }

    public int QHID { get; set; }
    public string PIDs { get; set; }
    public int ApprovalStatus { get; set; }
    public string CreatedBy { get; set; }

    public string PageNameFlag { get; set; }

    public decimal UnitQuoteCost { get; set; }
    public decimal TotalQuoteCost { get; set; }

    public int ChargesID { get; set; }
    public decimal ChargesValue { get; set; }
    public string ChargesType { get; set; }

    public int POApprovalStatus { get; set; }
    public string PoType { get; set; }
    public int SCVMID { get; set; }
    public int IssuedQty { get; set; }
    public string ItemDescription { get; set; }
    public int WOIHID { get; set; }
    public int WPODID { get; set; }
    public int UserID { get; set; }
    public string SupplierName { get; set; }

    public string Address { get; set; }
    public string ContactPerson { get; set; }
    public string ContactNumber { get; set; }
    public string FaxNo { get; set; }
    public string EmailID { get; set; }
    public string GSTNo { get; set; }
    public string SubVendorFlag { get; set; }
    public string PIDAndPOQuantity { get; set; }

    public int WPOApprovalStatus { get; set; }
    public string MIDID { get; set; }
    public string Flag { get; set; }
    public string ReviewName { get; set; }
    public int PENo { get; set; }
    public string AmendmentReason { get; set; }
    public string Modename { get; set; }
    public int CompanyID { get; set; }
    public int VendorType { get; set; }

    #endregion

    #region"Common Methods"

    public DataSet SaveSupplierPO()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSupplierPO";
            c.Parameters.AddWithValue("@SPOID", SPOID);
            c.Parameters.AddWithValue("@SUPID", SUPID);
            c.Parameters.AddWithValue("@IssueDate", IssueDate);
            c.Parameters.AddWithValue("@QuateReferenceNumber", QuateReferenceNumber);
            c.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
            c.Parameters.AddWithValue("@Note", Note);
            c.Parameters.AddWithValue("@PaymentMode", PaymentMode);
            c.Parameters.AddWithValue("@Enclosure", Enclosure);
            c.Parameters.AddWithValue("@LocationID", LocationID);
            c.Parameters.AddWithValue("@CurrencyID", CurrencyID);
            c.Parameters.AddWithValue("@CurrencyValueINR", DBNull.Value);
            c.Parameters.AddWithValue("@TaxID", DBNull.Value);
            c.Parameters.AddWithValue("@Value", DBNull.Value);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@AmendmentReason", AmendmentReason);
            c.Parameters.AddWithValue("@CompanyID", CompanyID);
            //if (CGST == 0)
            //    c.Parameters.AddWithValue("@TaxID", DBNull.Value);
            //else
            //    c.Parameters.AddWithValue("@CGST", CGST);
            //if (SGST == 0)
            //    c.Parameters.AddWithValue("@Value", DBNull.Value);
            //else
            //    c.Parameters.AddWithValue("@SGST", SGST);

            //if (IGST == 0)
            //    c.Parameters.AddWithValue("@IGST", DBNull.Value);
            //else
            //    c.Parameters.AddWithValue("@IGST", IGST);

            //c.Parameters.AddWithValue("@FrightCharges", FrightCharges);
            //c.Parameters.AddWithValue("@PackingAndForwardingCharges", PackingAndForwardingCharges);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkorderPO()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkorderPO";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            c.Parameters.AddWithValue("@IssueDate", IssueDate);
            c.Parameters.AddWithValue("@QuateReferenceNumber", QuateReferenceNumber);
            c.Parameters.AddWithValue("@DeliveryDate", DeliveryDate);
            c.Parameters.AddWithValue("@Note", Note);
            c.Parameters.AddWithValue("@PaymentMode", PaymentMode);
            c.Parameters.AddWithValue("@Enclosure", Enclosure);
            c.Parameters.AddWithValue("@LocationID", LocationID);
            //c.Parameters.AddWithValue("@handlingcharges", handlingcharges);
            c.Parameters.AddWithValue("@woremarks", woremarks);
            c.Parameters.AddWithValue("@AmendmentReason", AmendmentReason);
            c.Parameters.AddWithValue("@UserID", UserID);
            c.Parameters.AddWithValue("@CompanyID", CompanyID); 
            if (AttachementName != "")
                c.Parameters.AddWithValue("@AttachementName", AttachementName);
            else
                c.Parameters.AddWithValue("@AttachementName", DBNull.Value);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveQnApprovalStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQnApprovalStatus";
            c.Parameters.AddWithValue("@QNPID", QNPID);
            c.Parameters.AddWithValue("@QNSUPID", QNSUPID);
            c.Parameters.AddWithValue("@QHID", QHID);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@type", VendorType);
            //c.Parameters.AddWithValue("@PENo", PENo);
            //c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getSupplierPODetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPODetails";
            c.Parameters.AddWithValue("@SUPID", SUPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet getPurchaseIndentDetailsBySPOIDAndType()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsBySPOIDAndType";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@SPOID", SPOID);
            c.Parameters.Add("@Type", type);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSupplierPODetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSupplierPODetails";
            c.Parameters.Add("@PID", PID);
            if (SPOID == 0)
                c.Parameters.Add("@SPOID", DBNull.Value);
            else
                c.Parameters.Add("@SPOID", SPOID);
            c.Parameters.Add("@Quantity", Quantity);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@UnitCost", UnitCost);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public string GetRFPNoByPINumber()
    {
        string str = "";
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoByPINumber";
            c.Parameters.Add("@QHID", QHID);
            str = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return str;
    }

    public DataSet GetPurchaseIndentDetailsByPINumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByPINumber";
            c.Parameters.Add("@QHID", QHID);
            c.Parameters.Add("@PISUPID", PISUPID);
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet SaveAttachment(string Filename, string Description)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveAttachment";
            c.Parameters.AddWithValue("@Filename", Filename);
            c.Parameters.AddWithValue("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet SaveQuote()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQuote";
            c.Parameters.AddWithValue("@PID", PID);
            c.Parameters.AddWithValue("@SUPID", SUPID);
            c.Parameters.AddWithValue("@AttachmentID", AttachmentID);
            c.Parameters.AddWithValue("@Quotecost", Quotecost);
            c.Parameters.AddWithValue("@RequiredWeight", DBNull.Value);
            c.Parameters.AddWithValue("@TotalCost", DBNull.Value);
            c.Parameters.AddWithValue("@QHID", QHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet updateQuateStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateQuoteStatusByPINumber";
            c.Parameters.Add("@PIDs", PIDs);
            c.Parameters.Add("@QHID", QHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }


    public DataSet GetPurchaseIndentDetailsByQNNumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByQNumber";
            c.Parameters.Add("@QHID", QHID);
            c.Parameters.Add("@type", VendorType);
            // c.Parameters.Add("@PENo", PENo);  TC
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaxSPONoInSupplierPO(string Spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = Spname;
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet getSupplierPOItemDetailsBySPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPOItemDetailsBySPOID";
            c.Parameters.Add("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQuotationNumberByCIDAndSUPIDAndMGMIDAndTHKID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQuotationNumberByCIDAndSUPIDAndMGMIDAndTHKID";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@MGMID", MGMID);
            c.Parameters.Add("@THKID", THKID);
            c.Parameters.Add("@PID", PID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSupplierPOItemDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSupplierPOItemDetails";

            c.Parameters.Add("@SPOID", SPOID);
            c.Parameters.Add("@SPODID", SPODID);

            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@MGMID", MGMID);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@MTID", MTID);

            c.Parameters.Add("@DeliveryDate", DeliveryDate);
            c.Parameters.Add("@Remarks", Remarks);

            //c.Parameters.Add("@Quantity", Quantity);
            c.Parameters.Add("@THKID", THKID);

            c.Parameters.Add("@MTFIDs", MTFIDs);
            c.Parameters.Add("@MTFIDsValue", MTFIDsValue);
            c.Parameters.Add("@PIDAndPOQuantity", PIDAndPOQuantity);

            c.Parameters.Add("@UnitCost", UnitCost);

            c.Parameters.Add("@CreatedBy", CreatedBy);
            //c.Parameters.Add("@TotalQuoteCost", TotalQuoteCost);

            //c.Parameters.Add("@RW", RequiredWeight);
            // c.Parameters.Add("@PID", PID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteSupplierPOItemDetailsBySPODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSupplierPOItemDetailsBySPODID";
            c.Parameters.Add("@SPODID", SPODID);
            c.Parameters.Add("@PageNameFlag", PageNameFlag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseOrderDetaisBySPODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseOrderPDFDetailsBySPOID";
            c.Parameters.Add("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderPODetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderPOBySUPID";
            //c.Parameters.AddWithValue("@SUPID", SUPID);
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
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
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderIndentDetailsByWPOID";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveWorkOrderPODetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderPODetails";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            c.Parameters.AddWithValue("@type", type);
            c.Parameters.AddWithValue("@tblWorkOrderPODetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveVendorDCDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveVendorDCDetails";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            c.Parameters.AddWithValue("@WPOID", WPOID);
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            c.Parameters.AddWithValue("@DCDate", DCDate);
            c.Parameters.AddWithValue("@FormJJNo", FormJJNo);
            c.Parameters.AddWithValue("@TariffClassification", TariffClassification);
            c.Parameters.AddWithValue("@Duration ", Duration);
            c.Parameters.AddWithValue("@DutyDetailsDate ", DutyDetailsDate);
            c.Parameters.AddWithValue("@LocationID ", LocationID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetVendorDCDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCDetails";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateSupplierPOSharedStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSharedPOStatusBySPOID";
            c.Parameters.AddWithValue("@SPOID ", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderPOByWPOIDAndPOSharedStatusCompleted()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderPOByWPOIDAndPOSharedStatusCompleted";
            c.Parameters.AddWithValue("@WPOID ", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorDCDetailsByVDCIDForPDF()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCDetailsByVDCIDForPDF";
            c.Parameters.AddWithValue("@VDCID ", VDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseIndentDetailsByIndentNumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByQHID";
            c.Parameters.AddWithValue("@QHID", QHID);
            c.Parameters.AddWithValue("@Flag", PageNameFlag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdatePurchaseIndentApprovalStatus()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdatePurchaseIndentApprovalStatus";
            c.Parameters.AddWithValue("@PIDs", PIDs);
            c.Parameters.AddWithValue("@Status", ApprovalStatus);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@QHID", QHID);
            c.Parameters.AddWithValue("@IndentRemarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSupplierPOChargesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSupplierPOChargesdetails";
            c.Parameters.AddWithValue("@SPOID", SPOID);
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

    public DataSet UpdatePoApprovalStatusBySPOID(string spname)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@SPOID", SPOID);
            c.Parameters.AddWithValue("@POStatus", POApprovalStatus);
            if (Remarks == "")
                c.Parameters.AddWithValue("@Remarks", DBNull.Value);
            else
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
    public DataSet getApprovalPendingSupplierPODetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetApprovalPendingSupplierPODetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPOItemDetailsBySPODID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPOItemDetailsBySPODID";
            c.Parameters.AddWithValue("@SPODID", SPODID);
            c.Parameters.AddWithValue("@SUPID", SUPID);
            c.Parameters.AddWithValue("@CID", CID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierPOMaterialTypeFieldValuesBySPODID(string MaterialTypeId, string Mode, int SPODID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETSupplierPOMaterialTypeFieldsValues";
            c.Parameters.AddWithValue("@SPODID", SPODID);
            c.Parameters.AddWithValue("@Mode", Mode);
            c.Parameters.AddWithValue("@MaterialTypeId", MaterialTypeId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteSupplierPOChargesDetailsByChargesID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSupplierPOChargesDetailsByChargesID";
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

    public DataSet GetSupplierPOChargesDetails(string spname)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdatePOApprovalStatusByWPOID(string spname, string WPOIDs)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@WPOIDs", WPOIDs);
            //c.Parameters.AddWithValue("@Status", Status);
            //WPOApprovalStatus
            c.Parameters.AddWithValue("@WPOApprovalStatus", WPOApprovalStatus);
            c.Parameters.AddWithValue("@UserID", UserID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderPOApprovalDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderPOApprovalDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderApprovedPODetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkorderApprovedPODetails";
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorDCItemDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCItemDetailsByVDCID";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveVendorDCItemDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveVendorDCItemDetails";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            c.Parameters.AddWithValue("@WPOID", WPOID);
            c.Parameters.AddWithValue("@WOIHID", WOIHID);
            c.Parameters.AddWithValue("@IssuedQty", IssuedQty);
            c.Parameters.AddWithValue("@ItemDescription", ItemDescription);
            c.Parameters.AddWithValue("@Value", Value);
            c.Parameters.AddWithValue("@txtRawMaterialQty", txtRawMaterialQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteVendorDCItemDetailsByVDCDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteVendorDCitemDetailsByVDCDID";
            c.Parameters.AddWithValue("@VDCDID", VDCDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorNameByVDCID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorNameByVDCID";
            c.Parameters.AddWithValue("@VDCID", VDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetVendorNameByWPOID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorNameByWPOID";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkOrderPODetailsByWPOID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWorkOrderPODetailsByWPOID";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteWorkOrderPOItemDetailsByWPODID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderPOItemDetailsByWPODID";
            c.Parameters.AddWithValue("@WPODID", WPODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveWorkOrderPOTaxAndOtherChargesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveWorkOrderPOTaxAndOtherChargesDetails";
            c.Parameters.AddWithValue("@WPOID", WPOID);
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

    public DataSet GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber(string spname)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteWorkOrderPOtaxAndOtherChargesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderPOtaxAndOtherChargesDetailsByPrimaryID";
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

    public DataSet CancelWorkOrderPOByWPOID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteWorkOrderPOByWPOID";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            c.Parameters.AddWithValue("@PageNameFlag", PageNameFlag);
            c.Parameters.AddWithValue("@UserID", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateQuotationReviewStatus()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateQuotationReviewStatusByPID";
            c.Parameters.AddWithValue("@PID", PID);
            c.Parameters.AddWithValue("@QHID", QHID);
            c.Parameters.AddWithValue("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSuppliereDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSupplierDetails";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@SupplierName", SupplierName);
            c.Parameters.Add("@Description", Description);

            c.Parameters.Add("@Address ", Address);
            c.Parameters.Add("@ContactPerson", ContactPerson);
            c.Parameters.Add("@ContactNumber", ContactNumber);
            c.Parameters.Add("@FaxNo ", FaxNo);
            c.Parameters.Add("@EmailID ", EmailID);
            c.Parameters.Add("@GSTNo ", GSTNo);
            c.Parameters.Add("@SubVendorFlag", SubVendorFlag);
            c.Parameters.Add("@AttachementName", AttachementName);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetSupllierDetailsBySUPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierDetailsBySUPID";
            c.Parameters.Add("@SUPID", SUPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteSupplierDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteSupplier";
            c.Parameters.AddWithValue("@SUPID", SUPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetIndentDetailsByCIDAndSUPID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetIndentDetailsByCIDAndSUPID";
            c.Parameters.AddWithValue("@SUPID", SUPID);
            c.Parameters.AddWithValue("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseindentMaterialTypeFiledsByPID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseindentMaterialtypeFieldsByPID";
            c.Parameters.AddWithValue("@PID", PID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoAndIndentRemarksBySPODID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoAndIndentRemarksBySPODID";
            c.Parameters.AddWithValue("@SPODID", SPODID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseOrderAMDPDFDetaisBySPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSPOAMDpdfDetailsBySPOID";
            c.Parameters.Add("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPOApprovedValues()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPOApprovedValues";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPoApprovedDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPoApprovedDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierPOTermsAndConditionsDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPOTermsAndConditionDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveSupplierPOtermsAndConditions(string SPOTCMID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveSupplierPOTermsAndConditionDetails";
            c.Parameters.Add("@SPOID", SPOID);
            c.Parameters.Add("@SPOTCMID", SPOTCMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierPOTermsAndConditionsDetailsBySPOID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierPOTermAndConditionDetailsBySPOID";
            c.Parameters.Add("@SPOID", SPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdatePOAmendMentsBySPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_POAmendments";
            c.Parameters.Add("@SPOID", SPOID);
            c.Parameters.Add("@UserID", CreatedBy);
            c.Parameters.Add("@AmendmentReason", AmendmentReason);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteMaterialInwardStockCertificateDetailsByMIQCCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialinwardStockCertificateDetailsByMIQCCID";
            c.Parameters.Add("@MIQCCID", MIQCCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateStockCertificateSharedStatusByMIDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateStockCertificatesShredStatusByMIDID";
            c.Parameters.Add("@MIDID", MIDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAllPoNoDetails(string Mode)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAllPONoDetails";
            c.Parameters.Add("@Mode", Mode);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateAllowPermissionToPOQtyByPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAllowPermissionToPOQtyByPID";
            c.Parameters.Add("@PID", PID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateWorkOrderPOAmendmentDetailsByWPOID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateWorkOrderPOAmendmentDetails";
            c.Parameters.Add("@WPOID", WPOID);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@AmendmentReason", AmendmentReason);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetTechnicalQuotationReviewDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetTechnicalQuotationReviewDetailsByQHID";
            c.Parameters.Add("@QHID", QHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveQuotationReviewDetailsByIndentNoAndReviewname()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQuotationReviewDetailsByIndentNoAndReviewname";
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@tblQuotationReview", dt);
            c.Parameters.Add("@ReviewName", ReviewName);
            c.Parameters.Add("@QHID", QHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GeneratePurchaseEnquiryNoByQHID(string QHID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GeneratePurchaseEnquiryNoByQHID";
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@QHIDs", QHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateWorkOrderPOAmendmentDetailsByWPODID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateWorkOrderPOAmendmentDetailsByWPODID";
            c.Parameters.Add("@UnitCost", UnitCost);
            c.Parameters.Add("@WPODID", WPODID);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getQuotationEnquiryNoDetails(DropDownList ddlQNNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetQuotationEnquiryNoDetails", ref ds);
            ddlQNNumber.DataSource = ds.Tables[0];

            ddlQNNumber.DataTextField = "PENo";
            ddlQNNumber.DataValueField = "PNo";
            ddlQNNumber.DataBind();
            ddlQNNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

    #region"DropDown methods"

    public DataSet GetSupplierDetails(DropDownList ddlSupplier)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetSupplierDetails", ref ds);
            ddlSupplier.DataSource = ds.Tables[0];

            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataValueField = "SUPID";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

/*     public DataSet GetQNNumber(DropDownList ddlQNNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetQNNumber", ref ds);
            ddlQNNumber.DataSource = ds.Tables[0];

            ddlQNNumber.DataTextField = "PINumber";
            ddlQNNumber.DataValueField = "QHID";
            ddlQNNumber.DataBind();
            ddlQNNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    } */
	
	 public DataSet GetQNNumber(DropDownList ddlQNNumber, int Userid)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQNNumber";
            c.Parameters.Add("@Userid", Userid);
            DAL.GetDataset(c, ref ds);
            ddlQNNumber.DataSource = ds.Tables[0];

            ddlQNNumber.DataTextField = "PINumber";
            ddlQNNumber.DataValueField = "QHID";
            ddlQNNumber.DataBind();
            ddlQNNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCategoryNameByListBySUPID(DropDownList ddlCategoryName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialCategoryNameListBySUPID";
            c.Parameters.Add("@SUPID", SUPID);
            DAL.GetDataset(c, ref ds);
            ddlCategoryName.DataSource = ds.Tables[0];

            ddlCategoryName.DataTextField = "CategoryName";
            ddlCategoryName.DataValueField = "CID";
            ddlCategoryName.DataBind();
            ddlCategoryName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialGradeNameByCIDAndSUPID(DropDownList ddlGradeName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialGradeNameByCIDAndSUPID";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);

            ddlGradeName.DataSource = ds.Tables[0];

            ddlGradeName.DataTextField = "GradeName";
            ddlGradeName.DataValueField = "MGMID";
            ddlGradeName.DataBind();
            ddlGradeName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialThicknessByCIDAndSUPIDAndMGMID(DropDownList ddlThickness)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialThicknessByCIDAndSUPIDAndMGMID";
            c.Parameters.Add("@SUPID", SUPID);
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@MGMID", MGMID);
            // c.Parameters.Add("@PID", PID);

            DAL.GetDataset(c, ref ds);
            ddlThickness.DataSource = ds.Tables[0];

            ddlThickness.DataTextField = "THKValue";
            ddlThickness.DataValueField = "THKID";
            ddlThickness.DataBind();
            ddlThickness.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GETMaterialFieldsForSupplierPOItemDetails(string MaterialTypeId, string Mode, int PID)
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETMaterialFieldsForSupplierPOItemDetails";
            c.Parameters.AddWithValue("@MaterialTypeId", MaterialTypeId);
            c.Parameters.AddWithValue("@Mode", Mode);

            if (Mode == "Add")
                c.Parameters.AddWithValue("@PID", DBNull.Value);
            if (Mode == "Edit")
                c.Parameters.AddWithValue("@PID", PID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWPONumber(DropDownList ddlWPONo, DropDownList ddlSuppplierName)
    {
        try
        {
            DAL = new cDataAccess();
            DAL.GetDataset("LS_GetWPONumberByVendorDCStatus", ref ds);
            ddlWPONo.DataSource = ds.Tables[0];

            ddlWPONo.DataTextField = "Wonumber";
            ddlWPONo.DataValueField = "WPOID";
            ddlWPONo.DataBind();
            ddlWPONo.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlSuppplierName.DataSource = ds.Tables[1];
            ddlSuppplierName.DataTextField = "VendorName";
            ddlSuppplierName.DataValueField = "SCVMID";
            ddlSuppplierName.DataBind();
            ddlSuppplierName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorDCNoDetails(DropDownList ddlVendorDCNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCNoDetails";

            DAL.GetDataset(c, ref ds);
            ddlVendorDCNo.DataSource = ds.Tables[0];
            ddlVendorDCNo.DataTextField = "DCNo";
            ddlVendorDCNo.DataValueField = "VDCID";
            ddlVendorDCNo.DataBind();
            ddlVendorDCNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseIndentNumber(DropDownList ddlPINumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentNumber";

            DAL.GetDataset(c, ref ds);
            ddlPINumber.DataSource = ds.Tables[0];
            ddlPINumber.DataTextField = "PINumber";
            ddlPINumber.DataValueField = "QHID";
            ddlPINumber.DataBind();
            ddlPINumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCategoryNameByListByIndentNumber(DropDownList ddlCategoryname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCategoryNameListByIndentNumber";
            c.Parameters.AddWithValue("@QHID", QHID);
            DAL.GetDataset(c, ref ds);
            ddlCategoryname.DataSource = ds.Tables[0];
            ddlCategoryname.DataTextField = "CategoryName";
            ddlCategoryname.DataValueField = "CID";
            ddlCategoryname.DataBind();
            ddlCategoryname.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierDetailsByCategoryName(DropDownList ddlSuppliername, string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@CID", CID);
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

    public DataSet GetCategoryNameByListByApprovedIndentNumber(DropDownList ddlCategoryname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCategoryNameListByApprovedIndentNumber";
            DAL.GetDataset(c, ref ds);
            ddlCategoryname.DataSource = ds.Tables[0];
            ddlCategoryname.DataTextField = "CategoryName";
            ddlCategoryname.DataValueField = "CID";
            ddlCategoryname.DataBind();
            ddlCategoryname.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindChargesDetails(DropDownList ddlOtherCharges, DropDownList ddlTax)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();

            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetChargesDetails";
            DAL.GetDataset(c, ref ds);
            ddlOtherCharges.DataSource = ds.Tables[0];
            ddlOtherCharges.DataTextField = "ChargesName";
            ddlOtherCharges.DataValueField = "OCDID";
            ddlOtherCharges.DataBind();
            ddlOtherCharges.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlTax.DataSource = ds.Tables[1];
            ddlTax.DataTextField = "TaxName";
            ddlTax.DataValueField = "TaxId";
            ddlTax.DataBind();
            ddlTax.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierChainVendorNameDetails(DropDownList ddlSupplierChainVendor)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierChainVendorNameDetails";
            c.Parameters.AddWithValue("@POType", PoType);
            DAL.GetDataset(c, ref ds);

            ddlSupplierChainVendor.DataSource = ds.Tables[0];
            ddlSupplierChainVendor.DataTextField = "VendorName";
            ddlSupplierChainVendor.DataValueField = "SCVMID";
            ddlSupplierChainVendor.DataBind();
            ddlSupplierChainVendor.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDCNumberByVendorName(DropDownList ddlDCNo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDCNumberByVendorName";
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            DAL.GetDataset(c, ref ds);

            ddlDCNo.DataSource = ds.Tables[0];
            ddlDCNo.DataTextField = "DCNo";
            ddlDCNo.DataValueField = "VDCID";
            ddlDCNo.DataBind();
            ddlDCNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWPONumberDetails(DropDownList ddlWPONo)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetWPONumberDetails";
            DAL.GetDataset(c, ref ds);

            ddlWPONo.DataSource = ds.Tables[0];
            ddlWPONo.DataTextField = "WoNumber";
            ddlWPONo.DataValueField = "WPOID";
            ddlWPONo.DataBind();
            ddlWPONo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorNameByWPONumber(DropDownList ddlWPONumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorNameByWPONumber";
            c.Parameters.AddWithValue("@SCVMID", SCVMID);
            DAL.GetDataset(c, ref ds);

            ddlWPONumber.DataSource = ds.Tables[0];
            ddlWPONumber.DataTextField = "WoNumber";
            ddlWPONumber.DataValueField = "WPOID";
            ddlWPONumber.DataBind();
            ddlWPONumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPOPaymentDays(DropDownList ddlPOPaymentDays)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPOPaymentDays";
            DAL.GetDataset(c, ref ds);

            ddlPOPaymentDays.DataSource = ds.Tables[0];
            ddlPOPaymentDays.DataTextField = "Days";
            ddlPOPaymentDays.DataValueField = "POPDID";
            ddlPOPaymentDays.DataBind();
            ddlPOPaymentDays.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWPONumberDetailsByVendorDC(DropDownList ddlWPONo, string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            //c.CommandText = "LS_GetWPONumberDetails";
            c.CommandText = "LS_GetWPONumberDetailsByVendorDC";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);

            ddlWPONo.DataSource = ds.Tables[0];
            ddlWPONo.DataTextField = "WoNumber";
            ddlWPONo.DataValueField = "WPOID";
            ddlWPONo.DataBind();
            ddlWPONo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSupplierChainVendorNameDetailsByVendorDC(DropDownList ddlSupplierChainVendor, string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            ///c.CommandText = "LS_GetSupplierChainVendorNameDetails";
            c.CommandText = "LS_GetSupplierChainVendorNameDetailsByVendorDC";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);

            ddlSupplierChainVendor.DataSource = ds.Tables[0];
            ddlSupplierChainVendor.DataTextField = "VendorName";
            ddlSupplierChainVendor.DataValueField = "SCVMID";
            ddlSupplierChainVendor.DataBind();
            ddlSupplierChainVendor.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorDCNoDetailsByWorkorderinward(DropDownList ddlVendorDCNo, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCNoDetailsByWorkOrderInward";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);
            ddlVendorDCNo.DataSource = ds.Tables[0];
            ddlVendorDCNo.DataTextField = "DCNo";
            ddlVendorDCNo.DataValueField = "VDCID";
            ddlVendorDCNo.DataBind();
            ddlVendorDCNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetVendorNameDetailsByWorkOrderInward(DropDownList ddlvendorName, string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorNameDetailsByWorkOrderInward";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);
            ddlvendorName.DataSource = ds.Tables[0];
            ddlvendorName.DataTextField = "VendorName";
            ddlvendorName.DataValueField = "SCVMID";
            ddlvendorName.DataBind();
            ddlvendorName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdatePOQtyAllowPermissionByWOIHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdatePOQtyAllowPermissionByWOIHID";
            c.Parameters.AddWithValue("@WOIHID", WOIHID);
            c.Parameters.AddWithValue("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetWorkorderpoAmenmendPrintDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_WorkOrderAmenmendPrintDetails";
            c.Parameters.AddWithValue("@WPOID", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	public DataSet GetDetailsByVendorDCReports(string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetVendorDCReports";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);

            
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }   public DataSet GetQNNumberReports(DropDownList ddlQNNumber, int Userid)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQNNumber_Practice";
            //c.CommandText = "LS_GetQNNumber";
            c.Parameters.Add("@Userid", Userid);
            DAL.GetDataset(c, ref ds);
            ddlQNNumber.DataSource = ds.Tables[0];

            ddlQNNumber.DataTextField = "PINumber";
            ddlQNNumber.DataValueField = "QHID";
            ddlQNNumber.DataBind();
            ddlQNNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	
    public DataSet GetPurchaseIndentDetailsByQNNumberReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByQNumber_Practice";
            //c.CommandText = "LS_GetPurchaseIndentDetailsByQNumber";
            c.Parameters.Add("@QHID", QHID);
            c.Parameters.Add("@type", VendorType);
            // c.Parameters.Add("@PENo", PENo);  TC
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