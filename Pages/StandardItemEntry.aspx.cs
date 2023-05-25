using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_StandardItemEntry : System.Web.UI.Page
{
    #region "Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cDesign objDesign;
    cMaterials objMat;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            ShowHideControls("add,view");
            BindStandardItemEntryHeaderDetails();
        }
        if (target == "deletegvrow")
        {
            DataSet ds = new DataSet();
            objMat = new cMaterials();

            objMat.SITCID = Convert.ToInt32(arg.Split('/')[0].ToString());
            objMat.BOMID = Convert.ToInt32(arg.Split('/')[1].ToString());

            objMat.Flag = "Delete";
            ds = objMat.GetStandardItemBOMCostDetailsBySITCID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Material Name Deleted Successfully');", true);

            bindBOMCostDetails();
        }
        if (target == "ShareStandardItem")
        {
            ShareStandardItemDetails(arg.ToString());
        }
        //if (target == "ShareItem")
        //    UpdateItemSharedStatus();
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMaterialName_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialName.SelectedIndex > 0)
            {
                divFields.Visible = true;
                objMat.SIEHID = Convert.ToInt32(hdnSIEHID.Value);
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
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

    #region "Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        try
        {
            ShowHideControls("input");
            objDesign.GetStandardItemAndSizeDetails(ddlItemname, ddlsize);
            objDesign.GetPressureUnitsDetails(ddlPresureUnits);
            hdnSIEHID.Value = "0";
            ddlItemname.Enabled = ddlsize.Enabled = true;
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
            ShowHideControls("add,view");
            hdnSIEHID.Value = "0";
            ddlItemname.Enabled = ddlsize.Enabled = true;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancelBom_Click(object sender, EventArgs e)
    {
        ddlMaterialName.Enabled = chkAddtionalPart.Enabled = true;
        ddlMaterialName.SelectedIndex = 0;
        divFields.Visible = false;
        lblPartName_Edit.Text = "";
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

    protected void btnSaveStandardItemEntry_Click(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        string FileName = "";
        string[] extension;
        string AttachmentName;
        string path;
        try
        {
            objDesign.SIEHID = Convert.ToInt32(hdnSIEHID.Value);
            objDesign.ItemID = Convert.ToInt32(ddlItemname.SelectedValue);
            objDesign.drawingName = txtDrawingname.Text;
            objDesign.Sizeid = Convert.ToInt32(ddlsize.SelectedValue);
            if (rblTagNoItemCodeMatCode.SelectedValue != "None")
            {
                objDesign.ItemCodeValue = txt_tagno.Text;
                objDesign.itemCodeType = rblTagNoItemCodeMatCode.SelectedValue;
            }
            else
                objDesign.ItemCodeValue = null;

            objDesign.Description = txt_description.Text;
            objDesign.revisionNumber = Convert.ToInt32(txtRevision.Text);
            objDesign.pressure = txt_pressure.Text;
            objDesign.Temprature = txt_temperature.Text;
            objDesign.Movement = txt_movement.Text;

            string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
            FileName = Path.GetFileName(fAttachment.PostedFile.FileName);

            path = DrawingDocumentSavePath + "StandardItemDocs" + "\\";

            extension = FileName.Split('.');
            AttachmentName = extension[0].ToString() + '_' + txtRevision.Text + "_" + "StandardItem" + '.' + extension[1];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            fAttachment.SaveAs(path + AttachmentName);
            objDesign.AttachementName = AttachmentName;
            objDesign.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objDesign.SaveStandardItemEntryDetails();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Item Details Saved Successfully');", true);
                BindStandardItemEntryHeaderDetails();
                ShowHideControls("add,view");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvStandardItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "AddBom")
            {
                objMat = new cMaterials();

                Label lblItemName_h = (Label)gvStandardItemDetails.Rows[index].FindControl("lblItemName_h");
                Label lblitemsize = (Label)gvStandardItemDetails.Rows[index].FindControl("lblitemsize");
                Label lbltagno = (Label)gvStandardItemDetails.Rows[index].FindControl("lbltagno");

                lblitemname_h.Text = lblItemName_h.Text + "/" + lblitemsize.Text + "/" + lbltagno.Text;

                string SIEHID = gvStandardItemDetails.DataKeys[index].Values[0].ToString();
                hdnSIEHID.Value = SIEHID;
                objMat.GetMaterialList(ddlMaterialName);
                bindBOMCostDetails();
                divOutputBom.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "AddBomPopUp();", true);
            }
            if (e.CommandName.ToString() == "EditItem")
            {
                DataSet ds = new DataSet();
                objDesign = new cDesign();
                try
                {
                    objDesign.SIEHID = Convert.ToInt32(gvStandardItemDetails.DataKeys[index].Values[0].ToString());
                    ds = objDesign.getStandardItemDetailsBySIEHID();

                    hdnSIEHID.Value = ds.Tables[0].Rows[0]["SIEHID"].ToString();

                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SIBom"].ToString()))
                        ddlItemname.Enabled = ddlsize.Enabled = true;
                    else
                        ddlItemname.Enabled = ddlsize.Enabled = false;

                    txtDrawingname.Text = ds.Tables[0].Rows[0]["DrawingName"].ToString();
                    txtRevision.Text = ds.Tables[0].Rows[0]["RevisionNumber"].ToString();
                    ddlItemname.SelectedValue = ds.Tables[0].Rows[0]["ItemID"].ToString();
                    ddlsize.SelectedValue = ds.Tables[0].Rows[0]["SID"].ToString();
                    txt_description.Text = "";
                    txt_pressure.Text = ds.Tables[0].Rows[0]["Presure"].ToString();
                    txt_temperature.Text = ds.Tables[0].Rows[0]["Temprature"].ToString();
                    txt_movement.Text = ds.Tables[0].Rows[0]["Movement"].ToString();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = ds.Tables[1].Rows[0]["Name"].ToString();
                        txt_tagno.Text = ds.Tables[1].Rows[0]["Value"].ToString();
                    }
                    else
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = "None";
                        txt_tagno.Text = "";
                    }
                    ShowHideControls("input");
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

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

                if (dr["BOMStatus"].ToString() == "Completed")
                {
                    btnEdit.Attributes.Add("Class", "aspNetDisabled");
                    btnEdit.ToolTip = "Enquiry Moved to Next Stage.";

                    btnDelete.ToolTip = "Enquiry Moved to Next Stage.";
                    btnDelete.Attributes.Add("Class", "aspNetDisabled");


                    btnSave.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    btnSave.ToolTip = "Enquiry Moved to Next Stage.";

                    // btnEstimationCompleted.CssClass = "btn btn-cons btn-save aspNetDisabled";
                    btnSave.ToolTip = "Enquiry Moved to Next Stage.";
                }
                if (dr["BOMStatus"].ToString() == "InComplete")
                {
                    btnEdit.CssClass.Replace("aspNetDisabled", "");
                    btnSave.CssClass.Replace("aspNetDisabled", "");
                    btnDelete.CssClass.Replace("aspNetDisabled", "");
                    //btnEstimationCompleted.CssClass.Replace("aspNetDisabled", "");
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
                // objMat.MID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[1].ToString());

                hdnBomID.Value = gvBOMCostDetails.DataKeys[index].Values[2].ToString();
                hdnMID.Value = gvBOMCostDetails.DataKeys[index].Values[1].ToString();

                objMat.BOMID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[2].ToString());
                objMat.SITCID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[0].ToString());
                PartName = gvBOMCostDetails.DataKeys[index].Values[3].ToString();
                LayoutFileName = gvBOMCostDetails.DataKeys[index].Values[4].ToString();
            }
            else
            {
                chkAddtionalPart.Checked = true;
                //  objMat.MID = Convert.ToInt32(gvAddtionalPartDetails.DataKeys[index].Values[1].ToString());

                hdnBomID.Value = gvAddtionalPartDetails.DataKeys[index].Values[2].ToString();
                hdnMID.Value = gvAddtionalPartDetails.DataKeys[index].Values[1].ToString();

                objMat.BOMID = Convert.ToInt32(gvAddtionalPartDetails.DataKeys[index].Values[2].ToString());
                objMat.SITCID = Convert.ToInt32(gvAddtionalPartDetails.DataKeys[index].Values[0].ToString());
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

                ddlMaterialName.Enabled = false;
                ds = objMat.GetStandardItemBOMCostDetailsBySITCID();
                BindDrawingSequenceNumber("Edit");
                bindInputAndFormulaFieldsDetails(ds, "edit");
            }
            //if (e.CommandName.ToString() == "ViewLayOut")
            //{
            //    string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
            //    string FileName = BasehttpPath + LayoutFileName;

            //    ViewState["FileName"] = LayoutFileName;
            //    ifrm.Attributes.Add("src", FileName);
            //    if (File.Exists(DrawingDocumentSavePath + LayoutFileName))
            //    {
            //        ViewState["ifrmsrc"] = BasehttpPath + LayoutFileName;
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
            //    }
            //    else
            //    {
            //        ViewState["ifrmsrc"] = "";
            //        ifrm.Attributes.Add("src", "");
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            //    }
            //}

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
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

    private void BindStandardItemEntryHeaderDetails()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            ds = objDesign.BindStandardItemHeaderDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStandardItemDetails.DataSource = ds.Tables[0];
                gvStandardItemDetails.DataBind();
            }
            else
            {
                gvStandardItemDetails.DataSource = "";
                gvStandardItemDetails.DataBind();
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
            ddlMaterialName.Enabled = chkAddtionalPart.Enabled = true;
            //objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            //objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            objMat.SIEHID = Convert.ToInt32(hdnSIEHID.Value);

            ds = objMat.GetSIBOMCostDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
            {
                ds.Tables[1].Columns.Remove("MOC");
                dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "AddtionalPart='No'";
                dt = dv.ToTable();

                gvBOMCostDetails.DataSource = dt;
                gvBOMCostDetails.DataBind();

                dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "AddtionalPart='Yes'";
                dt = dv.ToTable();

                gvAddtionalPartDetails.DataSource = dt;
                gvAddtionalPartDetails.DataBind();

                lblAddtionalBOMTotalCost.Text = ds.Tables[2].Rows[0]["AdditionalPartBomCost"].ToString();
                lblItemBomTotalCost.Text = ds.Tables[3].Rows[0]["ItemBOMCost"].ToString();

                if (ds.Tables[4].Rows.Count > 0)
                    lblCost.Text = ds.Tables[4].Rows[0]["TotalBOMCost"].ToString();

                //lblDrawingNumber.Text = " ( " + ds.Tables[5].Rows[0]["DrawingNumber"].ToString() + " ) ";
                //lblItemQty.Text = " Qty: " + ds.Tables[5].Rows[0]["Quantity"].ToString() + "";
            }
            else
            {
                gvBOMCostDetails.DataSource = "";
                gvBOMCostDetails.DataBind();

                gvAddtionalPartDetails.DataSource = "";
                gvAddtionalPartDetails.DataBind();

                lblItemBomTotalCost.Text = lblAddtionalBOMTotalCost.Text = lblCost.Text = "0.00";

                //lblDrawingNumber.Text = " ( " + ds.Tables[1].Rows[0]["DrawingNumber"].ToString() + " ) ";
                //lblItemQty.Text = "  Qty: " + ds.Tables[1].Rows[0]["Quantity"].ToString() + "";
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

            objMat.SIEHID = Convert.ToInt32(hdnSIEHID.Value);
            objMat.BOMID = Convert.ToInt32(hdnBomID.Value);
            objMat.Flag = Mode;
            ds = objMat.GetDrawingSequencenumberBySTandardItemDetailsID();

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
                    chkAddtionalPart.Checked = false;
                else
                    chkAddtionalPart.Checked = true;
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
                    lblFormulaName.Attributes.Add("style", "display:block;color:brown;");
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

    private void ShareStandardItemDetails(string SIEHID)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            objDesign.SIEHID = Convert.ToInt32(SIEHID);
            ds = objDesign.ShareStandardItemDetailsBySIEHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Item Sahred Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Web Methods"
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string SaveBOMDetails(String[] strBOMDetails, String[] StrDetails)
    {
        DataSet ds = new DataSet();
        //string EDID;
        //string VersionNumber;
        string SIEHID;
        string MID;
        string MSMID = "";
        string MSMIDVal = "";
        string ReturnMsg = "";
        string BOMID = "";
        string Remarks = "";
        string DrawingSequenceNumber = "";
        string AddtionalPart = "";
        try
        {
            // EDID = StrDetails[0].Split('/')[0];
            SIEHID = StrDetails[0].Split('/')[1];
            MID = StrDetails[0].Split('/')[2];
            BOMID = StrDetails[0].Split('/')[3];
            Remarks = StrDetails[0].Split('/')[4];
            DrawingSequenceNumber = StrDetails[0].Split('/')[5];
            AddtionalPart = StrDetails[0].Split('/')[6];

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

            eplus.data.cDataAccess DAL = new eplus.data.cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveStandardItemBOMSheetDetails";
            //c.Parameters.Add("@EDID", EDID);
            //c.Parameters.Add("@VersionNumber", VersionNumber);

            c.Parameters.Add("@SIEHID", SIEHID);
            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);

            c.Parameters.Add("@MSMID", MSMID);
            c.Parameters.Add("@MSMIDVal", MSMIDVal);
            c.Parameters.Add("@Remarks", Remarks);
            c.Parameters.Add("@DrawingSequenceNumber", DrawingSequenceNumber);
            c.Parameters.Add("@AddtionalPart", AddtionalPart);

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
        //if (gvPartLayoutDetails.Rows.Count > 0)
        //    gvPartLayoutDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}