using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using SelectPdf;
using System.Globalization;

public partial class Pages_OTHERDC : System.Web.UI.Page
{
    #region"Declaration"

    cCommon objc;
    cStores objSt;
    cSession objSession = new cSession();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Envents"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            //deleteIDCIssueDetails
            if (!IsPostBack)
            {
                BindOtherDCDetails();
                ShowHideControls("add,addnew,view");
            }
            //if (target == "deleteIDCIssueDetails")
            //{
            //    objSt = new cStores();
            //    int IDCMIDID = Convert.ToInt32(arg);

            //    objSt.IDCMIDID = IDCMIDID;
            //    DataSet ds = objSt.DeleteInternalDCDetailsByIDCID();

            //    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Internal DC Deleted successfully');", true);
            //    else
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            //    BindInternalDCmaterialIssueDetails();
            //}

            if (target == "DeleteInterNalDC")
            {
                objSt = new cStores();
                int IDCID = Convert.ToInt32(arg.ToString());

                objSt.OTHID = IDCID;
                DataSet ds = objSt.DeleteOtherDCByOTHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','OTHER  DC Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "ErrorMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                BindOtherDCDetails();
            }
            if (target == "PrintDC")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "OTHERDCPrint.aspx?OTHID=" + arg.ToString() + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnotherdcDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.OTHID = Convert.ToInt32(hdnOTHID.Value);
            objSt.ItemDescription = txtItemDescription.Text;
            objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objSt.IssuedQty = Convert.ToInt32(txtIssuedQty.Text);

            ds = objSt.SaveOtherDCDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Other DC Item Saved Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Updated Successfully');", true);
            BindOTHERDCIssueDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objSt = new cStores();
        try
        {
            hdnOTHID.Value = "0";
            objSt.bindUnitDetails(ddlFromUnit, null, null);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnOTHID.Value = "0";
            ShowHideControls("add,addnew,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnInternalDC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.OTHID = Convert.ToInt32(hdnOTHID.Value);
            objSt.UMID = Convert.ToInt32(ddlFromUnit.SelectedValue);
            objSt.ToUnitName = txttounitName.Text;
            objSt.ToAddress = txtTounitAddress.Text;
            objSt.DCDate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objSt.Remarks = txtRemarks.Text;
            objSt.CompanyID = Convert.ToInt32(objSession.CompanyID);
            objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objSt.ExpectedDuration = txtexpectedDuration.Text;

            ds = objSt.SaveOTHERDCHeader();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC  Saved Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC Updated Successfully');", true);

            ShowHideControls("add,addnew,view");
            BindOtherDCDetails();
            clearFields();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }


    protected void btnShareDC_Clickk(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            //if (gvInternalDCMaterialIssueDetails.Rows.Count > 0)
            //{
            //    objSt.IDCID = Convert.ToInt32(hdnOTHID.Value);
            //    ds = objSt.UpdateInternalDCStatus();

            //    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC Updated Successfully');CloseIssueMaterialPopUP();", true);
            //    BindOtherDCDetails();
            //}
            //else
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Atleast One Item To Share');", true);
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion

    #region"Grid Events"

    protected void gvOTHERDCDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objSt.OTDID = Convert.ToInt32(gvOTHERDCDetails.DataKeys[index].Values[0].ToString());

            ds = objSt.DeleteOtherDCDetailsByOTDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','DC Delete Successfully');", true);
            BindOTHERDCIssueDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void gvOTHERDC_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            hdnOTHID.Value = gvOTHERDC.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "EditOtherDC")
            {
                DataSet ds = new DataSet();
                objSt = new cStores();
                objSt.bindUnitDetails(ddlFromUnit, null, null);
                objSt.OTHID = Convert.ToInt32(hdnOTHID.Value);

                ds = objSt.GetotherDCDetailsByOTHID();


                txtDCDate.Text = ds.Tables[0].Rows[0]["DCDate"].ToString();
                txttounitName.Text = ds.Tables[0].Rows[0]["ToUnitName"].ToString();
                ddlFromUnit.SelectedValue = ds.Tables[0].Rows[0]["UMID"].ToString();
                txtTounitAddress.Text = ds.Tables[0].Rows[0]["ToUnitAddress"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                txtexpectedDuration.Text = ds.Tables[0].Rows[0]["ExpectedDuration"].ToString();
                ShowHideControls("input");
            }

            if (e.CommandName == "AddOtherDC")
            {
                //  lblDCNumber.Text = "DC No" + lblDCNo.Text;
                BindOTHERDCIssueDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
            }
            else if (e.CommandName == "ReceivedTo")
            {
                // lblReceiverDCNumber.Text = "DC No" + lblDCNo.Text;
                bindDCReceiverDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowReceiverPoPup();", true);
            }
            else if (e.CommandName == "InternalDCPDF")
            {
                //DataSet ds = new DataSet();
                //objSt = new cStores();
                //objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);

                //ds = objSt.getInternalDCAndItemDetailsByDCSharedStatus();

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    //,FormalCompanyName,CINNo

                //    lblfootercompanyname_p.Text = ds.Tables[0].Rows[0]["FooterCompanyName"].ToString();
                //    lblCompanyName_p.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                //    lblFormarlyCompanyName_p.Text = ds.Tables[0].Rows[0]["FormalCompanyName"].ToString();

                //    lblFromUnitAddress_p.Text = ds.Tables[0].Rows[0]["FromUnitAddress"].ToString();
                //    lblDCNo_p.Text = ds.Tables[0].Rows[0]["DCNumber"].ToString();
                //    lblDCDate_p.Text = ds.Tables[0].Rows[0]["DCDate"].ToString();
                //    lblToUnitAddress_p.Text = ds.Tables[0].Rows[0]["ToUnitAddress"].ToString();
                //    lblExpectedDuration_p.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
                //    lblReasonRemarks_p.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                //    lblfootercompanyname_p.Text = "";

                //    gvItemDescriptionDetails_p.DataSource = ds.Tables[1];
                //    gvItemDescriptionDetails_p.DataBind();

                //    lblReceivedBy_p.Text = ds.Tables[0].Rows[0]["ReceiverTo"].ToString();

                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintDC();", true);
                //}
                //else
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Share DC Details')", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvOTHERDC_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (string.IsNullOrEmpty(dr["IssueDetails"].ToString()))
                {
                    e.Row.Attributes.Add("style", "background-color:yellow;");
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

    private void clearFields()
    {
        hdnOTHID.Value = "0";
        txttounitName.Text = "";
        txtTounitAddress.Text = "";
        txtDCDate.Text = "";
        txtexpectedDuration.Text = "";
    }

    private void BindOtherDCDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            ds = objSt.GetOtherDCHeader();

            ViewState["internalDC"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOTHERDC.DataSource = ds.Tables[0];
                gvOTHERDC.DataBind();
            }
            else
            {
                gvOTHERDC.DataSource = "";
                gvOTHERDC.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindOTHERDCIssueDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.OTHID = Convert.ToInt32(hdnOTHID.Value);
            ds = objSt.GetOTHERDCIssueDetailsByOTHID();

            // ViewState["OtherDCIssueDetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOTHERDCDetails.DataSource = ds.Tables[0];
                gvOTHERDCDetails.DataBind();
            }
            else
            {
                gvOTHERDCDetails.DataSource = "";
                gvOTHERDCDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindDCReceiverDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.IDCID = Convert.ToInt32(hdnOTHID.Value);
            ds = objSt.getInternalDCmaterialIssueDetails("LS_GetInternalDCReceivedDetails");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvReceiverDC.DataSource = ds.Tables[0];
                gvReceiverDC.DataBind();
            }
            else
            {
                gvReceiverDC.DataSource = "";
                gvReceiverDC.DataBind();
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

            StringBuilder sbCosting = new StringBuilder();
            divInternalDC_p.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "InternalDC_" + hdnOTHID.Value + ".html";
            string pdffile = "InternalDC_" + hdnOTHID.Value + ".pdf";
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

            SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());

            GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    public void GenerateAndSavePDF(string LetterPath, string strFile, string pdffile, string URL)
    {
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = new PdfDocument();
        try
        {
            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);
            if (File.Exists(strFile))
                File.Delete(strFile);

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 0;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 0;
            converter.Options.MarginBottom = 0;
            converter.Options.WebPageWidth = 700;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;

            doc = converter.ConvertUrl(URL);
            doc.Save(strFile);

            var ms = new System.IO.MemoryStream();

            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdffile);
            HttpContext.Current.Response.TransmitFile(strFile);
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            doc.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            converter = null;
            doc = null;
            LetterPath = null;
            strFile = null;
            URL = null;
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

            w.WriteLine("<style type='text/css'/>  .Qrcode{ float: right; } </style>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;margin:0 auto'>");
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

    #endregion
}