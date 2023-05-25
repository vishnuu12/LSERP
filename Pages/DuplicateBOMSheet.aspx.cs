using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Web.UI.HtmlControls;
using eplus.data;
using System.Data.SqlClient;

public partial class Pages_DuplicateBOMSheet : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;

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
                objc = new cCommon();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
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

                    bindBOMCostDetails("Item");

                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButton Events"

    protected void rblItemDuplicate_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (rblItemDuplicate.SelectedValue == "ItemDup")
            {
                divItemDuplicateEnquiryNumber.Visible = false;
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                objMat.ItemID = Convert.ToInt32(ddlItemName.SelectedValue);
                objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);
                //  ds = objMat.GetCompletedBOMItemDetails(ddlDuplicateItemName);
                ds = objMat.GetBOMAddedItemDetails(ddlDuplicateItemName);

                if (ds.Tables[0].Rows.Count > 0)
                    divItemDuplicate.Visible = true;
                else
                {
                    divItemDuplicate.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "ErrorMessage('Error','Empty Records Can not Duplicate');", true);
                }

                if (ViewState["BOMStatus"].ToString() == "Completed")
                {
                    btnDuplicate.CssClass = "btn btn-cons btn-save aspNetDisabled";
                }
                else
                {
                    btnDuplicate.CssClass = "btn btn-cons btn-save";
                }
            }
            else if (rblItemDuplicate.SelectedValue == "EnquiryDup")
            {
                objc = new cCommon();
                divItemDuplicate.Visible = true;
                divItemDuplicateEnquiryNumber.Visible = true;
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                objMat.GetAllEnquiryNumberAndCustomerName(ddlDuplicateEnquiryNumber);
                objc.EmptyDropDownList(ddlDuplicateItemName);
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
            objc.EmptyDropDownList(ddlItemName);
            objc.EmptyDropDownList(ddlMaterialName);
            objc.EmptyDropDownList(ddlVersionNumber);
            divOutput.Visible = false;
            divItemDuplicate.Visible = false;
            rblItemDuplicate.Visible = false;
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
                objc.EmptyDropDownList(ddlItemName);
                objc.EmptyDropDownList(ddlVersionNumber);
                objc.EmptyDropDownList(ddlMaterialName);
            }

            divOutput.Visible = false;
            divItemDuplicate.Visible = false;
            rblItemDuplicate.Visible = false;

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
                objc.EmptyDropDownList(ddlMaterialName);
            }
            //    divViewDrawing.Visible = false;
            // ShowHideControls("divadd");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);

            divOutput.Visible = false;
            divItemDuplicate.Visible = false;
            rblItemDuplicate.Visible = false;
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
                rblItemDuplicate.Visible = true;
                divOutput.Visible = true;
                objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                objMat.VersionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
                objMat.GetDrawingMaterialDetailsByEDIDAndVersionNumber(ddlMaterialName);
                bindBOMCostDetails("Item");
            }
            else
            {
                objc.EmptyDropDownList(ddlMaterialName);
                divOutput.Visible = false;
                divItemDuplicate.Visible = false;
                rblItemDuplicate.Visible = false;
            }

            divItemDuplicate.Visible = false;
            rblItemDuplicate.ClearSelection();

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
                ds = objMat.GetMaterialFormulaDetailsByMaterialID();
                bindInputAndFormulaFieldsDetails(ds, ds.Tables[2].Rows[0]["Mode"].ToString().ToLower());

                //gvBOMCostDetails.UseAccessibleHeader = true;
                //gvBOMCostDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
                divFields.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlDuplicateItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDuplicateItemName.SelectedIndex > 0)
            {
                gvItemDuplicateDetails.Visible = true;
                bindBOMCostDetails("DuplicateItem");
            }
            else
                gvItemDuplicateDetails.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlDuplicateEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        try
        {
            objMat.EnquiryNumber = Convert.ToInt32(ddlDuplicateEnquiryNumber.SelectedValue);
            objMat.GetCostEstimationCompletedItemListByEnquiryNumber(ddlDuplicateItemName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        bindBOMCostDetails("Item");
        ddlMaterialName_SelectIndexChanged(null, null);
    }

    protected void btnDuplicate_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet dsBomStatus = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedValue);
            dsBomStatus = objMat.GetItemBOMStatusByDDID();

            if (dsBomStatus.Tables[0].Rows[0]["BOMStatus"].ToString() != "Completed")
            {
                objMat.DDID = Convert.ToInt32(ddlDuplicateItemName.SelectedValue.Split('/')[0].ToString());
                ds = objMat.SaveDuplicateBOMDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','BOM Duplicate Item Saved Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                bindBOMCostDetails("Item");
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "ServerSideValidation('" + ddlItemName.ClientID + '/' + "Enquiry Moved To Next Stage" + "')", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

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
                            txtInput.Enabled = false;
                            txtInput.ToolTip = "Part Quantity";
                        }

                        txtInput.CssClass = "form-control" + " " + ds.Tables[0].Rows[i]["Name"].ToString();
                        txtInput.Attributes.Add("style", "width:50%;");
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

    private void bindBOMCostDetails(string Mode)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlVersionNumber.Enabled = ddlMaterialName.Enabled = true;

            if (Mode == "Item")
            {
                objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue);
                objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            }
            else
            {
                objMat.EDID = Convert.ToInt32(ddlDuplicateItemName.SelectedValue.Split('/')[1]);
                string[] stritemname = ddlDuplicateItemName.SelectedItem.Text.Split('/');
                objMat.versionNumber = Convert.ToInt32(stritemname[stritemname.Length - 1]);
            }

            ds = objMat.GetBOMCostDetails();
            if (Mode == "Item")
            {
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                {
                    gvBOMCostDetails.DataSource = ds.Tables[1];
                    gvBOMCostDetails.DataBind();

                    if (ds.Tables[4].Rows.Count > 0)
                        lblCost.Text = ds.Tables[4].Rows[0]["TotalBOMCost"].ToString();
                    ViewState["BOMStatus"] = ds.Tables[1].Rows[0]["BOMStatus"].ToString();
                }
                else
                {
                    gvBOMCostDetails.DataSource = "";
                    gvBOMCostDetails.DataBind();

                    lblCost.Text = "";
                    ViewState["BOMStatus"] = "InComplete";
                }

            }
            else
            {
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                {
                    gvItemDuplicateDetails.DataSource = ds.Tables[1];
                    gvItemDuplicateDetails.DataBind();

                    if (ds.Tables[4].Rows.Count > 0)
                        lblDuplicateItemFrom.Text = ds.Tables[4].Rows[0]["TotalBOMCost"].ToString();

                }
                else
                {
                    gvItemDuplicateDetails.DataSource = "";
                    gvItemDuplicateDetails.DataBind();
                    lblDuplicateItemFrom.Text = "";
                }

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvBOMCostDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            objMat.BOMID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[2].ToString());
            objMat.ETCID = Convert.ToInt32(gvBOMCostDetails.DataKeys[index].Values[0].ToString());

            if (e.CommandName.ToString() == "EditBOM")
            {
                tblInputFields.Rows.Clear();
                tblFormulaFields.Rows.Clear();
                divFields.Visible = true;
                objMat.Flag = "Edit";

                //objMat.GetDrawingMaterialDetailsByEDIDAndVersionNumber(ddlMaterialName);

                //  ddlMaterialName.SelectedValue = gvBOMCostDetails.DataKeys[index].Values[2].ToString() + '/' + gvBOMCostDetails.DataKeys[index].Values[1].ToString();

                ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = ddlItemName.Enabled = ddlVersionNumber.Enabled = ddlMaterialName.Enabled = false;

                ds = objMat.GetBOMCostDetailsByETCID();
                bindInputAndFormulaFieldsDetails(ds, "edit");
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
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
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

                    btnSave.ToolTip = "Enquiry Moved to Next Stage.";

                    btnDuplicate.Attributes.Add("Class", "btn btn-cons btn-save aspNetDisabled");
                }
                if (dr["BOMStatus"].ToString() == "InComplete")
                {
                    btnEdit.CssClass.Replace("aspNetDisabled", "");
                    btnSave.CssClass.Replace("aspNetDisabled", "");
                    btnDelete.CssClass.Replace("aspNetDisabled", "");
                }
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
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
        string EDID;
        string VersionNumber;
        string MID;
        string MSMID = "";
        string MSMIDVal = "";
        string ReturnMsg = "";
        string BOMID = "";
        string Remarks = "";
        try
        {
            EDID = StrDetails[0].Split('/')[0];
            VersionNumber = StrDetails[0].Split('/')[1];
            MID = StrDetails[0].Split('/')[2];
            BOMID = StrDetails[0].Split('/')[3];
            Remarks = StrDetails[0].Split('/')[4];

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
        if (gvItemDuplicateDetails.Rows.Count > 0)
            gvItemDuplicateDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}