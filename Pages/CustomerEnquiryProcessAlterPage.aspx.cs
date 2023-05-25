using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_CustomerEnquiryProcessAlterPage : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales = new cSales();
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    cCommon objcommon = new cCommon();

    #endregion

    #region "Page Init Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region "Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Response.ContentType = "image/jpeg/eml";         
            Response.ClearContent();
            Response.ClearHeaders();

            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                cSales objSales = new cSales();
                //bindEnquiryDetails();
                ddlenquiryload();
                //bind sales and marketing employee
                objSales.GetSalesAndMarketingEmployee(ddlSalesResource);
                //Bind Prospect Details
                objSales.GetProspectDetails(ddlProspect);
                //bind Enquiry Type Details
                objSales.GetEnquiryTypeName(ddlEnquiryType);
                objSales.GetHowSourcedEnquiry(ddlSource);
                objSales.GetRFPGroupName(ddlRfpGroupID);
                objSales.GetEnquiryLocation(ddlEnquiryLocation);
                objSales.GetProductName(ddlProductName);

                ShowHideControls("view");
            }
            else
                return;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlProspect_SelectedIndexChanged(object sender, EventArgs e)
    {
        c_Finance objFinance = new c_Finance();
        DataSet ds = new DataSet();
        try
        {
            if (ddlProspect.SelectedIndex > 0)
            {
                ds = objFinance.GetProspctDetailsByProspectID(Convert.ToInt32(ddlProspect.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                    txtContactNumber.Text = ds.Tables[0].Rows[0]["PhoneNumber"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();

                }
            }
            else
            {
                txtContactPerson.Text = "";
                txtContactNumber.Text = "";
                txtEmail.Text = "";
                txtContactPerson.Enabled = txtContactNumber.Enabled = txtEmail.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"
    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());
        }
        catch (Exception ec)
        {

        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        hdnAttachementFlag.Value = "A";
        ShowHideControls("add");
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearValues();
        ShowHideControls("view");
    }
    //Save Enquiry Details 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        cSales objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            if (ValidateFields())
            {
                objSales.EnquiryID = Convert.ToInt32(hdnEnquiryID.Value);
                objSales.EnquiryNumber = txtCustomerEnquiryNumber.Text;
                objSales.LseNumber = Convert.ToInt64(hdnLseNumberMaxID.Value) + 1;
                objSales.ProspectID = Convert.ToInt32(ddlProspect.SelectedValue);
                objSales.ContactPerson = txtContactPerson.Text;
                objSales.ContactNumber = Convert.ToInt64(txtContactNumber.Text);
                objSales.EmailId = txtEmail.Text;

                objSales.ProjectDescription = txtProjectDescription.Text;
                if (txtalternatenumber.Text != "") objSales.AlternateContactNumber = Convert.ToInt64(txtalternatenumber.Text);
                objSales.notinterested = Convert.ToInt32(rbl_notinterested.SelectedValue);
                objSales.budgetaryoffer = Convert.ToInt32(rbl_budgetary.SelectedValue);
                objSales.EnquiryTypeId = Convert.ToInt32(ddlEnquiryType.SelectedValue);
                if (txtEMDAmount.Text != "") objSales.EMDAmount = Convert.ToDecimal(txtEMDAmount.Text);

                objSales.ReceivedDate = DateTime.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                objSales.ClosingDate = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;

                objSales.EnquiryLocation = Convert.ToInt32(ddlEnquiryLocation.SelectedValue);

                objSales.ItemDescription = txtItemDescription.Text;
                objSales.OfferSubmissionDate = DateTime.ParseExact(txtOfferSubmissionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                objSales.ProductId = Convert.ToInt32(ddlProductName.SelectedValue);


                string MaxAttachementId = objSales.GetMaximumAttachementID();

                string AttachmentName = "";
                string FileName = "";
                string[] extension;
                if (fAttachment.HasFile)
                {
                    string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                    FileName = Path.GetFileName(fAttachment.PostedFile.FileName);

                    extension = FileName.Split('.');
                    AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];
                    objSales.AttachementName = AttachmentName;
                    objSales.AttachementTypeName = Convert.ToInt32(ddlTypeName.SelectedValue);
                    objSales.AttachementDescription = txtDescription.Text;
                }

                else
                {
                    AttachmentName = "";
                    objSales.AttachementTypeName = 0;
                    objSales.AttachementDescription = null;
                }

                objSales.UserID = Convert.ToInt32(objSession.employeeid);

                objSales.Source = Convert.ToInt32(ddlSource.SelectedValue);
                objSales.SalesResource = Convert.ToInt32(ddlSalesResource.SelectedValue);
                objSales.RfpGroupID = Convert.ToInt32(ddlRfpGroupID.SelectedValue);
                objSales.CommercialOffer = -1;

                objSales.CompanyID = Convert.ToInt32(objSession.CompanyID);

                ds = objSales.SaveCustomerEnquiryDetails();

                if (ds.Tables[0].Rows[0]["msg"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Already Exists');", true);
                else
                {
                    if (Convert.ToInt32(hdnEnquiryID.Value) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Enquiry Details Saved successfully');", true);
                        SaveAlertDetails(ds.Tables[0].Rows[0]["EnquiryID"].ToString());
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Enquiry Details  Updated successfully');", true);
                    }

                    string StrStaffDocumentPath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString() + lblEnquiryNumber.Text + "\\";

                    if (!Directory.Exists(StrStaffDocumentPath))
                        Directory.CreateDirectory(StrStaffDocumentPath);

                    if (AttachmentName != "")
                        fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);
                }

                ShowHideControls("view");
                ClearValues();

                bindEnquiryDetails(objSales.EnquiryID);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }

        finally
        {
            ds = null;
            objSales = null;
        }
    }
    protected void btnSaveAttchement_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "uniqueId" + Guid.NewGuid(), "<script>showLoader();</script>", false);
          
            objSales.EnquiryID = Convert.ToInt32(lblEnquiryNumber_V.Text);
            objSales.AttachementTypeName = Convert.ToInt32(ddlTypeName.SelectedValue);
            objSales.Description = txtDescription.Text;
            objSales.AttachementID = Convert.ToInt32(hdnAttachementID.Value);

            string MaxAttachementId = objSales.GetMaximumAttachementID();

            string AttachmentName = "";

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();

                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
            }

            string[] extension = AttachmentName.Split('.');

            AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[extension.Length - 1];

            objSales.AttachementName = AttachmentName;

            ds = objSales.saveEnquiryAttachements();


            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachment Details Saved successfully');", true);
            }

            string StrStaffDocumentPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";

            if (!Directory.Exists(StrStaffDocumentPath))
                Directory.CreateDirectory(StrStaffDocumentPath);

            if (AttachmentName != "")
                fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);
            BindAttachements(ViewState["EnquiryNumber"].ToString());

            txtDescription.Text = "";
            // }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }

        finally
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopUp();", true);
            objSales = null;
            ds = null;
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvCustomerEnquiry_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton btnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                if (dr["Budgetaryoffer"].ToString() == "1")
                {
                    Label lbl = new Label();
                    lbl.Text = "Budgetary";
                    lbl.Attributes.Add("class", "blinking budgetaryhighligt");
                    e.Row.Cells[0].Controls.Add(lbl);
                }
                if (objSession.type == 1 || objSession.type == 3)
                    btnEdit.Visible = true;
                else if (objSession.DepID == 3)
                    btnEdit.Visible = true;
                else
                    btnEdit.Visible = false;

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCustomerEnquiry_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Show Loader", "showLoader();", true);
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objSales.EnquiryID = Convert.ToInt32(gvCustomerEnquiry.DataKeys[index].Values[0].ToString());

            ds = objSales.GetEnquiryDetailsByEnquiryID();

            if (e.CommandName.ToString() == "EditEnquiry")
            {
                lblEnquiryNumber.Text = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
                hdnEnquiryID.Value = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
                txtCustomerEnquiryNumber.Text = ds.Tables[0].Rows[0]["CustomerEnquiryNumber"].ToString();
                ddlProspect.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                txtContactNumber.Text = ds.Tables[0].Rows[0]["ContactNumber"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ConTactPerson"].ToString();
                txtProjectDescription.Text = ds.Tables[0].Rows[0]["ProjectDescription"].ToString();
                ddlEnquiryType.SelectedValue = ds.Tables[0].Rows[0]["EnquiryTypeID"].ToString();
                txtEMDAmount.Text = ds.Tables[0].Rows[0]["EMDAmount"].ToString();
                if (ds.Tables[0].Rows[0]["AlternateContactNumber"].ToString() != "0") txtalternatenumber.Text = ds.Tables[0].Rows[0]["AlternateContactNumber"].ToString();
                // rbl_drawingoffer.SelectedValue = ds.Tables[0].Rows[0]["DO"].ToString();
                rbl_notinterested.SelectedValue = ds.Tables[0].Rows[0]["NI"].ToString();
                rbl_budgetary.SelectedValue = ds.Tables[0].Rows[0]["BO"].ToString();
                txtReceivedDate.Text = ds.Tables[0].Rows[0]["RD"].ToString();
                txtDeadLineDate.Text = ds.Tables[0].Rows[0]["CD"].ToString();
                ddlEnquiryLocation.SelectedValue = ds.Tables[0].Rows[0]["EL"].ToString();
                ddlSource.SelectedValue = ds.Tables[0].Rows[0]["Source"].ToString();
                ddlSalesResource.SelectedValue = ds.Tables[0].Rows[0]["SalesResource"].ToString();
                ddlRfpGroupID.SelectedValue = ds.Tables[0].Rows[0]["RFPGroupID"].ToString();

                txtOfferSubmissionDate.Text = ds.Tables[0].Rows[0]["OfferSubmissionDateEdit"].ToString();
                txtItemDescription.Text = ds.Tables[0].Rows[0]["ItemDescription"].ToString();
                ddlProductName.SelectedValue = ds.Tables[0].Rows[0]["ProductID"].ToString();

                ShowHideControls("edit");
            }

            if (e.CommandName.ToString() == "View")
            {
                lblEnquiryNumber_V.Text = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
                ViewState["EnquiryNumber"] = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
                lblCustomerEnquiryNumber_V.Text = ds.Tables[0].Rows[0]["CustomerEnquiryNumber"].ToString();
                lblProspectName_V.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();
                lblEmailID_V.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                lblContactNumber_V.Text = ds.Tables[0].Rows[0]["ContactNumber"].ToString();
                lblContactPerson_V.Text = ds.Tables[0].Rows[0]["ConTactPerson"].ToString();
                lblProjectDescription_V.Text = ds.Tables[0].Rows[0]["ProjectDescription"].ToString();
                lblEnquiryTypeName_V.Text = ds.Tables[0].Rows[0]["EnquiryTypeName"].ToString();
                lblEMDAmount_V.Text = ds.Tables[0].Rows[0]["EMDAmount"].ToString();
                lblalternatenumber.Text = ds.Tables[0].Rows[0]["AlternateContactNumber"].ToString();
                lbldrawingoffer_V.Text = ds.Tables[0].Rows[0]["Drawingoffer"].ToString();
                lblnotinterested_V.Text = ds.Tables[0].Rows[0]["notinterested"].ToString();
                lblbudgetary.Text = ds.Tables[0].Rows[0]["Budgetaryoffer"].ToString();
                lblReceivedDate_V.Text = ds.Tables[0].Rows[0]["ReceivedDate"].ToString();
                lblClosingDate_V.Text = ds.Tables[0].Rows[0]["ClosingDate"].ToString();
                lblEnquiyLocation_V.Text = ds.Tables[0].Rows[0]["EnquiryLocation"].ToString();
                lblRfpGroup_V.Text = ddlRfpGroupID.Items[Convert.ToInt32(ds.Tables[0].Rows[0]["RFPGroupID"])].Text;
                lblSalesResource_V.Text = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                BindAttachements(ViewState["EnquiryNumber"].ToString());
                lblItemDescription.Text = ds.Tables[0].Rows[0]["ItemDescription"].ToString();
                lblProductName_V.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();

                hdnAttachementFlag.Value = "V";
                ShowHideControls("view");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured!');", true);
            Log.Message("CustomerEnquiryProcess" + " " + "gvCustomerEnquiry_OnRowCommand" + " " + ex.ToString());
        }

        finally
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
            ds = null;
            objSales = null;
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtAttachement;
        objSales = new cSales();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            dtAttachement = (DataTable)ViewState["Attachement"];
            dtAttachement.DefaultView.RowFilter = "AttachementID='" + AttachementID + "'";
            dtAttachement.DefaultView.ToTable();


            if (e.CommandName.ToString() == "ViewDocs")
            {
                objcommon = new cCommon();

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();

                string fullpath = BasehttpPath + FileName;
                string Print = url.Replace(Replacevalue, fullpath);
                string newScript = "<script language='javascript'>window.open('" + fullpath.ToString() + "', '_blank');</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", newScript);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "Download('"+ fullpath+"');", true);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopUp();", true);
            }

            if (e.CommandName.ToString() == "DeleteAttachement")
            {
                objSales.AttachementID = Convert.ToInt32(dtAttachement.Rows[0]["AttachementID"].ToString());
                string AttachemnentFlag = objSales.DeleteAttachement();
                if (AttachemnentFlag == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "SuccessMessage('Success','Attachements Deleted successfully');showDataTable();", true);
                BindAttachements(ViewState["EnquiryNumber"].ToString());
            }

        }
        catch (Exception ex)
        {
            Log.Message("CustomerEnquiryProcess" + " " + "gvAttachments_OnRowCommandex" + "" + " " + ex.ToString());
        }
        finally
        {
            dtAttachement = null;
            objSales = null;
        }
    }

    protected void gvAttachments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbtnDelete_V = (LinkButton)e.Row.FindControl("lbtnDelete_V");
                string extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                
                if (extension == "PDF")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/pdf.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else if (extension == "DOC" || extension == "DOCX")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/word-ls.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else if (extension == "XLS" || extension == "XLSX")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/excel-ls.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else
                {
                    imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                }

                imgbtn.ToolTip = dr["FileName"].ToString();

                if (objSession.type == 2)
                    lbtnDelete_V.Visible = false;
                else
                    lbtnDelete_V.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"common methods"
    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;
                bindEnquiryDetails(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));
            }
            else
            {
                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    //Enquiry validation
    public bool ValidateFields()
    {
        bool isValid = true;
        string error = "";
        try
        {
            if (txtCustomerEnquiryNumber.Text.Trim() == "")
                error = "Customer Enquiry Number";

            else if (ddlProspect.SelectedIndex == 0)
                error = "Select Prospect";

            else if (txtContactPerson.Text.Trim() == "")
                error = "Contact Person";

            else if (txtContactNumber.Text.Trim() == "")
                error = "Contact Number";

            else if (txtEmail.Text.Trim() == "")
                error = "EMail";

            else if (EmailAndSmsAlerts.ValidateEmail(txtEmail.Text) == false)
                error = "Please Enter Valid Email";

            else if (ddlEnquiryType.SelectedIndex == 0)
                error = "Select EnquiryType";

            else if (txtReceivedDate.Text.Trim() == "")
                error = "Received Date";

            else if (txtDeadLineDate.Text.Trim() == "")
                error = "DeadLine Date";

            else if (ddlEnquiryLocation.SelectedIndex == 0)
                error = "Select Enquiry Location";

            else if (ddlSalesResource.SelectedIndex == 0)
                error = "Select Sales resource";

            else if (ddlSource.SelectedIndex == 0)
                error = "Select Source";

            else if (ddlRfpGroupID.SelectedIndex == 0)
                error = "Select RFP group Id";

            else if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
            }
            if (error != "")
            {
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Field Required','" + error + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return isValid;
    }

    //Attachements Validation
    private bool ValidateAttachements()
    {
        bool isValid = true;


        if (ddlTypeName.SelectedIndex == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Please Select Type Name');showAdd();showDataTable();", true);

        else if (txtDescription.Text == "")
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Please Enter Description');showAdd();showDataTable();", true);

        else if (fAttachment.HasFile)
        {
            string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();

            if (extn != ".PNG" && extn != ".DOC" && extn != ".DOCX" && extn != ".PDF" && extn != ".JPG" && extn != ".JPEG" && extn != ".XLSX" && extn != ".XLS")
            {
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Invalid File. Please upload a File with extension: .doc , .docx ,.xlsx,.xls, .pdf , .jpg , .jpeg');showAdd();showDataTable();", true);
            }
        }
        else
        {
            isValid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Please Upload File');showAdd();showDataTable();", true);
        }

        return isValid;
    }

    private void ddlenquiryload()
    {
        cCommon objc = new cCommon();
        try
        {
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();

            dsEnquiryNumber = objc.GetEnquiryNumberForReports(ddlEnquiryNumber, "LS_GetEnquiryIDForReports");
            ViewState["EnquiryIDDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }
    private void bindEnquiryDetails(int enquiry)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.EnquiryID = enquiry;
            ds = objSales.getEnquiryProcessDetails_Old();
            ViewState["EnquiryHeader"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCustomerEnquiry.DataSource = ds;
                gvCustomerEnquiry.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                gvCustomerEnquiry.DataSource = "";
                gvCustomerEnquiry.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            ds = null;
            objSales = null;
        }
    }

    public void BindAttachements(string EnquiryID)
    {
        objSales = new cSales();
        DataSet dsGetAttachementsDetails = new DataSet();
        dsGetAttachementsDetails = objSales.GetEnquiryprocessDetails(EnquiryID, "LS_GetAttachementsDetails", false);
        ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];
        try
        {
            if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
            {
                gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
            dsGetAttachementsDetails = null;
        }
    }

    private void ShowHideControls(string mode)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            divAdd.Style.Add("display", "none");
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "none");

            switch (mode.ToLower())
            {
                case "add":
                    divInput.Style.Add("display", "block");
                    txtCustomerEnquiryNumber.Focus();
                    ds = objSales.GetMaximumEnquiryID();
                    lblEnquiryNumber.Text = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
                    hdnLseNumberMaxID.Value = ds.Tables[1].Rows[0]["LseNumber"].ToString() != "" ? ds.Tables[1].Rows[0]["LseNumber"].ToString() : "30000";
                    ClearAttachements();
                    break;
                case "edit":
                    divInput.Style.Add("display", "block");
                    txtCustomerEnquiryNumber.Focus();
                    break;
                case "view":
                    //if (objSession.type == 2)
                     //   divAdd.Style.Add("display", "none");
                    //else
                        divAdd.Style.Add("display", "block");
                    divOutput.Style.Add("display", "block");
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        txtContactPerson.Enabled = true;
        txtContactNumber.Enabled = true;
        txtalternatenumber.Enabled = true;
        txtEmail.Enabled = true;
        txtCustomerEnquiryNumber.Text = "";
        ddlProspect.SelectedIndex = 0;
        txtContactPerson.Text = "";
        txtEmail.Text = "";
        txtProjectDescription.Text = "";
        ddlEnquiryType.SelectedIndex = 0;
        txtReceivedDate.Text = "";
        txtDeadLineDate.Text = "";
        ddlEnquiryLocation.SelectedIndex = 0;
        ddlSalesResource.SelectedIndex = 0;
        ddlSource.SelectedIndex = 0;
        ddlRfpGroupID.SelectedIndex = 0;
        hdnEnquiryID.Value = "0";
        fAttachment.Dispose();
        txtContactNumber.Text = "";
        txtalternatenumber.Text = "";
        txtEMDAmount.Text = "";
        lblEnquiryNumber.Text = "";
        ShowHideControls("view");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
    }

    private void ClearAttachements()
    {
        ddlTypeName.SelectedIndex = 0;
        txtDescription.Text = "";
        fAttachment.Dispose();
    }

    public void SaveAlertDetails(string EnquiryID)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            ds = objc.GetEmployeeIDDetailsByUserTypeIDSANDErpUserType("2,3,5", 3);
            string[] reciverID = ds.Tables[0].Rows[0]["EmployeeID"].ToString().Split(',');
            for (int i = 0; i < reciverID.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = reciverID[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Enquiry Allocation Alert";
                objAlerts.Message = "New Enquiry Added Please Allocate The Resources " + EnquiryID;
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
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
        if (gvCustomerEnquiry.Rows.Count > 0)
        {
            gvCustomerEnquiry.UseAccessibleHeader = true;
            gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvAttachments.Rows.Count > 0)
        {
            gvAttachments.UseAccessibleHeader = true;
            gvAttachments.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion

    #region"Web Methods"

    [WebMethod]

    public static object SaveEnquiryHeader(string xmldata)
    {
        // Initialization.         

        return "<ROOT><DATA><CID> 10 </CID></DATA></ROOT>";
    }

    #endregion
}