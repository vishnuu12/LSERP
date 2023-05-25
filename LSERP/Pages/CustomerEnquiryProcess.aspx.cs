using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;

public partial class Pages_CustomerEnquiryProcess : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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
            if (IsPostBack == false)
            {
                cSales objSales = new cSales();
                bindEnquiryDetails();
                //bind sales and marketing employee
                objSales.GetSalesAndMarketingEmployee(ddlSalesResource);
                //Bind Prospect Details
                objSales.GetProspectDetails(ddlProspect);
                //bind Enquiry Type Details
                objSales.GetEnquiryTypeName(ddlEnquiryType);
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

    #region "Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("add");
    }

    //Clear Enquiry Fields
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearValues();
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
                objSales.CommercialOffer = Convert.ToInt32(rblCommercialOffer.SelectedValue);
                objSales.EnquiryTypeId = Convert.ToInt32(ddlEnquiryType.SelectedValue);
                objSales.EMDAmount = Convert.ToDecimal(txtEMDAmount.Text);


                objSales.ReceivedDate = DateTime.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                objSales.ClosingDate = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;

                objSales.EnquiryLocation = Convert.ToInt32(ddlEnquiryLocation.SelectedValue);

                objSales.BudgetaryEnquiry = rblBudgetary.SelectedValue;

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

                ds = objSales.SaveCustomerEnquiryDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Already Exists');", true);
                else
                {
                    if (Convert.ToInt32(hdnEnquiryID.Value) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Enquiry Details Saved successfully');", true);
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
            Log.Message(ex.ToString());
        }

        finally
        {
            ds = null;
            objSales = null;
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvCustomerEnquiry_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
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
                rblCommercialOffer.SelectedValue = ds.Tables[0].Rows[0]["CO"].ToString();
                txtReceivedDate.Text = ds.Tables[0].Rows[0]["RD"].ToString();
                txtDeadLineDate.Text = ds.Tables[0].Rows[0]["CD"].ToString();
                ddlEnquiryLocation.SelectedValue = ds.Tables[0].Rows[0]["EL"].ToString();
                rblBudgetary.SelectedValue = ds.Tables[0].Rows[0]["Budget"].ToString();
                ddlSource.SelectedValue = ds.Tables[0].Rows[0]["Source"].ToString();
                ddlSalesResource.SelectedValue = ds.Tables[0].Rows[0]["SalesResource"].ToString();
                ddlRfpGroupID.SelectedValue = ds.Tables[0].Rows[0]["RFPGroupID"].ToString();
                divAttachements.Attributes.Add("style", "Display:none;");

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
                lblCommercialOffer_V.Text = ds.Tables[0].Rows[0]["CommercialOffer"].ToString();
                lblReceivedDate_V.Text = ds.Tables[0].Rows[0]["ReceivedDate"].ToString();
                lblClosingDate_V.Text = ds.Tables[0].Rows[0]["ClosingDate"].ToString();
                lblEnquiryTypeName_V.Text = ds.Tables[0].Rows[0]["EnquiryLocation"].ToString();
                lblBudgetaryEnquiry_V.Text = ds.Tables[0].Rows[0]["Budgetary"].ToString();
                lblSalesResource_V.Text = ds.Tables[0].Rows[0]["EmployeeName"].ToString();
                BindAttachements(ViewState["EnquiryNumber"].ToString());
                gvCustomerEnquiry.UseAccessibleHeader = true;
                gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopUp();showDataTable();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message("CustomerEnquiryProcess" + " " + "gvCustomerEnquiry_OnRowCommand" + " " + ex.ToString());
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
            if (ValidateAttachements())
            {
                objSales.EnquiryID = Convert.ToInt32(lblEnquiryNumber_V.Text);
                objSales.AttachementTypeName = Convert.ToInt32(ddlTypeName.SelectedValue);
                objSales.Description = txtDescription.Text;
                objSales.AttachementID = Convert.ToInt32(hdnAttachementID.Value);

                string MaxAttachementId = objSales.GetMaximumAttachementID();

                string AttachmentName = "";

                if (fAttachment.HasFile)
                {
                    string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG" || extn == ".XLS" || extn == ".XLSX")
                        AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                }
                //else
                //    AttachmentName = lblfAppointmentLetter.Text;
                string[] extension = AttachmentName.Split('.');

                AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                objSales.AttachementName = AttachmentName;

                ds = objSales.saveEnquiryAttachements();

                gvCustomerEnquiry.UseAccessibleHeader = true;
                gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachement Details Saved successfully');btnClearAttachements();ShowDataTable();", true);
                }

                string StrStaffDocumentPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";

                if (!Directory.Exists(StrStaffDocumentPath))
                    Directory.CreateDirectory(StrStaffDocumentPath);

                if (AttachmentName != "")
                    fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);

                BindAttachements(ViewState["EnquiryNumber"].ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        finally
        {
            objSales = null;
            ds = null;
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

            gvCustomerEnquiry.UseAccessibleHeader = true;
            gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (e.CommandName.ToString() == "ViewDocs")
            {
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                // string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
                cCommon.DownLoad(FileName, BasehttpPath + FileName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }

            if (e.CommandName.ToString() == "DeleteAttachement")
            {
                objSales.AttachementID = Convert.ToInt32(dtAttachement.Rows[0]["AttachementID"].ToString());
                string AttachemnentFlag = objSales.DeleteAttachement();
                if (AttachemnentFlag == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "SuccessMessage('Success','Attachements Deleted successfully');btnClearAttachements();showDataTable();", true);
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
            // string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
            string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                    byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }

                imgbtn.ToolTip = dr["FileName"].ToString();
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

            else if (txtProjectDescription.Text.Trim() == "")
                error = "Project Description";

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

                if (extn != ".DOC" && extn != ".DOCX" && extn != ".PDF" && extn != ".JPG" && extn != ".JPEG" && extn != ".XLSX" && extn != ".XLS")
                    error = "Invalid File.\n Please upload a File with extension: .doc , .docx ,.xlsx,.xls, .pdf , .jpg , .jpeg";
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

        gvCustomerEnquiry.UseAccessibleHeader = true;
        gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

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
            if (extn != ".DOC" && extn != ".DOCX" && extn != ".PDF" && extn != ".JPG" && extn != ".JPEG" && extn != ".XLSX" && extn != ".XLS")
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
            ds = objSales.getEnquiryProcessDetails();
            ViewState["EnquiryHeader"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCustomerEnquiry.DataSource = ds;
                gvCustomerEnquiry.DataBind();
                gvCustomerEnquiry.UseAccessibleHeader = true;
                gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        dsGetAttachementsDetails = objSales.GetAttachementsDetails(EnquiryID);
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
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "add":
                    divInput.Visible = true;
                    txtCustomerEnquiryNumber.Focus();
                    ds = objSales.GetMaximumEnquiryID();
                    lblEnquiryNumber.Text = ds.Tables[0].Rows[0]["EnquiryID"].ToString();
                    hdnLseNumberMaxID.Value = ds.Tables[1].Rows[0]["LseNumber"].ToString() != "" ? ds.Tables[1].Rows[0]["LseNumber"].ToString() : "30000";
                    ClearAttachements();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txtCustomerEnquiryNumber.Focus();
                    break;
                case "view":
                    divAdd.Visible = divOutput.Visible = true;
                    btnAddNew.Focus();
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
        divAttachements.Visible = true;
        fAttachment.Dispose();
        txtContactNumber.Text = "";
        txtEMDAmount.Text = "";
        lblEnquiryNumber.Text = "";
        ShowHideControls("view");
        gvCustomerEnquiry.UseAccessibleHeader = true;
        gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
    }
    private void ClearAttachements()
    {
        ddlTypeName.SelectedIndex = 0;
        txtDescription.Text = "";
        fAttachment.Dispose();
    }

    #endregion

}