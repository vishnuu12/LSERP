using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using eplus.core;
using System.Configuration;

public partial class Pages_DrawingHODApproval : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cDesign objDesign = new cDesign();
    EmailAndSmsAlerts objAlerts;
    c_Finance objFinance;
    c_HR objHR;
    cSales objSales;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    cCommon objc;
    cMaterials objMat;

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
                ddlEnquiryLoad();
                //objDesign.GetEnquiryNumberBySalesUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
                //BindDesignDocumentDetailsByUserID();
            }
            if (target == "Approve")
                UpdateApprovalStatus();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"radio Events"

    protected void rblEnquiryChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlEnquiryLoad();
        divOutput.Visible = false;
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataView dv;
        DataTable dcustomr = new DataTable();
        DataTable denquiry = new DataTable();
        try
        {
            // objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            if (ddlCustomerName.SelectedIndex > 0)
            {
                dv = new DataView(denquiry);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }

            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                //objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                divOutput.Visible = true;

                //  ds = objDesign.GetDesignDetailsByEDIDForApproval(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));
                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;
                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];
                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }

                ds = objDesign.GetUnSharedWithSalesDesignDetailsByEnquiryID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["Designer"] = ds.Tables[0].Rows[0]["Designer"].ToString();
                    ViewState["SalesPerson"] = ds.Tables[0].Rows[0]["SalesPerson"].ToString();
                    gvDesignApprovalDetails.DataSource = ds.Tables[0];
                    gvDesignApprovalDetails.DataBind();
                }
                else
                {
                    gvDesignApprovalDetails.DataSource = "";
                    gvDesignApprovalDetails.DataBind();
                }
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {

    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {

    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        bool itemselected = false;
        try
        {
            foreach (GridViewRow row in gvDesignApprovalDetails.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    itemselected = true;
                    DataSet dsApprove = new DataSet();
                    objDesign = new cDesign();
                    objDesign.DDID = Convert.ToInt32(Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[2].ToString()));

                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    objDesign.Remarks = txtRemarks.Text;

                    objDesign.RejectDrawingStatus();
                }
            }

            ddlEnquiryNumber_SelectIndexChanged(null, null);

            if (itemselected == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Drawing Rejected Successfully')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"gridView Events"

    protected void gvDesignApprovalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string BasehttpPath;
        string BaseSavepPath;
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("Class", "nosort");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtremarks = (TextBox)e.Row.FindControl("txtRemarks");
                CheckBox chkitems = (CheckBox)e.Row.FindControl("chkitems");

                BaseSavepPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
                BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                if (File.Exists(BaseSavepPath + dr["FileName"].ToString())) imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                imgbtn.ToolTip = dr["FileName"].ToString();

                if (dr["SharedWithHOD"].ToString() == "9" || dr["SharedWithHOD"].ToString() == "0")
                {
                    chkitems.Visible = false;
                    txtremarks.Text = dr["Remarks"].ToString();
                    //btnApprove.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    //btnReject.CssClass = "btn btn-cons btn-save aspNetDisabled";
                }
                if (dr["SharedWithSales"].ToString() == "1")
                {
                    //btnApprove.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    //btnReject.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    chkitems.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDesignApprovalDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objDesign = new cDesign();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            //  Label lblEnquiryNumber = (Label)gvDesignApprovalDetails.Rows[index].FindControl("lblEnquiryNumber");
            Label lblversionNumber = (Label)gvDesignApprovalDetails.Rows[index].FindControl("lblVersionNumber");

            string DDID = gvDesignApprovalDetails.DataKeys[index].Values[2].ToString();
            string AttachementID = gvDesignApprovalDetails.DataKeys[index].Values[1].ToString();

            //   string BasehttpPath = DrawingDocumentSavePath + lblEnquiryNumber.Text + "\\";
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";

            string FileName = gvDesignApprovalDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "ViewDocs")
            {
                objc = new cCommon();
                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, FileName, ddlEnquiryNumber.SelectedValue.ToString(), ifrm);

                //byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + FileName);
                //string base64String = Convert.ToBase64String(imageBytes);
                //ifrm.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                //ifrm.Attributes.Add("src", BasehttpPath + FileName);

                //string imgname = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName;
                //if (File.Exists(imgname))
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                //else
                //{
                //    ifrm.Attributes.Add("src", BasehttpPath + FileName);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                //}
            }
            if (e.CommandName == "ViewDeviationFile")
            {
                objc = new cCommon();
                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, FileName, ddlEnquiryNumber.SelectedValue.ToString(), ifrm);

                //ifrm.Attributes.Add("src", BasehttpPath + FileName);
                //string DeviationFileName = gvDesignApprovalDetails.DataKeys[index].Values[4].ToString();

                //string ImageName = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + DeviationFileName;

                //if (File.Exists(ImageName))
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                //else
                //{
                //    ifrm.Attributes.Add("src", BasehttpPath + FileName);
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attachements Not Found');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ddlEnquiryLoad()
    {
        objc = new cCommon();

        DataSet dsEnquiryNumber = new DataSet();
        DataSet dsCustomer = new DataSet();
        try
        {
            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeIDForDrawingApprovalPage", rblEnquiryChange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];
            dsEnquiryNumber = objc.GetEnquiryNumberByPendingList(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserIDForDrawingApprovalPage", rblEnquiryChange.SelectedValue);

            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails()
    {
        objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.EnquiryID = ddlEnquiryNumber.SelectedValue;
            ds = objc.GetEmployeeIDByEnquiryID("LS_GetEmployeeIDByEnquiryID");

            objAlerts.EntryMode = "Individual";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = objSession.employeeid;
            objAlerts.reciverType = "Individual";
            objAlerts.file = "";
            objAlerts.reciverID = ds.Tables[0].Rows[0]["Design"].ToString();
            objAlerts.EmailID = "";
            objAlerts.GroupID = 0;
            objAlerts.Subject = "Drawing Approved Alert";
            objAlerts.Message = "Drawing Approved From Enquiry Number " + ddlEnquiryNumber.SelectedValue;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateApprovalStatus()
    {
        DataSet ds = new DataSet();
        objHR = new c_HR();
        objDesign = new cDesign();
        objAlerts = new EmailAndSmsAlerts();
        string Message = "";
        bool check = false;
        try
        {
            string BaseSavepPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
            DataSet dsMessage = new DataSet();
            DataSet dsEmail = new DataSet();
            if (!string.IsNullOrEmpty(ViewState["SalesPerson"].ToString()))
            {
                dsEmail = objHR.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(ViewState["SalesPerson"].ToString()));
                if (dsEmail.Tables[0].Rows.Count > 0)
                {
                    if (dsEmail.Tables[0].Rows[0]["Email"].ToString() != "")
                    {
                        objAlerts.MessageID = 1;
                        dsMessage = objAlerts.GetAutomatedMessageMail();
                        Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
                        Message = Message.Replace("<EmployeeName>", ViewState["SalesPerson"].ToString());
                        Message = Message.Replace("<Version>", "");
                        Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue);
                        Message = Message.Replace("<OrderNumber>", "");
                    }
                }
                for (int i = 1; i <= gvDesignApprovalDetails.Rows.Count; i++)
                {
                    CheckBox chkitem = (CheckBox)gvDesignApprovalDetails.Rows[i - 1].FindControl("chkitems");
                    if (chkitem.Checked)
                    {
                        objDesign.DDID = Convert.ToInt32(gvDesignApprovalDetails.DataKeys[i - 1].Values[2].ToString());
                        TextBox txtRemarks = (TextBox)gvDesignApprovalDetails.Rows[i - 1].FindControl("txtRemarks");
                        objDesign.Remarks = txtRemarks.Text;
                        ds = objDesign.UpdateSharedWithSalesStatus();

                        check = true;
                        //try
                        //{
                        //    if (dsEmail.Tables[0].Rows.Count > 0)
                        //    {
                        //        if (dsEmail.Tables[0].Rows[0]["Email"].ToString() != "")
                        //        {
                        //            string FileName = gvDesignApprovalDetails.DataKeys[i - 1].Values[0].ToString();
                        //            objAlerts.file += BaseSavepPath + FileName + "|";
                        //            objAlerts.AttachementID = Convert.ToInt32(gvDesignApprovalDetails.DataKeys[i - 1].Values[3].ToString());
                        //            objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                        //            objAlerts.AlertType = "Mail";

                        //            objAlerts.reciverID = dsEmail.Tables[0].Rows[0]["EmployeeID"].ToString();
                        //            objAlerts.reciverType = "I";
                        //            objAlerts.GroupID = 0;
                        //            objAlerts.dtSettings = objAlerts.GetEmailSettings();
                        //            objAlerts.EmailID = dsEmail.Tables[0].Rows[0]["Email"].ToString(); //dsEmail.Tables[0].Rows[0]["Email"].ToString();//"karthik@innovasphere.in";
                        //            objAlerts.Subject = "Drawing Attachements";
                        //            objAlerts.Message = Message;
                        //            objAlerts.userID = objSession.employeeid;
                        //            //save Alert Details
                        //            objAlerts.Message = null;
                        //            objAlerts.SaveAlertDetails();
                        //        }
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    Log.Message(ex.ToString());
                        //}
                    }
                }
                //objAlerts.file = objAlerts.file.Remove(objAlerts.file.LastIndexOf("|"));
                //send Mail
                //  objAlerts.SendIndividualMail();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Shared With Sales Updated Successfully');", true);
                if (check)
                    SaveAlertDetails();
                ddlEnquiryNumber_SelectIndexChanged(null, null);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Please Allocate the Sales Resource');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvDesignApprovalDetails.Rows.Count > 0)
            gvDesignApprovalDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}