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

public partial class Pages_MRNPrintDetails : System.Web.UI.Page
{
    #region "Declaration"

    cQuality objQt;
    cCommon objc;
    cSession objSession = new cSession();

    #endregion

    #region "Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["MIDID"] = Request.QueryString["MIDID"].ToString();
                bindMRNPrintDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"

    protected void btnMRNPrint_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion

    #region"Common Methods"

    private void bindMRNPrintDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MIDID = Convert.ToInt32(ViewState["MIDID"].ToString());
            ds = objQt.GetMRNDetailsByMRNNumber();

            //lblOfficeAddress_p.Text = ds.Tables[2].Rows[0]["Address"].ToString();
            //lblOfficePhoneAndFaxNo_p.Text = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            //lblOfficeEmail_p.Text = ds.Tables[2].Rows[0]["Email"].ToString();
            //lblOfficeWebsite_p.Text = ds.Tables[2].Rows[0]["WebSite"].ToString();

         
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQCClearedDetails.DataSource = ds.Tables[0];
                gvQCClearedDetails.DataBind();
            }
            else
            {
                gvQCClearedDetails.DataSource = "";
                gvQCClearedDetails.DataBind();
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                gvInstrumentDetails_p.DataSource = ds.Tables[4];
                gvInstrumentDetails_p.DataBind();
            }
            else
            {
                gvInstrumentDetails_p.DataSource = "";
                gvInstrumentDetails_p.DataBind();
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                lblDocNo_p.Text = ds.Tables[5].Rows[0]["DocNo"].ToString();
                lblRevNo_p.Text = ds.Tables[5].Rows[0]["RevNo"].ToString();
                lblISODate_p.Text = ds.Tables[5].Rows[0]["ISODate"].ToString();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMRNNo_p.Text = ds.Tables[0].Rows[0]["MRNNumber"].ToString();
                lblcompanyname.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();

                lblOfficeAddress_p.Text = ds.Tables[0].Rows[0]["RegisteredAddress"].ToString();
                lblOfficePhoneAndFaxNo_p.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString() + "/" + ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                lblOfficeEmail_p.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                lblOfficeWebsite_p.Text = ds.Tables[0].Rows[0]["WebSite"].ToString();
                lblSupplierName_p.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                lblInVoiceNo_p.Text = ds.Tables[0].Rows[0]["InVoiceNoAndDate"].ToString();
                lblPONoDate_p.Text = ds.Tables[0].Rows[0]["SPONumber"].ToString();
                lblReceivedBy_p.Text = ds.Tables[0].Rows[0]["ReceivedBy"].ToString();
                lblReceivedDate_p.Text = ds.Tables[0].Rows[0]["ReceivedDate"].ToString();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                lblWorkAddress_p.Text = ds.Tables[3].Rows[0]["Address"].ToString();
                lblWorkPhoneAndFaxNo_p.Text = ds.Tables[3].Rows[0]["PhoneAndFaxNo"].ToString();
                lblWorkEmail_p.Text = ds.Tables[3].Rows[0]["Email"].ToString();
                lblWorkWebsite_p.Text = ds.Tables[3].Rows[0]["WebSite"].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                lblDate_p.Text = ds.Tables[1].Rows[0]["InspectedOn"].ToString();
                lblMTR_p.Text = ds.Tables[1].Rows[0]["MTR"].ToString();
                lblTPSNo_p.Text = ds.Tables[1].Rows[0]["TPSNo"].ToString();
                lblVisual_p.Text = ds.Tables[1].Rows[0]["Visual"].ToString();
                lblCheckTest_p.Text = ds.Tables[1].Rows[0]["CheckTest"].ToString();
                lblAddtionalTestRequirtment_p.Text = ds.Tables[1].Rows[0]["AddtionalRequirtment"].ToString();
                lblMeasuredDimension_p.Text = ds.Tables[1].Rows[0]["MeasuredDimension"].ToString();
                lblOriginalMarking_p.Text = ds.Tables[1].Rows[0]["OriginalMarking"].ToString();
                lblInspectedBy_p.Text = ds.Tables[1].Rows[0]["InspectedBy"].ToString();
                lblInspectedDate_p.Text = ds.Tables[1].Rows[0]["InspectedOn"].ToString();

                lblCertificateNo_p.Text = ds.Tables[1].Rows[0]["CertificateNo"].ToString();
                lblPMI_p.Text = ds.Tables[1].Rows[0]["PMI"].ToString();
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
            StringBuilder sbCosting = new StringBuilder();

            // divMRNPrint.Attributes.Add("style", "display:block;");

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                imgQrcode.Attributes.Add("style", "display:block;");
                imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "InternalDCPrint";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                imgQrcode.Attributes.Add("style", "display:none;");

            //  divMRNPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "MRNReport_" + lblMRNNo_p.Text + ".html";
            string pdffile = "MRNReport_" + lblMRNNo_p.Text + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string pdfFileURL = LetterPath + pdffile;

            string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            // SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());

            //objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);

            //  divMRNPrint.Attributes.Add("style", "display:none;");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "MRNPrint('" + epstyleurl + "','" + Main + "','" + QrCode + "','" + style + "','" + Print + "','" + topstrip + "');", true);

            // objc.ReadhtmlFile(htmlfile, hdnPdfContent);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}