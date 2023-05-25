using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Text;

public partial class Pages_SupplierPO : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cCommonMaster objcommon;
    c_Finance objFin;
    cSales objSales;
    cMaterials objMat;
    cStores objSt;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
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
                objPc = new cPurchase();
                objc = new cCommon();
                objcommon = new cCommonMaster();
                objFin = new c_Finance();
                //objPc.QHID = Convert.ToInt32(ddlPIIndentNumber.SelectedValue);
                objPc.GetCategoryNameByListByApprovedIndentNumber(ddlMaterialCategory);
                //objPc.GetSupplierDetails(ddlSuplierName);
                objc.GetLocationDetails(ddlLocationName);
                objcommon.GetCurrencyName(ddlCurrencyname);
                // objFin.GetTaxNameDetails(ddlTaxName);
                ShowHideControls("add");
            }
            if (target == "DeleteSPO")
            {
                DataSet ds = new DataSet();
                cCommonMaster objcommon = new cCommonMaster();

                int index = Convert.ToInt32(arg.ToString());

                objcommon.SPOID = Convert.ToInt32(gvSupplierPODetails.DataKeys[index].Values[0].ToString());

                ds = objcommon.DeleteSupplierPOBySPOID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Item Deleted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                hdnSPODID.Value = "0";
                BindSupplierPOItemDetails();
                BindSupplierPODetails();
            }

            if (target == "deletegvrowSPODID")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();

                objPc.SPODID = Convert.ToInt32(arg);
                objPc.PageNameFlag = "Delete";
                ds = objPc.DeleteSupplierPOItemDetailsBySPODID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Item Deleted Successfully');ShowAddPopUp();OpenTab('Item');", true);

                hdnSPODID.Value = "0";
                BindSupplierPOItemDetails();
                BindIndentDetails();
            }
            if (target == "CancelgvrowSPODID")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                objPc.PageNameFlag = "Cancel";
                objPc.SPODID = Convert.ToInt32(arg);

                ds = objPc.DeleteSupplierPOItemDetailsBySPODID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Item Cancelled Successfully');ShowAddPopUp();OpenTab('Item');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAddPopUp();OpenTab('Item');", true);

                hdnSPODID.Value = "0";
                BindSupplierPOItemDetails();
            }
            if (target == "deletegvRowotherCharges")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                objPc.ChargesID = Convert.ToInt32(arg);
                objPc.PageNameFlag = "othercharges";

                ds = objPc.DeleteSupplierPOChargesDetailsByChargesID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');ShowTaxPopUp();", true);
                BindOtherChargesDetails();

            }
            if (target == "deletegvRowtaxCharges")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                objPc.ChargesID = Convert.ToInt32(arg);
                objPc.PageNameFlag = "tax";

                ds = objPc.DeleteSupplierPOChargesDetailsByChargesID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');ShowTaxPopUp();", true);
                BindTaxChargesDetails();
            }

            if (target == "PrintPO")
                bindPurchaseOrderPDFDetails(Convert.ToInt32(arg.ToString()));
            if (target == "POAMD")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                int index = Convert.ToInt32(arg.ToString());
                objPc.SPOID = Convert.ToInt32(gvSupplierPODetails.DataKeys[index].Values[0].ToString());
                objPc.CreatedBy = objSession.employeeid;
                objPc.AmendmentReason = txtamdreason.Text;
                ds = objPc.UpdatePOAmendMentsBySPOID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Revision Changed Succesfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"radio Events"

    //protected void rblpurchase_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objPc = new cPurchase();
    //    try
    //    {
    //        if (rblpurchase.SelectedValue == "G")
    //        {
    //            divgvPurchase.Visible = true;

    //            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
    //            objPc.type = "General";

    //            ds = objPc.getPurchaseIndentDetailsBySPOIDAndType();

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                gvPurchaseIndentDetails.DataSource = ds.Tables[0];
    //                gvPurchaseIndentDetails.DataBind();
    //            }
    //            else
    //            {
    //                gvPurchaseIndentDetails.DataSource = "";
    //                gvPurchaseIndentDetails.DataBind();
    //            }
    //            divRFPNo.Visible = false;
    //        }
    //        else
    //        {
    //            divRFPNo.Visible = true;
    //            ddlRFPNo.SelectedIndex = 0;
    //            divgvPurchase.Visible = false;
    //        }

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAdditemdetailspopup();", true);
    //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"DropDown Events"

    protected void ddlSuplierName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        try
        {
            if (ddlSuplierName.SelectedIndex > 0)
            {
                objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
                objPc.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
                //   objPc.GetMaterialGradeNameByCIDAndSUPID(ddlmaterialGrdae);

                BindSupplierPODetails();
                ShowHideControls("add,aDdNew,VIEW");
            }
            else
            {
                objc = new cCommon();
                //objc.EmptyDropDownList(ddlmaterialGrdae);
                //objc.EmptyDropDownList(ddlThickness);
                //lblQN.Text = "";
                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMaterialCategory_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        objc = new cCommon();
        try
        {
            if (ddlMaterialCategory.SelectedIndex > 0)
                objPc.GetSupplierDetailsByCategoryName(ddlSuplierName, "LS_GetSupplierDetailsByCategoryNameAndQuoteApproved");
            else
                objc.EmptyDropDownList(ddlSuplierName);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMaterialType_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        //objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialType.SelectedIndex > 0)
                BindmaterialTypeFields("add", Convert.ToInt32(hdnSPODID.Value));

            else divMTFields.InnerHtml = "";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void ddlRFNo_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    objPc = new cPurchase();
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        if (ddlRFPNo.SelectedIndex > 0)
    //        {
    //            divgvPurchase.Visible = true;
    //            objPc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
    //            objPc.type = "RFP";
    //            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);

    //            ds = objPc.getPurchaseIndentDetailsBySPOIDAndType();

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                gvPurchaseIndentDetails.DataSource = ds.Tables[0];
    //                gvPurchaseIndentDetails.DataBind();
    //            }
    //            else
    //            {
    //                gvPurchaseIndentDetails.DataSource = "";
    //                gvPurchaseIndentDetails.DataBind();
    //            }
    //        }
    //        else
    //            divgvPurchase.Visible = false;

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAdditemdetailspopup();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"Check Box Events"

    protected void chkitems_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblTotalCost.Text = "0";
            lblrequiredweight.Text = "0";

            CheckBox ch = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)ch.Parent.Parent;

            int Count = 0;

            string MGMID = "0";
            string THKID = "0";

            bool msg = true;

            foreach (GridViewRow row in gvIndentDetails.Rows)
            {
                TextBox txtReqqty = (TextBox)gvIndentDetails.Rows[row.RowIndex].FindControl("txtReqWeight");
                TextBox txtUnitCost = (TextBox)gvIndentDetails.Rows[row.RowIndex].FindControl("txtUnitCost");
                CheckBox chk = (CheckBox)gvIndentDetails.Rows[row.RowIndex].FindControl("chkitems");

                string MGMIDNew = gvIndentDetails.DataKeys[row.RowIndex].Values[1].ToString();
                string THKIDNew = gvIndentDetails.DataKeys[row.RowIndex].Values[2].ToString();

                if (chk.Checked)
                {
                    if (Count == 0)
                    {
                        MGMID = gvIndentDetails.DataKeys[row.RowIndex].Values[1].ToString();
                        THKID = gvIndentDetails.DataKeys[row.RowIndex].Values[2].ToString();
                        msg = true;
                    }
                    else if (MGMID == MGMIDNew && THKID == THKIDNew)
                        msg = true;
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "ErrorMessage('ERROR','CLUBBING IS WRONG')", true);
                        msg = false;
                        chk.Checked = false;
                    }

                    if (msg)
                    {
                        lblTotalCost.Text = Convert.ToString(Convert.ToDecimal(lblTotalCost.Text) + (Convert.ToDecimal(txtReqqty.Text) * Convert.ToDecimal(txtUnitCost.Text)));
                        lblrequiredweight.Text = Convert.ToString(Convert.ToDecimal(lblrequiredweight.Text) + (Convert.ToDecimal(txtReqqty.Text)));
                    }
                    else
                        break;

                    Count++;
                }
            }

            if (ddlMaterialType.SelectedIndex == 0)
            {
                string PID = gvIndentDetails.DataKeys[gr.RowIndex].Values[0].ToString();
                BindPurchaseIndentMaterialTypeFields(PID, "Edit");
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveTC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        string SPOTCMID = "";
        try
        {
            foreach (GridViewRow row in gvPOTermsAndConditions.Rows)
            {
                CheckBox chk = (CheckBox)gvPOTermsAndConditions.Rows[row.RowIndex].FindControl("chkQC");

                if (chk.Checked)
                {
                    if (SPOTCMID == "")
                        SPOTCMID = gvPOTermsAndConditions.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        SPOTCMID = SPOTCMID + "," + gvPOTermsAndConditions.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.SaveSupplierPOtermsAndConditions(SPOTCMID);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','PO Terms And Conditions Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            cCommon.DownLoad(ViewState["FileName"].ToString() + '/' + ViewState["SPOID"].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            ShowHideControls("input");
            ds = objPc.GetMaxSPONoInSupplierPO("LS_GetMaxSPONumberInSupplierPO");
            string SPONumber = ds.Tables[0].Rows[0]["NewSPOnumber"].ToString();
            lblSPOnumber.Text = SPONumber == "" ? "SPO1" : SPONumber;
            hdnSPOID.Value = "0";
            objPc.GetPOPaymentDays(ddlPOPaymentDays);
            BindSupplierPODetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveSPOItem_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        objc = new cCommon();
        try
        {
            if (objc.Validate(divAddItems))
            {
                objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
                objPc.SPODID = Convert.ToInt32(hdnSPODID.Value);
                objPc.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);

                int MGMID = 0;
                int THKID = 0;
                decimal UnitCost = 0;
                string PIDAndPOQuantity = "";

                foreach (GridViewRow row in gvIndentDetails.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkitems");
                    if (chk.Checked)
                    {
                        TextBox txtReqWeight = (TextBox)row.FindControl("txtReqWeight");

                        if (PIDAndPOQuantity == "")
                        {
                            TextBox txtUnitCost = (TextBox)row.FindControl("txtUnitCost");

                            PIDAndPOQuantity = gvIndentDetails.DataKeys[row.RowIndex].Values[0].ToString() + "-" + txtReqWeight.Text;
                            MGMID = Convert.ToInt32(gvIndentDetails.DataKeys[row.RowIndex].Values[1].ToString());
                            THKID = Convert.ToInt32(gvIndentDetails.DataKeys[row.RowIndex].Values[2].ToString());
                            UnitCost = Convert.ToDecimal(txtUnitCost.Text);
                        }
                        else
                            PIDAndPOQuantity = PIDAndPOQuantity + "," + gvIndentDetails.DataKeys[row.RowIndex].Values[0].ToString() + "-" + txtReqWeight.Text;
                    }
                }

                objPc.MGMID = MGMID;
                objPc.PIDAndPOQuantity = PIDAndPOQuantity;
                objPc.THKID = THKID;
                objPc.UnitCost = UnitCost;

                objPc.CMID = Convert.ToInt32(ddlUOM.SelectedValue);
                objPc.MTID = Convert.ToInt32(ddlMaterialType.SelectedValue);
                objPc.DeliveryDate = DateTime.ParseExact(txtDeliveryDate_I.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objPc.Remarks = txtRemarks.Text;
                objPc.Quantity = 0;

                objPc.MTFIDs = hdn_MTFIDS.Value;
                objPc.MTFIDsValue = hdn_MTFIDsValue.Value;

                objPc.CreatedBy = objSession.employeeid;

                ds = objPc.SaveSupplierPOItemDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Supplier PO Saved Successfully');ShowAddPopUp();OpenTab('Item');dynamicvalueretain();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Supplier PO Updated Successfully');ShowAddPopUp();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAddPopUp();", true);

                BindSupplierPOItemDetails();
                showHidePopUpControls("add,view");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');HideAddPopUp();", true);
        }
        ClearField();
    }

    protected void lbtnSharePO_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            if (gvSupplierPOItemDetails.Rows.Count > 0)
            {
                ds = objPc.UpdateSupplierPOSharedStatus();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Po Shred Successfully');HideAddPopUp();", true);
                    BindSupplierPODetails();
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Atleast One Indent To Share PO');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void btnSaveSPOID_Click(object sender, EventArgs e)
    //{
    //    objPc = new cPurchase();
    //    DataSet ds = new DataSet();
    //    bool isValid = true;
    //    string error = "";
    //    try
    //    {
    //        foreach (GridViewRow row in gvPurchaseIndentDetails.Rows)
    //        {
    //            CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
    //            TextBox txtUnitCost = (TextBox)row.FindControl("txtUnitCost");
    //            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
    //            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

    //            if (txtUnitCost.Text == "")
    //                error = txtUnitCost.ClientID + '/' + "Field Required";
    //            else if (txtQuantity.Text == "")
    //                error = txtQuantity.ClientID + '/' + "Field Required";
    //            else if (txtRemarks.Text == "")
    //                error = txtRemarks.ClientID + '/' + "Field Required";

    //            if (error != "")
    //            {
    //                isValid = false;
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');ServerSideValidation('" + error + "')", true);
    //                break;
    //            }
    //        }

    //        if (isValid == true)
    //        {

    //            foreach (GridViewRow row in gvPurchaseIndentDetails.Rows)
    //            {
    //                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
    //                TextBox txtUnitCost = (TextBox)row.FindControl("txtUnitCost");
    //                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
    //                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

    //                objPc.PID = Convert.ToInt32(Convert.ToInt32(gvPurchaseIndentDetails.DataKeys[row.RowIndex].Values[0].ToString()));
    //                objPc.SPOID = chkditems.Checked == true ? Convert.ToInt32(hdnSPOID.Value) : 0;
    //                objPc.UnitCost = Convert.ToDecimal(txtUnitCost.Text);
    //                objPc.Quantity = Convert.ToInt32(txtQuantity.Text);
    //                objPc.Remarks = txtRemarks.Text;
    //                ds = objPc.SaveSupplierPODetails();
    //            }

    //            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');SuccessMessage('Success','Supplier PO Details Saved SuccessFully')", true);
    //                ddlRFNo_SelectIndexChanged(null, null);
    //            }
    //            else
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');ErrorMessage('Error','No Indent Selected')", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void btnItemAddNew_Click(object sender, EventArgs e)
    {
        //objPc = new cPurchase();
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            // objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            //ds = objPc.GetCategoryNameByListBySUPID(ddlMaterialCategory);
            // ddlmaterialGrdae.SelectedIndex = 0;
            objMat.GetMaterialClassificationNom(ddlUOM);
            objMat.getMaterialTypeName(ddlMaterialType);
            showHidePopUpControls("input,add,view");

            lblrequiredweight.Text = "0";
            lblTotalCost.Text = "0";
            BindIndentDetails();
            ClearField();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancelSPOItem_Click(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            showHidePopUpControls("add,view");
            ClearField();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();OpenTab('Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        objc = new cCommon();
        try
        {
            if (objc.Validate(divInput))
            {
                objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
                objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
                objPc.IssueDate = DateTime.ParseExact(txtIssueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objPc.QuateReferenceNumber = txtQuateReferenceNumber.Text;
                objPc.DeliveryDate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objPc.PaymentMode = ddlPOPaymentDays.SelectedValue;
                objPc.Note = txtNote.Text;
                objPc.Enclosure = txtEnclosure.Text;
                objPc.LocationID = Convert.ToInt32(ddlLocationName.SelectedValue);
                objPc.CurrencyID = Convert.ToInt32(ddlCurrencyname.SelectedValue);

                objPc.CreatedBy = objSession.employeeid;
                objPc.AmendmentReason = txtamdreason_edit.Text;

                objPc.CompanyID = Convert.ToInt32(objSession.CompanyID);

                // objPc.CurrencyValueINR = "";
                // objPc.TaxID = 0;
                // objPc.Value = 0;

                //objPc.CGST = txtCGST.Text == "" ? 0 : Convert.ToDecimal(txtCGST.Text);
                //objPc.SGST = txtSGST.Text == "" ? 0 : Convert.ToDecimal(txtSGST.Text);
                //objPc.IGST = txtIGST.Text == "" ? 0 : Convert.ToDecimal(txtIGST.Text);

                //objPc.FrightCharges = Convert.ToDecimal(txtrFrightCharges.Text);
                //objPc.PackingAndForwardingCharges = Convert.ToDecimal(txtPackingAndForwarding.Text);

                ds = objPc.SaveSupplierPO();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Supplier PO Saved Successfully');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Supplier PO Updated Successfully');", true);

                ShowHideControls("add,addnew,view");
                BindSupplierPODetails();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            ShowHideControls("add,addnew,view");
            hdnSPOID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            if (gvSupplierPOItemDetails.Rows.Count > 0)
            {
                objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
                objPc.POApprovalStatus = 7;
                objPc.Remarks = "";
                objPc.CreatedBy = objSession.employeeid;
                ds = objPc.UpdatePoApprovalStatusBySPOID("LS_UpdatePOApprovalStatusBySPOID");

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PO Approval Requested Successfully');", true);
                    SaveAlertDetails();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                BindSupplierPODetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Records Not Available');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveTaxAndOtherCharges_Click(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;
            if (CommandName == "othercharges")
            {
                objPc.ChargesID = Convert.ToInt32(ddlOtherCharges_t.SelectedValue);
                objPc.ChargesValue = Convert.ToDecimal(txtOtherCharges_t.Text);
            }
            else if (CommandName == "tax")
            {
                objPc.ChargesID = Convert.ToInt32(ddlTax_t.SelectedValue);
                objPc.ChargesValue = Convert.ToDecimal(txtTaxValue_t.Text);
            }
            objPc.ChargesType = CommandName;
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.SaveSupplierPOChargesDetails();

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

    protected void btnSaveInVoice_Click(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvSupplierPODetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAddSpoItem = (LinkButton)e.Row.FindControl("lbtnAdd");
                LinkButton btnEditSpo = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton btnAddTax = (LinkButton)e.Row.FindControl("btnAddTax");

                if (dr["POStatus"].ToString() == "1")
                {
                    btnEditSpo.Visible = false;
                    btnAddTax.Visible = false;
                }
                else if (dr["POStatus"].ToString() == "9" && string.IsNullOrEmpty(dr["POStatus"].ToString()))
                {
                    btnEditSpo.Visible = true;
                    btnAddTax.Visible = true;
                }
                else if (dr["POStatus"].ToString() == "7")
                {
                    btnEditSpo.Visible = true;
                    btnAddTax.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPurchaseIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkditems = (CheckBox)e.Row.FindControl("chkitems");
                TextBox txtUnitCost = (TextBox)e.Row.FindControl("txtUnitCost");
                TextBox txtQuantity = (TextBox)e.Row.FindControl("txtQuantity");
                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                if (dr["SPOID"].ToString() != "")
                    chkditems.Checked = true;
                else
                    chkditems.Checked = false;

                txtUnitCost.Text = dr["UnitCost"].ToString();
                txtQuantity.Text = dr["Quantity"].ToString();
                txtRemarks.Text = dr["RemarksSP"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSupplierPODetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string SPOID = gvSupplierPODetails.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["SupplierDetails"];
            hdnSPOID.Value = SPOID.ToString();
            dt.DefaultView.RowFilter = "SPOID='" + SPOID + "'";

            Label lblPONo = (Label)gvSupplierPODetails.Rows[index].FindControl("lblSPONumber");

            ViewState["PONo"] = lblPONo.Text;

            if (e.CommandName == "EditSP")
            {
                objPc = new cPurchase();
                objPc.GetPOPaymentDays(ddlPOPaymentDays);

                ddlSuplierName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["SUPID"].ToString();
                ddlLocationName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["LocationID"].ToString();
                txtIssueDate.Text = dt.DefaultView.ToTable().Rows[0]["IssueDateEdit"].ToString();
                txtDeliveryDate.Text = dt.DefaultView.ToTable().Rows[0]["DeliveryDateEdit"].ToString();
                ddlPOPaymentDays.SelectedValue = dt.DefaultView.ToTable().Rows[0]["PaymentMode"].ToString();
                txtQuateReferenceNumber.Text = dt.DefaultView.ToTable().Rows[0]["QuateReferenceNumber"].ToString();
                txtNote.Text = dt.DefaultView.ToTable().Rows[0]["Note"].ToString();
                txtEnclosure.Text = dt.DefaultView.ToTable().Rows[0]["Enclosure"].ToString();
                ddlCurrencyname.SelectedValue = dt.DefaultView.ToTable().Rows[0]["CurrencyID"].ToString();
                //  txtCurrencyValueINR.Text = dt.DefaultView.ToTable().Rows[0]["CurrencyValueINR"].ToString();

                //txtCGST.Text = dt.DefaultView.ToTable().Rows[0]["CGSTPer"].ToString();
                //txtSGST.Text = dt.DefaultView.ToTable().Rows[0]["SGSTPer"].ToString();
                //txtIGST.Text = dt.DefaultView.ToTable().Rows[0]["IGSTPer"].ToString();
                //txtrFrightCharges.Text = dt.DefaultView.ToTable().Rows[0]["FrightCharges"].ToString();
                //txtPackingAndForwarding.Text = dt.DefaultView.ToTable().Rows[0]["PackingAndForwarding"].ToString();

                ShowHideControls("input");
            }

            if (e.CommandName == "AddSPO")
            {
                objc = new cCommon();
                ViewState["SPOID"] = hdnSPOID.Value;
                //objc.GetRFPDetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                //divgvPurchase.Visible = false;
                //ddlRFPNo.SelectedIndex = 0;

                lblpodetailsheader_p.Text = ddlMaterialCategory.SelectedItem.Text + "/" + ddlSuplierName.SelectedItem.Text;

                hdnSPODID.Value = "0";

                BindIndentDetails();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();MandatoryGradeThicknessCheck();", true);

                btnItemAddNew.Visible = true;

                BindSupplierPOItemDetails();
                showHidePopUpControls("add,view");
            }
            if (e.CommandName == "PDF")
            {
                ViewState["SpoNo"] = dt.DefaultView.ToTable().Rows[0]["SPONumber"].ToString();
                //bindPurchaseOrderPDFDetails();
            }
            if (e.CommandName == "AddTax")
            {
                objPc = new cPurchase();
                objPc.BindChargesDetails(ddlOtherCharges_t, ddlTax_t);

                lblSpoNumber_T.Text = lblPONo.Text;

                BindTaxChargesDetails();
                BindOtherChargesDetails();
                BindPOTotalAmount();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowTaxPopUp();OpenTab('OtherCharges');", true);
            }
            if (e.CommandName == "AddPOInvoice")
            {
                BindMaterialInward(dt.DefaultView.ToTable().Rows[0]["LocationID"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowInwardPopUp();", true);
            }
            if (e.CommandName == "AddTC")
            {
                BindSupplierPOTermsAndConditions();
                BindSupplierPOTermsAndConditionDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowTCPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSupplierPOItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton btnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");

                if (ViewState["POStatus"].ToString() == "1")
                {
                    btnDelete.Visible = false;
                    btnEdit.Visible = false;
                    btnCancel.Visible = false;
                }
                else if (ViewState["POStatus"].ToString() == "9")
                {
                    if (ViewState["PoRevision"].ToString() != "0")
                        btnDelete.Visible = false;
                    else
                        btnDelete.Visible = true;
                    btnEdit.Visible = true;
                    btnCancel.Visible = true;
                }
                else if (ViewState["POStatus"].ToString() == "7")
                {
                    btnDelete.Visible = true;
                    //btnEdit.Visible = true;  jan-3-22
                    if (dr["SPOD_POInwardStatus"].ToString() == "0")
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;

                    btnCancel.Visible = false;
                }
                else if (ViewState["POStatus"].ToString() == "0" && ViewState["PoRevision"].ToString() == "0")
                    btnCancel.Visible = false;
                if (ViewState["PoRevision"].ToString() != "0")
                    btnDelete.Visible = false;
                if (ViewState["POStatus"].ToString() == "0" && dr["SPOD_POSharedStatus"].ToString() == "0" && ViewState["PoRevision"].ToString() != "0")
                {
                    btnCancel.Visible = true;
                    // jan 3 2022 / if (dr["SPOD_POInwardStatus"].ToString() == "0")
                    if (dr["SPOD_POInwardStatus"].ToString() == "0")
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;

                    ///else
                      //jan 3 2022//  btnEdit.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSupplierPOItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (e.CommandName == "EditPo")
            {
                //SPODID;
                int index = Convert.ToInt32(e.CommandArgument.ToString());

                objPc.SPODID = Convert.ToInt32(gvSupplierPOItemDetails.DataKeys[index].Values[1].ToString());
                objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
                objPc.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);

                ds = objPc.GetPOItemDetailsBySPODID();

                hdnSPODID.Value = ds.Tables[1].Rows[0]["SPODID"].ToString();

                objMat.getMaterialTypeName(ddlMaterialType);
                objMat.GetMaterialClassificationNom(ddlUOM);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvIndentDetails.DataSource = ds.Tables[0];
                    gvIndentDetails.DataBind();
                }
                else
                {
                    gvIndentDetails.DataSource = "";
                    gvIndentDetails.DataBind();
                }

                ddlMaterialType.SelectedValue = ds.Tables[1].Rows[0]["MaterialTypeID"].ToString();

                if (ds.Tables[1].Rows[0]["MaterialTypeID"].ToString() != "0")
                    BindmaterialTypeFields("Edit", Convert.ToInt32(ds.Tables[1].Rows[0]["SPODID"].ToString()));

                ddlUOM.SelectedValue = ds.Tables[1].Rows[0]["CMID"].ToString();
                txtRemarks.Text = ds.Tables[1].Rows[0]["Remarks"].ToString();
                txtDeliveryDate_I.Text = ds.Tables[1].Rows[0]["DeliveryDateEdit"].ToString();

                showHidePopUpControls("input,add,view");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (hdnSPODID.Value != "0")
                {

                    CheckBox chkitems = (CheckBox)e.Row.FindControl("chkitems");

                    if (string.IsNullOrEmpty(dr["CheckedStatus"].ToString()))
                        chkitems.Checked = false;
                    else
                        chkitems.Checked = true;
                }

                LinkButton btnAllow = (LinkButton)e.Row.FindControl("btnAllow");

                if (dr["APPO"].ToString() == "1")
                    btnAllow.Text = "Un Allow";
                else
                    btnAllow.Text = "Allow";

                if (objSession.type == 1)
                    btnAllow.Visible = true;
                else
                    btnAllow.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            if (e.CommandName == "Allow")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                objPc.PID = Convert.ToInt32(gvIndentDetails.DataKeys[index].Values[0].ToString());
                objPc.CreatedBy = objSession.employeeid;
                ds = objPc.UpdateAllowPermissionToPOQtyByPID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    if (ds.Tables[0].Rows[0]["Allow"].ToString() == "1")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Granted Succesfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Denied Succesfully');", true);
                }
                BindIndentDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPurchaseOrder_P_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].Text = "Unit Price" + " (" + ViewState["CurrencyName"].ToString() + ")";
                //e.Row.Cells[7].Text = ViewState["CurrencyName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    public void SaveAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        try
        {
            objAlerts.EntryMode = "Group";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = objSession.employeeid;
            objAlerts.reciverType = "Selected Group";
            objAlerts.file = "";
            objAlerts.reciverID = "0";
            objAlerts.EmailID = "";
            objAlerts.GroupID = 15;
            objAlerts.Subject = "PO Approval Alert";
            objAlerts.Message = "PO Approval Request From PO Number " + ViewState["PONo"].ToString();
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSupplierPOTermsAndConditionDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.GetSupplierPOTermsAndConditionsDetailsBySPOID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPOTermsAndConditionsDetails.DataSource = ds.Tables[0];
                gvPOTermsAndConditionsDetails.DataBind();
            }
            else
            {
                gvPOTermsAndConditionsDetails.DataSource = "";
                gvPOTermsAndConditionsDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSupplierPOTermsAndConditions()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            ds = objPc.GetSupplierPOTermsAndConditionsDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPOTermsAndConditions.DataSource = ds.Tables[0];
                gvPOTermsAndConditions.DataBind();
            }
            else
            {
                gvPOTermsAndConditions.DataSource = "";
                gvPOTermsAndConditions.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindIndentDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            objPc.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);

            ds = objPc.GetIndentDetailsByCIDAndSUPID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvIndentDetails.DataSource = ds.Tables[0];
                gvIndentDetails.DataBind();
            }
            else
            {
                gvIndentDetails.DataSource = "";
                gvIndentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMaterialInward(string LocationID)
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            objSt.LocationID = Convert.ToInt32(LocationID);

            ds = objSt.GetMaterialInwardBySUPIDAndLocationID();

            ViewState["MI"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialInward.DataSource = ds.Tables[0];
                gvMaterialInward.DataBind();
            }
            else
            {
                gvMaterialInward.DataSource = "";
                gvMaterialInward.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindPurchaseOrderPDFDetails(int index)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        string Tax = "";
        string OtherCharges = "";
        try
        {
            hdnSPOID.Value = gvSupplierPODetails.DataKeys[index].Values[0].ToString();
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.GetPurchaseOrderDetaisBySPODID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[4].Rows[0]["POStatus"].ToString() == "1")
                    divdigitalsign.Attributes.Add("style", "display:block;");
                else
                    divdigitalsign.Attributes.Add("style", "display:none;");

                lblsupplierAddress_P.Text = ds.Tables[4].Rows[0]["SupAddress"].ToString();
                lblSupplierName_P.Text = ds.Tables[4].Rows[0]["SupplierName"].ToString();
                lblPhoneNumber_P.Text = ds.Tables[4].Rows[0]["ContactNo"].ToString();
                lblEmail_P.Text = ds.Tables[4].Rows[0]["Emailid"].ToString();
                lblSupGSTNumber_P.Text = ds.Tables[4].Rows[0]["GSTNO"].ToString();

                lblAmdno_p.Text = ds.Tables[4].Rows[0]["PoRevision"].ToString() + " / " + ds.Tables[4].Rows[0]["SPOAmdOn"].ToString();
                lblamdreason_p.Text = ds.Tables[4].Rows[0]["AmendmentReason"].ToString();

                ViewState["CurrencyName"] = ds.Tables[4].Rows[0]["CurrencyName"].ToString();

                lblApprovedDatetime_p.Text = ds.Tables[4].Rows[0]["ApprovedDate"].ToString();

                lblPONumber_P.Text = ds.Tables[4].Rows[0]["SPONumber"].ToString();
                lblPOSharedDate_P.Text = ds.Tables[4].Rows[0]["POSharedDate"].ToString();

                lblQuoteReferenceNumber_P.Text = ds.Tables[4].Rows[0]["QuateReferenceNumber"].ToString();
                lblConsigneeAddress_P.Text = ds.Tables[4].Rows[0]["ConsigneeAddress"].ToString();
                lblRemarks_P.Text = ds.Tables[4].Rows[0]["Note"].ToString();

                if (ds.Tables[5].Rows[0]["CompanyID"].ToString() == "1")
                {
                    divGSTREV0.Visible = true;
                    divGSTREV1.Visible = false;
                    lblTNGSTNumber_P.Text = ds.Tables[5].Rows[0]["TNGSTNo"].ToString();
                    lblCSTNumber_P.Text = ds.Tables[5].Rows[0]["CSTNo"].ToString();
                    lblECCNo_P.Text = ds.Tables[5].Rows[0]["ECCNo"].ToString();
                    lblTINNumber_P.Text = ds.Tables[5].Rows[0]["TINNo"].ToString();
                    lblLonestatrGSTNo.Text = ds.Tables[5].Rows[0]["GSTNumber"].ToString();
                }
                else
                {
                    divGSTREV0.Visible = false;
                    divGSTREV1.Visible = true;
                    lblCINNo_P.Text = ds.Tables[5].Rows[0]["CINNo"].ToString();
                    lblPANNo_p.Text = ds.Tables[5].Rows[0]["PANNumber"].ToString();
                    lblTANNo_p.Text = ds.Tables[5].Rows[0]["TANNo"].ToString();
                    lblGSTIN_p.Text = ds.Tables[5].Rows[0]["GSTNumber"].ToString();
                }

                lblRange_P.Text = ds.Tables[4].Rows[0]["Range"].ToString();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvPurchaseOrder_P.DataSource = ds.Tables[0];
                    gvPurchaseOrder_P.DataBind();
                }
                else
                {
                    gvPurchaseOrder_P.DataSource = "";
                    gvPurchaseOrder_P.DataBind();
                }

                StringBuilder sbCharges;
                string sb = "<div class=\"row\"><label class=\"col-sm-9\" style=\"border: 1px solid #000;\">lblchargesname</label><span id=\"lblidname\" class=\"col-sm-3\" style=\"border: 1px solid #000;text-align:end;\">textname</span></div>";
                sbCharges = new StringBuilder();
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    sb = "<div class=\"col-sm-12\"><label class=\"col-sm-9\" style=\"border: 1px solid #000;\">lblchargesname</label><span id=\"lblidname\" class=\"col-sm-3\" style=\"border: 1px solid #000;text-align:end;\">textname</span></div>";
                    sb = sb.Replace("lblchargesname", dr["ChargesName"].ToString());
                    sb = sb.Replace("lblidname", dr["ChargesName"].ToString().Trim());
                    sb = sb.Replace("textname", dr["Value"].ToString());
                    sbCharges.Append(sb);

                    if (OtherCharges == "")
                        OtherCharges = dr["ChargesName"].ToString() + "#" + dr["Value"].ToString();
                    else
                        OtherCharges = OtherCharges + "/" + dr["ChargesName"].ToString() + "#" + dr["Value"].ToString();
                }
                divothercharges_p.InnerHtml = sbCharges.ToString();

                sbCharges = new StringBuilder();
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    sb = "<div class=\"col-sm-12\"><label class=\"col-sm-9\" style=\"border: 1px solid #000;\">lblchargesname</label><span id=\"lblidname\" class=\"col-sm-3\" style=\"border: 1px solid #000;text-align:end;\">textname</span></div>";
                    sb = sb.Replace("lblchargesname", dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % ");
                    sb = sb.Replace("lblidname", dr["TaxName"].ToString().Trim());
                    sb = sb.Replace("textname", dr["Amount"].ToString());
                    sbCharges.Append(sb);

                    if (Tax == "")
                        Tax = dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % " + "#" + dr["Amount"].ToString();
                    else
                        Tax = Tax + "/" + dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % " + "#" + dr["Amount"].ToString();
                }
                divtax_p.InnerHtml = sbCharges.ToString();

                lblSubTotal_P.Text = ds.Tables[3].Rows[0]["SUBTotal"].ToString();

                lblTotalAmount_P.Text = ds.Tables[3].Rows[0]["TotalAmount"].ToString();

                lblIndentNo_P.Text = ds.Tables[6].Rows[0]["QNo"].ToString();
                lblRFPNo_P.Text = ds.Tables[6].Rows[0]["RFPNo"].ToString();
                //[],
                lblDelivery_P.Text = ds.Tables[4].Rows[0]["DeliveryDate"].ToString();
                lblPaymentTerms_P.Text = ds.Tables[4].Rows[0]["PaymentMode"].ToString() + " " + ds.Tables[4].Rows[0]["Enclosure"].ToString();
                lblAmonutInWords_P.Text = ds.Tables[3].Rows[0]["RupeesInWords"].ToString();
                ViewState["PORevision"] = ds.Tables[4].Rows[0]["PoRevision"].ToString();


                lblAddress_h.Text = ds.Tables[7].Rows[0]["Address"].ToString();
                lblPhoneAndFaxNo_h.Text = ds.Tables[7].Rows[0]["PhoneAndFaxNo"].ToString();
                lblEmail_h.Text = ds.Tables[7].Rows[0]["Email"].ToString();
                lblwebsite_h.Text = ds.Tables[7].Rows[0]["WebSite"].ToString();
                lblCompanyName_h.Text = ds.Tables[7].Rows[0]["CompanyName"].ToString();
                lblFormarcompanyName_h.Text = ds.Tables[7].Rows[0]["FormalCompanyName"].ToString();
                lblcompanynamefooter_p.Text = ds.Tables[7].Rows[0]["CompanyNameFooter"].ToString();

                gvQualityRequirtments_P.DataSource = ds.Tables[8];
                gvQualityRequirtments_P.DataBind();

                lblDrawingRef_P.Text = ds.Tables[9].Rows[0]["DrawingName"].ToString();

                if (ds.Tables[10].Rows.Count > 0)
                {
                    gvForignPOTC_P.DataSource = ds.Tables[10];
                    gvForignPOTC_P.DataBind();
                }
                else
                {
                    gvForignPOTC_P.DataSource = "";
                    gvForignPOTC_P.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "POPrint('" + OtherCharges + "','" + Tax + "','" + ViewState["CurrencyName"].ToString() + "');", true);

                //  GeneratePDF();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','No Items Added');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindmaterialTypeFields(string Mode, int SPODID)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            ds = objPc.GetSupplierPOMaterialTypeFieldValuesBySPODID(ddlMaterialType.SelectedValue, Mode, SPODID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string dynamiccntrls = "<div class=\"col-sm-12 p-t-10\"><div class=\"col-sm-2\"></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label1replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt1replace\" type=\"text\" id=\"ContentPlaceHolder1_txt1replace\" onkeypress=\"return validationDecimal(this);\" Value='txtval1' class=\"form-control\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" onkeypress=\"return validationDecimal(this);\" type=\"text\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-2\"></div></div>";
                StringBuilder sbDivMTFields = new StringBuilder();
                int trowcnt = ds.Tables[0].Rows.Count;
                string partialdom = dynamiccntrls;
                string totoaldom = "";
                for (int trow = 0; trow < trowcnt; trow++)
                {
                    if (trow % 2 == 0)
                    {
                        partialdom = partialdom.Replace("label1replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt1replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval1", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval1", "");
                        if (trow == trowcnt - 1)
                        {
                            partialdom = partialdom.Replace("<div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" onkeypress=\"return validationDecimal(this);\" type=\"text\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div>", "");
                            sbDivMTFields.Append(partialdom);
                        }
                    }
                    else
                    {
                        partialdom = partialdom.Replace("label2replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt2replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval2", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval2", "");
                        sbDivMTFields.Append(partialdom);
                        partialdom = dynamiccntrls;
                    }
                }

                divMTFields.InnerHtml = sbDivMTFields.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPurchaseIndentMaterialTypeFields(string PID, string Mode)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.PID = Convert.ToInt32(PID);
            ds = objPc.GetPurchaseindentMaterialTypeFiledsByPID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMaterialType.SelectedValue = ds.Tables[0].Rows[0]["MaterialTypeID"].ToString();

                string dynamiccntrls = "<div class=\"col-sm-12 p-t-10\"><div class=\"col-sm-2\"></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label1replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt1replace\" type=\"text\" id=\"ContentPlaceHolder1_txt1replace\" onkeypress=\"return validationDecimal(this);\" Value='txtval1' class=\"form-control\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" onkeypress=\"return validationDecimal(this);\" type=\"text\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-2\"></div></div>";
                StringBuilder sbDivMTFields = new StringBuilder();
                int trowcnt = ds.Tables[0].Rows.Count;
                string partialdom = dynamiccntrls;
                string totoaldom = "";
                for (int trow = 0; trow < trowcnt; trow++)
                {
                    if (trow % 2 == 0)
                    {
                        partialdom = partialdom.Replace("label1replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt1replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval1", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval1", "");
                        if (trow == trowcnt - 1)
                        {
                            partialdom = partialdom.Replace("<div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" onkeypress=\"return validationDecimal(this);\" type=\"text\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div>", "");
                            sbDivMTFields.Append(partialdom);
                        }
                    }
                    else
                    {
                        partialdom = partialdom.Replace("label2replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt2replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval2", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval2", "");
                        sbDivMTFields.Append(partialdom);
                        partialdom = dynamiccntrls;
                    }
                }

                divMTFields.InnerHtml = sbDivMTFields.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSupplierPODetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            ds = objPc.getSupplierPODetails();
            ViewState["SupplierDetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierPODetails.DataSource = ds.Tables[0];
                gvSupplierPODetails.DataBind();
            }
            else
            {
                gvSupplierPODetails.DataSource = "";
                gvSupplierPODetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSupplierPOItemDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SPOID = Convert.ToInt32(ViewState["SPOID"].ToString());
            ds = objPc.getSupplierPOItemDetailsBySPOID();

            ViewState["POStatus"] = ds.Tables[1].Rows[0]["POStatus"].ToString();
            ViewState["POSharedStatus"] = ds.Tables[1].Rows[0]["PoSharedStatus"].ToString();
            ViewState["PoRevision"] = ds.Tables[1].Rows[0]["PoRevision"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierPOItemDetails.DataSource = ds.Tables[0];
                gvSupplierPOItemDetails.DataBind();
            }
            else
            {
                gvSupplierPOItemDetails.DataSource = "";
                gvSupplierPOItemDetails.DataBind();
            }
            if (ViewState["POStatus"].ToString() == "1")
            {
                lbtnSharePO.Visible = true;
                lbtnApprove.Visible = false;
                btnItemAddNew.Visible = false;
            }
            else if (ViewState["POStatus"].ToString() == "9")
            {
                lbtnSharePO.Visible = false;
                lbtnApprove.Visible = true;
            }
            else if (ViewState["POStatus"].ToString() == "7")
            {
                lbtnSharePO.Visible = false;
                lbtnApprove.Visible = false;
            }
            else if (ViewState["POStatus"].ToString() == "0")
            {
                lbtnSharePO.Visible = false;
                lbtnApprove.Visible = true;
            }
            //if (ViewState["POSharedStatus"].ToString() == "1")
            //    lbtnSharePO.Visible = false;
            //else
            //    lbtnSharePO.Visible = true;         
            objPc.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            objPc.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
            // objPc.GetMaterialGradeNameByCIDAndSUPID(ddlmaterialGrdae);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindOtherChargesDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.GetSupplierPOChargesDetails("LS_GetSupplierPOOtherChargesDetails");

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

    private void BindTaxChargesDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.GetSupplierPOChargesDetails("LS_GetSupplierPOTaxesDetails");
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
        objPc = new cPurchase();
        try
        {
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.GetSupplierPOChargesDetails("LS_BindPOTotalAmountWithOtherCharges");

            lblPOAmount_T.Text = "Total PO Amount : " + ds.Tables[0].Rows[0]["POAmountWithOtherCharges"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divAddNew.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "addnew":
                        divAddNew.Visible = true;
                        break;
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
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

    private void showHidePopUpControls(string divids)
    {
        divAddItems.Visible = divAddNewItem.Visible = divOutputsItems.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAddNewItem.Visible = true;
                        break;
                    case "view":
                        divOutputsItems.Visible = true;
                        break;
                    case "input":
                        divAddItems.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            //var sbPurchaseOrder = new StringBuilder();

            //divPurchaseOrder_PDF.Attributes.Add("style", "display:block;");

            //divPurchaseOrder_PDF.RenderControl(new HtmlTextWriter(new StringWriter(sbPurchaseOrder)));

            //string htmlfile = ViewState["SpoNo"].ToString() + "_" + ViewState["PORevision"].ToString() + ".html";
            //string pdffile = ViewState["SpoNo"].ToString() + "_" + ViewState["PORevision"].ToString() + ".pdf";
            //string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            //string pdfFileURL = LetterPath + pdffile;
            //string htmlfileURL = LetterPath + htmlfile;

            //SaveHtmlFile(htmlfileURL, "PO", "", sbPurchaseOrder.ToString());

            //objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);

            //divPurchaseOrder_PDF.Attributes.Add("style", "display:none;");



        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {

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
            w.WriteLine(HeaderTitle);
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
            w.WriteLine("<style type='text/css'> label{ font-weight: 900; } table{ border: solid; } .header{ width: 205mm;left: 2mm;border: 0px solid #000; } </style>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12'>");
            w.WriteLine(div);
            w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearField()
    {
        try
        {
            hdnSPODID.Value = "0";
            divMTFields.InnerHtml = "";
            ddlMaterialType.SelectedIndex = 0;
            lblTotalCost.Text = "0";
            lblrequiredweight.Text = "0";
            txtRemarks.Text = "";
            txtDeliveryDate_I.Text = "";
            ddlUOM.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

        if (gvSupplierPODetails.Rows.Count > 0)
            gvSupplierPODetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvSupplierPOItemDetails.Rows.Count > 0)
            gvSupplierPOItemDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}