using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using eplus.core;
using System.Data;
using System.Web.UI.HtmlControls;
using eplus.data;
using System.IO;
using System.Configuration;

public partial class Pages_AddtionalPartRequest : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;
    cProduction objP;
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
                ///objc = new cCommon();
                objP = new cProduction();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                //dsRFPHID = objc.GetRFPDetailsByUserIDInAddtionalPartBOMRequest(Convert.ToInt32(objSession.employeeid), ddlRFPNo);

                dsCustomer = objP.getRFPCustomerNameByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "0");
                dsRFPHID = objP.GetRFPDetailsByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo, "0");

                ViewState["RFPDetails"] = dsRFPHID.Tables[0];
                //ViewState["RFPDetails"] = dsRFPHID.Tables[0];
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    objMat = new cMaterials();

                    objMat.DDID = Convert.ToInt32(arg.Split('/')[0].ToString());
                    objMat.BOMID = Convert.ToInt32(arg.Split('/')[1].ToString());

                    objMat.Flag = "Delete";
                    ds = objMat.GetProductionBOMCostDetailsByETCID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Material Name Deleted Successfully');", true);

                    ddlItemName_SelectIndexChanged(null, null);
                }
                if (target == "UpdateBOMStatus")
                {
                    DataSet ds = new DataSet();
                    objMat = new cMaterials();

                    objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                    //  objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);

                    ds = objMat.UpdateBOMStatusByEDIDAndVersionNumber();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Item Cost Estimation Completed Successfully');", true);

                    bindBOMCostDetails();
                }
            }
            //if (ddlVersionNumber.SelectedIndex > 0)
            //    divDrawing.Visible = true;
            //else
            //    divDrawing.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

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

    #region"DropDown Events"

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
                objMat.GetItemDetailsByRFPHIDInAP(ddlItemName, "LS_GetProductionItemDetailsByRFPHID");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
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

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                //objMat.DDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
                //objMat.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());

                //ds = objMat.GetPartDetailsByDDID(ddlMaterialName);

                //ViewState["FileName"] = ds.Tables[1].Rows[0]["FileName"].ToString();

                //ViewState["AddtionalPartItemMaterials"] = ds;
                objMat.GetMaterialList(ddlMaterialName);
                bindBOMCostDetails();
            }
            else
            {
                ViewState["FileName"] = "";
                objc.EmptyDropDownList(ddlMaterialName);
                divOutput.Visible = false;
            }
            divFields.Visible = false;

            hdnUserID.Value = objSession.employeeid;
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
                objMat.DDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
                objMat.BOMID = 0;
                objMat.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                hdnBomID.Value = "0";
                ds = objMat.GetProductionBomMaterialFormulaDetailsByDDIDAndBOMID();

                //
                //if (ds.Tables[5].Rows.Count > 0)
                //    hdnPartSnoValid.Value = "1";
                //else
                //    hdnPartSnoValid.Value = "0";
                //

                bindInputAndFormulaFieldsDetails(ds, ds.Tables[2].Rows[0]["Mode"].ToString().ToLower());

                //  hdnBomID.Value = ddlMaterialName.SelectedValue.Split('/')[0];
                hdnMID.Value = ddlMaterialName.SelectedValue;

                //LiPartSno.DataSource = ds.Tables[5];
                //LiPartSno.DataTextField = "PartSno";
                //LiPartSno.DataValueField = "PRPDID";
                //LiPartSno.DataBind();

                //if (ds.Tables[6].Rows.Count > 0)
                //{
                //    LiPartSno.SelectionMode = ListSelectionMode.Multiple;
                //    foreach (DataRow dr in ds.Tables[6].Rows)
                //    {
                //        LiPartSno.Items[Convert.ToInt32(dr["PRPDID"].ToString())].Selected = true;
                //    }
                //}
            }
            else
            {
                divFields.Visible = false;
                // LiPartSno.DataSource = "";
                // LiPartSno.DataBind();
                // LiPartSno.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            lblPartName_Edit.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"CheckBox Events"

    //protected void chkAddtionalPart_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    objMat = new cMaterials();
    //    try
    //    {
    //        if (chkAddtionalPart.Checked)
    //        {
    //            // objMat.GetMaterialList(ddlMaterialName);
    //            objMat.DDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
    //            objMat.GetAddtionalPartNameListByDDID(ddlMaterialName);

    //            divSpecsDetails.Visible = false;
    //            divOutput.Visible = false;
    //            divNewPart.Visible = true;
    //            ddlMaterialName.AutoPostBack = false;
    //        }
    //        else
    //        {
    //            ddlItemName_SelectIndexChanged(null, null);
    //            divSpecsDetails.Visible = true;
    //            divOutput.Visible = true;
    //            divNewPart.Visible = false;
    //            ddlMaterialName.AutoPostBack = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"Button Events"

    protected void btnSaveNewPart_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
            objMat.DDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            ds = objMat.SaveProductionAddtionalPartDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Addtional Part Saved  Successfully');", true);
                //chkAddtionalPart_OnCheckedChanged(null, null);
            }
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

            btnCancel_Click(null, null);
            lblPartName_Edit.Text = "";
            //   LiPartSno.ClearSelection();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlRFPNo.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlMaterialName.Enabled = true;
        ddlMaterialName.SelectedIndex = 0;
        divFields.Visible = false;
        lblPartName_Edit.Text = "";
        hdnBomID.Value = "0";
        hdnMID.Value = "0";
        // LiPartSno.ClearSelection();
    }

    protected void imgViewDrawing_Click(object sender, EventArgs e)
    {
        try
        {
            //string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
            string BasehttpPath = "";
            string FileName = BasehttpPath + ViewState["FileName"].ToString();

            ifrm.Attributes.Add("src", FileName);
            //ddlEnquiryNumber.SelectedValue
            if (File.Exists(DrawingDocumentSavePath + '/' + ViewState["FileName"].ToString()))
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSendForApproval_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objMat = new cMaterials();
        try
        {
            DataRow dr;
            dt.Columns.Add("BOMID");
            dt.Columns.Add("Remarks");
            foreach (GridViewRow row in gvBOMCostDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                if (chkitems.Checked)
                {
                    dr = dt.NewRow();
                    dr["BOMID"] = gvBOMCostDetails.DataKeys[row.RowIndex].Values[2];
                    dr["Remarks"] = "";
                    dt.Rows.Add(dr);
                }
            }

            objMat.dt = dt;
            objMat.Flag = "Request";
            objMat.CreatedBy = objSession.employeeid;

            ds = objMat.UpdateProductionAddtionalPartRequestByBOMID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Addtional Part Requested  Successfully');", true);
                bindBOMCostDetails();
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion

    #region"Common Methods"

    private void BindSavedDetailsbyBOMID()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.DDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.BOMID = Convert.ToInt32(hdnBomID.Value);
            ds = objMat.GetProductionBomMaterialFormulaDetailsByDDIDAndBOMID();
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
                            txtInput.Text = ds.Tables[4].Rows[0]["PartQty"].ToString();
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

                    if (ds.Tables[1].Rows[i]["Name"].ToString() == "MCOST" || ds.Tables[1].Rows[i]["Name"].ToString() == "LCOST")
                        lblInput.Attributes.Add("style", "display:none;");

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

                    if (ds.Tables[1].Rows[i]["Name"].ToString() == "MCOST" || ds.Tables[1].Rows[i]["Name"].ToString() == "LCOST")
                        lblFormulaName.Attributes.Add("style", "display:none;color:white;");
                    else
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
                    if (ds.Tables[1].Rows[i]["Name"].ToString() == "MCOST" || ds.Tables[1].Rows[i]["Name"].ToString() == "LCOST")
                        lblFormulaValue.Attributes.Add("style", "display:none;");

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
        try
        {
            ddlRFPNo.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlMaterialName.Enabled = true;
            objMat.DDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());

            ds = objMat.GetProductionBOMCostDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
            {
                ds.Tables[1].Columns.Remove("MOC");
                ds.Tables[1].Columns.Remove("PartNo");
                ds.Tables[1].Columns.Remove("AddtionalPart");
                ds.Tables[1].Columns.Remove("PartRemarks");

                ds.Tables[1].Columns.Remove("MCOST");
                ds.Tables[1].Columns.Remove("LCOST");

                gvBOMCostDetails.DataSource = ds.Tables[1];
                gvBOMCostDetails.DataBind();

                lblItemBomTotalCost.Text = ds.Tables[3].Rows[0]["ItemBOMCost"].ToString();

                if (ds.Tables[4].Rows.Count > 0)
                    lblCost.Text = ds.Tables[4].Rows[0]["TotalBOMCost"].ToString();

                lblDrawingNumber.Text = " ( " + ds.Tables[5].Rows[0]["DrawingNumber"].ToString() + " ) ";
                lblItemQty.Text = " Qty: " + ds.Tables[5].Rows[0]["Quantity"].ToString() + "";

            }
            else
            {
                gvBOMCostDetails.DataSource = "";
                gvBOMCostDetails.DataBind();

                lblItemBomTotalCost.Text = lblCost.Text = "0.00";

                lblDrawingNumber.Text = " ( " + ds.Tables[1].Rows[0]["DrawingNumber"].ToString() + " ) ";
                lblItemQty.Text = "  Qty: " + ds.Tables[1].Rows[0]["Quantity"].ToString() + "";
            }
        }
        catch (Exception ex)
        {
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
                e.Row.Cells[7].Visible = false;

                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnedit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");

                if (dr["ProductionHApproval"].ToString() == "1" || dr["ProductionHApproval"].ToString() == "7")
                {
                    chk.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
                else
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    chk.Visible = true;
                }

                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;

                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
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

            hdnBomID.Value = gvBOMCostDetails.DataKeys[index].Values[2].ToString();
            hdnMID.Value = gvBOMCostDetails.DataKeys[index].Values[1].ToString();


            string PartName = "";
            string LayoutFileName = "";
            if (GvID == "gvBOMCostDetails")
            {
                // chkAddtionalPart.Checked = false;
                // objMat.MID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[1].ToString());
                objMat.BOMID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[2].ToString());
                objMat.DDID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[0].ToString());
                PartName = gvBOMCostDetails.DataKeys[index].Values[3].ToString();
                LayoutFileName = gvBOMCostDetails.DataKeys[index].Values[4].ToString();
            }


            lblPartName_Edit.Text = PartName.ToString();

            //chkAddtionalPart_CheckedChanged(null, null);

            if (e.CommandName.ToString() == "EditBOM")
            {
                tblInputFields.Rows.Clear();
                tblFormulaFields.Rows.Clear();
                divFields.Visible = true;
                objMat.Flag = "Edit";

                ddlRFPNo.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlMaterialName.Enabled = false;

                ds = objMat.GetProductionBOMCostDetailsByETCID();
                bindInputAndFormulaFieldsDetails(ds, "edit");

                //if (ds.Tables[5].Rows.Count > 0)
                //{
                //    LiPartSno.SelectionMode = ListSelectionMode.Multiple;
                //    foreach (DataRow dr in ds.Tables[5].Rows)
                //    {
                //        LiPartSno.Items[Convert.ToInt32(dr["PRPDID"].ToString())].Selected = true;
                //    }
                //}
            }
            if (e.CommandName.ToString() == "ViewLayOut")
            {
                string BasehttpPath = DrawingDocumentHttppath; //ddlEnquiryNumber.SelectedValue + "/";
                string FileName = BasehttpPath + LayoutFileName;

                ViewState["FileName"] = LayoutFileName;
                ifrm.Attributes.Add("src", FileName);
                if (File.Exists(DrawingDocumentSavePath + LayoutFileName))
                {
                    ViewState["ifrmsrc"] = BasehttpPath + LayoutFileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
                }
                else
                {
                    ViewState["ifrmsrc"] = "";
                    ifrm.Attributes.Add("src", "");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                }
            }

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
        string DDID;
        string MID;
        string MSMID = "";
        string MSMIDVal = "";
        string ReturnMsg = "";
        string BOMID = "";
        string UserID = "";
        string PRPDID = "";
        string RFPDID = "";
        try
        {
            DDID = StrDetails[0].Split('/')[0];
            MID = StrDetails[0].Split('/')[1];
            BOMID = StrDetails[0].Split('/')[2];
            UserID = StrDetails[0].Split('/')[3];
            PRPDID = StrDetails[0].Split('/')[4];
            RFPDID = StrDetails[0].Split('/')[5];

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
            c.CommandText = "LS_SaveProductionBOMSheetDetails";
            c.Parameters.Add("@DDID", DDID);

            c.Parameters.Add("@MID", MID);
            c.Parameters.Add("@BOMID", BOMID);

            c.Parameters.Add("@MSMID", MSMID);
            c.Parameters.Add("@MSMIDVal", MSMIDVal);
            c.Parameters.Add("@UserID", UserID);
            //c.Parameters.Add("@PRPDID", PRPDID);
            c.Parameters.Add("@RFPDID", RFPDID);

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
    }

    #endregion
}