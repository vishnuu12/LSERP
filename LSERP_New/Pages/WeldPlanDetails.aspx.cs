using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Globalization;

public partial class Pages_WeldPlanDetails : System.Web.UI.Page
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
                ViewState["RFPHID"] = Request.QueryString["RFPHID"].Split('/')[0].ToString();
                ViewState["UserID"] = Request.QueryString["UserID"].ToString();

                bindWeldPlanReportHeader();
                BindItemDetails();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                objQt.VERID = arg.ToString();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteVisualreportdetailsByVERID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    bindWeldPlanReportHeader();
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
                dt.Columns.Add("WPRDID");
                dt.Columns.Add("WPRHID");
                dt.Columns.Add("JointType");
                dt.Columns.Add("BMThicknessPart1");
                dt.Columns.Add("BMThicknessPart2");
                dt.Columns.Add("BMSpecificationPart1");
                dt.Columns.Add("BMSpecificationPNo1");
                dt.Columns.Add("BMSpecificationPart2");
                dt.Columns.Add("BMSpecificationPNo2");
                dt.Columns.Add("PartNoToPartNo");
                dt.Columns.Add("WPSNo");
                dt.Columns.Add("Process");
                dt.Columns.Add("Remarks");

                if (gvWeldPlanDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvWeldPlanDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvWeldPlanDetails.Rows)
                        {
                            string WPRDID = gvWeldPlanDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string WPRHID = gvWeldPlanDetails.DataKeys[row.RowIndex].Values[1].ToString();

                            TextBox txtJointType = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtJointType");
                            TextBox txtBaseMetalThickPart1 = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtBaseMetalThickPart1");
                            TextBox txtbaseMetalthickpart2 = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtbaseMetalthickpart2");
                            TextBox txtBaseMetalSpecificationPart1 = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtBaseMetalSpecificationPart1");
                            TextBox txtBaseMetalSpecificationPNo1 = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtBaseMetalSpecificationPNo1");
                            TextBox txtBaseMetalSpecificationPart2 = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtBaseMetalSpecificationPart2");
                            TextBox txtBaseMetalSpecificationPNo2 = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtBaseMetalSpecificationPNo2");

                            TextBox txtPartNotoPartNo = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtPartNotoPartNo");
                            TextBox txtWPSNo = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtWPSNo");
                            TextBox txtProcess = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtProcess");
                            TextBox txtRemarks = (TextBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("txtRemarks");

                            CheckBox chkpart = (CheckBox)gvWeldPlanDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvWeldPlanDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr["WPRDID"] = 0;
                                    dr["WPRHID"] = 0;
                                    dr["JointType"] = txtJointType.Text;
                                    dr["BMThicknessPart1"] = txtBaseMetalThickPart1.Text;
                                    dr["BMThicknessPart2"] = txtbaseMetalthickpart2.Text;
                                    dr["BMSpecificationPart1"] = txtBaseMetalSpecificationPart1.Text;
                                    dr["BMSpecificationPNo1"] = txtBaseMetalSpecificationPNo1.Text;
                                    dr["BMSpecificationPart2"] = txtBaseMetalSpecificationPart2.Text;
                                    dr["BMSpecificationPNo2"] = txtBaseMetalSpecificationPNo2.Text;
                                    dr["PartNoToPartNo"] = txtPartNotoPartNo.Text;
                                    dr["WPSNo"] = txtWPSNo.Text;
                                    dr["Process"] = txtProcess.Text;
                                    dr["Remarks"] = txtRemarks.Text;
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();
                                    dr["WPRDID"] = 0;
                                    dr["WPRHID"] = 0;
                                    dr["JointType"] = txtJointType.Text;
                                    dr["BMThicknessPart1"] = txtBaseMetalThickPart1.Text;
                                    dr["BMThicknessPart2"] = txtbaseMetalthickpart2.Text;
                                    dr["BMSpecificationPart1"] = txtBaseMetalSpecificationPart1.Text;
                                    dr["BMSpecificationPNo1"] = txtBaseMetalSpecificationPNo1.Text;
                                    dr["BMSpecificationPart2"] = txtBaseMetalSpecificationPart2.Text;
                                    dr["BMSpecificationPNo2"] = txtBaseMetalSpecificationPNo2.Text;
                                    dr["PartNoToPartNo"] = txtPartNotoPartNo.Text;
                                    dr["WPSNo"] = txtWPSNo.Text;
                                    dr["Process"] = txtProcess.Text;
                                    dr["Remarks"] = txtRemarks.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvWeldPlanDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();
                            dr["WPRDID"] = 0;
                            dr["WPRHID"] = 0;
                            dr["JointType"] = "";
                            dr["BMThicknessPart1"] = "";
                            dr["BMThicknessPart2"] = "";
                            dr["BMSpecificationPart1"] = "";
                            dr["BMSpecificationPNo1"] = "";
                            dr["BMSpecificationPart2"] = "";
                            dr["BMSpecificationPNo2"] = "";
                            dr["PartNoToPartNo"] = "";
                            dr["WPSNo"] = "";
                            dr["Process"] = "";
                            dr["Remarks"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                    gvWeldPlanDetails.DataSource = dt;
                    gvWeldPlanDetails.DataBind();
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

    protected void btnSaveWPR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.WPRHID = hdnWPRHID.Value;
            objQt.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Customername = txtCustomerName.Text;
            objQt.RFPNo = txtRFPNo.Text;
            objQt.PONo = txtCustomerPONo.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.ItemName = txtItemName.Text;
            objQt.Technique = txtTechniqueUsed.Text;
            objQt.ProjectName = txtProjectName.Text;
            objQt.ITPNo = txtITPNo.Text;
            objQt.AcceptedQty = txtQuantity.Text;
            objQt.CreatedBy = objSession.employeeid;
            objQt.Size = txtSize.Text;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("WPRDID");
            dt.Columns.Add("WPRHID");
            dt.Columns.Add("JointType");
            dt.Columns.Add("BMThicknessPart1");
            dt.Columns.Add("BMThicknessPart2");
            dt.Columns.Add("BMSpecificationPart1");
            dt.Columns.Add("BMSpecificationPNo1");
            dt.Columns.Add("BMSpecificationPart2");
            dt.Columns.Add("BMSpecificationPNo2");
            dt.Columns.Add("PartNoToPartNo");
            dt.Columns.Add("WPSNo");
            dt.Columns.Add("Process");
            dt.Columns.Add("Remarks");

            foreach (GridViewRow row in gvWeldPlanDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                TextBox txtJointType = (TextBox)row.FindControl("txtJointType");
                TextBox txtBaseMetalThickPart1 = (TextBox)row.FindControl("txtBaseMetalThickPart1");
                TextBox txtbaseMetalthickpart2 = (TextBox)row.FindControl("txtbaseMetalthickpart2");
                TextBox txtBaseMetalSpecificationPart1 = (TextBox)row.FindControl("txtBaseMetalSpecificationPart1");
                TextBox txtBaseMetalSpecificationPNo1 = (TextBox)row.FindControl("txtBaseMetalSpecificationPNo1");
                TextBox txtBaseMetalSpecificationPart2 = (TextBox)row.FindControl("txtBaseMetalSpecificationPart2");
                TextBox txtBaseMetalSpecificationPNo2 = (TextBox)row.FindControl("txtBaseMetalSpecificationPNo2");
                TextBox txtPartNotoPartNo = (TextBox)row.FindControl("txtPartNotoPartNo");
                TextBox txtWPSNo = (TextBox)row.FindControl("txtWPSNo");
                TextBox txtProcess = (TextBox)row.FindControl("txtProcess");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["WPRDID"] = 0;
                    dr["WPRHID"] = 0;
                    dr["JointType"] = txtJointType.Text;
                    dr["BMThicknessPart1"] = txtBaseMetalThickPart1.Text;
                    dr["BMThicknessPart2"] = txtbaseMetalthickpart2.Text;
                    dr["BMSpecificationPart1"] = txtBaseMetalSpecificationPart1.Text;
                    dr["BMSpecificationPNo1"] = txtBaseMetalSpecificationPNo1.Text;
                    dr["BMSpecificationPart2"] = txtBaseMetalSpecificationPart2.Text;
                    dr["BMSpecificationPNo2"] = txtBaseMetalSpecificationPNo2.Text;
                    dr["PartNoToPartNo"] = txtPartNotoPartNo.Text;
                    dr["WPSNo"] = txtWPSNo.Text;
                    dr["Process"] = txtProcess.Text;
                    dr["Remarks"] = txtRemarks.Text;
                    dt.Rows.Add(dr);
                }
            }

            objQt.dt = dt;

            ds = objQt.SaveWeldPlanDetailsReport();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Weld Plan Details successfully');", true);
                bindWeldPlanReportHeader();
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Weld Plan Details  Updated successfully');", true);
                bindWeldPlanReportHeader();
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

    protected void gvWPRHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objQt.WPRHID = gvWPRHeader.DataKeys[index].Values[0].ToString();
            ds = objQt.GetWelPlanReportDetailsByWPRHID();

            if (e.CommandName == "EditWPR")
            {
                hdnWPRHID.Value = ds.Tables[0].Rows[0]["WPRHID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                ViewState["RFPHID"] = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                txtCustomerName.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtCustomerPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtTechniqueUsed.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                txtITPNo.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                txtQuantity.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();

                gvWeldPlanDetails.DataSource = ds.Tables[1];
                gvWeldPlanDetails.DataBind();
            }
            else if (e.CommandName == "WPRPDF")
            {
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDate"].ToString();
                lblCustomername_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblPoNo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                lblDrawingNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblItemname_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblProject_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                lblQAPNo_p.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                lblQuantity_p.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();

                gvWeldPlanDetails_p.DataSource = ds.Tables[1];
                gvWeldPlanDetails_p.DataBind();

                hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintWeldPlanReport();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWeldPlanDetails_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableHeaderCell HeaderCell = new TableHeaderCell();
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Base Material Thickness";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.CssClass = "subheader";
                HeaderCell.Attributes.Add("style", "border-right: 4px solid #026c63 ! important;");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Base Material Specification";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.CssClass = "subheader";
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.ColumnSpan = 4;
                HeaderGridRow.Cells.Add(HeaderCell);

                gvWeldPlanDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindWeldPlanReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetWeldPlanReportDetailsByRFPHID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWPRHeader.DataSource = ds.Tables[0];
                gvWPRHeader.DataBind();
            }
            else
            {
                gvWPRHeader.DataSource = "";
                gvWPRHeader.DataBind();
            }
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

    private void BindItemDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());
            ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsBYRFPHIDInWeldPlanDetails");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}