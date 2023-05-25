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
using System.Web.UI.HtmlControls;

public partial class Pages_MaterialInward : System.Web.UI.Page
{

    #region"Declaration"

    cPurchase objPc;
    cCommon objc;
    cStores objSt;
    cSession objSession = new cSession();

    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objPc = new cPurchase();
                objc = new cCommon();
                objPc.GetSupplierDetails(ddlSuplierName);
                ShowHideControls("add");
            }
            if (target == "deletegvrow")
            {
                objSt = new cStores();
                int AttachID = Convert.ToInt32(arg);
                DataSet ds = objSt.DeleteAttachementDetailsByAttachementID(AttachID);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','DC Attachements Deleted successfully');", true);
                BindAttachements(hdnMIHID.Value);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlLocationName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLocationName.SelectedIndex > 0)
            {
                BindMaterialInward();
                ShowHideControls("add,view,addnew");
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlSuplierName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            if (ddlSuplierName.SelectedIndex > 0)
            {
                objc.GetLocationDetails(ddlLocationName);
                ShowHideControls("add");
            }
            else
            {
                ddlLocationName.Items.Clear();
                ShowHideControls("add");
            }
            ddlLocationName.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlSPONumber_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSPONumber.SelectedIndex > 0)
            {
                divSPONumber.Visible = true;
                divOutputsItems.Visible = true;
                BindSupplierPOItemDetails();
                // BindAddedMaterials();
            }
            else
                divOutputsItems.Visible = false;

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            hdnMIHID.Value = "0";
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveMI_Click(object sender, EventArgs e)
    {
        objSt = new cStores();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (objc.Validate(divInput))
            {
                cSales objSales = new cSales();

                objSt.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
                objSt.LocationID = Convert.ToInt32(ddlLocationName.SelectedValue);
                objSt.MIHID = Convert.ToInt32(hdnMIHID.Value);
                objSt.DCNumber = txtDCNumber.Text;
                objSt.DCdate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objSt.InvoiceNumber = txtInVoiceNumber.Text;
                objSt.InvoiceDate = DateTime.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //if (fpDCAttach.HasFile)
                //{
                //    objc.Foldername = Session["StoresDocsSavePath"].ToString();
                //    string Name = Path.GetFileName(fpDCAttach.PostedFile.FileName);
                //    string MaximumAttacheID = objSales.GetMaximumAttachementID();
                //    string[] extension = Name.Split('.');
                //    Name = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                //    objc.FileName = Name;
                //    objc.PID = ddlSuplierName.SelectedValue + '_' + ddlLocationName.SelectedValue;
                //    objc.AttachementControl = fpDCAttach;
                //    objc.SaveFiles();
                //    objSt.DCCopy = Name;
                //}


                ds = objSt.SaveMaterialInward();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Inward Saved Successfully');", true);
                    BindMaterialInward();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Inward Updated Successfully');", true);

                ShowHideControls("add,addnew,view");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnMIHID.Value = "0";
            ShowHideControls("add,addnew,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveMIDetail_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        bool itemselected = false;
        objSt = new cStores();
        try
        {
            foreach (GridViewRow row in gvSupplierPOItemDetails.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                FileUpload fb = (FileUpload)row.FindControl("fpDCMRNAttach");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                if (chkditems.Checked)
                {
                    string AttachmentName = "";
                    if (fb.HasFile)
                    {
                        string MaxAttachementId = objSt.GetMaximumAttachementID();
                        string extn = Path.GetExtension(fb.PostedFile.FileName).ToUpper();

                        AttachmentName = Path.GetFileName(fb.PostedFile.FileName);
                        string[] extension = AttachmentName.Split('.');
                        AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];
                    }

                    objSt.AttachementName = AttachmentName;
                    TextBox txtDCQty = (TextBox)gvSupplierPOItemDetails.Rows[row.RowIndex].FindControl("txtDCQuantity");
                    if (Convert.ToDecimal(txtDCQty.Text) > 0)
                    {
                        itemselected = true;

                        string SPODID = gvSupplierPOItemDetails.DataKeys[row.RowIndex].Values[1].ToString();
                        objSt.RJMIDID = Convert.ToInt32(gvSupplierPOItemDetails.DataKeys[row.RowIndex].Values[2].ToString());
                        objSt.MIHID = Convert.ToInt32(hdnMIHID.Value);
                        objSt.DcQty = Convert.ToDecimal(txtDCQty.Text);
                        objSt.SPODID = Convert.ToInt32(SPODID);
                        objSt.Remarks = txtRemarks.Text;
                        objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
                        ds = objSt.SaveMIDetails();
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIDID"].ToString()) && AttachmentName != "")
                    {
                        objc = new cCommon();
                        objc.Foldername = StoresDocsSavePath;
                        objc.FileName = AttachmentName;
                        objc.PID = ds.Tables[0].Rows[0]["MIDID"].ToString();
                        objc.AttachementControl = fb;
                        objc.SaveFiles();
                    }
                }
            }
            if (itemselected == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Added Successfully');", true);
                ddlSPONumber_OnSelectIndexChanged(null, null);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Materials Selected');", true);
            BindAddedMaterials();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "MakeMandatory('all');OpenTab('Documents');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }
    protected void btnsaveshare_Click(object sender, EventArgs e)
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.MIHID = Convert.ToInt32(hdnMIHID.Value);
            ds = objSt.UpdateInwardStatus();
            BindSupplierPOItemDetails();
            BindAddedMaterials();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Shared Successfully');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "MakeMandatory('all');OpenTab('Item');", true);
            BindMaterialInward();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }

    }
    protected void btnSaveAttchement_Click(object sender, EventArgs e)
    {
        objSt = new cStores();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            objSt.AttachementTypeName = Convert.ToInt32(ddlTypeName.SelectedValue);
            objSt.Description = txtDescription.Text;
            objSt.MIHID = Convert.ToInt32(hdnMIHID.Value);

            string MaxAttachementId = objSt.GetMaximumAttachementID();
            string AttachmentName = "";

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
            }

            string[] extension = AttachmentName.Split('.');

            AttachmentName = "DC" + '_' + MaxAttachementId + '.' + extension[1];
            objSt.AttachementName = AttachmentName;

            ds = objSt.SaveMaterialInwardAttachements();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachement Details Saved successfully');", true);

            string Directoryname = StoresDocsSavePath + hdnMIHID.Value + "\\";

            if (!Directory.Exists(Directoryname))
                Directory.CreateDirectory(Directoryname);

            if (AttachmentName != "")
                fAttachment.SaveAs(Directoryname + AttachmentName);

            BindAttachements(hdnMIHID.Value);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUp();", true);
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvMaterialInward_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string MIHID = gvMaterialInward.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["MI"];
            hdnMIHID.Value = MIHID.ToString();
            dt.DefaultView.RowFilter = "MIHID='" + MIHID + "'";

            if (e.CommandName == "EditMI")
            {
                txtDCNumber.Text = dt.DefaultView.ToTable().Rows[0]["DCNumber"].ToString();

                txtDCDate.Text = dt.DefaultView.ToTable().Rows[0]["DCDateEdit"].ToString();

                txtInvoiceDate.Text = dt.DefaultView.ToTable().Rows[0]["InVoiceDateEdit"].ToString();
                txtInVoiceNumber.Text = dt.DefaultView.ToTable().Rows[0]["InVoiceNumber"].ToString();

                ShowHideControls("input");
            }
            if (e.CommandName == "Add")
            {
                objPc = new cPurchase();
                DataSet ds = new DataSet();
                objSt = new cStores();
                objSt.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
                objSt.LocationID = Convert.ToInt32(ddlLocationName.SelectedValue);
                divOutputsItems.Visible = false;
                objSt.GetSPONumberBySUPIDAndLocationID(ddlSPONumber);
                BindAddedMaterials();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();OpenTab('Item');", true);
            }
            if (e.CommandName == "AddDocs")
            {
                objSt = new cStores();
                //objc = new cCommon();
                //string FileName = "";
                //FileName = gvMaterialInward.DataKeys[index].Values[1].ToString();
                //ViewState["FileName"] = FileName;
                //objc.ViewFileName(FileName, ViewState["EnquiryID"].ToString(), ifrm);
                hdnMIHID.Value = MIHID;
                objSt.GetMaterialInwardAttachementTypename(ddlTypeName);
                BindAttachements(MIHID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAttachementPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            if (e.CommandName.ToString() == "ViewDocs")
            {
                string BasehttpPath = StoresDocsHttpPath + hdnMIHID.Value + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, FileName, hdnMIHID.Value, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvaddeditems_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string Filename = gvaddeditems.DataKeys[index].Values[3].ToString();
            string MIDID = gvaddeditems.DataKeys[index].Values[2].ToString();
            objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, Filename, MIDID, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSupplierPOItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                CheckBox chkditems = (CheckBox)e.Row.FindControl("chkitems");

                if (Convert.ToDecimal(dr["AvailableQty"].ToString()) > 0)
                {
                    chkditems.Visible = true;
                    // btnSaveMIDetail.Visible = true;
                }
                else
                    chkditems.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMaterialInward()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.SUPID = Convert.ToInt32(ddlSuplierName.SelectedValue);
            objSt.LocationID = Convert.ToInt32(ddlLocationName.SelectedValue);

            ds = objSt.GetMaterialInwardBySUPIDAndLocationID();

            ViewState["MI"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialInward.DataSource = ds.Tables[0];
                gvMaterialInward.DataBind();
            }
            else
            {
                gvMaterialInward.DataSource = "";
                gvMaterialInward.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAddedMaterials()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.MIHID = Convert.ToInt32(hdnMIHID.Value);
            ds = objSt.GetAddedMaterials();
            btnSaveInward.Visible = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvaddeditems.DataSource = ds.Tables[0];
                gvaddeditems.DataBind();
                btnSaveInward.Visible = btnSaveMIDetail.Visible = ds.Tables[0].Rows[0]["BtnEnable"].ToString() != "1" ? true : false;
            }
            else
            {
                gvaddeditems.DataSource = "";
                gvaddeditems.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSupplierPOItemDetails()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.SPOID = Convert.ToInt32(ddlSPONumber.SelectedValue);
            objSt.MIHID = Convert.ToInt32(hdnMIHID.Value);
            ds = objSt.getSupplierPOItemDetailsBySPODIDs();
            //btnSaveMIDetail.Visible = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierPOItemDetails.DataSource = ds.Tables[0];
                gvSupplierPOItemDetails.DataBind();
                //btnSaveMIDetail.Enabled = ds.Tables[1].Rows[0]["BtnEnable"].ToString() != "1" ? true : false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "MakeMandatory('all');OpenTab('Item');", true);
            }
            else
            {
                gvSupplierPOItemDetails.DataSource = "";
                gvSupplierPOItemDetails.DataBind();
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

    public void BindAttachements(string MIHID)
    {
        objSt = new cStores();
        try
        {
            DataSet dsGetAttachementsDetails = new DataSet();

            objSt.MIHID = Convert.ToInt32(MIHID);

            dsGetAttachementsDetails = objSt.GetAttachementDetailsByRFPHID();

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
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvMaterialInward.Rows.Count > 0)
            gvMaterialInward.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}