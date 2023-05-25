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

public partial class Pages_WorkOrderPOApprovalPendingStatusReport : System.Web.UI.Page
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
        try
        {
            if (IsPostBack == false)
            {
                objPc = new cPurchase();
                BindWorkOrderPODetails();
            }
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

            string WPOIDs = "";

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
            if (CommandName.ToString() == "Approve")
                objPc.WPOApprovalStatus = objSession.type == 1 ? 1 : 8;
            else
                objPc.WPOApprovalStatus = 9;

            objPc.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objPc.UpdatePOApprovalStatusByWPOID("LS_UpdatePOApprovalStatusByWPOID", WPOIDs);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                if (CommandName == "Approve")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','PO Approved Successfuly')", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','PO Rejected Successfuly')", true);
                BindWorkOrderPODetails();
            }
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
            //cCommon.DownLoad(ViewState["FileName"].ToString() + '/' + ViewState["SPOID"].ToString());
            GeneratePDDF();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvWorkOrderPO_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string WPOID = gvWorkOrderPO.DataKeys[index].Values[0].ToString();
            objPc = new cPurchase();
            string Tax = "";
            string OtherCharges = "";
            objPc.WPOID = Convert.ToInt32(WPOID);
            ds = objPc.GetWorkOrderPOByWPOIDAndPOSharedStatusCompleted();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ApprovedTime"] = ds.Tables[0].Rows[0]["WPOL1ApprovedDate"].ToString();
                ViewState["POStatus"] = ds.Tables[0].Rows[0]["POStatus"].ToString();

                lblSupplierChainVendorName_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                lblReceiverAddress_p.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                lblConsigneeAddress_p.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                lblGSTNumber_p.Text = "<label> GST No: </label>" + ds.Tables[0].Rows[0]["GSTNO"].ToString();
                lblRange_p.Text = ds.Tables[0].Rows[0]["Range"].ToString();

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

                lblTNGSTNo.Text = "<label> TNGST No: </label>" + ds.Tables[3].Rows[0]["TNGSTNo"].ToString();
                lblCSTNo.Text = "<label> CST No: </label>" + ds.Tables[3].Rows[0]["CSTNo"].ToString();
                lblECCNo.Text = "<label> ECC No: </label>" + ds.Tables[3].Rows[0]["ECCNo"].ToString();
                lblTINNo.Text = "<label> TIN No: </label>" + ds.Tables[3].Rows[0]["TINNo"].ToString();
                lblGSTNo.Text = "<label> GST No: </label>" + ds.Tables[3].Rows[0]["GSTNumber"].ToString();

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

            //  ViewState["SpoNo"] = dt.DefaultView.ToTable().Rows[0]["SPONumber"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Metghods"

    private void BindWorkOrderPODetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            ds = objPc.GetWorkOrderPOApprovalDetails();

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

            // SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());
            // objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintWorkOrderPO('" + ViewState["ApprovedTime"].ToString() + "','" + ViewState["POStatus"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string Main, string epstyleurl, string style, string Print, string topstrip, string div)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            StreamWriter w;
            w = File.CreateText(URL);
            //w.WriteLine("<html><head><title>");       
            w.WriteLine("<html><head><title>");
            w.WriteLine("Offer");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
            w.WriteLine("<div class='print-page'>");
            w.WriteLine("<table><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            w.WriteLine("<div class='header' style='border-bottom:1px solid;left:5mm !important;background:transparent'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-8 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR INDUSTRIES </h3>");
            w.WriteLine("<p style='font-weight:500;color:#000;width: 100%;'>" + Address + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");
            w.WriteLine("<div class='col-sm-12 padding:0' style='padding-top:0px;'>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot><tr><td>");
            w.WriteLine("<div class='footer-space'>");
            w.WriteLine("<div class='footer'>");
            w.WriteLine("<div class='col-sm-12' style='border:1px solid #000;padding-top:10px'>");
            w.WriteLine("<div class='text-center' style='color:#000'>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OFACCEPTANCE</div>");
            w.WriteLine("<div class='col-sm-6' style='height:100px;display:flex;align-items:flex-end;justify-content:flex-start'>");
            w.WriteLine("<label style='padding-top:60px;padding-left:15px'>PREPARED & CHECKED BY</label>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-6' style='height:100px;display:flex;align-items:center;justify-content:space-around;flex-direction:column;text-align:center'>");
            w.WriteLine("<label style='display:block;width:100%;padding-top:10px'>For LONESTAR INDUSTRIES</label>");
            w.WriteLine("<label style='display:block;width:100%;padding-top:60px'>AUTHORISED SIGNATORY</label>");
            w.WriteLine("</div>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tfoot></table>");
            w.WriteLine("</div>");
            w.WriteLine("</html>");

            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}