using eplus.core;
using JqDatatablesWebForm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_CustomerEnquiryView : System.Web.UI.Page
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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            // Response.ContentType = "image/jpeg/eml";         
            Response.ClearContent();
            Response.ClearHeaders();

            if (IsPostBack == false)
            {
                cSales objSales = new cSales();
                HttpContext.Current.Session["CEP"] = null;
                bindEnquiryDetails();
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
            {
                if (target == "OpenCustomerEnquiry")
                {
                    DataSet ds = new DataSet();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Show Loader", "showLoader();", true);
                    objSales.EnquiryID = Convert.ToInt32(arg);

                    ds = objSales.GetEnquiryDetailsByEnquiryID();
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
                    //lblBudgetaryEnquiry_V.Text = ds.Tables[0].Rows[0]["Budgetary"].ToString();
                    lblSalesResource_V.Text = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                    BindAttachements(ViewState["EnquiryNumber"].ToString());
                    lblItemDescription.Text = ds.Tables[0].Rows[0]["ItemDescription"].ToString();
                    lblProductName_V.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();

                    hdnAttachementFlag.Value = "V";
                    //gvCustomerEnquiry.UseAccessibleHeader = true;
                    //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;
                    ShowHideControls("view");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopUp();", true);

                }
                if (target == "EditCustomerEnquiry")
                {
                    DataSet ds = new DataSet();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Show Loader", "showLoader();", true);
                    objSales.EnquiryID = Convert.ToInt32(arg);

                    ds = objSales.GetEnquiryDetailsByEnquiryID();
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
                    //rblBudgetary.SelectedValue = ds.Tables[0].Rows[0]["Budget"].ToString();
                    ddlSource.SelectedValue = ds.Tables[0].Rows[0]["Source"].ToString();
                    ddlSalesResource.SelectedValue = ds.Tables[0].Rows[0]["SalesResource"].ToString();
                    ddlRfpGroupID.SelectedValue = ds.Tables[0].Rows[0]["RFPGroupID"].ToString();

                    txtOfferSubmissionDate.Text = ds.Tables[0].Rows[0]["OfferSubmissionDateEdit"].ToString();
                    txtItemDescription.Text = ds.Tables[0].Rows[0]["ItemDescription"].ToString();
                    ddlProductName.SelectedValue = ds.Tables[0].Rows[0]["ProductID"].ToString();

                    ShowHideControls("edit");
                }
            }
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

                    // txtContactPerson.Enabled = txtContactNumber.Enabled = txtEmail.Enabled = false;
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
    //protected void btnAddNew_Click(object sender, EventArgs e)
    //{
    //    hdnAttachementFlag.Value = "A";
    //    ShowHideControls("add");
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ClearValues();
        ShowHideControls("view");
        Response.Redirect(Request.RawUrl);
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
                //objSales.Drawingoffer = Convert.ToInt32(rbl_drawingoffer.SelectedValue);
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

                //objSales.BudgetaryEnquiry = rblBudgetary.SelectedValue;

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

                bindEnquiryDetails();
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
            //if (ValidateAttachements())
            //{
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

            //else
            //    AttachmentName = lblfAppointmentLetter.Text;
            string[] extension = AttachmentName.Split('.');

            AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[extension.Length - 1];

            objSales.AttachementName = AttachmentName;

            ds = objSales.saveEnquiryAttachements();

            //gvCustomerEnquiry.UseAccessibleHeader = true;
            //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
				ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachment Details Saved successfully')", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachment Details Saved successfully');", true);
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
                //LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                if (dr["Budgetaryoffer"].ToString() == "1")
                {
                    Label lbl = new Label();
                    lbl.Text = "Budgetary";
                    lbl.Attributes.Add("class", "blinking budgetaryhighligt");
                    e.Row.Cells[0].Controls.Add(lbl);
                }
                if (objSession.type == 1 || objSession.type == 3)
                    btnEdit.Visible = true;
                else
                    btnEdit.Visible = false;
                if (dr[11].ToString() == "0")
                    btnSaveAttachements.Visible = false;
                else
                    btnSaveAttachements.Visible = true;
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
            objSales.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"]);

            ds = objSales.GetEnquiryDetailsByEnquiryID();


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

            //gvCustomerEnquiry.UseAccessibleHeader = true;
            //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (e.CommandName.ToString() == "ViewDocs")
            {
                objcommon = new cCommon();

                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "\\";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName;

                //if (FileName.ToString().Split('.')[1].ToUpper() == "ZIP")

                cCommon.DownLoad(FileName, CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName);

                //else
                //{
                //    Response.Clear();
                //    Response.BufferOutput = false;
                //
                //    objcommon.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, ViewState["EnquiryNumber"].ToString(), ifrm);
                //}

                // StoresDocsSavePath + "MaterialInwardQCCertificates" + "/" + FileName

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopUp();", true);

                //if (File.Exists(imgname))
                //{
                //    //ViewState["ifrmsrc"] = imgname;
                //    //ViewState["ifrmsrc"] = BasehttpPath + FileName;
                //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //    string s = "window.open('" + BasehttpPath + FileName + "','_blank');";
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                //}
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
            }

            if (e.CommandName.ToString() == "DeleteAttachement")
            {
                objSales.AttachementID = Convert.ToInt32(dtAttachement.Rows[0]["AttachementID"].ToString());
                string AttachemnentFlag = objSales.DeleteAttachement();
                if (AttachemnentFlag == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "SuccessMessage('Success','Attachements Deleted successfully');showDataTable();", true);
                BindAttachements(ViewState["EnquiryID"].ToString());
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
            // string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
            string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "/";
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
                    //byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                    //string base64String = Convert.ToBase64String(imageBytes);
                    //imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
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

                //if (fAttachment.PostedFile.ContentLength > 2048000)
                //    error = "File size must under 2MB";

                ////if (extn != ".DOC" && extn != ".DOCX" && extn != ".PDF" && extn != ".JPG" && extn != ".JPEG" && extn != ".XLSX" && extn != ".XLS")
                ////    error = "Invalid File.\n Please upload a File with extension: .doc , .docx ,.xlsx,.xls, .pdf , .jpg , .jpeg";
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

        //gvCustomerEnquiry.UseAccessibleHeader = true;
        //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (ddlTypeName.SelectedIndex == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Please Select Type Name');showAdd();showDataTable();", true);

        else if (txtDescription.Text == "")
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Please Enter Description');showAdd();showDataTable();", true);

        else if (fAttachment.HasFile)
        {
            string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();

            //if (fAttachment.PostedFile.ContentLength > 2048000)
            //{
            //    isValid = false;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','File size must under 2MB');showAdd();showDataTable();", true);
            //}
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

    private void bindEnquiryDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.getEnquiryProcessDetailsnew();
            ViewState["EnquiryHeader"] = ds.Tables[0];
            ViewState["EnquiryID"] = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
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
                    if (objSession.type == 2)
                        divAdd.Style.Add("display", "none");
                    else
                        divAdd.Style.Add("display", "block");
                    divOutput.Style.Add("display", "block");
                   // btnAddNew.Focus();
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
        //gvCustomerEnquiry.UseAccessibleHeader = true;
        //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    #region"Web Methods"

    [WebMethod]
    [System.Web.Script.Services.ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
    public static object GetData()
    {
        // Initialization.         
        DataTables da = new DataTables();
        List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

        //  Dictionary<string, object> data;
        string Jsonstring;
        string sessionname = "CEP";

        Jsonstring = string.Empty;
        DataSet ds = new DataSet();

        try
        {
            // Initialization.    


            string search = HttpContext.Current.Request.Params["search[value]"];

            string column0 = HttpContext.Current.Request.Params["columns[0][search][value]"];
            string column1 = HttpContext.Current.Request.Params["columns[1][search][value]"];
            string column2 = HttpContext.Current.Request.Params["columns[2][search][value]"];
            string column3 = HttpContext.Current.Request.Params["columns[3][search][value]"];
            string column4 = HttpContext.Current.Request.Params["columns[4][search][value]"];
            string column5 = HttpContext.Current.Request.Params["columns[5][search][value]"];
            string column6 = HttpContext.Current.Request.Params["columns[6][search][value]"];
            string column7 = HttpContext.Current.Request.Params["columns[7][search][value]"];

            string[] strcol = { column0, column1, column2, column3, column4, column5, column6 };

            string draw = HttpContext.Current.Request.Params["draw"];
            string order = HttpContext.Current.Request.Params["order[0][column]"];
            string orderDir = HttpContext.Current.Request.Params["order[0][dir]"];
            int startRec = Convert.ToInt32(HttpContext.Current.Request.Params["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.Params["length"]);

            //Loading.    

            if (HttpContext.Current.Session["sessionname"] == null)
            {
                dataList = LoadData();
                HttpContext.Current.Session["CEP"] = dataList;
            }
            else
                dataList = (List<Dictionary<string, object>>)HttpContext.Current.Session["CEP"];

            int totalRecords = dataList.Count;

            if (!string.IsNullOrEmpty(search) &&
              !string.IsNullOrWhiteSpace(search))
            {
                // Apply search    
                dataList = dataList.Where(p =>
                              p["CustomerEnquiryNumber"].ToString().ToLower().Contains(search.ToLower())
                          || p["ProspectName"].ToString().ToLower().Contains(search.ToLower())
                          || p["ProjectDescription"].ToString().ToLower().Contains(search.ToLower())
                          || p["ConTactPerson"].ToString().ToLower().Contains(search.ToLower())
                          || p["ReceivedDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["EnquiryLocation"].ToString().ToLower().Contains(search.ToLower())
                          || p["OfferSubmissionDate"].ToString().ToLower().Contains(search.ToLower())
                          || p["CreatedEmployeeName"].ToString().ToLower().Contains(search.ToLower())
                          || p["CreatedDate"].ToString().ToLower().Contains(search.ToLower())
                          ).ToList();
            }

            string[] colname = { "CustomerEnquiryNumber","ProspectName", "ProjectDescription", "ConTactPerson", "ReceivedDate"
                    , "EnquiryLocation", "OfferSubmissionDate", "CreatedEmployeeName", "CreatedDate"};

            for (int i = 0; i < strcol.Length; i++)
            {
                if (!string.IsNullOrEmpty(strcol[i]) && !string.IsNullOrWhiteSpace(strcol[i]) && strcol[i].ToString() != "select")
                {
                    dataList = dataList.Where(p =>
                             p[colname[i]].ToString().ToLower().Equals(strcol[i].ToLower())).ToList();
                }
            }

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            int recFilter = dataList.Count;
            // Apply pagination. 

            //EnquiryID	ProspectName	Staff	DesignStart	BomApproved	RFPStatus

            dataList = dataList.Skip(startRec).Take(pageSize).ToList();
            // Loading drop down lists.    
            da.draw = Convert.ToInt32(draw);
            da.recordsTotal = totalRecords;
            da.recordsFiltered = recFilter;
            da.Ldata = dataList;
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        // Return info.    
        return da;
    }

    private static List<Dictionary<string, object>> LoadData()
    {
        // Initialization. 
        DataSet ds = new DataSet();
        cSales objSales = new cSales();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            objSales = new cSales();
            ds = objSales.getEnquiryProcessDetailsnew();

            DataTable dt = new DataTable();
            dt = (DataTable)ds.Tables[0];

            Dictionary<string, object> row = new Dictionary<string, object>();

            DateTimeOffset d1 = DateTimeOffset.Now;
            long t1 = d1.ToUnixTimeMilliseconds();
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            DateTimeOffset d2 = DateTimeOffset.Now;
            long t2 = d2.ToUnixTimeMilliseconds();

            long t3 = t2 - t1;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        // info.    
        return rows;
    }
    public static object SaveEnquiryHeader(string xmldata)
    {
        // Initialization.         

        return "<ROOT><DATA><CID> 10 </CID></DATA></ROOT>";
    }

    #endregion
}