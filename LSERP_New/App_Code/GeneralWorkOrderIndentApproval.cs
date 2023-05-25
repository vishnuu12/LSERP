using eplus.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GeneralWorkOrderIndentApproval
/// </summary>
public class GeneralWorkOrderIndentApproval
{
    #region "Declaration"

    DataSet ds = new DataSet();
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    cSession objSession = new cSession();

    #endregion

    #region"properties"

    public int GWOApprovalStatus { get; set; }
    public int ChargesID { get; set; }
    public decimal ChargesValue { get; set; }
    public string ChargesType { get; set; }
    public int UserID { get; set; }
    public int SGWOID { get; set; }
    public int GWOPOID { get; set; }
    public int GWPODC { get; set; }
    public int GWOIDAID { get; set; }
    public int ddlSupplier { get; set; }
    public int ddlLocation { get; set; }
    public int EmpID { get; set; }
    public string txtQuote { get; set; }
    public string PageNameFlag { get; set; }
    public string txtDelivery { get; set; }
    public string txtTotalAmount { get; set; }
    public string txtNote { get; set; }
    public string txtPayment { get; set; }
    public string txtRemark { get; set; }
    public string txtRemarks { get; set; }
    public string GWOIDA { get; set; }
    public int GWOID { get; set; }
    public string GWOPOIDA { get; set; }
    public int GWPOID { get; set; }
    public string DCNo { get; set; }
    public string lblDCNo { get; set; }
    public string txtQuantity { get; set; }
    public string txtQuantitya { get; set; }
    public string txtUnitAmount { get; set; }
    public string txtUnitAmounta { get; set; }
    public string lblGWI { get; set; }
    public string lblGWOID { get; set; }
    public DataTable dt { get; set; }
    public string DCDate { get; set; }
    public string FormJJNo { get; set; }
    public string ItemDescription { get; set; }
    public string IndentNo { get; set; }
    public string lblDCQty { get; set; }
    public int DCQty { get; set; }
    public int InwardQty { get; set; }
    public int Location { get; set; }
    public int TariffClassification { get; set; }
    public int Duration { get; set; }
    public string DutyDetailsDate { get; set; }
    public string EwayBillNo { get; set; }
    public int SCVMID { get; set; }
    public int GDCID { get; set; }
    public int GIHD { get; set; }
    public int WPOID { get; set; }
    public int LocationID { get; set; }
    public string fpDCAttach { get; set; }
    public string Foldername { get; set; }
    public string FileName { get; set; }
    public string PID { get; set; }
    public FileUpload AttachementControl { get; set; }
    public string DCCopy { get; set; }
    #endregion

    #region"Common Methods"

    public void GetLocationDetails(DropDownList ddlLocation)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetAllLocationDetails();

            ddlLocation.DataSource = ds.Tables[0];
            ddlLocation.DataTextField = "Location";
            ddlLocation.DataValueField = "Lid";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetAllLocationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLocationDetailsForGWOPO";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public void GetDCLocationDetails(int Location, DropDownList ddlLocation)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetDCAllLocationDetails(Location);

            ddlLocation.DataSource = ds.Tables[0];
            ddlLocation.DataTextField = "Location";
            ddlLocation.DataValueField = "Lid";
            ddlLocation.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetDCAllLocationDetails(int Location)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLocationDetailsForGDC";
            c.Parameters.AddWithValue("@Location", Location);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public void GetDCLocation(string Location, DropDownList ddlLocation)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetDCAllLocation(Location);

            ddlLocation.DataSource = ds.Tables[0];
            ddlLocation.DataTextField = "Location";
            ddlLocation.DataValueField = "Lid";
            ddlLocation.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetDCAllLocation(string Location)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLocationForGDC";
            c.Parameters.AddWithValue("@Location", Location);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public void GetSupplierDetails(DropDownList ddlSupplier)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetAllSupplierDetails();

            ddlSupplier.DataSource = ds.Tables[0];
            ddlSupplier.DataTextField = "VendorName";
            ddlSupplier.DataValueField = "SCVMID";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetDCEditDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGWPODCDetailsForEdit";
            c.Parameters.AddWithValue("@GWPOID", GWPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetAllSupplierDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierDetailsForGWOPO";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
    public DataSet GetGeneralWorkOrderIndentApprovalDetails(int status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderIndentDetailsForApproval";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGeneralWorkOrderAllPODetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderIndentDetailsForPO";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetBindEditDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderIndentDetailsForEditUnit";
            c.Parameters.AddWithValue("@SGWOID", GWOPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGeneralWorkOrderIndentPODetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderIndentDetailsForPO";
            c.Parameters.AddWithValue("@status", ChargesID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGeneralWorkOrderAll()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderAllDetailsForPO";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateGWOApprovalStatusByID(string spname, string GWOIDA)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@GWOIDA", GWOIDA);
            c.Parameters.AddWithValue("@txtRemarks", txtRemarks);
            //WPOApprovalStatus
            c.Parameters.AddWithValue("@WPOApprovalStatus", GWOApprovalStatus);
            c.Parameters.AddWithValue("@UserID", UserID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateGWOPOApprovalByID(string spname, string GWOIDA)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@GWOIDA", GWOIDA);
            c.Parameters.AddWithValue("@txtRemarks", txtRemarks);
            //WPOApprovalStatus
            c.Parameters.AddWithValue("@WPOApprovalStatus", GWOApprovalStatus);
            c.Parameters.AddWithValue("@UserID", UserID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet InsertGWOPODetails(string GWOIDAID, string GWOIDID)
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGWOPO";
            c.Parameters.AddWithValue("@GWOPOID", GWOPOID);
            c.Parameters.AddWithValue("@GWOIDAID", GWOIDAID);
            c.Parameters.AddWithValue("@GWOIDID", GWOIDID);
            c.Parameters.AddWithValue("@txtQU", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveGeneralWorkOrderPOData()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateGeneralWOPOUnitCost";
            c.Parameters.AddWithValue("@GWOPOID", GWOPOID);
            c.Parameters.AddWithValue("@ddlSupplier", ddlSupplier);
            c.Parameters.AddWithValue("@ddlLocation", ddlLocation);
            c.Parameters.AddWithValue("@txtDelivery", txtDelivery);
            c.Parameters.AddWithValue("@txtQuantity", txtQuantity);
            c.Parameters.AddWithValue("@txtPayment", txtPayment);
            c.Parameters.AddWithValue("@txtUnitAmount", txtUnitAmount);
            c.Parameters.AddWithValue("@txtNote", txtNote);
            c.Parameters.AddWithValue("@txtRemark", txtRemark);
            c.Parameters.AddWithValue("@txtQuote", txtQuote);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGeneralWorkOrderPOApproval()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderPOApproval";
            c.Parameters.AddWithValue("@UserID", UserID);
            c.Parameters.AddWithValue("@status", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStatusBySGWOID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGWOStatusBySGWOID";
            c.Parameters.AddWithValue("@SGWOID", SGWOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetgeneralWorkOrderDcPendingReport(string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetgeneralWorkOrderDcPendingReport";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetgeneralWorkOrderDcReport()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetgeneralWorkOrderDcReport";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetgeneralWorkOrderDcJob(int GDCID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetgeneralWorkOrderDcJob";
            c.Parameters.AddWithValue("@GDCID", GDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveGeneralWorkOrderDC()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralWorkOrderDCDetails";
            c.Parameters.AddWithValue("@GDCID", GWPOID);
            c.Parameters.AddWithValue("@GWPODC", GWOPOID);
            c.Parameters.AddWithValue("@DCDate", DCDate);
            c.Parameters.AddWithValue("@FormJJNo", FormJJNo);
            c.Parameters.AddWithValue("@TariffClassification", TariffClassification);
            c.Parameters.AddWithValue("@Duration ", Duration);
            c.Parameters.AddWithValue("@DutyDetailsDate ", DutyDetailsDate);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveGeneralWorkOrderDCQty()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralWorkOrderDCDetailsForQty";
            c.Parameters.AddWithValue("@GDCID", GWPOID);
            c.Parameters.AddWithValue("@ItemDescription", ItemDescription);
            c.Parameters.AddWithValue("@DCQty", DCQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateAllowPermissionDcQtyByGDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateAllowPermissionDcQtyByGDCID";
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@GDCID", GDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateDCStatusByGDCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateDCStatusByGDCID";
            c.Parameters.AddWithValue("@GDCID", GDCID);
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
            c.Parameters.AddWithValue("@SGWOID", SGWOID);
            DAL.GetDataset(c, ref ds);
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

    public DataSet SaveGeneralWorkOrderPOTaxAndOtherChargesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralWorkOrderPOTaxAndOtherChargesDetails";
            c.Parameters.AddWithValue("@SGWOID", SGWOID);
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
    public DataSet ShareGeneralWorkOrderPoDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_ShareGeneralWorkOrderPoDetails";
            c.Parameters.AddWithValue("@SGWOID", SGWOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteGeneralWorkOrderPOtaxAndOtherChargesDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand(); 
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteGeneralWorkOrderPOtaxAndOtherChargesDetailsByPrimaryID";
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

    public DataSet SaveGeneralMaterialInward()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralMaterialInward";
            c.Parameters.Add("@GIHD", GIHD);
            c.Parameters.Add("@GDCID", GDCID);
            c.Parameters.Add("@GWPOID", GWPOID);
            c.Parameters.Add("@DCNo", DCNo);
            c.Parameters.Add("@DCDate", DCDate);
            c.Parameters.Add("@EwayBillNo", EwayBillNo);
            c.Parameters.Add("@DCCopy", DBNull.Value);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string SaveFiles()
    {
        var page = HttpContext.Current.CurrentHandler as Page;
        string reslt = "";
        try
        {
            string path = Foldername + PID + "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!string.IsNullOrEmpty(FileName))
                AttachementControl.SaveAs(path + FileName);

            reslt = "saved";
        }
        catch (Exception ex)
        {
            reslt = "failed";
            Log.Message(ex.ToString());
        }
        return reslt;
    }


    public DataSet generalDeleteGWOInward(int GWOIDA)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_generalDeleteGWOInward";
            c.Parameters.AddWithValue("@GHID", GWOIDA);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet generalShareGWOInwardDC()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_generalShareGWOInwardDC";
            c.Parameters.AddWithValue("@GHID", GDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGWInwardDetailsForModal(int GHID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_generalGWInwardDetailsForModal";
            c.Parameters.AddWithValue("@GHID", GHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveGeneralWorkOrderInwardQty()
    {
        DataSet ds = new DataSet();
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveGeneralWorkOrderInwardDetailsForQty";
            c.Parameters.AddWithValue("@GIHD", GWOIDAID);
            c.Parameters.AddWithValue("@lblDCQty", lblDCQty);
            c.Parameters.AddWithValue("@InwardQty", InwardQty);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }


    public DataSet GetGeneralWorkOrderInward(string status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDCNoDetailsByGeneralWorkOrderInward";
            c.Parameters.AddWithValue("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompleted()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompleted";
            c.Parameters.AddWithValue("@GWPOID ", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompletedReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompletedReports";
            c.Parameters.AddWithValue("@GDCID ", WPOID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetGeneralDCDetailsByGDCIDForPDF()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralDCDetailsByGDCIDForPDF";
            c.Parameters.AddWithValue("@GDCID ", GDCID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetGeneralWorkOrderIndentApprovalDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralWorkOrderIndentDetailsForApproval";
            c.Parameters.AddWithValue("@status ", ChargesID);
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