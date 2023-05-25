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
using System.Globalization;

public partial class Pages_GRNPrintDetailsNew : System.Web.UI.Page
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
                bindGRNPrintDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"

    protected void btnGRNPrint_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion

    #region"Common Methods"

    private void bindGRNPrintDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MIDID = Convert.ToInt32(ViewState["MIDID"].ToString());
            ds = objQt.GetGRNDetailsByGRNNumber();


            lblCompanyName_h.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
            lblAddress_h.Text = ds.Tables[0].Rows[0]["Address"].ToString();

            lblPhoneAndFaxNo_h.Text = ds.Tables[0].Rows[0]["PhoneAndFaxNo"].ToString();
            lblEmail_h.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            lblwebsite_h.Text = ds.Tables[0].Rows[0]["WebSite"].ToString();

            lblMTR_p.Text = ds.Tables[1].Rows[0]["MTR"].ToString();
            lblVisual_p.Text = ds.Tables[1].Rows[0]["Visual"].ToString();
            lblCheckTest_p.Text = ds.Tables[1].Rows[0]["CheckTest"].ToString();
            lblAddtionalTestRequirtment_p.Text = ds.Tables[1].Rows[0]["AddtionalRequirtment"].ToString();
            lblMeasuredDimension_p.Text = ds.Tables[1].Rows[0]["MeasuredDimension"].ToString();
            lblQualityInspection_p.Text = ds.Tables[1].Rows[0]["QuaityMarketing"].ToString();
            lblPMI_p.Text = ds.Tables[1].Rows[0]["PMI"].ToString();
            lblGrnNo_p.Text = ds.Tables[1].Rows[0]["GRNNumber"].ToString();
            lblGrnDate_p.Text = ds.Tables[1].Rows[0]["Created_On"].ToString();


            lblSupplierName_p.Text = ds.Tables[2].Rows[0]["SupplierName"].ToString();
            lblReceivedBy_p.Text = ds.Tables[2].Rows[0]["ReceivedBy"].ToString();
            lblReceivedDate_p.Text = ds.Tables[2].Rows[0]["ReceivedDate"].ToString();
            lblInVoiceNo_p.Text = ds.Tables[2].Rows[0]["InVoiceNoAndDate"].ToString();
            lblPONoDate_p.Text = ds.Tables[2].Rows[0]["SPONumber"].ToString();


            if (ds.Tables[2].Rows.Count > 0)
            {
                gvQCClearedDetails.DataSource = ds.Tables[2];
                gvQCClearedDetails.DataBind();
            }
            else
            {
                gvQCClearedDetails.DataSource = "";
                gvQCClearedDetails.DataBind();
            }


            lblInspectedBy_p.Text = ds.Tables[3].Rows[0]["InspectedBy"].ToString();
            lblInspectedDate_p.Text = ds.Tables[3].Rows[0]["InspectedOn"].ToString();

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

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "GRNPrint('" + epstyleurl + "','" + Main + "','" + style + "','" + Print + "','" + topstrip + "');", true);

            // objc.ReadhtmlFile(htmlfile, hdnPdfContent);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}