using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
using System.IO;
using eplus.core;
using SelectPdf;

public partial class Pages_MakeOffer : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsAnnexure = new DataSet();
    cSession _objSession = new cSession();
    cSales _objSales = new cSales();
    cCommon _objc = new cCommon();
    cCommonMaster _objCommon = new cCommonMaster();
    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            ddlcustomerload();
            bindAnnexures();
            bindOfferName();
        }
        else
        {
            if (target == "GeneratePDF")
            {
                GeneratePDF();
            }
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvAnnexure1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                string sowhead = (e.Row.FindControl("lblSOWHeader") as Label).Text;
                DropDownList ddlsowpoints = (e.Row.FindControl("ddlsowpoints") as DropDownList);
                ddlsowpoints.DataSource = _objSales.GetddlData("Headername", sowhead, "LS_SowDropdown");
                ddlsowpoints.DataTextField = "Point";
                ddlsowpoints.DataValueField = "SOWPID";
                ddlsowpoints.DataBind();
                string sowpoint = (e.Row.FindControl("lblsowPoint") as Label).Text;
                if (ddlsowpoints.Items.Count < 1) (e.Row.FindControl("chksow") as CheckBox).Checked = false;
                ddlsowpoints.Items.FindByValue(sowpoint).Selected = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    protected void gvAnnexure3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                string tachead = (e.Row.FindControl("lblTACHeader") as Label).Text;
                DropDownList ddltacpoints = (e.Row.FindControl("ddltacpoints") as DropDownList);
                ddltacpoints.DataSource = _objSales.GetddlData("Headername", tachead, "LS_TACDropdown");
                if (ddltacpoints.DataSource == null) (e.Row.FindControl("chktac") as CheckBox).Checked = false;
                ddltacpoints.DataTextField = "Point";
                ddltacpoints.DataValueField = "TACPID";
                ddltacpoints.DataBind();
                string sowpoint = (e.Row.FindControl("lblTACPoint") as Label).Text;
                ddltacpoints.Items.FindByValue(sowpoint).Selected = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    protected void gvAnnexure2_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["CurrencySymbol"].ToString() == "")
            {
                e.Row.Cells[6].Text = "Unit Price (INR/Foreign Currency)";
                e.Row.Cells[7].Text = "Total Price (INR/Foreign Currency)";
            }
            else
            {
                e.Row.Cells[6].Text = "Unit Price (" + ViewState["CurrencySymbol"].ToString() + ")";
                e.Row.Cells[7].Text = "Total Price (" + ViewState["CurrencySymbol"].ToString() + ")";
            }
        }
    }

    #endregion

    #region "Button Events"

    protected void btnFrontPage_Click(object sender, EventArgs e)
    {
        try
        {
            BindFrontpage();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowFrontPagePopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnOfferPrint_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        _objSales = new cSales();
        try
        {
            _objSales.EODID = 349;
            ds = _objSales.GetOfferPrintDetailsByEODID();
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

            lblAnnexure1HeaderName.Text = "Annexure 1";

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
            lblsumofprice_p.Text = ds.Tables[7].Rows[0]["TotalPriceOfExWorks"].ToString();

            lblAnnexure2MarketingHead.Text = ds.Tables[0].Rows[0]["MarketingHeadName"].ToString();
            lblFooterHeadDesignationWihMobNo.Text = ds.Tables[0].Rows[0]["MarketingHeadDesignation"].ToString();

            lblAnnnexure2MD.Text = ds.Tables[0].Rows[0]["MDName"].ToString();

            hdnAddress.Value = ds.Tables[5].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[5].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[5].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[5].Rows[0]["WebSite"].ToString();

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

            string Code = displayQrnumber + "/" + _objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);

            if (QrCode != "")
            {
                imgQrcode.Attributes.Add("style", "display:block;");
                imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "";
                objQr.createdBy = _objSession.employeeid;
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
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Dropdown Events"

    protected void ddlcustomers_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            clearfields();
            _objc.customerddlchnage(ddlcustomers, ddlenquiries, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }
    protected void ddlenquiries_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            clearfields();
            if (ddlenquiries.SelectedValue != "0")
            {
                try
                {
                    _objc.enquiryddlchange(ddlenquiries, ddlcustomers, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                    cDataAccess DAL1 = new cDataAccess();
                    DataSet ds1 = new DataSet();
                    SqlCommand c1 = new SqlCommand();
                    c1.CommandType = CommandType.StoredProcedure;
                    c1.CommandText = "LS_Getoffercost";
                    c1.Parameters.AddWithValue("@Enquiryid", ddlenquiries.SelectedValue);
                    DAL1.GetDataset(c1, ref ds1);

                    ViewState["CurrencySymbol"] = ds1.Tables[0].Rows[0]["CurrencySymbol"].ToString();

                    gvAnnexure2.DataSource = ds1.Tables[0];
                    gvAnnexure2.DataBind();
                    if (ds1.Tables[0].Rows.Count > 0) divothercharges.Visible = true;
                    else divothercharges.Visible = false;
                    lblSumOfPrice.Text = ds1.Tables[1].Rows[0]["SumCost"].ToString();
                    hdnSumOfPrice.Value = ds1.Tables[1].Rows[0]["SumCost"].ToString();

                    lblCurrencySymbol.Text = " ( " + ViewState["CurrencySymbol"].ToString() + " ) " + " - ";
                }
                catch (Exception ec)
                {
                }
                cDataAccess DAL = new cDataAccess();
                DataSet ds = new DataSet();
                SqlCommand c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetEnquirydetails";
                c.Parameters.AddWithValue("@Enquiryid", ddlenquiries.SelectedValue);
                DAL.GetDataset(c, ref ds);
                DataTable dt = ds.Tables[0];
                ViewState["FrontPageCustomerDetails"] = ds.Tables[0];
                ViewState["SalesHeadDetails"] = ds.Tables[2];

                ViewState["Address"] = ds.Tables[3];

                lblFooterHeadDesignationWihMobNo.Text = ds.Tables[2].Rows[2]["Marktdesignation"].ToString();

                //  lblOfferNo[],, 
                txtofferno.Text = dt.Rows[0]["OfferNo"].ToString();
                txt_offerrevision.Text = dt.Rows[0]["OfferNoRevision"].ToString();

                txt_FaxNo.Text = dt.Rows[0]["Faxno"].ToString();
                txt_CustomerphoneNo.Text = dt.Rows[0]["CustomerphoneNo"].ToString();
                txt_CustomerEmail.Text = dt.Rows[0]["CustomerEmail"].ToString();
                txt_ContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
                txt_CustomerMobile.Text = dt.Rows[0]["CustomerMobile"].ToString();
                txt_ContactPersonEmail.Text = dt.Rows[0]["ContactPersonEmail"].ToString();
                lbl_offertype.InnerText = dt.Rows[0]["Offertype"].ToString() + " ";
                if (dt.Rows[0]["Offertype"].ToString().Trim() == "Technical") lblAnnexure2Header.Text = "Unprice Schedule";
                else lblAnnexure2Header.Text = "Price Schedule";
                hdnOfferSubmissionDate.Value = "DT: " + dt.Rows[0]["OfferSubmissionDate"].ToString();
                hdnOfferNo.Value = dt.Rows[0]["OfferNo"].ToString();

                DataTable dt1 = ds.Tables[2];

                for (int dtl = 0; dtl < dt1.Rows.Count; dtl++)
                {
                    if (dt1.Rows[dtl]["Emptype"].ToString() == "Employee")
                    {
                        txt_MarktEngg.Text = dt1.Rows[dtl]["MarktEngg"].ToString();
                        txt_Marktdesignation.Text = dt1.Rows[dtl]["Marktdesignation"].ToString();
                        txt_MarktMobileNo.Text = dt1.Rows[dtl]["MarktMobileNo"].ToString();
                        txt_Marktemail.Text = dt1.Rows[dtl]["Marktemail"].ToString();
                        txt_MarktOfficePhoneNo.Text = dt1.Rows[dtl]["MarktOfficePhoneNo"].ToString();
                    }
                    else if (dt1.Rows[dtl]["Emptype"].ToString() == "MD")
                    {
                        txt_HeadName.Text = dt1.Rows[dtl]["MarktEngg"].ToString();
                        txt_HDesignation.Text = dt1.Rows[dtl]["Marktdesignation"].ToString();
                        txt_HMobileNo.Text = dt1.Rows[dtl]["MarktMobileNo"].ToString();
                        txt_HEmail.Text = dt1.Rows[dtl]["Marktemail"].ToString();
                    }
                    else if (dt1.Rows[dtl]["Emptype"].ToString() == "Head")
                    {
                        txt_SubHeadName.Text = dt1.Rows[dtl]["MarktEngg"].ToString();
                        txt_SHdesignation.Text = dt1.Rows[dtl]["Marktdesignation"].ToString();
                        txt_SHMobileNo.Text = dt1.Rows[dtl]["MarktMobileNo"].ToString();
                        txt_SHEmail.Text = dt1.Rows[dtl]["Marktemail"].ToString();
                    }
                    //else if (dt1.Rows[dtl]["Emptype"].ToString() == "Subhead")
                    //{
                    //    txt_SubHeadName.Text = dt1.Rows[0]["MarktEngg"].ToString();
                    //    txt_SHdesignation.Text = dt1.Rows[0]["Marktdesignation"].ToString();
                    //    txt_SHMobileNo.Text = dt1.Rows[0]["MarktMobileNo"].ToString();
                    //    txt_SHEmail.Text = dt1.Rows[0]["Marktemail"].ToString();
                    //}
                }
                lblFooterHeadDesignationWihMobNo.Text = txt_SHdesignation.Text;
                lblAnnnexure2MD.Text = lblFrontPageMD.Text = txt_HeadName.Text;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    #endregion

    #region Common Methods

    private void BindFrontpage()
    {
        string Annnexure1 = "";
        try
        {
            foreach (GridViewRow row in gvAnnexure1.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chksow");
                Label lblA1Header = (Label)row.FindControl("lblSOWHeader");

                if (chk.Checked)
                {
                    if (Annnexure1 == "")
                    {
                        Annnexure1 = lblA1Header.Text;
                        break;
                    }
                    else
                        Annnexure1 = Annnexure1 + ',' + lblA1Header.Text;
                }
            }

            DataTable dt;
            dt = (DataTable)ViewState["FrontPageCustomerDetails"];

            lblCustomerAddress.Text = dt.Rows[0]["Address"].ToString();
            lblEmail.Text = dt.Rows[0]["CustomerEmail"].ToString();

            lblPhoneNumber.Text = dt.Rows[0]["CustomerMobile"].ToString();
            lblFaxNumber.Text = dt.Rows[0]["FaxNo"].ToString();
            lblOfferDate.Text = lblAnneure1OfferDate.Text = lblAnnexure2OfferDate.Text = lblAnnexure3OfferDate.Text = dt.Rows[0]["OfferDate"].ToString();
            lblReference.Text = txtReference.Text;//"Your Email Enquiry Dated " + dt.Rows[0]["ReceivedDate"].ToString();
            lblProjectDescription.Text = txtProjectName.Text;
            lblsubjectItems.Text = txtSubjectItem.Text;
            lblfrontpageres.Text = ddlfornt.SelectedItem.Text;
            lblAnnexure1Header.Text = Annnexure1;
            //lblAnnexure2Header.Text = "Price Schedule";
            lblOfferNo.Text = (dt.Rows[0]["OfferNo"].ToString() + '/' + dt.Rows[0]["OfferNoRevision"].ToString()).TrimEnd('/');

            lblCustomerContactName.Text = txt_ContactPerson.Text;
            lblCustomerPhoneNumber.Text = txt_CustomerphoneNo.Text;
            lblCustomerMobileNumber.Text = txt_CustomerMobile.Text;
            lblCustomerEmail.Text = txt_ContactPersonEmail.Text;


            dt = (DataTable)ViewState["SalesHeadDetails"];

            DataView dv = new DataView(dt);

            dv.RowFilter = "Emptype='Head'";
            dt = dv.ToTable();
            // lblSalesHODName.Text = dt.Rows[0]["MarktEngg"].ToString() + "-" + "MANAGER(MARKETING)";
            //    lblSalesMobileNumber.Text = dt.Rows[0]["MarktMobileNo"].ToString();
            //  lblSalesPhoneNumber.Text = dt.Rows[0]["MarktOfficePhoneNo"].ToString();

            //lblSalesHODName.Text = txt_MarktEngg.Text + " - " + txt_Marktdesignation.Text;
            lblSalesMobileNumber.Text = txt_MarktMobileNo.Text;
            lblSalesPhoneNumber.Text = txt_MarktOfficePhoneNo.Text;

            lblFrontPageMD.Text = txt_HeadName.Text;
            lblFrontPageMarketingHead.Text = txt_SubHeadName.Text;

            lblFooterHeadDesignationWihMobNo.Text = txt_SHdesignation.Text;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlcustomerload()
    {
        try
        {
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = _objc.getCustomerNameByUserID(Convert.ToInt32(_objSession.employeeid), ddlcustomers, "LS_GetCustomerNameByEmployeeID");
            ViewState["CustomerDetails"] = dsCustomer.Tables[1];
            dsEnquiryNumber = _objc.GetEnquiryNumberByUserID(Convert.ToInt32(_objSession.employeeid), ddlenquiries, "LS_GetEnquiryIDByUserID");
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        { }
    }

    private void bindAnnexures()
    {
        int[] anxrs = { 1, 3 };
        for (int i = 0; i < anxrs.Length; i++)
        {
            try
            {
                int anxrno = anxrs[i];
                GridView gv = new GridView();
                if (anxrno == 1) gv = gvAnnexure1;
                else if (anxrno == 3) gv = gvAnnexure3;
                dsAnnexure = _objSales.GetAnnexure(anxrno);
                if (dsAnnexure.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = dsAnnexure.Tables[0];
                    gv.DataBind();
                    gv.UseAccessibleHeader = true;
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gv.DataSource = "";
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Message(ex);
            }
        }
    }

    protected void clearfields()
    {
        try
        {
            txt_FaxNo.Text = "";
            txt_CustomerphoneNo.Text = "";
            txt_CustomerEmail.Text = "";
            txt_ContactPerson.Text = "";
            txt_CustomerMobile.Text = "";
            txt_ContactPersonEmail.Text = "";


            txt_MarktEngg.Text = "";
            txt_Marktdesignation.Text = "";
            txt_MarktMobileNo.Text = "";
            txt_Marktemail.Text = "";
            txt_MarktOfficePhoneNo.Text = "";

            txt_HeadName.Text = "";
            txt_HDesignation.Text = "";
            txt_HMobileNo.Text = "";
            txt_HEmail.Text = "";


            txt_SubHeadName.Text = "";
            txt_SHdesignation.Text = "";
            txt_SHMobileNo.Text = "";
            txt_SHEmail.Text = "";
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        DataSet dsG = new DataSet();
        string SOWPID = "";
        string TACPID = "";
        try
        {
            _objSales.EnquiryNumber = ddlenquiries.SelectedValue;
            ds = _objSales.GetOfferNumberByEnquiryID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                _objSales.UserID = Convert.ToInt32(_objSession.employeeid);
                _objSales.EODID = Convert.ToInt32(ds.Tables[0].Rows[0]["EODID"].ToString());
                _objSales.OfferNameID = ddlOfferName.SelectedValue;
                _objSales.EnquiryNumber = ddlenquiries.SelectedValue;
                _objSales.FaxNo = txt_FaxNo.Text;
                _objSales.CustomermobileNo = txt_CustomerMobile.Text;
                _objSales.ContactPersonPhoneNo = txt_CustomerphoneNo.Text;
                _objSales.CustomerEmail = txt_CustomerEmail.Text;
                _objSales.ContactPerson = txt_ContactPerson.Text;
                _objSales.ContactPersonEmail = txt_ContactPersonEmail.Text;
                _objSales.MarketingPersonName = txt_MarktEngg.Text;
                _objSales.MarketingPersonDesignation = txt_Marktdesignation.Text;
                _objSales.MarketingPersonMobileNo = txt_MarktMobileNo.Text;
                _objSales.MarketingPersonEmail = txt_Marktemail.Text;
                _objSales.MarketingOfficePhoneNo = txt_MarktOfficePhoneNo.Text;
                _objSales.MarketingHeadName = txt_SubHeadName.Text;
                _objSales.MarketingHeadDesignation = txt_SHdesignation.Text;
                _objSales.MarketingHeadMobileNo = txt_SHMobileNo.Text;
                _objSales.MarketingHeadEmail = txt_SHEmail.Text;
                _objSales.SubJectItem = txtSubjectItem.Text;
                _objSales.Projectname = txtProjectName.Text;
                _objSales.Reference = txtReference.Text;
                _objSales.Frontpage = ddlfornt.SelectedItem.Text;
                _objSales.CreatedBy = _objSession.employeeid;
                _objSales.GST = txt_GST.Text;
                _objSales.Freight = txt_Freight.Text;
                _objSales.Insurance = txt_Insurance.Text;
                _objSales.Inspection = txt_Inspection.Text;
                _objSales.TransitInsurance = txt_TransitInsurance.Text;
                _objSales.Delivery = txt_Delivery.Text;
                _objSales.Guarantee = txt_Guarantee.Text;
                _objSales.PaymentTerms = txt_PaymentTerms.Text;
                _objSales.Validity = txt_Validity.Text;
                _objSales.LDClause = txt_LDClause.Text;
                _objSales.Settlement = txt_Settlement.Text;
                _objSales.Legalization = txt_Legalization.Text;
                _objSales.ForceMajeure = txt_ForceMajeure.Text;
                _objSales.CountryofOrigin = txt_CountryofOrigin.Text;
                _objSales.PortandLocationofShipment = txt_PortLocationofShipment.Text;
                _objSales.DrawingGivenByCustomer = txt_DrawingGivenByCustomer.Text;
                _objSales.UStampValue = txt_UStampValue.Text;
                _objSales.IBRValue = txt_IBRValue.Text;
                _objSales.SupervisionCharges = txt_SupervisionCharges.Text;
                _objSales.IIIPartyInspectionCharges = txt_partyinspectioncharges.Text;
                _objSales.SeaFreight = txt_SeaFreight.Text;
                _objSales.AirFreight = txt_AirFreight.Text;
                _objSales.FOBCharges = txt_FOBCharges.Text;
                _objSales.ReplaceExWorks = TextBox3.Text;
                _objSales.ReplacePackingChargesOnRate = txt_ReplacePackingCharges.Text;
                _objSales.SeaWorthCharges = txt_SeaWorthCharges.Text;
                _objSales.ASMECodeCharges = txt_ASMECodeCharges.Text;
                _objSales.OceanFreightCharges = txt_OceanFreightCharges.Text;
                _objSales.Note = txt_Note.Text;

                if (chckpricequoted.Checked)
                    _objSales.QuotedPrice = "1";
                else
                    _objSales.QuotedPrice = "0";

                _objSales.ExForBasis = chchkforbasis.Checked == true ? "1" : "0";

                foreach (GridViewRow row in gvAnnexure1.Rows)
                {
                    DropDownList ddlsowpoints = (DropDownList)row.FindControl("ddlsowpoints");
                    CheckBox chksow = (CheckBox)row.FindControl("chksow");

                    if (chksow.Checked)
                    {
                        if (SOWPID == "")
                            SOWPID = ddlsowpoints.SelectedValue;
                        else
                            SOWPID = SOWPID + "," + ddlsowpoints.SelectedValue;
                    }
                }

                foreach (GridViewRow row in gvAnnexure3.Rows)
                {
                    DropDownList ddltacpoints = (DropDownList)row.FindControl("ddltacpoints");
                    CheckBox chktac = (CheckBox)row.FindControl("chktac");

                    if (chktac.Checked)
                    {
                        if (TACPID == "")
                            TACPID = ddltacpoints.SelectedValue;
                        else
                            TACPID = TACPID + "," + ddltacpoints.SelectedValue;
                    }
                }

                dsG = _objSales.SaveGenerateOfferDetails(SOWPID, TACPID);

                if (dsG.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','Offer Saved Succesfully');", true);
                    //string EODID = ds.Tables[0].Rows[0]["EODID"].ToString();
                    //_objSales.EODID = Convert.ToInt32(EODID);
                    //_objSales.CreatedBy = _objSession.employeeid;
                    //_objSales.UpdateOfferGeneratedStatusbyEODID();
                }
                else if (dsG.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','Offer Updated Succesfully');", true);
                    //string EODID = ds.Tables[0].Rows[0]["EODID"].ToString();
                    //_objSales.EODID = Convert.ToInt32(EODID);
                    //_objSales.CreatedBy = _objSession.employeeid;
                    //_objSales.UpdateOfferGeneratedStatusbyEODID();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ErrorMessage('Error','" + dsG.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                //BindFrontpage();
                //var sbFrontpage = new StringBuilder();
                //var sbAnnexure1 = new StringBuilder();
                //var sbAnnexure2 = new StringBuilder();
                //var sbAnnexure3 = new StringBuilder();
                //var sbFooterContent = new StringBuilder();
                //var sbAnnexure2HeaderContent = new StringBuilder();

                //cQRcode objQr = new cQRcode();

                //string QrNumber = objQr.generateQRNumber(9);
                //string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                //string Code = displayQrnumber + "/" + _objSession.employeeid + "/" + DateTime.Now.ToString();

                //string QrCode = objQr.QRcodeGeneration(Code);

                //if (QrCode != "")
                //{
                //    imgQrcode.Attributes.Add("style", "display:block;");
                //    imgQrcode.ImageUrl = QrCode;
                //    objQr.QRNumber = displayQrnumber;
                //    objQr.fileName = "";
                //    objQr.createdBy = _objSession.employeeid;
                //    objQr.saveQRNumber();
                //}
                //else
                //    imgQrcode.Attributes.Add("style", "display:none;");
                //if (hdnoffernote.Value != "0") lbloffernote.Text = hdnoffernote.Value;
                //divAnnexure1PDF.Attributes.Add("style", "display:block;");
                //divAnnexure3PDF.Attributes.Add("style", "display:block;");
                //divfrontpagetopstripePDF.Attributes.Add("style", "display:none;");
                //divAnnexure2topstripePDF.Attributes.Add("style", "display:none;");
                //divfrontpageFooter.Attributes.Add("style", "display:none;");

                //divAnnexure1popupcontentPDF.InnerHtml = hdnAnnexure1PopUpContent.Value;
                //divannexure2.InnerHtml = hdnAnnexure2PopUpContent.Value;
                //divAnnexure3popupcontentPDF.InnerHtml = hdnAnnexure3PopUpContent.Value;

                //lblAnnexure2MarketingHead.Text = lblFrontPageMarketingHead.Text = txt_SubHeadName.Text;
                //lblAnnnexure2MD.Text = lblFrontPageMD.Text = txt_HeadName.Text;

                //lblAnnexure1OfferNumber.Text = lblAnnexure2OfferNo.Text = lblAnnexure3offerNo.Text = hdnOfferNo.Value;

                //lblAnnexure2PopColumnHead.Text = "Annexure 2";
                //divAnnexure2footer.Attributes.Add("style", "display:none;");

                //if (Convert.ToInt32(hdnAnnexure1CheckedLength.Value) > 0)
                //{
                //    lblAnnexure1PopUpColumnHead.Text = "Annexure 1";
                //    lblAnnexure2PopColumnHead.Text = "Annexure 2";
                //    lblAnnexure3PopUpColumnHead.Text = "Annexure 3";

                //    divAnnexure1HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                //    divAnnexure2HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                //    divAnnexure3HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");

                //    lblAnnexure1HeaderName.Text = "Annexure 1";
                //    lblAnnexure2HeaderName.Text = "Annexure 2";
                //    lblAnnexure3HeaderName.Text = "Annexure 3";
                //}
                //else
                //{
                //    lblAnnexure2PopColumnHead.Text = "Annexure 1";
                //    lblAnnexure3PopUpColumnHead.Text = "Annexure 2";

                //    divAnnexure1HeaderName.Attributes.Add("style", "display:none;padding-left: 15px; padding-right: 15px;");
                //    divAnnexure2HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");
                //    divAnnexure3HeaderName.Attributes.Add("style", "display:block;padding-left: 15px; padding-right: 15px;");

                //    lblAnnexure2HeaderName.Text = "Annexure 1";
                //    lblAnnexure3HeaderName.Text = "Annexure 2";
                //}

                //divAnnexure2HeaderContent.RenderControl(new HtmlTextWriter(new StringWriter(sbAnnexure2HeaderContent)));
                //hdndivAnnexure2HeaderContent.Value = sbAnnexure2HeaderContent.ToString();

                //divFrontagePDF.RenderControl(new HtmlTextWriter(new StringWriter(sbFrontpage)));
                //divAnnexure1PDF.RenderControl(new HtmlTextWriter(new StringWriter(sbAnnexure1)));
                //divAnnexure2PDF.RenderControl(new HtmlTextWriter(new StringWriter(sbAnnexure2)));
                //divAnnexure3PDF.RenderControl(new HtmlTextWriter(new StringWriter(sbAnnexure3)));

                //string OfferNo = ds.Tables[0].Rows[0]["OfferNo"].ToString();

                //string htmlfile = OfferNo + ".html";
                //string pdffile = OfferNo + ".pdf";
                //string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
                //string pdfFileURL = LetterPath + pdffile;

                //string htmlfileURL = LetterPath + htmlfile;

                //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                //url = url.ToLower();
                //string[] pagename = url.ToString().Split('/');
                //string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

                //string Main = url.Replace(Replacevalue, "Assets/css/main.css");
                //string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
                //string style = url.Replace(Replacevalue, "Assets/css/style.css");
                //string Print = url.Replace(Replacevalue, "Assets/css/print.css");
                //string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

                //DataTable dtAddress = new DataTable();
                //dtAddress = (DataTable)ViewState["Address"];

                //hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                //hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                //hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                //hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                //SaveHtmlFile(htmlfileURL, "Offer", sbFrontpage.ToString(), sbAnnexure1.ToString(), sbAnnexure2.ToString(), sbAnnexure3.ToString(), hdnAnnexure2FooterContent.Value, QrCode);

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "PrintGenerateOffer('" + epstyleurl + "','" + style + "','" + Print + "','" + Main + "','" + topstrip + "');", true);

                //string Annxure2htmlfile = "Annexure2_" + OfferNo + ".html";
                //string Annxure2pdffile = "Annexure2_" + OfferNo + ".pdf";
                //string Annxure2LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
                //string Annxure2pdfFileURL = LetterPath + Annxure2pdffile;

                //string Annxure2htmlfileURL = LetterPath + Annxure2htmlfile;

                //SaveAnnexure2HtmlFile(Annxure2htmlfileURL, "Offer", sbFrontpage.ToString(), sbAnnexure2.ToString(), hdnAnnexure2FooterContent.Value);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','You can download On Offer Dispatch Page');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveHtmlFile(string URL, string lbltitle, string Frontpage, string Annexutr1, string Annexure2, string Annexure3, string FooterContent, string QrCode)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css?version=51");
            string Print = url.Replace(Replacevalue, "Assets/css/printg.css?version=51");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);

            //  w.WriteLine("<html><head><title>");
            w.WriteLine("<html><head><title>");
            w.WriteLine("Offer");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            w.WriteLine("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            w.WriteLine("<div>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            w.WriteLine("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            w.WriteLine("<div class='row' style='position:fixed;width:200mm;left:5mm;border-bottom:1px solid #000;'>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-6 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            w.WriteLine("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            w.WriteLine("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            w.WriteLine("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            w.WriteLine("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");

            w.WriteLine("<div class='col-sm-12 padding0 page_generateoffer'>");
            w.WriteLine(Frontpage);
            w.WriteLine("</div>");

            if (Convert.ToInt32(hdnAnnexure1CheckedLength.Value) > 0)
            {
                w.WriteLine("<div class='col-sm-12 padding0 page_generateoffer'>");
                w.WriteLine(Annexutr1);
                w.WriteLine("</div>");
            }

            string[] tablerow = hdntablerows.Value.Split('#');
            for (int j = 0; j < tablerow.Length; j++)
            {
                w.WriteLine("<div class='col-sm-12 padding0 page_generateoffer'>");
                if (Convert.ToInt32(hdnRecordLength.Value) > 0)
                {
                    w.WriteLine(hdndivAnnexure2HeaderContent.Value);
                    w.WriteLine("<div id='divannexure2' class='col-sm-12 annexure2PopUpContent' style='padding-right: 15px; padding-left: 15px;'>");
                    w.WriteLine("<div class='col-sm-12 p-t-10 p-l-0 p-r-0' id='divgvAnnexure2'>");
                    w.WriteLine("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvAnnexure2_" + j + "' style='border-collapse:collapse;border-width: 1px !important; text-transform: none'>");
                    w.WriteLine("<tbody>");
                    w.WriteLine("<tr align='center'>" + hdndivAnnexure2TableHeader.Value + "</tr>");

                    w.WriteLine(tablerow[j]);

                    w.WriteLine("</tbody>");
                    w.WriteLine("</table>");
                    if (tablerow.Length - 1 != j)
                    {
                        w.WriteLine("<div class='col-sm-12 p-t-20 text-center'> -- CONTINUE -- </div>");
                    }
                    w.WriteLine("</div></div>");
                    if (tablerow.Length - 1 == j)
                    {
                        w.WriteLine(hdndivAnnexure2TotalPrice.Value);
                        w.WriteLine(hdndivAnnexure2OfferNote.Value);
                    }
                }
                else
                    w.WriteLine(Annexure2);
                w.WriteLine("</div>");
            }

            if (Convert.ToInt32(hdnAnnexure3CheckedLength.Value) > 0)
            {
                w.WriteLine("<div class='col-sm-12 padding0 page_generateoffer'>");
                w.WriteLine(Annexure3);
                w.WriteLine("</div>");
            }

            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot><tr><td>");
            w.WriteLine("<div class='col-sm-12' style='height:30mm;'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 row' style='margin-bottom:20px;border-top:1px solid #000;position:fixed;width:200mm;bottom:0px'>");
            //w.WriteLine(FooterContent);

            //Footer Content
            w.WriteLine("<div class='col-sm-6' style='font-weight:700;padding-top:10px; padding-left:15px; padding-right:15px;'>");

            w.WriteLine("<div class='col-sm-12'>");
            w.WriteLine("<label style='font-weight: 700; font-size:14px; font-style:italic; color:black; font-family:Arial;display:contents;'> For LONESTAR </label>");
            w.WriteLine("<span style='font-weight: 700; font-size: 14px; font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12' style='margin-top:40px;'>");
            w.WriteLine("<span class='MarketingHead' style='font-size:14px; font-weight:700; color:black;'>" + txt_SubHeadName.Text + "</span>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12'>");
            w.WriteLine("<span style='font-size:14px; font-weight:bold; color:black;'>" + txt_SHdesignation.Text + "</span>");
            w.WriteLine("</div>");

            w.WriteLine("</div>");

            w.WriteLine("<div class='col-sm-4 text-right' style='font-weight:700; border-left:1px solid; height:100px; padding-top:10px; padding-right:0px'>");

            w.WriteLine("<div class='col-sm-12'>");
            w.WriteLine("<label style='font-weight: 700; font-size:14px; font-style:italic; color:black; font-family:Arial;display:contents;'> For LONESTAR </label>");
            w.WriteLine("<span style='font-weight: 700; font-size: 14px; font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12' style='margin-top:40px;'>");
            w.WriteLine("<label style='font-size:14px; font-weight:700; color:black;'><span>" + txt_HeadName.Text + "</span></label>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12'>");
            w.WriteLine("<label style='font-size:14px; font-weight:700; color:black;'> MANAGING PARTNER</label>");
            w.WriteLine("</div>");

            w.WriteLine("</div>");

            w.WriteLine("<div class='col-sm-2'>");

            w.WriteLine("<img id='imgqrcode' class='Qrcode' src='" + QrCode + "' />");

            w.WriteLine("</div>");

            w.WriteLine("</div>");
            //footer content end

            w.WriteLine("</div></div>");
            w.WriteLine("</td></tr></tfoot></table>");
            w.WriteLine("</body></html>");

            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAnnexure2HtmlFile(string URL, string lbltitle, string Frontpage, string Annexure2, string FooterContent)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);

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
            w.WriteLine("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-6 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR </h3>");
            w.WriteLine("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("<p style='font-weight:500;color:#000;width: 103%;'>" + Address + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");
            w.WriteLine("<div class='col-sm-12 padding0'>");
            w.WriteLine(Annexure2);
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot><tr><td>");
            w.WriteLine("<div class='col-sm-12 footer-space' style='padding-right: 0 !important;padding-left: 0  !important;padding-top:78%;'>");
            w.WriteLine("<div class='footer' style='border: 0px solid #000 ! important;'>");
            w.WriteLine("<div class='col-sm-12'>");
            w.WriteLine(FooterContent);
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12 text-left' style='color:black;font-weight:bold;'>");
            w.WriteLine("Enclosure: as above");
            w.WriteLine("</div>");
            w.WriteLine("</div></div>");
            w.WriteLine("</td></tr></tfoot></table>");
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
            converter.Options.MarginLeft = 25;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 50;
            converter.Options.MarginBottom = 40;
            converter.Options.WebPageWidth = 700;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;

            doc = converter.ConvertUrl(URL);
            doc.Save(strFile);

            var ms = new System.IO.MemoryStream();

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

    private void bindOfferName()
    {
        DataSet ds = new DataSet();
        cSales objSales = new cSales();
        try
        {
            ds = objSales.GetOfferName(ddlOfferName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    #endregion
}