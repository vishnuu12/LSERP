using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using eplus.core;

public partial class Pages_Design : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cDesign objDesign = new cDesign();
    EmailAndSmsAlerts objAlerts;
    c_HR objHR;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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
            if (IsPostBack == false)
            {
                DataSet ds = new DataSet();
                ds = objDesign.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
                ViewState["EnquiryDetails"] = ds.Tables[0];
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

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                lblEnquiryNumber_A.Text = ddlEnquiryNumber.SelectedItem.Text;
                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                ds = objDesign.GetDesignDetailsByEnquiryID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    divOutput.Visible = true;
                    lblClientName.Text = ds.Tables[0].Rows[0]["ClientName"].ToString();
                    lblProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    lblSalesAgent.Text = ds.Tables[0].Rows[0]["SalesAgent"].ToString();
                    ViewState["EmployeeID"] = ds.Tables[0].Rows[0]["Sales"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        lblVersionNumber_A.Text = (Convert.ToInt32(ds.Tables[1].Rows[0]["Version"].ToString()) + 1).ToString();
                        lblLastDrawingSubmittedOn.Text = ds.Tables[1].Rows[0]["CreatedOn"].ToString();
                        ViewState["NewDrawingVersionNumber"] = (Convert.ToInt32(ds.Tables[1].Rows[0]["Version"].ToString()) + 1).ToString();
                    }
                    else
                    {
                        lblVersionNumber_A.Text = "1";
                        ViewState["NewDrawingVersionNumber"] = "1";
                    }

                    gvDesignDetails.DataSource = ds.Tables[1];
                    gvDesignDetails.DataBind();


                    dt = (DataTable)ViewState["EnquiryDetails"];

                    DataView dv = new DataView(dt);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dt = dv.ToTable();

                    if (dt.Rows[0]["ERPUserType"].ToString() == "1")
                        lbtnNewDrawing.Visible = true;

                    else if (dt.Rows[0]["ERPUserType"].ToString() == "3")
                        lbtnNewDrawing.Visible = false;

                    else if (dt.Rows[0]["ERPUserType"].ToString() == "2" && dt.Rows[0]["own"].ToString() == "0")
                        lbtnNewDrawing.Visible = true;
                    else
                        lbtnNewDrawing.Visible = false;

                }
                else
                {
                    divOutput.Visible = false;
                }
            }
            else
                divOutput.Visible = false;

            if (sender != "load")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveDrawing_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet dsMessage = new DataSet();
        DataSet dsEmail = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        objHR = new c_HR();
        string FileName = "";
        string[] extension;
        string AttachmentName;
        string path;
        string Message;
        try
        {
            if (ValidateFields())
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                FileName = Path.GetFileName(fAttachment.PostedFile.FileName);

                extension = FileName.Split('.');
                AttachmentName = "LSEDRA" + '_' + ddlEnquiryNumber.SelectedValue + '_' + ViewState["NewDrawingVersionNumber"] + '.' + extension[1];
                objDesign.AttachementName = AttachmentName;

                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                objDesign.VersionNumber = Convert.ToInt32(ViewState["NewDrawingVersionNumber"].ToString());
                objDesign.Remarks = txtRemarks_A.Text;
                ds = objDesign.SaveDesignDocumentDetails();

                if (ds.Tables[0].Rows[0]["AttachementID"].ToString() != "")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Design Details Saved Successfully');", true);

                path = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fAttachment.SaveAs(path + AttachmentName);

                //Send Individual Mail

                dsEmail = objHR.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(ViewState["EmployeeID"].ToString()));

                if (dsEmail.Tables[0].Rows.Count > 0)
                {
                    if (dsEmail.Tables[0].Rows[0]["Email"].ToString() != "")
                    {
                        objAlerts.MessageID = 1;

                        string BaseHttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";

                        dsMessage = objAlerts.GetAutomatedMessageMail();
                        Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
                        Message = Message.Replace("<EmployeeName>", lblSalesAgent.Text);
                        Message = Message.Replace("<Version>", ViewState["NewDrawingVersionNumber"].ToString());
                        Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue);
                        Message = Message.Replace("<OrderNumber>", lblCustomerOrderNumber.Text);

                        objAlerts.AttachementID = Convert.ToInt32(ds.Tables[0].Rows[0]["AttachementID"].ToString());

                        objAlerts.file = BaseHttpPath + AttachmentName;

                        objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                        objAlerts.AlertType = "Mail";

                        objAlerts.reciverID = dsEmail.Tables[0].Rows[0]["EmployeeID"].ToString();
                        objAlerts.reciverType = "I";
                        objAlerts.GroupID = 0;
                        objAlerts.dtSettings = objAlerts.GetEmailSettings();
                        objAlerts.EmailID = dsEmail.Tables[0].Rows[0]["Email"].ToString(); //dsEmail.Tables[0].Rows[0]["Email"].ToString();//"karthik@innovasphere.in";
                        objAlerts.Subject = "Drawing Attachements";
                        objAlerts.Message = Message;
                        objAlerts.userID = objSession.employeeid;

                        //send Mail
                        objAlerts.SendIndividualMail();

                        //save Alert Details
                        objAlerts.Message = null;
                        objAlerts.SaveAlertDetails();
                    }
                }

                txtRemarks_A.Text = "";

                ddlEnquiryNumber_SelectIndexChanged("load", null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string extension = "";
            // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
             string BasehttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");

                byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                string base64String = Convert.ToBase64String(imageBytes);
                imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                imgbtn.ToolTip = dr["FileName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDesignDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

           //  string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

            string BasehttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";

            string FileName = gvDesignDetails.DataKeys[index].Values[0].ToString();

            byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + FileName);
            string base64String = Convert.ToBase64String(imageBytes);
            imgDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);

            // divContent.InnerHtml = "<iframe src='http://docs.google.com/gview?url=" + BasehttpPath + FileName + "&embedded=true' style='width:100%; height:100%;' frameborder='0' id='frame1'></iframe>";

            // cCommon.DownLoad(FileName, BasehttpPath + FileName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region "Common Methods"

    private bool ValidateFields()
    {
        string error = "";
        bool isvalid = true;
        string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
        try
        {
            if (fAttachment.HasFile == false)
                error = "Please Upload File";

            else if (extn != ".JPG" && extn != ".JPEG" && extn != ".PNG")
                error = "Please upload a File with extension: .jpg , .jpeg , .png";

            else if (txtRemarks_A.Text == "")
                error = "Please Enter Remarks";

            if (error != "")
            {
                isvalid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Field Required " + error + "');ShowAddPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isvalid;
    }

    #endregion
}