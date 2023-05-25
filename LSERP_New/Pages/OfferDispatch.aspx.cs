using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Pages_OfferDispatch : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objcommon;
    cSales objSales;

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
                objcommon = new cCommon();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objcommon.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objcommon.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];

                ShowHideControls("input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objcommon = new cCommon();
        try
        {
            objcommon.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objcommon = new cCommon();
        cSales objSales = new cSales();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objcommon.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

                objSales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;

                ds = objSales.GetEnquiryOfferDetailsByEnquiryID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvOfferDetails.DataSource = ds.Tables[0];
                    gvOfferDetails.DataBind();
                }
                else
                {
                    gvOfferDetails.DataSource = "";
                    gvOfferDetails.DataBind();
                }
                ShowHideControls("input,view");
            }
            else
                ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"   

    protected void btnOfferDispatch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objSales = new cSales();
        try
        {
            DataRow dr;
            dt.Columns.Add("EODID");

            foreach (GridViewRow row in gvOfferDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    dr = dt.NewRow();
                    dr["EODID"] = gvOfferDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dt.Rows.Add(dr);
                }
            }

            objSales.dt = dt;
            ds = objSales.UpdateOfferDispatchedStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Offer Dispatched successfully');", true);
                ddlEnquiryNumber_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Error Occcured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvOfferDetails_OnRowCommand(object sender, CommandEventArgs e)
    {
        DataSet ds = new DataSet();
        string pdffile = "";
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblOfferNo = (Label)gvOfferDetails.Rows[index].FindControl("lblOfferNumber");

            if (e.CommandName.ToString() == "ViewOfferDetails")
            {
                pdffile = lblOfferNo.Text + ".html";
                ReadhtmlFile(pdffile);
            }
            else if (e.CommandName == "ViewOfferCostDetails")
            {
                pdffile = "Annexure2_" + lblOfferNo.Text + ".html";
                ReadhtmlFile(pdffile);
            }
            if (e.CommandName == "ViewOfferPrintDetails")
                OfferPrintDetails(gvOfferDetails.DataKeys[index].Values[1].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void OfferPrintDetails(string EODID)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.EODID = Convert.ToInt32(EODID);
            ds = objSales.GetOfferPrintDetailsByEODID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMDDesignationName.Text = ds.Tables[0].Rows[0]["MD_Designation"].ToString();
                lblCustomerAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                lblPhoneNumber.Text = ds.Tables[0].Rows[0]["CustomerphoneNo"].ToString();
                lblFaxNumber.Text = ds.Tables[0].Rows[0]["Faxno"].ToString();
                lblEmail.Text = ds.Tables[0].Rows[0]["CustomerEmail"].ToString();
                lblCustomerContactName.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                lblCustomerPhoneNumber.Text = ds.Tables[0].Rows[0]["CustomerphoneNo"].ToString();
                lblCustomerMobileNumber.Text = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();
                lblCustomerEmail.Text = ds.Tables[0].Rows[0]["CustomerEmail"].ToString();
                lblOfferNo.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblOfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();
                lblsubjectItems.Text = ds.Tables[0].Rows[0]["SubJectItem"].ToString();
                lblReference.Text = ds.Tables[0].Rows[0]["Reference"].ToString();
                lblProjectDescription.Text = ds.Tables[0].Rows[0]["Projectname"].ToString();
                lblfrontpageres.Text = ds.Tables[0].Rows[0]["Frontpage"].ToString();

                lbl_offertype.InnerText = ds.Tables[0].Rows[0]["Offertype"].ToString() + " ";

                lblMarketingEngineer.Text = ds.Tables[0].Rows[0]["MarketingPersonName"].ToString();
                lblSalesPhoneNumber.Text = ds.Tables[0].Rows[0]["MarketingOfficePhoneNo"].ToString();
                lblSalesMobileNumber.Text = ds.Tables[0].Rows[0]["MarketingPersonMobileNo"].ToString();

                lblAnnexure1PopUpColumnHead.Text = "Annexure 1";
                lblAnnexure1OfferNumber.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblAnneure1OfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvAnnexure1_p.DataSource = ds.Tables[1];
                    gvAnnexure1_p.DataBind();
                }
                else
                {
                    gvAnnexure1_p.DataSource = "";
                    gvAnnexure1_p.DataBind();
                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    gvAnnexure3_p.DataSource = ds.Tables[2];
                    gvAnnexure3_p.DataBind();
                }
                else
                {
                    gvAnnexure3_p.DataSource = "";
                    gvAnnexure3_p.DataBind();
                }


                lblAnnexure2PopColumnHead.Text = "Annexure 2";
                lblAnnexure2OfferNo.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblAnnexure2OfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();

                lblNote_p.Text = ds.Tables[4].Rows[0]["Note"].ToString();

                if (ds.Tables[3].Rows.Count > 0)
                {
                    gvAnnexure2_p.DataSource = ds.Tables[3];
                    gvAnnexure2_p.DataBind();
                }
                else
                {
                    gvAnnexure2_p.DataSource = "";
                    gvAnnexure2_p.DataBind();
                }

                lblCurrencySymbol_p.Text = ds.Tables[3].Rows[0]["CurrencySymbol"].ToString();
				if (ds.Tables[8].Rows[0]["ExForBasis"].ToString() == "0")
                    lblExForBasis_p.Text = "EX-Works";
                else
                    lblExForBasis_p.Text = "For Basis";

                if (ds.Tables[8].Rows[0]["QuotedPrice"].ToString() == "0")
                    lblsumofprice_p.Text = ds.Tables[7].Rows[0]["TotalPriceOfExWorks"].ToString();
                else
                    lblsumofprice_p.Text = "Quoted";

                lblAnnexure2MarketingHead.Text = ds.Tables[0].Rows[0]["MarketingHeadName"].ToString();
                lblFooterHeadDesignationWihMobNo.Text = ds.Tables[0].Rows[0]["MarketingHeadDesignation"].ToString();

                lblAnnnexure2MD.Text = ds.Tables[0].Rows[0]["MDName"].ToString();

                hdnAddress.Value = ds.Tables[5].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[5].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[5].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[5].Rows[0]["WebSite"].ToString();
                hdnCompanyName.Value = ds.Tables[5].Rows[0]["CompanyName"].ToString();
                hdnFormarlyCompanyName.Value = ds.Tables[5].Rows[0]["FormalCompanyName"].ToString();

                lblLeftfootercompanyName.Text = lblrightfootercompanyName.Text = ds.Tables[5].Rows[0]["Footer"].ToString();

                hdnAnnexure1CheckedLength.Value = ds.Tables[1].Rows.Count.ToString();
                hdnAnnexure3CheckedLength.Value = ds.Tables[2].Rows.Count.ToString();

                lblAnnexure3PopUpColumnHead.Text = "Annexure 3";
                lblAnnexure3offerNo.Text = ds.Tables[0].Rows[0]["OfferNo"].ToString();
                lblAnnexure3OfferDate.Text = ds.Tables[0].Rows[0]["OfferDate"].ToString();

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvOfferChargesDetails_p.DataSource = ds.Tables[6];
                    gvOfferChargesDetails_p.DataBind();
                }
                else
                {
                    gvOfferChargesDetails_p.DataSource = "";
                    gvOfferChargesDetails_p.DataBind();
                }

                lblAnnexure1HeaderName.Text = "Annexure 1";

                //if (ds.Tables[8].Rows.Count > 0)



                string Annnexure1 = "";

                foreach (GridViewRow row in gvAnnexure1_p.Rows)
                {
                    Label lblheader = (Label)row.FindControl("lblSOWHeader");
                    if (Annnexure1 == "")
                        Annnexure1 = lblheader.Text;
                    else
                        Annnexure1 = Annnexure1 + ',' + lblheader.Text;
                }
                lblAnnexure1Header.Text = Annnexure1;

                lblAnnexure2HeaderName.Text = "Annexure 2";
                if (ds.Tables[0].Rows[0]["Offertype"].ToString().Trim() == "Technical")
                    lblAnnexure2Header.Text = "Unprice Schedule";
                else
                    lblAnnexure2Header.Text = "Price Schedule";

                lblAnnexure3HeaderName.Text = "Annexure 3";
                lblAnnexure3Header.Text = "Commercial Terms And Conditions";

                if (Convert.ToInt32(hdnAnnexure1CheckedLength.Value) > 0)
                {
                    lblAnnexure1PopUpColumnHead.Text = "Annexure 1";
                    lblAnnexure2PopColumnHead.Text = "Annexure 2";
                    lblAnnexure3PopUpColumnHead.Text = "Annexure 3";

                    divAnnexure1HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                    divAnnexure2HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                    divAnnexure3HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");

                    lblAnnexure1HeaderName.Text = "Annexure 1";
                    lblAnnexure2HeaderName.Text = "Annexure 2";
                    lblAnnexure3HeaderName.Text = "Annexure 3";
                }
                else
                {
                    lblAnnexure2PopColumnHead.Text = "Annexure 1";
                    lblAnnexure3PopUpColumnHead.Text = "Annexure 2";

                    divAnnexure1HeaderName.Attributes.Add("style", "display:none;padding-left: 15px; padding-right: 15px;");
                    divAnnexure2HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                    divAnnexure3HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");

                    lblAnnexure2HeaderName.Text = "Annexure 1";
                    lblAnnexure3HeaderName.Text = "Annexure 2";
                }

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
                    objQr.fileName = "";
                    objQr.createdBy = objSession.employeeid;
                    objQr.saveQRNumber();
                }
                else
                    imgQrcode.Attributes.Add("style", "display:none;");

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();
                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

                string Main = url.Replace(Replacevalue, "Assets/css/main.css");
                string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
                string style = url.Replace(Replacevalue, "Assets/css/style.css");
                string Print = url.Replace(Replacevalue, "Assets/css/print.css");
                string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "PrintGenerateOffer('" + epstyleurl + "','" + style + "','" + Print + "','" + Main + "','" + topstrip + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Offer Is Not Available Contact System Adminstrator');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divInput.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
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

    private void ReadhtmlFile(string pdffile)
    {
        string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
        string pdfpath = LetterPath + pdffile;

        if (File.Exists(pdfpath))
        {
            string fileName = pdfpath;
            StreamReader reader = new StreamReader(fileName);

            string pathToHTMLFile = pdfpath;
            StringBuilder sb = new StringBuilder();

            StreamReader sr = new StreamReader(pdfpath);
            string htmlString = sr.ReadToEnd();
            htmlString = htmlString.Replace(Environment.NewLine, "");
            htmlString = htmlString.Replace("\n", String.Empty);
            htmlString = htmlString.Replace("\r", String.Empty);
            htmlString = htmlString.Replace("\t", String.Empty);

            sr.Close();
            sr.Dispose();

            hdnpdfContent.Value = htmlString;
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','File Not Found');", true);
    }

    #endregion
}