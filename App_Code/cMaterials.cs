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


public class cMaterials
{

    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    public int SIEHID;
    public string status;

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
    public int EnquiryNumber { get; set; }
    public int Quantity { get; set; }

    public int versionNumber { get; set; }
    public int BOMID { get; set; }
    public int DrawingSequenceNumber { get; set; }

    public string MaterialType { get; set; }

    //Material Specs Master Properties

    public string SpecsName { get; set; }
    public string SpecsShortCode { get; set; }
    public int MSMID { get; set; }
    public string MSMIds { get; set; }
    public string FieldType { get; set; }

    public string FormulaName { get; set; }
    public int MFID { get; set; }

    public string CategoryName { get; set; }
    public int CID { get; set; }

    public int VersionNumber { get; set; }
    public int ETCID { get; set; }

    public string Flag { get; set; }

    public string HSNCode { get; set; }
    public string ItemName { get; set; }
    public string StandardItemFlag { get; set; }
    public int ItemID { get; set; }

    public string ItemSize { get; set; }
    public int SID { get; set; }
    public int EDID { get; set; }

    public int SUPID { get; set; }
    public string SupplierName { get; set; }

    public int DuplicateEDID { get; set; }
    public int DuplicateVersionNumber { get; set; }
    public int DDID { get; set; }

    public string Addtionalpart { get; set; }

    public string GradeName { get; set; }
    public Decimal SpecificWeight { get; set; }
    public Decimal AWTValue { get; set; }

    public Decimal Cost { get; set; }
    public string UOM { get; set; }
    public int MGMID { get; set; }

    public int RFPHID { get; set; }
    public int MGMIDinDesign { get; set; }
    public int MGMIDinProduction { get; set; }
    public Decimal RequiredWeight { get; set; }
    public int MPID { get; set; }

    public int PartQuantityProduction { get; set; }

    public int RFPDID { get; set; }

    public DateTime Date { get; set; }
    public int IndentBy { get; set; }
    public int IndentTo { get; set; }
    public int MTID { get; set; }

    public DateTime DeliveryDate { get; set; }
    public string Remarks { get; set; }

    public string certficates { get; set; }

    public string PIndentMRNnumber { get; set; }
    public string Jobdescription { get; set; }


    public string PurchaseCopy { get; set; }

    public int PID { get; set; }

    public int MRNNumber { get; set; }

    public int QualityStatus { get; set; }

    public int MaterialTypeId { get; set; }
    public string MaterialtypeName { get; set; }
    public int MTFID { get; set; }

    public string MTFields { get; set; }

    public string MPIDS { get; set; }
    public string MTFIDs { get; set; }
    public string MTFIDsValue { get; set; }

    public int MPMD { get; set; }
    public string LayoutAttachement { get; set; }

    public string Mode { get; set; }

    public int DeadinventoryAmount { get; set; }
    public string DeadinventoryRemarks { get; set; }

    public string jobtype { get; set; }
    public decimal joborderweight { get; set; }

    public DataTable dt { get; set; }

    public int PRIDID { get; set; }

    public string DrawingName { get; set; }
    public int SITCID { get; set; }
    public int UserID { get; set; }

    #endregion

    #region "Common Methods"


    #endregion

    #region "Masters"


    public DataSet saveMaterialGroup()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialGroupDetails";
            c.Parameters.AddWithValue("@MGID", MGID);
            c.Parameters.AddWithValue("@Name", MateriyalGroupName.ToUpper());
            c.Parameters.AddWithValue("@Prefix", Prefix);
            c.Parameters.AddWithValue("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialGroup()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetMaterialGroupDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deleteMaterialGroup(int MGID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialGroup";
            c.Parameters.AddWithValue("@MGID", MGID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet saveMaterialThicknessValue()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialThicknessDetails";
            c.Parameters.AddWithValue("@THKId", THKId);
            c.Parameters.AddWithValue("@THKValue", THKValue);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialThicknessDetails()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetMaterialThicknessDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet DeleteMaterialThicknessValue(int THKId)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialThicknessValue";
            c.Parameters.AddWithValue("@THKId", THKId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet savePartMaster()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartNameDetails";
            c.Parameters.AddWithValue("@PartId", PartId);
            c.Parameters.AddWithValue("@PartName", PartName.ToUpper());
            c.Parameters.AddWithValue("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartMasterDetails()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetPartNameDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deletePartMaster(int PartId)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeletePartNameDetails";
            c.Parameters.AddWithValue("@PartId", PartId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMaterialClassificationMaster()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialCLassificaionNomDetails";
            c.Parameters.AddWithValue("@CMID", 0);
            c.Parameters.AddWithValue("@Name", ClassificationNomName.ToUpper());
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    //public DataSet GetMaterialClassificationMaster()
    //{
    //    DAL = new cDataAccess();
    //    ds = new DataSet();
    //    try
    //    {
    //        DAL.GetDataset("LS_GetMaterialClassificationNomDetails", ref ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex);
    //    }
    //    return ds;
    //}

    //public DataSet deleteMaterialClassificationMaster(int CMID)
    //{
    //    try
    //    {
    //        DAL = new cDataAccess();
    //        c = new SqlCommand();
    //        c.CommandType = CommandType.StoredProcedure;
    //        c.CommandText = "LS_DeleteMaterialClassificationNom";
    //        c.Parameters.AddWithValue("@CMID", CMID);
    //        DAL.GetDataset(c, ref ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //    return ds;
    //}

    public DataSet SaveMaterialMaster()
    {
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialMaterDetails";
            c.Parameters.AddWithValue("@MID", MID);
            c.Parameters.AddWithValue("@MaterialName", MaterialName.ToUpper());
            c.Parameters.AddWithValue("@MaterialType", MaterialType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialMaster()
    {
        DAL = new cDataAccess();
        ds = new DataSet();
        try
        {
            DAL.GetDataset("LS_GetMaterialMasterDetails", ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        return ds;
    }

    public DataSet deleteMaterialMaster(int MID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialMaster";
            c.Parameters.AddWithValue("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteBomiDetailsByEDIDAndVersionNumber()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteBomDetailsByEDIDAndVersionNumber";
            c.Parameters.AddWithValue("@EDID", EDID);
            c.Parameters.AddWithValue("@versionNumber", versionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    //public DataSet GetMaterialPartNumber()
    //{
    //    DAL = new cDataAccess();
    //    ds = new DataSet();
    //    c = new SqlCommand();
    //    try
    //    {
    //        c.CommandType = CommandType.StoredProcedure;
    //        c.CommandText = "LS_GetMaterialPartNumber";
    //        c.Parameters.AddWithValue("@Prefix", Prefix);
    //        c.Parameters.AddWithValue("@MGID", MGID);
    //        DAL.GetDataset(c, ref ds);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex);
    //    }
    //    return ds;
    //}

    public DataSet SaveDrawingBOMMaterialDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDrawingBOMMaterialDetails";
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", versionNumber);
            c.Parameters.AddWithValue("@MID", MID);
            c.Parameters.AddWithValue("@Quantity", Quantity);
            c.Parameters.Add("@DrawingSequenceNumber", DrawingSequenceNumber);
            c.Parameters.Add("@AddtionalPart", Addtionalpart);
            c.Parameters.Add("@LayoutAttachement", LayoutAttachement);
            c.Parameters.Add("@Remarks", Remarks);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBOMDetailsbyEDIDAndVersionnumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMHeaderDetailsByEDIDAndVersionNumber";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", versionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBOMDetailsByBOMID(int BOMID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMDetailsByBOMID";
            c.Parameters.Add("@BOMID", BOMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteBOMDetailsByBOMID(int BOMID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteBOMDetailsByBOMID";
            c.Parameters.Add("@BOMID", BOMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetDrawingSequencenumberByEDIDAndVersionnumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingSequenceNumberByEDIDAndDrawingVersionNumber";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@DrawingVersionNumber", versionNumber);
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

    public DataSet SaveMaterialSpecsDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialSpecsDetails";
            c.Parameters.Add("@SpecsName", SpecsName.ToUpper());
            c.Parameters.Add("@SpecsShortCode", SpecsShortCode.ToUpper());
            c.Parameters.Add("@FieldType", FieldType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetMaterialSpecsDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialSpecsDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet DeleteMaterialSpecs()
    {
        DataSet ds = new DataSet();

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialSpecs";
            c.Parameters.Add("@MSMID", MSMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialSpecsListByMGID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialSpecsListByMID";
            c.Parameters.Add("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet saveAssignedMaterialSpecsDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveAssignMaterialSpecsDetails";
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@MSMIDs", MSMIds);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialFormulaDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialFormulaDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveMaterialFormulaDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialFormulaDetails";
            c.Parameters.Add("@MFID", MFID);
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@MSMID", MSMID);
            c.Parameters.Add("@FormulaName", FormulaName.ToUpper());
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());

        }
        return ds;
    }

    public DataSet GetMaterialFormulaByFormulaID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialFormulaDetailsByFormulaID";
            c.Parameters.Add("@MFID", MFID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialCategoryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialCategoryDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveMaterialCategoryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialCategoryMasterDetails";
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@CategoryName", CategoryName.ToUpper());
            c.Parameters.Add("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetMaterialCategoryMasterDetailsByCategoryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "GetMaterialCategoryMasterDetailsByCategoryID";
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet DeleteMaterialCategoryDetailsByCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialCategoryDetailsByCategoryID";
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialFormulaDetailsByMaterialID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialFormulaDetailsbyMaterialID";
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", versionNumber);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetBOMCostDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMCostDetails";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", versionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBOMCostDetailsByETCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMCostDetailsByETCIDAndMID";
            c.Parameters.Add("@ETCID", ETCID);
            // c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@Flag", Flag.ToLower());
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateBOMStatusByEDIDAndVersionNumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateBOMStatusbyEDIDAndVersionNumber";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@versionNumber", versionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet LS_CheckItemBOMCostOfPendindMaterial()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_CheckItemBOMCostOfPendindMaterial";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@versionNumber", versionNumber);
            c.Parameters.Add("@CostEstimatedBy", EmpID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveItemDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveItemDetails";
            c.Parameters.Add("@ItemID", ItemID);
            c.Parameters.Add("@HSNCode", HSNCode);
            c.Parameters.Add("@ItemName", ItemName);
            c.Parameters.Add("@StandardItemFlag", StandardItemFlag);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemMasterDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getItemDetailsByItemID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemDetailsByItemID";
            c.Parameters.Add("@ItemID", ItemID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet DeleteItemDetailsByItemID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteItemDetails";
            c.Parameters.Add("@ItemID", ItemID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveItemSizeDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveItemSizeDetails";
            c.Parameters.Add("@SID", SID);
            c.Parameters.Add("@ItemSize", ItemSize.ToUpper());
            c.Parameters.Add("@Description", Description);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemSizeDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemSizeMasterDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetItemSizeDetailsBySID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSizeDetailsBySID";
            c.Parameters.Add("@SID", SID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteItemSizeDetailsBySID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteItemSizebySID";
            c.Parameters.Add("@SID", SID);
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
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetSupllierDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSupplierDetails";
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
    public DataSet SaveDuplicateBOMDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDuplicateBOMDetails";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@versionNumber", versionNumber);
            c.Parameters.Add("@DuplicateDDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemBOMStatusByDDID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemBOMStatusByDDID";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@versionNumber", versionNumber);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetViewCostingDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetViewCostingDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetViewCostingDetailsByItemID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetViewCostingDetailsByItemID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveMaterialGradeDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialGradeDetails";
            c.Parameters.Add("@MGMID", MGMID);
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@GradeName", GradeName);
            c.Parameters.Add("@SpecificWeight", SpecificWeight);
            c.Parameters.Add("@Cost", Cost);
            //  c.Parameters.Add("@UOM", UOM);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialGradeDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialGradeDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet deleteMaterialGradeNameByMGMID(int MGMID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialGradeNameByMGMID";
            c.Parameters.Add("@MGMID", MGMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateBOMStatusByDDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateBOMStatusByDDID";
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetQuanityAndUnitPriceByEDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetQuanityAndUnitPriceByEDID";
            c.Parameters.Add("@EDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string GetProspectNameByRFPHID()
    {
        string ProspectID = "";
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProspectNameByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            ProspectID = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ProspectID;
    }

    public DataSet SaveMaterialPlanningDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialPlanningDetails";
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@MGMIDInProduction", MGMIDinProduction);
            // c.Parameters.Add("@PartQuantityProduction", PartQuantityProduction);

            c.Parameters.Add("@THKId", THKId);
            //   c.Parameters.Add("@THKValue", THKValue);
            c.Parameters.Add("@UnitRequiredWeight", RequiredWeight);
            c.Parameters.Add("@AWTValue", AWTValue);

            if (joborderweight != 0)
                c.Parameters.Add("@UnitJobWeight", joborderweight);
            else
                c.Parameters.Add("@UnitJobWeight", DBNull.Value);
            if (jobtype != "")
                c.Parameters.Add("@jobtype", jobtype);
            else
                c.Parameters.Add("@jobtype", DBNull.Value);
            //  c.Parameters.Add("@MRNNumber", MRNNumber);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialPlanningDetailsByRFPHIDAndEDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialPlanningDetailsByRFPHIDAndEDID";
            c.Parameters.Add("@RFPHID", RFPHID);
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

    public DataSet UpdatematerialPlanningStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateMaterialPlanningStatus";
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

    public DataSet GetItemDetailsByRFPHIDforViewMaterialPlanning(string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "GetItemDetailsByRFPHIDforViewMaterialPlanning";
            c.CommandText = spname;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@Type", Type);
            c.Parameters.Add("@IndentBy", IndentBy);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet updateRFPStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateMPStatusByRFPHID";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePurchaseIndentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePurchaseIndentDetails";
            c.Parameters.Add("@PID", PID);
            if (Type == "RFP")
                c.Parameters.Add("@RFPHID", RFPHID);
            else
                c.Parameters.Add("@RFPHID", DBNull.Value);
            //  c.Parameters.Add("@RFPDID", RFPDID);
            //  c.Parameters.Add("@Date", DeliveryDate);
            c.Parameters.Add("@IndentBy", IndentBy);
            c.Parameters.Add("@IndentTo", IndentTo);
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@MGMID", MGMID);
            c.Parameters.Add("@CMID", CMID);
            c.Parameters.Add("@MTID", MTID);
            c.Parameters.Add("@Quantity", Quantity);
            c.Parameters.Add("@DeliveryDate", DeliveryDate);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@certficates", certficates);

            //   c.Parameters.Add("@BOMID", BOMID);

            // c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@THKID", THKId);
            c.Parameters.Add("@Type", Type);
            c.Parameters.Add("@MTFIDs", MTFIDs);
            c.Parameters.Add("@MTFIDsValue", MTFIDsValue);
            if (MPIDS == "")
                c.Parameters.Add("@MPIDs", DBNull.Value);
            else
                c.Parameters.Add("@MPIDs", MPIDS);
            c.Parameters.Add("@PIndentMRNnumber", PIndentMRNnumber);
            c.Parameters.Add("@Jobdescription", Jobdescription);
            c.Parameters.Add("@RW", RequiredWeight);

            if (string.IsNullOrEmpty(PurchaseCopy))
                c.Parameters.Add("@PurchaseCopy", DBNull.Value);
            else
                c.Parameters.Add("@PurchaseCopy", PurchaseCopy);
            c.Parameters.Add("@DrawingName", DrawingName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseIndentDetailsByRFHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByRFPHIDAndRFPDID";
            c.Parameters.Add("@RFPHID", RFPHID);
            // c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@Type", Type);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMGMIDAndCIDByBOMIDInMaterialplanning()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMGMIDAndCIDInMaterialPlanning";
            c.Parameters.Add("@BOMID", BOMID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet UpdateQualityStatusByMPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateQualityStatusByMPID";
            c.Parameters.Add("@MPID", MPID);
            c.Parameters.Add("@QualityStatus", QualityStatus);
            c.Parameters.Add("@Remarks", Remarks);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePurchaseIndentStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdatePurchaseIndentStatus";
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

    public DataSet SaveMaterialTypeSpecsDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialTypeSpecsDetails";
            c.Parameters.Add("@SpecsName", SpecsName.ToUpper());
            c.Parameters.Add("@SpecsShortCode", SpecsShortCode.ToUpper());
            DAL.GetDataset(c, ref ds);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialTypeFieldsDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetmaterialTypeSpecsDetails";
            DAL.GetDataset(c, ref ds);






        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet saveMaterialType()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveMaterialTypeDetails";
            c.Parameters.Add("@MaterialTypeId", MaterialTypeId);
            c.Parameters.Add("@TypeName", MaterialtypeName);
            c.Parameters.Add("@Description", Description);
            c.Parameters.Add("@MTFields", MTFields);
            DAL.GetDataset(c, ref ds);





        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialType()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialTypeDetails";

            DAL.GetDataset(c, ref ds);





        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteMaterialTypeSpecs()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeletematerialTypeSpecs";
            c.Parameters.Add("@MTFID", MTFID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetMaterialTypeMasterDetailsBymaterialtypeID()
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetmaterialTypeMasterDetailsBymaterialTypeID";
            c.Parameters.Add("@materialTypeId", MaterialTypeId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet updatePIStatus(string PID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdatePIStatusByRFPHID";
            if (RFPHID == 0)
                c.Parameters.Add("@RFPHID", DBNull.Value);
            else
                c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@PID", PID);
            c.Parameters.Add("@IndentBy", IndentBy);
            c.Parameters.Add("@IndentTo", IndentTo);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet UpdateDesignHApprovalStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateDesignHApprovalStatus";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@Mode", Mode);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@DeadinventoryAmount", DeadinventoryAmount);
            c.Parameters.Add("@DeadInventoryRemarks", DeadinventoryRemarks);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteMaterialType()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteMaterialTypeByMaterialTypeID";
            c.Parameters.Add("@MaterialTypeId", MaterialTypeId);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProductionBomMaterialFormulaDetailsByDDIDAndBOMID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionBOMMaterialFormulaDetailsByDDIDAndBOMID";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@MID", MID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetProductionBOMCostDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionBOMCostDetails";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProductionAddtionalPartRequestDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionAddtionalPartRequestDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseIndentDetailsByRFPHIDForPDF()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByRFPHIDForPDF";
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SavePartLayOutAttachements()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePartLayoutAttachements";
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@LayoutAttachement", LayoutAttachement);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSIBOMCostDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSIBOMCostDetails";
            c.Parameters.Add("@SIEHID", SIEHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDrawingSequencenumberBySTandardItemDetailsID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingSequenceNumberByStandardItemDetailsID";
            c.Parameters.Add("@SIEHID", SIEHID);
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

    public DataSet GetMaterialFormulaDetailsBySIEHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "GetMaterialFormulaDetailsBySIEHID";
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@SIEHID", SIEHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStandardItemBOMCostDetailsBySITCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMCostDetailsBySITCID";
            c.Parameters.Add("@SITCID", SITCID);
            // c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@Flag", Flag.ToLower());
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateBOMreviewStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateBOMreviewStatusByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPurchaseIndentDetailsByPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseIndentDetailsByPID";
            c.Parameters.Add("@PID", PID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet CheckPOProcessignIndentByPID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGeneralPurchaseindentDetailsByPID";
            c.Parameters.Add("@PID", PID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAddtionalpartApprovedDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAddtionalpartApprovedDetails";
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

    public void GetMaterialGroupName(DropDownList ddlMaterialGroup)
    {

        try
        {
            DAL.GetDataset("LS_GetMaterialGroupName", ref ds);
            {
                ddlMaterialGroup.DataSource = ds.Tables[0];
                ddlMaterialGroup.DataTextField = "Name";
                ddlMaterialGroup.DataValueField = "MaterialGroupName";
                ddlMaterialGroup.DataBind();
                ddlMaterialGroup.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void GetMaterialClassificationNom(DropDownList ddlMaterialClassificationNom)
    {

        try
        {
            ddlMaterialClassificationNom.Items.Clear();
            DAL.GetDataset("LS_GetMaterialClassificationNom", ref ds);
            {
                int count = ds.Tables[0].Rows.Count + 1;
                ddlMaterialClassificationNom.DataSource = ds.Tables[0];
                ddlMaterialClassificationNom.DataTextField = "ClassificationNom";
                ddlMaterialClassificationNom.DataValueField = "CMID";
                ddlMaterialClassificationNom.DataBind();
                ddlMaterialClassificationNom.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlMaterialClassificationNom.Items.Insert(count, new ListItem("AddNew", "-1"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    public void GetEnquiryNumber(DropDownList ddlEnquirynumber)
    {

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryNumberInDrawingBOM";




            DAL.GetDataset(c, ref ds);
            ddlEnquirynumber.DataSource = ds.Tables[0];
            ddlEnquirynumber.DataTextField = "EnquiryName";
            ddlEnquirynumber.DataValueField = "EnquiryID";
            ddlEnquirynumber.DataBind();
            ddlEnquirynumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    public void GetDrawingVersionNumberbyEnquiryNumber(DropDownList ddlVersionNumber)
    {

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingVersionNumberByEnquiryNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
            ddlVersionNumber.DataSource = ds.Tables[0];
            ddlVersionNumber.DataTextField = "Version";
            ddlVersionNumber.DataValueField = "FileName";
            ddlVersionNumber.DataBind();
            ddlVersionNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    public DataSet GetMaterialList(DropDownList ddlMaterialName)
    {

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialNameDetails";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", VersionNumber);

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

    public void GetMaterialGrade(DropDownList ddlmaterialgrade, string RFPHID)
    {
        DataSet ds = new DataSet();
        try
        {
            ddlmaterialgrade.ClearSelection();
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialGrade";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
            ddlmaterialgrade.DataSource = ds.Tables[0];
            ddlmaterialgrade.DataTextField = "GradeName";
            ddlmaterialgrade.DataValueField = "MGMIDProduction";
            ddlmaterialgrade.DataBind();
            ddlmaterialgrade.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetMaterialThickness(string RFPHID, string MPID)
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialThickness";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            //c.Parameters.AddWithValue("@MGMID", MGMID);
            c.Parameters.AddWithValue("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetMaterialFields(string MaterialTypeId, string Mode, int PID)
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETMaterialFields";
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
    public DataSet GetPartname(string RFPHID, string THKID)
    {
        DataSet ds = new DataSet();
        try
        {

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartname";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@MGMID", MGMID);
            c.Parameters.AddWithValue("@THKID", THKID);
            c.Parameters.AddWithValue("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void GetMaterialCategoryNameDetails(DropDownList ddlCategoryName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetMaterialCategoryDetails();

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
    }

    public void GetMaterialGroupNameDetails(DropDownList ddlMaterialGroupName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetMaterialGroup();
            ddlMaterialGroupName.DataSource = ds.Tables[0];
            ddlMaterialGroupName.DataTextField = "Name";
            ddlMaterialGroupName.DataValueField = "MGID";
            ddlMaterialGroupName.DataBind();
            ddlMaterialGroupName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetDrawingMaterialDetailsByEDIDAndVersionNumber(DropDownList ddlMaterialName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingMaterialDetailsByEDIDAndVersionNumber";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@versionNumber", VersionNumber);
            DAL.GetDataset(c, ref ds);

            ddlMaterialName.DataSource = ds.Tables[0];
            ddlMaterialName.DataTextField = "MaterialName";
            //ddlMaterialName.DataValueField = "MID";
            ddlMaterialName.DataValueField = "BOMID";
            ddlMaterialName.DataBind();
            ddlMaterialName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void GetItemDetailsByEnquiryNumber(DropDownList ddlItemName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemDetailsByEnquiryNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
            ddlItemName.DataSource = ds.Tables[0];
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "EDID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetCompletedBOMItemDetails(DropDownList ddlDuplicateItemName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMCompletedItemDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);

            DAL.GetDataset(c, ref ds);
            ddlDuplicateItemName.DataSource = ds.Tables[0];
            ddlDuplicateItemName.DataTextField = "ItemName";
            ddlDuplicateItemName.DataValueField = "DDID";
            ddlDuplicateItemName.DataBind();
            ddlDuplicateItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    //public DataSet GetAddtionalPartMaterialDetailsByEDIDAndVersionNumber(DropDownList ddlMaterialName)
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        DAL = new cDataAccess();
    //        c = new SqlCommand();
    //        c.CommandType = CommandType.StoredProcedure;
    //        c.CommandText = "LS_GetAddtionalPartOfItemMaterialByEDIDAndVersionNumber";
    //        c.Parameters.Add("@EDID", EDID);
    //        c.Parameters.Add("@versionNumber", versionNumber);

    //        DAL.GetDataset(c, ref ds);
    //        ddlMaterialName.DataSource = ds.Tables[0];
    //        ddlMaterialName.DataTextField = "MID";
    //        ddlMaterialName.DataValueField = "MaterialName";
    //        ddlMaterialName.DataBind();
    //        ddlMaterialName.Items.Insert(0, new ListItem("--Select--", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //    return ds;
    //}

    public DataSet GetMaterialNameDetails(DropDownList ddlMaterialName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetMaterialMaster();

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

    public DataSet GetBOMAddedItemDetails(DropDownList ddlDuplicateItemName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMAddedItemDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@ItemID", ItemID);
            c.Parameters.Add("@versionNumber", versionNumber);
            DAL.GetDataset(c, ref ds);
            ddlDuplicateItemName.DataSource = ds.Tables[0];
            ddlDuplicateItemName.DataTextField = "ItemName";
            ddlDuplicateItemName.DataValueField = "DDID";
            ddlDuplicateItemName.DataBind();
            ddlDuplicateItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemDetailsByRFPHID(DropDownList ddlItemName, string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetItemDetailsByRFPHID";
            c.CommandText = spname;
            c.Parameters.Add("@RFPHID", RFPHID);
            //c.Parameters.Add("@status", status);
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

    public DataSet GetPartDetailsByEDID(DropDownList ddlPartname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartNameByEDID";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
            ddlPartname.DataSource = ds.Tables[0];
            ddlPartname.DataTextField = "AssemplyPartName";
            ddlPartname.DataValueField = "BOMID";
            ddlPartname.DataBind();
            ddlPartname.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialGradeNameUsedInDesignByBOMIDAndEDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialGradeNameByBOMIDAndEDID";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@MPID", MPID);
            DAL.GetDataset(c, ref ds);
            //ddlmaterialGradeNameDesign.DataSource = ds.Tables[0];
            //ddlmaterialGradeNameDesign.DataTextField = "GradeName";
            //ddlmaterialGradeNameDesign.DataValueField = "MGMID";
            //ddlmaterialGradeNameDesign.DataBind();
            //ddlmaterialGradeNameDesign.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialGradeDetailsByCategoryID(DropDownList ddlMaterialNameProduction)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialGradeDetailsByCategoryID";
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);

            ddlMaterialNameProduction.DataSource = ds.Tables[0];
            ddlMaterialNameProduction.DataTextField = "GradeName";
            ddlMaterialNameProduction.DataValueField = "MGMID";
            ddlMaterialNameProduction.DataBind();
            ddlMaterialNameProduction.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetMaterialThicknessDetailsformaterialPlanning(DropDownList ddlThickness)
    {
        DataSet ds = new DataSet();
        try
        {
            ddlThickness.ClearSelection();
            ds = GetMaterialThicknessDetails();
            ddlThickness.DataSource = ds.Tables[0];
            ddlThickness.DataTextField = "THKValue";
            ddlThickness.DataValueField = "THKId";
            ddlThickness.DataBind();
            ddlThickness.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetMaterialGradeNameDetails(DropDownList ddlGradeName)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = GetMaterialGradeDetails();
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

    public DataSet GetMaterialGradeNameDetailsByEDID(DropDownList ddlGradeName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialGradeNameDetailsByEDID";
            c.Parameters.Add("@EDID", EDID);
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

    public DataSet getPurchaseEmployeeDetails(DropDownList ddlIndentTo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPurchaseEmployeeDetails";
            c.Parameters.Add("@EmployeeID", EmpID);
            DAL.GetDataset(c, ref ds);

            ddlIndentTo.DataSource = ds.Tables[0];
            ddlIndentTo.DataTextField = "EmployeeName";
            ddlIndentTo.DataValueField = "EmployeeID";
            ddlIndentTo.DataBind();
            ddlIndentTo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCertificateNameDetails(ListBox liCertificates)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCertificateDetails";
            DAL.GetDataset(c, ref ds);

            liCertificates.DataSource = ds.Tables[0];
            liCertificates.DataTextField = "CertificateName";
            liCertificates.DataValueField = "CFID";
            liCertificates.DataBind();
            liCertificates.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getMaterialTypeName(DropDownList ddlMaterialType)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialTypeNameDetails";
            DAL.GetDataset(c, ref ds);

            ddlMaterialType.DataSource = ds.Tables[0];
            ddlMaterialType.DataTextField = "MaterialTypeName";
            ddlMaterialType.DataValueField = "MTID";
            ddlMaterialType.DataBind();
            ddlMaterialType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartDetailsByRFPDID(DropDownList ddlpartname, int MRN)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartDetailsByRFPDID";
            c.Parameters.Add("@RFPDID", RFPDID);
            c.Parameters.Add("@MRN", MRN);
            DAL.GetDataset(c, ref ds);

            ddlpartname.DataSource = ds.Tables[0];
            ddlpartname.DataTextField = "PartName";
            ddlpartname.DataValueField = "MPID";
            ddlpartname.DataBind();
            ddlpartname.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetDropdownDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDropdownDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemDetailsByRFPHIDInAP(DropDownList ddlItemName, string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetItemDetailsByRFPHID";
            c.CommandText = spname;
            c.Parameters.Add("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
            ddlItemName.DataSource = ds.Tables[0];
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "DDID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPartDetailsByDDID(DropDownList ddlMaterialName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);

            ddlMaterialName.DataSource = ds.Tables[0];
            ddlMaterialName.DataTextField = "MaterialName";
            ddlMaterialName.DataValueField = "BOMID";
            ddlMaterialName.DataBind();
            ddlMaterialName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


    public DataSet GetProductionBOMCostDetailsByETCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProductionBOMCostDetailsByETCIDAndBOMID";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@BOMID", BOMID);
            c.Parameters.Add("@Flag", Flag.ToLower());
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateProductionAddtionalPartRequestByBOMID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateProductionAddtionalPartRequestByBOMID";
            c.Parameters.Add("@tblProductionPartDetails", dt);
            c.Parameters.Add("@Status", Flag);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            //c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveProductionAddtionalPartDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveProductionAddtionalPartDetails";
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPNoList(ListBox liRFPNo)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPNoList";
            c.Parameters.Add("@PID", PID);
            DAL.GetDataset(c, ref ds);

            liRFPNo.DataSource = ds.Tables[0];
            liRFPNo.DataTextField = "RFPNo";
            liRFPNo.DataValueField = "RFPHID";
            liRFPNo.DataBind();
            liRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetOldMRNNumber(DropDownList ddlOldMRNNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetOldMRNNumber";
            DAL.GetDataset(c, ref ds);

            ddlOldMRNNumber.DataSource = ds.Tables[0];
            ddlOldMRNNumber.DataTextField = "MRNNumber";
            ddlOldMRNNumber.DataValueField = "MRNID";
            ddlOldMRNNumber.DataBind();
            ddlOldMRNNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAllEnquiryNumberAndCustomerName(DropDownList ddlEnquiryNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAllEnquiryNumberAndCustomerName";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
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

    public DataSet GetCostEstimationCompletedItemListByEnquiryNumber(DropDownList ddlDuplicateItemName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCostEstimationCompletedItemListByEnquiryNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);

            ddlDuplicateItemName.DataSource = ds.Tables[0];
            ddlDuplicateItemName.DataTextField = "ItemName";
            ddlDuplicateItemName.DataValueField = "DDID";
            ddlDuplicateItemName.DataBind();
            ddlDuplicateItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void GetMaterialClassificationNomByPurchaseindent(DropDownList ddlMaterialClassificationNom, string type)
    {
        try
        {
            ddlMaterialClassificationNom.Items.Clear();

            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialClassificationNomByPurchaseindent";
            c.Parameters.Add("@type", type);
            DAL.GetDataset(c, ref ds);

            //DAL.GetDataset("LS_GetMaterialClassificationNomByPurchaseindent", ref ds);
            //{
            //    int count = ds.Tables[0].Rows.Count + 1;
            //    ddlMaterialClassificationNom.DataSource = ds.Tables[0];
            //    ddlMaterialClassificationNom.DataTextField = "ClassificationNom";
            //    ddlMaterialClassificationNom.DataValueField = "CMID";
            //    ddlMaterialClassificationNom.DataBind();
            //    ddlMaterialClassificationNom.Items.Insert(0, new ListItem("--Select--", "0"));
            //}

            ddlMaterialClassificationNom.DataSource = ds.Tables[0];
            ddlMaterialClassificationNom.DataTextField = "ClassificationNom";
            ddlMaterialClassificationNom.DataValueField = "CMID";
            ddlMaterialClassificationNom.DataBind();
            ddlMaterialClassificationNom.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetMaterialCategoryNameDetailsInMaterialPLanning(DropDownList ddlCategoryName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialCategoryNameDetailsInMaterialPlanning";
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
    }

    public DataSet GetAddtionalPartNameListByDDID(DropDownList ddlMaterialName)
    {

        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAddtionalPartNameListByDDID";
            c.Parameters.Add("@DDID", DDID);

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

    public DataSet GetItemDetailsByRFPQualityPlanning(DropDownList ddlItemName, string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetItemDetailsByRFPHID";
            c.CommandText = spname;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@status", status);
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

    public DataSet GetItemDetailsByRFPHIDByMaterialPlanning(DropDownList ddlItemName, string spname)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            // c.CommandText = "LS_GetItemDetailsByRFPHID";
            c.CommandText = spname;
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

    #endregion

}

