using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.IO;
using System.Configuration;

public partial class Pages_QuatetionSubmission : System.Web.UI.Page
{

    #region"Declaration"

    cPurchase objPur;
    cSession objSession;
    cCommon objc;
    bool IsPageRefresh = false;
    string PurchaseIndentSavePath = ConfigurationManager.AppSettings["PurchaseIndentSavePath"].ToString();
    string PurchaseIndentHttpPath = ConfigurationManager.AppSettings["PurchaseIndentHttpPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"pageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objc = new cCommon();
                DataSet dsPINum = new DataSet();
                DataSet dsRFNo = new DataSet();
                dsPINum = objc.GetPINumberByUserID(ddlPIIndentNumber, ddlRFPno, Convert.ToInt32(objSession.employeeid));
                ViewState["dsPINum"] = dsPINum.Tables[0];
                ShowHideControls("add");
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
            }
            else
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
            }
            if (target == "UpdateQuoteStatus" && IsPageRefresh == false)
                UpdateQuoteStatus();
            if (target == "ViewIndentCopy")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvQuateSubmission.DataKeys[index].Values[1].ToString();
                ViewState["PID"] = gvQuateSubmission.DataKeys[index].Values[0].ToString();
                ViewDrawingFilename();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button events"

    //protected void btnShareQuote_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objPur = new cPurchase();
    //    Label lblQuateStatus;
    //    string QuateStatus = "";
    //    try
    //    {
    //        for (int i = 0; i < gvQuateSubmission.Rows.Count; i++)
    //        {
    //            lblQuateStatus = (Label)gvQuateSubmission.Rows[i].FindControl("lblQuateStatus");
    //            if (lblQuateStatus.Text == "Completed")
    //                QuateStatus = "Completed";
    //            else
    //            {
    //                QuateStatus = "Incomplete";
    //                break;
    //            }
    //        }
    //        if (QuateStatus == "Incomplete")
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','All Materials Shouldve Quoted before Sharing');", true);
    //        else
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "QUateStatus", "UpdateQuoteStatus();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error Occured');", true);
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            objPur = new cPurchase();
            objPur.QHID = Convert.ToInt32(ddlPIIndentNumber.SelectedValue);

            ds = objPur.GetCategoryNameByListByIndentNumber(ddlCategoryName);
            //bindPurchaseIndentDetailsByPINumber("popup");
            //objPur.GetSupplierDetails(ddlSupplierName);
            gvQuateSubmissionDetails.Visible = false;
            btnSaveQPDetails.Visible = false;
            //  lnkAttachmentFile.Text = "";
            objc.EmptyDropDownList(ddlSupplierName);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        objPur = new cPurchase();
        objc = new cCommon();
        cSales objSales = new cSales();
        try
        {
            if (IsPageRefresh == false)
            {
                bool itemselected = false;
                string AttachmentID = "";
                string Remarks = "";
                string Msg = "";
                foreach (GridViewRow row in gvQuateSubmissionDetails.Rows)
                {
                    CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                    if (chkditems.Checked)
                    {
                        DataSet ds = new DataSet();
                        if (fpSupplierAttachements.HasFile && itemselected == false)
                        {
                            objc.Foldername = Session["PurchaseDocsSavePath"].ToString();
                            string Name = Path.GetFileName(fpSupplierAttachements.PostedFile.FileName);
                            string MaximumAttacheID = objSales.GetMaximumAttachementID();
                            string[] extension = Name.Split('.');
                            Name = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                            objc.FileName = Name;
                            objc.PID = ddlPIIndentNumber.SelectedValue;
                            objc.AttachementControl = fpSupplierAttachements;
                            objc.SaveFiles();
                            ds = objPur.SaveAttachment(objc.FileName, txtRemarks.Text);
                            AttachmentID = ds.Tables[0].Rows[0]["AttachementID"].ToString();
                            Remarks = ds.Tables[0].Rows[0]["Description"].ToString();
                        }
                        itemselected = true;
                        int PID = Convert.ToInt32(gvQuateSubmissionDetails.DataKeys[row.RowIndex].Values[0]);
                        TextBox txtCost = (TextBox)gvQuateSubmissionDetails.Rows[row.RowIndex].FindControl("txtCost");
                       // TextBox txtRequiredWeight = (TextBox)gvQuateSubmissionDetails.Rows[row.RowIndex].FindControl("txtRequiredWeight");

                        decimal UnitCost = Convert.ToDecimal(txtCost.Text);
                        decimal RequiredWeight = Convert.ToDecimal(0);

                        decimal TotalCost = 0;

                        objPur.PID = PID;
                        objPur.SUPID = Convert.ToInt32(ddlSupplierName.SelectedValue);
                        objPur.QHID = Convert.ToInt32(ddlPIIndentNumber.SelectedValue);
                        objPur.AttachmentID = AttachmentID;
                        objPur.Quotecost = Convert.ToDecimal(UnitCost);
                        objPur.RequiredWeight = RequiredWeight.ToString();
                        objPur.TotalCost = TotalCost;
                        ds = objPur.SaveQuote();
                        Msg = ds.Tables[0].Rows[0]["MSG"].ToString();
                    }
                }
                if (itemselected == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Quoted " + Msg + " Successfully');", true);
                    bindPurchaseIndentDetailsByPINumber("main");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Materials Selected');", true);
                }
            }
        }
        catch (Exception ec)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCategoryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objPur = new cPurchase();
        objc = new cCommon();
        try
        {
            if (ddlCategoryName.SelectedIndex > 0)
                objPur.GetSupplierDetailsByCategoryName(ddlSupplierName, "LS_GetSupplierDetailsByCategoryName");
            else
            {
                //  lnkAttachmentFile.Text = "";
                gvQuateSubmissionDetails.Visible = false;
                btnSaveQPDetails.Visible = false;
                objc.EmptyDropDownList(ddlSupplierName);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlPIIndentNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objPur = new cPurchase();
        string RFNo = "";
        try
        {
            if (ddlPIIndentNumber.SelectedIndex > 0)
            {
                objPur.QHID = Convert.ToInt32(ddlPIIndentNumber.SelectedValue);
                RFNo = objPur.GetRFPNoByPINumber();
                ddlRFPno.SelectedValue = RFNo;

                bindPurchaseIndentDetailsByPINumber("main");
                ShowHideControls("add,addnew,view");
            }
            else
            {
                ddlRFPno.SelectedIndex = 0;
                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlSupplierName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSupplierName.SelectedIndex > 0)
            {
                bindPurchaseIndentDetailsByPINumber("popup");
                gvQuateSubmissionDetails.Visible = true;
                btnSaveQPDetails.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "MakeCostMandatory('all');", true);
            }
            else
            {
                //   lnkAttachmentFile.Text = "";
                gvQuateSubmissionDetails.Visible = false;
                btnSaveQPDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPno_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = (DataTable)ViewState["dsPINum"];
            if (ddlRFPno.SelectedIndex > 0)
            {
                if (ddlRFPno.SelectedValue == "G")
                {
                    dt.DefaultView.RowFilter = "RFPNo='G'";
                    dt.DefaultView.ToTable();
                }
                else
                {
                    dt.DefaultView.RowFilter = "RFPHID='" + ddlRFPno.SelectedValue + "'";
                    dt.DefaultView.ToTable();
                }
            }

            ddlPIIndentNumber.DataSource = dt;
            ddlPIIndentNumber.DataTextField = "PINumber";
            ddlPIIndentNumber.DataValueField = "QHID";
            ddlPIIndentNumber.DataBind();
            ddlPIIndentNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvQuateSubmission_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkQC");
               /*  if (dr["QuoteStatus"].ToString() == "Completed" && dr["QuotationSharedStatus_PI"].ToString() == "0")
                    chk.Visible = true; */
				
                if (dr["QuoteStatus"].ToString() == "Completed" && dr["QuotationSharedStatus_PI"].ToString() == "0")
                {
                    if((dr["SupplierCount"].ToString() == "3" || (dr["SupplierCount"].ToString() == "1" && dr["VendorType"].ToString() == "2")))
                        chk.Visible = true;
                    else

                        chk.Visible = false;
                }
                else
                    chk.Visible = false;
				
				
                if (objSession.type == 1)
                    e.Row.Cells[15].Visible = true;
                else
                    e.Row.Cells[15].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvQuateSubmission_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "Review")
            {
                hdnPID.Value = gvQuateSubmission.DataKeys[index].Values[0].ToString();
                Quotationreview();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Comon Methods"

    private void ViewDrawingFilename()
    {
        try
        {
            objc = new cCommon();

            objc.ViewFileName(PurchaseIndentSavePath, PurchaseIndentHttpPath, ViewState["FileName"].ToString(), ViewState["PID"].ToString(), ifrm);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowIndentPopUp();", true);
            //if (File.Exists(imgname))
            //{
            //    ViewState["ifrmsrc"] = httpPath + ViewState["FileName"].ToString();
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
            //    string s = "window.open('" + httpPath + EnquiryID + "/" + ViewState["FileName"].ToString() + "','_blank');ShowViewPopUP();";
            //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //}
            //else
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);                
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUP();", true);
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateQuoteStatus()
    {
        DataSet ds = new DataSet();
        objPur = new cPurchase();
        string PIDs = "";
        try
        {
            foreach (GridViewRow row in gvQuateSubmission.Rows)
            {
                CheckBox chkQC = (CheckBox)row.FindControl("chkQC");
                if (chkQC.Checked)
                {
                    if (PIDs == "")
                        PIDs = gvQuateSubmission.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PIDs = PIDs + "," + gvQuateSubmission.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }
            objPur.PIDs = PIDs;
            ds = objPur.updateQuateStatus();

            if (ds.Tables[0].Rows[0]["MSG"].ToString() == "Inserted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Quotation Details shared Successfully')", true);
                bindPurchaseIndentDetailsByPINumber("main");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    private void Quotationreview()
    {
        DataSet ds = new DataSet();
        objPur = new cPurchase();
        try
        {
            objPur.QHID = Convert.ToInt32(ddlPIIndentNumber.SelectedValue);
            objPur.PID = Convert.ToInt32(hdnPID.Value);
            objPur.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objPur.UpdateQuotationReviewStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Quotation Details Updated Successfully')", true);
                bindPurchaseIndentDetailsByPINumber("main");
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "')", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindPurchaseIndentDetailsByPINumber(string Mode)
    {
        objPur = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            objPur.QHID = Convert.ToInt32(ddlPIIndentNumber.SelectedValue);
            objPur.CID = Convert.ToInt32(ddlCategoryName.SelectedValue);
            objPur.PISUPID = 0;
            if (Mode != "main")
                objPur.PISUPID = Convert.ToInt32(ddlSupplierName.SelectedValue);

            ds = objPur.GetPurchaseIndentDetailsByPINumber();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Mode == "main")
                {
                    gvQuateSubmission.DataSource = ds.Tables[0];
                    gvQuateSubmission.DataBind();
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        if (ds.Tables[2].Rows[0]["BtnVisibleStatus"].ToString() == "False")
                        {
                            btnShareQuote.Enabled = false;
                            btnAddNew.Enabled = false;
                        }
                        else
                        {
                            btnShareQuote.Enabled = true;
                            btnAddNew.Enabled = true;
                        }
                    }

                    if (objSession.type == 1)
                        gvQuateSubmission.Columns[10].Visible = true;
                    else
                        gvQuateSubmission.Columns[10].Visible = false;
                }
                else
                {
                    gvQuateSubmissionDetails.DataSource = ds.Tables[0];
                    gvQuateSubmissionDetails.DataBind();
                    //if (ds.Tables[1].Rows.Count > 0)
                    //{
                    //    // lnkAttachmentFile.Text = ds.Tables[1].Rows[0]["FileName"].ToString();
                    //    fpSupplierAttachements.Attributes.Add("class", fpSupplierAttachements.Attributes["class"].ToString().Replace("mandatoryfield", ""));
                    //    lblfileuploader.Attributes.Add("class", lblfileuploader.Attributes["class"].ToString().Replace("mandatorylbl", ""));
                    //}
                    //else
                    //{
                    //    fpSupplierAttachements.Attributes.Add("class", fpSupplierAttachements.Attributes["class"].ToString().Replace("mandatoryfield", "") + " mandatoryfield");
                    //    lblfileuploader.Attributes.Add("class", lblfileuploader.Attributes["class"].ToString().Replace("mandatorylbl", "") + " mandatorylbl");
                    //}
                }
            }
            else
            {
                if (Mode == "main")
                {
                    gvQuateSubmission.DataSource = "";
                    gvQuateSubmission.DataBind();
                }
                else
                {
                    gvQuateSubmissionDetails.DataSource = ds.Tables[0];
                    gvQuateSubmissionDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divAddNew.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "addnew":
                        divAddNew.Visible = true;
                        break;
                    case "add":
                        divAdd.Visible = true;
                        break;
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

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvQuateSubmission.Rows.Count > 0)
            gvQuateSubmission.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}