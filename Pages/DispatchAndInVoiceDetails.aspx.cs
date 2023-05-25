using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using eplus.data;
using System.Text;
using System.Configuration;
using System.IO;
using System.Globalization;
using SelectPdf;

public partial class Pages_DispatchAndInVoiceDetails : System.Web.UI.Page
{

    #region"Declaration"
    cSession objSession = new cSession();
    cCommon objc;
    cCommonMaster objCommon;
    cSales objSales;
    cPurchase objPc;
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
                objCommon = new cCommonMaster();

                rblDispatchType.SelectedValue = "D";
                divinterBasciinfo.Visible = false;
                divdomesticBasicInfo.Visible = true;

                objc.GetLocationDetails(ddlLocation_I);
                objc.GetLocationDetails(ddlLocation_d);
                objCommon.GetCurrencyName(ddlCurrency_I);
                bindDespatchAndInvoiceDetails();
            }
            if (target == "deleteinvoiceitemrow")
            {
                DataSet ds = new DataSet();
                objCommon = new cCommonMaster();
                objCommon.RIIDID = Convert.ToInt32(arg.ToString());

                ds = objCommon.DeleteDispatchAndInvoiceItemDetailsByRIIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','item Deleted Successfuly');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','item Deleted Successfuly');", true);
            }

            if (target == "deletegvRowotherCharges")
            {
                DataSet ds = new DataSet();
                objCommon = new cCommonMaster();
                objCommon.ChargesID = Convert.ToInt32(arg);
                objCommon.PageNameFlag = "othercharges";

                ds = objCommon.DeleteInvoiceChargesDetailsByChargesID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');ShowTaxPopUp();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                BindOtherChargesDetails();

            }
            if (target == "deletegvRowtaxCharges")
            {
                DataSet ds = new DataSet();
                objCommon = new cCommonMaster();
                objCommon.ChargesID = Convert.ToInt32(arg);
                objCommon.PageNameFlag = "tax";

                ds = objCommon.DeleteInvoiceChargesDetailsByChargesID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');ShowTaxPopUp();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                BindTaxChargesDetails();
            }

            if (target == "deleteDIDID")
            {
                DataSet ds = new DataSet();
                objCommon = new cCommonMaster();
                objCommon.DIDID = Convert.ToInt32(arg);
                ds = objCommon.DeleteInvoiceByDIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                bindDespatchAndInvoiceDetails();
            }

            if (target == "PrintInvoice")
                bindInvoicePDFDetails(arg.ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

    protected void rblDispatchType_OnSelectindexchganged(object sender, EventArgs e)
    {
        try
        {
            if (rblDispatchType.SelectedValue == "D")
            {
                divdomesticBasicInfo.Visible = true;
                divinterBasciinfo.Visible = false;
            }
            else
            {
                divdomesticBasicInfo.Visible = false;
                divinterBasciinfo.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button EVents"

    protected void btnsaveInvoice_Click(object sender, EventArgs e)
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);

            if (rblDispatchType.SelectedValue == "D")
            {
                objCommon.InvoiceNoPrefix = ddlInvoiceNoType_d.SelectedValue;
                objCommon.InVoiceType = ddlInvoicetype_d.SelectedValue;
                objCommon.InvoiceDate = DateTime.ParseExact(txtInvoicedate_d.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCommon.HSNno = txtHSNNo_d.Text;
                objCommon.Payment = txtPayment_d.Text;
                objCommon.TransportationMode = txttransportmode_d.Text;
                objCommon.LocationID = Convert.ToInt32(ddlLocation_d.SelectedValue);
                objCommon.TransporterName = txttransportername_d.Text;
                objCommon.Freight = txtFreight_d.Text;
                objCommon.LRNo = txtLR_d.Text;
                objCommon.vechileNo = txtvehicleNo_d.Text;
                objCommon.TimeOfSupply = "";
                objCommon.Placeofsupply = txtplaceofsupply_d.Text;
                objCommon.Remarks = txtRemarks_d.Text;
                objCommon.InvoiceNo = txtinvoiceNo_d.Text;
                objCommon.ElectronicReferenceNo = txtelectronicreferenceNo_d.Text;
            }
            else
            {
                objCommon.InvoiceNoPrefix = ddlinvoiceNotype_I.SelectedValue;
                objCommon.InVoiceType = ddlInvoicetype_I.SelectedValue;
                objCommon.InvoiceDate = DateTime.ParseExact(txtInvoicedate_I.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCommon.HSNno = txtHSNNo_I.Text;
                objCommon.Payment = txtPayment_I.Text;
                objCommon.TransportationMode = txttransportmode_I.Text;
                objCommon.LocationID = Convert.ToInt32(ddlLocation_I.SelectedValue);
                objCommon.Remarks = txtRemarks_I.Text;
                objCommon.InvoiceNo = txtInvoiceNo_I.Text;
                objCommon.ElectronicReferenceNo = txtElectornicReferenceNo_I.Text;
            }

            objCommon.BuyerOrderNo = txtBuyerOrderNo_I.Text;
            objCommon.BuyerDate = txtBuyerDate_I.Text;
            objCommon.PreCarriagedBy = txtpriCarriagedBy_I.Text;
            objCommon.PlaceOfReceipt = txtPlaceOfReceipt_I.Text;
            objCommon.PortLoading = txtPortLoading_I.Text;
            objCommon.PortOfDischarge = txtPortOfDischarge_I.Text;
            objCommon.VesselFlightNo = txtvesselFlightNo_I.Text;
            objCommon.CountryOfOrginGoods = txtcountryoforgingoods_I.Text;
            objCommon.CountryOfFinalDestination = txtCountryofFinaldestination_I.Text;
            objCommon.KindPackage = txtkindpackage_I.Text;
            objCommon.CurrencyID = Convert.ToInt32(ddlCurrency_I.SelectedValue);

            objCommon.BillingName = txtBillingName.Text;
            objCommon.BillingAddress = txtBillingAddress.Text;
            objCommon.BillingState = txtbillingstate.Text;
            objCommon.BillingStateCode = txtBillingstatecode.Text;
            objCommon.BillingGSTINNo = txtBillingGSTINNo.Text;
            objCommon.BillingPONO = txtBillingPONo.Text;
            objCommon.Consigneename = txtConsigneename.Text;
            objCommon.ConsigneeAddress = txtconsigneeaddress.Text;
            objCommon.Consigneestate = txtconsigneestate.Text;
            objCommon.Consigneestatecode = txtconsigneestatecode.Text;
            objCommon.ConsigneeGSTINNo = txtconsigneeGSTINo.Text;
            objCommon.ConsigneePONo = txtconsigneepoNo.Text;
            objCommon.CreatedBy = objSession.employeeid;
            objCommon.ConsigneePANNo = txtconsigneePANNo.Text;
            objCommon.BiilingPANNo = txtBillingPANNo.Text;

            objCommon.DespatchType = rblDispatchType.SelectedValue;

            ds = objCommon.SaveDispatchAndInvoiceDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Enquiry Details  Added successfully');ClearFields();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Enquiry Details  Updated successfully');ClearFields();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        bindDespatchAndInvoiceDetails();
        hdnDIDID.Value = "0";
    }

    protected void btncancelinvoice_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveRFPInvoiceItem_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.RIIDID = Convert.ToInt32(hdnRIIDID.Value);
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);
            objCommon.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objCommon.RFPDID = Convert.ToInt32(ddlitemname.SelectedValue);
            objCommon.invoiceQty = txtinvoiceqty.Text;
            objCommon.Description = txtdescription.Text;
            objCommon.CreatedBy = objSession.employeeid;
            ds = objCommon.SaveRFPInvoiceItemDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RFP Invoice Item details Saved Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            bindInvoiceItemDetails();
            ddlitemname_OnSelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlcustomername_OnSelectindexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["CustomerDetails"];
            if (ddlcustomername.SelectedIndex > 0)
            {
                string ProspectID = ddlcustomername.SelectedValue;
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

    protected void ddlRFPNo_OnSelectindexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        DataTable dt;
        try
        {
            objCommon.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objCommon.GetInvoiceItemDetailsByRFPHID(ddlitemname);

            dt = (DataTable)ViewState["CustomerDetails"];
            //if (ddlcustomername.SelectedIndex > 0)
            //{
            ////string RFPHID = ddlRFPNo.SelectedValue;
            ////dt.DefaultView.RowFilter = "RFPHID='" + RFPHID + "'";
            ////dt.DefaultView.ToTable();
            // }

            string RFPHID = ddlRFPNo.SelectedValue;
            DataView dv = new DataView(dt);

            dv.RowFilter = "RFPHID='" + RFPHID + "'";
            dt = dv.ToTable();

            ddlcustomername.SelectedValue = dt.Rows[0]["ProspectID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlitemname_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.RFPDID = Convert.ToInt32(ddlitemname.SelectedValue);
            ds = objCommon.GetInvoiceItemQtyDetailsByRFPDID();

            lblTotalItemQty.Text = "Total Item QTY - " + ds.Tables[0].Rows[0]["RFPItemQty"].ToString();
            lblDispatchQty.Text = "Dispatch Item QTY - " + ds.Tables[0].Rows[0]["ItemQty"].ToString();
            lblunitrate.Text = hdnunitrate.Value = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
            hdndispatchqty.Value = ds.Tables[0].Rows[0]["ItemQty"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveTaxAndOtherCharges_Click(object sender, EventArgs e)
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;
            if (CommandName == "othercharges")
            {
                objCommon.ChargesID = Convert.ToInt32(ddlOtherCharges_t.SelectedValue);
                objCommon.ChargesValue = Convert.ToDecimal(txtOtherCharges_t.Text);
            }
            else if (CommandName == "tax")
            {
                objCommon.ChargesID = Convert.ToInt32(ddlTax_t.SelectedValue);
                objCommon.ChargesValue = Convert.ToDecimal(txtTaxValue_t.Text);
            }
            objCommon.ChargesType = CommandName;
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);
            ds = objCommon.SaveInvoiceTaxAndOtherChargesDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','" + CommandName + " Saved Successfully');ShowTaxPopUp();", true);
                if (CommandName == "tax")
                    BindTaxChargesDetails();
                else
                    BindOtherChargesDetails();
                BindPOTotalAmount();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvDespatchAndInvoiceDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            hdnDIDID.Value = gvDespatchAndInvoiceDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "EditDespatchDetails")
            {
                DataSet ds = new DataSet();
                objCommon = new cCommonMaster();

                objCommon.DIDID = Convert.ToInt32(gvDespatchAndInvoiceDetails.DataKeys[index].Values[0].ToString());

                ds = objCommon.GetDespatchAndInvoiceDetailsByDIDID();

                hdnDIDID.Value = ds.Tables[0].Rows[0]["DIDID"].ToString();
                ddlInvoiceNoType_d.SelectedValue = ds.Tables[0].Rows[0]["InvoiceNoPrefix"].ToString();
                ddlInvoicetype_d.SelectedValue = ds.Tables[0].Rows[0]["InVoiceType"].ToString();
                txtInvoicedate_d.Text = ds.Tables[0].Rows[0]["InvoiceDate"].ToString();
                txtHSNNo_d.Text = ds.Tables[0].Rows[0]["HSNno"].ToString();
                txtPayment_d.Text = ds.Tables[0].Rows[0]["Payment"].ToString();
                txttransportmode_d.Text = ds.Tables[0].Rows[0]["TransportationMode"].ToString();
                ddlLocation_d.SelectedValue = ds.Tables[0].Rows[0]["LocationID"].ToString();
                txttransportername_d.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();
                txtFreight_d.Text = ds.Tables[0].Rows[0]["Freight"].ToString();
                txtLR_d.Text = ds.Tables[0].Rows[0]["LRNo"].ToString();
                txtvehicleNo_d.Text = ds.Tables[0].Rows[0]["vechileNo"].ToString();
                //txttimeOfSupply_d.Text = ds.Tables[0].Rows[0]["TimeOfSupply"].ToString();
                txtplaceofsupply_d.Text = ds.Tables[0].Rows[0]["Placeofsupply"].ToString();
                txtRemarks_d.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                ddlinvoiceNotype_I.SelectedValue = ds.Tables[0].Rows[0]["InvoiceNoPrefix"].ToString();
                ddlInvoicetype_I.SelectedValue = ds.Tables[0].Rows[0]["InVoiceType"].ToString();
                txtInvoicedate_I.Text = ds.Tables[0].Rows[0]["InvoiceDate"].ToString();
                txtHSNNo_I.Text = ds.Tables[0].Rows[0]["HSNno"].ToString();
                txtPayment_I.Text = ds.Tables[0].Rows[0]["Payment"].ToString();
                txttransportmode_I.Text = ds.Tables[0].Rows[0]["TransportationMode"].ToString();
                ddlLocation_I.SelectedValue = ds.Tables[0].Rows[0]["LocationID"].ToString();
                txtRemarks_I.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtBuyerOrderNo_I.Text = ds.Tables[0].Rows[0]["BuyerOrderNo"].ToString();
                txtBuyerDate_I.Text = ds.Tables[0].Rows[0]["BuyerDate"].ToString();
                txtpriCarriagedBy_I.Text = ds.Tables[0].Rows[0]["PreCarriagedBy"].ToString();
                txtPlaceOfReceipt_I.Text = ds.Tables[0].Rows[0]["PlaceOfReceipt"].ToString();
                txtPortLoading_I.Text = ds.Tables[0].Rows[0]["PortLoading"].ToString();
                txtPortOfDischarge_I.Text = ds.Tables[0].Rows[0]["PortOfDischarge"].ToString();
                txtvesselFlightNo_I.Text = ds.Tables[0].Rows[0]["VesselFlightNo"].ToString();
                txtcountryoforgingoods_I.Text = ds.Tables[0].Rows[0]["CountryOfOrginGoods"].ToString();
                txtCountryofFinaldestination_I.Text = ds.Tables[0].Rows[0]["CountryOfFinalDestination"].ToString();
                txtkindpackage_I.Text = ds.Tables[0].Rows[0]["KindPackage"].ToString();
                ddlCurrency_I.SelectedValue = ds.Tables[0].Rows[0]["CurrencyID"].ToString();
                txtBillingName.Text = ds.Tables[0].Rows[0]["BillingName"].ToString();
                txtBillingAddress.Text = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
                txtbillingstate.Text = ds.Tables[0].Rows[0]["BillingState"].ToString();
                txtBillingstatecode.Text = ds.Tables[0].Rows[0]["BillingStateCode"].ToString();
                txtBillingGSTINNo.Text = ds.Tables[0].Rows[0]["BillingGSTINNo"].ToString();
                txtBillingPONo.Text = ds.Tables[0].Rows[0]["BillingPONO"].ToString();
                txtConsigneename.Text = ds.Tables[0].Rows[0]["Consigneename"].ToString();
                txtconsigneeaddress.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                txtconsigneestate.Text = ds.Tables[0].Rows[0]["Consigneestate"].ToString();
                txtconsigneestatecode.Text = ds.Tables[0].Rows[0]["Consigneestatecode"].ToString();
                txtconsigneeGSTINo.Text = ds.Tables[0].Rows[0]["ConsigneeGSTINNo"].ToString();
                txtconsigneepoNo.Text = ds.Tables[0].Rows[0]["ConsigneePONo"].ToString();
                rblDispatchType.SelectedValue = ds.Tables[0].Rows[0]["DespathcType"].ToString();

                txtElectornicReferenceNo_I.Text = ds.Tables[0].Rows[0]["ElectronicReferenceNo"].ToString();
                txtconsigneePANNo.Text = ds.Tables[0].Rows[0]["ConsignePANNo"].ToString();
                txtBillingPANNo.Text = ds.Tables[0].Rows[0]["BillingPANNo"].ToString();
            }

            if (e.CommandName.ToString() == "viewDispatchRFPDetails")
            {
                Label lblinvoiceno = (Label)gvDespatchAndInvoiceDetails.Rows[index].FindControl("lblInvoiceNo");
                lblinvoiceno_p.Text = lblinvoiceno.Text;

                bindRFPNoDetails();
                bindInvoiceItemDetails();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "showdispatchpopup();", true);
            }

            if (e.CommandName == "AddTax")
            {
                objPc = new cPurchase();
                objPc.BindChargesDetails(ddlOtherCharges_t, ddlTax_t);

                Label lblinvoiceno = (Label)gvDespatchAndInvoiceDetails.Rows[index].FindControl("lblInvoiceNo");
                lblinvoiceno_p.Text = lblinvoiceno.Text;

                BindTaxChargesDetails();
                BindOtherChargesDetails();
                BindPOTotalAmount();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowTaxPopUp();OpenTab('OtherCharges');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvinvoiceItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDispatchItemDetails")
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindDespatchAndInvoiceDetails()
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            ds = objCommon.GetDespatchAndInvoiceDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDespatchAndInvoiceDetails.DataSource = ds.Tables[0];
                gvDespatchAndInvoiceDetails.DataBind();
            }
            else
            {
                gvDespatchAndInvoiceDetails.DataSource = "";
                gvDespatchAndInvoiceDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindRFPNoDetails()
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            ds = objCommon.GetReadyToDispatchRFPNODetails(ddlRFPNo, ddlcustomername);

            ViewState["RFPDetails"] = ds.Tables[0];
            ViewState["CustomerDetails"] = ds.Tables[1];

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindInvoiceItemDetails()
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);
            ds = objCommon.GetInvoiceItemDetailsByDIDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvinvoiceItemDetails.DataSource = ds.Tables[0];
                gvinvoiceItemDetails.DataBind();
            }
            else
            {
                gvinvoiceItemDetails.DataSource = "";
                gvinvoiceItemDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindTaxChargesDetails()
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);
            ds = objCommon.GetInvoiceTaxAndOtherChargesDetails("LS_GetInvoiceTaxChargesDetailsByDIDID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvTaxDetails.DataSource = ds.Tables[0];
                gvTaxDetails.DataBind();
            }
            else
            {
                gvTaxDetails.DataSource = "";
                gvTaxDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPOTotalAmount()
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);
            ds = objCommon.GetInvoiceTaxAndOtherChargesDetails("LS_InvoiceTotalAmountWithOtherChargesByDIDID");

            lblPOAmount_T.Text = "Total PO Amount : " + ds.Tables[0].Rows[0]["POAmountWithOtherCharges"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindOtherChargesDetails()
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.DIDID = Convert.ToInt32(hdnDIDID.Value);
            ds = objCommon.GetInvoiceTaxAndOtherChargesDetails("LS_GetInvoiceOtherChargesDetailsByDIDID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierPOOtherCharges.DataSource = ds.Tables[0];
                gvSupplierPOOtherCharges.DataBind();
            }
            else
            {
                gvSupplierPOOtherCharges.DataSource = "";
                gvSupplierPOOtherCharges.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindInvoicePDFDetails(string DIDID)
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        string Tax = "";
        string OtherCharges = "";
        try
        {
            objCommon.DIDID = Convert.ToInt32(DIDID);
            ds = objCommon.GetInvoicePDFDetailsByDIDID();

            lblInvoiceNo_d.Text = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
            lblinvoicedate_d.Text = ds.Tables[0].Rows[0]["InvoiceDate"].ToString();
            lblhsnNo_d.Text = ds.Tables[0].Rows[0]["HSNno"].ToString();
            lbltransportationmode_d.Text = ds.Tables[0].Rows[0]["TransportationMode"].ToString();
            lblvechileNo_d.Text = ds.Tables[0].Rows[0]["vechileNo"].ToString();
            lblplaceofsupply_d.Text = ds.Tables[0].Rows[0]["Placeofsupply"].ToString();
            lbltransportername_d.Text = ds.Tables[0].Rows[0]["TransporterName"].ToString();
            lblfright_d.Text = ds.Tables[0].Rows[0]["Freight"].ToString();
            lblpayment_d.Text = ds.Tables[0].Rows[0]["Payment"].ToString();

            lblbillingname_d.Text = ds.Tables[0].Rows[0]["BillingName"].ToString();
            lblbillingaddress_d.Text = ds.Tables[0].Rows[0]["BillingAddress"].ToString();
            lblbillingstate_d.Text = ds.Tables[0].Rows[0]["BillingState"].ToString();
            lblbillinggstinNo_d.Text = ds.Tables[0].Rows[0]["BillingGSTINNo"].ToString();
            lblbillingPONo_d.Text = ds.Tables[0].Rows[0]["BillingPONO"].ToString();

            lblconsigneename_d.Text = ds.Tables[0].Rows[0]["Consigneename"].ToString();
            lblconsigeeaddress_d.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
            lblconsigneestate_d.Text = ds.Tables[0].Rows[0]["Consigneestate"].ToString();
            lblConsigneeGSTINNo_d.Text = ds.Tables[0].Rows[0]["ConsigneeGSTINNo"].ToString();
            lblConsigneePoNo_d.Text = ds.Tables[0].Rows[0]["ConsigneePONo"].ToString();

            lblbillingstatecode_d.Text = ds.Tables[0].Rows[0]["BillingStateCode"].ToString();
            lblconsigneestatecode_d.Text = ds.Tables[0].Rows[0]["Consigneestatecode"].ToString();

            gvinvoiceinvoiceDescription_d.DataSource = ds.Tables[1];
            gvinvoiceinvoiceDescription_d.DataBind();

            foreach (DataRow dr in ds.Tables[2].Rows)
            {
                if (OtherCharges == "")
                    OtherCharges = dr["ChargesName"].ToString() + "#" + dr["Value"].ToString();
                else
                    OtherCharges = OtherCharges + "/" + dr["ChargesName"].ToString() + "#" + dr["Value"].ToString();
            }

            foreach (DataRow dr in ds.Tables[3].Rows)
            {
                if (Tax == "")
                    Tax = dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % " + "#" + dr["Value"].ToString();
                else
                    Tax = Tax + "/" + dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % " + "#" + dr["Value"].ToString();
            }

            lblsubtotal_d.Text = ds.Tables[4].Rows[0]["SubTotal"].ToString();
            lbltotalamount_d.Text = ds.Tables[4].Rows[0]["TotalAmount"].ToString();
            lblamountinwords_d.Text = ds.Tables[4].Rows[0]["RupeesInWords"].ToString();

            lbltaxablevalue_d.Text = ds.Tables[4].Rows[0]["TaxableValue"].ToString();
            lblinvoiceitemqty_d.Text = ds.Tables[4].Rows[0]["TotalInvoiceQty"].ToString();

            lblAddress_h.Text = ds.Tables[5].Rows[0]["Address"].ToString();
            lblPhoneAndFaxNo_h.Text = ds.Tables[5].Rows[0]["PhoneAndFaxNo"].ToString();
            lblEmail_h.Text = ds.Tables[5].Rows[0]["Email"].ToString();
            lblwebsite_h.Text = ds.Tables[5].Rows[0]["WebSite"].ToString();

            lblfactoryaddress_d.Text = ds.Tables[5].Rows[0]["FactoryAddress"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InvoicePrint('" + OtherCharges + "','" + Tax + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion      

}
