using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralWorkOrderPO : System.Web.UI.Page
{

    #region"Declaration"

    GeneralWorkOrderIndentApproval gwoia;
    cSession objSession = new cSession();
    string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
    string PDFHttpPath = "http://183.82.33.21/LSERPDocs/PDFFiles/GSTDocs/";

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                GeneralWorkOrderIndentPODetails();
                BindDropDownDetails();
                ShowHideControls("view");

            }
            if (target == "ViewIndentFile")
            {
                int index = Convert.ToInt32(arg);
                ViewState["GWOID"] = gvGeneralWorkOrderPO.DataKeys[index].Values[0].ToString();
                ViewState["FileName"] = gvGeneralWorkOrderPO.DataKeys[index].Values[2].ToString();
                ViewDrawingFilename();
            }
            if (target == "SharePO")
            {
                DataSet ds = new DataSet();
                gwoia = new GeneralWorkOrderIndentApproval();
                gwoia.SGWOID = Convert.ToInt32(arg);
                ds = gwoia.ShareGeneralWorkOrderPoDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','PO Shared Successfully');ShowTaxPopUp();", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideTaxPopUp", "HideTaxPopUp();", true);

                GeneralWorkOrderIndentPODetails();
            }
            if (target == "ViewIndentAttach")
            {
                int index = Convert.ToInt32(arg);
                ViewState["GWOID"] = gvGeneralWorkOrderPO.DataKeys[index].Values[0].ToString();
                ViewState["FileName"] = gvGeneralWorkOrderPO.DataKeys[index].Values[1].ToString();
                ViewDrawingFilename();
            }
            if (target == "deletegvRowotherCharges")
            {
                DataSet ds = new DataSet();
                gwoia = new GeneralWorkOrderIndentApproval();
                int index = Convert.ToInt32(arg);
                gwoia.ChargesID = Convert.ToInt32(arg);
                gwoia.PageNameFlag = "Othercharges";

                ds = gwoia.DeleteGeneralWorkOrderPOtaxAndOtherChargesDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
                BindOtherChargesDetails(index);
                GeneralWorkOrderIndentPODetails();

            }
            if (target == "deletegvRowtaxCharges")
            {
                DataSet ds = new DataSet();
                gwoia = new GeneralWorkOrderIndentApproval();
                int index = Convert.ToInt32(arg);
                gwoia.ChargesID = Convert.ToInt32(arg);
                gwoia.PageNameFlag = "Tax";

                ds = gwoia.DeleteGeneralWorkOrderPOtaxAndOtherChargesDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Value Deleted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
                BindTaxChargesDetails(index);
                GeneralWorkOrderIndentPODetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    #endregion

    #region"Button Events"

    protected void btnSave_ClickPO(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.GWOPOID = Convert.ToInt32(hdnGWOPOID.Value);
            gwoia.ddlSupplier = Convert.ToInt32(ddlSupplier.SelectedValue);
            gwoia.ddlLocation = Convert.ToInt32(ddlLocation.SelectedValue);
            gwoia.txtDelivery = txtDelivery.Text;
            gwoia.txtQuantity = txtQuantity.Text;
            gwoia.txtUnitAmount = txtUnitCost.Text;
            gwoia.txtPayment = txtPayment.Text;
            gwoia.txtNote = txtNote.Text;
            gwoia.txtQuote = txtQuote.Text;
            gwoia.txtRemark = txtRemark.Text;
            ds = gwoia.SaveGeneralWorkOrderPOData();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Succeessfully');", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "ERROR")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Please Add Tax Details');hideLoader();", true);
            ClearValues();
            ShowHideControls("view");
            GeneralWorkOrderIndentPODetails();
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
            ShowHideControls("view");
            ClearValues();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveTaxAndOtherCharges_Click(object sender, EventArgs e)
    {
        try
        {
            gwoia = new GeneralWorkOrderIndentApproval();
            DataSet ds = new DataSet();
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;
            if (CommandName == "Othercharges")
            {
                if (txtOtherCharges_t.Text == "," || Convert.ToInt32(ddlOtherCharges_t.SelectedValue) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Please Add Details');ShowTaxPopUp();OpenTab('OtherCharges');", true);
                }
                else
                {
                    gwoia.ChargesID = Convert.ToInt32(ddlOtherCharges_t.SelectedValue);
                    gwoia.ChargesValue = Convert.ToDecimal(txtOtherCharges_t.Text);
                    gwoia.ChargesType = CommandName;
                    gwoia.SGWOID = Convert.ToInt32(hdnGWOPOID.Value);
                    gwoia.GWOPOID = Convert.ToInt32(hdnGWOID.Value);
                    ds = gwoia.SaveGeneralWorkOrderPOTaxAndOtherChargesDetails();
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','" + CommandName + " Saved Successfully');", true);


                        GeneralWorkOrderIndentPODetails();
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
                }

            }
            else if (CommandName == "Tax")
            {
                if (txtTaxValue_t.Text == "," || Convert.ToInt32(ddlTax_t.SelectedValue) == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Please Add Details');ShowTaxPopUp();OpenTab('Tax');", true);
                }
                else
                {
                    gwoia.ChargesID = Convert.ToInt32(ddlTax_t.SelectedValue);
                    gwoia.ChargesValue = Convert.ToDecimal(txtTaxValue_t.Text);
                    gwoia.ChargesType = CommandName;
                    gwoia.SGWOID = Convert.ToInt32(hdnGWOPOID.Value);
                    gwoia.GWOPOID = Convert.ToInt32(hdnGWOID.Value);
                    ds = gwoia.SaveGeneralWorkOrderPOTaxAndOtherChargesDetails();
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','" + CommandName + " Saved Successfully');", true);
                        GeneralWorkOrderIndentPODetails();

                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowTaxPopUp();", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
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


    #region"Grid Events"

    protected void gvGeneralWorkOrderPO_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        gwoia = new GeneralWorkOrderIndentApproval();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnGWOPOID.Value = gvGeneralWorkOrderPO.DataKeys[index].Values[0].ToString();
            hdnGWOID.Value = gvGeneralWorkOrderPO.DataKeys[index].Values[1].ToString();
            gwoia.GWOPOID = Convert.ToInt32(hdnGWOPOID.Value);
            gwoia.GWOID = Convert.ToInt32(hdnGWOID.Value);
            if (e.CommandName == "AddTax")
            {
                txtOtherCharges_t.Text = "";
                txtTaxValue_t.Text = "";
                gwoia.BindChargesDetails(ddlOtherCharges_t, ddlTax_t);

                int sid = gwoia.GWOPOID;

                BindPOTotalAmount(sid);
                BindTaxChargesDetails(sid);
                BindOtherChargesDetails(sid);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowTaxPopUp();OpenTab('OtherCharges');", true);
            }

            if (e.CommandName.ToString() == "AddSPO")
            {
                BindEditDetails(gwoia.GWOPOID);
                if (this.gvGeneralWorkOrderPO.Rows.Count > 0)
                {
                    gvGeneralWorkOrderPO.UseAccessibleHeader = true;
                    gvGeneralWorkOrderPO.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
                ShowHideControls("edit");
            }
            //   <!--------------------------- >       //


            if (e.CommandName == "PDF")
            {
                gwoia = new GeneralWorkOrderIndentApproval();
                string Tax = "";
                string OtherCharges = "";
                gwoia.WPOID = Convert.ToInt32(hdnGWOPOID.Value);
                ds = gwoia.GetGeneralWorkOrderPOByWPOIDAndPOSharedStatusCompleted();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["ApprovedTime"] = ds.Tables[0].Rows[0]["GWPOL1ApprovedDate"].ToString();
                    ViewState["POStatus"] = ds.Tables[0].Rows[0]["POStatus"].ToString();
                    ViewState["ApprovedBy"] = ds.Tables[0].Rows[0]["GWPOL1ApprovedBy"].ToString();

                    lblNote_p.Text = ds.Tables[0].Rows[0]["Note"].ToString();
                    lblSupplierChainVendorName_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
                    lblReceiverAddress_p.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    lblConsigneeAddress_p.Text = lblConsigneeAddressRev1_p.Text = ds.Tables[0].Rows[0]["ConsigneeAddress"].ToString();
                    lblGSTNumber_p.Text = "<label> GST No: </label>" + ds.Tables[0].Rows[0]["GSTNO"].ToString();
                    lblRange_p.Text = ds.Tables[0].Rows[0]["Range"].ToString();

                    //lblAMDReson_p.Text = ds.Tables[0].Rows[0]["GWPOAmendmentReason"].ToString();
                    //lblAMDDate_p.Text = ds.Tables[0].Rows[0]["GWPOAmendmentOn"].ToString();

                    lblWoNo_p.Text = ds.Tables[0].Rows[0]["GWonumber"].ToString();
                    ViewState["WPONo"] = ds.Tables[0].Rows[0]["GWonumber"].ToString();
                    ViewState["PORevision"] = ds.Tables[0].Rows[0]["PORevision"].ToString();

                    //lblWODate_p.Text = "NIL";
                    //lblRFPNo_p.Text = "NIL";
                    lblWODate_p.Text = ds.Tables[0].Rows[0]["POCreated_On"].ToString();
                    lblQuoteRefNo_p.Text = ds.Tables[0].Rows[0]["QuateReferenceNumber"].ToString();
                    lblDeliveryDate_p.Text = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                    lblPayment_p.Text = ds.Tables[0].Rows[0]["PaymentDays"].ToString();

                    lblSubTotal_p.Text = ds.Tables[5].Rows[0]["SubTotal"].ToString();
                    lblTotalAmount_p.Text = ds.Tables[5].Rows[0]["TotalAmount"].ToString();

                    lblRupeesInwords_p.Text = ds.Tables[0].Rows[0]["AmountInRupees"].ToString();

                    gvWorkOrderPOItemDetails_p.DataSource = ds.Tables[6];
                    gvWorkOrderPOItemDetails_p.DataBind();

                    if (ds.Tables[1].Rows[0]["CompanyID"].ToString() == "1")
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPoPrintPopUp();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Please Add  Item  Details');", true);

                ViewState["SpoNo"] = dt.DefaultView.ToTable().Rows[0]["SPONumber"].ToString();
            }

            //<!------------------------------------ >  //


        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void GeneralWorkOrderSubIndent_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnGWOID1.Value = GeneralWorkOrderSubIndent.DataKeys[index].Values[0].ToString();
            int a = Convert.ToInt32(hdnGWOID1.Value);
            //if (e.CommandName == "AddTax")
            //{
            //    gwoia.BindChargesDetails(ddlOtherCharges_t, ddlTax_t);

            //    BindTaxChargesDetails(gwoia.GWOPOID);
            //    BindOtherChargesDetails(gwoia.GWOPOID);
            //    BindPOTotalAmount(gwoia.GWOPOID);

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowTaxPopUp();OpenTab('OtherCharges');", true);
            //}

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion


    #region"Common Methods"

    private void GeneratePDDF()
    {
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
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
            hdnCompanyName.Value = dtAddress.Rows[0]["CompanyName"].ToString();
            hdnFormalCompanyname.Value = dtAddress.Rows[0]["FormalCompanyName"].ToString();
            hdnCompanyNameFooter.Value = dtAddress.Rows[0]["CompanyNameFooter"].ToString();

            hdnLonestarLogo.Value = dtAddress.Rows[0]["LonestarLOGO"].ToString();
            hdnISOLogo.Value = dtAddress.Rows[0]["ISOLogo"].ToString();
            // SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());
            // objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintWorkOrderPO('" + ViewState["ApprovedTime"].ToString() + "','" + ViewState["POStatus"].ToString() + "','" + ViewState["ApprovedBy"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindOtherChargesDetails(int id)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.SGWOID = id;
            ds = gwoia.GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber("LS_GetGeneralWorkOrderPOOtherChargesDetailsByGWPOID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierPOOtherCharges.DataSource = ds.Tables[0];
                gvSupplierPOOtherCharges.DataBind();
            }
            else
            {
                gvSupplierPOOtherCharges.DataSource = "";
                gvSupplierPOOtherCharges.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindTaxChargesDetails(int id)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.SGWOID = id;
            ds = gwoia.GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber("LS_GetGeneralWorkOrderPOTaxDetails");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvTaxDetails.DataSource = ds.Tables[0];
                gvTaxDetails.DataBind();
            }
            else
            {
                gvTaxDetails.DataSource = "";
                gvTaxDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPOTotalAmount(int id)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.SGWOID = id;
            ds = gwoia.GetWorkOrderPOTaxAndOtherChargesDetailsByWPONumber("LS_GetGeneralWorkOrderPOTotalAmountWithOtherCharges");

            lblPOAmount_T.Text = "Total PO Amount : " + ds.Tables[0].Rows[0]["POAmountWithOtherCharges"].ToString() + ".00";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void BindDropDownDetails()
    {
        gwoia = new GeneralWorkOrderIndentApproval();
        DataSet ds = new DataSet();
        try
        {
            gwoia.GetSupplierDetails(ddlSupplier);
            gwoia.GetLocationDetails(ddlLocation);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindEditDetails(int GWOPOID)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {

            gwoia.GWOPOID = GWOPOID;
            ds = gwoia.GetBindEditDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                GeneralWorkOrderSubIndent.DataSource = ds.Tables[0];
                GeneralWorkOrderSubIndent.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvGeneralWorkOrderPO.DataSource = "";
                gvGeneralWorkOrderPO.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }
    private void GeneralWorkOrderIndentPODetails()
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            ds = gwoia.GetGeneralWorkOrderAllPODetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrderPO.DataSource = ds.Tables[0];
                gvGeneralWorkOrderPO.DataBind();
                ViewState["GeneralWorkOrderIndentDetails"] = ds.Tables[0];
                gvGeneralWorkOrderPO.UseAccessibleHeader = true;
                gvGeneralWorkOrderPO.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvGeneralWorkOrderPO.DataSource = "";
                gvGeneralWorkOrderPO.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string mode)
    {
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;
            switch (mode.ToLower())
            {
                case "edit":
                    divInput.Visible = true;
                    ddlSupplier.Focus();
                    break;
                case "view":
                    divAdd.Visible = divOutput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void ViewDrawingFilename()
    {
        try
        {
            cCommon objc = new cCommon();
            objc.GeneralViewFileName(PDFSavePath, PDFHttpPath, ViewState["FileName"].ToString(), ViewState["GWOID"].ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        hdnGWOPOID.Value = "0";
        ddlSupplier.SelectedValue = "0";
        ddlLocation.SelectedValue = "0";
        txtQuote.Text = "";
        txtDelivery.Text = "";
        txtQuantity.Text = "";
        txtUnitCost.Text = "";
        txtPayment.Text = "";
        txtNote.Text = "";
        txtRemark.Text = "";


    }
    #endregion
}