using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Reflection;
using eplus.core;
using eplus.data;
using System.Text;

public partial class Pages_AddEmployee : System.Web.UI.Page
{
    #region "Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    c_HR objHR = new c_HR();
    cCommon _objc = new cCommon();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (IsPostBack == false)
            {
                // AcadSetInitialRow();
                ExpSetInitialRow();
                LoadCommunication();
                txtEmployeeID.Style.Add("display", "block");
                btnAddEmployee.Style.Add("display", "block");
                ViewState["EmployeeID"] = "0";
                // hdnNew.Value = "0";
                txtEmployeeID.Focus();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCState_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindState();
        divSComm.Style.Add("display", "block");
        divEComm.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
        //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "HideLoader();", true);
    }

    protected void ddlCCoun_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCCoun();
        divSComm.Style.Add("display", "block");
        divEComm.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "HideLoader();", true);
    }

    protected void ddlPState_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPState();
        divSComm.Style.Add("display", "block");
        divEComm.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "HideLoader();", true);
    }

    protected void ddlPCoun_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPCoun();
        divSComm.Style.Add("display", "block");
        divEComm.Style.Add("display", "none");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "HideLoader();", true);
    }

    #endregion

    #region"CheckBox Events"

    protected void chkAddress_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkAddress.Checked == true)
            {
                if (ValidateCommunication())
                {
                    txtCpbox.Text = txtPPbox.Text;
                    txtCStreet.Text = txtPStreet.Text;
                    ddlCCoun.SelectedValue = ddlPCon.SelectedValue;
                    BindCCoun();
                    ddlCState.SelectedValue = ddlPState.SelectedValue;
                    BindState();
                    ddlCCity.SelectedValue = ddlPCity.SelectedValue;
                    txtCZip.Text = txtPZip.Text;
                }
                else
                {
                    chkAddress.Checked = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','All Fields Required to Copy Permanent Address');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "HideLoader();", true);
    }

    #endregion

    #region"Button Events"

    protected void btnget_click(object sender, EventArgs e)
    {
        try
        {
            if (txtEmployeeID.Text != "" && txtEmployeeID.Text.Contains("#"))
            {
                string strloginID = string.Empty;
                strloginID = txtEmployeeID.Text;
                string[] arrloginID = strloginID.Split('#');
                ViewState["EmployeeID"] = arrloginID[1];
                BindEditStaffDetails(arrloginID[1]);
                txtEmployeeID.Style.Add("display", "none");
                btnAddEmployee.Style.Add("display", "none");
                Tabs.Style.Add("display", "block");
                divBasicInfo.Style.Add("display", "block");
                ScriptManager.RegisterStartupScript(this, GetType(), "Load", "OpenTab('Communication');", true);
                txtPPbox.Focus();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.Message);
        }
    }

    protected void btnPSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (ValidatePersonal())
            {
                objHR.EFirstName = txtFirstname.Text.Trim().ToUpper();
                objHR.EMiddleName = null;
                objHR.ELastName = txtLastname.Text.Trim().ToUpper();

                int years = Convert.ToInt32(DateTime.Today.Year.ToString()) - 15;
                string[] DOB = txtDob.Text.Split('/');
                if (hdnNew.Value == "1")
                {
                    if (Convert.ToInt32(DOB[2]) <= years)
                        objHR.EDob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Minimum age is 15');", true);
                        return;
                    }
                }
                else
                    objHR.EDob = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                objHR.EDoj = DateTime.ParseExact(txtDoj.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objHR.EGender = rblGender.SelectedValue;
                objHR.EInitials = txtInitials.Text;
                objHR.ETitle = null;
                objHR.EmpType = Convert.ToInt32(ddlEmpType.SelectedValue);
                objHR.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                objHR.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
                objHR.Role = Convert.ToInt32(ddlRoles.SelectedValue);
                objHR.ERPRole = Convert.ToInt32(ddlERPRoles.SelectedValue);
                objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
                objHR.CreatedBy = _objSession.employeeid.ToString();

                string filename = "";

                if (fEmpPhoto.HasFile)
                {
                    if (fEmpPhoto.PostedFile.ContentLength < 2048000)
                    {
                        string extn = Path.GetExtension(fEmpPhoto.PostedFile.FileName).ToUpper();
                        if (extn == ".JPG" || extn == ".JPEG")
                        {
                            objHR.EPhoto = fEmpPhoto.PostedFile.FileName;
                            filename = fEmpPhoto.PostedFile.FileName;
                            lblImageName.Text = fEmpPhoto.PostedFile.FileName;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Invalid File. Please upload a File with extension: .jpg, .jpeg');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','File size exceeds 2MB');", true);
                        return;
                    }
                }
                else
                {
                    objHR.EPhoto = lblImageName.Text;
                }

                ds = objHR.SaveEmployeeBasicInfo();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Employee Already Exists');", true);
                }

                else
                {
                    ViewState["EmployeeID"] = ds.Tables[0].Rows[0]["Message"].ToString();

                    string StrStaffDocumentPath = ConfigurationManager.AppSettings["EmployeeDocsSavePath"].ToString() + ViewState["EmployeeID"].ToString() + "\\";

                    if (!Directory.Exists(StrStaffDocumentPath))
                        Directory.CreateDirectory(StrStaffDocumentPath);
                    if (filename != "")
                        fEmpPhoto.SaveAs(StrStaffDocumentPath + filename);
                    string path = "";
                    string BasehttpPath = ConfigurationManager.AppSettings["EmployeeDocs"].ToString() + ViewState["EmployeeID"].ToString() + "/";
                    path = BasehttpPath + lblImageName.Text;

                    if (lblImageName.Text != "")
                        imgPhoto.ImageUrl = Convert.ToString(path);
                    else
                        imgPhoto.ImageUrl = "../Assets/images/NoPhoto.png";

                    Tabs.Style.Add("display", "block");

                    divSComm.Style.Add("display", "block");
                    divSExperience.Style.Add("display", "block");
                    divSEducationProfile.Style.Add("display", "block");
                    divSBankDetails.Style.Add("display", "block");
                    divSDocuments.Style.Add("display", "block");
                    divSRoles.Style.Add("display", "block");

                    lblfirstname.Text = txtFirstname.Text;
                    lbllastname.Text = txtLastname.Text;
                    lblDob.Text = txtDob.Text;
                    lblDoj.Text = txtDoj.Text;
                    lblGender.Text = rblGender.SelectedItem.Text;
                    lblEmpType.Text = ddlEmpType.SelectedItem.Text;
                    lblImageName.Text = lblImageValue.Text;
                    lblInitials.Text = txtInitials.Text;
                    lblDepartment.Text = ddlDepartment.SelectedItem.Text;
                    lblDesignation.Text = ddlDesignation.SelectedItem.Text;
                    lblRoles.Text = ddlRoles.SelectedItem.Text;
                    lblERPRoles.Text = ddlERPRoles.SelectedItem.Text;

                    txtPPbox.Focus();
                    divSPersonal.Style.Add("display", "none");
                    divEPersonalDetails.Style.Add("display", "block");
                    divBasicInfo.Style.Add("display", "block");
                    btnAddEmployee.Style.Add("display", "none");
                    txtEmployeeID.Style.Add("display", "none");
                    fEmpPhoto.Style.Add("display", "none");
                    BindEditStaffDetails(ViewState["EmployeeID"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
                }
            }

        }

        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnPCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            btnAddEmployee.Style.Add("display", "block");
            txtEmployeeID.Style.Add("display", "block");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnEPersonalCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            btnAddEmployee.Style.Add("display", "block");
            txtEmployeeID.Style.Add("display", "block");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCSave_Click(object sender, EventArgs e)
    {
        try
        {
            objHR.EPerPostBoxNo = txtPPbox.Text;
            objHR.EPerStreet = txtPStreet.Text;
            objHR.EPerCountry = Convert.ToInt16(ddlPCon.SelectedValue);
            objHR.EPerState = Convert.ToInt16(ddlPState.SelectedValue);
            objHR.EPerCity = Convert.ToInt16(ddlPCity.SelectedValue);
            if (txtPZip.Text == "")
                objHR.EPerZipCode = 0;
            else
                objHR.EPerZipCode = Convert.ToInt64(txtPZip.Text);
            objHR.EComPostBoxNo = txtCpbox.Text;
            objHR.EComStreet = txtCStreet.Text;
            objHR.EComCountry = Convert.ToInt16(ddlCCoun.SelectedValue);
            objHR.EComState = Convert.ToInt16(ddlCState.SelectedValue);
            objHR.EComCity = Convert.ToInt16(ddlCCity.SelectedValue);
            if (txtCZip.Text == "")
                objHR.EComZipCode = 0;
            else
                objHR.EComZipCode = Convert.ToInt64(txtCZip.Text);
            objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
            if (txtMobileno.Text == "")
                objHR.EMobileNo = 0;
            else
                objHR.EMobileNo = Convert.ToInt64(txtMobileno.Text);
            if (txtPhoneno.Text == "")
                objHR.PhoneNo = 0;
            else
                objHR.EPhoneNo = Convert.ToInt64(txtPhoneno.Text);
            objHR.EEmailID = txtEmail.Text;
            objHR.ECorporateEmail = txtCorporateEMail.Text;
            objHR.EPrimaryEmail = Convert.ToInt16(rblPrimaryEmail.SelectedValue);
            objHR.EPrimaryMobileNo = Convert.ToInt16(rblPrimaryMobileNo.SelectedValue);
            objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
            objHR.SaveEmployeeCommunication();

            lblPbox.Text = txtPPbox.Text;
            lblPStreet.Text = txtPStreet.Text;
            lblPCoun.Text = (ddlPCon.SelectedValue == "0") ? "" : ddlPCon.SelectedItem.Text;
            lblPState.Text = (ddlPState.SelectedValue == "0") ? "" : ddlPState.SelectedItem.Text;
            lblPCity.Text = (ddlPCity.SelectedValue == "0") ? "" : ddlPCity.SelectedItem.Text;
            lblPZip.Text = txtPZip.Text;
            lblCpbox.Text = txtCpbox.Text;
            lblCStreet.Text = txtCStreet.Text;
            lblCCoun.Text = (ddlCCoun.SelectedValue == "0") ? "" : ddlCCoun.SelectedItem.Text;
            lblCState.Text = (ddlCState.SelectedValue == "0") ? "" : ddlCState.SelectedItem.Text;
            lblCCity.Text = (ddlCCity.SelectedValue == "0") ? "" : ddlCCity.SelectedItem.Text;
            lblCZip.Text = txtCZip.Text;
            lblSEmail.Text = txtEmail.Text;
            lblStudMobileNo.Text = txtMobileno.Text;
            lblPhoneNo.Text = txtPhoneno.Text;
            lblCorporateEmail.Text = txtCorporateEMail.Text;
            lblPrimaryEmail.Text = rblPrimaryEmail.SelectedItem.Text;
            lblPrimaryMobileNo.Text = rblPrimaryMobileNo.SelectedItem.Text;

            divSComm.Style.Add("display", "none");
            divEComm.Style.Add("display", "block");
            BindEditStaffDetails(ViewState["EmployeeID"].ToString());
            //    lblimage1.CssClass ="fas fa-check-circle";
            //  lblimage2.CssClass = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Roles');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnRSave_Click(object sender, EventArgs e)
    {
        try
        {
            objHR.ENationality = (ddlNationality.SelectedValue == "0") ? "" : ddlNationality.SelectedItem.Text;
            objHR.EQualification = txtQualification.Text;

            objHR.EReligion = Convert.ToInt32(ddlReligion.SelectedValue);
            objHR.EBloodGroup = Convert.ToInt32(ddlBloodGroup.SelectedValue);
            objHR.EReportingTo = ddlReportingTo.SelectedValue;
            objHR.AccessCardNo = txtAccessCardNo.Text;
            objHR.EmployeeCode = txtEmployeeCode.Text;
            objHR.ReferenceID = txtrefID.Text;
            if (txtValidityTill.Text == "")
                objHR.ValidityTill = DateTime.MaxValue;
            else if ((DateTime.ParseExact(txtValidityTill.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)) > DateTime.Today)
                objHR.ValidityTill = DateTime.ParseExact(txtValidityTill.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
            {
                divSRoles.Style.Add("display", "block");
                divERoles.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MessageRoles", "messageRoles('Error!','Validity Till greater than today's date' );", true);
                return;
            }
            objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
            objHR.SaveEmployeeProfileData();

            lblNational.Text = (ddlNationality.SelectedValue == "0") ? "" : ddlNationality.SelectedItem.Text;
            lblQualification.Text = txtQualification.Text;
            lblReligion.Text = (ddlReligion.SelectedValue == "0") ? "" : ddlReligion.SelectedItem.Text;
            lblBloodGroup.Text = (ddlBloodGroup.SelectedValue == "0") ? "" : ddlBloodGroup.SelectedItem.Text;
            lblReportingTo.Text = (ddlReportingTo.SelectedValue == "0") ? "" : ddlReportingTo.SelectedItem.Text;
            lblAccessCardNo.Text = txtAccessCardNo.Text;
            lblEmployeeCode.Text = txtEmployeeCode.Text;
            lblValidityTill.Text = txtValidityTill.Text;
            lblRefID.Text = txtrefID.Text;
            divSRoles.Style.Add("display", "none");
            divERoles.Style.Add("display", "block");
            BindEditStaffDetails(ViewState["EmployeeID"].ToString());
            //  bindSubmit();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('BankDetails');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnBSave_Click(object sender, EventArgs e)
    {
        try
        {
            objHR.EPANNo = txtPanNumber.Text;
            objHR.EPFNo = txtPFNumber.Text;
            objHR.ESalaryPaymentMode = Convert.ToInt32(ddlSalarypaymentMode.SelectedValue);
            objHR.EEmployeeESINo = txtEmployeeESINo.Text;
            objHR.EAadharNo = (txtAadharNo.Text == "") ? 0 : Convert.ToInt64(txtAadharNo.Text);
            objHR.EBankName = txtBankName.Text;
            objHR.EBankAcc = txtAccountNo.Text;
            objHR.EBankIFSC = txtBankIFSCCode.Text;
            objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());

            objHR.SaveEmployeeBankDetails();

            lblPanNumber.Text = txtPanNumber.Text;
            lblPFNumber.Text = txtPFNumber.Text;
            lblBankName.Text = txtBankName.Text;
            lblAccountNo.Text = txtAccountNo.Text;
            lblBankIFSCCode.Text = txtBankIFSCCode.Text;
            lblEmployeeESINo.Text = txtEmployeeESINo.Text;
            lblAadharNo.Text = txtAadharNo.Text;
            lblSalaryPaymentMode.Text = (ddlSalarypaymentMode.SelectedValue == "0") ? "" : ddlSalarypaymentMode.SelectedItem.Text;
            divSBankDetails.Style.Add("display", "none");
            divEBankDetails.Style.Add("display", "block");

            BindEditStaffDetails(ViewState["EmployeeID"].ToString());
            // bindSubmit();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('EducationProfile');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnExSave_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Experience"] = "";
            DataSet ds = new DataSet();
            string strOrg = "", strDesig = "", strFrom = "", strTo = "";
            int value = 0;

            int TotYear = Convert.ToInt32(ddlTotExperienceYear.SelectedValue);
            int TotMonth = Convert.ToInt32(ddlTotExperienceMonth.SelectedValue);
            int RelYear = Convert.ToInt32(ddlRelExperienceYear.SelectedValue);
            int RelMonth = Convert.ToInt32(ddlRelExperienceMonth.SelectedValue);
            int Total = (TotYear * 12) + TotMonth;
            int Relavent = (RelYear * 12) + RelMonth;
            if ((TotYear == 0) || (TotMonth == 0) || (RelMonth == 0) || (RelYear == 0))
            {
                if (Total >= Relavent)
                {
                    objHR.ETotYear = Convert.ToInt32(ddlTotExperienceYear.SelectedValue);
                    objHR.ETotMonth = Convert.ToInt32(ddlTotExperienceMonth.SelectedValue);
                    objHR.ERelYear = Convert.ToInt32(ddlRelExperienceYear.SelectedValue);
                    objHR.ERelMonth = Convert.ToInt32(ddlRelExperienceMonth.SelectedValue);
                }
                else
                {
                    divSRoles.Style.Add("display", "block");
                    divERoles.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "messageExperience", "messageExperience('Error!','Total Year Experience greater than Relavnet Experience');", true);
                    return;
                }
            }
            else
            {
                objHR.ETotYear = Convert.ToInt32(ddlTotExperienceYear.SelectedValue);
                objHR.ETotMonth = Convert.ToInt32(ddlTotExperienceMonth.SelectedValue);
                objHR.ERelYear = Convert.ToInt32(ddlRelExperienceYear.SelectedValue);
                objHR.ERelMonth = Convert.ToInt32(ddlRelExperienceMonth.SelectedValue);
            }
            if (gvExperience.Rows.Count > 0)
            {
                foreach (GridViewRow dr in gvExperience.Rows)
                {
                    int Proceed = 0;
                    TextBox ExpOrg = (TextBox)dr.FindControl("txtExpOrg");
                    TextBox ExpDesig = (TextBox)dr.FindControl("txtExpDesig");
                    TextBox ExpFrom = (TextBox)dr.FindControl("txtExpFrom");
                    TextBox ExpTo = (TextBox)dr.FindControl("txtExpTo");
                    int EEID;
                    try
                    {
                        if ((gvExperience.DataKeys[dr.RowIndex].Values[0].ToString()) == "")
                            EEID = 0;
                        else
                            EEID = Convert.ToInt32(gvExperience.DataKeys[dr.RowIndex].Values[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        EEID = 0;
                    }
                    strOrg = ExpOrg.Text;
                    strDesig = ExpDesig.Text;
                    strFrom = ExpFrom.Text;
                    strTo = ExpTo.Text;
                    if (strOrg != "" && strDesig != "" && strFrom != "0" && strTo != "0")
                    {
                        if (DateTime.ParseExact(strFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture) > DateTime.ParseExact(strTo, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                        {
                            divSExperience.Style.Add("display", "block");
                            divEExperience.Style.Add("display", "none");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageExperience", "messageExperience('Information !','To Year has to be greater than From Year');", true);
                            return;
                        }
                        else
                        {
                            if (dr.RowIndex != 0)
                            {
                                if (DateTime.ParseExact(((TextBox)gvExperience.Rows[dr.RowIndex - 1].FindControl("txtExpTo")).Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) >= DateTime.ParseExact(strFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                                {
                                    divSExperience.Style.Add("display", "block");
                                    divEExperience.Style.Add("display", "none");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "messageExperience", "messageExperience('Information !','From Date has to be greater than previous To Date');", true);
                                    return;
                                }
                                else
                                    Proceed = 1;
                            }
                            else
                                Proceed = 1;
                            if (Proceed == 1)
                            {
                                objHR.EEID = EEID;
                                objHR.EAutoNum = Convert.ToInt32(dr.RowIndex + 1);
                                objHR.EEndYear = DateTime.ParseExact(strTo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                objHR.EStartYear = DateTime.ParseExact(strFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                objHR.EOrganisation = strOrg;
                                objHR.EExpDesignation = strDesig;
                                objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
                                ds = objHR.SaveEmployeeExperience();
                                if (ds.Tables[0].Rows.Count > 0)
                                    ViewState["Experience"] = ds.Tables[0];
                            }
                        }
                    }
                }
            }
            else
                value++;
            if (ViewState["Experience"].ToString() != "")
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["Experience"];
                if (dt.Rows.Count > 0)
                {
                    BindExpGrid();
                    gvShowExperience.DataSource = dt;
                    gvShowExperience.DataBind();
                }
                else
                {
                    gvShowExperience.DataSource = "";
                    gvShowExperience.DataBind();
                }
            }

            lblTotYear.Text = ddlTotExperienceYear.SelectedValue;
            lblTotMonth.Text = ddlTotExperienceMonth.SelectedValue;
            lblRelYear.Text = ddlRelExperienceYear.SelectedValue;
            lblRelMonth.Text = ddlRelExperienceMonth.SelectedValue;
            divSExperience.Style.Add("display", "none");
            divEExperience.Style.Add("display", "block");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Documents');", true);

            lblImg5.CssClass = "fas fa-check-circle";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnDSave_Click(object sender, EventArgs e)
    {
        try
        {
            string[] Text = new string[10];
            string[] size = new string[10];
            int i = 0, k = 0;
            string AppointmentLetter = "", BankAcc = "", Resume = "", ResignLetter = "", Aadhar = "", PANCard = "", PF = "", EduPapers = "", Medical = "", Other1 = "", Other2 = "", Other3 = "", Other4 = "", Other5 = "";
            if (fAppointLetter.HasFile)
            {
                if (fAppointLetter.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fAppointLetter.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        AppointmentLetter = Path.GetFileName(fAppointLetter.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Appointment Letter,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Appointment Letter,";
                    k++;
                }
            }
            else
                AppointmentLetter = lblfAppointmentLetter.Text;
            if (fBankAcc.HasFile)
            {
                if (fBankAcc.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fBankAcc.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        BankAcc = Path.GetFileName(fBankAcc.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Bank Account Copy,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Bank Account Copy,";
                    k++;
                }
            }
            else
                BankAcc = lblfBankAcc.Text;
            if (fPanCard.HasFile)
            {
                if (fPanCard.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fPanCard.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        PANCard = Path.GetFileName(fPanCard.PostedFile.FileName);
                    else
                    {
                        Text[i] = " PAN Card,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " PAN Card,";
                    k++;
                }
            }
            else
                PANCard = lblfPanCard.Text;
            if (fAadhar.HasFile)
            {
                if (fAadhar.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fAadhar.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Aadhar = Path.GetFileName(fAadhar.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Aadhar,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Aadhar,";
                    k++;
                }
            }
            else
                Aadhar = lblfAadhar.Text;
            if (fResignLetter.HasFile)
            {
                if (fResignLetter.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fResignLetter.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        ResignLetter = Path.GetFileName(fResignLetter.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Resignation Letter,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Resignation Letter,";
                    k++;
                }
            }
            else
                ResignLetter = lblfResignLetter.Text;
            if (fResume.HasFile)
            {
                if (fResume.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fResume.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Resume = Path.GetFileName(fResume.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Resume,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Resume,";
                    k++;
                }
            }
            else
                Resume = lblfResume.Text;
            if (fPF.HasFile)
            {
                if (fPF.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fPF.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        PF = Path.GetFileName(fPF.PostedFile.FileName);
                    else
                    {
                        Text[i] = " PF Declaration Form,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " PF Declaration Form,";
                    k++;
                }
            }
            else
                PF = lblfPF.Text;
            if (fMedicalReport.HasFile)
            {
                if (fMedicalReport.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fMedicalReport.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Medical = Path.GetFileName(fMedicalReport.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Medical Report,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Medical Report,";
                    k++;
                }
            }
            else
                Medical = lblfMedical.Text;
            if (fEduPaper.HasFile)
            {
                if (fEduPaper.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fEduPaper.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        EduPapers = Path.GetFileName(fEduPaper.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Education Papers,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Education Papers,";
                    k++;
                }
            }
            else
                EduPapers = lblfEduPapers.Text;
            if (fOther1DOC.HasFile)
            {
                if (fOther1DOC.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fOther1DOC.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Other1 = Path.GetFileName(fOther1DOC.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Other Document1,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Other Document1,";
                    k++;
                }
            }
            else if (lblOther1.Text != "")
                Other1 = lblOther1.Text;
            if (fOther2DOC.HasFile)
            {
                if (fOther2DOC.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fOther2DOC.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Other2 = Path.GetFileName(fOther2DOC.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Other Document2,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Other Document2,";
                    k++;
                }
            }
            else if (lblOther2.Text != "")
                Other2 = lblOther2.Text;
            if (fOther3DOC.HasFile)
            {
                if (fOther3DOC.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fOther3DOC.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Other3 = Path.GetFileName(fOther3DOC.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Other Document3,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Other Document3,";
                    k++;
                }
            }
            else if (lblOther3.Text != "")
                Other3 = lblOther3.Text;
            if (fOther4DOC.HasFile)
            {
                if (fOther4DOC.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fOther4DOC.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Other4 = Path.GetFileName(fOther4DOC.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Other Document4,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Other Document4,";
                    k++;
                }
            }
            else if (lblOther4.Text != "")
                Other4 = lblOther4.Text;
            if (fOther5DOC.HasFile)
            {
                if (fOther5DOC.PostedFile.ContentLength < 2048000)
                {
                    string extn = Path.GetExtension(fOther5DOC.PostedFile.FileName).ToUpper();
                    if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        Other5 = Path.GetFileName(fOther5DOC.PostedFile.FileName);
                    else
                    {
                        Text[i] = " Other Document5,";
                        i++;
                    }
                }
                else
                {
                    size[k] = " Other Document5,";
                    k++;
                }
            }
            else if (lblOther5.Text != "")
                Other5 = lblOther5.Text;
            if ((i == 0) && (k == 0))
            {
                objHR.EAppointmentLetter = AppointmentLetter;
                objHR.EPF = PF;
                objHR.EAadhar = Aadhar;
                objHR.EBankAcc = BankAcc;
                objHR.EPAN = PANCard;
                objHR.EEduPapers = EduPapers;
                objHR.EMedicalReport = Medical;
                objHR.EResignLetter = ResignLetter;
                objHR.EResume = Resume;
                objHR.EOther1Name = txtOther1Name.Text;
                objHR.EOther1Doc = Other1;
                objHR.EOther2Name = txtOther2Name.Text;
                objHR.EOther2Doc = Other2;
                objHR.EOther3Name = txtOther3Name.Text;
                objHR.EOther3Doc = Other3;
                objHR.EOther4Name = txtOther4Name.Text;
                objHR.EOther4Doc = Other4;
                objHR.EOther5Name = txtOther5Name.Text;
                objHR.EOther5Doc = Other5;
                objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
                objHR.InsertEmployeeDocs();
                string StrStaffDocumentPath = ConfigurationManager.AppSettings["EmployeeDocsSavePath"].ToString() + ViewState["EmployeeID"].ToString() + "\\";
                if (!Directory.Exists(StrStaffDocumentPath))
                    Directory.CreateDirectory(StrStaffDocumentPath);
                if (AppointmentLetter != "")
                {
                    fAppointLetter.SaveAs(StrStaffDocumentPath + AppointmentLetter);
                    lblfAppointmentLetter.Text = AppointmentLetter;
                }
                if (BankAcc != "")
                {
                    fBankAcc.SaveAs(StrStaffDocumentPath + BankAcc);
                    lblfBankAcc.Text = BankAcc;
                }
                if (Aadhar != "")
                {
                    fAadhar.SaveAs(StrStaffDocumentPath + Aadhar);
                    lblfAadhar.Text = Aadhar;
                }
                if (PF != "")
                {
                    fPF.SaveAs(StrStaffDocumentPath + PF);
                    lblfPF.Text = PF;
                }
                if (ResignLetter != "")
                {
                    fResignLetter.SaveAs(StrStaffDocumentPath + ResignLetter);
                    lblfResignLetter.Text = ResignLetter;
                }
                if (EduPapers != "")
                {
                    fEduPaper.SaveAs(StrStaffDocumentPath + EduPapers);
                    lblfEduPapers.Text = EduPapers;
                }
                if (Medical != "")
                {
                    fMedicalReport.SaveAs(StrStaffDocumentPath + Medical);
                    lblfMedical.Text = Medical;
                }
                if (ResignLetter != "")
                {
                    fResignLetter.SaveAs(StrStaffDocumentPath + ResignLetter);
                    lblfResignLetter.Text = ResignLetter;
                }
                if (Resume != "")
                {
                    fResume.SaveAs(StrStaffDocumentPath + Resume);
                    lblfResume.Text = Resume;
                }
                if (Other1 != "")
                {
                    fOther1DOC.SaveAs(StrStaffDocumentPath + Other1);
                    lblOther1.Text = Other1;
                }
                if (Other2 != "")
                {
                    fOther2DOC.SaveAs(StrStaffDocumentPath + Other2);
                    lblOther2.Text = Other2;
                }
                if (Other3 != "")
                {
                    fOther3DOC.SaveAs(StrStaffDocumentPath + Other3);
                    lblOther3.Text = Other3;
                }
                if (Other4 != "")
                {
                    fOther4DOC.SaveAs(StrStaffDocumentPath + Other4);
                    lblOther4.Text = Other4;
                }
                if (Other5 != "")
                {
                    fOther5DOC.SaveAs(StrStaffDocumentPath + Other5);
                    lblOther5.Text = Other5;
                }
                if (lblfAppointmentLetter.Text != "")
                    divAppointmentLetter.Style.Add("display", "inline-block");
                if (lblfPF.Text != "")
                    divPF.Style.Add("display", "inline-block");
                if (lblfPanCard.Text != "")
                    divPanCard.Style.Add("display", "inline-block");
                if (lblfResume.Text != "")
                    divResume.Style.Add("display", "inline-block");
                if (lblfResignLetter.Text != "")
                    divResignLetter.Style.Add("display", "inline-block");
                if (lblfAadhar.Text != "")
                    divAadhar.Style.Add("display", "inline-block");
                if (lblfBankAcc.Text != "")
                    divBankAcc.Style.Add("display", "inline-block");
                if (lblfMedical.Text != "")
                    divMedical.Style.Add("display", "inline-block");
                if (lblfEduPapers.Text != "")
                    divEduPapers.Style.Add("display", "inline-block");
                if (lblOther1.Text != "")
                    divOther1.Style.Add("display", "inline-block");
                if (lblOther2.Text != "")
                    divOther2.Style.Add("display", "inline-block");
                if (lblOther3.Text != "")
                    divOther3.Style.Add("display", "inline-block");
                if (lblOther4.Text != "")
                    divOther4.Style.Add("display", "inline-block");
                if (lblOther5.Text != "")
                    divOther5.Style.Add("display", "inline-block");



                lblEAppointmentLetter.Text = lblfAppointmentLetter.Text;
                lblEBankAccountCopy.Text = lblfBankAcc.Text;
                lblEPFDeclarationForm.Text = lblfPF.Text;
                lblEPANCard.Text = lblfPanCard.Text;
                lblEAadhar.Text = lblfAadhar.Text;
                lblEMedicalReport.Text = lblfMedical.Text;
                lblEEducationPapers.Text = lblfEduPapers.Text;
                lblEResignationLetter.Text = lblfResignLetter.Text;
                lblEResume.Text = lblfResume.Text;
                lblOther1Name.Text = txtOther1Name.Text;
                lblEOther1.Text = lblOther1.Text;
                lblOther2Name.Text = txtOther2Name.Text;
                lblEOther2.Text = lblOther2.Text;
                lblOther3Name.Text = txtOther3Name.Text;
                lblEOther3.Text = lblOther3.Text;
                lblOther4Name.Text = txtOther4Name.Text;
                lblEOther4.Text = lblOther4.Text;
                lblOther5Name.Text = txtOther5Name.Text;
                lblEOther5.Text = lblOther5.Text;
                if (lblEAppointmentLetter.Text.Trim() != "")
                    btnEAppointmentLetter.Style.Add("display", "inline-block");
                if (lblEBankAccountCopy.Text.Trim() != "")
                    btnEBankAccountCopy.Style.Add("display", "inline-block");
                if (lblEPFDeclarationForm.Text.Trim() != "")
                    btnEPFDeclarationForm.Style.Add("display", "inline-block");
                if (lblEPANCard.Text.Trim() != "")
                    btnEPANCard.Style.Add("display", "inline-block");
                if (lblEAadhar.Text.Trim() != "")
                    btnEAadhar.Style.Add("display", "inline-block");
                if (lblEMedicalReport.Text.Trim() != "")
                    btnEMedicalReport.Style.Add("display", "inline-block");
                if (lblEEducationPapers.Text.Trim() != "")
                    btnEEducationPapers.Style.Add("display", "inline-block");
                if (lblEResignationLetter.Text.Trim() != "")
                    btnEResignationLetter.Style.Add("display", "inline-block");
                if (lblEResume.Text.Trim() != "")
                    btnEResume.Style.Add("display", "inline-block");
                if (lblEOther1.Text.Trim() != "")
                    divEOthers1.Style.Add("display", "inline-block");
                if (lblEOther2.Text.Trim() != "")
                    divEOthers2.Style.Add("display", "inline-block");
                if (lblEOther3.Text.Trim() != "")
                    divEOthers3.Style.Add("display", "inline-block");
                if (lblEOther4.Text.Trim() != "")
                    divEOthers4.Style.Add("display", "inline-block");
                if (lblEOther5.Text.Trim() != "")
                    divEOthers5.Style.Add("display", "inline-block");
                divSDocuments.Style.Add("display", "none");
                divEDocuments.Style.Add("display", "block");
                hdnDocsOpen.Value = "0";
            }
            else
            {
                string Error = "";
                if (k == 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        Error += Text[j];
                    }
                    divSDocuments.Style.Add("display", "block");
                    divEDocuments.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "messageDocs", "messageDocs('Invalid file format','" + Error + "');", true);
                }
                else
                {
                    for (int j = 0; j < k; j++)
                    {
                        Error += size[j];
                    }
                    divSDocuments.Style.Add("display", "block");
                    divEDocuments.Style.Add("display", "none");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "messageDocs", "messageDocs('Invalid file format','" + Error + "');", true);
                }
            }

            BindEditStaffDetails(ViewState["EmployeeID"].ToString());
            // bindSubmit();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "HideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ViewDoc();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "showAddDocs();", true);
    }

    protected void btnEdSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Attachment = "";
            Decimal Percentage;
            //  string Count = ViewState["AcadRecCount"].ToString();
            if ((txtCertification.Text != "") && (ddlYOC.SelectedValue != "0") && (txtMarks.Text != "") && (txtPercentage.Text != ""))
            {
                try
                {
                    Percentage = Convert.ToDecimal(txtPercentage.Text);
                }
                catch (Exception ex)
                {
                    Percentage = 0;
                }
                if (fAttachment.HasFile)
                {
                    if (fAttachment.PostedFile.ContentLength < 2048000)
                    {
                        string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                        if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        {
                            Attachment = Path.GetFileName(fAttachment.PostedFile.FileName);
                            //SaveAttachment("AcadRecords_" + Count, Attachment);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageEducation", "messageEducationAdd('Invalid File !','Please upload a File with extension: .doc , .docx, .pdf');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageEducation", "messageEducationAdd('Error!','File size exceeds 2MB');", true);
                        return;
                    }
                }

                DataSet ds = new DataSet();
                objHR.ECertification = txtCertification.Text;
                objHR.ESubjects = txtSubjects.Text;
                objHR.EMarks = txtMarks.Text;
                objHR.EPercentage = Percentage;
                objHR.ELocation = txtBoard.Text;
                objHR.EYOC = Convert.ToInt32(ddlYOC.SelectedValue);
                objHR.EAttachments = Attachment;
                objHR.EARID = 0;
                objHR.CreatedBy = _objSession.employeeid;
                objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
                ds = objHR.SaveEmployeeAcadRecords();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvShowAcaDetails.DataSource = ds.Tables[0];
                    gvShowAcaDetails.DataBind();
                }
                txtCertification.Text = "";
                txtSubjects.Text = "";
                txtMarks.Text = "";
                txtBoard.Text = "";
                txtPercentage.Text = "";
                ddlYOC.SelectedValue = "0";

                if (fAttachment.HasFile)
                {
                    string StrStaffDocumentPath = ConfigurationManager.AppSettings["EmployeeDocsSavePath"].ToString() + ViewState["EmployeeID"].ToString() + "\\";
                    if (!Directory.Exists(StrStaffDocumentPath))
                        Directory.CreateDirectory(StrStaffDocumentPath);
                    if (Attachment != "")
                        fAttachment.SaveAs(StrStaffDocumentPath + Attachment);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('EducationProfile');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageEducation", "messageEducationAdd('Error!','Field Required');", true);

            int rowIndex = 0, value = 0;
            for (int i = 0; i < gvShowAcaDetails.Rows.Count; i++)
            {
                Label lblCertification = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblCertification");
                Label lblSubject = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblSubject");
                Label lblMarks = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblMarks");
                Label lblPercentage = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblPercentage");
                Label lblLocation = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblLocation");
                Label lblYOC = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblYOC");
                Label lblAttachments = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblAttachments");

                if (lblAttachments.Text.Trim() != "")
                    gvShowAcaDetails.Rows[rowIndex].FindControl("btnView").Visible = true;
                else
                    gvShowAcaDetails.Rows[rowIndex].FindControl("btnView").Visible = false;

                if (rowIndex == 0 || rowIndex == 1)
                {
                    if ((lblCertification.Text == "") || (lblSubject.Text == "") || (lblMarks.Text == "") || (lblPercentage.Text == "") || (lblLocation.Text == "") || (lblYOC.Text == ""))// || (lblAttachments.Text == "")
                        value++;
                }
                rowIndex++;
            }
            lblImg4.CssClass = "fas fa-check-circle";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnEdESave_Click(object sender, EventArgs e)
    {
        try
        {
            string Attachment = "";
            Decimal Percentage;
            string Count = ViewState["AcadRecECount"].ToString();
            if ((txtECertification.Text != "") && (ddlEYOC.SelectedValue != "0") && (txtEMarks.Text != "") && (txtEPercentage.Text != ""))
            {
                try
                {
                    Percentage = Convert.ToDecimal(txtEPercentage.Text);
                }
                catch (Exception ex)
                {
                    Percentage = 0;
                }
                if (fEAttachment.HasFile)
                {
                    if (fEAttachment.PostedFile.ContentLength < 2048000)
                    {
                        string extn = Path.GetExtension(fEAttachment.PostedFile.FileName).ToUpper();
                        if (extn == ".DOC" || extn == ".DOCX" || extn == ".PDF" || extn == ".JPG" || extn == ".JPEG")
                        {
                            Attachment = Path.GetFileName(fEAttachment.PostedFile.FileName);
                            //SaveAttachment("AcadRecords_" + Count, Attachment);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "messageEducation", "messageEducationEdit('Invalid File !','Please upload a File with extension: .doc , .docx, .pdf');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "messageEducation", "messageEducationEdit('Error!','File size exceeds 2MB');", true);
                        return;
                    }
                }
                else if (hdnFileName.Value != "")
                    Attachment = hdnFileName.Value;
                else
                    Attachment = "";

                DataSet ds = new DataSet();
                objHR.ECertification = txtECertification.Text;
                objHR.ESubjects = txtESubjects.Text;
                objHR.EMarks = txtEMarks.Text;
                objHR.EPercentage = Percentage;
                objHR.ELocation = txtEBoard.Text;
                objHR.EAttachments = Attachment;
                objHR.EYOC = Convert.ToInt32(ddlEYOC.SelectedValue);
                objHR.EARID = Convert.ToInt32(hdnEARID.Value);
                objHR.CreatedBy = _objSession.employeeid;
                objHR.EmpID = Convert.ToInt32(ViewState["EmployeeID"].ToString());
                ds = objHR.SaveEmployeeAcadRecords();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvShowAcaDetails.DataSource = ds.Tables[0];
                    gvShowAcaDetails.DataBind();
                    gvShowAcaDetails.Focus();
                }

                if (fEAttachment.HasFile)
                {
                    string StrStaffDocumentPath = ConfigurationManager.AppSettings["EmployeeDocsSavePath"].ToString() + ViewState["EmployeeID"].ToString() + "\\";
                    if (!Directory.Exists(StrStaffDocumentPath))
                        Directory.CreateDirectory(StrStaffDocumentPath);
                    if (Attachment != "")
                        fEAttachment.SaveAs(StrStaffDocumentPath + Attachment);
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('EducationProfile');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageEducation", "messageEducationEdit('Error!','Field Required');", true);
            int rowIndex = 0, value = 0;
            for (int i = 0; i < gvShowAcaDetails.Rows.Count; i++)
            {
                Label lblCertification = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblCertification");
                Label lblSubject = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblSubject");
                Label lblMarks = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblMarks");
                Label lblPercentage = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblPercentage");
                Label lblLocation = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblLocation");
                Label lblYOC = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblYOC");
                Label lblAttachments = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblAttachments");

                if (lblAttachments.Text.Trim() != "")
                    gvShowAcaDetails.Rows[rowIndex].FindControl("btnView").Visible = true;
                else
                    gvShowAcaDetails.Rows[rowIndex].FindControl("btnView").Visible = false;

                if (rowIndex == 0 || rowIndex == 1)
                {
                    if ((lblCertification.Text == "") || (lblSubject.Text == "") || (lblMarks.Text == "") || (lblPercentage.Text == "") || (lblLocation.Text == "") || (lblYOC.Text == "")) //|| (lblAttachments.Text == "")
                        value++;
                }
                rowIndex++;
            }

            lblImg4.CssClass = "fas fa-check-circle";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvShowAcaDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "View")
            {
                Label lblFileName = (Label)gvShowAcaDetails.Rows[index].FindControl("lblAttachments");
                hdnFileName.Value = lblFileName.Text;
                ViewDoc();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "showAdd();", true);
            }
            else if (e.CommandName == "Edit")
            {
                ViewState["AcadRecECount"] = index + 1;
                Label lblCertification = (Label)gvShowAcaDetails.Rows[index].FindControl("lblCertification");
                Label lblSubject = (Label)gvShowAcaDetails.Rows[index].FindControl("lblSubject");
                Label lblMarks = (Label)gvShowAcaDetails.Rows[index].FindControl("lblMarks");
                Label lblPercentage = (Label)gvShowAcaDetails.Rows[index].FindControl("lblPercentage");
                Label lblLocation = (Label)gvShowAcaDetails.Rows[index].FindControl("lblLocation");
                Label lblYOC = (Label)gvShowAcaDetails.Rows[index].FindControl("lblYOC");
                Label lblAttachments = (Label)gvShowAcaDetails.Rows[index].FindControl("lblAttachments");
                hdnEARID.Value = gvShowAcaDetails.DataKeys[index].Values[0].ToString();
                txtECertification.Text = lblCertification.Text;
                txtESubjects.Text = lblSubject.Text;
                txtEMarks.Text = lblMarks.Text;
                txtEPercentage.Text = lblPercentage.Text;
                txtEBoard.Text = lblLocation.Text;
                ddlEYOC.SelectedValue = (lblYOC.Text == "") ? "0" : lblYOC.Text;
                hdnFileName.Value = lblAttachments.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "btnEdit();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvShowAcaDetails_RowEditing(object sender, GridViewEditEventArgs e) { }

    protected void gvShowAcaDetails_RowDataBound(object sender, GridViewRowEventArgs e) { }

    protected void gvExperience_OnRowDataBound(object sender, GridViewRowEventArgs e) { }

    protected void gvShowExperience_OnRowDataBound(object sender, GridViewRowEventArgs e) { }

    #endregion

    #region "Common Methods"

    private void ExpSetInitialRow()
    {
        try
        {


            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Organization", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("StartYear", typeof(string)));
            dt.Columns.Add(new DataColumn("EndYear", typeof(string)));
            dt.Columns.Add(new DataColumn("EEID", typeof(string)));
            for (int i = 0; i <= 5; i++)
            {
                dr = dt.NewRow();
                dr["Organization"] = string.Empty;
                dr["Designation"] = string.Empty;
                dr["StartYear"] = string.Empty;
                dr["EndYear"] = string.Empty;
                dr["EEID"] = string.Empty;
                dt.Rows.Add(dr);
                ViewState["CurrentTable"] = dt;
            }



            gvExperience.DataSource = dt;
            gvExperience.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindExpGrid()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["Experience"];

            DataTable dtExp = new DataTable();
            dtExp = (DataTable)ViewState["CurrentTable"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtExp.Rows[i]["Organization"] = dt.Rows[i]["Organization"].ToString();
                dtExp.Rows[i]["Designation"] = dt.Rows[i]["Designation"].ToString();
                dtExp.Rows[i]["StartYear"] = dt.Rows[i]["StartYear"].ToString();
                dtExp.Rows[i]["EndYear"] = dt.Rows[i]["EndYear"].ToString();
                dtExp.Rows[i]["EEID"] = dt.Rows[i]["EEID"].ToString();
            }

            gvExperience.DataSource = dtExp;
            gvExperience.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void BindState()
    {
        try
        {
            ddlCCity.Items.Clear();
            _objCommon.StateID = Convert.ToInt64(ddlCState.SelectedValue);
            _objCommon.getCity(ddlCCity);

        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    public void BindCCoun()
    {
        try
        {
            ddlCState.Items.Clear();
            ddlCCity.Items.Clear();
            ddlCCity.Items.Insert(0, new ListItem("--Select--", "0"));
            _objCommon.CountryID = Convert.ToInt64(ddlCCoun.SelectedValue);
            _objCommon.getState(ddlCState);
            //  ddlCState.Focus();
            //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
            //mpeLoader.Hide();
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    public void BindPState()
    {
        try
        {
            ddlPCity.Items.Clear();
            _objCommon.StateID = Convert.ToInt64(ddlPState.SelectedValue);
            _objCommon.getCity(ddlPCity);
            //   ddlPCity.Focus();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
            //mpeLoader.Hide();
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    public void BindPCoun()
    {
        try
        {
            ddlPState.Items.Clear();
            ddlPCity.Items.Clear();
            ddlPCity.Items.Insert(0, new ListItem("--Select--", "0"));
            _objCommon.CountryID = Convert.ToInt64(ddlPCon.SelectedValue);
            _objCommon.getState(ddlPState);
            //   ddlPState.Focus();
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Load", "OpenTab('Communication');", true);
            //mpeLoader.Hide();
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    private void LoadCommunication()
    {
        try
        {
            _objCommon.GetDepartmentName(ddlDepartment);
            _objCommon.getReligion(ddlReligion);
            objHR.GetEmployeeList(ddlReportingTo);
            _objCommon.GetRoles(ddlRoles);
            _objCommon.GetERPRoles(ddlERPRoles);
            _objCommon.GetDesignation(ddlDesignation);
            _objCommon.GetBloodGroup(ddlBloodGroup);
            _objCommon.GetEmploymentType(ddlEmpType);
            _objCommon.getNationality(ddlNationality);
            _objCommon.getCountry(ddlCCoun);
            _objCommon.getCountry(ddlPCon);
            objHR.GetSalaryPaymentMode(ddlSalarypaymentMode);

            ddlYOC.Items.Clear();
            ddlEYOC.Items.Clear();
            int curYear = Convert.ToInt32(DateTime.Now.Year.ToString());
            ddlYOC.Items.Add(new ListItem("--Select--", "0"));
            ddlEYOC.Items.Add(new ListItem("--Select--", "0"));
            for (int i = curYear - 50; i <= curYear + 10; i++)
            {
                ddlYOC.Items.Add(i.ToString());
                ddlEYOC.Items.Add(i.ToString());
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public bool ValidatePersonal()
    {
        bool isValid = true;
        if (txtFirstname.Text == "")
            isValid = false;
        if (txtLastname.Text == "")
            isValid = false;
        if (txtDob.Text == "")
            isValid = false;
        if (txtDoj.Text == "")
            isValid = false;
        if (ddlEmpType.SelectedValue == "0")
            isValid = false;
        //if (txtTitle.Text == "")
        //    isValid = false;
        if (txtInitials.Text == "")
            isValid = false;
        if (ddlDepartment.SelectedValue == "0")
            isValid = false;
        if (ddlDesignation.SelectedValue == "0")
            isValid = false;
        if (ddlERPRoles.SelectedValue == "0")
            isValid = false;
        if (ddlRoles.SelectedValue == "0")
            isValid = false;

        return isValid;
    }

    private void ClearControls()
    {
        try
        {
            txtEmployeeID.Text = "";
            lblfirstname.Text = "";
            //  lblMiddleName.Text = "";
            lbllastname.Text = "";
            //lblName.Text = "";
            lblInitials.Text = "";
            lblGender.Text = "";
            //lblMaritalStatus.Text = "";
            lblDoj.Text = "";
            lblDob.Text = "";
            lblImageName.Text = "";
            lblEmpType.Text = "";
            //    lblTitle.Text = "";
            lblRoles.Text = "";
            lblDepartment.Text = "";
            lblDesignation.Text = "";
            lblAccessCardNo.Text = "";
            lblEmployeeCode.Text = "";
            lblReportingTo.Text = "";
            lblValidityTill.Text = "";
            lblRefID.Text = "";
            txtFirstname.Text = "";
            //  txtMiddleName.Text = "";
            txtLastname.Text = "";
            txtInitials.Text = "";
            rblGender.SelectedValue = "0";
            txtDoj.Text = "";
            txtDob.Text = "";
            ddlEmpType.SelectedValue = "0";
            // txtTitle.Text = "";
            ddlReportingTo.SelectedValue = "0";
            ddlRoles.SelectedValue = "0";
            txtAccessCardNo.Text = "";
            txtEmployeeCode.Text = "";
            txtValidityTill.Text = "";
            txtrefID.Text = "";
            ddlDepartment.SelectedValue = "0";
            ddlDesignation.SelectedValue = "0";
            fEmpPhoto.Style.Add("display", "none");
            txtQualification.Text = "";
            ddlTotExperienceYear.SelectedValue = "0";
            ddlTotExperienceMonth.SelectedValue = "0";
            ddlRelExperienceYear.SelectedValue = "0";
            ddlRelExperienceMonth.SelectedValue = "0";
            lblTotMonth.Text = "";
            lblTotYear.Text = "";
            lblRelMonth.Text = "";
            lblRelYear.Text = "";

            ddlEmpType.SelectedValue = "0";
            txtAccessCardNo.Text = string.Empty;
            ddlRoles.SelectedValue = "0";

            lblPbox.Text = "";
            lblPStreet.Text = "";
            lblPStreet.Text = "";
            lblPCoun.Text = "";
            lblPState.Text = "";
            lblPCity.Text = "";
            lblPZip.Text = "";
            lblCpbox.Text = "";
            lblCStreet.Text = "";
            lblCCoun.Text = "";
            lblCState.Text = "";
            lblCCity.Text = "";
            lblCZip.Text = "";
            lblPhoneNo.Text = "";
            lblStudMobileNo.Text = "";
            //lblBusinessNo.Text = "";
            lblSEmail.Text = "";
            lblCorporateEmail.Text = "";
            lblPrimaryEmail.Text = "";
            lblPrimaryMobileNo.Text = "";

            txtPPbox.Text = "";
            txtPStreet.Text = "";
            ddlPCon.SelectedValue = "0";
            ddlPState.Items.Clear();
            ddlPState.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPCity.Items.Clear();
            ddlPCity.Items.Insert(0, new ListItem("--Select--", "0"));
            txtPZip.Text = "";
            txtCpbox.Text = "";
            txtCStreet.Text = "";
            ddlCCoun.SelectedValue = "0";
            ddlCState.Items.Clear();
            ddlCState.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCCity.Items.Clear();
            ddlPCity.Items.Insert(0, new ListItem("--Select--", "0"));
            txtCZip.Text = "";
            txtMobileno.Text = "";
            txtEmail.Text = "";
            txtPhoneno.Text = "";
            txtCorporateEMail.Text = "";
            rblPrimaryEmail.SelectedValue = "0";
            rblPrimaryMobileNo.SelectedValue = "0";
            lblAccountNo.Text = "";
            lblBankName.Text = "";
            txtAccountNo.Text = "";
            txtBankName.Text = "";
            txtPanNumber.Text = "";
            txtPFNumber.Text = "";
            txtEmployeeESINo.Text = "";
            txtEmployeeCode.Text = "";
            txtAadharNo.Text = "";
            txtBankIFSCCode.Text = "";

            lblPanNumber.Text = "";
            lblPFNumber.Text = "";
            lblEmployeeESINo.Text = "";
            lblEmployeeCode.Text = "";
            lblAadharNo.Text = "";
            lblBankIFSCCode.Text = "";

            lblReligion.Text = "";
            lblDepartment.Text = "";
            lblReportingTo.Text = "";
            lblDesignation.Text = "";
            lblAccessCardNo.Text = "";
            lblQualification.Text = "";
            lblNational.Text = "";
            lblBloodGroup.Text = "";
            lblValidityTill.Text = "";
            lblRefID.Text = "";

            ddlReligion.SelectedValue = "0";
            ddlDepartment.SelectedValue = "0";
            ddlReportingTo.SelectedValue = "0";
            ddlDesignation.SelectedValue = "0";
            txtAccessCardNo.Text = "";
            txtQualification.Text = "";
            ddlNationality.SelectedValue = "0";
            ddlBloodGroup.SelectedValue = "0";
            txtValidityTill.Text = "";
            txtrefID.Text = "";

            ddlSalarypaymentMode.SelectedValue = "0";
            lblEAppointmentLetter.Text = "";
            lblEBankAccountCopy.Text = "";
            lblEResume.Text = "";
            lblEPANCard.Text = "";
            lblEResignationLetter.Text = "";
            lblEEducationPapers.Text = "";
            lblEMedicalReport.Text = "";
            lblEPFDeclarationForm.Text = "";
            lblEAadhar.Text = "";
            lblOther1Name.Text = "";
            lblOther2Name.Text = "";
            lblOther3Name.Text = "";
            lblOther4Name.Text = "";
            lblOther5Name.Text = "";
            lblEOther1.Text = "";
            lblEOther2.Text = "";
            lblEOther3.Text = "";
            lblEOther4.Text = "";
            lblEOther5.Text = "";
            lblfAppointmentLetter.Text = "";
            lblfBankAcc.Text = "";
            lblfResume.Text = "";
            lblfPanCard.Text = "";
            lblfResignLetter.Text = "";
            lblfEduPapers.Text = "";
            lblfMedical.Text = "";
            lblfResignLetter.Text = "";
            lblfAadhar.Text = "";
            txtOther1Name.Text = "";
            txtOther2Name.Text = "";
            txtOther3Name.Text = "";
            txtOther4Name.Text = "";
            txtOther5Name.Text = "";
            lblOther1.Text = "";
            lblOther2.Text = "";
            lblOther3.Text = "";
            lblOther4.Text = "";
            lblOther5.Text = "";

            hdnNew.Value = "0";
            imgPhoto.ImageUrl = "../Assets/images/NoPhoto.png";
            divAppointmentLetter.Style.Add("display", "none");
            divBankAcc.Style.Add("display", "none");
            divResignLetter.Style.Add("display", "none");
            divResume.Style.Add("display", "none");
            divPanCard.Style.Add("display", "none");
            divPF.Style.Add("display", "none");
            divMedical.Style.Add("display", "none");
            divEduPapers.Style.Add("display", "none");
            divAadhar.Style.Add("display", "none");
            divOther1.Style.Add("display", "none");
            divOther2.Style.Add("display", "none");
            divOther3.Style.Add("display", "none");
            divOther4.Style.Add("display", "none");
            divOther5.Style.Add("display", "none");
            Tabs.Style.Add("display", "none");

            divSPersonal.Style.Add("display", "none");
            divEPersonalDetails.Style.Add("display", "none");

            divBasicInfo.Style.Add("display", "none");
            ViewState["EmployeeID"] = "0";
            // Session["EID"] = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public bool ValidateCommunication()
    {
        bool isvalid = true;
        if (txtPPbox.Text == "")
            isvalid = false;
        if (txtPStreet.Text == "")
            isvalid = false;
        if (txtPZip.Text == "")
            isvalid = false;
        if (ddlPCon.SelectedValue == "0")
            isvalid = false;
        if (ddlPCity.SelectedValue == "0")
            isvalid = false;
        if (ddlPState.SelectedValue == "0")
            isvalid = false;
        return isvalid;
    }

    public void ViewDoc()
    {
        try
        {
            string BasehttpPath = ConfigurationManager.AppSettings["EmployeeDocs"].ToString() + ViewState["EmployeeID"].ToString() + "/";
            BasehttpPath = BasehttpPath + hdnFileName.Value;

          //  BasehttpPath = "D:/LiveImages/LSERP/EnquiryDocs/100000/LSEDRA_100000_3.jpeg";

            if (hdnFileName.Value.Contains(".pdf"))
                divContent.InnerHtml = "<object data='" + BasehttpPath + "' width='100%' height='100%'></object> ";
            else
            {
                divContent.InnerHtml = "<iframe src='"+ BasehttpPath +"' width='100%' height='500px' frameborder='0' id='frame1'></iframe>";
                //   divContent.InnerHtml = "<iframe src='"+BasehttpPath+"' style='width:100%; height:100%;' frameborder='0' id='frame1'></iframe>";
                 // byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath);
                 // string base64String = Convert.ToBase64String(imageBytes);

                 // ImgViewDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);         
            }
            Updateview.Update();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindEditStaffDetails(string ID)
    {
        try
        {
            DataSet ds = new DataSet();
            objHR.EmpID = Convert.ToInt32(ID);
            ds = objHR.GetStaffDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    //hfStaffId.Value = ds.Tables[1].Rows[0]["EmployeeID"].ToString();    
                    ViewState["EmployeeID"] = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                    lblfirstname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    // lblMiddleName.Text = ds.Tables[0].Rows[0]["MiddileName"].ToString();
                    lbllastname.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                    lblInitials.Text = ds.Tables[0].Rows[0]["Intials"].ToString();
                    lblGender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
                    lblDoj.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    lblDob.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    lblImageName.Text = ds.Tables[0].Rows[0]["Photo"].ToString();
                    lblEmpType.Text = ds.Tables[0].Rows[0]["EmploymentTypeName"].ToString();
                    //  lblTitle.Text = ds.Tables[0].Rows[0]["Titile"].ToString();
                    lblRoles.Text = ds.Tables[0].Rows[0]["RolesName"].ToString();
                    lblERPRoles.Text = ds.Tables[0].Rows[0]["ERPRoleName"].ToString();
                    lblDepartment.Text = ds.Tables[0].Rows[0]["DepartmentName"].ToString();
                    lblDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();

                    lblAccessCardNo.Text = ds.Tables[0].Rows[0]["AccessCardNo"].ToString();
                    lblReportingTo.Text = ds.Tables[0].Rows[0]["ReportingToName"].ToString();
                    lblEmployeeCode.Text = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                    lblValidityTill.Text = ds.Tables[0].Rows[0]["ValidityTill"].ToString();
                    lblRefID.Text = ds.Tables[0].Rows[0]["ReferenceID"].ToString();


                    txtEmployeeCode.Text = ds.Tables[0].Rows[0]["EmployeeCode"].ToString();
                    txtValidityTill.Text = ds.Tables[0].Rows[0]["ValidityTill"].ToString();
                    txtrefID.Text = ds.Tables[0].Rows[0]["ReferenceID"].ToString();
                    txtAccessCardNo.Text = ds.Tables[0].Rows[0]["AccessCardNo"].ToString();

                    ddlReportingTo.SelectedValue = (ds.Tables[0].Rows[0]["ReportingTo"].ToString() == "") ? "0" : ds.Tables[0].Rows[0]["ReportingTo"].ToString();

                    txtFirstname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    // txtMiddleName.Text = ds.Tables[0].Rows[0]["MiddileName"].ToString();
                    txtLastname.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                    txtInitials.Text = ds.Tables[0].Rows[0]["Intials"].ToString();
                    rblGender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                    txtDoj.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                    txtDob.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    ddlEmpType.SelectedValue = ds.Tables[0].Rows[0]["EmploymentType"].ToString();
                    //      txtTitle.Text = ds.Tables[0].Rows[0]["Titile"].ToString();
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DepartmentID"].ToString();
                    ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["DesignationID"].ToString();
                    ddlRoles.SelectedValue = ds.Tables[0].Rows[0]["RolesID"].ToString();
                    ddlERPRoles.SelectedValue = ds.Tables[0].Rows[0]["ERPRoleID"].ToString();

                    try
                    {
                        ddlDepartment.SelectedValue = (ds.Tables[0].Rows[0]["DepartmentID"].ToString() == "") ? "0" : ds.Tables[0].Rows[0]["DepartmentID"].ToString();
                    }
                    catch (Exception ex)
                    {
                        ddlDepartment.SelectedValue = "0";
                        //StuPhoto.Style.Add("display", "block");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information!','Department is in InActive State');", true);
                        ddlDepartment.Focus();
                        //catchValue = 1;
                    }

                    ddlDesignation.SelectedValue = (ds.Tables[0].Rows[0]["DesignationID"].ToString() == "") ? "0" : ds.Tables[0].Rows[0]["DesignationID"].ToString();

                    if (ds.Tables[0].Rows[0]["Photo"].ToString() != "")
                    {
                        string path = "";
                        string BasehttpPath = ConfigurationManager.AppSettings["EmployeeDocs"].ToString() + ViewState["EmployeeID"].ToString() + "/";
                        path = BasehttpPath + ds.Tables[0].Rows[0]["Photo"].ToString();
                        imgPhoto.ImageUrl = Convert.ToString(path);
                    }

                    else
                        imgPhoto.ImageUrl = "../Assets/images/NoPhoto.png";


                    if ((lblfirstname.Text.Trim() == "") && (lbllastname.Text.Trim() == "") && (lblGender.Text.Trim() == "") && (lblDob.Text.Trim() == "") && (lblDoj.Text.Trim() == "") && (lblEmpType.Text.Trim() == "") && (lblImageName.Text.Trim() == "") && (lblInitials.Text.Trim() == ""))
                    {
                        divSPersonal.Style.Add("display", "block");
                        divEPersonalDetails.Style.Add("display", "none");

                    }
                    else
                    {
                        divEPersonalDetails.Style.Add("display", "block");
                        divSPersonal.Style.Add("display", "none");
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                try
                {
                    lblPbox.Text = ds.Tables[1].Rows[0]["PerPostBoxNo"].ToString();
                    lblPStreet.Text = ds.Tables[1].Rows[0]["PerStreet"].ToString();
                    lblPCoun.Text = ds.Tables[1].Rows[0]["PerCountryName"].ToString();
                    lblPState.Text = ds.Tables[1].Rows[0]["PerStateName"].ToString();
                    lblPCity.Text = ds.Tables[1].Rows[0]["PerCityName"].ToString();
                    lblPZip.Text = ds.Tables[1].Rows[0]["PerZipCode"].ToString();
                    lblCpbox.Text = ds.Tables[1].Rows[0]["ComPostBoxNo"].ToString();
                    lblCStreet.Text = ds.Tables[1].Rows[0]["ComStreet"].ToString();
                    lblCCoun.Text = ds.Tables[1].Rows[0]["ComCountryName"].ToString();
                    lblCState.Text = ds.Tables[1].Rows[0]["ComStateName"].ToString();
                    lblCCity.Text = ds.Tables[1].Rows[0]["ComCityName"].ToString();
                    lblCZip.Text = ds.Tables[1].Rows[0]["ComZipCode"].ToString();
                    lblPhoneNo.Text = ds.Tables[1].Rows[0]["PhoneNo"].ToString();
                    lblStudMobileNo.Text = ds.Tables[1].Rows[0]["MobileNo"].ToString();
                    lblSEmail.Text = ds.Tables[1].Rows[0]["Email"].ToString();
                    if (lblPZip.Text == "0")
                        lblPZip.Text = "";
                    if (lblCZip.Text == "0")
                        lblCZip.Text = "";
                    if (lblStudMobileNo.Text == "0")
                        lblStudMobileNo.Text = "";
                    if (lblPhoneNo.Text == "0")
                        lblPhoneNo.Text = "";
                    lblCorporateEmail.Text = ds.Tables[1].Rows[0]["CorporateEmail"].ToString();
                    if (ds.Tables[1].Rows[0]["PrimaryEmail"].ToString() == "0")
                        lblPrimaryEmail.Text = "Personal Email";
                    else
                        lblPrimaryEmail.Text = "Corporate Email";
                    if (ds.Tables[1].Rows[0]["PrimaryMobileNo"].ToString() == "0")
                        lblPrimaryMobileNo.Text = "Personal Mobile Number";
                    else
                        lblPrimaryMobileNo.Text = "Alternate Mobile Number";
                    if ((lblPbox.Text.Trim() == "") && (lblPStreet.Text.Trim() == "") && (lblPCoun.Text.Trim() == "") && (lblPState.Text.Trim() == "") && (lblPCity.Text.Trim() == "") && (lblPZip.Text.Trim() == "") && (lblCpbox.Text.Trim() == "") && (lblCStreet.Text.Trim() == "") && (lblCCoun.Text.Trim() == "") && (lblCState.Text.Trim() == "") && (lblCCity.Text.Trim() == "") && (lblCZip.Text.Trim() == "") && (lblPhoneNo.Text.Trim() == "") && (lblStudMobileNo.Text.Trim() == "") && (lblSEmail.Text.Trim() == ""))
                    {
                        divSComm.Style.Add("display", "block");
                        divEComm.Style.Add("display", "none");
                    }

                    else
                    {
                        txtPPbox.Text = ds.Tables[1].Rows[0]["PerPostBoxNo"].ToString();
                        txtPStreet.Text = ds.Tables[1].Rows[0]["PerStreet"].ToString();
                        ddlPCon.SelectedValue = ds.Tables[1].Rows[0]["PerCountry"].ToString();
                        BindPCoun();
                        ddlPState.SelectedValue = ds.Tables[1].Rows[0]["PerState"].ToString();
                        BindPState();
                        ddlPCity.SelectedValue = ds.Tables[1].Rows[0]["PerCity"].ToString();
                        txtPZip.Text = ds.Tables[1].Rows[0]["PerZipCode"].ToString();

                        txtCpbox.Text = ds.Tables[1].Rows[0]["ComPostBoxNo"].ToString();
                        txtCStreet.Text = ds.Tables[1].Rows[0]["ComStreet"].ToString();
                        ddlCCoun.SelectedValue = ds.Tables[1].Rows[0]["ComCountry"].ToString();
                        BindCCoun();
                        ddlCState.SelectedValue = ds.Tables[1].Rows[0]["ComState"].ToString();
                        BindState();
                        ddlCCity.SelectedValue = ds.Tables[1].Rows[0]["ComCity"].ToString();
                        txtCZip.Text = ds.Tables[1].Rows[0]["ComZipCode"].ToString();

                        txtMobileno.Text = (ds.Tables[1].Rows[0]["MobileNo"].ToString() == "0") ? "" : ds.Tables[1].Rows[0]["MobileNo"].ToString();
                        txtEmail.Text = ds.Tables[1].Rows[0]["Email"].ToString();
                        txtPhoneno.Text = (ds.Tables[1].Rows[0]["PhoneNo"].ToString() == "0") ? "" : ds.Tables[1].Rows[0]["PhoneNo"].ToString();

                        txtCorporateEMail.Text = ds.Tables[1].Rows[0]["CorporateEmail"].ToString();
                        rblPrimaryEmail.SelectedValue = ds.Tables[1].Rows[0]["PrimaryEmail"].ToString();
                        rblPrimaryMobileNo.SelectedValue = ds.Tables[1].Rows[0]["PrimaryMobileNo"].ToString();

                        divEComm.Style.Add("display", "block");
                        divSComm.Style.Add("display", "none");
                        //spimage1.cs ="fas fa-check-circle";
                        lblImg1.CssClass = "fas fa-check-circle StyleIcon Gray";

                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            else
            {
                divSComm.Style.Add("display", "block");
                divEComm.Style.Add("display", "none");
                lblImg1.CssClass = "fas fa-check-circle fa-times StyleIcon Gray";
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                try
                {
                    lblReligion.Text = ds.Tables[2].Rows[0]["ReligionName"].ToString();
                    lblBloodGroup.Text = ds.Tables[2].Rows[0]["BloodGroupName"].ToString();
                    lblQualification.Text = ds.Tables[2].Rows[0]["Qualification"].ToString();
                    lblNational.Text = (ds.Tables[2].Rows[0]["Nationality"].ToString() == "") ? "" : ds.Tables[2].Rows[0]["Nationality"].ToString();
                    lblTotYear.Text = ds.Tables[2].Rows[0]["TotExpyear"].ToString();
                    lblTotMonth.Text = ds.Tables[2].Rows[0]["TotExpmonth"].ToString();
                    lblRelYear.Text = ds.Tables[2].Rows[0]["RelExpyear"].ToString();
                    lblRelMonth.Text = ds.Tables[2].Rows[0]["RelExpmonth"].ToString();
                    //  lblAccessCardNo.Text=

                    if ((lblReligion.Text.Trim() == "") && (lblBloodGroup.Text.Trim() == "") && (lblQualification.Text.Trim() == "") && (lblNational.Text.Trim() == "") && (lblTotYear.Text.Trim() == "") && (lblTotMonth.Text.Trim() == "") && (lblRelYear.Text.Trim() == "") && (lblRelMonth.Text.Trim() == "") && (lblRoles.Text.Trim() == "") && (lblDepartment.Text.Trim() == "") && (lblDesignation.Text.Trim() == "") && (lblAccessCardNo.Text.Trim() == "") && (lblEmployeeCode.Text.Trim() == "") && (lblReportingTo.Text.Trim() == "") && (lblValidityTill.Text.Trim() == "") && (lblRefID.Text.Trim() == ""))
                    {
                        divSRoles.Style.Add("display", "block");
                        divERoles.Style.Add("display", "none");
                    }
                    else
                    {
                        if ((ds.Tables[2].Rows[0]["Nationality"].ToString() == "0") || (ds.Tables[2].Rows[0]["Nationality"].ToString() == ""))
                            ddlNationality.SelectedValue = "0";
                        else
                            ddlNationality.SelectedValue = ddlNationality.Items.FindByText(ds.Tables[2].Rows[0]["Nationality"].ToString()).Value;
                        txtQualification.Text = ds.Tables[2].Rows[0]["Qualification"].ToString();
                        ddlTotExperienceYear.SelectedValue = ds.Tables[2].Rows[0]["TotExpyear"].ToString();
                        ddlTotExperienceMonth.SelectedValue = ds.Tables[2].Rows[0]["TotExpmonth"].ToString();
                        ddlRelExperienceYear.SelectedValue = ds.Tables[2].Rows[0]["RelExpyear"].ToString();
                        ddlRelExperienceMonth.SelectedValue = ds.Tables[2].Rows[0]["RelExpmonth"].ToString();
                        ddlReligion.SelectedValue = ds.Tables[2].Rows[0]["Religion"].ToString();
                        ddlBloodGroup.SelectedValue = ds.Tables[2].Rows[0]["BloodGroup"].ToString();

                        divSRoles.Style.Add("display", "none");
                        divERoles.Style.Add("display", "block");
                        lblImg2.CssClass = "fas fa-check-circle StyleIcon Gray";
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            else
            {
                divSRoles.Style.Add("display", "block");
                divERoles.Style.Add("display", "none");
                lblImg2.CssClass = "fas fa-check-circle fa-times StyleIcon Gray";
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                try
                {
                    lblPanNumber.Text = ds.Tables[3].Rows[0]["PAN_Number"].ToString();
                    lblPFNumber.Text = ds.Tables[3].Rows[0]["PF_Number"].ToString();
                    lblBankName.Text = ds.Tables[3].Rows[0]["BankName"].ToString();
                    lblAccountNo.Text = ds.Tables[3].Rows[0]["BankAccountNo"].ToString();
                    lblBankIFSCCode.Text = ds.Tables[3].Rows[0]["BankIFSCcode"].ToString();
                    lblEmployeeESINo.Text = ds.Tables[3].Rows[0]["EmployeeESI"].ToString();
                    lblAadharNo.Text = ds.Tables[3].Rows[0]["AadharNo"].ToString();
                    lblSalaryPaymentMode.Text = ds.Tables[3].Rows[0]["SalaryPaymentModeName"].ToString();

                    if ((lblPanNumber.Text.Trim() == "") && (lblPFNumber.Text.Trim() == "") && (lblBankName.Text.Trim() == "") && (lblAccountNo.Text.Trim() == "") && (lblBankIFSCCode.Text.Trim() == "") && (lblAadharNo.Text.Trim() == "") && (lblSalaryPaymentMode.Text == ""))
                    {
                        divSBankDetails.Style.Add("display", "block");
                        divEBankDetails.Style.Add("display", "none");
                    }
                    else
                    {
                        txtPanNumber.Text = ds.Tables[3].Rows[0]["PAN_Number"].ToString();
                        txtPFNumber.Text = ds.Tables[3].Rows[0]["PF_Number"].ToString();
                        txtBankName.Text = ds.Tables[3].Rows[0]["BankName"].ToString();
                        txtAccountNo.Text = ds.Tables[3].Rows[0]["BankAccountNo"].ToString();
                        txtBankIFSCCode.Text = ds.Tables[3].Rows[0]["BankIFSCcode"].ToString();
                        txtEmployeeESINo.Text = ds.Tables[3].Rows[0]["EmployeeESI"].ToString();
                        txtAadharNo.Text = ds.Tables[3].Rows[0]["AadharNo"].ToString();
                        ddlSalarypaymentMode.SelectedValue = ds.Tables[3].Rows[0]["SalaryPaymentMode"].ToString();
                        divSBankDetails.Style.Add("display", "none");
                        divEBankDetails.Style.Add("display", "block");
                        lblImg3.CssClass = "fas fa-check-circle StyleIcon Gray";
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            else
            {
                divSBankDetails.Style.Add("display", "block");
                divEBankDetails.Style.Add("display", "none");
                lblImg3.CssClass = "fas fa-check-circle fa-times StyleIcon Gray";
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                try
                {
                    lblEAppointmentLetter.Text = ds.Tables[4].Rows[0]["Appointment_letter"].ToString();
                    lblEBankAccountCopy.Text = ds.Tables[4].Rows[0]["Bank_Acc"].ToString();
                    lblEResume.Text = ds.Tables[4].Rows[0]["Empresume"].ToString();
                    lblEPANCard.Text = ds.Tables[4].Rows[0]["PanCard"].ToString();
                    lblEResignationLetter.Text = ds.Tables[4].Rows[0]["ResignLetter"].ToString();
                    lblEEducationPapers.Text = ds.Tables[4].Rows[0]["EduPapers"].ToString();
                    lblEMedicalReport.Text = ds.Tables[4].Rows[0]["Medicalreport"].ToString();
                    lblEPFDeclarationForm.Text = ds.Tables[4].Rows[0]["PFForm"].ToString();
                    lblEAadhar.Text = ds.Tables[4].Rows[0]["Aadhar"].ToString();
                    lblOther1Name.Text = ds.Tables[4].Rows[0]["Other_1_Name"].ToString();
                    lblOther2Name.Text = ds.Tables[4].Rows[0]["Other_2_Name"].ToString();
                    lblOther3Name.Text = ds.Tables[4].Rows[0]["Other_3_Name"].ToString();
                    lblOther4Name.Text = ds.Tables[4].Rows[0]["Other_4_Name"].ToString();
                    lblOther5Name.Text = ds.Tables[4].Rows[0]["Other_5_Name"].ToString();
                    lblEOther1.Text = ds.Tables[4].Rows[0]["Other_1_DOC"].ToString();
                    lblEOther2.Text = ds.Tables[4].Rows[0]["Other_2_DOC"].ToString();
                    lblEOther3.Text = ds.Tables[4].Rows[0]["Other_3_DOC"].ToString();
                    lblEOther4.Text = ds.Tables[4].Rows[0]["Other_4_DOC"].ToString();
                    lblEOther5.Text = ds.Tables[4].Rows[0]["Other_5_DOC"].ToString();

                    if ((lblEAppointmentLetter.Text.Trim() == "") && (lblEBankAccountCopy.Text.Trim() == "") && (lblEResume.Text.Trim() == "") && (lblEResignationLetter.Text.Trim() == "") && (lblEPANCard.Text.Trim() == "") && (lblEEducationPapers.Text.Trim() == "") && (lblEMedicalReport.Text.Trim() == "") && (lblEPFDeclarationForm.Text.Trim() == "") && (lblEAadhar.Text.Trim() == "") && (lblEOther1.Text.Trim() == "") && (lblEOther2.Text.Trim() == "") && (lblEOther3.Text.Trim() == "") && (lblEOther4.Text.Trim() == "") && (lblEOther5.Text.Trim() == ""))
                    {
                        divSDocuments.Style.Add("display", "block");
                        divEDocuments.Style.Add("display", "none");
                        hdnDocsOpen.Value = "1";
                    }
                    else
                    {
                        divEDocuments.Style.Add("display", "block");
                        divSDocuments.Style.Add("display", "none");
                        hdnDocsOpen.Value = "0";
                    }

                    if (lblEAppointmentLetter.Text.Trim() != "")
                        btnEAppointmentLetter.Style.Add("display", "inline-block");
                    if (lblEBankAccountCopy.Text.Trim() != "")
                        btnEBankAccountCopy.Style.Add("display", "inline-block");
                    if (lblEResume.Text.Trim() != "")
                        btnEResume.Style.Add("display", "inline-block");
                    if (lblEResignationLetter.Text.Trim() != "")
                        btnEResignationLetter.Style.Add("display", "inline-block");
                    if (lblEAadhar.Text.Trim() != "")
                        btnEAadhar.Style.Add("display", "inline-block");
                    if (lblEPANCard.Text.Trim() != "")
                        btnEPANCard.Style.Add("display", "inline-block");
                    if (lblEMedicalReport.Text.Trim() != "")
                        btnEMedicalReport.Style.Add("display", "inline-block");
                    if (lblEEducationPapers.Text.Trim() != "")
                        btnEEducationPapers.Style.Add("display", "inline-block");
                    if (lblEPFDeclarationForm.Text.Trim() != "")
                        btnEPFDeclarationForm.Style.Add("display", "inline-block");
                    if (lblEOther1.Text.Trim() != "")
                        divEOthers1.Style.Add("display", "inline-block");
                    if (lblEOther2.Text.Trim() != "")
                        divEOthers2.Style.Add("display", "inline-block");
                    if (lblEOther3.Text.Trim() != "")
                        divEOthers3.Style.Add("display", "inline-block");
                    if (lblEOther4.Text.Trim() != "")
                        divEOthers4.Style.Add("display", "inline-block");
                    if (lblEOther5.Text.Trim() != "")
                        divEOthers5.Style.Add("display", "inline-block");


                    lblfAppointmentLetter.Text = ds.Tables[4].Rows[0]["Appointment_letter"].ToString();
                    lblfBankAcc.Text = ds.Tables[4].Rows[0]["Bank_Acc"].ToString();
                    lblfResume.Text = ds.Tables[4].Rows[0]["Empresume"].ToString();
                    lblfPanCard.Text = ds.Tables[4].Rows[0]["PanCard"].ToString();
                    lblfResignLetter.Text = ds.Tables[4].Rows[0]["ResignLetter"].ToString();
                    lblfEduPapers.Text = ds.Tables[4].Rows[0]["EduPapers"].ToString();
                    lblfMedical.Text = ds.Tables[4].Rows[0]["Medicalreport"].ToString();
                    lblfPF.Text = ds.Tables[4].Rows[0]["PFForm"].ToString();
                    lblfAadhar.Text = ds.Tables[4].Rows[0]["Aadhar"].ToString();
                    txtOther1Name.Text = ds.Tables[4].Rows[0]["Other_1_Name"].ToString();
                    txtOther2Name.Text = ds.Tables[4].Rows[0]["Other_2_Name"].ToString();
                    txtOther3Name.Text = ds.Tables[4].Rows[0]["Other_3_Name"].ToString();
                    txtOther4Name.Text = ds.Tables[4].Rows[0]["Other_4_Name"].ToString();
                    txtOther5Name.Text = ds.Tables[4].Rows[0]["Other_5_Name"].ToString();
                    lblOther1.Text = ds.Tables[4].Rows[0]["Other_1_DOC"].ToString();
                    lblOther2.Text = ds.Tables[4].Rows[0]["Other_2_DOC"].ToString();
                    lblOther3.Text = ds.Tables[4].Rows[0]["Other_3_DOC"].ToString();
                    lblOther4.Text = ds.Tables[4].Rows[0]["Other_4_DOC"].ToString();
                    lblOther5.Text = ds.Tables[4].Rows[0]["Other_5_DOC"].ToString();

                    if (lblfAppointmentLetter.Text.Trim() != "")
                        divAppointmentLetter.Style.Add("display", "inline-block");
                    if (lblfBankAcc.Text.Trim() != "")
                        divBankAcc.Style.Add("display", "inline-block");
                    if (lblfResume.Text.Trim() != "")
                        divResume.Style.Add("display", "inline-block");
                    if (lblfResignLetter.Text.Trim() != "")
                        divResignLetter.Style.Add("display", "inline-block");
                    if (lblfAadhar.Text.Trim() != "")
                        divAadhar.Style.Add("display", "inline-block");
                    if (lblfPanCard.Text.Trim() != "")
                        divPanCard.Style.Add("display", "inline-block");
                    if (lblfMedical.Text.Trim() != "")
                        divMedical.Style.Add("display", "inline-block");
                    if (lblfEduPapers.Text.Trim() != "")
                        divEduPapers.Style.Add("display", "inline-block");
                    if (lblfPF.Text.Trim() != "")
                        divPF.Style.Add("display", "inline-block");
                    if (lblOther1.Text.Trim() != "")
                        divOther1.Style.Add("display", "inline-block");
                    if (lblOther2.Text.Trim() != "")
                        divOther2.Style.Add("display", "inline-block");
                    if (lblOther3.Text.Trim() != "")
                        divOther3.Style.Add("display", "inline-block");
                    if (lblOther4.Text.Trim() != "")
                        divOther4.Style.Add("display", "inline-block");
                    if (lblOther5.Text.Trim() != "")
                        divOther5.Style.Add("display", "inline-block");

                    lblImg6.CssClass = "fas fa-check-circle StyleIcon Gray";
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            else
            {
                divSDocuments.Style.Add("display", "block");
                divEDocuments.Style.Add("display", "none");
                lblImg6.CssClass = "fas fa-check-circle fa-times StyleIcon Gray";
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                //gvExperience.DataSource = ds.Tables[6];
                //gvExperience.DataBind();
                try
                {
                    ViewState["Experience"] = ds.Tables[5];
                    BindExpGrid();
                    gvShowExperience.DataSource = ds.Tables[5];
                    gvShowExperience.DataBind();
                    foreach (GridViewRow gvRow in gvExperience.Rows)
                    {
                        TextBox txtExpOrg = (TextBox)gvRow.FindControl("txtExpOrg");
                        TextBox txtExpDesig = (TextBox)gvRow.FindControl("txtExpDesig");
                        TextBox txtExpFrom = (TextBox)gvRow.FindControl("txtExpFrom");
                        TextBox txtExpTo = (TextBox)gvRow.FindControl("txtExpTo");
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            if (gvRow.RowIndex == 0)
                            {
                                if ((txtExpOrg.Text == "") && (txtExpDesig.Text == "") && (txtExpFrom.Text == "0") && (txtExpTo.Text == "0"))
                                {
                                    divSExperience.Style.Add("display", "block");
                                    divEExperience.Style.Add("display", "none");
                                }
                                else
                                {
                                    divSExperience.Style.Add("display", "none");
                                    divEExperience.Style.Add("display", "block");
                                }
                            }
                            else
                            {
                                divSExperience.Style.Add("display", "none");
                                divEExperience.Style.Add("display", "block");
                            }
                        }
                    }

                    if ((ddlTotExperienceMonth.SelectedValue != "0") || (ddlTotExperienceYear.SelectedValue != "0"))
                    {
                        if (gvShowExperience.Rows.Count == 0)
                        {
                            //image5.CssClass.Remove(0);
                            // image5.CssClass = "StyleIcon Red";
                            //  image5.ToolTip = "Fill Work Experience";
                            hdnExperienceStatus.Value = "0";
                        }
                        else
                        {
                            //  image5.CssClass.Remove(0);
                            //  image5.CssClass = "StyleIcon Green";
                            hdnExperienceStatus.Value = "1";
                        }
                    }
                    else if ((ddlTotExperienceMonth.SelectedValue == "0") && (ddlTotExperienceYear.SelectedValue == "0"))
                    {
                        // image5.CssClass.Remove(0);
                        //image5.CssClass = "StyleIcon Gray";
                        // image5.ToolTip = "";
                        hdnExperienceStatus.Value = "1";
                    }
                    else
                    {
                        //image5.CssClass.Remove(0);
                        // image5.CssClass = "StyleIcon Green";
                        hdnExperienceStatus.Value = "1";
                    }

                    divSExperience.Style.Add("display", "none");
                    divEExperience.Style.Add("display", "block");
                    lblImg5.CssClass = "fas fa-check-circle StyleIcon Gray";
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            else
            {
                gvShowExperience.DataSource = "";
                gvShowExperience.DataBind();
                divSExperience.Style.Add("display", "block");
                divEExperience.Style.Add("display", "none");
                hdnExperienceStatus.Value = "1";
                lblImg5.CssClass = "fas fa-check-circle fa-times StyleIcon Gray";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "GvExperienceClear();", true);

            }

            if (ds.Tables[6].Rows.Count > 0)
            {
                try
                {
                    ViewState["AcadRecCount"] = Convert.ToString(ds.Tables[6].Rows.Count + 1);
                    gvShowAcaDetails.DataSource = ds.Tables[6];
                    gvShowAcaDetails.DataBind();
                    int rowIndex = 0, value = 0;
                    for (int i = 0; i < gvShowAcaDetails.Rows.Count; i++)
                    {
                        Label lblAttachments = (Label)gvShowAcaDetails.Rows[i].FindControl("lblAttachments");
                        if (lblAttachments.Text.Trim() == "")
                            gvShowAcaDetails.Rows[i].FindControl("btnView").Visible = false;
                        else
                            gvShowAcaDetails.Rows[i].FindControl("btnView").Visible = true;
                    }
                    for (int i = 0; i < gvShowAcaDetails.Rows.Count; i++)
                    {
                        Label lblCertification = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblCertification");
                        Label lblSubject = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblSubject");
                        Label lblMarks = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblMarks");
                        Label lblPercentage = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblPercentage");
                        Label lblLocation = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblLocation");
                        Label lblYOC = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblYOC");
                        Label lblAttachments = (Label)gvShowAcaDetails.Rows[rowIndex].FindControl("lblAttachments");

                        if (lblAttachments.Text.Trim() != "")
                            gvShowAcaDetails.Rows[rowIndex].FindControl("btnView").Visible = true;
                        else
                            gvShowAcaDetails.Rows[rowIndex].FindControl("btnView").Visible = false;

                        if (rowIndex == 0 || rowIndex == 1)
                        {
                            if ((lblCertification.Text == "") || (lblSubject.Text == "") || (lblMarks.Text == "") || (lblPercentage.Text == "") || (lblLocation.Text == "") || (lblYOC.Text == ""))// || (lblAttachments.Text == "")
                                value++;
                        }
                        rowIndex++;
                    }
                    if (value != 0)
                    {
                        //image4.CssClass.Remove(0);
                        //image4.CssClass = "StyleIcon Red";
                        //image4.ToolTip = "Add Education Profile";
                        hdnEducationProfileStatus.Value = "0";
                    }
                    else
                    {
                        // image4.CssClass.Remove(0);
                        // image4.CssClass = "StyleIcon Green";
                        hdnEducationProfileStatus.Value = "1";
                    }

                    lblImg4.CssClass = "fas fa-check-circle StyleIcon Gray";
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            else
            {
                ViewState["AcadRecCount"] = "1";
                gvShowAcaDetails.DataSource = "";
                gvShowAcaDetails.DataBind();
                // image4.CssClass.Remove(0);
                // image4.CssClass = "StyleIcon Red";
                // image4.ToolTip = "Add Education Profile";
                hdnEducationProfileStatus.Value = "0";
                lblImg4.CssClass = "fas fa-check-circle fa-times StyleIcon Gray";
                // divSEducationProfile.Style.Add("display", "block");
                // divEEducationProfile.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Web Method"

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetEmployee(string prefixText)
    {
        cDataAccess DAL = new cDataAccess();

        DataSet ds = new DataSet();

        List<string> EmployeeName = new List<string>();

        DAL = new cDataAccess();

        string[] paramNames = { "@RegNo" };
        object[] paramValue = { prefixText };

        DAL.GetDataset("LS_GetAllStaffByAutoSearch", paramValue, paramNames, ref ds);

        DataTable dt = new DataTable();

        dt = ds.Tables[0];

        if (dt.Rows.Count == 0)
        {
            EmployeeName.Add("Record not available");
        }
        else
        {
            for (int i = 0; i < dt.Rows.Count; i++)
                EmployeeName.Add(dt.Rows[i]["Name"].ToString() + " #" + dt.Rows[i]["ID"].ToString());
        }

        return EmployeeName;
    }

    #endregion
}