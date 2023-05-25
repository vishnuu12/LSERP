using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_RadioGraphicExaminationReport : System.Web.UI.Page
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
                //bindRGSReportDetails();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteRadioGraphicExaminationReportHeader", arg.ToString());

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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRadioGraphicExaminationReportHeader");

                bindRadioGraphicExaminationReport();
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
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //     txtBSLNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;
            txtCustomername.Text = ddlCustomerName.SelectedItem.Text;

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                dt.Columns.Add("RGEDID");
                dt.Columns.Add("RGERID");
                dt.Columns.Add("JointIndentification");
                dt.Columns.Add("Segment");
                dt.Columns.Add("SizeOfFilm");
                dt.Columns.Add("DensityIQI");
                dt.Columns.Add("DensityAI");
                dt.Columns.Add("TypeOfDiscontinuity");
                dt.Columns.Add("Disposition");

                if (gvRadioGraphicExaminationReportDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvRadioGraphicExaminationReportDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvRadioGraphicExaminationReportDetails.Rows)
                        {
                            string RGEDID = gvRadioGraphicExaminationReportDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string RGERID = gvRadioGraphicExaminationReportDetails.DataKeys[row.RowIndex].Values[1].ToString();
                            TextBox txtJointIdentification = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtJointIdentification");
                            TextBox txtSegment = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtSegment");
                            TextBox txtSizeofFilm = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtSizeofFilm");
                            TextBox txtIQIDensity = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtIQIDensity");
                            TextBox txtDensityAI = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtDensityAI");
                            TextBox txtTypeOfDiscontinuity = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtTypeOfDiscontinuity");
                            TextBox txtDisposition = (TextBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("txtDisposition");

                            CheckBox chkpart = (CheckBox)gvRadioGraphicExaminationReportDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvRadioGraphicExaminationReportDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr["RGEDID"] = 0;
                                    dr["RGERID"] = 0;
                                    dr["JointIndentification"] = txtJointIdentification.Text;
                                    dr["Segment"] = txtSegment.Text;
                                    dr["SizeOfFilm"] = txtSizeofFilm.Text;
                                    dr["DensityIQI"] = txtIQIDensity.Text;
                                    dr["DensityAI"] = txtDensityAI.Text;
                                    dr["TypeOfDiscontinuity"] = txtTypeOfDiscontinuity.Text;
                                    dr["Disposition"] = txtDisposition.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();
                                    dr["RGEDID"] = 0;
                                    dr["RGERID"] = 0;
                                    dr["JointIndentification"] = txtJointIdentification.Text;
                                    dr["Segment"] = txtSegment.Text;
                                    dr["SizeOfFilm"] = txtSizeofFilm.Text;
                                    dr["DensityIQI"] = txtIQIDensity.Text;
                                    dr["DensityAI"] = txtDensityAI.Text;
                                    dr["TypeOfDiscontinuity"] = txtTypeOfDiscontinuity.Text;
                                    dr["Disposition"] = txtDisposition.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvRadioGraphicExaminationReportDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();
                            dr["RGEDID"] = 0;
                            dr["RGERID"] = 0;
                            dr["JointIndentification"] = "";
                            dr["Segment"] = "";
                            dr["SizeOfFilm"] = "";
                            dr["DensityIQI"] = "";
                            dr["DensityAI"] = "";
                            dr["TypeOfDiscontinuity"] = "";
                            dr["Disposition"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                    gvRadioGraphicExaminationReportDetails.DataSource = dt;
                    gvRadioGraphicExaminationReportDetails.DataBind();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Given rows Rows Cant same of Grid view rows');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Please select the Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveRGE_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RGERID = hsnRGERID.Value;
            objQt.PONo = txtPONo.Text;
            objQt.RFPNo = txtRFPNo.Text;
            objQt.ControlOfRecords = txtConvolutionOfRecords.Text;
            objQt.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Customername = txtCustomername.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.ItemName = txtItemName.Text;
            objQt.ItemSerielNo = txtJobSerielNo.Text;
            objQt.NoOfExposures = txtNoOfExposures.Text;
            objQt.IQI = rblIQI.SelectedValue;
            objQt.TechniqueSheetReferenceNo = txtTechniqueSheetAndReferenceNo.Text;
            objQt.ExposureTechnique = rblExposuretechnique.SelectedValue;
            objQt.StageOfInspection = txtStageOfInspection.Text;
            objQt.Viewingtechnique = rblViewingTechinque.Text;
            objQt.ProcedureAndRevNo = txtProcedureNoAndRevNo.Text;
            objQt.WeldReinforcementthickness = txtWeldReinforcementThickness.Text;
            objQt.LeadScreensFront = txtLeadScreensFront.Text;
            objQt.LeadScreensBack = txtLeadScreensback.Text;
            objQt.NoofFilmsFilmCassette = txtNoOfFilmsInFilmCastee.Text;
            objQt.SODFOD = txtSODFOD.Text;
            objQt.FilmManufacture = txtFilmManufacture.Text;
            objQt.SSOFD = txtSSOFD.Text;
            objQt.FilmTypeDesignation = txtFilmTypeDesignation.Text;
            objQt.XRayKV = txtXrayKV.Text;
            objQt.XRayMa = txtXrayMa.Text;
            objQt.Sensitivity = txtSensitivity.Text;
            objQt.FocalSpot = txtFocalSpot.Text;
            objQt.GameRaySource = txtGamaRaySource.Text;
            objQt.GameRayStrength = txtGamaRayStrength.Text;
            objQt.GameRaySize = txtGamaRaySize.Text;

            objQt.DevelopementTemprature = txtDevelopingTemprature.Text;
            objQt.DevelopementTime = txtDevelopingtime.Text;

            objQt.SizeOfBLetter = txtSizeOfBLetter.Text;
            objQt.PlacingOfBLetter = txtPlacingOfBLetter.Text;
            objQt.EvaluvationOfBLettter = txtEvaluvationOfBleeter.Text;
            objQt.RTPerformer = txtRTPerformer.Text;
            objQt.LSEPreparedBy = txtLSEPreparedName.Text;
            objQt.LSEPreparedDate = DateTime.ParseExact(txtLSEPreparedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.LSEPreapredLevel = txtLSEPreparedLevel.Text;
            objQt.LSEReviewedBy = txtLSEReviwedName.Text;
            objQt.LSEReviewdDate = DateTime.ParseExact(txtLSEReviewdDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.LSEReviewedDesignation = txtLSEReviewdDesignation.Text;
            objQt.CustomerReviwedBy = txtCustomerReviewerName.Text;
            objQt.CustomerReviwedDate = DateTime.ParseExact(txtCustomerReviwedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.CustomerDesignation = txtCustomerReviwereDesignation.Text;
            objQt.AIReveiwedBy = txtAIName.Text;
            objQt.AIReviewedDate = DateTime.ParseExact(AIDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.AIDesignation = txtAIDesignation.Text;
            objQt.Other = txtOther.Text;

            objQt.BaseMaterial = txtBasematerial.Text;
            objQt.Film = txtFilm.Text;
            objQt.Technique = txtTechnique.Text;
            objQt.BaseMaterialSizeAndThickness = txtBaseMaterialSizeAndThickness.Text;
            objQt.PlacementOfBackScatter = txtPlacementOfBackScatter.Text;
            objQt.weldthickness = txtWeldThickness.Text;
            objQt.Density = txtDensity.Text;

            objQt.CreatedBy = objSession.employeeid;


            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("RGEDID");
            dt.Columns.Add("RGERID");
            dt.Columns.Add("JointIndentification");
            dt.Columns.Add("Segment");
            dt.Columns.Add("SizeOfFilm");
            dt.Columns.Add("DensityIQI");
            dt.Columns.Add("DensityAI");
            dt.Columns.Add("TypeOfDiscontinuity");
            dt.Columns.Add("Disposition");

            foreach (GridViewRow row in gvRadioGraphicExaminationReportDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");

                TextBox txtJointIdentification = (TextBox)row.FindControl("txtJointIdentification");
                TextBox txtSegment = (TextBox)row.FindControl("txtSegment");
                TextBox txtSizeofFilm = (TextBox)row.FindControl("txtSizeofFilm");
                TextBox txtIQIDensity = (TextBox)row.FindControl("txtIQIDensity");
                TextBox txtDensityAI = (TextBox)row.FindControl("txtDensityAI");
                TextBox txtTypeOfDiscontinuity = (TextBox)row.FindControl("txtTypeOfDiscontinuity");
                TextBox txtDisposition = (TextBox)row.FindControl("txtDisposition");

                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["RGEDID"] = gvRadioGraphicExaminationReportDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["RGERID"] = gvRadioGraphicExaminationReportDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["JointIndentification"] = txtJointIdentification.Text;
                    dr["Segment"] = txtSegment.Text;
                    dr["SizeOfFilm"] = txtSizeofFilm.Text;
                    dr["DensityIQI"] = txtIQIDensity.Text;
                    dr["DensityAI"] = txtDensityAI.Text;
                    dr["TypeOfDiscontinuity"] = txtTypeOfDiscontinuity.Text;
                    dr["Disposition"] = txtDisposition.Text;
                    dt.Rows.Add(dr);
                }
            }
            objQt.dt = dt;
            ds = objQt.SaveRadioGraphicExaminationReport();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RGE Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RGE Report Updated successfully');", true);
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

    protected void gvRadioGraphicHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        cQuality objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objQt.RGERID = gvRadioGraphicHeader.DataKeys[index].Values[0].ToString();
            ds = objQt.GetRadioGraphicExaminationDetailsByRGERID();

            if (e.CommandName.ToString() == "EditRGER")
            {
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();

                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                hsnRGERID.Value = ds.Tables[0].Rows[0]["RGERID"].ToString();
                txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtConvolutionOfRecords.Text = ds.Tables[0].Rows[0]["ControlOfRecords"].ToString();
                txtTestDate.Text = ds.Tables[0].Rows[0]["TestDateEdit"].ToString();
                txtCustomername.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtJobDescription.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtJobSerielNo.Text = ds.Tables[0].Rows[0]["ItemSerielNo"].ToString();
                txtNoOfExposures.Text = ds.Tables[0].Rows[0]["NoOfExposures"].ToString();
                rblIQI.SelectedValue = ds.Tables[0].Rows[0]["IQI"].ToString();
                txtTechniqueSheetAndReferenceNo.Text = ds.Tables[0].Rows[0]["TechniqueSheetReferenceNo"].ToString();
                rblExposuretechnique.SelectedValue = ds.Tables[0].Rows[0]["ExposureTechnique"].ToString();
                txtStageOfInspection.Text = ds.Tables[0].Rows[0]["StageOfInspection"].ToString();
                rblViewingTechinque.Text = ds.Tables[0].Rows[0]["Viewingtechnique"].ToString();
                txtProcedureNoAndRevNo.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                txtWeldReinforcementThickness.Text = ds.Tables[0].Rows[0]["WeldReinforcementthickness"].ToString();
                txtLeadScreensFront.Text = ds.Tables[0].Rows[0]["LeadScreensFront"].ToString();
                txtLeadScreensback.Text = ds.Tables[0].Rows[0]["LeadScreensBack"].ToString();
                txtNoOfFilmsInFilmCastee.Text = ds.Tables[0].Rows[0]["NoofFilmsFilmCassette"].ToString();
                txtSODFOD.Text = ds.Tables[0].Rows[0]["SODFOD"].ToString();
                txtFilmManufacture.Text = ds.Tables[0].Rows[0]["FilmManufacture"].ToString();
                txtSSOFD.Text = ds.Tables[0].Rows[0]["SSOFD"].ToString();
                txtFilmTypeDesignation.Text = ds.Tables[0].Rows[0]["FilmTypeDesignation"].ToString();
                txtXrayKV.Text = ds.Tables[0].Rows[0]["XRayKV"].ToString();
                txtXrayMa.Text = ds.Tables[0].Rows[0]["XRayMa"].ToString();
                txtSensitivity.Text = ds.Tables[0].Rows[0]["Sensitivity"].ToString();
                txtFocalSpot.Text = ds.Tables[0].Rows[0]["FocalSpot"].ToString();
                txtGamaRaySource.Text = ds.Tables[0].Rows[0]["GameRaySource"].ToString();
                txtGamaRayStrength.Text = ds.Tables[0].Rows[0]["GameRayStrength"].ToString();
                txtGamaRaySize.Text = ds.Tables[0].Rows[0]["GameRaySize"].ToString();
                // txtDevelopingTempraturetime.Text = ds.Tables[0].Rows[0]["DevelopingTempratureAndTime"].ToString();
                txtSizeOfBLetter.Text = ds.Tables[0].Rows[0]["SizeOfBLetter"].ToString();
                txtPlacingOfBLetter.Text = ds.Tables[0].Rows[0]["PlacingOfBLetter"].ToString();
                txtEvaluvationOfBleeter.Text = ds.Tables[0].Rows[0]["EvaluvationOfBLettter"].ToString();
                txtRTPerformer.Text = ds.Tables[0].Rows[0]["RTPerformer"].ToString();
                txtLSEPreparedName.Text = ds.Tables[0].Rows[0]["LSEPreparedBy"].ToString();
                txtLSEPreparedDate.Text = ds.Tables[0].Rows[0]["LSEPreparedDateEdit"].ToString();
                txtLSEPreparedLevel.Text = ds.Tables[0].Rows[0]["LSEPreapredLevel"].ToString();
                txtLSEReviwedName.Text = ds.Tables[0].Rows[0]["LSEReviewedBy"].ToString();
                txtLSEReviewdDate.Text = ds.Tables[0].Rows[0]["LSEReviewdDateEdit"].ToString();
                txtLSEReviewdDesignation.Text = ds.Tables[0].Rows[0]["LSEReviewedDesignation"].ToString();
                txtCustomerReviewerName.Text = ds.Tables[0].Rows[0]["CustomerReviwedBy"].ToString();
                txtCustomerReviwedDate.Text = ds.Tables[0].Rows[0]["CustomerReviwedDateEdit"].ToString();
                txtCustomerReviwereDesignation.Text = ds.Tables[0].Rows[0]["CustomerDesignation"].ToString();
                txtAIName.Text = ds.Tables[0].Rows[0]["AIReveiwedBy"].ToString();
                AIDate.Text = ds.Tables[0].Rows[0]["AIReviewedDateEdit"].ToString();
                txtAIDesignation.Text = ds.Tables[0].Rows[0]["AIDesignation"].ToString();
                txtOther.Text = ds.Tables[0].Rows[0]["Other"].ToString();

                txtBasematerial.Text = ds.Tables[0].Rows[0]["BaseMaterial"].ToString();
                txtFilm.Text = ds.Tables[0].Rows[0]["Film"].ToString();
                txtTechnique.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                txtBaseMaterialSizeAndThickness.Text = ds.Tables[0].Rows[0]["BaseMaterialSizeAndThickness"].ToString();
                txtPlacementOfBackScatter.Text = ds.Tables[0].Rows[0]["PlacementOfBackScatter"].ToString();
                txtWeldThickness.Text = ds.Tables[0].Rows[0]["weldthickness"].ToString();
                txtDensity.Text = ds.Tables[0].Rows[0]["Density"].ToString();

                txtDevelopingtime.Text = ds.Tables[0].Rows[0]["DevelopementTime"].ToString();
                txtDevelopingTemprature.Text = ds.Tables[0].Rows[0]["DevelopementTemprature"].ToString();

                gvRadioGraphicExaminationReportDetails.DataSource = ds.Tables[1];
                gvRadioGraphicExaminationReportDetails.DataBind();
            }
            if (e.CommandName == "RGEPDF")
            {
                lblReportdate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblDateOftest_p.Text = ds.Tables[0].Rows[0]["TestDateView"].ToString();
                lblCustomer_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblDrawingNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblJobdescription_p.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                lblJobSerialNo_p.Text = ds.Tables[0].Rows[0]["ItemSerielNo"].ToString();
                lblNoOfExposures_p.Text = ds.Tables[0].Rows[0]["NoOfExposures"].ToString();
                lblImageQualityIndicater_p.Text = ds.Tables[0].Rows[0]["IQI"].ToString();
                lblTechniqueReferenceNo_p.Text = ds.Tables[0].Rows[0]["TechniqueSheetReferenceNo"].ToString();
                lblExposureTechnique_p.Text = ds.Tables[0].Rows[0]["ExposureTechnique"].ToString();
                lblStageOfInspection_p.Text = ds.Tables[0].Rows[0]["StageOfInspection"].ToString();
                lblViewingTechnique_p.Text = ds.Tables[0].Rows[0]["Viewingtechnique"].ToString();
                lblProcedureAndRevNo_p.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                lblWeldthicknessreinforcement_p.Text = ds.Tables[0].Rows[0]["WeldReinforcementthickness"].ToString();
                lblLeadScreensFront_p.Text = "<label> Front: </label>" + ds.Tables[0].Rows[0]["LeadScreensFront"].ToString();
                lblLeadscreensBack_p.Text = "<label> Back: </label>" + ds.Tables[0].Rows[0]["LeadScreensBack"].ToString();
                lblNoOfFilmsInCastelee_p.Text = ds.Tables[0].Rows[0]["NoofFilmsFilmCassette"].ToString();
                lblSODFOD_p.Text = ds.Tables[0].Rows[0]["SODFOD"].ToString();
                lblFilmManufacture_p.Text = ds.Tables[0].Rows[0]["FilmManufacture"].ToString();
                lblSSOFD_p.Text = ds.Tables[0].Rows[0]["SSOFD"].ToString();
                lblFilmTypeDesignation_p.Text = ds.Tables[0].Rows[0]["FilmTypeDesignation"].ToString();
                lblKV_p.Text = "<label> KV: </label>" + ds.Tables[0].Rows[0]["XRayKV"].ToString();
                lblMA_p.Text = "<label> MA: </label>" + ds.Tables[0].Rows[0]["XRayMa"].ToString();
                lblSensitivity_p.Text = ds.Tables[0].Rows[0]["Sensitivity"].ToString();
                lblFocalSpot_p.Text = "<label> Focal Spot: </label>" + ds.Tables[0].Rows[0]["FocalSpot"].ToString();
                lblGammaRaySource_p.Text = "<label> Source: </label>" + ds.Tables[0].Rows[0]["GameRaySource"].ToString();
                lblstrength_p.Text = "<label> Strength: </label>" + ds.Tables[0].Rows[0]["GameRayStrength"].ToString();
                lblSourcesize_p.Text = "<label> Size: </label>" + ds.Tables[0].Rows[0]["GameRaySize"].ToString();
                //lblTemprature_p.Text = ds.Tables[0].Rows[0]["DevelopingTempratureAndTime"].ToString();
                lblTemprature_p.Text = "<label> Temprature: </label>" + ds.Tables[0].Rows[0]["DevelopementTemprature"].ToString();
                lblTime_p.Text = "<label> Time: </label>" + ds.Tables[0].Rows[0]["DevelopementTime"].ToString();

                lblSizeOfBLetter_p.Text = ds.Tables[0].Rows[0]["SizeOfBLetter"].ToString();
                lblPlacingOfBletter_p.Text = ds.Tables[0].Rows[0]["PlacingOfBLetter"].ToString();
                lblEvaluvationOfBLetter_p.Text = "<label> Evaluvation Of B Letter: </label>" + ds.Tables[0].Rows[0]["EvaluvationOfBLettter"].ToString();
                lblRTPerformer_p.Text = "<label> RT Performer: </label>" + ds.Tables[0].Rows[0]["RTPerformer"].ToString();
                lblLSIPreparedName_p.Text = "<label> Name: </label>" + ds.Tables[0].Rows[0]["LSEPreparedBy"].ToString();
                lblLSIPreparedDate_p.Text = "<label> Date: </label>" + ds.Tables[0].Rows[0]["LSEPreparedDateView"].ToString();
                lblLSIPreparedlevel_p.Text = "<label> Level: </label>" + ds.Tables[0].Rows[0]["LSEPreapredLevel"].ToString();
                LSIReviewedName_p.Text = "<label> Name: </label>" + ds.Tables[0].Rows[0]["LSEReviewedBy"].ToString();
                lblLSIReviewedDate_p.Text = "<label> Date: </label>" + ds.Tables[0].Rows[0]["LSEReviewdDateView"].ToString();
                lblLSIReviewdDesignation_p.Text = "<label> Designation: </label>" + ds.Tables[0].Rows[0]["LSEReviewedDesignation"].ToString();
                lblCustomerReviewedName_p.Text = "<label> Name: </label>" + ds.Tables[0].Rows[0]["CustomerReviwedBy"].ToString();
                lblCustomerReviewdDate_p.Text = "<label> Date: </label>" + ds.Tables[0].Rows[0]["CustomerReviwedDateView"].ToString();
                lblCustomerDesignation_p.Text = "<label> Designation: </label>" + ds.Tables[0].Rows[0]["CustomerDesignation"].ToString();
                lblAIReviewdname_p.Text = "<label> Name: </label>" + ds.Tables[0].Rows[0]["AIReveiwedBy"].ToString();
                lblAIreviewdDate_p.Text = "<label> Date: </label>" + ds.Tables[0].Rows[0]["AIReviewedDateView"].ToString();
                lblAIDesignation_p.Text = "<label> Designation: </label>" + ds.Tables[0].Rows[0]["AIDesignation"].ToString();
                txtOther.Text = ds.Tables[0].Rows[0]["Other"].ToString();

                // lblfi.Text = ds.Tables[0].Rows[0]["Film"].ToString();
                // txtTechnique.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                lblBaseMaterialAndThickness_p.Text = ds.Tables[0].Rows[0]["BaseMaterialSizeAndThickness"].ToString();
                //  lblb.Text = ds.Tables[0].Rows[0]["PlacementOfBackScatter"].ToString();
                //  lblWeldthicknessreinforcement_p.Text = ds.Tables[0].Rows[0]["weldthickness"].ToString();
                // txtDensity.Text = ds.Tables[0].Rows[0]["Density"].ToString();

                gvRadioGraphicExaminationDetails_p.DataSource = ds.Tables[1];
                gvRadioGraphicExaminationDetails_p.DataBind();

                hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "PrintRadioGraphicExaminationReport();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindRadioGraphicExaminationReport()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LSE_GetradioGraphicExaminationReportHeader");
            gvRadioGraphicHeader.DataSource = ds.Tables[0];
            gvRadioGraphicHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindRGSReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetRadioGraphicExaminationReportDetails");

            gvRadioGraphicExaminationReportDetails.DataSource = ds.Tables[0];
            gvRadioGraphicExaminationReportDetails.DataBind();

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