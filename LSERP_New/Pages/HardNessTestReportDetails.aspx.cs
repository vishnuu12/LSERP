using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_HardNessTestReportDetails : System.Web.UI.Page
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

                BindHardNessTestReportHeader();
                BindItemDetails();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                objQt.VERID = arg.ToString();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteHardnessTestReportHeaderByHTRHID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    BindHardNessTestReportHeader();
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
                dt.Columns.Add("HTRDID");
                dt.Columns.Add("HTRHID");
                dt.Columns.Add("JointNo");
                dt.Columns.Add("Weld1");
                dt.Columns.Add("Weld2");
                dt.Columns.Add("Weld3");
                dt.Columns.Add("WeldAvg");
                dt.Columns.Add("Haz1Part1");
                dt.Columns.Add("Haz2Part1");
                dt.Columns.Add("Haz3Part1");
                dt.Columns.Add("HazPart1Avg");
                dt.Columns.Add("Haz1Part2");
                dt.Columns.Add("Haz2Part2");
                dt.Columns.Add("Haz3Part2");
                dt.Columns.Add("HazPart2Avg");
                dt.Columns.Add("Parent1Part1");
                dt.Columns.Add("Parent2Part1");
                dt.Columns.Add("Parent3Part1");
                dt.Columns.Add("ParentPart1Avg");
                dt.Columns.Add("Parent1Part2");
                dt.Columns.Add("Parent2Part2");
                dt.Columns.Add("Parent3Part2");
                dt.Columns.Add("ParentPart2Avg");

                if (gvHardNessTestReportDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvHardNessTestReportDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvHardNessTestReportDetails.Rows)
                        {
                            string HTRDID = gvHardNessTestReportDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string HTRHID = gvHardNessTestReportDetails.DataKeys[row.RowIndex].Values[1].ToString();
                            TextBox txtJointNo = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtJointNo");
                            TextBox txtWeld1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtWeld1");
                            TextBox txtweld2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtweld2");
                            TextBox txtweld3 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtweld3");
                            TextBox txtweldavg = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtweldavg");
                            TextBox txtHaz1part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz1part1");
                            TextBox txtHaz2part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz2part1");
                            TextBox txtHaz3part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz3part1");
                            TextBox txtHazPart1Avg = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHazPart1Avg");
                            TextBox txtHaz1part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz1part2");
                            TextBox txtHaz2part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz2part2");
                            TextBox txtHaz3part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz3part2");
                            TextBox txtHazPart2Avg = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHazPart2Avg");
                            TextBox txtParent1Part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent1Part1");
                            TextBox txtParent2Part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent2Part1");
                            TextBox txtParent3Part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent3Part1");
                            TextBox txtParent1Average = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent1Average");
                            TextBox txtParent1Part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent1Part2");
                            TextBox txtParent2Part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent2Part2");
                            TextBox txtParent3Part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent3Part2");
                            TextBox txtParent2Average = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent2Average");

                            CheckBox chkpart = (CheckBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvHardNessTestReportDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr["HTRDID"] = HTRDID;
                                    dr["HTRHID"] = HTRHID;
                                    dr["JointNo"] = txtJointNo.Text;
                                    dr["Weld1"] = txtWeld1.Text;
                                    dr["Weld2"] = txtweld2.Text;
                                    dr["Weld3"] = txtweld3.Text;
                                    dr["WeldAvg"] = txtweldavg.Text;
                                    dr["Haz1Part1"] = txtHaz1part1.Text;
                                    dr["Haz2Part1"] = txtHaz2part1.Text;
                                    dr["Haz3Part1"] = txtHaz3part1.Text;
                                    dr["HazPart1Avg"] = txtHazPart1Avg.Text;
                                    dr["Haz1Part2"] = txtHaz1part2.Text;
                                    dr["Haz2Part2"] = txtHaz2part2.Text;
                                    dr["Haz3Part2"] = txtHaz3part2.Text;
                                    dr["HazPart2Avg"] = txtHazPart2Avg.Text;
                                    dr["Parent1Part1"] = txtParent1Part1.Text;
                                    dr["Parent2Part1"] = txtParent2Part1.Text;
                                    dr["Parent3Part1"] = txtParent3Part1.Text;
                                    dr["ParentPart1Avg"] = txtParent1Average.Text;
                                    dr["Parent1Part2"] = txtParent1Part2.Text;
                                    dr["Parent2Part2"] = txtParent2Part2.Text;
                                    dr["Parent3Part2"] = txtParent3Part2.Text;
                                    dr["ParentPart2Avg"] = txtParent2Average.Text;
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();
                                    dr["HTRDID"] = HTRDID;
                                    dr["HTRHID"] = HTRHID;
                                    dr["JointNo"] = txtJointNo.Text;
                                    dr["Weld1"] = txtWeld1.Text;
                                    dr["Weld2"] = txtweld2.Text;
                                    dr["Weld3"] = txtweld3.Text;
                                    dr["WeldAvg"] = txtweldavg.Text;
                                    dr["Haz1Part1"] = txtHaz1part1.Text;
                                    dr["Haz2Part1"] = txtHaz2part1.Text;
                                    dr["Haz3Part1"] = txtHaz3part1.Text;
                                    dr["HazPart1Avg"] = txtHazPart1Avg.Text;
                                    dr["Haz1Part2"] = txtHaz1part2.Text;
                                    dr["Haz2Part2"] = txtHaz2part2.Text;
                                    dr["Haz3Part2"] = txtHaz3part2.Text;
                                    dr["HazPart2Avg"] = txtHazPart2Avg.Text;
                                    dr["Parent1Part1"] = txtParent1Part1.Text;
                                    dr["Parent2Part1"] = txtParent2Part1.Text;
                                    dr["Parent3Part1"] = txtParent3Part1.Text;
                                    dr["ParentPart1Avg"] = txtParent1Average.Text;
                                    dr["Parent1Part2"] = txtParent1Part2.Text;
                                    dr["Parent2Part2"] = txtParent2Part2.Text;
                                    dr["Parent3Part2"] = txtParent3Part2.Text;
                                    dr["ParentPart2Avg"] = txtParent2Average.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvHardNessTestReportDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();
                            dr["HTRDID"] = 0;
                            dr["HTRHID"] = 0;
                            dr["JointNo"] = "";
                            dr["Weld1"] = "";
                            dr["Weld2"] = "";
                            dr["Weld3"] = "";
                            dr["WeldAvg"] = "";
                            dr["Haz1Part1"] = "";
                            dr["Haz2Part1"] = "";
                            dr["Haz3Part1"] = "";
                            dr["HazPart1Avg"] = "";
                            dr["Haz1Part2"] = "";
                            dr["Haz2Part2"] = "";
                            dr["Haz3Part2"] = "";
                            dr["HazPart2Avg"] = "";
                            dr["Parent1Part1"] = "";
                            dr["Parent2Part1"] = "";
                            dr["Parent3Part1"] = "";
                            dr["ParentPart1Avg"] = "";
                            dr["Parent1Part2"] = "";
                            dr["Parent2Part2"] = "";
                            dr["Parent3Part2"] = "";
                            dr["ParentPart2Avg"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                    gvHardNessTestReportDetails.DataSource = dt;
                    gvHardNessTestReportDetails.DataBind();
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

    protected void btnSaveHTR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.HTRHID = hdnHTRHID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.Customername = txtCustomerName.Text;
            objQt.RFPNo = txtRFPNo.Text;
            objQt.PONo = txtPONo.Text;
            objQt.ItemName = txtItemName.Text;
            objQt.QAPNo = txtQAPNo.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.BSLNo = txtBSLNo.Text;
            objQt.Size = txtSize.Text;
            objQt.Inspection = txtInspection.Text;
            objQt.WeldPlan = hdnWeldPlanname.Value;
            objQt.PartName1 = hdnPartname1.Value;
            objQt.PartName2 = hdnPartname2.Value;
            objQt.WorkOrderNo = txtWorkorderNo.Text;
            objQt.Instrument = txtInstrument.Text;
            objQt.CreatedBy = objSession.employeeid;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("HTRDID");
            dt.Columns.Add("HTRHID");
            dt.Columns.Add("JointNo");
            dt.Columns.Add("Weld1");
            dt.Columns.Add("Weld2");
            dt.Columns.Add("Weld3");
            dt.Columns.Add("WeldAvg");
            dt.Columns.Add("Haz1Part1");
            dt.Columns.Add("Haz2Part1");
            dt.Columns.Add("Haz3Part1");
            dt.Columns.Add("HazPart1Avg");
            dt.Columns.Add("Haz1Part2");
            dt.Columns.Add("Haz2Part2");
            dt.Columns.Add("Haz3Part2");
            dt.Columns.Add("HazPart2Avg");
            dt.Columns.Add("Parent1Part1");
            dt.Columns.Add("Parent2Part1");
            dt.Columns.Add("Parent3Part1");
            dt.Columns.Add("ParentPart1Avg");
            dt.Columns.Add("Parent1Part2");
            dt.Columns.Add("Parent2Part2");
            dt.Columns.Add("Parent3Part2");
            dt.Columns.Add("ParentPart2Avg");


            foreach (GridViewRow row in gvHardNessTestReportDetails.Rows)
            {
                string HTRDID = gvHardNessTestReportDetails.DataKeys[row.RowIndex].Values[0].ToString();
                string HTRHID = gvHardNessTestReportDetails.DataKeys[row.RowIndex].Values[1].ToString();
                TextBox txtJointNo = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtJointNo");
                TextBox txtWeld1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtWeld1");
                TextBox txtweld2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtweld2");
                TextBox txtweld3 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtweld3");
                TextBox txtweldavg = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtweldavg");
                TextBox txtHaz1part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz1part1");
                TextBox txtHaz2part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz2part1");
                TextBox txtHaz3part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz3part1");
                TextBox txtHazPart1Avg = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHazPart1Avg");
                TextBox txtHaz1part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz1part2");
                TextBox txtHaz2part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz2part2");
                TextBox txtHaz3part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHaz3part2");
                TextBox txtHazPart2Avg = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtHazPart2Avg");
                TextBox txtParent1Part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent1Part1");
                TextBox txtParent2Part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent2Part1");
                TextBox txtParent3Part1 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent3Part1");
                TextBox txtParent1Average = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent1Average");
                TextBox txtParent1Part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent1Part2");
                TextBox txtParent2Part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent2Part2");
                TextBox txtParent3Part2 = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent3Part2");
                TextBox txtParent2Average = (TextBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("txtParent2Average");

                CheckBox chk = (CheckBox)gvHardNessTestReportDetails.Rows[row.RowIndex].FindControl("chkitems");

                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["HTRDID"] = HTRDID;
                    dr["HTRHID"] = HTRHID;
                    dr["JointNo"] = txtJointNo.Text;
                    dr["Weld1"] = txtWeld1.Text;
                    dr["Weld2"] = txtweld2.Text;
                    dr["Weld3"] = txtweld3.Text;
                    dr["WeldAvg"] = txtweldavg.Text;
                    dr["Haz1Part1"] = txtHaz1part1.Text;
                    dr["Haz2Part1"] = txtHaz2part1.Text;
                    dr["Haz3Part1"] = txtHaz3part1.Text;
                    dr["HazPart1Avg"] = txtHazPart1Avg.Text;
                    dr["Haz1Part2"] = txtHaz1part2.Text;
                    dr["Haz2Part2"] = txtHaz2part2.Text;
                    dr["Haz3Part2"] = txtHaz3part2.Text;
                    dr["HazPart2Avg"] = txtHazPart2Avg.Text;
                    dr["Parent1Part1"] = txtParent1Part1.Text;
                    dr["Parent2Part1"] = txtParent2Part1.Text;
                    dr["Parent3Part1"] = txtParent3Part1.Text;
                    dr["ParentPart1Avg"] = txtParent1Average.Text;
                    dr["Parent1Part2"] = txtParent1Part2.Text;
                    dr["Parent2Part2"] = txtParent2Part2.Text;
                    dr["Parent3Part2"] = txtParent3Part2.Text;
                    dr["ParentPart2Avg"] = txtParent2Average.Text;
                    dt.Rows.Add(dr);
                }
            }

            objQt.dt = dt;

            ds = objQt.SaveHardNessTestReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','HardNess Report Details Saved successfully');", true);
                BindHardNessTestReportHeader();
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','HardNess Report Details Updated successfully');", true);
                BindHardNessTestReportHeader();
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

    protected void gvHTRHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objQt.HTRHID = gvHTRHeader.DataKeys[index].Values[0].ToString();
            ds = objQt.GetHardNessTestReportDetailsByHTRHID();

            if (e.CommandName == "EditWPR")
            {
                hdnHTRHID.Value = ds.Tables[0].Rows[0]["HTRHID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                txtCustomerName.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtQAPNo.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtSize.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                txtInspection.Text = ds.Tables[0].Rows[0]["Inspection"].ToString();
                hdnWeldPlanname.Value = ds.Tables[0].Rows[0]["WeldPlan"].ToString();
                hdnPartname1.Value = ds.Tables[0].Rows[0]["PartName1"].ToString();
                hdnPartname2.Value = ds.Tables[0].Rows[0]["PartName2"].ToString();
                txtWorkorderNo.Text = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                txtInstrument.Text = ds.Tables[0].Rows[0]["Instrument"].ToString();

                gvHardNessTestReportDetails.DataSource = ds.Tables[1];
                gvHardNessTestReportDetails.DataBind();
            }
            else if (e.CommandName == "WPRPDF")
            {
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblCustomername_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblReportNo_p.Text= ds.Tables[0].Rows[0]["ReportNo"].ToString(); 
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblPoNo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                lblItemname_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblQAPNo_p.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();
                lblDrawingNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblBSLNo_p.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                lblSize_p.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                lblInspection_p.Text = ds.Tables[0].Rows[0]["Inspection"].ToString();
                hdnWeldPlanname.Value = ds.Tables[0].Rows[0]["WeldPlan"].ToString();
                hdnPartname1.Value = ds.Tables[0].Rows[0]["PartName1"].ToString();
                hdnPartname2.Value = ds.Tables[0].Rows[0]["PartName2"].ToString();
                lblWorkorderNo_p.Text = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                lblInstrument_p.Text = ds.Tables[0].Rows[0]["Instrument"].ToString();

                gvhardnesstestReportDetails_p.DataSource = ds.Tables[1];
                gvhardnesstestReportDetails_p.DataBind();

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

    protected void gvHardNessTestReportDetails_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow;

                HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TextBox txt;
                TableHeaderCell HeaderCell;

                HeaderCell = new TableHeaderCell();
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Weld";
                HeaderCell.ColumnSpan = 4;
                HeaderCell.CssClass = "subheader";
                HeaderCell.Attributes.Add("style", "border-right: 4px solid #026c63 ! important;");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "HAZ";
                HeaderCell.ColumnSpan = 8;
                HeaderCell.CssClass = "subheader";
                HeaderCell.Attributes.Add("style", "border-right: 4px solid #026c63 ! important;");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "PARENT";
                HeaderCell.ColumnSpan = 8;
                HeaderCell.CssClass = "subheader";
                HeaderCell.Attributes.Add("style", "border-right: 4px solid #026c63 ! important;");
                HeaderGridRow.Cells.Add(HeaderCell);

                gvHardNessTestReportDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                HeaderCell = new TableHeaderCell();
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                txt = new TextBox();
                txt.ID = "txtweldpartname";
                txt.CssClass = "form-control mandatoryfield weldpartname";
                txt.Text = hdnWeldPlanname.Value;
                HeaderCell.ColumnSpan = 4;
                HeaderCell.Controls.Add(txt);
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                txt = new TextBox();
                txt.ID = "txtHazpart1name";
                txt.CssClass = "form-control mandatoryfield hazpartname1";
                txt.Text = hdnPartname1.Value;
                HeaderCell.ColumnSpan = 4;
                HeaderCell.Controls.Add(txt);
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                txt = new TextBox();
                txt.ID = "txtHazpart2name";
                txt.CssClass = "form-control mandatoryfield hazpartname2";
                txt.Text = hdnPartname2.Value;
                HeaderCell.ColumnSpan = 4;
                HeaderCell.Controls.Add(txt);
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                txt = new TextBox();
                txt.ID = "txtparentpart1name";
                txt.CssClass = "form-control mandatoryfield parentpartname1";
                txt.Text = hdnPartname1.Value;
                HeaderCell.ColumnSpan = 4;
                HeaderCell.Controls.Add(txt);
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                txt = new TextBox();
                txt.ID = "txtparentpart2name";
                txt.CssClass = "form-control mandatoryfield parentpartname2";
                txt.Text = hdnPartname2.Value;
                HeaderCell.ColumnSpan = 4;
                HeaderCell.Controls.Add(txt);
                HeaderGridRow.Cells.Add(HeaderCell);

                gvHardNessTestReportDetails.Controls[0].Controls.AddAt(1, HeaderGridRow);

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindHardNessTestReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ViewState["RFPHID"].ToString());
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetHardNessTestReportHeaderByRFPHID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvHTRHeader.DataSource = ds.Tables[0];
                gvHTRHeader.DataBind();
            }
            else
            {
                gvHTRHeader.DataSource = "";
                gvHTRHeader.DataBind();
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