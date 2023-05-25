using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SupplierPOApprovalPendingStatusReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cCommonMaster objcommon;
    c_Finance objFin;
    cSales objSales;
    cMaterials objMat;
    string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
    string PDFHttpPath = ConfigurationManager.AppSettings["PDFPath"].ToString();

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
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (IsPostBack == false)
            {
                objPc = new cPurchase();
                BindSupplierPODetails();
            }
            if (target == "PrintPO")
                bindPurchaseOrderPDFDetails(Convert.ToInt32(arg.ToString()));
            if (target == "PrintAMDPO")
                bindPurchaseOrderAMDPDFDetails(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"Button Events"

    protected void btnApprovalReject_Click(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;

            foreach (GridViewRow dr in gvSupplierPODetails.Rows)
            {
                CheckBox chk = (CheckBox)dr.FindControl("chkitems");
                string SPOID = gvSupplierPODetails.DataKeys[dr.RowIndex].Values[0].ToString();
                if (chk.Checked)
                {
                    if (CommandName == "Approve")
                        objPc.POApprovalStatus = objSession.type == 1 ? 1 : 8;
                    if (CommandName == "Reject")
                        objPc.POApprovalStatus = 9;
                    objPc.SPOID = Convert.ToInt32(SPOID);
                    objPc.Remarks = txtRemarks.Text;
                    objPc.CreatedBy = objSession.employeeid;
                    ds = objPc.UpdatePoApprovalStatusBySPOID("LS_UpdatePOApprovalStatusBySPOID");
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PO " + CommandName + "ed Successfully');", true);
                BindSupplierPODetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvSupplierPODetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int PORevision;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PONumber = gvSupplierPODetails.DataKeys[index].Values[2].ToString();
            if (e.CommandName == "PDF")
                PORevision = Convert.ToInt32(gvSupplierPODetails.DataKeys[index].Values[1].ToString());
            else
                PORevision = Convert.ToInt32(gvSupplierPODetails.DataKeys[index].Values[1].ToString()) - 1;
            string Filename = PONumber + "_" + PORevision.ToString() + ".pdf";
            if (File.Exists(PDFSavePath + Filename))
                cCommon.DownLoad(Filename, PDFHttpPath + Filename);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','File Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSupplierPODetails_OnRowDatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnoldPDF = (LinkButton)e.Row.FindControl("lbtnoldPDF");
                Label lblamdpdf = (Label)e.Row.FindControl("lblamdpdf");

                if (dr["PoRevision"].ToString() == "0")
                    lbtnoldPDF.Visible = false;
                else
                {
                    lbtnoldPDF.Visible = true;
                    lblamdpdf.Attributes.Add("style", "color:brown;");
                    lblamdpdf.Text = "AMD-" + dr["PoRevision"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindSupplierPODetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            ds = objPc.getApprovalPendingSupplierPODetails();

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
                lblsupplierAddress_P.Text = ds.Tables[4].Rows[0]["SupAddress"].ToString();
                lblSupplierName_P.Text = ds.Tables[4].Rows[0]["SupplierName"].ToString();
                lblPhoneNumber_P.Text = ds.Tables[4].Rows[0]["ContactNo"].ToString();
                lblEmail_P.Text = ds.Tables[4].Rows[0]["Emailid"].ToString();
                lblSupGSTNumber_P.Text = ds.Tables[4].Rows[0]["GSTNO"].ToString();

                lblAmdno_p.Text = ds.Tables[4].Rows[0]["PoRevision"].ToString();

                lblPONumber_P.Text = ds.Tables[4].Rows[0]["SPONumber"].ToString();
                lblPOSharedDate_P.Text = ds.Tables[4].Rows[0]["POSharedDate"].ToString();

                lblQuoteReferenceNumber_P.Text = ds.Tables[4].Rows[0]["QuateReferenceNumber"].ToString();
                lblConsigneeAddress_P.Text = ds.Tables[4].Rows[0]["ConsigneeAddress"].ToString();
                lblRemarks_P.Text = ds.Tables[4].Rows[0]["Note"].ToString();

                lblTNGSTNumber_P.Text = ds.Tables[5].Rows[0]["TNGSTNo"].ToString();
                lblCSTNumber_P.Text = ds.Tables[5].Rows[0]["CSTNo"].ToString();
                lblECCNo_P.Text = ds.Tables[5].Rows[0]["ECCNo"].ToString();
                lblTINNumber_P.Text = ds.Tables[5].Rows[0]["TINNo"].ToString();
                lblLonestatrGSTNo.Text = ds.Tables[5].Rows[0]["GSTNumber"].ToString();

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
                lblPaymentTerms_P.Text = ds.Tables[4].Rows[0]["PaymentMode"].ToString();
                lblAmonutInWords_P.Text = ds.Tables[3].Rows[0]["RupeesInWords"].ToString();
                ViewState["PORevision"] = ds.Tables[4].Rows[0]["PoRevision"].ToString();

                lblAddress_h.Text = ds.Tables[7].Rows[0]["Address"].ToString();
                lblPhoneAndFaxNo_h.Text = ds.Tables[7].Rows[0]["PhoneAndFaxNo"].ToString();
                lblEmail_h.Text = ds.Tables[7].Rows[0]["Email"].ToString();
                lblwebsite_h.Text = ds.Tables[7].Rows[0]["WebSite"].ToString();

                gvQualityRequirtments_P.DataSource = ds.Tables[8];
                gvQualityRequirtments_P.DataBind();

                lblDrawingRef_P.Text = ds.Tables[9].Rows[0]["DrawingName"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "POPrint('" + OtherCharges + "','" + Tax + "');", true);

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


    private void bindPurchaseOrderAMDPDFDetails(int index)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        string Tax = "";
        string OtherCharges = "";
        try
        {
            hdnSPOID.Value = gvSupplierPODetails.DataKeys[index].Values[0].ToString();
            objPc.SPOID = Convert.ToInt32(hdnSPOID.Value);
            ds = objPc.GetPurchaseOrderAMDPDFDetaisBySPOID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblsupplierAddress_P.Text = ds.Tables[4].Rows[0]["SupAddress"].ToString();
                lblSupplierName_P.Text = ds.Tables[4].Rows[0]["SupplierName"].ToString();
                lblPhoneNumber_P.Text = ds.Tables[4].Rows[0]["ContactNo"].ToString();
                lblEmail_P.Text = ds.Tables[4].Rows[0]["Emailid"].ToString();
                lblSupGSTNumber_P.Text = ds.Tables[4].Rows[0]["GSTNO"].ToString();

                lblAmdno_p.Text = ds.Tables[4].Rows[0]["PoRevision"].ToString();

                lblPONumber_P.Text = ds.Tables[4].Rows[0]["SPONumber"].ToString();
                lblPOSharedDate_P.Text = ds.Tables[4].Rows[0]["POSharedDate"].ToString();

                lblQuoteReferenceNumber_P.Text = ds.Tables[4].Rows[0]["QuateReferenceNumber"].ToString();
                lblConsigneeAddress_P.Text = ds.Tables[4].Rows[0]["ConsigneeAddress"].ToString();
                lblRemarks_P.Text = ds.Tables[4].Rows[0]["Note"].ToString();

                lblTNGSTNumber_P.Text = ds.Tables[5].Rows[0]["TNGSTNo"].ToString();
                lblCSTNumber_P.Text = ds.Tables[5].Rows[0]["CSTNo"].ToString();
                lblECCNo_P.Text = ds.Tables[5].Rows[0]["ECCNo"].ToString();
                lblTINNumber_P.Text = ds.Tables[5].Rows[0]["TINNo"].ToString();
                lblLonestatrGSTNo.Text = ds.Tables[5].Rows[0]["GSTNumber"].ToString();

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
                lblPaymentTerms_P.Text = ds.Tables[4].Rows[0]["PaymentMode"].ToString();
                lblAmonutInWords_P.Text = ds.Tables[3].Rows[0]["RupeesInWords"].ToString();
                ViewState["PORevision"] = ds.Tables[4].Rows[0]["PoRevision"].ToString();

                lblAddress_h.Text = ds.Tables[7].Rows[0]["Address"].ToString();
                lblPhoneAndFaxNo_h.Text = ds.Tables[7].Rows[0]["PhoneAndFaxNo"].ToString();
                lblEmail_h.Text = ds.Tables[7].Rows[0]["Email"].ToString();
                lblwebsite_h.Text = ds.Tables[7].Rows[0]["WebSite"].ToString();

                gvQualityRequirtments_P.DataSource = ds.Tables[8];
                gvQualityRequirtments_P.DataBind();

                lblDrawingRef_P.Text = ds.Tables[9].Rows[0]["DrawingName"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "POPrint('" + OtherCharges + "','" + Tax + "');", true);

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

    #endregion
}