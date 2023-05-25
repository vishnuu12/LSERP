using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_PositiveMaterialIdentificationReport : System.Web.UI.Page
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

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeletePMIReportDetailsByPMIRID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','PMI Deleted successfully');", true);
                    ddlRFPNo_SelectIndexChanged(null, null);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                dt.Columns.Add("PMIPDID");
                dt.Columns.Add("PMIRID");
                //  dt.Columns.Add("PartName");
                dt.Columns.Add("Elements");
                //  dt.Columns.Add("STDRequirtment");
                dt.Columns.Add("Value1");
                dt.Columns.Add("Value2");
                //dt.Columns.Add("Value3");
                //dt.Columns.Add("Value4");
                //dt.Columns.Add("Value5");
                dt.Columns.Add("PMIResult");

                if (gvPartDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvPartDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvPartDetails.Rows)
                        {
                            string PMIPDID = gvPartDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string PMIRID = gvPartDetails.DataKeys[row.RowIndex].Values[1].ToString();
                            //   TextBox txtPartName = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtPartName");
                            TextBox txtElements = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtElements");
                            //   TextBox txtSTDRequirtment = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtSTDRequirtment");
                            TextBox txtValue1 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtRequirtment");
                            TextBox txtValue2 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtobserved");
                            //   TextBox txtValue3 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtValue3");
                            //   TextBox txtValue4 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtValue4");
                            //   TextBox txtValue5 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtValue5");
                            TextBox txtPMIResult = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtPMIResult");
                            CheckBox chkpart = (CheckBox)gvPartDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvPartDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr["PMIPDID"] = PMIPDID;
                                    dr["PMIRID"] = PMIRID;
                                    // dr["PartName"] = txtPartName.Text;
                                    dr["Elements"] = txtElements.Text;
                                    //   dr["STDRequirtment"] = txtSTDRequirtment.Text;
                                    dr["Value1"] = txtValue1.Text;
                                    dr["Value2"] = txtValue2.Text;
                                    //dr["Value3"] = txtValue3.Text;
                                    //dr["Value4"] = txtValue4.Text;
                                    //dr["Value5"] = txtValue5.Text;
                                    dr["PMIResult"] = txtPMIResult.Text;
                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();

                                    dr["PMIPDID"] = PMIPDID;
                                    dr["PMIRID"] = PMIRID;
                                    //dr["PartName"] = txtPartName.Text;
                                    dr["Elements"] = txtElements.Text;
                                    // dr["STDRequirtment"] = txtSTDRequirtment.Text;
                                    dr["Value1"] = txtValue1.Text;
                                    dr["Value2"] = txtValue2.Text;
                                    //dr["Value3"] = txtValue3.Text;
                                    //dr["Value4"] = txtValue4.Text;
                                    //dr["Value5"] = txtValue5.Text;
                                    dr["PMIResult"] = txtPMIResult.Text;
                                    dt.Rows.Add(dr);
                                }

                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvPartDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();
                            dr["PMIPDID"] = "";
                            dr["PMIRID"] = "";
                            // dr["PartName"] = "";
                            dr["Elements"] = "";
                            //  dr["STDRequirtment"] = "";
                            dr["Value1"] = "";
                            dr["Value2"] = "";
                            //dr["Value3"] = "";
                            //dr["Value4"] = "";
                            //dr["Value5"] = "";
                            dr["PMIResult"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                    gvPartDetails.DataSource = dt;
                    gvPartDetails.DataBind();
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

    protected void btnSavePMIReport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.PMIRID = hdnPMIRID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.IfSpecifyItemName = txtItemName.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.PONo = txtPONo.Text;
            objQt.ItemQty = txtItemQty.Text;
            objQt.QAPNo = txtQAPNo.Text;
            objQt.BSLNo = txtBSLNo.Text;

            objQt.ITPNo = txtITPDocNo.Text;
            objQt.tagNo = txtTagNo.Text;
            objQt.EILSpecification = txtEILSpecification.Text;
            objQt.DateOfCal = DateTime.ParseExact(txtDateOfCall.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.DueForcal = DateTime.ParseExact(txtDueForCal.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Instrument = txtInstrument.Text;
            objQt.ReportReferenceNo = txtReportReference.Text;
            objQt.RFPNo = txtRFPNo.Text;
            objQt.Remarks = txtRemarks.Text;
            objQt.Customername = txtCustomername.Text;
            objQt.ProjectName = txtProject.Text;
            objQt.Size = txtSize.Text;
            objQt.DRGPartNo = txtDRGPartNo_p.Text;

            objQt.UserID = objSession.employeeid;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("PMIPDID");
            dt.Columns.Add("PMIRID");
            dt.Columns.Add("PartName");
            dt.Columns.Add("Elements");
            dt.Columns.Add("STDRequirtment");
            dt.Columns.Add("Value1");
            dt.Columns.Add("Value2");
            dt.Columns.Add("Value3");
            dt.Columns.Add("Value4");
            dt.Columns.Add("Value5");
            dt.Columns.Add("PMIResult");

            foreach (GridViewRow row in gvPartDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");

                string PMIPDID = gvPartDetails.DataKeys[row.RowIndex].Values[0].ToString();
                string PMIRID = gvPartDetails.DataKeys[row.RowIndex].Values[1].ToString();
                //TextBox txtPartName = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtPartName");
                TextBox txtElements = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtElements");
                //TextBox txtSTDRequirtment = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtSTDRequirtment");

                TextBox txtValue1 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtRequirtment");
                TextBox txtValue2 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtobserved");
                //TextBox txtValue3 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtValue3");
                //TextBox txtValue4 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtValue4");
                //TextBox txtValue5 = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtValue5");
                TextBox txtPMIResult = (TextBox)gvPartDetails.Rows[row.RowIndex].FindControl("txtPMIResult");
                CheckBox chkpart = (CheckBox)gvPartDetails.Rows[row.RowIndex].FindControl("chkitems");

                dr = dt.NewRow();
                if (chkpart.Checked)
                {
                    dr["PMIPDID"] = PMIPDID;
                    dr["PMIRID"] = PMIRID;
                    dr["PartName"] = "";
                    dr["Elements"] = txtElements.Text;
                    dr["STDRequirtment"] = "";
                    dr["Value1"] = txtValue1.Text;
                    dr["Value2"] = txtValue2.Text;
                    dr["Value3"] = "";
                    dr["Value4"] = "";
                    dr["Value5"] = "";
                    dr["PMIResult"] = txtPMIResult.Text;
                    dt.Rows.Add(dr);
                }
            }

            objQt.dt = dt;
            ds = objQt.SavePositiveMaterialIdentificationReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PMI Report Saved successfully');", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PMI Updated successfully');", true);
            ddlRFPNo_SelectIndexChanged(null, null);
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

                bindPMIReportHeader();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }

            hdnPMIRID.Value = "0";
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

            bindPartDetailsOfPMIReport();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPMIReportHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objQt.PMIRID = gvPMIReportHeader.DataKeys[index].Values[0].ToString();
            ds = objQt.GetPMIReportDetailsByPMIRID_PRINT();

            if (e.CommandName.ToString() == "PDF")
            {
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();

                lblCustomer_p.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                lblProject_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                lblDRGNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                lblITPNo_p.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                lblSize_p.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                lblBSLNo_p.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                lblQTY_p.Text = ds.Tables[0].Rows[0]["ItemQty"].ToString();
                lblDescription_p.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                lblDRGPartNo_p.Text = ds.Tables[0].Rows[0]["DRGPartNO"].ToString();
                lblTagNo_p.Text = ds.Tables[0].Rows[0]["tagNo"].ToString();
                lblEILSpecification_p.Text = ds.Tables[0].Rows[0]["EILSpecification"].ToString();

                gvPMIReportDetails_p.DataSource = ds.Tables[1];
                gvPMIReportDetails_p.DataBind();

                lblRemarks_p.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                lblInstrument_p.Text = ds.Tables[0].Rows[0]["Instrument"].ToString();
                lblDateOfCal_p.Text = ds.Tables[0].Rows[0]["DateOfCalView"].ToString();
                lblDueOfcal_p.Text = ds.Tables[0].Rows[0]["DueForcalView"].ToString();
                lblReportReference_p.Text = ds.Tables[0].Rows[0]["ReportReferenceNo"].ToString();

                hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintPositiveMaterialIdentificationReport();", true);
            }
            if (e.CommandName == "EditPMI")
            {
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                hdnPMIRID.Value = ds.Tables[0].Rows[0]["PMIRID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtJobDescription.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtItemQty.Text = ds.Tables[0].Rows[0]["ItemQty"].ToString();
                txtQAPNo.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtITPDocNo.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                txtTagNo.Text = ds.Tables[0].Rows[0]["tagNo"].ToString();
                txtEILSpecification.Text = ds.Tables[0].Rows[0]["EILSpecification"].ToString();
                txtDateOfCall.Text = ds.Tables[0].Rows[0]["DateOfCalEdit"].ToString();
                txtDueForCal.Text = ds.Tables[0].Rows[0]["DueForcalEdit"].ToString();
                txtInstrument.Text = ds.Tables[0].Rows[0]["Instrument"].ToString();
                txtReportReference.Text = ds.Tables[0].Rows[0]["ReportReferenceNo"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtCustomername.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                txtProject.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                txtSize.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                txtDRGPartNo_p.Text = ds.Tables[0].Rows[0]["DRGPartNo"].ToString();

                gvPartDetails.DataSource = ds.Tables[1];
                gvPartDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPMIReportDetails_p_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindPMIReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetPMIReportHeader");
            gvPMIReportHeader.DataSource = ds.Tables[0];
            gvPMIReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindPartDetailsOfPMIReport()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetPMIPartDetailsAndReportNoByPMID");

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    gvPartDetails.DataSource = ds.Tables[0];
            //    gvPartDetails.DataBind();
            //}
            //else

            gvPartDetails.DataSource = "";
            gvPartDetails.DataBind();


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