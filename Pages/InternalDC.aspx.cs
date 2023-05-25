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

public partial class Pages_InternalDC : System.Web.UI.Page
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
                bindInternalDCDetails();
                ShowHideControls("add,addnew,view");
            }
            if (target == "deleteIDCIssueDetails")
            {
                objSt = new cStores();
                int IDCMIDID = Convert.ToInt32(arg);

                objSt.IDCMIDID = IDCMIDID;
                DataSet ds = objSt.DeleteInternalDCDetailsByIDCID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Internal DC Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                BindInternalDCmaterialIssueDetails();
            }

            if (target == "DeleteInterNalDC")
            {
                objSt = new cStores();
                int IDCID = Convert.ToInt32(arg.ToString());

                objSt.IDCID = IDCID;
                DataSet ds = objSt.DeleteInternalDCByIDCID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Internal DC Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                bindInternalDCDetails();
            }
            if (target == "PrintDC")
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "InternalDCPrint.aspx?IDCID=" + arg.ToString() + "");

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

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objSt = new cStores();
        try
        {
            hdnIDCID.Value = "0";
            objSt.bindUnitDetails(ddlFromUnit, ddlToUnit, ddlReceivedTo);
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
            hdnIDCID.Value = "0";
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
            objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);
            objSt.FromUnit = Convert.ToInt32(ddlFromUnit.SelectedValue);
            objSt.ToUnit = Convert.ToInt32(ddlToUnit.SelectedValue);
            objSt.Duration = Convert.ToInt32(txtDuration.Text);
            objSt.Remarks = txtRemarks.Text;
            objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objSt.ReceiverID = Convert.ToInt32(ddlReceivedTo.SelectedValue);
            objSt.CompanyID = Convert.ToInt32(objSession.CompanyID);

            ds = objSt.SaveInternalDCDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC  Saved Successfully');", true);
                bindInternalDCDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC Updated Successfully');", true);

            ShowHideControls("add,addnew,view");
            bindInternalDCDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnMaterialIssue_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);
            objSt.IDCMIDID = Convert.ToInt32(hdnIDCMIDID.Value);
            objSt.ItemDescription = txtItemDescription.Text;
            objSt.IssuedQty = Convert.ToInt32(txtIssuedQty.Text);
            objSt.ReturnableQty = Convert.ToInt32(txtreturnableQty.Text);

            ds = objSt.SaveInternalDCmaterialIssueDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC  Saved Successfully');", true);
                BindInternalDCmaterialIssueDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC Updated Successfully');", true);

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
            if (gvInternalDCMaterialIssueDetails.Rows.Count > 0)
            {
                objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);
                ds = objSt.UpdateInternalDCStatus();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Internal DC Updated Successfully');CloseIssueMaterialPopUP();", true);
                bindInternalDCDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Atleast One Item To Share');", true);
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnReceived_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        DataTable dt = new DataTable();
        try
        {
            DataRow dr;

            dt.Columns.Add("IDCMIDID");
            dt.Columns.Add("ReceivedQty");
            dt.Columns.Add("CreatedBy");
            dt.Columns.Add("Remarks");

            foreach (GridViewRow row in gvReceiverDC.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                TextBox txtReceivedQty = (TextBox)gvReceiverDC.Rows[row.RowIndex].FindControl("txtReceivedQty");
                TextBox txtRemarks = (TextBox)gvReceiverDC.Rows[row.RowIndex].FindControl("txtRemarks");

                string IDCMIDID = gvReceiverDC.DataKeys[row.RowIndex].Values[0].ToString();
                if (chkditems.Checked)
                {
                    dr = dt.NewRow();
                    dr["IDCMIDID"] = Convert.ToInt32(IDCMIDID);
                    dr["ReceivedQty"] = txtReceivedQty.Text;
                    dr["CreatedBy"] = objSession.employeeid;
                    dr["Remarks"] = txtRemarks.Text;

                    dt.Rows.Add(dr);
                }
            }
            objSt.dt = dt;
            ds = objSt.SaveReceivedDCDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Records Saved  Successfully');HideReceiverPopUp();", true);
            bindDCReceiverDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion

    #region"Grid Events"

    protected void gvInternalDC_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            Label lblDCNo = (Label)gvInternalDC.Rows[index].FindControl("lblDCNo");

            hdnIDCID.Value = gvInternalDC.DataKeys[index].Values[0].ToString();
            if (e.CommandName == "AddInternalDC")
            {
                lblDCNumber.Text = "DC No" + lblDCNo.Text;
                BindInternalDCmaterialIssueDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
            }
            else if (e.CommandName == "ReceivedTo")
            {
                lblReceiverDCNumber.Text = "DC No" + lblDCNo.Text;
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

    protected void gvInternalDC_OnRowDataBound(object sender, GridViewRowEventArgs e)
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


    protected void gvInternalDCMaterialIssueDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //Set the edit index.
            gvInternalDCMaterialIssueDetails.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindInternalDCmaterialIssueDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvInternalDCMaterialIssueDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvInternalDCMaterialIssueDetails.EditIndex = -1;
            BindInternalDCmaterialIssueDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvInternalDCMaterialIssueDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            TextBox txtReturnQty = ((TextBox)gvInternalDCMaterialIssueDetails.Rows[e.RowIndex].FindControl("txtReturnedQty"));

            if (txtReturnQty.Text != "")
            {
                objSt.IDCMIDID = Convert.ToInt32(gvInternalDCMaterialIssueDetails.DataKeys[e.RowIndex].Values[0].ToString());
                objSt.ReturnableQty = Convert.ToInt32(txtReturnQty.Text);
                ds = objSt.UpdateinternalDCMaterialIssueDetailsByIDCMIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Updated Successfully');", true);

                gvInternalDCMaterialIssueDetails.EditIndex = -1;
                BindInternalDCmaterialIssueDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "ErrorMessage('Error !','Field required');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }



    protected void gvInternalDCMaterialIssueDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                e.Row.Cells[5].ToolTip = "Edit";
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (e.Row.Cells.GetCellIndex(cell) == 5)
                        {
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[10].Controls[0])).ToolTip = "Update";
                            ((System.Web.UI.LiteralControl)(e.Row.Cells[10].Controls[1])).Text = "&nbsp;";
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[10].Controls[2])).ToolTip = "Close";
                        }
                    }
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

    private void bindInternalDCDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.UserID = Convert.ToInt32(objSession.employeeid);
            ds = objSt.getInternalDCDetails();
            ViewState["internalDC"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvInternalDC.DataSource = ds.Tables[0];
                gvInternalDC.DataBind();
            }
            else
            {
                gvInternalDC.DataSource = "";
                gvInternalDC.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindInternalDCmaterialIssueDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);
            ds = objSt.getInternalDCmaterialIssueDetails("LS_GetInternalDCmaterialIssueDetailsByIDCID");

            ViewState["internalDCMaterialIssue"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvInternalDCMaterialIssueDetails.DataSource = ds.Tables[0];
                gvInternalDCMaterialIssueDetails.DataBind();
            }
            else
            {
                gvInternalDCMaterialIssueDetails.DataSource = "";
                gvInternalDCMaterialIssueDetails.DataBind();
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
            objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);
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

            string htmlfile = "InternalDC_" + hdnIDCID.Value + ".html";
            string pdffile = "InternalDC_" + hdnIDCID.Value + ".pdf";
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