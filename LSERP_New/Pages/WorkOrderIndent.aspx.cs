using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public partial class Pages_WorkOrderIndent : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cProduction objP;
    cMaterials objMat;
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (IsPostBack == false)
            {
                objP = new cProduction();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objP.GetRFPCustomerNameByMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsRFPHID = objP.GetRFPDetailsByUserIDAndMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                ViewState["RFPDetails"] = dsRFPHID.Tables[0];

                divOutput.Visible = false;
            }
            if (target == "deleteIndent")
            {
                objP = new cProduction();
                objP.WOIHID = Convert.ToInt32(arg.ToString());
                DataSet ds = objP.DeleteWorkorderIndentDetailsByWOIHID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Indent Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                ReloadPopup();
            }
            if (target == "ViewWODrawingFile")
                viewWorkOrderDrawingFile(arg.ToString());
            if (target == "ShareQC")
                ShareQC();
            if (target == "WorkOrderIndentPrint")
                BindWorkOrderIndentPDFDetails(Convert.ToInt32(arg.ToString()));
            //if (target == "ViewDrawings")
            //    ViewDrawings(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDownEvents"

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;

                hdnRFPHID.Value = ddlRFPNo.SelectedValue;

                //bindWorkOrderIndentDetails();
                //  BindItemDetails();

                BindPartDetailsByRFPDID();
                BindWorkOrderIndentDetailsByItemID();

                divOutput.Visible = true;
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["RFPDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlRFPNo.DataSource = dt;
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlPartName_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlPartName.SelectedIndex > 0)
            //    BindPartDetailsByMPID();
            //else
            //{
            //    gvPartSno.DataSource = "";
            //    gvPartSno.DataBind();
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlPONo_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPONo.SelectedIndex > 0)
            {
                bindBoughtOutItemMRNDetailsByMPID();
                ShowHideMRNControls("add,view");
            }
            else
                ShowHideMRNControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button events"

    protected void btnSaveWOI_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        string AttachmentName = "";
        string PRPDIDs = "";
        string JobListID = "";
        string MPIDs = "";
        bool msg = true;
        try
        {
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            // objP.MPID = Convert.ToInt32(ddlPartName.SelectedValue);
            objP.jobDescription = txtJobDescription.Text;
            objP.Remarks = txtremarks.Text;
            objP.RawMaterialQuantity = Convert.ToInt32(txtRawmaterialQuantity.Text);

            // objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.RFPHID = Convert.ToInt32(hdnRFPHID.Value);

            //objP.JobQty = Convert.ToInt32(txtJobQty.Text);           

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();

                string Attchname = "";

                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                string[] extension = AttachmentName.Split('.');
                Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");
                AttachmentName = Attchname + '_' + "WODrawing" + '.' + extension[extension.Length - 1];
            }
            objP.AttachementName = AttachmentName;

            //if (hdnWOIHID.Value == "0")
            //{
            //foreach (GridViewRow row in gvPartSno.Rows)
            //{
            //    CheckBox chk = (CheckBox)row.FindControl("chkQC");
            //    if (chk.Checked)
            //    {
            //        if (PRPDIDs == "")
            //            PRPDIDs = gvPartSno.DataKeys[row.RowIndex].Values[0].ToString();
            //        else
            //            PRPDIDs = PRPDIDs + "," + gvPartSno.DataKeys[row.RowIndex].Values[0].ToString();
            //    }
            //}

            foreach (GridViewRow row in gvPartDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                TextBox txtJobQty = (TextBox)row.FindControl("txtJobCuttingQty");
                TextBox txtIndentQty = (TextBox)row.FindControl("txtIndentQty");

                string RFPDID = gvPartDetails.DataKeys[row.RowIndex].Values[1].ToString();

                if (chk.Checked)
                {
                    if (Convert.ToInt32(txtJobQty.Text) > 0 && Convert.ToInt32(txtIndentQty.Text) > 0)
                    {
                        if (MPIDs == "")
                            MPIDs = gvPartDetails.DataKeys[row.RowIndex].Values[0].ToString() + "." + txtIndentQty.Text + "." + txtJobQty.Text + "." + RFPDID;
                        else
                            MPIDs = MPIDs + "," + gvPartDetails.DataKeys[row.RowIndex].Values[0].ToString() + "." + txtIndentQty.Text + "." + txtJobQty.Text + "." + RFPDID;
                    }
                    else
                        msg = false;
                }
            }
            //}

            foreach (ListItem li in LiJobOperationList.Items)
            {
                if (li.Selected)
                {
                    if (JobListID == "")
                        JobListID = li.Value;
                    else if (JobListID != "")
                        JobListID = JobListID + ',' + li.Value;
                }
            }

            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objP.WOjobListID = JobListID;
            // objP.PRPDIDs = PRPDIDs;
            objP.MPIDs = MPIDs;

            objP.QCPlan = rblQCPlan.SelectedValue;

            if (txtJobWeight.Text != "")
                objP.JobWeight = Convert.ToDecimal(txtJobWeight.Text);
            else
                objP.JobWeight = 0;

            if (msg)
            {
                ds = objP.SaveWorkOrderIndentDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Indent Saved Successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Indent Updated Successfully');", true);
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                    msg = false;
                }
                if (msg)
                {
                    if (AttachmentName != "")
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objc = new cCommon();
                            objc.Foldername = Session["WorkOrderIndentSavePath"].ToString();
                            objc.FileName = AttachmentName;
                            objc.PID = ds.Tables[0].Rows[0]["WOINo"].ToString();
                            objc.AttachementControl = fAttachment;
                            objc.SaveFiles();
                        }
                    }
                    hdnWOIHID.Value = "0";
                    ReloadPopup();
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Job Qty Should Not Grater Zero');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','ErrorOccured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnWOIHID.Value = "0";
            //ddlPartName.SelectedIndex = 0;
            txtJobDescription.Text = "";
            txtremarks.Text = "";
            BindPartDetailsByRFPDID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnIssueMRN_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        string MRNDetails = "";
        try
        {
            //objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            objP.WPOID = Convert.ToInt32(ddlPONo.SelectedValue);

            foreach (GridViewRow row in gvMRNIssueBOI_WorkOrder.Rows)
            {
                TextBox txtIssuedQty = (TextBox)row.FindControl("txtIssuedQty");
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                if (chkitems.Checked)
                {
                    if (MRNDetails == "")
                        MRNDetails = gvMRNIssueBOI_WorkOrder.DataKeys[row.RowIndex].Values[0].ToString() + "#"
                            + gvMRNIssueBOI_WorkOrder.DataKeys[row.RowIndex].Values[1].ToString() + "#" + txtIssuedQty.Text;
                    else
                        MRNDetails = MRNDetails + "," + gvMRNIssueBOI_WorkOrder.DataKeys[row.RowIndex].Values[0].ToString() + "#"
                          + gvMRNIssueBOI_WorkOrder.DataKeys[row.RowIndex].Values[1].ToString() + "#" + txtIssuedQty.Text;
                }
            }
            objP.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objP.SaveWorkOrderBoughtOutItemMRNIssueDetails(MRNDetails);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','MRN Issued Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Gridview Events"

    protected void gvMPItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void gvMPItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument.ToString());
    //        if (e.CommandName == "ADD")
    //        {
    //            hdnWOIHID.Value = "0";
    //            string[] RFPDID = gvMPItemDetails.DataKeys[index].Values[0].ToString().Split('/');
    //            hdnRFPDID.Value = RFPDID[0];
    //            hdnEDID.Value = RFPDID[1];
    //            Label lblItemName = (Label)gvMPItemDetails.Rows[index].FindControl("lblItemName");

    //            ViewState["Itemname"] = lblItemName.Text;
    //            lblitemname_h.Text = "( " + lblItemName.Text + " )" + "RFP No:" + ddlRFPNo.SelectedItem.Text;

    //            //gvPartSno.DataSource = "";
    //            //gvPartSno.DataBind();

    //            BindPartDetailsByRFPDID();
    //            BindWorkOrderIndentDetailsByItemID();

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowWoPopup();", true);
    //            //Session["RFPDID"] = hdnRFPDID.Value;
    //            //Session["RFPHID"] = ddlRFPNo.SelectedValue;

    //            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
    //            //url = url.ToLower();

    //            //string[] pagename = url.ToString().Split('/');
    //            //string Replacevalue = pagename[pagename.Length - 1].ToString();
    //            //string s = "window.open('" + url.Replace(Replacevalue, "AddWorkOrderIndentDetails.aspx") + "','_blank');";
    //            //this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    //        }
    //        if (e.CommandName == "ViewDrawings")
    //        {
    //            string EnquiryNumber = gvMPItemDetails.DataKeys[index].Values[2].ToString();
    //            string DrawingName = gvMPItemDetails.DataKeys[index].Values[1].ToString();

    //            string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    //            string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    //            // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
    //            string BasehttpPath = DrawingDocumentHttppath + EnquiryNumber + "/";
    //            string FileName = BasehttpPath + DrawingName;

    //            ifrm.Attributes.Add("src", FileName);
    //            if (File.Exists(DrawingDocumentSavePath + EnquiryNumber + '/' + DrawingName))
    //            {
    //                string s = "window.open('" + FileName + "','_blank');";
    //                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    //            }
    //            // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
    //            else
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void gvWorkOrderIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnWOIHID.Value = gvWorkOrderIndentDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "EditWO")
            {
                EditWorkOrderIndentDetails();
            }
            if (e.CommandName == "issueRM")
            {
                BindBoughtOutItemPONumber();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ShowBOIIssuePopup();", true);
                ShowHideMRNControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkQC");
                TextBox txtJobCuttingQty = (TextBox)e.Row.FindControl("txtJobCuttingQty");


                if (dr["TotalPartQty"].ToString() == "0" && dr["Count"].ToString() == "1")
                {
                    chk.Visible = false;
                }
                else
                {
                    chk.Visible = true;
                }
                if (dr["EditMPID"].ToString() == "0")
                {
                    chk.Checked = false;
                    txtJobCuttingQty.Text = "";
                    txtJobCuttingQty.CssClass = "form-control";
                }
                else
                {
                    chk.Checked = true;
                    txtJobCuttingQty.Text = dr["CuttingQty"].ToString();
                    txtJobCuttingQty.CssClass = "form-control mandatoryfield";
                }

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton btndelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");
                if (string.IsNullOrEmpty(dr["ShareQC"].ToString()))
                {
                    btndelete.Visible = true;
                    chkQC.Visible = true;
                }
                else
                {
                    btndelete.Visible = false;
                    chkQC.Visible = false;
                }

                if (dr["BtnEnable"].ToString() == "hide")
                {
                    btnEdit.Visible = false;
                }
                else
                {
                    btnEdit.Visible = true;
                }

                if (objSession.type == 1)
                    btndelete.Visible = true;

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    public void SaveAlertDetails(string WOINo)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 10;
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objc.JCHID = 0;
            ds = objc.GetStaffDetailsByRFPHIDAndUserType();

            string[] str = ds.Tables[0].Rows[0]["EmployeeIDS"].ToString().Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = str[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Work Order Indent Raised Alert";
                objAlerts.Message = "Work Order Indent Raised From RFP No" +
                    ddlRFPNo.SelectedItem.Text + " And WOINo " + WOINo + "Add Work Order QC Planning";
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ReloadPopup()
    {
        try
        {
            //gvPartSno.DataSource = "";
            //gvPartSno.DataBind();
            BindPartDetailsByRFPDID();
            BindWorkOrderIndentDetailsByItemID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShareQC()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        string WOIHID = "";
        try
        {
            foreach (GridViewRow row in gvWorkOrderIndentDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    if (WOIHID == "")
                        WOIHID = gvWorkOrderIndentDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        WOIHID = WOIHID + "," + gvWorkOrderIndentDetails.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            if (WOIHID != "")
            {
                ds = objP.UpdateShareIndentToQC(WOIHID);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Indent QC Shared Successfully');", true);
                    SaveAlertDetails(ds.Tables[0].Rows[0]["WOINo"].ToString());
                    BindWorkOrderIndentDetailsByItemID();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Indent Selected');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void EditWorkOrderIndentDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            //objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

            ds = objP.GetWorkOrderIndentDetailsByWOIHID();

            // ddlPartName.SelectedValue = ds.Tables[0].Rows[0]["MPID"].ToString();
            BindPartDetailsByMPID();
            txtJobDescription.Text = ds.Tables[1].Rows[0]["JobDescription"].ToString();
            txtremarks.Text = ds.Tables[1].Rows[0]["Remarks"].ToString();
            txtRawmaterialQuantity.Text = ds.Tables[1].Rows[0]["RawMaterialQuantity"].ToString();

            //txtJobQty.Text = ds.Tables[0].Rows[0]["PartQuanity"].ToString();
            txtJobWeight.Text = ds.Tables[1].Rows[0]["JobWeight"].ToString();
            rblQCPlan.SelectedValue = ds.Tables[1].Rows[0]["QCPlanMandatory"].ToString();

            gvPartDetails.DataSource = ds.Tables[0];
            gvPartDetails.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselect('" + ds.Tables[1].Rows[0]["JobOperationList"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetailsByMPID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            //objP.MPID = Convert.ToInt32(ddlPartName.SelectedValue);
            //ds = objP.GetPartSnoByMPID();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    gvPartSno.DataSource = ds.Tables[0];
            //    gvPartSno.DataBind();
            //}
            //else
            //{
            //    gvPartSno.DataSource = "";
            //    gvPartSno.DataBind();
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindItemDetails()
    {
        //DataSet ds = new DataSet();
        //objMat = new cMaterials();
        //try
        //{
        //    objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
        //    ds = objMat.GetItemDetailsByRFPHID(null, "LS_GetItemDetailsByRFPHIDByWorkOrderIndent");

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        gvMPItemDetails.DataSource = ds.Tables[0];
        //        gvMPItemDetails.DataBind();
        //    }
        //    else
        //    {
        //        gvMPItemDetails.DataSource = "";
        //        gvMPItemDetails.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Log.Message(ex.ToString());
        //}
    }

    private void BindPartDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            // objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.RFPHID = Convert.ToInt32(hdnRFPHID.Value);
            ds = objP.GetPartDetailsByRFPDID(null, LiJobOperationList);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartDetails.DataSource = ds.Tables[0];
                gvPartDetails.DataBind();
            }
            else
            {
                gvPartDetails.DataSource = "";
                gvPartDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderIndentDetailsByItemID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            // objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.RFPHID = Convert.ToInt32(hdnRFPHID.Value);
            ds = objP.GetWorkOrderIndentDetailsByRFPDID();

            gvWorkOrderIndentDetails.DataSource = ds.Tables[0];
            gvWorkOrderIndentDetails.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderIndentPDFDetails(int index)
    {
        DataSet ds;
        objP = new cProduction();
        cQRcode objQr = new cQRcode();
        try
        {
            string WOIHID = gvWorkOrderIndentDetails.DataKeys[index].Values[0].ToString();
            objP.WOIHID = Convert.ToInt32(WOIHID);

            ds = objP.GetWorkOrderIndenPrintDetailsByWOIHID();

            lblRFPNo_P.Text = ddlRFPNo.SelectedItem.Text;
            lblWorkOrderID_P.Text = ds.Tables[0].Rows[0]["WOINo"].ToString();
            lblIndentRaisedBy.Text = ds.Tables[0].Rows[0]["IndentBy"].ToString() + " " + ds.Tables[0].Rows[0]["IndentDate"].ToString();
            lblRemarks_p.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            lblItemName_p.Text = lblitemname_h.Text;
            lblPartName_p.Text = ds.Tables[0].Rows[0]["PartName"].ToString();
            lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQuanity"].ToString();
            lblRawMaterialQty_p.Text = ds.Tables[0].Rows[0]["RawMaterialQuantity"].ToString();
            lblJobDescription_p.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
            lblJobOperation_p.Text = ds.Tables[0].Rows[0]["JobOpeartionName"].ToString();

            hdnAddress.Value = ds.Tables[1].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[1].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[1].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[1].Rows[0]["WebSite"].ToString();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "WorkOrderIndentPrint";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }

            // GeneratePDF();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "PrintWorkorderIndentDetails('" + QrCode + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            var sbPurchaseOrder = new StringBuilder();

            divWorkOrderIndent_PDF.Attributes.Add("style", "display:block;");

            divWorkOrderIndent_PDF.RenderControl(new HtmlTextWriter(new StringWriter(sbPurchaseOrder)));

            string htmlfile = lblWorkOrderID_P.Text + ".html";
            string pdffile = lblWorkOrderID_P.Text + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string pdfFileURL = LetterPath + pdffile;

            string htmlfileURL = LetterPath + htmlfile;

            SaveHtmlFile(htmlfileURL, "PO", "", sbPurchaseOrder.ToString());

            objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);

            divWorkOrderIndent_PDF.Attributes.Add("style", "display:none;");

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string Main = url.Replace("pages/WorkOrderIndent", "Assets/css/main.css");
            string epstyleurl = url.Replace("pages/WorkOrderIndent", "Assets/css/ep-style.css");
            string style = url.Replace("pages/WorkOrderIndent", "Assets/css/style.css");
            string Print = url.Replace("pages/WorkOrderIndent", "Assets/css/print.css");
            string topstrip = url.Replace("pages/WorkOrderIndent", "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine(HeaderTitle);
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;'>");
            w.WriteLine(div);
            w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void viewWorkOrderDrawingFile(string index)
    {
        objc = new cCommon();
        try
        {
            string FileName = gvWorkOrderIndentDetails.DataKeys[Convert.ToInt32(index)].Values[1].ToString();
            string WONo = gvWorkOrderIndentDetails.DataKeys[Convert.ToInt32(index)].Values[2].ToString();
            objc.ViewFileName(Session["WorkOrderIndentSavePath"].ToString(), Session["WorkOrderIndentHttpPath"].ToString(), FileName, WONo, ifrm);
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowWoPopup();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindBoughtOutItemPONumber()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            ds = objP.GetBoughtOutItemPONumberByWOIHID(ddlPONo);
            lblTotalPartQty_BOIMRN.Text = hdnTotalPartQty.Value = ds.Tables[1].Rows[0]["TotalPartQty"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindBoughtOutItemMRNDetailsByMPID()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            objP.WPOID = Convert.ToInt32(ddlPONo.SelectedValue);
            ds = objP.GetBoughtOutItemMRNDetailsByMPID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMRNIssueBOI_WorkOrder.DataSource = ds.Tables[0];
                gvMRNIssueBOI_WorkOrder.DataBind();
            }
            else
            {
                gvMRNIssueBOI_WorkOrder.DataSource = "";
                gvMRNIssueBOI_WorkOrder.DataBind();
            }

            ////lblPOQty_BOIMRN.Text = hdnPOQty.Value = ds.Tables[1].Rows[0]["POQty"].ToString();
            ////hdnTotalBlockedQty.Value = ds.Tables[1].Rows[0]["TotalBlockedWeight"].ToString();  //TotalBlockedWeight

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvBOIIssuedDetails_WorkOrder.DataSource = ds.Tables[2];
                gvBOIIssuedDetails_WorkOrder.DataBind();
            }
            else
            {
                gvBOIIssuedDetails_WorkOrder.DataSource = "";
                gvBOIIssuedDetails_WorkOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    //gvIssuedDetails_WorkOrder

    private void ShowHideMRNControls(string divids)
    {
        divadd_BOIMRN.Visible = divoutput_BOIMRN.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divadd_BOIMRN.Visible = true;
                        break;
                    case "view":
                        divoutput_BOIMRN.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //private void ViewDrawings(int index)
    //{
    //    try
    //    {
    //        string EnquiryNumber = gvMPItemDetails.DataKeys[index].Values[2].ToString();
    //        string DrawingName   = gvMPItemDetails.DataKeys[index].Values[1].ToString();

    //        string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    //        string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    //        // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
    //        string BasehttpPath = DrawingDocumentHttppath + EnquiryNumber + "/";
    //        string FileName = BasehttpPath + DrawingName;

    //        ifrm.Attributes.Add("src", FileName);
    //        if (File.Exists(DrawingDocumentSavePath + EnquiryNumber + '/' + DrawingName))
    //        {
    //            string s = "window.open('" + FileName + "','_blank');";
    //            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    //        }
    //        // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
    //        else
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvPartDetails.Rows.Count > 0)
            gvPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}