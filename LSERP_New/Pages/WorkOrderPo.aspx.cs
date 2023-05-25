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

public partial class Pages_WorkOrderPo : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cCommonMaster objcommon;
    c_Finance objFin;
    cSales objSales;
    cMaterials objMat;
    cProduction objP;
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
                //objPc = new cPurchase();
                objc = new cCommon();
                //objcommon = new cCommonMaster();
                //objFin = new c_Finance();
                //objPc.GetSupplierDetails(ddlSuplierName);
                objc.GetLocationDetails(ddlLocationName);
                BindPendingIndentListDetails();
                ShowHideControls("add,view");
            }

            if (target == "deletegvrow")
            {
                DataSet ds = new DataSet();
                objcommon = new cCommonMaster();

                objcommon.AttachementID = Convert.ToInt32(arg);
                ds = objcommon.DeleteAttachements();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Attachements Deleted Successfully');", true);

                //BindAttachements(ViewState["SPOID"].ToString());
            }
            if (target == "deletegvrowSPODID")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();

                objPc.SPODID = Convert.ToInt32(arg);

                ds = objPc.DeleteSupplierPOItemDetailsBySPODID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Item Deleted Successfully');ShowAddPopUp();OpenTab('Item');", true);

            }

            if (target == "SharePOApproval")
                UpdateSharePOApproval();

            if (target == "deletegvRowotherCharges")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                objPc.ChargesID = Convert.ToInt32(arg);
                objPc.PageNameFlag = "othercharges";

                ds = objPc.DeleteWorkOrderPOtaxAndOtherChargesDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');ShowTaxPopUp();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
                BindOtherChargesDetails();

            }
            if (target == "deletegvRowtaxCharges")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                objPc.ChargesID = Convert.ToInt32(arg);
                objPc.PageNameFlag = "tax";

                ds = objPc.DeleteWorkOrderPOtaxAndOtherChargesDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');ShowTaxPopUp();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
                BindTaxChargesDetails();
            }
            if (target == "WPOCancel")
            {
                //DataSet ds = new DataSet();
                //objPc = new cPurchase();
                //objPc.PageNameFlag = "Cancel";
                //objPc.WPOID = Convert.ToInt32(arg);
                //objPc.CreatedBy = objSession.employeeid;
                //ds = objPc.CancelWorkOrderPOByWPOID();

                //if (ds.Tables[0].Rows[0]["Message"].ToString() == "Cancelled")
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','PO Cancelled Successfully');", true);
                //else
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                UpdateAmendmentsDetailsByWPOID(arg.ToString());
                BindWorkOrderPODetails();
            }
            if (target == "ViewWODrawingFile")
                viewWorkOrderDrawingFile(arg.ToString());
            if (target == "ViewWODrawingFileAFO")
            {
                objc = new cCommon();
                try
                {
                    string FileName = gvWorkOrderPODetails.DataKeys[Convert.ToInt32(arg.ToString())].Values[2].ToString();
                    string WONo = gvWorkOrderPODetails.DataKeys[Convert.ToInt32(arg.ToString())].Values[1].ToString();
                    objc.ViewFileName(Session["WorkOrderIndentSavePath"].ToString(), Session["WorkOrderIndentHttpPath"].ToString(), FileName, WONo, ifrm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();", true);
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            if (target == "ViewWODrawingFilePending")
            {
                objc = new cCommon();
                try
                {
                    string FileName = gvPendingIndentListDetails.DataKeys[Convert.ToInt32(arg.ToString())].Values[1].ToString();
                    string WONo = gvPendingIndentListDetails.DataKeys[Convert.ToInt32(arg.ToString())].Values[0].ToString();
                    objc.ViewFileName(Session["WorkOrderIndentSavePath"].ToString(), Session["WorkOrderIndentHttpPath"].ToString(), FileName, WONo, ifrm);
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            if (target == "ViewIndentAttach")
            {
                int index = Convert.ToInt32(arg);
                string FileName = gvWorkOrderPO.DataKeys[index].Values[3].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "opennewtab('" + FileName + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rbPoTypeChanged_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            objPc.PoType = rbPoTypeChanged.SelectedValue;
            objPc.GetSupplierChainVendorNameDetails(ddlSuplierName);
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlSuplierName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSuplierName.SelectedIndex > 0)
            {
                BindWorkOrderPODetails();
                ShowHideControls("add,aDdNew,VIEW");
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            //cCommon.DownLoad(ViewState["FileName"].ToString() + '/' + ViewState["SPOID"].ToString());
            GeneratePDDF();
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
            ds = objPc.GetMaxSPONoInSupplierPO("LS_GetMaxWPONumber");
            string WPONumber = ds.Tables[0].Rows[0]["NewWOnumber"].ToString();
            lblSPOnumber.Text = WPONumber == "" ? "WO-1/20" : WPONumber;

            hdnWPOID.Value = "0";

            clearFieldValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnWorkOrderPODetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        objPc = new cPurchase();
        objc = new cCommon();
        bool msg = true;
        try
        {
            decimal totalcost;
            dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("WPOID");
            dt.Columns.Add("WOID");
            dt.Columns.Add("UnitCost");
            dt.Columns.Add("TotalCost");
            dt.Columns.Add("UnitRequiredWeight");

            foreach (GridViewRow row in gvWorkOrderIndent.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                TextBox txtUnitCost = (TextBox)row.FindControl("txtUnitCost");
                TextBox lblPartQty = (TextBox)row.FindControl("txtPoQty");
                string WOID = gvWorkOrderIndent.DataKeys[row.RowIndex].Values[0].ToString();
                TextBox txtUnitRequiredWeight = (TextBox)gvWorkOrderIndent.Rows[row.RowIndex].FindControl("txtActualWeight");
                Label lblRawMaterialQty = (Label)gvWorkOrderIndent.Rows[row.RowIndex].FindControl("lblRawMatQty");

                Label lblActualQty = (Label)gvWorkOrderIndent.Rows[row.RowIndex].FindControl("lblActualQty");

                string UnitRequiredWeight = txtUnitRequiredWeight.Text;

                // string Per = gvWorkOrderIndent.DataKeys[row.RowIndex].Values[5].ToString();
                // string AvailableQty = gvWorkOrderIndent.DataKeys[row.RowIndex].Values[6].ToString();

                if (chkitems.Checked)
                {

                    if (Convert.ToDecimal(txtUnitCost.Text) > 0 && Convert.ToInt32(lblPartQty.Text) > 0)
                    {
                        Decimal UnitPartRawMatQty = Convert.ToDecimal(lblRawMaterialQty.Text) / Convert.ToDecimal(lblActualQty.Text);

                        if (rbPoTypeChanged.SelectedValue == "WPO")
                            totalcost = Convert.ToDecimal(txtUnitCost.Text) * Convert.ToDecimal(lblPartQty.Text);
                        else
                            totalcost = Convert.ToDecimal(txtUnitCost.Text) * Convert.ToDecimal(UnitRequiredWeight);

                        dr = dt.NewRow();
                        dr["WPOID"] = lblPartQty.Text;
                        dr["WOID"] = WOID;
                        dr["UnitCost"] = txtUnitCost.Text;
                        dr["TotalCost"] = totalcost;
                        dr["UnitRequiredWeight"] = UnitRequiredWeight;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        msg = false;
                        break;
                    }
                }
            }
            if (msg)
            {
                objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
                objPc.type = rbPoTypeChanged.SelectedValue;
                objPc.dt = dt;
                ds = objPc.SaveWorkOrderPODetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Workorder PO Saved Successfully');ShowAddPopUp();", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Workorder PO Updated Successfully');ShowAddPopUp();", true);

                BindWorkOrderIndentDetailsByWPOID();
                BindWorkOrderPODetailsByWPOID(gvWorkOrderPODetails);
                BindWorkOrderPODetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Zero Is Not Valid');", true);
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
        bool HasFile = false;
        try
        {
            if (objc.Validate(divInput))
            {
                objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
                objPc.SCVMID = Convert.ToInt32(ddlSuplierName.SelectedValue);
                objPc.IssueDate = DateTime.ParseExact(txtIssueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objPc.QuateReferenceNumber = txtQuateReferenceNumber.Text;
                objPc.DeliveryDate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objPc.PaymentMode = txtPayment.Text;
                objPc.Note = txtNote.Text;
                objPc.Enclosure = txtEnclosure.Text;
                objPc.LocationID = Convert.ToInt32(ddlLocationName.SelectedValue);
                //   if (txthandlingcharges.Text != "") objPc.handlingcharges = Convert.ToDecimal(txthandlingcharges.Text);
                objPc.woremarks = txtworemarks.Text;

                string AttachmentName = "";
                if (fAttachment.HasFile)
                {
                    objSales = new cSales();

                    string MaxAttachementId = objSales.GetMaximumAttachementID();
                    string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                    AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                    string[] extension = AttachmentName.Split('.');
                    AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                    HasFile = true;
                }

                objPc.AttachementName = AttachmentName;
                objPc.AmendmentReason = txtamdreason_edit.Text;
                objPc.UserID = Convert.ToInt32(objSession.employeeid);
                objPc.CompanyID = Convert.ToInt32(objSession.CompanyID);
                ds = objPc.SaveWorkorderPO();

                string Msg = ds.Tables[0].Rows[0]["Message"].ToString();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Workorder PO Saved Successfully');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Workorder PO Updated Successfully');", true);

                if (Msg == "Added" || Msg == "Updated")
                {
                    if (HasFile)
                    {
                        objc.Foldername = Session["WorkOrderIndentSavePath"].ToString();
                        objc.FileName = AttachmentName;
                        objc.PID = ds.Tables[0].Rows[0]["WOIPONo"].ToString();
                        objc.AttachementControl = fAttachment;
                        objc.SaveFiles();
                    }
                }
                ShowHideControls("add,addNew,View");
                BindWorkOrderPODetails();

                clearFieldValues();
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
            hdnWPOID.Value = "0";
            clearFieldValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSharePO_Click(object sender, EventArgs e)
    {

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
            objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
            ds = objPc.SaveWorkOrderPOTaxAndOtherChargesDetails();

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

    #region "GridView Events"

    protected void gvWorkOrderPO_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string WPOID = gvWorkOrderPO.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["WorkOrderPO"];
            hdnWPOID.Value = WPOID.ToString();
            dt.DefaultView.RowFilter = "WPOID='" + WPOID + "'";

            Label lblSPONumber = (Label)gvWorkOrderPO.Rows[index].FindControl("lblSPONumber");

            if (e.CommandName == "EditWP")
            {
                ddlSuplierName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["SCVMID"].ToString();
                ddlLocationName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["LocationID"].ToString();
                txtIssueDate.Text = dt.DefaultView.ToTable().Rows[0]["IssueDateEdit"].ToString();
                txtDeliveryDate.Text = dt.DefaultView.ToTable().Rows[0]["DeliveryDateEdit"].ToString();
                txtPayment.Text = dt.DefaultView.ToTable().Rows[0]["PaymentMode"].ToString();
                txtQuateReferenceNumber.Text = dt.DefaultView.ToTable().Rows[0]["QuateReferenceNumber"].ToString();
                txtNote.Text = dt.DefaultView.ToTable().Rows[0]["Note"].ToString();
                txtEnclosure.Text = dt.DefaultView.ToTable().Rows[0]["Enclosure"].ToString();
                txtamdreason_edit.Text = dt.DefaultView.ToTable().Rows[0]["WPOAmendmentReason"].ToString();

                ShowHideControls("input");
            }

            if (e.CommandName == "AddWPO")
            {
                ViewState["WPOID"] = hdnWPOID.Value;

                Label lblWpoNo = (Label)gvWorkOrderPO.Rows[index].FindControl("lblSPONumber");
                lblWPONo_H.Text = lblWpoNo.Text;

                string POStatus = gvWorkOrderPO.DataKeys[index].Values[1].ToString();
                string ItemAdded = gvWorkOrderPO.DataKeys[index].Values[0].ToString();

                if ((POStatus == "NA" && ItemAdded == "Yes") || POStatus == "Rejected" || POStatus == "NA")
                    btnWPOD.Visible = true;
                else
                    btnWPOD.Visible = false;

                BindWorkOrderIndentDetailsByWPOID();
                BindWorkOrderPODetailsByWPOID(gvWorkOrderPODetails);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();Orderingfalse();", true);
            }

            if (e.CommandName == "PDF")
            {
                DataSet ds = new DataSet();
                objPc = new cPurchase();
                string Tax = "";
                string OtherCharges = "";
                objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
                ds = objPc.GetWorkOrderPOByWPOIDAndPOSharedStatusCompleted();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ApprovedTime"] = ds.Tables[0].Rows[0]["WPOL1ApprovedDate"].ToString();
                    ViewState["POStatus"] = ds.Tables[0].Rows[0]["POStatus"].ToString();
                    ViewState["ApprovedBy"] = ds.Tables[0].Rows[0]["L1ApprovedBy"].ToString();

                    lblNote_p.Text = ds.Tables[0].Rows[0]["Note"].ToString();
                    lblSupplierChainVendorName_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                    lblReceiverAddress_p.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    lblConsigneeAddress_p.Text = lblConsigneeAddressRev1_p.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                    lblGSTNumber_p.Text = "<label> GST No: </label>" + ds.Tables[0].Rows[0]["GSTNO"].ToString();
                    lblRange_p.Text = ds.Tables[0].Rows[0]["Range"].ToString();

                    lblAMDReson_p.Text = ds.Tables[0].Rows[0]["WPOAmendmentReason"].ToString();
                    lblAMDDate_p.Text = ds.Tables[0].Rows[0]["WPOAmendmentOn"].ToString();

                    lblWoNo_p.Text = ds.Tables[0].Rows[0]["Wonumber"].ToString();
                    ViewState["WPONo"] = ds.Tables[0].Rows[0]["Wonumber"].ToString();
                    ViewState["PORevision"] = ds.Tables[0].Rows[0]["PORevision"].ToString();

                    lblWODate_p.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();
                    lblRFPNo_p.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                    lblQuoteRefNo_p.Text = ds.Tables[0].Rows[0]["QuateReferenceNumber"].ToString();
                    lblDeliveryDate_p.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                    lblPayment_p.Text = ds.Tables[0].Rows[0]["PaymentMode"].ToString();

                    lblSubTotal_p.Text = ds.Tables[7].Rows[0]["SubTotal"].ToString();
                    lblTotalAmount_p.Text = ds.Tables[7].Rows[0]["TotalAmount"].ToString();

                    lblRupeesInwords_p.Text = ds.Tables[0].Rows[0]["AmountInRupees"].ToString();

                    gvWorkOrderPOItemDetails_p.DataSource = ds.Tables[2];
                    gvWorkOrderPOItemDetails_p.DataBind();

                    if (ds.Tables[4].Rows[0]["CompanyID"].ToString() == "1")
                    {
                        divGSTREV0.Visible = true;
                        divGSTREV1.Visible = false;

                        lblTNGSTNo.Text = "<label> TNGST No: </label>" + ds.Tables[3].Rows[0]["TNGSTNo"].ToString();
                        lblCSTNo.Text = "<label> CST No: </label>" + ds.Tables[3].Rows[0]["CSTNo"].ToString();
                        lblECCNo.Text = "<label> ECC No: </label>" + ds.Tables[3].Rows[0]["ECCNo"].ToString();
                        lblTINNo.Text = "<label> TIN No: </label>" + ds.Tables[3].Rows[0]["TINNo"].ToString();
                        lblGSTNo.Text = "<label> GST No: </label>" + ds.Tables[3].Rows[0]["GSTNumber"].ToString();
                    }
                    else
                    {
                        divGSTREV0.Visible = false;
                        divGSTREV1.Visible = true;
                        lblCINNo_P.Text = "<label> CIN No: </label>" + ds.Tables[3].Rows[0]["CINNo"].ToString();
                        lblPANNo_p.Text = "<label> PAN No: </label>" + ds.Tables[3].Rows[0]["PANNumber"].ToString();
                        lblTANNo_p.Text = "<label> TAN No: </label>" + ds.Tables[3].Rows[0]["TANNo"].ToString();
                        lblGSTIN_p.Text = "<label> GSTIN No: </label>" + ds.Tables[3].Rows[0]["GSTNumber"].ToString();
                    }

                    ViewState["Address"] = ds.Tables[4];

                    StringBuilder sbCharges;
                    string sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                    sbCharges = new StringBuilder();
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                        sb = sb.Replace("lblchargesname", dr["ChargesName"].ToString());
                        sb = sb.Replace("textname", dr["Value"].ToString());
                        sbCharges.Append(sb);
                    }
                    divothercharges_p.InnerHtml = sbCharges.ToString();

                    sbCharges = new StringBuilder();
                    foreach (DataRow dr in ds.Tables[6].Rows)
                    {
                        sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                        sb = sb.Replace("lblchargesname", dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % ");
                        sb = sb.Replace("textname", dr["Amount"].ToString());
                        sbCharges.Append(sb);
                    }
                    divtax_p.InnerHtml = sbCharges.ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPoPrintPopUp();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Please Add  Item  Details');", true);

                ViewState["SpoNo"] = dt.DefaultView.ToTable().Rows[0]["SPONumber"].ToString();
            }

            if (e.CommandName == "AddTax")
            {
                objPc = new cPurchase();
                objPc.BindChargesDetails(ddlOtherCharges_t, ddlTax_t);

                lblSpoNumber_T.Text = lblSPONumber.Text;

                BindTaxChargesDetails();
                BindOtherChargesDetails();
                BindPOTotalAmount();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowTaxPopUp();OpenTab('OtherCharges');", true);
            }

            if (e.CommandName == "updateAMD")
            {
                ViewState["WPOID"] = hdnWPOID.Value;
                BindWorkOrderPODetailsByWPOID(gvWorkOrderPOAmendsDetails);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowWorkOrderPOAmendmentsPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPO_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAdd = (LinkButton)e.Row.FindControl("lbtnAdd");
                LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
                LinkButton btnAMD = (LinkButton)e.Row.FindControl("btnAMD");

                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (dr["POStatus"].ToString() == "NA" && dr["ItemsAdded"].ToString() == "Yes")
                    chk.Visible = true;
                else if (dr["POStatus"].ToString() == "NA")
                    chk.Visible = false;
                else if (dr["POStatus"].ToString() == "Requested" || dr["POStatus"].ToString() == "L1 Approved" 
				|| dr["POStatus"].ToString() == "L2 Approved" || dr["POStatus"].ToString() == "L3 Approved")
                    chk.Visible = false;
                else if (dr["POStatus"].ToString() == "Rejected")
                    chk.Visible = true;

                if (dr["POStatus"].ToString() == "L1 Approved")
                    btnAMD.Visible = true;
                else
                    btnAMD.Visible = false;

                if (dr["PORevision"].ToString() != "0")
                    btnAdd.Visible = false;
                else
                    btnAdd.Visible = true;

                //if (dr["POStatus"].ToString() == "Approved" && objSession.type == 1)
                //    btnCancel.Visible = true;
                //if (!string.IsNullOrEmpty(dr["WPOIDStatus"].ToString()))
                //    btnAdd.Enabled = false;               
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPODetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "DeletePOItem")
            {
                ViewState["WPODID"] = gvWorkOrderPODetails.DataKeys[index].Values[0].ToString();
                DeleteWorkOrderPOItemDetailsByWPODID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPODetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btndelete = (LinkButton)e.Row.FindControl("btndelete");
                if (btnWPOD.Visible)
                    btndelete.Visible = true;
                else
                    btndelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderIndent_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Allow")
            {
                string WOIHID = gvWorkOrderIndent.DataKeys[index].Values[0].ToString();
                UpdatePOQtyAllowPermission(WOIHID);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderIndent_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAllow = (LinkButton)e.Row.FindControl("btnAllow");

                if (dr["APPO"].ToString() == "0")
                    btnAllow.Text = "Allow";
                else
                    btnAllow.Text = "Un Allow";

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

    protected void gvWorkOrderPOAmendsDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtEmployeeListByDept;
        try
        {
            //Set the edit index.
            gvWorkOrderPOAmendsDetails.EditIndex = e.NewEditIndex;
            BindWorkOrderPODetailsByWPOID(gvWorkOrderPOAmendsDetails);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPOAmendsDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvWorkOrderPOAmendsDetails.EditIndex = -1;
            BindWorkOrderPODetailsByWPOID(gvWorkOrderPOAmendsDetails);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPOAmendsDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            TextBox txtunitcost = (TextBox)gvWorkOrderPOAmendsDetails.Rows[e.RowIndex].FindControl("txtUnitCost");

            objPc.UnitCost = Convert.ToDecimal(txtunitcost.Text);
            objPc.WPODID = Convert.ToInt32(gvWorkOrderPOAmendsDetails.DataKeys[e.RowIndex].Values[0].ToString());
            objPc.CreatedBy = objSession.employeeid;

            ds = objPc.UpdateWorkOrderPOAmendmentDetailsByWPODID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','Unit Cost Updated Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            gvWorkOrderPOAmendsDetails.EditIndex = -1;
            BindWorkOrderPODetailsByWPOID(gvWorkOrderPOAmendsDetails);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"    

    private void BindPendingIndentListDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            ds = objP.GetWorkOrderIndentDetailsByWPOID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPendingIndentListDetails.DataSource = ds.Tables[0];
                gvPendingIndentListDetails.DataBind();
            }
            else
            {
                gvPendingIndentListDetails.DataSource = "";
                gvPendingIndentListDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateAmendmentsDetailsByWPOID(string WPOID)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.WPOID = Convert.ToInt32(WPOID);
            objPc.UserID = Convert.ToInt32(objSession.employeeid);
            objPc.AmendmentReason = txtamdreason.Text;

            ds = objPc.UpdateWorkOrderPOAmendmentDetailsByWPOID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PO Revision Changed Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString().Replace("'", "") + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdatePOQtyAllowPermission(string WOIHID)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.WOIHID = Convert.ToInt32(WOIHID);
            objPc.UserID = Convert.ToInt32(objSession.employeeid);
            ds = objPc.UpdatePOQtyAllowPermissionByWOIHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                if (ds.Tables[0].Rows[0]["Allow"].ToString() == "1")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Po qty Permission Granted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Po qty Permission Denied Successfully');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void viewWorkOrderDrawingFile(string index)
    {
        objc = new cCommon();
        try
        {
            string FileName = gvWorkOrderIndent.DataKeys[Convert.ToInt32(index)].Values[2].ToString();
            string WONo = gvWorkOrderIndent.DataKeys[Convert.ToInt32(index)].Values[4].ToString();
            objc.ViewFileName(Session["WorkOrderIndentSavePath"].ToString(), Session["WorkOrderIndentHttpPath"].ToString(), FileName, WONo, ifrm);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteWorkOrderPOItemDetailsByWPODID()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.WPODID = Convert.ToInt32(ViewState["WPODID"].ToString());
            ds = objPc.DeleteWorkOrderPOItemDetailsByWPODID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','PO Details Deleted Successfully');ShowAddPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAddPopUp();", true);

            BindWorkOrderIndentDetailsByWPOID();
            BindWorkOrderPODetailsByWPOID(gvWorkOrderPODetails);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderPODetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SCVMID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            ds = objPc.GetWorkOrderPODetails();
            ViewState["WorkOrderPO"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderPO.DataSource = ds.Tables[0];
                gvWorkOrderPO.DataBind();
            }
            else
            {
                gvWorkOrderPO.DataSource = "";
                gvWorkOrderPO.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderIndentDetailsByWPOID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            // objP.WPOID = Convert.ToInt32(ViewState["WPOID"].ToString());
            ds = objP.GetWorkOrderIndentDetailsByWPOID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderIndent.DataSource = ds.Tables[0];
                gvWorkOrderIndent.DataBind();
            }
            else
            {
                gvWorkOrderIndent.DataSource = "";
                gvWorkOrderIndent.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderPODetailsByWPOID(GridView gv)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.WPOID = Convert.ToInt32(ViewState["WPOID"].ToString());
            ds = objPc.GetWorkOrderPODetailsByWPOID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = ds.Tables[0];
                gv.DataBind();
            }
            else
            {
                gv.DataSource = "";
                gv.DataBind();
            }
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

    private void GeneratePDDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            //StringBuilder sbCosting = new StringBuilder();
            //divWorkOrderPoPrint_p.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            //string htmlfile = "WorkOrderPo_" + ViewState["WPONo"].ToString().Replace("/", "") + "_" + ViewState["PORevision"].ToString() + ".html";
            //string pdffile = "WorkOrderPo_" + ViewState["WPONo"].ToString().Replace("/", "") + "_" + ViewState["PORevision"].ToString() + ".pdf";
            //string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            //string pdfFileURL = LetterPath + pdffile;

            //string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
            hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();
            hdnCompanyName.Value = dtAddress.Rows[0]["CompanyName"].ToString();
            hdnFormalCompanyname.Value = dtAddress.Rows[0]["FormalCompanyName"].ToString();
            hdnCompanyNameFooter.Value = dtAddress.Rows[0]["CompanyNameFooter"].ToString();

            hdnLonestarLogo.Value = dtAddress.Rows[0]["LonestarLOGO"].ToString();
            hdnISOLogo.Value = dtAddress.Rows[0]["ISOLogo"].ToString();
            // SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());
            // objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintWorkOrderPO('" + ViewState["ApprovedTime"].ToString() + "','" + ViewState["POStatus"].ToString() + "','" + ViewState["ApprovedBy"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}

    //public void SaveHtmlFile(string URL, string Main, string epstyleurl, string style, string Print, string topstrip, string div)
    //{
    //    try
    //    {
    //        DataTable dtAddress = new DataTable();
    //        dtAddress = (DataTable)ViewState["Address"];

    //        //string Address = dtAddress.Rows[0]["Address"].ToString();
    //        //string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
    //        //string Email = dtAddress.Rows[0]["Email"].ToString();
    //        //string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

    //        hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
    //        hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
    //        hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
    //        hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

    //        StreamWriter w;
    //        w = File.CreateText(URL);
    //        //w.WriteLine("<html><head><title>");       
    //        w.WriteLine("<html><head><title>");
    //        w.WriteLine("Offer");
    //        w.WriteLine("</title>");
    //        w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
    //        w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
    //        w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
    //        w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
    //        w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
    //        w.WriteLine("<div class='print-page'>");
    //        w.WriteLine("<table><thead><tr><td>");
    //        w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
    //        w.WriteLine("<div class='header' style='border-bottom:1px solid;left:5mm !important;background:transparent'>");
    //        w.WriteLine("<div>");
    //        w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
    //        w.WriteLine("<div class='row'>");
    //        w.WriteLine("<div class='col-sm-2'>");
    //        w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
    //        w.WriteLine("</div>");
    //        w.WriteLine("<div class='col-sm-8 text-center'>");
    //        w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR INDUSTRIES </h3>");
    //        w.WriteLine("<p style='font-weight:500;color:#000;width: 100%;'>" + Address + "</p>");
    //        w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
    //        w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
    //        w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
    //        w.WriteLine("</div>");
    //        w.WriteLine("<div class='col-sm-2'>");
    //        w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
    //        w.WriteLine("</div></div>");
    //        w.WriteLine("</div></div></div>");
    //        w.WriteLine("</td></tr></thead>");
    //        w.WriteLine("<tbody><tr><td>");
    //        w.WriteLine("<div class='col-sm-12 padding:0' style='padding-top:0px;'>");
    //        w.WriteLine(div);
    //        w.WriteLine("</div>");
    //        w.WriteLine("</td></tr></tbody>");
    //        w.WriteLine("<tfoot><tr><td>");
    //        w.WriteLine("<div class='footer-space'>");
    //        w.WriteLine("<div class='footer'>");
    //        w.WriteLine("<div class='col-sm-12' style='border:1px solid #000;padding-top:10px'>");
    //        w.WriteLine("<div class='text-center' style='color:#000'>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OFACCEPTANCE</div>");
    //        w.WriteLine("<div class='col-sm-6' style='height:100px;display:flex;align-items:flex-end;justify-content:flex-start'>");
    //        w.WriteLine("<label style='padding-top:60px;padding-left:15px'>PREPARED & CHECKED BY</label>");
    //        w.WriteLine("</div>");
    //        w.WriteLine("<div class='col-sm-6' style='height:100px;display:flex;align-items:center;justify-content:space-around;flex-direction:column;text-align:center'>");
    //        w.WriteLine("<label style='display:block;width:100%;padding-top:10px'>For LONESTAR INDUSTRIES</label>");
    //        w.WriteLine("<label style='display:block;width:100%;padding-top:60px'>AUTHORISED SIGNATORY</label>");
    //        w.WriteLine("</div>");
    //        w.WriteLine("</div></div>");
    //        w.WriteLine("</div>");
    //        w.WriteLine("</td></tr></tfoot></table>");
    //        w.WriteLine("</div>");
    //        w.WriteLine("</html>");

    //        w.Flush();
    //        w.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    private void UpdateSharePOApproval()
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        string WPOIDs = "";
        try
        {
            foreach (GridViewRow row in gvWorkOrderPO.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    if (WPOIDs == "")
                        WPOIDs = gvWorkOrderPO.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        WPOIDs = WPOIDs + ',' + gvWorkOrderPO.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            objPc.WPOApprovalStatus = 7;
            objPc.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objPc.UpdatePOApprovalStatusByWPOID("LS_UpdatePOApprovalStatusByWPOID", WPOIDs);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','Approval Requested Successfuly')", true);
                SaveAlertDetails(ds.Tables[0].Rows[0]["WPONo"].ToString());
            }
            BindWorkOrderPODetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails(string WPONo)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            ds = objc.GetEmployeeIDDetailsByUserTypeIDSANDErpUserType("12", 1);
            string[] str = ds.Tables[0].Rows[0]["EmployeeIDS"].ToString().Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = str[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Vendor DC Alert";
                objAlerts.Message = "Vendor DC Requested No From WO No" + WPONo;
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
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
            objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
            ds = objPc.GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber("LS_GetWorkOrderPOOtherChargesDetailsByWPOID");

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
            objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
            ds = objPc.GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber("LS_GetWorkOrderPOTaxDetails");
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
            objPc.WPOID = Convert.ToInt32(hdnWPOID.Value);
            ds = objPc.GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber("LS_GetWorkOrderPOTotalAmountWithOtherCharges");

            lblPOAmount_T.Text = "Total PO Amount : " + ds.Tables[0].Rows[0]["POAmountWithOtherCharges"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void clearFieldValues()
    {
        try
        {
            hdnWPOID.Value = "0";
            txtIssueDate.Text = "";
            txtQuateReferenceNumber.Text = "";
            txtDeliveryDate.Text = "";
            ddlLocationName.SelectedIndex = 0;
            txtPayment.Text = "";
            txtNote.Text = "";
            txtEnclosure.Text = "";
            txtworemarks.Text = "";
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
        if (gvWorkOrderPO.Rows.Count > 0)
            gvWorkOrderPO.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvWorkOrderIndent.Rows.Count > 0)
            gvWorkOrderIndent.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvPendingIndentListDetails.Rows.Count > 0)
            gvPendingIndentListDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}