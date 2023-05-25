using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.IO;
using System.Text;
using System.Configuration;

public partial class Pages_ViewPurchaseIndent : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;
    cPurchase objP;
    cProduction objProd;

    string PurchaseIndentSavePath = ConfigurationManager.AppSettings["PurchaseIndentSavePath"].ToString();
    string PurchaseIndentHttpPath = ConfigurationManager.AppSettings["PurchaseIndentHttpPath"].ToString();

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

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                //objc = new cCommon();
                objMat = new cMaterials();
                objProd = new cProduction();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objProd.GetRFPCustomerNameByMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsRFPHID = objProd.GetRFPDetailsByUserIDAndMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                objMat.getPurchaseEmployeeDetails(ddlIndentTo);
            }
            if (target == "UpdatePIStatus")
                UpdatePIStatus();
            if (target == "ViewIndentCopy")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvViewAllPurchaseIndentDetails.DataKeys[index].Values[1].ToString();
                ViewState["PID"] = gvViewAllPurchaseIndentDetails.DataKeys[index].Values[0].ToString();
                ViewDrawingFilename();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButtonEvents"

    protected void rblpurchase_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        try
        {
            if (rblpurchase.SelectedValue == "General")
            {
                divInput.Visible = false;
            }
            else if (rblpurchase.SelectedValue == "RFP")
            {
                divInput.Visible = true;
                divRFPAndGeneral.Visible = true;
                divAllIndent.Visible = false;
            }
            else if (rblpurchase.SelectedValue == "ViewAllIndent")
            {
                divInput.Visible = false;
                divRFPAndGeneral.Visible = false;
                divAllIndent.Visible = true;
            }
            BindGradeNameByRFPHID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                BindGradeNameByRFPHID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSharePI_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        Label lblPIStatus;
        string PIStatus = "";
        try
        {
            for (int i = 0; i < gvViewPurchaseIndent.Rows.Count; i++)
            {
                lblPIStatus = (Label)gvViewPurchaseIndent.Rows[i].FindControl("lblPIStatus");
                if (lblPIStatus.Text == "Completed")
                    PIStatus = "Completed";
                else
                {
                    PIStatus = "Incomplete";
                    break;
                }
            }
            if (PIStatus == "Incomplete")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','all Grade Should have Completed To Purchase Indent');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RFPStatus", "UpdatePIStatus();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnIndent_OnClick(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        cQRcode objQr = new cQRcode();
        try
        {
            ds = objc.GetAddress();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                //imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "ViewPurchaseIndent";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }

            lblIndentBy_p.Text = ViewState["IndentBy"].ToString();
            lblIndentNo_p.Text = lblIndentNo.Text;
            lblIndentDate_p.Text = ViewState["IndentDate"].ToString();

            //StringBuilder sbAllIndent = new StringBuilder();
            //gvViewAllPurchaseIndentDetails.RenderControl(new HtmlTextWriter(new StringWriter(sbAllIndent)));

            //divPurchaseIndentpdf.InnerHtml = "";
            //divPurchaseIndentpdf.InnerHtml = sbAllIndent.ToString();

            hdnAddress.Value = ds.Tables[0].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[0].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[0].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[0].Rows[0]["WebSite"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintHtmlFile('" + QrCode + "');ShowIndentPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvViewAllIndent_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "ViewIndent")
            {
                hdnQHID.Value = gvViewAllIndent.DataKeys[index].Values[0].ToString();
                lblIndentNo.Text = gvViewAllIndent.DataKeys[index].Values[1].ToString();
                Label lblindentby = (Label)gvViewAllIndent.Rows[index].FindControl("lblIndentBy");

                ViewState["IndentDate"] = gvViewAllIndent.DataKeys[index].Values[2].ToString();
                ViewState["IndentBy"] = lblindentby.Text;

                bindIndentDetailsByIndentNumber();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowIndentPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ViewDrawingFilename()
    {
        try
        {
            objc = new cCommon();

            objc.ViewFileName(PurchaseIndentSavePath, PurchaseIndentHttpPath, ViewState["FileName"].ToString(), ViewState["PID"].ToString(), ifrm);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowIndentPopUp();", true);
            //if (File.Exists(imgname))
            //{
            //    ViewState["ifrmsrc"] = httpPath + ViewState["FileName"].ToString();
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
            //    string s = "window.open('" + httpPath + EnquiryID + "/" + ViewState["FileName"].ToString() + "','_blank');ShowViewPopUP();";
            //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //}
            //else
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);                
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUP();", true);
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void UpdatePIStatus()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        string PID = "";
        try
        {
            if (rblpurchase.SelectedValue == "RFP")
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            else
                objMat.RFPHID = 0;
            foreach (GridViewRow row in gvViewPurchaseIndent.Rows)
            {
                if (PID == "")
                    PID = gvViewPurchaseIndent.DataKeys[row.RowIndex].Values[0].ToString();
                else
                    PID = PID + "," + gvViewPurchaseIndent.DataKeys[row.RowIndex].Values[0].ToString();
            }
            objMat.IndentBy = Convert.ToInt32(objSession.employeeid);
            objMat.IndentTo = Convert.ToInt32(ddlIndentTo.SelectedValue);

            ds = objMat.updatePIStatus(PID);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Purchase Indent Shared Successfully')", true);
                SaveAlertDetails(ds.Tables[0].Rows[0]["IndentNo"].ToString());
                BindGradeNameByRFPHID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails(string IndentNo)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        try
        {
            objAlerts.EntryMode = "Group";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = objSession.employeeid;
            objAlerts.reciverType = "Selected Group";
            objAlerts.file = "";
            objAlerts.reciverID = "0";
            objAlerts.EmailID = "";
            objAlerts.GroupID = 14;
            objAlerts.Subject = "PI Indent Approval Alert";
            objAlerts.Message = "PI Indent Approval Request From Indent No " + IndentNo;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindGradeNameByRFPHID()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objMat.Type = rblpurchase.SelectedValue;
            objMat.IndentBy = Convert.ToInt32(objSession.employeeid);
            ds = objMat.GetItemDetailsByRFPHIDforViewMaterialPlanning("LS_GetGradeNameByRFPHID");

            if (rblpurchase.SelectedValue == "ViewAllIndent")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvViewAllIndent.DataSource = ds.Tables[0];
                    gvViewAllIndent.DataBind();
                }
                else
                {
                    gvViewAllIndent.DataSource = "";
                    gvViewAllIndent.DataBind();
                }
            }
            else
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvViewPurchaseIndent.DataSource = ds.Tables[0];
                    gvViewPurchaseIndent.DataBind();
                    btnSharePI.Visible = true;
                }
                else
                {
                    gvViewPurchaseIndent.DataSource = "";
                    gvViewPurchaseIndent.DataBind();
                    btnSharePI.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindIndentDetailsByIndentNumber()
    {
        DataSet ds = new DataSet();
        objP = new cPurchase();
        try
        {
            objP.QHID = Convert.ToInt32(hdnQHID.Value);
            objP.PageNameFlag = "ViewAll";
            ds = objP.GetPurchaseIndentDetailsByIndentNumber();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvViewAllPurchaseIndentDetails.DataSource = ds.Tables[0];
                gvViewAllPurchaseIndentDetails.DataBind();

                gvViewPurchaseindentAll_p.DataSource = ds.Tables[0];
                gvViewPurchaseindentAll_p.DataBind();
            }
            else
            {
                gvViewAllPurchaseIndentDetails.DataSource = "";
                gvViewAllPurchaseIndentDetails.DataBind();

                gvViewPurchaseindentAll_p.DataSource = "";
                gvViewPurchaseindentAll_p.DataBind();
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
            divPurchaseIndentPrint_p.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "PurchaseIndent_" + ddlRFPNo.SelectedValue + ".html";
            string pdffile = "PurchaseIndent_" + ddlRFPNo.SelectedValue + ".pdf";
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

            // objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);

            objc.ReadhtmlFile(htmlfile, hdnPdfContent);
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
            w.WriteLine("<div class='header' style='border-bottom:1px solid;'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-8 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR INDUSTRIES</h3>");
            w.WriteLine("<div style='font-weight:500;color:#000;width: 100%;font-size:12px'>" + Address + "</div>");
            w.WriteLine("<div style='font-weight:500;color:#000;font-size:12px'>" + PhoneAndFaxNo + "</div>");
            w.WriteLine("<div style='font-weight:500;color:#000;font-size:12px'>" + Email + "</div>");
            w.WriteLine("<div style='font-weight:500;color:#000;font-size:12px'>" + WebSite + "</div>");
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

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvViewPurchaseIndent.Rows.Count > 0)
            gvViewPurchaseIndent.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvViewAllIndent.Rows.Count > 0)
            gvViewAllIndent.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvViewAllPurchaseIndentDetails.Rows.Count > 0)
            gvViewAllPurchaseIndentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}