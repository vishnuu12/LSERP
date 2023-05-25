using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using eplus.core;
using System.IO;


public partial class Pages_DrawingBOM : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cMaterials objMat = new cMaterials();
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    cCommon objc;
    cDesign objDesign;

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
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        try
        {
            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];

                ShowHideControls("add");
                // objMat.GetEnquiryNumber(ddlEnquiryNumber);
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    int BOMID = Convert.ToInt32(arg);
                    ds = objMat.DeleteBOMDetailsByBOMID(BOMID);
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    {
                        gvDrawingBOMDetails.UseAccessibleHeader = true;
                        gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','BOM Deleted successfully');", true);
                    }
                    BindBOMDetails();
                    ShowHideControls("drawing");
                    //  ShowHideControls("View");
                }
                if (target == "deleteAllBomPart")
                    DeleteAllBomPart();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            ShowHideControls("add");
            lblDrawingNumber.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                objMat.GetItemDetailsByEnquiryNumber(ddlItemName);
                //  objMat.GetDrawingVersionNumberbyEnquiryNumber(ddlVersionNumber);
            }
            else
            {
                ddlItemName.DataSource = "";
                ddlItemName.DataBind();
                ddlItemName.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlVersionNumber.DataSource = "";
                ddlVersionNumber.DataBind();
                ddlVersionNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);
            ShowHideControls("add");
            lblDrawingNumber.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                objDesign.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                //objDesign.GetDrawingRevisionNumberByEnquiryDetailsID(ddlVersionNumber);
                //  objMat.GetDrawingVersionNumberbyEnquiryNumber(ddlVersionNumber);
                ds = objDesign.GetRevisionNumberAndAttachementNameByEDID(ddlVersionNumber);

                if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["MaterialWarning"].ToString()))
                    lblMaterialWarning.Text = "Material Warning : " + ds.Tables[1].Rows[0]["MaterialWarning"].ToString();
                else
                    lblMaterialWarning.Text = "";

            }
            else
            {
                ddlVersionNumber.DataSource = "";
                ddlVersionNumber.DataBind();
                ddlVersionNumber.Items.Insert(0, new ListItem("--Select--", "0"));

                lblMaterialWarning.Text = "";
            }

            // divViewDrawing.Visible = false;
            // ShowHideControls("divadd");
            ShowHideControls("add");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);

            lblDrawingNumber.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    protected void ddlVersionNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVersionNumber.SelectedIndex > 0)
            {
                // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";

                string FileName = BasehttpPath + ddlVersionNumber.SelectedValue;

                ViewState["FileName"] = ddlVersionNumber.SelectedValue;

                //   byte[] imageBytes = System.IO.File.ReadAllBytes(FileName);
                //   string base64String = Convert.ToBase64String(imageBytes);
                //  imgViewDrawing.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                ifrm.Attributes.Add("src", BasehttpPath + ddlVersionNumber.SelectedValue);
                if (File.Exists(DrawingDocumentSavePath + ddlVersionNumber.SelectedValue))
                {
                    imgViewDrawing.ImageUrl = FileName;
                }
                else
                {
                    //  ViewState["ifrmsrc"] = "";
                    //  ifrm.Attributes.Add("src", "");
                    imgViewDrawing.ImageUrl = "../Assets/images/NoImage.png";
                }

                imgViewDrawing.ToolTip = ddlVersionNumber.SelectedValue;
                // divViewDrawing.Visible = true;

                BindBOMDetails();
                ShowHideControls("drawing");
                //  ShowHideControls("view");
            }
            else
            {
                // divViewDrawing.Visible = false;
                //  ShowHideControls("divadd");
                ShowHideControls("add");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "hideLoader();", true);
            }

            if (gvDrawingBOMDetails.Rows.Count > 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "hideLoader();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "hideLoader();", true);

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
        cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            ds = objMat.GetItemBOMStatusByDDID();

            hdnBOMId.Value = "0";
            objMat.GetMaterialList(ddlMaterialName);
            BindDrawingSequenceNumber();
            ShowHideControls("input");
            if (ds.Tables[0].Rows[0]["BOMStatus"].ToString() == "Completed")
            {
                btnAddNew.ToolTip = "This Item Moved Into Next Stage";
                divInput.Visible = false;
            }
            //else
            //{
            //    btnAddNew.ToolTip = "";
            //    divInput.Visible = true;
            //}

            gvDrawingBOMDetails.UseAccessibleHeader = true;
            gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        string FileName = null;
        try
        {
            if (ValidateControl())
            {
                objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                objMat.Quantity = Convert.ToInt32(txtQuantity.Text);
                objMat.BOMID = Convert.ToInt32(hdnBOMId.Value);
                objMat.DrawingSequenceNumber = Convert.ToInt32(ddlDrawingSequenceNumber.SelectedValue);

                objMat.Addtionalpart = chkAddtionalPart.Checked == true ? "Yes" : "No";

                if (fAttachment.HasFile)
                {
                    cSales ojSales = new cSales();
                    objc = new cCommon();
                    objc.Foldername = DrawingDocumentSavePath;
                    FileName = Path.GetFileName(fAttachment.PostedFile.FileName);
                    string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                    string[] extension = FileName.Split('.');
                    FileName = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                    objc.FileName = FileName;
                    objc.PID = ddlEnquiryNumber.SelectedValue;
                    objc.AttachementControl = fAttachment;
                    objc.SaveFiles();
                }

                objMat.LayoutAttachement = FileName;

                objMat.Remarks = txtRemarks.Text;

                ds = objMat.SaveDrawingBOMMaterialDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Saved Successfully');hideLoader();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Meterial Name Already Exists In This Enquiry Number');hideLoader();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Updated Successfully');hideLoader();", true);

                BindBOMDetails();
                BindDrawingSequenceNumber();
                ShowHideControls("input");
                ClearValues();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void imgViewDrawing_Click(object sender, EventArgs e)
    {
        try
        {
            // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
            string FileName = BasehttpPath + ddlVersionNumber.SelectedValue;

            ViewState["FileName"] = ddlVersionNumber.SelectedValue;
            ifrm.Attributes.Add("src", FileName);
            if (File.Exists(DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + '/' + ddlVersionNumber.SelectedValue))
            {
                ViewState["ifrmsrc"] = BasehttpPath + ddlVersionNumber.SelectedValue;
                string s = "window.open('" + FileName + "','_blank');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
            }
            else
            {
                ViewState["ifrmsrc"] = "";
                //ifrm.Attributes.Add("src", FileName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlMaterialName.SelectedIndex = 0;
            ddlDrawingSequenceNumber.SelectedIndex = 0;
            txtQuantity.Text = "";
            // ShowHideControls("view");
            if (gvDrawingBOMDetails.Rows.Count > 0)
            {
                gvDrawingBOMDetails.UseAccessibleHeader = true;
                gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
            }
            ShowHideControls("drawing");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

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
            objMat.BOMID = Convert.ToInt32(hdnBOMId.Value);
            ds = objMat.SavePartLayOutAttachements();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Layout Attachements Saved Successfully');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    //protected void btnSaveBOMCost_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objMat = new cMaterials();
    //    try
    //    {
    //        objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
    //        objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
    //        ds = objMat.UpdateBOMStatusByEnquiryNumberAndVersionNumber();

    //        if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','BOM Completion Status Updated Successfully');", true);

    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"GridView Events"

    protected void gvDrawingBOMDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                if (dr["Flag"].ToString() == "1")
                {
                    //   lbtnEdit.Attributes.Add("Class", "aspNetDisabled");
                    lbtnDelete.Attributes.Add("Class", "aspNetDisabled");
                    lbtnDelete.ToolTip = "BOM Already Uses";
                }
                else
                {
                    // lbtnEdit.ToolTip = "Edit";
                    lbtnDelete.CssClass.Replace("aspNetDisabled", "");
                    lbtnDelete.ToolTip = "Delete";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDrawingBOMDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
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
                FileName = gvDrawingBOMDetails.DataKeys[index].Values[1].ToString();
                ViewState["FileName"] = FileName;
                ViewState["ifrmsrc"] = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, FileName, ddlEnquiryNumber.SelectedValue, ifrm);
            }

            if (e.CommandName == "AddLayoutAttach")
            {
                hdnBOMId.Value = gvDrawingBOMDetails.DataKeys[index].Values[0].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "ShowAttachementPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void DeleteAllBomPart()
    {
        DataSet ds = new DataSet();
        try
        {
            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            ds = objMat.DeleteBomiDetailsByEDIDAndVersionNumber();

            if (ds.Tables[0].Rows[0]["msg"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "SuccesMessage('Succes','BOM Part Deleted successFully');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }


    private void BindBOMDetails()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            btnAddNew.CssClass.Replace("aspNetDisabled", "");

            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);

            ds = objMat.GetBOMDetailsbyEDIDAndVersionnumber();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDrawingBOMDetails.DataSource = ds.Tables[0];
                gvDrawingBOMDetails.DataBind();
                gvDrawingBOMDetails.UseAccessibleHeader = true;
                gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

                divTotalCost.Visible = true;
            }
            else
            {
                gvDrawingBOMDetails.DataSource = "";
                gvDrawingBOMDetails.DataBind();
                divTotalCost.Visible = true;
            }
            if (ds.Tables[1].Rows.Count > 0)
                lblCost.Text = ds.Tables[1].Rows[0]["BOMTotalCost"].ToString();
            else
                lblCost.Text = "0.00";
            if (ds.Tables[2].Rows[0]["BOMStatus"].ToString() == "Completed")
                btnAddNew.CssClass = "btn btn-success add-emp aspNetDisabled";
            else
                btnAddNew.CssClass = "btn btn-success add-emp";

            lblDrawingNumber.Text = " ( " + ds.Tables[3].Rows[0]["DrawingNumber"].ToString() + " ) ";
            lblItemQty.Text = "Qty: " + ds.Tables[3].Rows[0]["Quantity"].ToString() + "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private bool ValidateControl()
    {
        bool isValid = true;
        string error = "";
        try
        {
            if (ddlMaterialName.SelectedIndex == 0)
                error = ddlMaterialName.ClientID + '/' + "Field Required";
            else if (txtQuantity.Text == "")
                error = txtQuantity.ClientID + '/' + "Field Required";
            else if (ddlDrawingSequenceNumber.SelectedIndex == 0)
                error = ddlDrawingSequenceNumber.ClientID + '/' + "Field Required";
            if (error != "")
            {
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Validate", "ServerSideValidation('" + error + "');hideLoader();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isValid;
    }

    private void ShowHideControls(string mode)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            divAdd.Visible = divDrawing.Visible = divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "add":
                    divAdd.Visible = true;
                    break;
                case "drawing":
                    divAdd.Visible = true;
                    divDrawing.Visible = true;
                    divOutput.Visible = true;
                    break;
                case "input":
                    divInput.Visible = true;
                    divAdd.Visible = true;
                    divDrawing.Visible = true;
                    divOutput.Visible = true;
                    break;
                    //case "output":
                    //    divOutput.Visible = true;
                    //    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDrawingSequenceNumber()
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
            ds = objMat.GetDrawingSequencenumberByEDIDAndVersionnumber();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    dt = ds.Tables[0];
            //    DataRow dr;

            //    int count = dt.Rows.Count;

            //    for (int i = 0; i <= 30 - count; i++)
            //    {
            //        dr = dt.NewRow();
            //        dr["DrawingSequenceNumber"] = 0;
            //        dt.Rows.Add(dr);
            //    }

            //    for (int i = 1; i <= 30; i++)
            //    {
            //        if (Convert.ToInt32(dt.Rows[i - 1]["DrawingSequenceNumber"].ToString()) != i)
            //        {
            //            ddlDrawingSequenceNumber.Items.Insert(j, new ListItem(i.ToString(), i.ToString()));
            //            j++;

            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 1; i <= 30; i++)
            //    {
            //        ddlDrawingSequenceNumber.Items.Insert(i, new ListItem(i.ToString(), i.ToString()));
            //    }
            //}

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


    private void ClearValues()
    {
        txtQuantity.Text = "";
        ddlMaterialName.SelectedIndex = 0;
        ddlDrawingSequenceNumber.SelectedIndex = 0;
        hdnBOMId.Value = "0";
    }

    #endregion

}