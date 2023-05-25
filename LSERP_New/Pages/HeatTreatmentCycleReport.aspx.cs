using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_HeatTreatmentCycleReport : System.Web.UI.Page
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


            if (target == "deleteLPIRID")
            {
                objQt = new cQuality();

                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteHeatTreatmentCycleReportByHTCRID", arg.ToString());

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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRFPHIDINHeatTreatmentCycleReport");
                // lblTotalItemQty.Text = hdnTotalItemQty.Value = "Total Item Qty" + ds.Tables[0].Rows[0]["QTY"].ToString();
                bindHeatTratementCycleReportDetails();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }
            hdnHTCRID.Value = "0";
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
            //ds = objQt.GetItemSpecificationDetailsByRFPDIDAndEDID(ddlPenetrantBrand, ddlDeveloperBrand, ddlCleanerBrand);

            //txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //txtProjectName.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBellowSNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            //txtItemName.Text = ddlItemName.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveHTCyle_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.HTCRID = hdnHTCRID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RFPNo = txtRFPNo.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.ControlOfRecords = txtConvolutionOfRecords.Text;
            objQt.ItemQty = txtItemQty.Text;
            objQt.EQUIPNo = txtEQUIPNo.Text;
            objQt.IfSpecifyItemName = txtItemName.Text;
            objQt.MethodOfHeatTreatment = txtMethodOfHeatTreatment.Text;
            objQt.JobSizeAndMaterial = txtJobSizeAndMaterial.Text;
            objQt.TypeOfHeatTreatment = txtTypeOfHeatTreatment.Text;
            objQt.LoadingTemprature = txtLoadingTemprature.Text;
            objQt.RateOfHeading = txtRateOfHeating.Text;
            objQt.SoakingTemprature = txtSoakingTemprature.Text;
            objQt.SoakingTimeDuration = txtSoakingTimeDuration.Text;
            objQt.RateOfCooling = txtRateOfCooling.Text;
            objQt.UnLoadinfTemprature = txtUnloadingTemprature.Text;
            objQt.MethodOfThermoCoubleAttachement = txtThermocoubleAttachement.Text;
            objQt.MethodOfThermoCoubleRemoval = txtThermocoubleRemoval.Text;
            objQt.MethodOfSupport = txtMethodofsupport.Text;
            objQt.Identification = txtIdentification.Text;
            objQt.ThermocoubleLOcationSketch = txtThermocoublelocationSketch.Text;
            objQt.Note = txtNote.Text;
            objQt.PrefaredEnginearname = txtWeldingEngineername.Text;
            objQt.QualityInchargename = txtQualityInchargeName.Text;
            objQt.CreatedBy = objSession.employeeid;

            ds = objQt.SaveHeattreatmentCycleReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','DT Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','DT Report Updated successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Error Occcured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvHeatTreatmentCycleReport_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string HTCRID = gvHeatTreatmentCycleReport.DataKeys[index].Values[0].ToString();
            objQt.HTCRID = HTCRID;
            ds = objQt.GetHeatTreatmentCycleReportDetailsByHTCRID();

            if (e.CommandName == "EditHTC")
            {
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();

                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                hdnHTCRID.Value = ds.Tables[0].Rows[0]["HTCRID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtConvolutionOfRecords.Text = ds.Tables[0].Rows[0]["ControlOfRecords"].ToString();
                txtItemQty.Text = ds.Tables[0].Rows[0]["ItemQty"].ToString();
                txtEQUIPNo.Text = ds.Tables[0].Rows[0]["EQUIPNo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtMethodOfHeatTreatment.Text = ds.Tables[0].Rows[0]["MethodOfHeatTreatment"].ToString();
                txtJobSizeAndMaterial.Text = ds.Tables[0].Rows[0]["JobSizeAndMaterial"].ToString();
                txtTypeOfHeatTreatment.Text = ds.Tables[0].Rows[0]["TypeOfHeatTreatment"].ToString();
                txtLoadingTemprature.Text = ds.Tables[0].Rows[0]["LoadingTemprature"].ToString();
                txtRateOfHeating.Text = ds.Tables[0].Rows[0]["RateOfHeading"].ToString();
                txtSoakingTemprature.Text = ds.Tables[0].Rows[0]["SoakingTemprature"].ToString();
                txtSoakingTimeDuration.Text = ds.Tables[0].Rows[0]["SoakingTimeDuration"].ToString();
                txtRateOfCooling.Text = ds.Tables[0].Rows[0]["RateOfCooling"].ToString();
                txtUnloadingTemprature.Text = ds.Tables[0].Rows[0]["UnLoadinfTemprature"].ToString();
                txtThermocoubleAttachement.Text = ds.Tables[0].Rows[0]["MethodOfThermoCoubleAttachement"].ToString();
                txtThermocoubleRemoval.Text = ds.Tables[0].Rows[0]["MethodOfThermoCoubleRemoval"].ToString();
                txtMethodofsupport.Text = ds.Tables[0].Rows[0]["MethodOfSupport"].ToString();
                txtIdentification.Text = ds.Tables[0].Rows[0]["Identification"].ToString();
                txtThermocoublelocationSketch.Text = ds.Tables[0].Rows[0]["ThermocoubleLOcationSketch"].ToString();
                txtNote.Text = ds.Tables[0].Rows[0]["Note"].ToString();
                txtWeldingEngineername.Text = ds.Tables[0].Rows[0]["PrefaredEnginearname"].ToString();
                txtQualityInchargeName.Text = ds.Tables[0].Rows[0]["QualityInchargename"].ToString();
            }
            if (e.CommandName == "PdfHTC")
            {
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblDRGNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblQty_p.Text = ds.Tables[0].Rows[0]["ItemQty"].ToString();
                lblEquipNo_p.Text = ds.Tables[0].Rows[0]["EQUIPNo"].ToString();
                lblItemname_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblMethodOfHeatTreatment_p.Text = ds.Tables[0].Rows[0]["MethodOfHeatTreatment"].ToString();
                lblJobsizeMaterial_p.Text = ds.Tables[0].Rows[0]["JobSizeAndMaterial"].ToString();
                lblTypeOfHeatTreatment_p.Text = ds.Tables[0].Rows[0]["TypeOfHeatTreatment"].ToString();
                lblLoadingTemprature_p.Text = ds.Tables[0].Rows[0]["LoadingTemprature"].ToString();
                lblRateOfHeading_p.Text = ds.Tables[0].Rows[0]["RateOfHeading"].ToString();
                lblSoakingTemprature_p.Text = ds.Tables[0].Rows[0]["SoakingTemprature"].ToString();
                lblSoakingTime_p.Text = ds.Tables[0].Rows[0]["SoakingTimeDuration"].ToString();
                lblRateOfCooling_p.Text = ds.Tables[0].Rows[0]["RateOfCooling"].ToString();
                lblUnloadingTemprature_p.Text = ds.Tables[0].Rows[0]["UnLoadinfTemprature"].ToString();
                lblMethodOfTherMoCoubleAttachement_p.Text = ds.Tables[0].Rows[0]["MethodOfThermoCoubleAttachement"].ToString();
                lblMethodOfThermoCoubleRemoval_p.Text = ds.Tables[0].Rows[0]["MethodOfThermoCoubleRemoval"].ToString();
                lblMethodOfSupport_p.Text = ds.Tables[0].Rows[0]["MethodOfSupport"].ToString();
                lblIdentification_p.Text = ds.Tables[0].Rows[0]["Identification"].ToString();
                //   txtThermocoublelocationSketch.Text = ds.Tables[0].Rows[0]["ThermocoubleLOcationSketch"].ToString();
                lblNote_p.Text = ds.Tables[0].Rows[0]["Note"].ToString();
                //txtWeldingEngineername.Text = ds.Tables[0].Rows[0]["PrefaredEnginearname"].ToString();
                //txtQualityInchargeName.Text = ds.Tables[0].Rows[0]["QualityInchargename"].ToString();

                hdnAddress.Value = ds.Tables[1].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[1].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[1].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[1].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintHeatTreatmentCycleReport();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindReportNo()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetHeatTreatmentReportNo");

            lblReportNo.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            txtConvolutionOfRecords.Text = ds.Tables[0].Rows[0]["ControlID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindHeatTratementCycleReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetHeatTreatmentCycleReportDetails");
            gvHeatTreatmentCycleReport.DataSource = ds.Tables[0];
            gvHeatTreatmentCycleReport.DataBind();
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