using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_SpringRateAndMovementTestReport : System.Web.UI.Page
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
                //  bindRGSReportDetails();
            }

            if (target == "deleteSRMTRID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteSpringRateReportDetailsBySRMTRID", arg.ToString());

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

                bindSpringRateReportDetails();
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
            //  bindSpringRateItemDetails();
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
                dt.Columns.Add("SRMDDID");
                dt.Columns.Add("SRMTRID");
                dt.Columns.Add("Movement");
                dt.Columns.Add("CompressionLoadKG");
                dt.Columns.Add("ReturnLoadKG");

                if (gvDimensions.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvDimensions.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvDimensions.Rows)
                        {
                            string SRMDDID = gvDimensions.DataKeys[row.RowIndex].Values[0].ToString();
                            string SRMTRID = gvDimensions.DataKeys[row.RowIndex].Values[1].ToString();
                            TextBox txtMovement = (TextBox)gvDimensions.Rows[row.RowIndex].FindControl("txtMovement");
                            TextBox txtCompressionLoad = (TextBox)gvDimensions.Rows[row.RowIndex].FindControl("txtCompressionLoad");
                            TextBox txtReturnLoad = (TextBox)gvDimensions.Rows[row.RowIndex].FindControl("txtReturnLoad");

                            CheckBox chkpart = (CheckBox)gvDimensions.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvDimensions.Rows.Count)
                            {
                                if (chkpart.Checked)
                                {
                                    dr = dt.NewRow();
                                    dr["SRMDDID"] = 0;
                                    dr["SRMTRID"] = 0;
                                    dr["Movement"] = txtMovement.Text;
                                    dr["CompressionLoadKG"] = txtCompressionLoad.Text;
                                    dr["ReturnLoadKG"] = txtReturnLoad.Text;
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();

                                    dr["SRMDDID"] = 0;
                                    dr["SRMTRID"] = 0;
                                    dr["Movement"] = txtMovement.Text;
                                    dr["CompressionLoadKG"] = txtCompressionLoad.Text;
                                    dr["ReturnLoadKG"] = txtReturnLoad.Text;
                                    dt.Rows.Add(dr);
                                }

                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvDimensions.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();

                            dr["SRMDDID"] = "";
                            dr["SRMTRID"] = "";
                            dr["Movement"] = "";
                            dr["CompressionLoadKG"] = "";
                            dr["ReturnLoadKG"] = "";

                            dt.Rows.Add(dr);
                        }
                    }

                    gvDimensions.DataSource = dt;
                    gvDimensions.DataBind();
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

    protected void btnSaveWTR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        DataTable dtGvDimension;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.SRMTRID = hdnSRMTRID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RFPNo = ddlRFPNo.SelectedItem.Text;
            objQt.IfSpecifyItemName = txtItemName.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.PONo = txtPONo.Text;
            objQt.QAPNo = txtQAPNo.Text;
            // objQt.BSLNo = txtBSLNo.Text;
            objQt.CreatedBy = objSession.employeeid;

            objQt.TotalAverage = txtTotalAverage.Text;
            objQt.DrawingSpringRateValue = txtDrawingSpringRateValue.Text;
            objQt.SpringRateActualGraphValue = txtActualSpringRateGraphValue.Text;
            objQt.Percentage = txtPercentage.Text;

            objQt.Size = txtSize.Text;
            objQt.BSLNo = txtBSLNo.Text;
            objQt.AcceptedQty = txtQTY.Text;
            objQt.OtherReference = txtOtherReference.Text;
            objQt.TestProcedureNo = txtProcedureNo.Text;
            objQt.Type = txtType.Text;

            dtGvDimension = new DataTable();

            dtGvDimension.Columns.Add("SRMDDID");
            dtGvDimension.Columns.Add("SRMTRID");
            dtGvDimension.Columns.Add("Movement");
            dtGvDimension.Columns.Add("CompressionLoadKG");
            dtGvDimension.Columns.Add("ReturnLoadKG");

            DataRow drDimension;

            foreach (GridViewRow row in gvDimensions.Rows)
            {
                string SRMDDID = gvDimensions.DataKeys[row.RowIndex].Values[0].ToString();
                string SRMTRID = gvDimensions.DataKeys[row.RowIndex].Values[1].ToString();
                TextBox txtMovement = (TextBox)gvDimensions.Rows[row.RowIndex].FindControl("txtMovement");
                TextBox txtCompressionLoad = (TextBox)gvDimensions.Rows[row.RowIndex].FindControl("txtCompressionLoad");
                TextBox txtReturnLoad = (TextBox)gvDimensions.Rows[row.RowIndex].FindControl("txtReturnLoad");

                CheckBox chkpart = (CheckBox)gvDimensions.Rows[row.RowIndex].FindControl("chkitems");

                if (chkpart.Checked)
                {
                    drDimension = dtGvDimension.NewRow();
                    drDimension["SRMDDID"] = SRMDDID;
                    drDimension["SRMTRID"] = SRMTRID;
                    drDimension["Movement"] = txtMovement.Text;
                    drDimension["CompressionLoadKG"] = txtCompressionLoad.Text;
                    drDimension["ReturnLoadKG"] = txtReturnLoad.Text;
                    dtGvDimension.Rows.Add(drDimension);
                }
            }

            objQt.dtDimension = dtGvDimension;

            ds = objQt.SaveSpringRateAndMovementTestReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Spring rate Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Spring rate Report Updated successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvSpringrateReportHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string SRMTRID = gvSpringrateReportHeader.DataKeys[index].Values[0].ToString();
            objQt.SRMTRID = SRMTRID;
            ds = objQt.GetSpringrateAndMovementTestReportDetailsBySRMTRID();

            if (e.CommandName.ToString() == "EditSpringRate")
            {
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                hdnSRMTRID.Value = ds.Tables[0].Rows[0]["SRMTRID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtJobDescription.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtQAPNo.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtTotalAverage.Text = ds.Tables[0].Rows[0]["TotalAverage"].ToString();
                txtDrawingSpringRateValue.Text = ds.Tables[0].Rows[0]["DrawingSpringRateValue"].ToString();
                txtActualSpringRateGraphValue.Text = ds.Tables[0].Rows[0]["SpringRateActualGraphValue"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                txtSize.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtQTY.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();
                txtOtherReference.Text = ds.Tables[0].Rows[0]["OtherReference"].ToString();
                txtProcedureNo.Text = ds.Tables[0].Rows[0]["TestProcedureNo"].ToString();
                txtType.Text = ds.Tables[0].Rows[0]["Type"].ToString();

                gvDimensions.DataSource = ds.Tables[1];
                gvDimensions.DataBind();
            }

            if (e.CommandName.ToString() == "Print")
            {
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDate"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                // lblDra.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblProjectName_p.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                // txtQAPNo.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();            
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();

                // txtTotalAverage.Text = ds.Tables[0].Rows[0]["TotalAverage"].ToString();
                lblSpringratevalue_p.Text = ds.Tables[0].Rows[0]["DrawingSpringRateValue"].ToString();
                lblspringrateActualAverage_p.Text = ds.Tables[0].Rows[0]["SpringRateActualGraphValue"].ToString();
                txtPercentage.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
                lblSize_p.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                lblBSLNo_p.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                lblQuantity_p.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();
                lblothereReference_p.Text = ds.Tables[0].Rows[0]["OtherReference"].ToString();
                lblProcedureNo_p.Text = ds.Tables[0].Rows[0]["TestProcedureNo"].ToString();
                lblType_p.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                // lblremarks_p.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                gvSpringRateMovementDimensionDetails_p.DataSource = ds.Tables[1];
                gvSpringRateMovementDimensionDetails_p.DataBind();

                hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrinrSpringRateTestReport();", true);
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindSpringRateReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetSpringRateTestReportDetails");
            gvSpringrateReportHeader.DataSource = ds.Tables[0];
            gvSpringrateReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindSpringRateItemDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetSpringRateReportItemDetails");

            //gvItemDetails.DataSource = ds.Tables[0];
            //gvItemDetails.DataBind();

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