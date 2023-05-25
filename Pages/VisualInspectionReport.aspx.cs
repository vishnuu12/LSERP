using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_VisualInspectionReport : System.Web.UI.Page
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
                bindLPIReportDetails();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                objQt.VERID = arg.ToString();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteVisualreportdetailsByVERID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    bindVEReportHeader();
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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsBYRFPHIDInVEReportDeatils");

                //  lblTotalItemQty.Text = hdnTotalItemQty.Value = "Total Item Qty" + ds.Tables[0].Rows[0]["QTY"].ToString();

                bindVEReportHeader();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }

            hdnVERID.Value = "0";
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
            //   ds = objQt.GetItemSpecificationDetailsByRFPDIDAndEDID();

            txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //txtProjectName.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBellowSNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;

            //objQt.StageOfTest = txtStageOfTest.Text = "Circumferential fillet welding of Duct to bellows andsupport";

            //objQt.MaterialSpecification = txtMaterialSpecification.Text = "A 240 TYP 321,IS 2062 Gr.B";
            //objQt.OtherReference = txtOtherReference.Text = "CHEM JOB NO:PEF-379";

            //objQt.PenetrantBatchNo = txtBatchNo.Text = "sadska";
            //objQt.Thickness = txtThickness.Text = "2 MM";

            //objQt.CleanRemoverBatchNo = txtCleanerBatch.Text = "17E01";
            //objQt.ProcedureAndRevNo = txtProceduerAndRevNo.Text = "sadskad";
            //objQt.DwellTime = txtDwellTime.Text = "10 mins";
            //objQt.SurfaceCondition = txtSurfaceCondition.Text = "oj";

            //objQt.DeveloperBatchNo = txtDeveloperBatchNo.Text = "ok";
            //objQt.SurfaceTemprature = txtSurfaceTemprature.Text = "10 deg";
            //objQt.DevelopementTime = txtDevelopementTime.Text = "110 mins";
            //objQt.PenetrateSystem = txtPenetrateSystem.Text = "lpi";
            //objQt.LightningEquipment = txtLightningEquipment.Text = "HandLamp";
            //objQt.Technique = txtTechnique.Text = "Solvent Removable";
            //objQt.LightIntensity = txtLightIntensity.Text = "140 LUX";
            //objQt.SheetOfIndications = txtSketchOfIndications.Text = "Ok";
            //objQt.InspectionQty = txtInspectionQty.Text = "1";
            //objQt.AcceptedQty = txtAcceptedQty.Text = "1";
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
                dt.Columns.Add("VERDID");
                dt.Columns.Add("VERID");
                dt.Columns.Add("JointIndentification");
                dt.Columns.Add("InterPretation");
                dt.Columns.Add("Result");

                if (gvVERDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvVERDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvVERDetails.Rows)
                        {
                            string MTRDID = gvVERDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string MTRID = gvVERDetails.DataKeys[row.RowIndex].Values[1].ToString();
                            TextBox txtJointIdentification = (TextBox)gvVERDetails.Rows[row.RowIndex].FindControl("txtJointIdentification");
                            TextBox txtInterPretation = (TextBox)gvVERDetails.Rows[row.RowIndex].FindControl("txtInterPretation");
                            TextBox txtResult = (TextBox)gvVERDetails.Rows[row.RowIndex].FindControl("txtResult");

                            CheckBox chkpart = (CheckBox)gvVERDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvVERDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr["VERDID"] = 0;
                                    dr["VERID"] = 0;
                                    dr["JointIndentification"] = txtJointIdentification.Text;
                                    dr["InterPretation"] = txtInterPretation.Text;
                                    dr["Result"] = txtResult.Text;
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();
                                    dr["VERDID"] = 0;
                                    dr["VERID"] = 0;
                                    dr["JointIndentification"] = txtJointIdentification.Text;
                                    dr["InterPretation"] = txtInterPretation.Text;
                                    dr["Result"] = txtResult.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvVERDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();
                            dr["VERDID"] = 0;
                            dr["VERID"] = 0;
                            dr["JointIndentification"] = "";
                            dr["InterPretation"] = "";
                            dr["Result"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                    gvVERDetails.DataSource = dt;
                    gvVERDetails.DataBind();
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

    protected void btnSaveVER_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.VERID = hdnVERID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Customername = txtCustomerName.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.ProcedureAndRevNo = txtProcedure.Text;
            objQt.EquipmentUsed = txtEquipmentUsed.Text;
            objQt.LightIntensity = txtLightingEquipment.Text;
            objQt.Technique = txtTechniqueUsed.Text;
            objQt.BSLNo = txtBellowSNo.Text;
            objQt.CreatedBy = objSession.employeeid;
            objQt.IfSpecifyItemName = txtItemName.Text;
            objQt.ITPNo = txtITPNo.Text;
            objQt.SurfaceCondition = txtSurfaceCondition.Text;
            objQt.AcceptedQty = txtQuantity.Text;
            objQt.ProjectName = txtProjectName.Text;
            objQt.PONo = txtCustomerPONo.Text;
            objQt.RFPNo = txtRFPNo.Text;
            objQt.Remarks = txtRemarks.Text;
            objQt.other = txtOther.Text;
            objQt.UserID = objSession.employeeid;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("VERDID");
            dt.Columns.Add("VERID");
            dt.Columns.Add("JobIdentification");
            dt.Columns.Add("InterPretation");
            dt.Columns.Add("Result");

            foreach (GridViewRow row in gvVERDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");

                TextBox txtJointIdentification = (TextBox)row.FindControl("txtJointIdentification");
                TextBox txtInterPretation = (TextBox)row.FindControl("txtInterPretation");
                TextBox txtResult = (TextBox)row.FindControl("txtResult");
                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["VERDID"] = gvVERDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dr["VERID"] = gvVERDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["JobIdentification"] = txtJointIdentification.Text;
                    dr["InterPretation"] = txtInterPretation.Text;
                    dr["Result"] = txtResult.Text;
                    dt.Rows.Add(dr);
                }
            }
            objQt.dt = dt;
            ds = objQt.SaveVisualExaminationReport();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','VER Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','VER Report Updated successfully');", true);
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

    protected void gvVEReportHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objQt.VERID = gvVEReportHeader.DataKeys[index].Values[0].ToString();
            ds = objQt.GetVisualInspectionReportByVERID();

            if (e.CommandName == "EditVER")
            {
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                hdnVERID.Value = ds.Tables[0].Rows[0]["VERID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                txtCustomerName.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtProcedure.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                txtEquipmentUsed.Text = ds.Tables[0].Rows[0]["EquipmentUsed"].ToString();
                txtLightingEquipment.Text = ds.Tables[0].Rows[0]["LightIntensity"].ToString();
                txtTechniqueUsed.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                txtBellowSNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtITPNo.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                txtSurfaceCondition.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();
                txtQuantity.Text = ds.Tables[0].Rows[0]["QtmOfInspection"].ToString();
                txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                txtCustomerPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();

                gvVERDetails.DataSource = ds.Tables[1];
                gvVERDetails.DataBind();

            }
            else if (e.CommandName == "VERPDF")
            {
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblcustomername_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblDrawingNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblProcedure_p.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                lblEquipmentused_p.Text = ds.Tables[0].Rows[0]["EquipmentUsed"].ToString();
                lblLightingequipmentAndIntensity_p.Text = ds.Tables[0].Rows[0]["LightIntensity"].ToString();
                lblTechniqueUsed_p.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                lblBellowSno_p.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblITPNo_p.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                lblsurfacecondition_p.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();
                lblQuantity_p.Text = ds.Tables[0].Rows[0]["QtmOfInspection"].ToString();
                lblprojectname_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                lblPONO_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();


                gvVERDetails_p.DataSource = ds.Tables[1];
                gvVERDetails_p.DataBind();

                hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintVERReport();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"Common Methods"

    private void bindVEReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetVEReportHeaderDetailsByRFPHID");
            gvVEReportHeader.DataSource = ds.Tables[0];
            gvVEReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindLPIReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetVERDetails();
            gvVERDetails.DataSource = ds.Tables[0];
            gvVERDetails.DataBind();

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