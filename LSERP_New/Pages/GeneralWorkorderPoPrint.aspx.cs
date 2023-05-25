using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralWorkorderPoPrint : System.Web.UI.Page
{
    #region"Declaration"

    cPurchase objPc;
    cCommon objc;
    GeneralWorkOrderIndentApproval gwoia;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["GDCID"] = Request.QueryString["GDCID"].ToString();
                GeneralWorkOrderPOPrint();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnGeneralWorkOrderPrint_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion

    #region"Common Events"

    private void GeneralWorkOrderPOPrint()
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        objPc = new cPurchase();
        string Tax = "";
        string OtherCharges = "";
        try
        {
            gwoia.WPOID = Convert.ToInt32(ViewState["GDCID"].ToString());
            ds = gwoia.GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompletedReports();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ApprovedTime"] = ds.Tables[0].Rows[0]["GWPOL1ApprovedDate"].ToString();
                ViewState["POStatus"] = ds.Tables[0].Rows[0]["POStatus"].ToString();
                ViewState["ApprovedBy"] = ds.Tables[0].Rows[0]["L1ApprovedBy"].ToString();

                lblNote_p.Text = ds.Tables[0].Rows[0]["Note"].ToString();
                lblSupplierChainVendorName_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                lblReceiverAddress_p.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                lblConsigneeAddress_p.Text = lblConsigneeAddressRev1_p.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                lblGSTNumber_p.Text = "<label> GST No: </label>" + ds.Tables[0].Rows[0]["GSTNO"].ToString();
                lblRange_p.Text = ds.Tables[0].Rows[0]["Range"].ToString();

                lblWoNo_p.Text = ds.Tables[0].Rows[0]["GWonumber"].ToString();
                ViewState["WPONo"] = ds.Tables[0].Rows[0]["GWonumber"].ToString();
                ViewState["PORevision"] = ds.Tables[0].Rows[0]["PORevision"].ToString();

                lblWODate_p.Text = ds.Tables[0].Rows[0]["POCreated_On"].ToString();
                //lblRFPNo_p.Text = ds.Tables[1].Rows[0]["RFPNo"].ToString();
                lblQuoteRefNo_p.Text = ds.Tables[0].Rows[0]["QuateReferenceNumber"].ToString();
                lblDeliveryDate_p.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                lblPayment_p.Text = ds.Tables[0].Rows[0]["PaymentDays"].ToString(); 

                lblSubTotal_p.Text = ds.Tables[5].Rows[0]["SubTotal"].ToString();
                lblTotalAmount_p.Text = ds.Tables[5].Rows[0]["TotalAmount"].ToString();

                lblRupeesInwords_p.Text = ds.Tables[0].Rows[0]["AmountInRupees"].ToString();

                gvGeneralWorkOrderPOItemDetails_p.DataSource = ds.Tables[6];
                gvGeneralWorkOrderPOItemDetails_p.DataBind();

                if (ds.Tables[2].Rows[0]["CompanyID"].ToString() == "1")
                {
                    divGSTREV0.Visible = true;
                    divGSTREV1.Visible = false;

                    lblTNGSTNo.Text = "<label> TNGST No: </label>" + ds.Tables[2].Rows[0]["TNGSTNo"].ToString();
                    lblCSTNo.Text = "<label> CST No: </label>" + ds.Tables[2].Rows[0]["CSTNo"].ToString();
                    lblECCNo.Text = "<label> ECC No: </label>" + ds.Tables[2].Rows[0]["ECCNo"].ToString();
                    lblTINNo.Text = "<label> TIN No: </label>" + ds.Tables[2].Rows[0]["TINNo"].ToString();
                    lblGSTNo.Text = "<label> GST No: </label>" + ds.Tables[2].Rows[0]["GSTNumber"].ToString();
                }
                else
                {
                    divGSTREV0.Visible = false;
                    divGSTREV1.Visible = true;
                    lblCINNo_P.Text = "<label> CIN No: </label>" + ds.Tables[2].Rows[0]["CINNo"].ToString();
                    lblPANNo_p.Text = "<label> PAN No: </label>" + ds.Tables[2].Rows[0]["PANNumber"].ToString();
                    lblTANNo_p.Text = "<label> TAN No: </label>" + ds.Tables[2].Rows[0]["TANNo"].ToString();
                    lblGSTIN_p.Text = "<label> GSTIN No: </label>" + ds.Tables[2].Rows[0]["GSTNumber"].ToString();
                }


                ViewState["Address"] = ds.Tables[2];

                StringBuilder sbCharges;
                string sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                sbCharges = new StringBuilder();
                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                    sb = sb.Replace("lblchargesname", dr["ChargesName"].ToString());
                    sb = sb.Replace("textname", dr["Value"].ToString());
                    sbCharges.Append(sb);
                }
                divothercharges_p.InnerHtml = sbCharges.ToString();

                sbCharges = new StringBuilder();
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    sb = "<div class=\"m-t-10\" style=\"width: 100%; float: left\"><label style = \"float: left;\" class=\"lbltaxothercharges\" >lblchargesname</label><span  Style=\"float: right;\" class=\"spntaxothercharges\">textname</span></div>";
                    sb = sb.Replace("lblchargesname", dr["TaxName"].ToString() + " @ " + dr["Per"].ToString().Replace(".00", "") + " % ");
                    sb = sb.Replace("textname", dr["Amount"].ToString());
                    sbCharges.Append(sb);
                }
                divtax_p.InnerHtml = sbCharges.ToString();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Please Add  Item  Details');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintJobDC('" + epstyleurl + "','" + Main + "','" + style + "','" + Print + "','" + topstrip + "','" + ViewState["POStatus"].ToString() + "','" + ViewState["ApprovedBy"].ToString() + "','" + ViewState["ApprovedTime"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}