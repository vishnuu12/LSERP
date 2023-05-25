using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eplus.data;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public class cDesign
{
    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    public string itemCodeType;
    public int revisionNumber;
    public string pressure;
    public int SIEHID;
    public string ItemCodeValue;
    public string tabname;
    public DateTime DeadLineDate;

    #endregion

    #region"Properties"

    public int UserID { get; set; }
    public int EnquiryNumber { get; set; }
    public string AttachementName { get; set; }
    public int VersionNumber { get; set; }
    public string Remarks { get; set; }

    public int DDID { get; set; }
    public string ChangesRequested { get; set; }
    public int CommercialImpact { get; set; }
    public int StateAmount { get; set; }
    public string DelayInImplementation { get; set; }
    public DateTime ReScheduledDate { get; set; }
    public string RejectReason { get; set; }

    public string Datetime { get; set; }


    public int ItemID { get; set; }

    public string drawingName { get; set; }
    public string DesignNumber { get; set; }
    public string DesignCode { get; set; }
    public string Tag { get; set; }
    public string OverAllLength { get; set; }
    public string ListOfDeviation { get; set; }
    public string DeviationAttachementName { get; set; }

    public int EDID { get; set; }
    public int BOMCostApprove { get; set; }
    public int SharedWithSales { get; set; }

    public int RFPHID { get; set; }
    public int POHID { get; set; }
    public string RFPNo { get; set; }
    public int LocationID { get; set; }
    public string QAPRefNo { get; set; }
    public string ProjectName { get; set; }
    public int QAPApproval { get; set; }
    public int DrawingApproval { get; set; }
    public DateTime DeliveryDate { get; set; }
    public int InspectionRequirtment { get; set; }
    public int LDClause { get; set; }
    public string DespatchDetails { get; set; }
    public string NotesSummary { get; set; }

    public string StatusFlag { get; set; }
    public bool Date { get; set; }
    public int Flag { get; set; }

    public string Description { get; set; }
    public int Attachementtype { get; set; }

    public int AttachementID { get; set; }
    public int Sizeid { get; set; }
    public string Tagno { get; set; }
    public string Temprature { get; set; }
    public string Movement { get; set; }
    public int ProcessID { get; set; }
    public string CurrentStatus { get; set; }
    public string ReScheduledSubmissiondate { get; set; }
    public string RescheduledateReason { get; set; }

    public string DrawingNo { get; set; }
    public string RevNo { get; set; }
    public string DrawingFile { get; set; }
    public string ExcelFile { get; set; }
    public string BOMCost { get; set; }
    public string AddtionalPartBOMCost { get; set; }
    public string CreatedBy { get; set; }
    public int DBDID { get; set; }
    public string Stress { get; set; }
    public string MOCID { get; set; }
    public string FYID { get; set; }
    #endregion

    #region"Masters"



    #endregion

    #region"Common Methods"

    public DataSet SaveDesignDocumentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDesignDocumentDetails";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", VersionNumber);
            c.Parameters.Add("@AttachementName", AttachementName);
            c.Parameters.Add("@Remarks", Remarks);

            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@drawingName", drawingName);
            c.Parameters.Add("@DesignNumber", DesignNumber);
            c.Parameters.Add("@DesignCode", DesignCode);
            c.Parameters.Add("@Tag", Tag);
            c.Parameters.Add("@OverAllLength", OverAllLength);
            c.Parameters.Add("@ListOfDeviation", ListOfDeviation);
            c.Parameters.Add("@DeviationAttachementName", DeviationAttachementName);
            c.Parameters.Add("@BOMCostApprove", BOMCostApprove);
            c.Parameters.Add("@UserID", UserID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetDesignDetailsByEDIDForApproval(int enqno)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignDetailsByEDIDForApproval";
            c.Parameters.AddWithValue("@EnquiryNumber", enqno);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateForwardToCustomerStatusInDesignDocumentDetails(int DDID)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSharedWithCustomerStatusInDesignDocument";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateApproveDesignStatus(int DDID, int status)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateCustomerDrawingApprovalStatus";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetApprovedDesignDetailsByDDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetReopenApprovedDesignDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public void UpdateCustomerApprovedDrawingVersionStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateCustomerApprovedDrawingVersionStatus";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet SaveReopenApprovedDrawingDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveReopenApprovalDrawingDetails";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@ChangesRequested", ChangesRequested);
            c.Parameters.Add("@CommercialImpact", CommercialImpact);
            c.Parameters.Add("@StateAmount", StateAmount);
            c.Parameters.Add("@DelayInImplementation", DelayInImplementation);
            if (Datetime != "")
                c.Parameters.Add("@ReScheduledDate", ReScheduledDate);
            else
                c.Parameters.Add("@ReScheduledDate", DBNull.Value);

            c.Parameters.Add("@NotApprovedReason", RejectReason);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignDocumentDetailsByEnquiryIDAndItemID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignDocumentDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@ItemID", ItemID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignDocumentDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignDocumentDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateDrawingCustomerResponseStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUpdateDrawingCustomerResponseStatus";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@VersionNumber", VersionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
    public DataSet UpdateSharedWithSalesStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSharedWithSalesStatus";
            //   c.Parameters.Add("@EDID", EDID);
            //   c.Parameters.Add("@Version", VersionNumber);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@DDID", DDID);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet SaveRFPHeaderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveRFPHeader";
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@POHID", POHID);
            c.Parameters.Add("@RFPNo", RFPNo);
            c.Parameters.Add("@LocationID", LocationID);
            c.Parameters.Add("@ProjectName", ProjectName);
            c.Parameters.Add("@QAPRefNo", QAPRefNo);
            c.Parameters.Add("@QAPApproval", QAPApproval);
            c.Parameters.Add("@DrawingApproval", DrawingApproval);
            c.Parameters.Add("@InspectionRequirtment", InspectionRequirtment);
            c.Parameters.Add("@LDClause", LDClause);
            c.Parameters.Add("@DespatchDetails", DespatchDetails);
            c.Parameters.Add("@NotesSummary", NotesSummary);

            if (Date == false)
                c.Parameters.Add("@DeliveryDate", DBNull.Value);
            else
                c.Parameters.Add("@DeliveryDate", DeliveryDate);

            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetRFPHeaderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRFPDetails";
            c.Parameters.Add("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public string GetProspectNameByPOHID()
    {
        string ProspectID = "";
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProspectNameByPOHID";
            c.Parameters.Add("@POHID", POHID);
            ProspectID = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ProspectID;
    }

    public DataSet UpdateRFPStatus(string spName)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spName;
            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@StatusFlag", StatusFlag);
            c.Parameters.Add("@CreatedBy", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateSharedWithHODStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateSharedWithHODStatus";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@Version", VersionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateDrawingHODApprovalStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateHODDrawingApprovalStatus";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetUnSharedWithSalesDesignDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetUnSharedWithSalesDesignDetailsByEnquiryID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet RejectDrawingStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateRejetcDrawingStatusByDDID";
            c.Parameters.Add("@DDID", DDID);
            c.Parameters.Add("@Remarks", Remarks);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetDesignDetailsByDDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet saveDesignMultiAttachementDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDesignMultiAttachementDetails";
            c.Parameters.Add("@EnquiryID", EnquiryNumber);
            c.Parameters.Add("@AttachementDescription", Description);
            c.Parameters.Add("@AttachementTypeID", Attachementtype);
            c.Parameters.Add("@FileName", AttachementName);
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetAttachementsDetailsByDDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignAttachementsDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetMaxRFPNoINRFPHeader()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMaxRFPNoInRFPHeader";
            c.Parameters.Add("@POHID", POHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetRevisionNumberDetailsByDDID(DropDownList ddlRevisionNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRevisionNumberDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);

            ddlRevisionNumber.DataSource = ds.Tables[0];
            ddlRevisionNumber.DataTextField = "RevisionNumber";
            ddlRevisionNumber.DataValueField = "RevisionNumber";
            ddlRevisionNumber.DataBind();
            ddlRevisionNumber.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet UpdateItemSharedStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateItemSharedStatusByEnquiryNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet BindStandardItemHeaderDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStandardItemHeaderDetails";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    //SIEHID
    //ItemID
    //drawingName
    //Sizeid
    //itemCodeType
    //Tagno
    //Description
    //revisionNumber
    //pressure
    //Temprature
    //Movement

    public DataSet SaveStandardItemEntryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStandardItemEntryDetails";
            c.Parameters.Add("@SIEHID", SIEHID);
            c.Parameters.Add("@ItemID", ItemID);
            c.Parameters.Add("@drawingName", drawingName);
            c.Parameters.Add("@Sizeid", Sizeid);
            c.Parameters.Add("@itemCodeType", itemCodeType);
            c.Parameters.Add("@ItemCodeValue", ItemCodeValue);
            c.Parameters.Add("@Description", Description);
            c.Parameters.Add("@revisionNumber", revisionNumber);
            c.Parameters.Add("@pressure", pressure);
            c.Parameters.Add("@Temprature", Temprature);
            c.Parameters.Add("@Movement", Movement);
            c.Parameters.Add("@CreatedBy", UserID);
            c.Parameters.Add("@AttachementName", AttachementName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet ShareStandardItemDetailsBySIEHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_ShareStandardItemDetailsBySIEHID";
            c.Parameters.Add("@SIEHID", SIEHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getStandardItemDetailsBySIEHID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStandardItemDetailsBySIEHID";
            c.Parameters.Add("@SIEHID", SIEHID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetProcessDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetProcessDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getItemDetailsByEnquiryNumberAndProcessID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemDetailsByProcessIDAndEnquiryNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@ProcessID", ProcessID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateItemStaffAssignment(string EDID, string Checked, string ProcessID, string EmployeeID, DateTime DeadLineDate)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateItemStaffAssignment";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@Checked", Checked);
            c.Parameters.Add("@ProcessID", ProcessID);
            c.Parameters.Add("@EmployeeID", EmployeeID);
            c.Parameters.Add("@DeadLineDate", DeadLineDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemDetailsAndItemSpecificationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_ItemDetailsAndItemSpecificationDetailsByEnquiryNumber";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@tabname", tabname);
            c.Parameters.Add("@UserID", UserID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveItemSpecsDocsDetails(string EDIDs)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveItemSpecDetails";
            c.Parameters.Add("@tabname", tabname);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@EDIDs", EDIDs);
            c.Parameters.Add("@AttachementName", AttachementName);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateEnquiryProcessStaffAssignmentByEnquiryNumberAndProcessName()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateEnquiryProcessStaffAssignmentByEnquiryNumberAndProcessID";
            c.Parameters.Add("@EnquiryID", EnquiryNumber);
            c.Parameters.Add("@UserID", UserID);
            c.Parameters.Add("@ProcessID", ProcessID);
            c.Parameters.Add("@DeadLineDate", DeadLineDate);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateOverAllItemStatusByEDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateOverAllItemStatusByEDID";
            c.Parameters.Add("@EDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet UpdateProcessStatusByEnquiryNumberAndItemID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateProcessStatusByEnquiryNumberAndItemID";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@tabname", tabname);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveBomCostBulkDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveBomBulkDetails";
            c.Parameters.Add("@VersionNumber", VersionNumber);
            c.Parameters.Add("@EDID", EDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignActivitiesEnquiryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignActivitiesEnquiryDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveDailyActivitiesDesignEnquiryDetails(bool rsdate)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDailyActivitiesDesignEnquiryDetails";
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

    public DataSet GetSalesStaffDetailsByEnquiryID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetSalesStaffDetailsByEnquiryID";
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
            c.CommandText = "LS_GetViewCostingSheetDetailsByDDID";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet ReviewDrawingByDDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_drawingapprovalreverse";
            c.Parameters.Add("@DDID", DDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveDirectBOMDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDirectBOMDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@DrawingNo", DrawingNo);
            c.Parameters.Add("@DrawingFile", DrawingFile);
            c.Parameters.Add("@ExcelFile", ExcelFile);
            c.Parameters.Add("@BOMCost", BOMCost);
            c.Parameters.Add("@AddtionalPartBOMCost", AddtionalPartBOMCost);
            c.Parameters.Add("@CreatedBy", CreatedBy);
            c.Parameters.Add("@RevNo", RevNo);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

    #region"DropDownList"

    public void GetEnquiryNumberBySalesUserID(int SalesUserID, DropDownList ddlEnquiryNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDBySalesUserID";
            c.Parameters.Add("@SalesEmployeeID", SalesUserID);
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
    }

    public void GetEnquiryNumberByDesignUserID(int DesignUserID, DropDownList ddlEnquiryNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEnquiryIDByDesignUserID";
            c.Parameters.Add("@DesignEmployeeID", DesignUserID);
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
    }

    public void GetCustomerApprovedDrawings(int SalesUserID, DropDownList ddlEnquiryNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCustomerApprovedDesignDrawings";
            c.Parameters.Add("@SalesHOD", SalesUserID);
            DAL.GetDataset(c, ref ds);
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "DetailsAvailable")
            {
                ddlEnquiryNumber.DataSource = ds.Tables[1];
                ddlEnquiryNumber.DataTextField = "EnquiryName";
                ddlEnquiryNumber.DataValueField = "DDID";
                ddlEnquiryNumber.DataBind();
                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    public DataSet GetDrawingRevisionNumberByEnquiryDetailsID(DropDownList ddlVersionNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDrawingRevisionNumberByEnquiryDetailID";
            c.Parameters.Add("@EDID", EDID);
            DAL.GetDataset(c, ref ds);

            ddlVersionNumber.DataSource = ds.Tables[0];
            ddlVersionNumber.DataTextField = "Version";
            ddlVersionNumber.DataValueField = "Version";
            ddlVersionNumber.DataBind();
            ddlVersionNumber.Items.Insert(0, new ListItem("--Select--", "-1"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetRevisionNumberAndAttachementNameByEDID(DropDownList ddlVersionNumber)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetRevisionNumberAndAttachementNameByEDID";
            c.Parameters.Add("@EDID", EDID);
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
        return ds;
    }

    public DataSet GetPressureUnitsDetails(DropDownList ddlPrsureUnits)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_getPresureUnitsDetails";
            DAL.GetDataset(c, ref ds);

            ddlPrsureUnits.DataSource = ds.Tables[0];
            ddlPrsureUnits.DataTextField = "Name";
            ddlPrsureUnits.DataValueField = "PUID";
            ddlPrsureUnits.DataBind();
            ddlPrsureUnits.Items.Insert(0, new ListItem("--Select--", "0"));

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStandardItemAndSizeDetails(DropDownList ddlItemname, DropDownList ddlSize)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStandardItemAndSizeDetails";
            DAL.GetDataset(c, ref ds);

            ddlItemname.DataSource = ds.Tables[0];
            ddlItemname.DataTextField = "ItemName";
            ddlItemname.DataValueField = "ItemID";
            ddlItemname.DataBind();
            ddlItemname.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlSize.DataSource = ds.Tables[1];
            ddlSize.DataTextField = "ItemSize";
            ddlSize.DataValueField = "SID";
            ddlSize.DataBind();
            ddlSize.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetStandardItemSizeDetailsByItemID(DropDownList ddlSISize)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetStandardItemSizeDetailsByItemID";
            c.Parameters.Add("@ItemID", ItemID);
            DAL.GetDataset(c, ref ds);

            ddlSISize.DataSource = ds.Tables[0];
            ddlSISize.DataTextField = "SizeName";
            ddlSISize.DataValueField = "SIEHID";
            ddlSISize.DataBind();
            ddlSISize.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetEmployeeNameList(DropDownList ddlEmployee)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEmployeeNameList";
            DAL.GetDataset(c, ref ds);

            ddlEmployee.DataSource = ds.Tables[0];
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet getPendingDesignEnquiryIDByUserID(int UserID, DropDownList ddlCustomerName, DropDownList ddlEnquiryNumber)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignPendingEnquiryIDByUserID";
            c.Parameters.Add("@EmployeeID", UserID);
            DAL.GetDataset(c, ref ds);

            ddlEnquiryNumber.DataSource = ds.Tables[0];
            ddlEnquiryNumber.DataTextField = "EnquiryName";
            ddlEnquiryNumber.DataValueField = "EnquiryID";
            ddlEnquiryNumber.DataBind();
            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlCustomerName.DataSource = ds.Tables[1];
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

    public void designPendingEnquiryIDChange(DropDownList ddlCustomerName, DropDownList ddlEnquiryNumber, DataTable dcustomr, DataTable denquiry)
    {
        DataView dv;
        try
        {
            if (ddlCustomerName.SelectedIndex > 0)
            {
                dv = new DataView(dcustomr);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "EnquiryName";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "EnquiryName";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }

            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            //divInfo.Visible = false;
            //divGrid.Visible = false;

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void designpendingenquiryddlchange(DropDownList ddlEnquiryNumber, DropDownList ddlCustomerName, DataTable dcustomr, DataTable denquiry)
    {
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                DataView dv = new DataView(denquiry);
                dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                denquiry = dv.ToTable();
                ddlCustomerName.SelectedValue = denquiry.Rows[0]["ProspectID"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet GetCustomerNameByPendingList(int UserID, DropDownList ddlCustomerName, string spname, string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
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

    public DataSet GetEnquiryNumberByPendingList(int UserID, DropDownList ddlCustomerName, string spname, string status)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = spname;
            c.Parameters.Add("@EmployeeID", UserID);
            c.Parameters.Add("@status", status);
            DAL.GetDataset(c, ref ds);

            ddlCustomerName.DataSource = ds.Tables[0];
            ddlCustomerName.DataTextField = "EnquiryNumber";
            ddlCustomerName.DataValueField = "EnquiryID";
            ddlCustomerName.DataBind();
            ddlCustomerName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetItemDetailsByBOM(DropDownList ddlItemName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetItemDetailsByDirectBOM";
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
        return ds;
    }

    public DataSet GetDirectBOMEntryDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDirectBOMEntryDetails";
            c.Parameters.Add("@EnquiryNumber", EnquiryNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet EditDirectBOmDetailsByDBDID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDirectBOMDetailsByItemName";
            c.Parameters.Add("@DBDID", DBDID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet BindMOCDetails(DropDownList ddlItemName)
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMOCDetails";
            DAL.GetDataset(c, ref ds);

            ddlItemName.DataSource = ds.Tables[0];
            ddlItemName.DataTextField = "Name";
            ddlItemName.DataValueField = "MOCID";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignFatigueValuesDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDesignFatigueValueDetails";
            c.Parameters.Add("@MOCID", MOCID);
            c.Parameters.Add("@Temprature", Temprature);
            c.Parameters.Add("@Stress", Stress);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDDIDByEDIDAndVersionNo()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetDDIDByEDIDAndVersionNo";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", VersionNumber);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetDesignKPIReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@FYID", FYID);
            c.Parameters.Add("@EmployeeID", UserID);
            c.CommandText = "LS_DesignKPIReports_OverALL";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetNoteTemparaturerangeDetailsByMOCID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.Parameters.Add("@MOCID", MOCID);
            c.CommandText = "LS_GetTemparatureRangeDetailsByMOCID";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
	
	public DataSet GetCAPAReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCAPAReports";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }
	
	    public DataSet GetRFPPaymentCollectionReports()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_RFPPaymentCollectionValue";
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