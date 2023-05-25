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


    //Material Specs Master Properties

    public string SpecsName { get; set; }
    public string SpecsShortCode { get; set; }
    public int MSMID { get; set; }
    public string MSMIds { get; set; }

    public string FormulaName { get; set; }
    public int MFID { get; set; }

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
            c.Parameters.AddWithValue("@MGID", MGID);
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
            c.Parameters.AddWithValue("@CMID", CMID);
            c.Parameters.AddWithValue("@MGID", MGID);
            c.Parameters.AddWithValue("@MaterialName", MaterialName.ToUpper());
            c.Parameters.AddWithValue("@MaterialPartnumber", MaterialPartNumber);
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
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@VersionNumber", versionNumber);
            c.Parameters.AddWithValue("@MID", MID);
            c.Parameters.AddWithValue("@Quantity", Quantity);
            c.Parameters.Add("@DrawingSequenceNumber", DrawingSequenceNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetBOMDetailsbyEnquiryNumberAndVersionnumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetBOMHeaderDetailsByEnquiryNumberAndDrawingVersionNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
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

    public DataSet GetDrawingSequencenumberByEnquiryIDAndVersionnumber()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingSequenceNumberByEnquiryIDAndDrawingVersionNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@DrawingVersionNumber", versionNumber);
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

    public DataSet GetMaterialSpecsListByMaterialID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialSpecsListByMaterialID";
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

    public void GetMaterialList(DropDownList ddlMaterialName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaterialNameDetails";
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
    }

    #endregion

}

