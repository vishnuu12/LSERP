using eplus.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for cQCTestReports
/// </summary>
public class cQCTestReports
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
    public string inditicationSize;

    public int RFPDID { get; set; }
    public int RFPHID { get; set; }
    public DateTime TestDate { get; set; }
    public string BSLNo { get; set; }
    public string QTY { get; set; }
    public string Remarks { get; set; }
    public int CreatedBy { get; set; }
    public string RIRHID { get; set; }

    public DataTable dt { get; set; }
    public int LPIRID { get; set; }
    public string BellowSNo { get; set; }
    public string StageOfTest { get; set; }
    public string MaterialSpecification { get; set; }
    public string PenetrantBrandName { get; set; }
    public string PenetrantBatchNo { get; set; }
    public string Thickness { get; set; }
    public string CleanRemoverBrandName { get; set; }
    public string CleanRemoverBatchNo { get; set; }
    public string ProcedureAndRevNo { get; set; }
    public string DwellTime { get; set; }
    public string SurfaceCondition { get; set; }
    public string DeveloperBrandName { get; set; }
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
    public string JointIdentification { get; set; }
    public string IndicationType { get; set; }
    public string Interpretaion { get; set; }
    public string Disposition { get; set; }
    public string LSIInspectorName { get; set; }
    public string LSIInspectorLevel { get; set; }
    public DateTime LSIInspectionDate { get; set; }
    public string ThirdPartyInspectorname { get; set; }
    public string ThirdPartyInspectionLevel { get; set; }
    public DateTime ThirdPartyInspectionDate { get; set; }
    public string PTRHID { get; set; }
    public string TestProcedureNo { get; set; }
    public string TypeOfTest { get; set; }
    public string Medium { get; set; }
    public string TestPressure { get; set; }
    public string HoldingTime { get; set; }
    public string TestTemprature { get; set; }
    public string Result { get; set; }
    public int TPTID { get; set; }

    public int CalibrationID1 { get; set; }
    public int CalibrationID2 { get; set; }

    #endregion

    #region"Properties"

    #endregion

    #region"Common Methods"

    public DataSet GetPartDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPartDetailsForRawMaterialInspectionReportByRFPDID";
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveRawMaterialInspectionReport()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRawMaterialInspectionReportDetails";
            c.Parameters.AddWithValue("@RIRHID", RIRHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@TestDate", TestDate);
            c.Parameters.AddWithValue("@BSLNo", BSLNo);
            c.Parameters.AddWithValue("@QTY", QTY);
            c.Parameters.AddWithValue("@Remarks", Remarks);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@tblRawMaterialinspectionReportDetails", dt);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRawMaterialInspectionReportDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRawMaterialInspectionReportDetailsByRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRawMaterialInspectionReportByRIRHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRawMaterialInspectionReportByRIRHID";
            c.Parameters.AddWithValue("@RIRHID", RIRHID);
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

    public DataSet GetQualityTestReportDetailsForPrintByID(string spname, string ID)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.AddWithValue("@ID", ID);
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

            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@LPIRID", LPIRID);
            c.Parameters.AddWithValue("@BellowSNo", BellowSNo);
            c.Parameters.AddWithValue("@StageOfTest", StageOfTest);
            c.Parameters.AddWithValue("@TestDate", TestDate);
            c.Parameters.AddWithValue("@MaterialSpecification", MaterialSpecification);
            c.Parameters.AddWithValue("@PenetrantBrandName", PenetrantBrandName);
            c.Parameters.AddWithValue("@PenetrantBatchNo", PenetrantBatchNo);
            c.Parameters.AddWithValue("@Thickness", Thickness);
            c.Parameters.AddWithValue("@CleanRemoverBrandName", CleanRemoverBrandName);
            c.Parameters.AddWithValue("@CleanRemoverBatchNo", CleanRemoverBatchNo);
            c.Parameters.AddWithValue("@ProcedureAndRevNo", ProcedureAndRevNo);
            c.Parameters.AddWithValue("@DwellTime", DwellTime);
            c.Parameters.AddWithValue("@SurfaceCondition", SurfaceCondition);
            c.Parameters.AddWithValue("@DeveloperBrandName", DeveloperBrandName);
            c.Parameters.AddWithValue("@DeveloperBatchNo", DeveloperBatchNo);
            c.Parameters.AddWithValue("@SurfaceTemprature", SurfaceTemprature);
            c.Parameters.AddWithValue("@DevelopementTime", DevelopementTime);
            c.Parameters.AddWithValue("@PenetrateSystem", PenetrateSystem);
            c.Parameters.AddWithValue("@LightningEquipment", LightningEquipment);
            c.Parameters.AddWithValue("@Technique", Technique);
            c.Parameters.AddWithValue("@LightIntensity", LightIntensity);
            c.Parameters.AddWithValue("@SheetOfIndications", SheetOfIndications);
            c.Parameters.AddWithValue("@InspectionQty", InspectionQty);
            c.Parameters.AddWithValue("@AcceptedQty", AcceptedQty);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@JointIdentification", JointIdentification);
            c.Parameters.AddWithValue("@IndicationType", IndicationType);
            c.Parameters.AddWithValue("@inditicationSize", inditicationSize);
            c.Parameters.AddWithValue("@Interpretaion", Interpretaion);
            c.Parameters.AddWithValue("@Disposition", Disposition);
            c.Parameters.AddWithValue("@LSIInspectorName", LSIInspectorName);
            c.Parameters.AddWithValue("@LSIInspectorLevel", LSIInspectorLevel);
            c.Parameters.AddWithValue("@LSIInspectionDate", LSIInspectionDate);
            c.Parameters.AddWithValue("@ThirdPartyInspectorname ", ThirdPartyInspectorname);
            c.Parameters.AddWithValue("@ThirdPartyInspectionLevel", ThirdPartyInspectionLevel);
            c.Parameters.AddWithValue("@ThirdPartyInspectionDate ", ThirdPartyInspectionDate);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetLPIreportDetailsByRFPHID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLPIReportDetailsByRFPHID";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetLPIDetailsLPIRID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetLPIDetailsByLPIRID";
            c.Parameters.AddWithValue("@LPIRID", LPIRID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetPressureTestReportDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetPressureTestReportDetails";
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
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
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SavePressureTestReportDetails";
            c.Parameters.AddWithValue("@PTRHID", PTRHID);
            c.Parameters.AddWithValue("@RFPHID", RFPHID);
            c.Parameters.AddWithValue("@RFPDID", RFPDID);
            c.Parameters.AddWithValue("@TPTID", TPTID);
            c.Parameters.AddWithValue("@BSLNo", BSLNo);
            c.Parameters.AddWithValue("@QTY", QTY);
            c.Parameters.AddWithValue("@TestProcedureNo", TestProcedureNo);
            c.Parameters.AddWithValue("@Medium", Medium);
            c.Parameters.AddWithValue("@TestPressure", TestPressure);
            c.Parameters.AddWithValue("@HoldingTime", HoldingTime);
            c.Parameters.AddWithValue("@CalibrationID1", CalibrationID1);
            c.Parameters.AddWithValue("@CalibrationID2", CalibrationID2);
            c.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            c.Parameters.AddWithValue("@TestDate", TestDate);
            c.Parameters.AddWithValue("@Result", Result); 
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
            // c.CommandText = "LS_GetPressureTestReportDetailsByPTRHID_PRINT";
            c.CommandText = "LS_GetPressureTestReportDetailsByPTRHID";
            c.Parameters.Add("@PTRHID", PTRHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #region"DropDown Events"

    public DataSet GetTypeOfTest(DropDownList ddlTypeOftest)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetTypeOfTest";
            DAL.GetDataset(c, ref ds);

            ddlTypeOftest.DataSource = ds.Tables[0];
            ddlTypeOftest.DataTextField = "TestName";
            ddlTypeOftest.DataValueField = "TPTID";
            ddlTypeOftest.DataBind();
            ddlTypeOftest.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetCalibrationDetails(DropDownList ddlCalibrationNo1, DropDownList ddlCalibrationNo2)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCalibrationDetails";
            DAL.GetDataset(c, ref ds);

            ddlCalibrationNo1.DataSource = ds.Tables[0];
            ddlCalibrationNo1.DataTextField = "CodeNo";
            ddlCalibrationNo1.DataValueField = "CalibrationId";
            ddlCalibrationNo1.DataBind();
            ddlCalibrationNo1.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlCalibrationNo2.DataSource = ds.Tables[0];
            ddlCalibrationNo2.DataTextField = "CodeNo";
            ddlCalibrationNo2.DataValueField = "CalibrationId";
            ddlCalibrationNo2.DataBind();
            ddlCalibrationNo2.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion


    #endregion
}