using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_IMStock : System.Web.UI.Page
{
    #region"Declaration"

    cStores objSt;
    cCommon objc;
    cSession objSession = new cSession();
    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();
    cMaterials objMat;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        if (!IsPostBack)
        {
            ShowHideControls("view,addnew");
            bindIMStockDetails();
        }
        if (target == "ShareIM")
            UpdateIMSharedStatus(Convert.ToInt32(arg.ToString()));
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMaterialType_OnSelectIndexChanged(object sender, EventArgs e)
    {
        cMaterials objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialType.SelectedIndex > 0)
            {
                BindmaterialTypeFields("add", Convert.ToInt32(hdnSPODID.Value));
            }
            else divMTFields.InnerHtml = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMaterialCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
            objMat.GetMaterialGradeDetailsByCategoryID(ddlmaterialGrdae);
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
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            ShowHideControls("input");
            hdnSPODID.Value = "0";
            bindDropDownList();
            clearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveIMStock_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        objc = new cCommon();
        string Attachementname = "";
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            if (objc.Validate(divInput))
            {
                objSt.SPODID = Convert.ToInt32(hdnSPODID.Value);
                objSt.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
                objSt.MGMID = Convert.ToInt32(ddlmaterialGrdae.SelectedValue);
                objSt.THKID = Convert.ToInt32(ddlThickness.SelectedValue);
                objSt.CMID = Convert.ToInt32(ddlUOM.SelectedValue);
                objSt.MTID = Convert.ToInt32(ddlMaterialType.SelectedValue);
                objSt.RequiredWeight = txtInwardedQty.Text;
                objSt.ExpiryDate = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objSt.Remarks = txtRemarks.Text;
                objSt.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
                objSt.CustomerMaterial = rblCustomerMaterial.SelectedValue;

                if (chkQC.Checked == true)
                    objSt.OldMRNID = 0;
                else
                    objSt.OldMRNID = Convert.ToInt32(ddlOldMRNNumber.SelectedValue);

                objSt.MTFIDs = hdn_MTFIDS.Value;
                objSt.MTFIDsValue = hdn_MTFIDsValue.Value;

                if (fpAttachement.HasFile)
                {
                    objc.Foldername = StoresDocsSavePath;
                    Attachementname = Path.GetFileName(fpAttachement.PostedFile.FileName);
                    string MaximumAttacheID = objSt.GetMaximumAttachementID();
                    string[] extension = Attachementname.Split('.');
                    Attachementname = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                    objc.FileName = Attachementname;
                    objc.PID = "IMStock";
                    objc.AttachementControl = fpAttachement;
                    objc.SaveFiles();
                }
                objSt.AttachementName = Attachementname;
                objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
                objSt.MaterialRequestQC = chkQC.Checked == true ? 7 : 0;

                objSt.QtyInNumbers = txtQuantityInNumbers.Text == "" ? 0 : Convert.ToInt32(txtQuantityInNumbers.Text);
                objSt.DocumentName = txtDocumentname.Text;

                objSt.CompanyID = Convert.ToInt32(objSession.CompanyID);

                ds = objSt.SaveIMStockDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','IM Stock Saved Successfully');OpenTab('Item');dynamicvalueretain();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','IM Stock Updated Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                ShowHideControls("view,addnew");
                bindIMStockDetails();
                ddlMaterialCategory.SelectedIndex = 0;
                clearValues();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
        hdnSPODID.Value = "0";
    }

    protected void btnSaveStockAvailability_Click(object sender, EventArgs e)
    {
        DataTable dt;
        DataSet ds = new DataSet();
        objSt = new cStores();
        string Attachementname = "";
        try
        {
            if (Convert.ToDecimal(txtQty.Text) <= Convert.ToDecimal(hdnQCCheckedQty.Value) && Convert.ToDecimal(txtQty.Text) > 0)
            {
                objSt.StockQty = Convert.ToDecimal(txtQty.Text);
                objSt.UnitCost = Convert.ToDecimal(txtUnitCost.Text);

                // objSt.ReceiptDate = DateTime.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objSt.MRNID = Convert.ToInt32(hdnMRNID.Value);
                objSt.LocationID = Convert.ToInt32(ViewState["LocationID"].ToString());
                objSt.SPODID = Convert.ToInt32(hdnSPODID.Value);

                //if (fbStockUpload.HasFile)
                //{
                //    objc.Foldername = StoresDocsSavePath;
                //    Attachementname = Path.GetFileName(fpAttachement.PostedFile.FileName);
                //    string MaximumAttacheID = objSt.GetMaximumAttachementID();
                //    string[] extension = Attachementname.Split('.');
                //    Attachementname = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                //    objc.FileName = Attachementname;
                //    objc.PID = "IMStock";
                //    objc.AttachementControl = fpAttachement;
                //    objc.SaveFiles();
                //}

                objSt.AttachementName = Attachementname;
                ds = objSt.SaveStockAvailabilityDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Stock Qty Successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Stock Qty Updated Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Entered Qty " + (txtQty.Text) + " Should Not Great QCChecked Qty');", true);

            bindIMStockDetails();
            bindIMStockAvailabilityDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');HideAddPopUp();", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancelIMStock_Click(object sender, EventArgs e)
    {
        ShowHideControls("view,addnew");
        bindIMStockDetails();
        clearValues();
    }

    #endregion

    #region"GridView Events"

    protected void gvStockAvailabilty_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            if (e.CommandName.ToString() == "DeleteIM")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());

                objSt.SIID = Convert.ToInt32(gvStockAvailabilty.DataKeys[index].Values[0].ToString());

                ds = objSt.DeleteStockAvailabilityDetailsBySIID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Deleted');", true);
                bindIMStockAvailabilityDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStockAvailabilty_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dr["SIStatus"].ToString() == "1")
                {
                    btnDelete.Visible = false;
                    btnSaveStockAvailability.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                    btnSaveStockAvailability.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvIMStock_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string SPODID = gvIMStock.DataKeys[index].Values[0].ToString();
            hdnMRNID.Value = gvIMStock.DataKeys[index].Values[2].ToString();
            ViewState["LocationID"] = gvIMStock.DataKeys[index].Values[3].ToString();
            hdnSPODID.Value = SPODID;

            if (e.CommandName == "EditIMStock")
            {
                objSt.SPODID = Convert.ToInt32(gvIMStock.DataKeys[index].Values[0].ToString());
                ds = objSt.GetIMStockDetailsEdit();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Exists")
                {
                    hdnSPODID.Value = ds.Tables[0].Rows[0]["SPODID"].ToString();

                    bindDropDownList();
                    ddlMaterialCategory.SelectedValue = ds.Tables[0].Rows[0]["CID"].ToString();

                    ddlMaterialCategory_OnSelectedIndexChanged(null, null);

                    ddlmaterialGrdae.SelectedValue = ds.Tables[0].Rows[0]["MGMID"].ToString();
                    ddlThickness.SelectedValue = ds.Tables[0].Rows[0]["THKID"].ToString();

                    ddlMaterialType.SelectedValue = ds.Tables[0].Rows[0]["MaterialTypeId"].ToString();
                    if (ds.Tables[0].Rows[0]["MaterialTypeID"].ToString() != "0")
                        BindmaterialTypeFields("Edit", Convert.ToInt32(ds.Tables[0].Rows[0]["SPODID"].ToString()));

                    ddlUOM.SelectedValue = ds.Tables[0].Rows[0]["CMID"].ToString();
                    txtRemarks.Text = ds.Tables[0].Rows[0]["IM_Remarks"].ToString();
                    txtExpiryDate.Text = ds.Tables[0].Rows[0]["IM_ExpiryDateEdit"].ToString();
                    txtInwardedQty.Text = ds.Tables[0].Rows[0]["InwardedQty"].ToString();
                    chkQC.Checked = ds.Tables[0].Rows[0]["IM_MaterialRequestQC"].ToString() == "1" ? true : false;
                    rblCustomerMaterial.SelectedValue = ds.Tables[0].Rows[0]["IM_CustomerMaterial"].ToString();
                    ddlOldMRNNumber.SelectedValue = ds.Tables[0].Rows[0]["IM_OldMRNID"].ToString();
                    //IM_QtyInNumbers,IM_Documentname 
                    txtQuantityInNumbers.Text = ds.Tables[0].Rows[0]["IM_QtyInNumbers"].ToString();
                    txtDocumentname.Text = ds.Tables[0].Rows[0]["IM_Documentname"].ToString();
                    ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["IM_LocationID"].ToString();
                    //IM_LocationID

                    ShowHideControls("input");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "applicable", "OldMRNApplicable();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "applicable", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }

            if (e.CommandName == "AddStockAvailability")
            {
                // DataTable dtsock = new DataTable();
                bindIMStockAvailabilityDetails();
                // dtsock = (DataTable)ViewState["stock"];

                if (ViewState["QCStatus"].ToString() != "QCProcessing")
                {
                    Label lblUom = (Label)gvIMStock.Rows[index].FindControl("lblUOM");
                    Label lblMaterialRequestQC = (Label)gvIMStock.Rows[index].FindControl("lblMaterialRequestQC");
                    Label lblMRNNumber = (Label)gvIMStock.Rows[index].FindControl("lblMRNNumber");

                    Label lblCategoryName = (Label)gvIMStock.Rows[index].FindControl("lblCategoryName");
                    Label lblTypeName = (Label)gvIMStock.Rows[index].FindControl("lblTypeName");
                    Label lblGradeName = (Label)gvIMStock.Rows[index].FindControl("lblGradeName");

                    Label lblMaterialThickness = (Label)gvIMStock.Rows[index].FindControl("lblMaterialThickness");
                    Label lblMeasurement = (Label)gvIMStock.Rows[index].FindControl("lblMeasurement");
                    Label lblLocation = (Label)gvIMStock.Rows[index].FindControl("lblLocation");
                    Label lblCustomerMaterial = (Label)gvIMStock.Rows[index].FindControl("lblCustomerMaterial");


                    lblCategoryName_v.Text = lblCategoryName.Text;
                    lblType_v.Text = lblTypeName.Text;
                    lblGradeName_v.Text = lblGradeName.Text;
                    lblThickness_v.Text = lblMaterialThickness.Text;
                    lblMeasurment_v.Text = lblMeasurement.Text;
                    lblLocation_v.Text = lblLocation.Text;
                    lblUOM_v.Text = lblUom.Text;
                    lblCustomermaterial_v.Text = lblCustomerMaterial.Text;

                    lblMRNNumber_p.Text = lblMRNNumber.Text;
                    lblQtyAndUom.Text = "Qty" + " / " + lblUom.Text;

                    if (lblMaterialRequestQC.Text == "1")
                        lblQCCheckedQty.Text = "QC-Checked Qty:" + ViewState["QCClearedQty"].ToString();
                    else
                        lblQCCheckedQty.Text = "";
                    // hdnQCCheckedQty.Value = "NA";

                    txtQty.Text = ViewState["QCClearedQty"].ToString();

                    hdnQCCheckedQty.Value = ViewState["QCClearedQty"].ToString();

                    //if (dtsock.Rows.Count > 0)
                    //{
                    //    txtUnitCost.Text = dtsock.Rows[0]["IM_UnitCost"].ToString();
                    //    txtQuantityInNumbers.Text = dtsock.Rows[0]["IM_QtyInNumbers"].ToString();
                    //    //txtReceiptDate.Text = dtsock.Rows[0]["ReceiptDateEdit"].ToString();
                    //    txtDocumentname.Text = dtsock.Rows[0]["IM_DocumentName"].ToString();
                    //}
                    //else
                    //{
                    //    txtUnitCost.Text = "";
                    //    txtQuantityInNumbers.Text = "";
                    //    //txtReceiptDate.Text = "";
                    //}
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "applicable", "ShowAddPopUp();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "InfoMessage('Information','QC Processing');", true);
            }
            if (e.CommandName == "ViewAttachFile")
            {
                objc = new cCommon();
                string FileName = gvIMStock.DataKeys[index].Values[1].ToString();
                string BasehttpPath = StoresDocsHttpPath + "IMStock" + "/";
                ViewState["FileName"] = FileName;
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, FileName, "IMStock", null);
            }
            if (e.CommandName == "ViewMRNReport")
            {
                objc = new cCommon();
                string htmlfile = lblMRNNumber_p.Text + ".html";
                objc.ReadhtmlFile(htmlfile, hdnPdfContent);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvIMStock_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            //Label lblInwardStatus = (Label)e.Row.FindControl("lblStockInwardStatus");
            //Label lblMRNNumber = (Label)e.Row.FindControl("lblMRNNumber");
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnShare = (LinkButton)e.Row.FindControl("btnShare");
                LinkButton btnAdd = (LinkButton)e.Row.FindControl("btnAdd");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");

                if (dr["ShareIM"].ToString() == "1")
                {
                    btnShare.Visible = false;
                    if (objSession.ERPUsertype == 3)
                        btnAdd.Visible = true;
                    else
                        btnAdd.Visible = false;

                    btnEdit.Visible = objSession.type == 1 ? true : false;
                }
                else
                {
                    btnShare.Visible = true;
                    btnAdd.Visible = false;
                    btnEdit.Visible = true;
                }

                //lblInwardStatus.Text = "";
                //if (dr["Status"].ToString() == "Red")
                //    lblInwardStatus.Attributes.Add("style", "color:red;");

                //else if (dr["Status"].ToString() == "Green")
                //    lblInwardStatus.Attributes.Add("style", "color:Green;");

                //if (dr["QCProcessedStatus"].ToString() == "Red")
                //    lblMRNNumber.Attributes.Add("style", "color:Red;");
                //else if (dr["QCProcessedStatus"].ToString() == "Green")
                //    lblMRNNumber.Attributes.Add("style", "color:Green;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnShareInward_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            objSt.MRNID = Convert.ToInt32(hdnMRNID.Value);
            objSt.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objSt.UpdateInwardStatusByMRNID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','MRN Saved Stock SuccessFully');ShowAddPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAddPopUp();", true);

            bindIMStockAvailabilityDetails();
            bindIMStockDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Commmon Methods"

    private void bindIMStockDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            ds = objSt.GetIMStockDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvIMStock.DataSource = ds.Tables[0];
                gvIMStock.DataBind();
            }
            else
            {
                gvIMStock.DataSource = "";
                gvIMStock.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindIMStockAvailabilityDetails()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            btnSaveStockAvailability.Visible = true;

            objSt.MRNID = Convert.ToInt32(hdnMRNID.Value);
            ds = objSt.GetIMStockAvailabilityDetails();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvStockAvailabilty.DataSource = ds.Tables[1];
                gvStockAvailabilty.DataBind();
            }
            else
            {
                gvStockAvailabilty.DataSource = "";
                gvStockAvailabilty.DataBind();
            }

            //ViewState["stock"] = ds.Tables[0];
            ViewState["QCClearedQty"] = ds.Tables[0].Rows[0]["QCClearedQty"].ToString();
            ViewState["QCStatus"] = ds.Tables[0].Rows[0]["Message"].ToString();
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

    private void clearValues()
    {
        hdnSPODID.Value = "0";
        ddlMaterialCategory.SelectedIndex = 0;
        ddlmaterialGrdae.SelectedIndex = 0;
        ddlThickness.SelectedIndex = 0;
        ddlMaterialType.SelectedIndex = 0;
        txtExpiryDate.Text = "";
        ddlUOM.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        txtRemarks.Text = "";
        divMTFields.InnerHtml = "";
        txtDocumentname.Text = "";
        txtQuantityInNumbers.Text = "";
    }

    private void bindDropDownList()
    {
        DataSet ds = new DataSet();
        cMaterials objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            objMat.getMaterialTypeName(ddlMaterialType);
            objMat.GetMaterialClassificationNom(ddlUOM);
            objMat.GetMaterialCategoryNameDetails(ddlMaterialCategory);
            objMat.GetMaterialThicknessDetailsformaterialPlanning(ddlThickness);
            //objMat.GetMaterialGradeNameDetails(ddlmaterialGrdae);
            objc.GetLocationDetails(ddlLocation);
            objMat.GetOldMRNNumber(ddlOldMRNNumber);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindmaterialTypeFields(string Mode, int SPODID)
    {
        DataSet ds = new DataSet();
        cPurchase objPc = new cPurchase();
        try
        {
            ds = objPc.GetSupplierPOMaterialTypeFieldValuesBySPODID(ddlMaterialType.SelectedValue, Mode, SPODID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string dynamiccntrls = "<div class=\"col-sm-12 p-t-10\"><div class=\"col-sm-2\"></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label1replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt1replace\" type=\"text\" id=\"ContentPlaceHolder1_txt1replace\" onkeypress=\"return validationDecimal(this);\" Value='txtval1' class=\"form-control\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" onkeypress=\"return validationDecimal(this);\" type=\"text\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-2\"></div></div>";
                StringBuilder sbDivMTFields = new StringBuilder();
                int trowcnt = ds.Tables[0].Rows.Count;
                string partialdom = dynamiccntrls;
                string totoaldom = "";
                for (int trow = 0; trow < trowcnt; trow++)
                {
                    if (trow % 2 == 0)
                    {
                        partialdom = partialdom.Replace("label1replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt1replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval1", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval1", "");
                        if (trow == trowcnt - 1)
                        {
                            partialdom = partialdom.Replace("<div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" onkeypress=\"return validationDecimal(this);\" type=\"text\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div>", "");
                            sbDivMTFields.Append(partialdom);
                        }
                    }
                    else
                    {
                        partialdom = partialdom.Replace("label2replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt2replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval2", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval2", "");
                        sbDivMTFields.Append(partialdom);
                        partialdom = dynamiccntrls;
                    }
                }

                divMTFields.InnerHtml = sbDivMTFields.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateIMSharedStatus(int SPODID)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.SPODID = SPODID;
            ds = objSt.UpdateIMSharedStatusBySPODID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "SuccessMessage('Success','IM Shared Succesfully');HideAddPopUp();", true);
            bindIMStockDetails();
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
        if (gvIMStock.Rows.Count > 0)
            gvIMStock.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}




