using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MaterialInwardQCClearenceList : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cQuality objQt;
    cStores objSt;

    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objPc = new cPurchase();
                objc = new cCommon();

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindAddedMaterials();
            }
            if (target == "PrintMRN")
                bindMRNPrintDetails(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblMRNChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            BindAddedMaterials();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvaddeditems_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCertficateDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlQCStatus = (DropDownList)e.Row.FindControl("ddlCertificateName");

                DataTable dt;

                dt = (DataTable)ViewState["Certificates"];

                ddlQCStatus.DataSource = dt;
                ddlQCStatus.DataTextField = "CertificateName";
                ddlQCStatus.DataValueField = "CFID";
                ddlQCStatus.DataBind();
                ddlQCStatus.Items.Insert(0, new ListItem("--Select--", "0"));

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvcertificatesAddedDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
           
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvQCQtyDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvcertificatesAddedDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDeleteCF");

                if (dr["CertificatesSharedStatus_MID"].ToString() == "0")
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvaddeditems_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
           
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"


    private void BindAddedMaterials()
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.mrnchangestatus = rblMRNChange.SelectedValue;
            ds = objQt.GetInwardedMaterialDetailsAllLocation();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MIQCDetails"] = ds.Tables[0];
                gvaddeditems.DataSource = ds.Tables[0];
                gvaddeditems.DataBind();
            }
            else
            {
                gvaddeditems.DataSource = "";
                gvaddeditems.DataBind();
            }

            if (objSession.type == 1 || objSession.type == 11)
                gvaddeditems.Columns[1].Visible = true;
            else
                gvaddeditems.Columns[1].Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindMRNPrintDetails(int index)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            hdnMIDID.Value = gvaddeditems.DataKeys[index].Values[1].ToString();
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
            ds = objQt.GetMRNDetailsByMRNNumber();

            lblOfficeAddress_p.Text = ds.Tables[2].Rows[0]["Address"].ToString();
            lblOfficePhoneAndFaxNo_p.Text = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            lblOfficeEmail_p.Text = ds.Tables[2].Rows[0]["Email"].ToString();
            lblOfficeWebsite_p.Text = ds.Tables[2].Rows[0]["WebSite"].ToString();

            lblWorkAddress_p.Text = ds.Tables[3].Rows[0]["Address"].ToString();
            lblWorkPhoneAndFaxNo_p.Text = ds.Tables[3].Rows[0]["PhoneAndFaxNo"].ToString();
            lblWorkEmail_p.Text = ds.Tables[3].Rows[0]["Email"].ToString();
            lblWorkWebsite_p.Text = ds.Tables[3].Rows[0]["WebSite"].ToString();

            lblMRNNo_p.Text = ds.Tables[0].Rows[0]["MRNNumber"].ToString();
            lblDate_p.Text = ds.Tables[1].Rows[0]["InspectedOn"].ToString();

            lblSupplierName_p.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();
            lblInVoiceNo_p.Text = ds.Tables[0].Rows[0]["InVoiceNoAndDate"].ToString();
            lblPONoDate_p.Text = ds.Tables[0].Rows[0]["SPONumber"].ToString();
            lblMTR_p.Text = ds.Tables[1].Rows[0]["MTR"].ToString();
            lblTPSNo_p.Text = ds.Tables[1].Rows[0]["TPSNo"].ToString();
            lblVisual_p.Text = ds.Tables[1].Rows[0]["Visual"].ToString();
            lblCheckTest_p.Text = ds.Tables[1].Rows[0]["CheckTest"].ToString();
            lblAddtionalTestRequirtment_p.Text = ds.Tables[1].Rows[0]["AddtionalRequirtment"].ToString();
            lblMeasuredDimension_p.Text = ds.Tables[1].Rows[0]["MeasuredDimension"].ToString();
            lblOriginalMarking_p.Text = ds.Tables[1].Rows[0]["OriginalMarking"].ToString();

            lblReceivedBy_p.Text = ds.Tables[0].Rows[0]["ReceivedBy"].ToString();
            lblReceivedDate_p.Text = ds.Tables[0].Rows[0]["ReceivedDate"].ToString();

            lblInspectedBy_p.Text = ds.Tables[1].Rows[0]["InspectedBy"].ToString();
            lblInspectedDate_p.Text = ds.Tables[1].Rows[0]["InspectedOn"].ToString();

            lblCertificateNo_p.Text = ds.Tables[1].Rows[0]["CertificateNo"].ToString();
            lblPMI_p.Text = ds.Tables[1].Rows[0]["PMI"].ToString();

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

            GeneratePDDF();
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

            divMRNPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

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

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "MRNPrint('" + epstyleurl + "','" + Main + "','" + QrCode + "','" + style + "','" + Print + "','" + topstrip + "');", true);

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
            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("<style type='text/css'/>  .Qrcode{ float: right; } label{ color: black ! important;font-weight: bold;} p{ margin:0;padding:0; }  @page { size: landscape; } span { padding: 0px 0px; } </style>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;margin:0 auto'>");
            w.WriteLine("<div>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
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
        if (gvaddeditems.Rows.Count > 0)
            gvaddeditems.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}