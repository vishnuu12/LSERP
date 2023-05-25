using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SupplierPOPrint : System.Web.UI.Page
{
    #region"Declaration"

    cPurchase objPc;
    cCommon objc;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hdnSPOID.Value = Request.QueryString["SPOID"].ToString();
                bindPurchaseOrderPDFDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSPOPrint_Click(object sender, EventArgs e)
    {
        bindPurchaseOrderPDFDetails();
    }

    #endregion

    #region"Grid View Events"

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

    #region"Common Events"

    private void bindPurchaseOrderPDFDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        string Tax = "";
        string OtherCharges = "";
        try
        {
            //hdnSPOID.Value = ViewState["SPOID"].ToString();
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

    #endregion
}