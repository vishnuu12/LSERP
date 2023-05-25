using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;
using System.Globalization;

public partial class Pages_PrimaryJobOrder : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cProduction objP;
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
                ddlCustomerNameAndRFPNo();
                ShowHideControls("add");
            }

            if (target == "deletePayment")
            {
                objP = new cProduction();
                DataSet ds = new DataSet();
                objP.CPDID = Convert.ToInt32(arg);
                ds = objP.DeleteContractorPaymentDetailsByCPDID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Payment Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                bindJobCardDetails();
                BindContractorPaymentDetails();
            }

            if (target == "SharePJO")
            {
                objP = new cProduction();
                DataSet ds = new DataSet();
                objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
                ds = objP.SharePrimaryJoborderByRFPDID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Shared successfully');hidePartDetailpopup();", true);
                    BindPrimaryJobOrderDetails();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }

            if (target == "ShareAllPJO")
            {
                objP = new cProduction();
                DataSet ds = new DataSet();
                objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
                ds = objP.SharePrimaryJoborderByRFPHID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Shared successfully');", true);
                    BindPrimaryJobOrderDetails();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ShowHideControls("add");
        ddlCustomerNameAndRFPNo();
    }

    #endregion

    #region"DropDown Events"

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                ShowHideControls("add,view");

                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                //  objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRFPHID");
                BindPrimaryJobOrderDetails();
            }
            else
            {
                ShowHideControls("add");
                //  objc.EmptyDropDownList(ddlItemName);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            ShowHideControls("add");
            dt = (DataTable)ViewState["RFPDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlRFPNo.DataSource = dt;
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void ddlItemName_OnSelectIndexChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        if (ddlItemName.SelectedIndex > 0)
    //        {

    //        }
    //        else
    //        {
    //            ShowHideControls("add,view");
    //            lblItemQty.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    //protected void ddlRateGroup_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlRateGroup.SelectedIndex > 0)
    //        {
    //            decimal Rate = Convert.ToDecimal(ddlRateGroup.SelectedValue.Split('/')[1].ToString());
    //            lblUnitRate.Text = Convert.ToString(Rate * Convert.ToDecimal(lblUnitCalculatedWeight.Text.Replace("Kgs", "")));
    //        }
    //        else
    //            lblUnitRate.Text = "";
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void ddlContractorName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            if (ddlContractorName.SelectedIndex > 0)
            {
                // divContractorPayment.Visible = true;
                //  objP.CMID = Convert.ToInt32(ddlContractorName.SelectedValue.Split('/')[0].ToString());
                objP.CTDID = Convert.ToInt32(ddlContractorName.SelectedValue);
                objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);

                ds = objP.GetContractorPaymentDetailsByCMIDAndCTDID();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvContractorPaymentDetails.DataSource = ds.Tables[1];
                    gvContractorPaymentDetails.DataBind();
                }
                else
                {
                    gvContractorPaymentDetails.DataSource = "";
                    gvContractorPaymentDetails.DataBind();
                }
                lblContractorAmt.Text = "Contract Amt : " + ds.Tables[0].Rows[0]["ContractorAmount"].ToString();
                lblPaidAmt.Text = "Paid Amt : " + ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                lblBalanceAmt.Text = "Balance Amt : " + ds.Tables[0].Rows[0]["BalanceAmount"].ToString();

                hdnContractorAmount.Value = ds.Tables[0].Rows[0]["ContractorAmount"].ToString();
                hdnPaidAmount.Value = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                hdnBalanceAmt.Value = ds.Tables[0].Rows[0]["BalanceAmount"].ToString();
            }
            else
            {
                //  divContractorPayment.Visible = false;
                hdnContractorAmount.Value = "0";
                hdnPaidAmount.Value = "0";
                hdnBalanceAmt.Value = "0";

                lblContractorAmt.Text = "";
                lblPaidAmt.Text = "";
                lblBalanceAmt.Text = "";
            }
            txtAmountInPercentage.Text = "";
            txtAmount.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

    protected void rblBellowFormingType_b_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            //objP.BOMID = Convert.ToInt32(ViewState["BOMID"].ToString());
            //objP.BellowFormType = rblBellowFormingType_b.SelectedValue;

            //ds = objP.GetBellowFormingCostDetailsbyFormType();
            //if (rblBellowFormingType_b.SelectedValue == "Roll")
            //    lblFormTypeCost_b.Text = "Bellow Forming Total Cost (Roll):" + ds.Tables[0].Rows[0]["TotalCost"].ToString();
            //if (rblBellowFormingType_b.SelectedValue == "Expandal")
            //    lblFormTypeCost_b.Text = "Bellow Forming Total Cost (Expandal):" + ds.Tables[0].Rows[0]["TotalCost"].ToString();

            //ViewState["FormingCost"] = ds.Tables[0].Rows[0]["TotalCost"].ToString();

            objP.BOMID = Convert.ToInt32(ViewState["BOMID"]);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.BellowFormType = rblBellowFormingType_b.SelectedValue;
            ds = objP.GetBellowsCostDetailsByBOMID();

            BindBellowsCostDetails("add", ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void rblTangetCuttingCost_b_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.BOMID = Convert.ToInt32(ViewState["BOMID"].ToString());
            objP.TangetCuttingType = rblTangetCuttingCost_b.SelectedValue;

            ds = objP.GetTangetCuttingCostByCuttingType();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Get")
            {
                if (rblTangetCuttingCost_b.SelectedValue == "Plasma")
                    lblTangetCuttingTotalCost_b.Text = "Tanget Cutting Total Cost (Plasma):" + ds.Tables[1].Rows[0]["TotalCost"].ToString();
                else
                    lblTangetCuttingTotalCost_b.Text = "Tanget Cutting Total Cost (Scisscor):" + ds.Tables[1].Rows[0]["TotalCost"].ToString();

                ViewState["TangetCost"] = ds.Tables[1].Rows[0]["TotalCost"].ToString();
            }
            else
            {
                if (rblTangetCuttingCost_b.SelectedValue == "Plasma")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Thickness Range Failure! Select Scisscor Cutting');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','ID And THK Rang Faliure! Select Plasma Cutting');", true);

                rblTangetCuttingCost_b.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        DataTable dt = new DataTable();
        decimal Rate = 0;
        decimal PartUnitrate = 0;
        decimal PartTotalrate = 0;
        string PJORateType = "";
        string DeviationStatus = "";
        decimal DesignAWT = 0;
        int Qty = 0;
        try
        {
            // string[] str = hdnPartRateDetails.Value.Split(',');

            dt.Columns.Add("PRDID");
            dt.Columns.Add("RFPDID");
            dt.Columns.Add("BOMID");
            dt.Columns.Add("PartUnitRate");
            dt.Columns.Add("PartTotalRate");
            dt.Columns.Add("PJORate");
            dt.Columns.Add("AWT");
            dt.Columns.Add("DeviationStatus");

            Rate = Convert.ToDecimal(0.00);

            foreach (GridViewRow row in gvPartNameDetails.Rows)
            {
                //string[] strVal = str[i].Split('-');
                CheckBox chkQC = (CheckBox)row.FindControl("chkQC");
                /// TextBox txtCalWT = (TextBox)row.FindControl("txtCalWT");

                PJORateType = gvPartNameDetails.DataKeys[row.RowIndex].Values[3].ToString();
                DesignAWT = Convert.ToDecimal(gvPartNameDetails.DataKeys[row.RowIndex].Values[4].ToString());

                DeviationStatus = null;

                if (chkQC.Checked)
                {
                    // PartUnitrate = Convert.ToDecimal(Convert.ToDecimal(txtCalWT.Text) * Rate);
                    Qty = Convert.ToInt32(gvPartNameDetails.DataKeys[row.RowIndex].Values[2].ToString());

                    DataRow dr;

                    dr = dt.NewRow();
                    dr["RFPDID"] = hdnRFPDID.Value;
                    dr["BOMID"] = gvPartNameDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["PartUnitRate"] = 0;
                    dr["PartTotalRate"] = 0;
                    dr["PRDID"] = 0;
                    dr["PJORate"] = PJORateType;
                    dr["AWT"] = 0;
                    dr["DeviationStatus"] = DeviationStatus;
                    dt.Rows.Add(dr);
                }
            }

            //else
            //{
            //    DataRow dr;

            //    dr = dt.NewRow();
            //    dr["RFPDID"] = "0";
            //    dr["BOMID"] = "0";
            //    dr["PartUnitRate"] = "0";
            //    dr["PartTotalRate"] = "0";
            //    dr["PRDID"] = "0";
            //    dr["PJORate"] = "0";
            //    dr["AWT"] = "0";
            //    dt.Rows.Add(dr);
            //}

            objP.dt = dt;

            int ItemQTY = Convert.ToInt32(hdnItemQTY.Value);
            objP.PJOID = Convert.ToInt32(hdnPJOID.Value);

            if (ViewState["PJOStatus"].ToString() == "Completed")
                objP.RGMID = 0;
            else
                objP.RGMID = Convert.ToInt32(ddlRateGroup.SelectedValue.Split('/')[0].ToString());

            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            //objP.UnitRate = Convert.ToDecimal(hdnUnitRate.Value);
            //objP.TotalRate = Convert.ToDecimal(ItemQTY * Convert.ToDecimal(hdnUnitRate.Value));
            //objP.TotalCalculatedWeight = Convert.ToDecimal(Convert.ToDecimal(lblUnitCalculatedWeight.Text.Replace("Kgs", "")) * ItemQTY);
            //objP.UnitCalculatedWeight = Convert.ToDecimal(lblUnitCalculatedWeight.Text.Replace("Kgs", ""));
            objP.UnitAWTWeight = Convert.ToDecimal(0.00);
            objP.TotalAWTWeight = Convert.ToDecimal(0.00);
            objP.Remarks = txtRemarks.Text;
            objP.ItemQTY = Convert.ToInt32(hdnItemQTY.Value);
            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);

            ds = objP.SavePrimaryJobOrderDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Job Order Details Saved successfully');PartDetailsShowPopUp();", true);
            //else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            //{
            //    hdnPJOID.Value = "0";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Job Order Details Updated successfully');PartDetailsShowPopUp();", true);
            //}
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            BindPrimaryJobOrderDetails();
            BindPartDetails("AddJobOrder");
            BindPrimaryJobOrderPartCostDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured')", true);
            Log.Message(ex.ToString());
        }
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ShowHideControls("add,view");
    //        hdnPJOID.Value = "0";
    //        // ddlItemName.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        GeneratePDFFile();
    }

    protected void btnSaveBellowCost_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.BOMID = Convert.ToInt32(ViewState["BOMID"].ToString());
            objP.PJOID = Convert.ToInt32(hdnPJOID.Value);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.SheetMarkingCost = Convert.ToDecimal(ViewState["SheetMarkingCost"]);
            objP.FormingCost = Convert.ToDecimal(ViewState["FormingCost"]);
            objP.TangetCost = Convert.ToDecimal(ViewState["TangetCost"]);
            objP.BellowFormType = rblBellowFormingType_b.SelectedValue;
            objP.TangetCuttingType = rblTangetCuttingCost_b.SelectedValue;
            objP.BellowCalculatedWeight = Convert.ToDecimal(ViewState["BellowCalculatedWeight"]);

            ds = objP.SaveBellowCostDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Bellows Cost Details Saved Succesfully');hideBellowDetailsPopUp();", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Bellows Cost Details Updated Succesfully');hideBellowDetailsPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            BindPartDetails("AddJobOrder");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSavePayment_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objP = new cProduction();
        DateTime dtime;
        try
        {
            //objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ////objP.AmtInPercentage = Convert.ToDecimal(txtAmountInPercentage.Text);
            //objP.Amount = Convert.ToDecimal(txtAmount.Text);
            //objP.PaymentDate = DateTime.ParseExact(txtPaymentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //objP.Remarks = txtPaymentRemarks.Text;
            //objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
            ////objP.CMID = Convert.ToInt32(ddlContractorName.SelectedValue.Split('/')[0].ToString());
            //objP.CTDID = Convert.ToInt32(ddlContractorName.SelectedValue);

            string JCHID = "";
            string CTDID = "";

            DataRow dr;

            dt.Columns.Add("JCHID");
            dt.Columns.Add("CTDID");
            dt.Columns.Add("AMT");
            dt.Columns.Add("PaymentDate");
            dt.Columns.Add("Remarks");

            foreach (GridViewRow row in gvJobCardProcessDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                JCHID = gvJobCardProcessDetails.DataKeys[row.RowIndex].Values[0].ToString();
                CTDID = gvJobCardProcessDetails.DataKeys[row.RowIndex].Values[1].ToString();

                TextBox txtPayment = (TextBox)gvJobCardProcessDetails.Rows[row.RowIndex].FindControl("txtEnterAmount");
                TextBox txtpaymentdate = (TextBox)gvJobCardProcessDetails.Rows[row.RowIndex].FindControl("txtpaymentdate");
                TextBox txtremarks = (TextBox)gvJobCardProcessDetails.Rows[row.RowIndex].FindControl("txtremarks");

                if (chkitems.Checked && Convert.ToDecimal(txtPayment.Text) > 0)
                {
                    dr = dt.NewRow();
                    dr["JCHID"] = JCHID;
                    dr["CTDID"] = CTDID;
                    dr["AMT"] = txtPayment.Text;
                    dtime = DateTime.ParseExact(txtpaymentdate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dr["PaymentDate"] = dtime.ToString("MM/dd/yyyy").Replace("-", "/");
                    dr["Remarks"] = txtremarks.Text;

                    dt.Rows.Add(dr);
                }
            }

            foreach (GridViewRow row in gvAssemplyJobCard.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                JCHID = gvAssemplyJobCard.DataKeys[row.RowIndex].Values[0].ToString();
                CTDID = gvAssemplyJobCard.DataKeys[row.RowIndex].Values[1].ToString();

                TextBox txtPayment = (TextBox)gvAssemplyJobCard.Rows[row.RowIndex].FindControl("txtEnterAmount");
                TextBox txtpaymentdate = (TextBox)gvAssemplyJobCard.Rows[row.RowIndex].FindControl("txtpaymentdate");
                TextBox txtremarks = (TextBox)gvAssemplyJobCard.Rows[row.RowIndex].FindControl("txtremarks");

                if (chkitems.Checked && Convert.ToDecimal(txtPayment.Text) > 0)
                {
                    dr = dt.NewRow();
                    dr["JCHID"] = JCHID;
                    dr["CTDID"] = CTDID;
                    dr["AMT"] = txtPayment.Text;
                    dtime = DateTime.ParseExact(txtpaymentdate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dr["PaymentDate"] = dtime.ToString("MM/dd/yyyy").Replace("-", "/");
                    dr["Remarks"] = txtremarks.Text;
                    dt.Rows.Add(dr);
                }
            }

            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.dt = dt;
            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);

            ds = objP.SaveContractorPaymentdetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Contractor Payment Details Saved Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            bindJobCardDetails();
            BindContractorPaymentDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        objP = new cProduction();
        string CPDID = "";
        DataSet ds = new DataSet();
        try
        {
            foreach (GridViewRow row in gvContractorPaymentDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                if (chk.Checked)
                {
                    if (CPDID == "")
                        CPDID = gvContractorPaymentDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        CPDID = CPDID + "," + gvContractorPaymentDetails.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }
            objP.CPDIDs = CPDID;
            objP.Flag = "Request";
            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
            ds = objP.UpdateContractorPaymentRequestStatusByPaymentID("LS_UpdateContractorPaymentRequestStatusByPaymentID");

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Payment Approval Requested Succesfully');", true);
            BindContractorPaymentDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPrimaryJobOrderDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PJOID = gvPrimaryJobOrderDetails.DataKeys[index].Values[0].ToString();
            hdnPJOID.Value = PJOID;

            hdnRFPDID.Value = gvPrimaryJobOrderDetails.DataKeys[index].Values[1].ToString().Split('/')[0];
            hdnEDID.Value = gvPrimaryJobOrderDetails.DataKeys[index].Values[1].ToString().Split('/')[1];

            Label lblItemName = (Label)gvPrimaryJobOrderDetails.Rows[index].FindControl("lblItemName");
            ViewState["ItemName"] = lblItemName.Text;
            if (e.CommandName == "AddJobOrder")
            {
                ViewState["PJOStatus"] = gvPrimaryJobOrderDetails.DataKeys[index].Values[2].ToString();

                ViewState["Command"] = "Job";
                BindPartDetails("AddJobOrder");

                BindPrimaryJobOrderPartCostDetails();

                //if (ViewState["PJOStatus"].ToString() == "Completed")
                //    divrateGroup.Visible = false;
                //else
                //    divrateGroup.Visible = true;
            }
            else if (e.CommandName == "pdf")
            {
                ViewState["Command"] = "Pdf";
                BindPartDetails("pdf");
            }
            else if (e.CommandName == "Payment")
            {
                LinkButton btn = (LinkButton)gvPrimaryJobOrderDetails.Rows[index].FindControl("lbtnJobNo");
                bindJobCardDetails();
                BindContractorPaymentDetails();
                divContractorPayment.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowJobCardDetailsPopUP();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPrimaryJobOrderDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnPDF = (LinkButton)e.Row.FindControl("lbtnPDF");
                if (dr["PJOID"].ToString() == "0")
                    lbtnPDF.CssClass = "aspNetDisabled";
                else
                    lbtnPDF.CssClass = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartNameDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnBellowsCard = (LinkButton)e.Row.FindControl("btnBellowsCard");
                Label lblPartName = (Label)e.Row.FindControl("lblPartName");
                Label lblCalWT = (Label)e.Row.FindControl("lblCalWT");

                CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");

                if (dr["PJORateType"].ToString().Split('/')[0] == "F")
                {
                    if (ViewState["Command"].ToString() == "Job")
                    {
                        btnBellowsCard.Visible = true;
                        lblPartName.Visible = false;
                    }
                    else if (ViewState["Command"].ToString() == "Pdf")
                    {
                        lblPartName.Visible = true;
                        btnBellowsCard.Visible = false;
                    }
                }
                else
                {
                    btnBellowsCard.Visible = false;
                    lblPartName.Visible = true;
                }

                if (dr["PRDID"].ToString() == "0")
                    chkQC.Visible = true;
                else
                    chkQC.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartNameDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        string Mode = "";
        try
        {
            rblTangetCuttingCost_b.ClearSelection();
            rblBellowFormingType_b.ClearSelection();

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string BOMID = gvPartNameDetails.DataKeys[index].Values[0].ToString();

            LinkButton btnBellowsCard = (LinkButton)gvPartNameDetails.Rows[index].FindControl("btnBellowsCard");

            lblItemName_b.Text = btnBellowsCard.Text + " / " + ViewState["ItemName"].ToString();

            ViewState["BOMID"] = BOMID;

            objP.BOMID = Convert.ToInt32(ViewState["BOMID"]);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.BellowFormType = "";
            ds = objP.GetBellowsCostDetailsByBOMID();

            if (ds.Tables[1].Rows.Count > 0)
                Mode = "edit";
            else
                Mode = "add";

            BindBellowsCostDetails(Mode, ds);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowBellowsPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvContractorPaymentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");

                if (dr["PaymentApprovalStatus"].ToString() == "0" || dr["PaymentApprovalStatus"].ToString() == "9")
                {
                    btnDelete.Visible = true;
                    chk.Visible = true;
                }
                else if (dr["PaymentApprovalStatus"].ToString() == "1" || dr["PaymentApprovalStatus"].ToString() == "7")
                {
                    btnDelete.Visible = false;
                    chk.Visible = false;
                }

                if (objSession.type == 1)
                    btnDelete.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPrimaryJobOrderPartCostDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "DeletePart")
            {
                string RFPDID = gvPrimaryJobOrderPartCostDetails.DataKeys[index].Values[0].ToString();
                string BOMID = gvPrimaryJobOrderPartCostDetails.DataKeys[index].Values[1].ToString();

                DeletePrimaryJobOrderPartCostDetailsByRFPDIDAndBOMID(RFPDID, BOMID);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPrimaryJobOrderPartCostDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (dr["PJOStatus"].ToString() == "Completed")
                    btnDelete.Visible = false;
                else
                    btnDelete.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common methods"

    //objP.CTDID = Convert.ToInt32(ddlContractorName.SelectedValue);
    //objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
    //ds = objP.GetContractorPaymentDetailsByCMIDAndCTDID();

    private void ddlCustomerNameAndRFPNo()
    {
        try
        {
            objc = new cCommon();
            objP = new cProduction();
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);

            objP.status = rblRFPChange.SelectedValue;
            dsCustomer = objP.getRFPCustomerNameByUserIDForPrimaryJoborder(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
            dsRFPHID = objP.GetRFPDetailsByUserIDForPrimaryJoborder(Convert.ToInt32(objSession.employeeid), ddlRFPNo);

            ViewState["RFPDetails"] = dsRFPHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorPaymentDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            objP.CTDID = Convert.ToInt32(ddlContractorName.SelectedValue);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);

            ds = objP.GetContractorPaymentDetailsByCMIDAndCTDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvContractorPaymentDetails.DataSource = ds.Tables[0];
                gvContractorPaymentDetails.DataBind();
            }
            else
            {
                gvContractorPaymentDetails.DataSource = "";
                gvContractorPaymentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindBellowsCostDetails(string Mode, DataSet ds)
    {
        try
        {
            lblNumberOfPly_b.Text = ds.Tables[0].Rows[0]["NOP"].ToString();
            lblThickness_b.Text = ds.Tables[0].Rows[0]["THK"].ToString();
            lblCalculatedWeight_b.Text = ds.Tables[0].Rows[0]["Weight"].ToString();
            lblSheetMarkingCuttingTotalCost_b.Text = ds.Tables[0].Rows[0]["TotalCost"].ToString();

            ViewState["BellowCalculatedWeight"] = ds.Tables[0].Rows[0]["Weight"].ToString();
            ViewState["SheetMarkingCost"] = ds.Tables[0].Rows[0]["TotalCost"].ToString();

            if (Mode == "edit")
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    rblBellowFormingType_b.SelectedValue = ds.Tables[1].Rows[0]["FormingType"].ToString();
                    rblTangetCuttingCost_b.SelectedValue = ds.Tables[1].Rows[0]["TangentType"].ToString();
                    lblFormTypeCost_b.Text = "Bellow Forming Total Cost (" + ds.Tables[1].Rows[0]["FormingType"].ToString() + "):" + ds.Tables[1].Rows[0]["FormingCost"].ToString();
                    lblTangetCuttingTotalCost_b.Text = "Tanget Cutting Total Cost (" + ds.Tables[1].Rows[0]["TangentType"].ToString() + "):" + ds.Tables[1].Rows[0]["TangentCost"].ToString();
                }
            }
            else if (Mode == "add")
            {
                if (rblBellowFormingType_b.SelectedValue == "Roll")
                    lblFormTypeCost_b.Text = "Bellow Forming Total Cost (Roll):" + ds.Tables[2].Rows[0]["TotalCost"].ToString();
                if (rblBellowFormingType_b.SelectedValue == "Expandal")
                    lblFormTypeCost_b.Text = "Bellow Forming Total Cost (Expandal):" + ds.Tables[2].Rows[0]["TotalCost"].ToString();
            }

            ViewState["FormingCost"] = ds.Tables[2].Rows[0]["TotalCost"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GeneratePDFFile()
    {
        objc = new cCommon();
        try
        {
            string MAXPDFID = "";
            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));           

            divLSERPLogo.Visible = false;

            divJoborderPdf.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();

            MAXPDFID = objc.GetMaximumNumberPDF();
            string htmlfile = "JobOrder" + "_" + MAXPDFID + ".html";
            string pdffile = "JobOrder" + "_" + MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            SaveHtmlFile(URL, "Primary Job Order Details", "", div);

            objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            divLSERPLogo.Visible = true;

            //  objc.SavePDFFile("EnquiryReviewCheckList.aspx", pdffile, _objSession.employeeid);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();


            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("");
            w.WriteLine("</title>");

            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");

            w.WriteLine("</head><body>");
            //w.WriteLine("<div class='col-sm-12' style='text-align:center;padding-top:10px;font-size:20px;font-weight:bold;'>");
            //w.WriteLine(lbltitle);
            //w.WriteLine("</div>");
            //w.WriteLine("<div>");
            w.WriteLine("<div class='page'>");
            w.WriteLine("<div class='col-sm-12' id='divLSERPLogo' style='padding-top: 30px;' runat='server'>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-8 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR INDUSTRIES </h3>");
            w.WriteLine("<div style='font-weight:700;color:#000;font-size:12px;'>" + Address + "</div>");
            w.WriteLine("<div style='font-weight:700;color:#000;font-size:12px'>" + PhoneAndFaxNo + "</div>");
            w.WriteLine("<div style='font-weight:700;color:#000;font-size:12px'>" + Email + "</div>");
            w.WriteLine("<div style='font-weight:700;color:#000;font-size:12px'>" + WebSite + "</div>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12 p-t-10'>");
            w.WriteLine(div);
            w.WriteLine("</div></div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPrimaryJobOrderDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.status = rblRFPChange.SelectedValue;
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objP.GetPrimaryJobOrderDetailsByRFPHID();

            ViewState["PrimaryJobOrderDetails"] = ds.Tables[0];
            ViewState["Address"] = ds.Tables[1];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPrimaryJobOrderDetails.DataSource = ds.Tables[0];
                gvPrimaryJobOrderDetails.DataBind();
            }
            else
            {
                gvPrimaryJobOrderDetails.DataSource = "";
                gvPrimaryJobOrderDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindJobCardDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            //ds = objP.bindJobCardDetailsByRFPDID(ddlContractorName);
            ds = objP.bindJobCardActualExpensesDetails(ddlContractorName);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobCardProcessDetails.DataSource = ds.Tables[0];
                gvJobCardProcessDetails.DataBind();
            }
            else
            {
                gvJobCardProcessDetails.DataSource = "";
                gvJobCardProcessDetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvAssemplyJobCard.DataSource = ds.Tables[1];
                gvAssemplyJobCard.DataBind();
            }
            else
            {
                gvAssemplyJobCard.DataSource = "";
                gvAssemplyJobCard.DataBind();
            }

            // lblPJOID_PM.Text = ViewState["ItemName"].ToString() + " / " + ds.Tables[0].Rows[0]["PJONumber"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divOutput.Visible = false;
        try
        {
            string[] mode = divids.Split(',');
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetails(string CommandName)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            DataTable dt;
            dt = (DataTable)ViewState["PrimaryJobOrderDetails"];
            dt.DefaultView.RowFilter = "PJOID='" + hdnPJOID.Value + "'";

            objP.EDID = Convert.ToInt32(hdnEDID.Value);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

            ds = objP.GetPartDetailsByEDIDAndRFPHIDAndRFPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartNameDetails.DataSource = ds.Tables[0];
                gvPartNameDetails.DataBind();
            }
            else
            {
                gvPartNameDetails.DataSource = "";
                gvPartNameDetails.DataBind();
            }

            if (CommandName == "AddJobOrder")
            {
                hdnItemQTY.Value = dt.DefaultView.ToTable().Rows[0]["QTY"].ToString();

                //objP.GetRateGroupingMasterDetails(ddlRateGroup);

                //if (ds.Tables[2].Rows.Count > 0)
                //    lblUnitAWTWeight.Text = ds.Tables[2].Rows[0]["UnitAWTWeight"].ToString();
                //if (ds.Tables[1].Rows.Count > 0)
                //    lblUnitCalculatedWeight.Text = ds.Tables[1].Rows[0]["UnitCalculatedWeight"].ToString() + "Kgs";

                //lblItemName_P.Text = ViewState["ItemName"].ToString();

                //if (hdnPJOID.Value != "0")
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        ddlRateGroup.SelectedValue = dt.DefaultView.ToTable().Rows[0]["RGMID"].ToString();
                //        txtRemarks.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
                //        lblUnitRate.Text = dt.DefaultView.ToTable().Rows[0]["UnitRate"].ToString();
                //    }
                //}
                //else
                //{
                //    ddlRateGroup.SelectedValue = "0";
                //    txtRemarks.Text = "";
                //    lblUnitRate.Text = "";
                //}

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "PartDetailsShowPopUp();", true);
            }

            if (CommandName == "pdf")
            {
                lblJobOrderID.Text = dt.DefaultView.ToTable().Rows[0]["JobOrderID"].ToString();
                lblDate.Text = dt.DefaultView.ToTable().Rows[0]["JobDate"].ToString();
                lblRFPNo.Text = ddlRFPNo.SelectedItem.Text;
                lblItemName.Text = ViewState["ItemName"].ToString();

                gvPartNameDetails.Columns[3].Visible = false;
                gvPartNameDetails.Columns[4].Visible = false;

                var sb = new StringBuilder();
                gvPartNameDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                string div = sb.ToString();
                //string div = divPrintReceiptDetails1.InnerHtml;
                gvPartNameDetails.Columns[3].Visible = true;
                gvPartNameDetails.Columns[4].Visible = true;

                divPartDetailsContent.InnerHtml = div;

                lblItemQuantity.Text = dt.DefaultView.ToTable().Rows[0]["QTY"].ToString();
                lblUnitWeight.Text = dt.DefaultView.ToTable().Rows[0]["UnitCalculatedWeight"].ToString();
                lblTotalWeight_P.Text = dt.DefaultView.ToTable().Rows[0]["TotalUnitCaculateWeight"].ToString();
                lblTotalRate_P.Text = dt.DefaultView.ToTable().Rows[0]["TotalUnitRate"].ToString();
                lblUnitRate_P.Text = dt.DefaultView.ToTable().Rows[0]["UnitRate"].ToString();
                lblRemarks.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowViewPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void BindPrimaryJobOrderPartCostDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.GetPrimaryJoborderPartCostDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPrimaryJobOrderPartCostDetails.DataSource = ds.Tables[0];
                gvPrimaryJobOrderPartCostDetails.DataBind();
            }
            else
            {
                gvPrimaryJobOrderPartCostDetails.DataSource = "";
                gvPrimaryJobOrderPartCostDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeletePrimaryJobOrderPartCostDetailsByRFPDIDAndBOMID(string RFPDID, string BOMID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(RFPDID);
            objP.BOMID = Convert.ToInt32(BOMID);
            ds = objP.DeletePrimaryJobOrderPartCostDetailsByRFPDIDAndBOMID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Part rate Deleted SuccessFully');", true);
            BindPartDetails("");
            BindPrimaryJobOrderPartCostDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvPartNameDetails.Rows.Count > 0)
            gvPartNameDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvPrimaryJobOrderDetails.Rows.Count > 0)
            gvPrimaryJobOrderDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvJobCardProcessDetails.Rows.Count > 0)
            gvJobCardProcessDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvPrimaryJobOrderPartCostDetails.Rows.Count > 0)
            gvPrimaryJobOrderPartCostDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion   

}