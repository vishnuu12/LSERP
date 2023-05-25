using eplus.core;
using eplus.data;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_BOMMaterialSpecSheet : System.Web.UI.Page
{

    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                ddlEnquiryLoad();
                //objc = new cCommon();
                //DataSet dsEnquiryNumber = new DataSet();
                //DataSet dsCustomer = new DataSet();
                //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                //ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                //dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                //ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
                //chkAddtionalPart.Visible = false;
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    objMat = new cMaterials();

                    objMat.ETCID = Convert.ToInt32(arg.Split('/')[0].ToString());
                    objMat.BOMID = Convert.ToInt32(arg.Split('/')[1].ToString());

                    objMat.Flag = "Delete";
                    ds = objMat.GetBOMCostDetailsByETCID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Material Name Deleted Successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                    bindBOMCostDetails();
                }
                if (target == "UpdateBOMStatus")
                {
                    DataSet ds = new DataSet();
                    objMat = new cMaterials();

                    objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                    objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);

                    ds = objMat.UpdateBOMStatusByEDIDAndVersionNumber();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Item Cost Estimation Completed Successfully');", true);

                    bindBOMCostDetails();
                }
                if (target == "deleteAllBomPart")
                    DeleteAllBomPart();

                if (target == "viewpartlayout")
                    viewpartlayout(Convert.ToInt32(arg.ToString()));
            }
            if (ddlVersionNumber.SelectedIndex > 0)
                divDrawing.Visible = true;
            else
                divDrawing.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

    protected void rblEnquiryChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlEnquiryLoad();
        divOutput.Visible = false;
        chkAddtionalPart.Visible = false;
        chkIssuePart.Visible = false;
        divFields.Visible = false;
        objc.EmptyDropDownList(ddlItemName);
        objc.EmptyDropDownList(ddlMaterialName);
        objc.EmptyDropDownList(ddlVersionNumber);
    }

    //protected void rblItemDuplicate_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objMat = new cMaterials();
    //    try
    //    {
    //        if (rblItemDuplicate.SelectedValue == "ItemDup")
    //        {
    //            objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
    //            ds = objMat.GetCompletedBOMItemDetails(ddlDuplicateItemName);

    //            if (ds.Tables[0].Rows.Count > 0)
    //                divItemDuplicate.Visible = true;
    //            else
    //            {
    //                divItemDuplicate.Visible = false;
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','There Is No Duplicate Item In This Enquiry');", true);
    //            }
    //        }
    //        else if (rblItemDuplicate.SelectedValue == "EnquiryDup")
    //            divItemDuplicate.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"CheckBox Events"

    //protected void chkAddtionalPart_CheckedChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        ds = (DataSet)ViewState["AddtionalPartItemMaterials"];
    //        ddlMaterialName.DataSource = chkAddtionalPart.Checked == true ? ds.Tables[1] : ds.Tables[0];
    //        ddlMaterialName.DataTextField = "MaterialName";
    //        ddlMaterialName.DataValueField = "BOMID";
    //        ddlMaterialName.DataBind();
    //        ddlMaterialName.Items.Insert(0, new ListItem("--Select--", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        //objc = new cCommon();
        //try
        //{
        //    objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
        //    objc.EmptyDropDownList(ddlItemName);
        //    objc.EmptyDropDownList(ddlMaterialName);
        //    objc.EmptyDropDownList(ddlVersionNumber);
        //    divOutput.Visible = false;
        //    chkAddtionalPart.Visible = false;
        //    divFields.Visible = false;
        //}
        //catch (Exception ex)
        //{
        //    Log.Message(ex.ToString());
        //}

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

            objc.EmptyDropDownList(ddlVersionNumber);
            objc.EmptyDropDownList(ddlItemName);

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
        objMat = new cMaterials();
        DataTable dt;
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                //objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;

                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];

                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }

                objMat.GetItemDetailsByEnquiryNumber(ddlItemName);
                //  objMat.GetDrawingVersionNumberbyEnquiryNumber(ddlVersionNumber);
            }
            else
            {
                objc.EmptyDropDownList(ddlItemName);
            }
            objc.EmptyDropDownList(ddlVersionNumber);
            objc.EmptyDropDownList(ddlMaterialName);

            divOutput.Visible = false;
            chkAddtionalPart.Visible = false;
            chkIssuePart.Visible = false;
            divFields.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                objDesign.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                ds = objDesign.GetDrawingRevisionNumberByEnquiryDetailsID(ddlVersionNumber);
                //  objMat.GetDrawingVersionNumberbyEnquiryNumber(ddlVersionNumber);
                if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MaterialWarning"].ToString()))
                    lblMaterialWarning.Text = "Material Warning : " + ds.Tables[1].Rows[0]["MaterialWarning"].ToString();
                else
                    lblMaterialWarning.Text = "";
            }
            else
            {
                objc.EmptyDropDownList(ddlVersionNumber);
                lblMaterialWarning.Text = "";
            }
            //    divViewDrawing.Visible = false;
            // ShowHideControls("divadd");
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);
            objc.EmptyDropDownList(ddlMaterialName);
            divOutput.Visible = false;
            chkAddtionalPart.Visible = false;
            chkIssuePart.Visible = false;
            divFields.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    protected void ddlVersionNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            if (ddlVersionNumber.SelectedIndex > 0)
            {
                // rblItemDuplicate.Visible = true;
                divOutput.Visible = true;
                chkAddtionalPart.Visible = true;
                chkIssuePart.Visible = true;
                objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                objMat.VersionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
                //ds = objMat.GetDrawingMaterialDetailsByEDIDAndVersionNumber(ddlMaterialName);
                ds = objMat.GetMaterialList(ddlMaterialName);
                ViewState["FileName"] = ds.Tables[1].Rows[0]["FileName"].ToString();
                //  ViewState["AddtionalPartItemMaterials"] = ds;
                BindDrawingSequenceNumber("Add");
                bindBOMCostDetails();
            }
            else
            {
                ViewState["FileName"] = "";
                objc.EmptyDropDownList(ddlMaterialName);
                // rblItemDuplicate.Visible = false;
                divOutput.Visible = false;
                chkAddtionalPart.Visible = false;
                chkIssuePart.Visible = false;
            }
            divFields.Visible = false;
            chkAddtionalPart.Checked = false;
            chkIssuePart.Checked = false;
            //divItemDuplicate.Visible = false;        
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "hideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMaterialName_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialName.SelectedIndex > 0)
            {
                divFields.Visible = true;
                objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                //objMat.BOMID = Convert.ToInt32(ddlMaterialName.SelectedValue.Split('/')[0].ToString());
                objMat.BOMID = 0;

                ds = objMat.GetMaterialFormulaDetailsByMaterialID();
                bindInputAndFormulaFieldsDetails(ds, ds.Tables[2].Rows[0]["Mode"].ToString().ToLower());

                //hdnBomID.Value = ddlMaterialName.SelectedValue.Split('/')[0];
                hdnBomID.Value = "0";
                hdnMID.Value = ddlMaterialName.SelectedValue;
                BindDrawingSequenceNumber("Add");
            }
            else
                divFields.Visible = false;

            lblPartName_Edit.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnviewcosting_Click(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objDesign.VersionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);

            ds = objDesign.GetDDIDByEDIDAndVersionNo();

            string DDID = ds.Tables[0].Rows[0]["DDID"].ToString();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "ViewCostingSheet.aspx?DDID=" + DDID + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            bindBOMCostDetails();
            //   ds = objMat.GetDrawingMaterialDetailsByEDIDAndVersionNumber(ddlMaterialName);
            //BindSavedDetailsbyBOMID();

            btnCancel_Click(null, null);
            lblPartName_Edit.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnEstimationCompleted_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);
            //ds = objMat.UpdateBOMStatusByEDIDAndVersionNumber();
            objMat.EmpID = Convert.ToInt32(objSession.employeeid);
            ds = objMat.LS_CheckItemBOMCostOfPendindMaterial();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Cost Estimated Successfully')", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "')", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlVersionNumber.Enabled = ddlMaterialName.Enabled = chkAddtionalPart.Enabled = true;
        ddlMaterialName.SelectedIndex = 0;
        divFields.Visible = false;
        lblPartName_Edit.Text = "";
    }

    protected void imgViewDrawing_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
            string FileName = BasehttpPath + ViewState["FileName"].ToString();

            ifrm.Attributes.Add("src", FileName);
            //if (File.Exists(DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + '/' + ViewState["FileName"].ToString()))
            //{
            objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, ViewState["FileName"].ToString(), ddlEnquiryNumber.SelectedValue, ifrm);
            //   }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
            // else
            //  {

            //  }
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddLayout_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            lblItemName_Layout.Text = ddlItemName.SelectedItem.Text + "/" + ddlVersionNumber.SelectedItem.Text;

            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.VersionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            ds = objMat.GetDrawingMaterialDetailsByEDIDAndVersionNumber(ddlBomPartName);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartLayoutDetails.DataSource = ds.Tables[0];
                gvPartLayoutDetails.DataBind();
            }
            else
            {
                gvPartLayoutDetails.DataSource = "";
                gvPartLayoutDetails.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "loader", "ShowLayoutAttachementsPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void btnDuplicate_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objMat = new cMaterials();
    //    try
    //    {
    //        objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
    //        objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);
    //        objMat.DDID = Convert.ToInt32(ddlDuplicateItemName.SelectedValue);
    //        //objMat.DuplicateVersionNumber = Convert.ToInt32(ddlDuplicateRevisionNumber.SelectedValue);

    //        ds = objMat.SaveDuplicateBOMDetails();

    //        if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','BOM Duplicate Item Saved Successfully');", true);
    //        bindBOMCostDetails();
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void btnSaveLayOutAttachement_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        string FileName = null;
        try
        {
            if (fbLayoutAttach_p.HasFile)
            {
                cSales ojSales = new cSales();
                objc = new cCommon();
                objc.Foldername = DrawingDocumentSavePath;
                FileName = Path.GetFileName(fbLayoutAttach_p.PostedFile.FileName);
                string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                string[] extension = FileName.Split('.');
                FileName = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                objc.FileName = FileName;
                objc.PID = ddlEnquiryNumber.SelectedValue;
                objc.AttachementControl = fbLayoutAttach_p;
                objc.SaveFiles();
            }
            objMat.LayoutAttachement = FileName;
            objMat.Remarks = txtProductionpartRemarks.Text;
            objMat.BOMID = Convert.ToInt32(ddlBomPartName.SelectedValue.Split('/')[0].ToString());
            ds = objMat.SavePartLayOutAttachements();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Layout Attachements Saved Successfully');", true);
            btnAddLayout_Click(null, null);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ddlEnquiryLoad()
    {
        objc = new cCommon();
        try
        {
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();

            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByBOMMaterialSpecSheet", rblEnquiryChange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];
            dsEnquiryNumber = objc.GetEnquiryNumberByPendingList(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByBOMMaterialSpecSheet", rblEnquiryChange.SelectedValue);

            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    private void viewpartlayout(int index)
    {
        objc = new cCommon();
        try
        {
            string LayoutFileName = gvBOMCostDetails.DataKeys[index].Values[4].ToString();
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
            string FileName = BasehttpPath + LayoutFileName;

            ViewState["FileName"] = LayoutFileName;
            ifrm.Attributes.Add("src", FileName);
            //if (File.Exists(DrawingDocumentSavePath + LayoutFileName))
            //{
            //    ViewState["ifrmsrc"] = BasehttpPath + LayoutFileName;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
            //}
            //else
            //{
            //    ViewState["ifrmsrc"] = "";
            //    ifrm.Attributes.Add("src", "");
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            //}
            objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, LayoutFileName, ddlEnquiryNumber.SelectedValue, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSavedDetailsbyBOMID()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            objMat.BOMID = Convert.ToInt32(hdnBomID.Value);
            ds = objMat.GetMaterialFormulaDetailsByMaterialID();
            bindInputAndFormulaFieldsDetails(ds, ds.Tables[2].Rows[0]["Mode"].ToString().ToLower());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindInputAndFormulaFieldsDetails(DataSet ds, string Mode)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();

        int cellcount = 0;
        int rowindex = 0;
        try
        {
            if (Mode == "edit")
            {
                txtRemarks.Text = ds.Tables[4].Rows[0]["BomPartRemarks"].ToString();

                if (ds.Tables[4].Rows[0]["AddtionalPart"].ToString() == "No")
                {
                    chkAddtionalPart.Checked = false;
                    chkIssuePart.Checked = false;
                }
                else if (ds.Tables[4].Rows[0]["AddtionalPart"].ToString() == "Issue")
                {
                    chkAddtionalPart.Checked = false;
                    chkIssuePart.Checked = true;
                }
                else
                {
                    chkAddtionalPart.Checked = true;
                    chkIssuePart.Checked = false;
                }
                ddlDrawingSequenceNumber.SelectedValue = ds.Tables[4].Rows[0]["DrawingSequenceNumber"].ToString();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                row = new HtmlTableRow();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cellcount++;
                    cell = new HtmlTableCell();

                    Label lblInput = new Label();
                    lblInput.ID = "lbl" + "InputField" + "_" + rowindex + "_" + cellcount;
                    lblInput.Text = ds.Tables[0].Rows[i]["Name"].ToString();
                    if (ds.Tables[0].Rows[i]["Name"].ToString() == "M/CRATE")
                        lblInput.CssClass = "form-label";
                    else
                        lblInput.CssClass = "form-label mandatorylbl";
                    cell.Controls.Add(lblInput);
                    row.Cells.Add(cell);

                    Label lblMSMID = new Label();
                    lblMSMID.ID = "lblMSMID" + "_" + rowindex + "_" + cellcount;
                    lblMSMID.Text = ds.Tables[0].Rows[i]["MSMID"].ToString();
                    lblMSMID.CssClass = "lbl" + "_" + ds.Tables[0].Rows[i]["Name"].ToString();
                    lblMSMID.Attributes.Add("style", "display:none;");
                    cell.Attributes.Add("style", "padding-right:20px;padding-top:30px;");
                    cell.Controls.Add(lblMSMID);
                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();
                    if (ds.Tables[0].Rows[i]["Name"].ToString() == "MOC")
                    {

                        DropDownList ddlMOC = new DropDownList();
                        ddlMOC.ID = "ddlMOC";
                        ddlMOC.DataSource = ds.Tables[3];
                        ddlMOC.DataTextField = "GradeName";
                        ddlMOC.DataValueField = "MGMID";
                        ddlMOC.DataBind();
                        ddlMOC.CssClass = "form-control ddlMOC";
                        ddlMOC.Items.Insert(0, new ListItem("--Select--", "0"));
                        if (Mode == "edit")
                        {
                            string MOCMSMIDValue = ds.Tables[0].Rows[i]["MSMIDValue"].ToString().Split('.')[0];

                            for (int k = 0; k < ds.Tables[3].Rows.Count; k++)
                            {
                                if (MOCMSMIDValue == (ds.Tables[3].Rows[k]["MGMID"].ToString()).Split('/')[0].ToString())
                                {
                                    ddlMOC.SelectedValue = ds.Tables[3].Rows[k]["MGMID"].ToString();
                                    break;
                                }
                            }
                        }
                        ddlMOC.Attributes.Add("OnChange", "return MOCChanged(this);");
                        cell.Attributes.Add("style", "padding-top:30px;");
                        cell.Controls.Add(ddlMOC);
                    }
                    else
                    {
                        TextBox txtInput = new TextBox();
                        //  txtInput.ID = "txt" + "InputField" + "_" + rowindex + "_" + cellcount;
                        txtInput.ID = ds.Tables[0].Rows[i]["Name"].ToString();

                        if (Mode == "edit")
                            txtInput.Text = ds.Tables[0].Rows[i]["MSMIDValue"].ToString();

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "QTY")
                        {
                            if (ds.Tables[4].Rows.Count > 0)
                                txtInput.Text = ds.Tables[4].Rows[0]["PartQty"].ToString();
                            else
                                txtInput.Text = "";
                            // txtInput.Enabled = false;
                            txtInput.ToolTip = "Part Quantity";
                        }

                        txtInput.CssClass = "form-control" + " " + ds.Tables[0].Rows[i]["Name"].ToString();
                        txtInput.Attributes.Add("style", "width:100%;");
                        txtInput.Attributes.Add("autocomplete", "off");
                        txtInput.Attributes.Add("onkeypress", "return fnAllowNumeric();");
                        cell.Attributes.Add("style", "padding-top:30px;");
                        cell.Controls.Add(txtInput);

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "MRATE")
                        {
                            Label lblMrate = new Label();
                            lblMrate.ID = "lblMrate";
                            lblMrate.CssClass = "SuggestedMrateCost";
                            lblMrate.Attributes.Add("style", " padding-right: 50px;  color: black; font-weight: bold;");
                            cell.Controls.Add(lblMrate);
                        }
                    }
                    row.Cells.Add(cell);

                    if (cellcount == 3)
                    {
                        cellcount = 0;
                        rowindex++;
                        row.Attributes.Add("style", "padding-top:10px;");
                        tblInputFields.Rows.Add(row);
                        row = new HtmlTableRow();
                    }
                }

                if (cellcount != 0)
                    tblInputFields.Rows.Add(row);

            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                cellcount = 0;
                rowindex = 0;
                row = new HtmlTableRow();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    cellcount++;
                    cell = new HtmlTableCell();

                    Label lblInput = new Label();
                    lblInput.ID = "lbl" + "_" + ds.Tables[1].Rows[i]["MSMID"].ToString();
                    lblInput.Text = ds.Tables[1].Rows[i]["Name"].ToString();
                    lblInput.CssClass = "form-label";
                    cell.Controls.Add(lblInput);


                    TextBox txtFormula = new TextBox();
                    //    txtFormula.ID = "txtFormula" + "_" + rowindex + "_" + cellcount + "_" + ds.Tables[1].Rows[i]["Name"].ToString();
                    txtFormula.ID = "txtFormula" + "_" + ds.Tables[1].Rows[i]["Name"].ToString().Replace("SQUARE", "sqrt");
                    txtFormula.Text = ds.Tables[1].Rows[i]["Formula"].ToString();
                    txtFormula.Attributes.Add("style", "display:none;");
                    //cell.Attributes.Add("style", "padding-right:20px;padding-top:30px;");
                    cell.Controls.Add(txtFormula);

                    Label lblFormulaName = new Label();
                    lblFormulaName.ID = "lblFormulaName" + "_" + ds.Tables[1].Rows[i]["MSMID"].ToString();
                    lblFormulaName.Text = ds.Tables[1].Rows[i]["Formula"].ToString();
                    lblFormulaName.CssClass = "";
                    lblFormulaName.Attributes.Add("style", "display:block;color:white;");
                    cell.Controls.Add(lblFormulaName);

                    row.Cells.Add(cell);
                    //  cell.Attributes.Add("style", "padding-right:20px;");
                    // row.Cells.Add(cell);

                    cell = new HtmlTableCell();

                    Label lblFormulaValue = new Label();
                    lblFormulaValue.ID = ds.Tables[1].Rows[i]["Name"].ToString();

                    if (Mode == "edit")
                        lblFormulaValue.Text = ds.Tables[1].Rows[i]["MSMIDValue"].ToString();

                    //    lblFormulaValue.Attributes.Add("style", "width:50%;");
                    lblFormulaValue.CssClass = "lblvalue";
                    cell.Attributes.Add("style", "padding-left:30px;padding-bottom:22px;");
                    cell.Controls.Add(lblFormulaValue);
                    row.Cells.Add(cell);

                    if (cellcount == 3)
                    {
                        cellcount = 0;
                        rowindex++;
                        row.Attributes.Add("style", "padding-top:10px;");
                        tblFormulaFields.Rows.Add(row);
                        row = new HtmlTableRow();
                    }
                }

                if (cellcount != 0)
                    tblFormulaFields.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindBOMCostDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        DataView dv;
        DataTable dt;
        try
        {
            ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlVersionNumber.Enabled = ddlMaterialName.Enabled = chkAddtionalPart.Enabled = true;

            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);

            ds = objMat.GetBOMCostDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
            {
                //ds.Tables[1].Columns.Remove("MOC");
                //dv = new DataView(ds.Tables[1]);
                //dv.RowFilter = "CONVERT(Isnull(FIM, ''), System.String) = ''";
                //dv.RowFilter = "AddtionalPart='No'";
                //dv.RowFilter = "CONVERT(Isnull(FIM, ''), System.String) = ''";
                //dt = dv.ToTable();

                //gvBOMCostDetails.DataSource = dt;
                //gvBOMCostDetails.DataBind();

                //dv = new DataView(ds.Tables[1]);
                //dv.RowFilter = "AddtionalPart='Yes'";
                //dt = dv.ToTable();

                //gvAddtionalPartDetails.DataSource = dt;
                //gvAddtionalPartDetails.DataBind();

                //dv = new DataView(ds.Tables[1]);
                //dv.RowFilter = "FIM='Issue'";
                //dt = dv.ToTable();

                ds.Tables[1].Columns.Remove("MOC");
                dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "AddtionalPart='No'";
                dt = dv.ToTable();

                if (dt.Columns.Contains("MCRate"))
                    dt.Columns["MCRate"].ColumnName = "M/C RATE";

                gvBOMCostDetails.DataSource = dt;
                gvBOMCostDetails.DataBind();

                dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "AddtionalPart='Yes'";
                dt = dv.ToTable();

                if (dt.Columns.Contains("MCRate"))
                    dt.Columns["MCRate"].ColumnName = "M/C RATE";

                gvAddtionalPartDetails.DataSource = dt;
                gvAddtionalPartDetails.DataBind();

                dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "AddtionalPart='Issue'";
                dt = dv.ToTable();


                if (dt.Columns.Contains("MCRate"))
                    dt.Columns["MCRate"].ColumnName = "M/C RATE";

                gvIssuePart.DataSource = dt;
                gvIssuePart.DataBind();

                //if (ds.Tables[1].Rows[0]["BOMStatus"].ToString() == "Completed")
                //{
                //    // btnSave.Enabled = false;
                //    btnSave.CssClass = "";
                //    btnSave.ToolTip = "This Item Moved Into Next Stage so! You Cant Save";
                //}
                //else
                //{
                //    btnSave.Enabled = true;
                //    btnSave.ToolTip = "";
                //}

                lblAddtionalBOMTotalCost.Text = ds.Tables[2].Rows[0]["AdditionalPartBomCost"].ToString();
                lblItemBomTotalCost.Text = ds.Tables[3].Rows[0]["ItemBOMCost"].ToString();
                lblIssueBomTotalCost.Text = ds.Tables[4].Rows[0]["IssueBOMCost"].ToString();

                if (ds.Tables[5].Rows.Count > 0)
                    lblCost.Text = ds.Tables[5].Rows[0]["TotalBOMCost"].ToString();

                lblDrawingNumber.Text = " ( " + ds.Tables[6].Rows[0]["DrawingNumber"].ToString() + " ) ";
                lblItemQty.Text = " Qty: " + ds.Tables[6].Rows[0]["Quantity"].ToString() + "";

                // gvBOMCostDetails.UseAccessibleHeader = true;
                // gvBOMCostDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvBOMCostDetails.DataSource = "";
                gvBOMCostDetails.DataBind();

                gvAddtionalPartDetails.DataSource = "";
                gvAddtionalPartDetails.DataBind();

                lblItemBomTotalCost.Text = lblAddtionalBOMTotalCost.Text = lblCost.Text = "0.00";

                lblDrawingNumber.Text = " ( " + ds.Tables[1].Rows[0]["DrawingNumber"].ToString() + " ) ";
                lblItemQty.Text = "  Qty: " + ds.Tables[1].Rows[0]["Quantity"].ToString() + "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDrawingSequenceNumber(string Mode)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        DataTable dt = new DataTable();
        int j = 1;
        try
        {
            ddlDrawingSequenceNumber.Items.Clear();

            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            objMat.BOMID = Convert.ToInt32(hdnBomID.Value);
            objMat.Flag = Mode;
            ds = objMat.GetDrawingSequencenumberByEDIDAndVersionnumber();

            ddlDrawingSequenceNumber.DataSource = ds.Tables[0];
            ddlDrawingSequenceNumber.DataTextField = "DrawingSequenceNumber";
            ddlDrawingSequenceNumber.DataValueField = "DrawingSequenceNumber";
            ddlDrawingSequenceNumber.DataBind();
            ddlDrawingSequenceNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDrawingSequenceNumber.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteAllBomPart()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);

            ds = objMat.DeleteBomiDetailsByEDIDAndVersionNumber();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "SuccessMessage('Succes','BOM All Part Deleted successFully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "InfoMessage('Infprmation','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            bindBOMCostDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvBOMCostDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[10].Visible = false;

                e.Row.Cells[6].Text = "Part Layout";
                e.Row.Cells[7].Text = "Part No";
                e.Row.Cells[8].Text = "Part Name";
                e.Row.Cells[9].Text = "Part Remarks";
                e.Row.Cells[11].Text = "Add Part";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnedit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                LinkButton btnPartLayOut = (LinkButton)e.Row.FindControl("btnPartLayOut");

                if (dr["BOMStatus"].ToString() == "Completed")
                {
                    //btnEdit.Attributes.Add("Class", "aspNetDisabled");
                    //btnEdit.ToolTip = "Enquiry Moved to Next Stage.";

                    //btnDelete.ToolTip = "Enquiry Moved to Next Stage.";
                    //btnDelete.Attributes.Add("Class", "aspNetDisabled");

                    //btnSave.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    //btnSave.ToolTip = "Enquiry Moved to Next Stage.";

                    //btnEstimationCompleted.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    //btnSave.ToolTip = "Enquiry Moved to Next Stage.";

                    if (objSession.type == 1)
                    {
                        btnDelete.Visible = true;
                        btnEdit.Visible = true;
                        btnEstimationCompleted.Visible = true;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                        btnEdit.Visible = false;
                        btnEstimationCompleted.Visible = false;
                        btnSave.Visible = false;
					}

                    if (string.IsNullOrEmpty(dr["LayOutFile"].ToString()))
                        btnPartLayOut.Visible = false;
                    else
                        btnPartLayOut.Visible = true;

                    lbtnDeleteAll.Visible = false;
                }
                else
                {
                    //btnEdit.CssClass.Replace("aspNetDisabled", "");
                    //btnSave.CssClass.Replace("aspNetDisabled", "");
                    //btnDelete.CssClass.Replace("aspNetDisabled", "");
                    //btnEstimationCompleted.CssClass.Replace("aspNetDisabled", "");
                    btnDelete.Visible = true;
                    btnSave.Visible = true;
                    btnEstimationCompleted.Visible = true;

                    lbtnDeleteAll.Visible = true;
                }

                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[10].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvBOMCostDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string GvID = ((GridView)sender).ID;

            string PartName = "";
            string LayoutFileName = "";
            if (GvID == "gvBOMCostDetails")
            {
                chkAddtionalPart.Checked = false;
                chkIssuePart.Checked = false;
                // objMat.MID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[1].ToString());

                hdnBomID.Value = gvBOMCostDetails.DataKeys[index].Values[2].ToString();
                hdnMID.Value = gvBOMCostDetails.DataKeys[index].Values[1].ToString();

                objMat.BOMID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[2].ToString());
                objMat.ETCID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[0].ToString());
                PartName = gvBOMCostDetails.DataKeys[index].Values[3].ToString();
                LayoutFileName = gvBOMCostDetails.DataKeys[index].Values[4].ToString();
            }
            else if (GvID == "gvIssuePart")
            {
               // chkAddtionalPart.Checked = false;
                chkIssuePart.Checked = true;
                // objMat.MID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[1].ToString());

                hdnBomID.Value = gvIssuePart.DataKeys[index].Values[2].ToString();
                hdnMID.Value = gvIssuePart.DataKeys[index].Values[1].ToString();

                objMat.BOMID = Convert.ToInt32(gvIssuePart.DataKeys[index].Values[2].ToString());
                objMat.ETCID = Convert.ToInt32(gvIssuePart.DataKeys[index].Values[0].ToString());
                PartName = gvIssuePart.DataKeys[index].Values[3].ToString();
                LayoutFileName = gvIssuePart.DataKeys[index].Values[4].ToString();
            }
            else if (GvID == "gvAddtionalPartDetails")
            {
                chkAddtionalPart.Checked = true;
                //chkIssuePart.Checked = true;
                //  objMat.MID = Convert.ToInt32(gvAddtionalPartDetails.DataKeys[index].Values[1].ToString());

                hdnBomID.Value = gvAddtionalPartDetails.DataKeys[index].Values[2].ToString();
                hdnMID.Value = gvAddtionalPartDetails.DataKeys[index].Values[1].ToString();

                objMat.BOMID = Convert.ToInt32(gvAddtionalPartDetails.DataKeys[index].Values[2].ToString());
                objMat.ETCID = Convert.ToInt32(gvAddtionalPartDetails.DataKeys[index].Values[0].ToString());
                PartName = gvAddtionalPartDetails.DataKeys[index].Values[3].ToString();
                LayoutFileName = gvAddtionalPartDetails.DataKeys[index].Values[4].ToString();
            }

            lblPartName_Edit.Text = PartName.ToString();

            // chkAddtionalPart_CheckedChanged(null, null);

            if (e.CommandName.ToString() == "EditBOM")
            {
                tblInputFields.Rows.Clear();
                tblFormulaFields.Rows.Clear();
                divFields.Visible = true;
                objMat.Flag = "Edit";
                //if (GvID == "gvBOMCostDetails")
                //    ddlMaterialName.SelectedValue = gvBOMCostDetails.DataKeys[index].Values[2].ToString() + '/' + gvBOMCostDetails.DataKeys[index].Values[1].ToString();
                //else
                //    ddlMaterialName.SelectedValue = gvAddtionalPartDetails.DataKeys[index].Values[2].ToString() + '/' + gvAddtionalPartDetails.DataKeys[index].Values[1].ToString();
                ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlVersionNumber.Enabled = ddlMaterialName.Enabled = false;
                ds = objMat.GetBOMCostDetailsByETCID();
                BindDrawingSequenceNumber("Edit");
                bindInputAndFormulaFieldsDetails(ds, "edit");
            }
            if (e.CommandName.ToString() == "ViewLayOut")
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartLayoutDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        int BOMID;
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "ViewLayout")
            {
                objc = new cCommon();
                string FileName = "";
                FileName = gvPartLayoutDetails.DataKeys[index].Values[1].ToString();
                ViewState["FileName"] = FileName;
                ViewState["ifrmsrc"] = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, FileName, ddlEnquiryNumber.SelectedValue, ifrm);
            }
            if (e.CommandName == "EditLayout")
            {
                Label lblRemarks = (Label)gvPartLayoutDetails.Rows[index].FindControl("lblRemarks");
                string BomID = gvPartLayoutDetails.DataKeys[index].Values[0].ToString();
                string Remarks = lblRemarks.Text;
                ddlBomPartName.SelectedValue = BomID;
                txtProductionpartRemarks.Text = Remarks;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "loader", "ShowLayoutAttachementsPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Web Methods"
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string SaveBOMDetails(String[] strBOMDetails, String[] StrDetails, string BOMRemarks)
    {
        DataSet ds = new DataSet();
        string EDID;
        string VersionNumber;
        string MID;
        string MSMID = "";
        string MSMIDVal = "";
        string ReturnMsg = "";
        string BOMID = "";
        string Remarks = "";
        string DrawingSequenceNumber = "";
        string AddtionalPart = "";
        //string IssuePart = "";
        try
        {
            EDID = StrDetails[0].Split('/')[0];
            VersionNumber = StrDetails[0].Split('/')[1];
            MID = StrDetails[0].Split('/')[2];
            BOMID = StrDetails[0].Split('/')[3];
            // Remarks = StrDetails[0].Split('/')[4];
            Remarks = BOMRemarks;
            DrawingSequenceNumber = StrDetails[0].Split('/')[5];
            AddtionalPart = StrDetails[0].Split('/')[6];
            //IssuePart = StrDetails[0].Split('/')[7];

            for (int i = 0; i < strBOMDetails.Length; i++)
            {
                if (MSMID != "")
                    MSMID = MSMID + "," + strBOMDetails[i].Split(':')[0].ToString();
                else
                    MSMID = strBOMDetails[i].Split(':')[0].ToString();


                if (MSMIDVal != "")
                    MSMIDVal = MSMIDVal + "," + strBOMDetails[i].Split(':')[1].ToString();
                else
                    MSMIDVal = strBOMDetails[i].Split(':')[1].ToString();
            }

            cDataAccess DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveBOMSheetDetails";
            c.Parameters.Add("@EDID", EDID);
            c.Parameters.Add("@VersionNumber", VersionNumber);

            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);

            c.Parameters.Add("@MSMID", MSMID);
            c.Parameters.Add("@MSMIDVal", MSMIDVal);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@DrawingSequenceNumber", DrawingSequenceNumber);
            c.Parameters.Add("@AddtionalPart", AddtionalPart);
            //c.Parameters.Add("@IssuePart", IssuePart);

            DAL.GetDataset(c, ref ds);
            ReturnMsg = ds.Tables[0].Rows[0]["Message"].ToString();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ReturnMsg;
    }

    #endregion

    #region"PageLoad_Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvBOMCostDetails.Rows.Count > 0)
            gvBOMCostDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvAddtionalPartDetails.Rows.Count > 0)
            gvAddtionalPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvPartLayoutDetails.Rows.Count > 0)
            gvPartLayoutDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}