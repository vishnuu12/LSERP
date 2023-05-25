using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Pages_ReopenApprovalDesign : System.Web.UI.Page
{

    #region"Declaration"

    cDesign objDesign = new cDesign();
    cSession objSession = new cSession();
    EmailAndSmsAlerts objAlerts;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Event"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                ProcessIntial();
                AssignModuleName();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objDesign.DDID = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());
                ds = objDesign.GetApprovedDesignDetailsByDDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "NotSaved")
                {
                    lblCustomerName.Text = ds.Tables[1].Rows[0]["ProspectName"].ToString();
                    lblEnquiryNumber.Text = ds.Tables[1].Rows[0]["EnquiryNumber"].ToString();
                    lblVersionNumber.Text = ds.Tables[1].Rows[0]["Version"].ToString();
                    lblApprovedOn.Text = ds.Tables[1].Rows[0]["CustomerApprovalOn"].ToString();
                    lblCurrentStatus.Text = ds.Tables[1].Rows[0]["Status"].ToString();

                    ViewState["EmpCommunication"] = ds.Tables[2];

                    ShowHideControls("save");
                }
                else
                {
                    ShowHideControls("Initial");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "InfoMessage('Information','Already Saved');hideLoader();", true);
                }
            }
            else
                ShowHideControls("Initial");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

    protected void rblCommercialImpact_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblCommercialImpact.SelectedValue == "1")
                divSaveStateAmount.Visible = true;
            else
                divSaveStateAmount.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void rblDelayIsImplementation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblDelayIsImplementation.SelectedValue == "1")
                divSaveResheduledDate.Visible = true;
            else
                divSaveResheduledDate.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["EmpCommunication"];
        objDesign = new cDesign();
        objAlerts = new EmailAndSmsAlerts();
        string Message;
        DataSet dsMessage = new DataSet();
        try
        {
            LinkButton clickedButton = (LinkButton)sender;

            if (clickedButton.Text == "Approval")
                objAlerts.MessageID = 17;

            else
                objAlerts.MessageID = 18;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dsMessage = objAlerts.GetAutomatedMessageMail();
                    Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
                    Message = Message.Replace("<EmployeeName>", dr["EmployeeName"].ToString());
                    Message = Message.Replace("<VersionNumber>", lblVersionNumber.Text);
                    Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue.Split('/')[1].ToString());
                    Message = Message.Replace("<ChangeRequestbyClient>", txtChangesRequested.Text);
                    objAlerts.file = "";
                    objAlerts.dtSettings = objAlerts.GetEmailSettings();
                    objAlerts.EmailID = dr["Email"].ToString();// "karthik@innovasphere.in";

                    if (clickedButton.Text == "Approval")
                        objAlerts.Subject = "Design change request approved";
                    else
                        objAlerts.Subject = "Design change request rejected";

                    objAlerts.Message = Message;
                    objAlerts.userID = objSession.employeeid;
                    objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[1].ToString());
                    objAlerts.AlertType = "Email";
                    objAlerts.reciverID = dr["EmployeeID"].ToString();
                    objAlerts.reciverType = "I";
                    objAlerts.GroupID = 0;

                    //Send Individual mail
                    objAlerts.SendIndividualMail();

                    //Save Alert Details
                    objAlerts.Message = null;
                    objAlerts.SaveAlertDetails();

                    if (clickedButton.Text == "Approval")
                        txtRejectReason.Text = null;

                    objDesign.DDID = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());

                    if (clickedButton.Text == "Approval")
                        objDesign.UpdateCustomerApprovedDrawingVersionStatus();
                }

                //Save Reopen Drawing Details
                SaveReopenApprovedDrawingDetails();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Method"

    private void SaveReopenApprovedDrawingDetails()
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.DDID = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());
            objDesign.ChangesRequested = txtChangesRequested.Text;
            objDesign.CommercialImpact = Convert.ToInt32(rblCommercialImpact.SelectedValue);

            objDesign.StateAmount = rblCommercialImpact.SelectedValue == "0" ? 0 : Convert.ToInt32(txtStateAmount.Text);
            objDesign.DelayInImplementation = rblDelayIsImplementation.SelectedValue;

            if (rblDelayIsImplementation.SelectedValue == "1")
                objDesign.ReScheduledDate = DateTime.ParseExact(txtReScheduledDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            objDesign.Datetime = txtReScheduledDate.Text;

            objDesign.RejectReason = txtRejectReason.Text;

            ds = objDesign.SaveReopenApprovedDrawingDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "SuccessMessage('Success','Aprroval Details Saved Successfully');hideLoader();", true);

            ProcessIntial();
            ClearFields();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ProcessIntial()
    {
        try
        {
            objDesign.GetCustomerApprovedDrawings(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
            ShowHideControls("Initial");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string Mode)
    {
        divCustomerInformation.Visible = divSaveApproval.Visible = false;
        switch (Mode.ToLower())
        {
            case "save":
                divSaveApproval.Visible = true;
                divCustomerInformation.Visible = true;
                break;

            case "initial":
                divCustomerInformation.Visible = false;
                divSaveApproval.Visible = false;
                break;
        }
    }

    private void ClearFields()
    {
        txtChangesRequested.Text = "";
        rblCommercialImpact.SelectedValue = "0";
        rblDelayIsImplementation.SelectedValue = "0";
        txtStateAmount.Text = "";
        txtRejectReason.Text = "";
        txtReScheduledDate.Text = "";
        divSaveStateAmount.Visible = false;
        divSaveResheduledDate.Visible = false;
        divRejectReason.Visible = false;
    }

    private void AssignModuleName()
    {
        DataTable dt;
        try
        {
            dt = (DataTable)Session["UserModules"];
            DataView dv = new DataView(dt);
            dv.RowFilter = "MID='3'";
            dt = dv.ToTable();
            if (dt.Rows.Count > 0)
                lblModuleName.Text = dt.Rows[0]["MID"].ToString() == "3" ? "Leads & Enquiry" : "Design";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}