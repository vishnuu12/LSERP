using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_UltraSonicExaminationReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cSales objSales;
    cMaterials objMat;
    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet dsPOHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                DataSet dsLocation = new DataSet();

                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO);

                ViewState["Customer"] = dsCustomer.Tables[0];
                ViewState["PO"] = dsPOHID.Tables[0];
                ShowHideControls("input");
                bindReportNo();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteUltraSonicReportDetailsByUserID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    ddlRFPNo_SelectIndexChanged(null, null);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        cDesign objDesign = new cDesign();
        objSales = new cSales();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;

                objSales.POHID = ddlCustomerPO.SelectedValue;
                objSales.GetRFPDetailsByPOHID(ddlRFPNo);

                ShowHideControls("input,view");
            }
            else
            {
                ddlCustomerName.SelectedIndex = 0;
                ShowHideControls("input");
            }

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["PO"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlCustomerPO.DataSource = dt;
            ddlCustomerPO.DataTextField = "PORefNo";
            ddlCustomerPO.DataValueField = "POHID";
            ddlCustomerPO.DataBind();
            ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsBYRFPHIDInUTReportDetails");
                //  lblTotalItemQty.Text = hdnTotalItemQty.Value = "Total Item Qty" + ds.Tables[0].Rows[0]["QTY"].ToString();

                bindUltraSonicReportHeader();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            ds = objQt.GetItemSpecificationDetailsByRFPDIDAndEDID(null, null, null);

            txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            txtJobDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            ////txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBSLNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;
            txtCustomername.Text = ddlCustomerName.SelectedItem.Text;
            //txtRFPNo.Text = ddlRFPNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveUTR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {

            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.USERID = hdnUserID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Material = txtMaterial.Text;
            objQt.ControlOfRecords = txtConvolutionOfRecords.Text;
            objQt.Customername = txtCustomername.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.IfSpecifyItemName = txtItemName.Text;
            objQt.CalibrationBlock = txtCalibrationBlock.Text;
            objQt.Thickness = txtThickness.Text;
            objQt.ProcedureAndRevNo = txtProceduerAndRevNo.Text;
            objQt.ReferenceBlock = txtReferanceBlock.Text;
            objQt.SurfaceCondition = txtSurfaceCondition.Text;
            objQt.CalibrationRangeNormal = txtCalibrationNormal.Text;
            objQt.CalibrationRangeAngle = txtCalibrationAngle.Text;
            objQt.Equipment = txtEquipment.Text;
            objQt.SearchUnitNormal = txtSearchUnitNormal.Text;
            objQt.SearchUnitAngle = txtSearchUnitAngle.Text;
            objQt.Model = txtModel.Text;
            objQt.SizeAndIdentification = txtSizeAndIdentification.Text;
            objQt.SerielNo = txtSerielNo.Text;
            objQt.Frequency = txtFrequency.Text;
            objQt.GainReference = txtGainReference.Text;
            objQt.GainScanning = txtGainScanning.Text;
            objQt.ExtentOfTest = txtExtentOfTest.Text;
            objQt.Coublent = txtCouplant.Text;
            objQt.SearchUnitCableLength = txtSearchUnitCablelength.Text;
            objQt.RejectionLevel = txtRejectionLevel.Text;
            objQt.AcceptanceStandard = txtAcceptanceStandard.Text;
            objQt.ScanningSketch = txtScanningSketch.Text;
            objQt.PerformedName = txtPreparedName.Text;
            objQt.PerformedLevel = txtPrefaredLevel.Text;
            objQt.EvaluvatedName = txtEvaluvatedname.Text;
            objQt.EvaluvatedDesignation = txtEvaluvatedDesignation.Text;
            objQt.InspectionAuthorityName = txtAIName.Text;
            objQt.InspectionAuthorityDesignation = txtAIDesignation.Text;
            objQt.CreatedBy = objSession.employeeid;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("USERDID");
            dt.Columns.Add("USERID");
            dt.Columns.Add("SizeLength");
            dt.Columns.Add("TypeOfDiscontinuity");
            dt.Columns.Add("Evaluvation");

            foreach (GridViewRow row in gvUserDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");

                TextBox txtsizelength = (TextBox)row.FindControl("txtsizelength");
                TextBox txtTypeOfDiscontinuty = (TextBox)row.FindControl("txtTypeOfDiscontinuty");
                TextBox txtEvaluvation = (TextBox)row.FindControl("txtEvaluvation");
                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["USERDID"] = gvUserDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dr["USERID"] = gvUserDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["SizeLength"] = txtsizelength.Text;
                    dr["TypeOfDiscontinuity"] = txtTypeOfDiscontinuty.Text;
                    dr["Evaluvation"] = txtEvaluvation.Text;
                    dt.Rows.Add(dr);
                }
            }

            objQt.dt = dt;
            ds = objQt.SaveUltraSonicReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Ultra Sonic Report Saved successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Ultra Sonic Report Updated successfully');", true);
            ddlRFPNo_SelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Error Occcured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvUTReportHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objQt.USERID = gvUTReportHeader.DataKeys[index].Values[0].ToString();
            ds = objQt.GetUltraSonicReportDetailsByUSERID();

            if (e.CommandName == "EditUTRH")
            {
                try
                {
                    ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                    ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                    hdnUserID.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                    txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                    txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                    txtTestDate.Text = ds.Tables[0].Rows[0]["TestDateEdit"].ToString();
                    txtMaterial.Text = ds.Tables[0].Rows[0]["Material"].ToString();
                    txtConvolutionOfRecords.Text = ds.Tables[0].Rows[0]["ControlOfRecords"].ToString();
                    txtCustomername.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                    txtJobDescription.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                    txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    txtCalibrationBlock.Text = ds.Tables[0].Rows[0]["CalibrationBlock"].ToString();
                    txtThickness.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();
                    txtProceduerAndRevNo.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                    txtReferanceBlock.Text = ds.Tables[0].Rows[0]["ReferenceBlock"].ToString();
                    txtSurfaceCondition.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();
                    txtCalibrationNormal.Text = ds.Tables[0].Rows[0]["CalibrationRangeNormal"].ToString();
                    txtCalibrationAngle.Text = ds.Tables[0].Rows[0]["CalibrationRangeAngle"].ToString();
                    txtEquipment.Text = ds.Tables[0].Rows[0]["Equipment"].ToString();
                    txtSearchUnitNormal.Text = ds.Tables[0].Rows[0]["SearchUnitNormal"].ToString();
                    txtSearchUnitAngle.Text = ds.Tables[0].Rows[0]["SearchUnitAngle"].ToString();
                    txtModel.Text = ds.Tables[0].Rows[0]["Model"].ToString();
                    txtSizeAndIdentification.Text = ds.Tables[0].Rows[0]["SizeAndIdentification"].ToString();
                    txtSerielNo.Text = ds.Tables[0].Rows[0]["SerielNo"].ToString();
                    txtFrequency.Text = ds.Tables[0].Rows[0]["Frequency"].ToString();
                    txtGainReference.Text = ds.Tables[0].Rows[0]["GainReference"].ToString();
                    txtGainScanning.Text = ds.Tables[0].Rows[0]["GainScanning"].ToString();
                    txtExtentOfTest.Text = ds.Tables[0].Rows[0]["ExtentOfTest"].ToString();
                    txtCouplant.Text = ds.Tables[0].Rows[0]["Coublent"].ToString();
                    txtSearchUnitCablelength.Text = ds.Tables[0].Rows[0]["SearchUnitCableLength"].ToString();
                    txtRejectionLevel.Text = ds.Tables[0].Rows[0]["RejectionLevel"].ToString();
                    txtAcceptanceStandard.Text = ds.Tables[0].Rows[0]["AcceptanceStandard"].ToString();
                    txtScanningSketch.Text = ds.Tables[0].Rows[0]["ScanningSketch"].ToString();
                    txtPreparedName.Text = ds.Tables[0].Rows[0]["PerformedName"].ToString();
                    txtPrefaredLevel.Text = ds.Tables[0].Rows[0]["PerformedLevel"].ToString();
                    txtEvaluvatedname.Text = ds.Tables[0].Rows[0]["EvaluvatedName"].ToString();
                    txtEvaluvatedDesignation.Text = ds.Tables[0].Rows[0]["EvaluvatedDesignation"].ToString();
                    txtAIName.Text = ds.Tables[0].Rows[0]["InspectionAuthorityName"].ToString();
                    txtAIDesignation.Text = ds.Tables[0].Rows[0]["InspectionAuthorityDesignation"].ToString();

                    gvUserDetails.DataSource = ds.Tables[1];
                    gvUserDetails.DataBind();
                }
                catch (Exception ex)
                {

                }

            }
            else if (e.CommandName == "PDFUTRH")
            {
                try
                {
                    ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                    hdnUserID.Value = ds.Tables[0].Rows[0]["USERID"].ToString();
                    txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                    txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                    txtTestDate.Text = ds.Tables[0].Rows[0]["TestDateView"].ToString();
                    txtMaterial.Text = ds.Tables[0].Rows[0]["Material"].ToString();
                    txtConvolutionOfRecords.Text = ds.Tables[0].Rows[0]["ControlOfRecords"].ToString();
                    txtCustomername.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                    txtJobDescription.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                    txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    txtCalibrationBlock.Text = ds.Tables[0].Rows[0]["CalibrationBlock"].ToString();
                    txtThickness.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();
                    txtProceduerAndRevNo.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                    txtReferanceBlock.Text = ds.Tables[0].Rows[0]["ReferenceBlock"].ToString();
                    txtSurfaceCondition.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();
                    txtCalibrationNormal.Text = ds.Tables[0].Rows[0]["CalibrationRangeNormal"].ToString();
                    txtCalibrationAngle.Text = ds.Tables[0].Rows[0]["CalibrationRangeAngle"].ToString();
                    txtEquipment.Text = ds.Tables[0].Rows[0]["Equipment"].ToString();
                    txtSearchUnitNormal.Text = ds.Tables[0].Rows[0]["SearchUnitNormal"].ToString();
                    txtSearchUnitAngle.Text = ds.Tables[0].Rows[0]["SearchUnitAngle"].ToString();
                    txtModel.Text = ds.Tables[0].Rows[0]["Model"].ToString();
                    txtSizeAndIdentification.Text = ds.Tables[0].Rows[0]["SizeAndIdentification"].ToString();
                    txtSerielNo.Text = ds.Tables[0].Rows[0]["SerielNo"].ToString();
                    txtFrequency.Text = ds.Tables[0].Rows[0]["Frequency"].ToString();
                    txtGainReference.Text = ds.Tables[0].Rows[0]["GainReference"].ToString();
                    txtGainScanning.Text = ds.Tables[0].Rows[0]["GainScanning"].ToString();
                    txtExtentOfTest.Text = ds.Tables[0].Rows[0]["ExtentOfTest"].ToString();
                    txtCouplant.Text = ds.Tables[0].Rows[0]["Coublent"].ToString();
                    txtSearchUnitCablelength.Text = ds.Tables[0].Rows[0]["SearchUnitCableLength"].ToString();
                    txtRejectionLevel.Text = ds.Tables[0].Rows[0]["RejectionLevel"].ToString();
                    txtAcceptanceStandard.Text = ds.Tables[0].Rows[0]["AcceptanceStandard"].ToString();
                    txtScanningSketch.Text = ds.Tables[0].Rows[0]["ScanningSketch"].ToString();
                    txtPreparedName.Text = ds.Tables[0].Rows[0]["PerformedName"].ToString();
                    txtPrefaredLevel.Text = ds.Tables[0].Rows[0]["PerformedLevel"].ToString();
                    txtEvaluvatedname.Text = ds.Tables[0].Rows[0]["EvaluvatedName"].ToString();
                    txtEvaluvatedDesignation.Text = ds.Tables[0].Rows[0]["EvaluvatedDesignation"].ToString();
                    txtAIName.Text = ds.Tables[0].Rows[0]["InspectionAuthorityName"].ToString();
                    txtAIDesignation.Text = ds.Tables[0].Rows[0]["InspectionAuthorityDesignation"].ToString();
                }
                catch (Exception Ex)
                {

                }
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }



    #endregion

    #region"Common Methods"

    private void bindUltraSonicReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetUltraSonicReportHeader");
            gvUTReportHeader.DataSource = ds.Tables[0];
            gvUTReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindReportNo()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetUltraSonicReportDetails");
            gvUserDetails.DataSource = ds.Tables[0];
            gvUserDetails.DataBind();

            lblReportNo.Text = ds.Tables[1].Rows[0]["ReportNo"].ToString();
            txtConvolutionOfRecords.Text = ds.Tables[1].Rows[0]["ControlID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divInput.Visible = divOutPut.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "view":
                        divOutPut.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}