using eplus.core;
using eplus.data;
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

public partial class Pages_InternalDevelopementBOM : System.Web.UI.Page
{
    #region"Declaration"

    cProduction objP;
    cSession objSession = new cSession();
    cMaterials objMat;

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
                objP = new cProduction();
                DataSet dsEnquiryNumber = new DataSet();

                objP.GetInternalItemDetails(ddlItemName);
                objP.GetPartNameDetails(ddlMaterialName);
                BindInterNalDevelopementDetails();
            }
            if (target == "deletegvrow")
            {
                DataSet ds = new DataSet();
                objP = new cProduction();

                objP.ETCID = Convert.ToInt32(arg.Split('/')[0].ToString());
                objP.BOMID = Convert.ToInt32(arg.Split('/')[1].ToString());
                objP.DDID = 0;
                objP.Flag = "Delete";
                ds = objP.GetInterBomDetailsByBOMID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Material Name Deleted Successfully');", true);

                BindInterNalDevelopementDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"   

    protected void ddlMaterialName_SelectIndexChanged(object sender, EventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialName.SelectedIndex > 0)
            {
                divFields.Visible = true;
                objP.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                objP.BOMID = 0;
                ds = objP.GePartFormulaDetailsByMID();
                bindInputAndFormulaFieldsDetails(ds, ds.Tables[2].Rows[0]["Mode"].ToString().ToLower());

                hdnBomID.Value = "0";
                hdnMID.Value = ddlMaterialName.SelectedValue;
            }
            else
                divFields.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"  

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
            foreach (GridViewRow row in gvInternalBOMDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                if (chkitems.Checked)
                {
                    dr = dt.NewRow();
                    dr["BOMID"] = gvInternalBOMDetails.DataKeys[row.RowIndex].Values[2];
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Internal Developement Material Requested Successfully');", true);
                BindInterNalDevelopementDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnMID.Value = "0";
        hdnBomID.Value = "0";
        divFields.Visible = false;
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
        BindInterNalDevelopementDetails();
        btnCancel_Click(null, null);
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
            if (Mode == "edit")
            {
                txtRemarks.Text = ds.Tables[4].Rows[0]["BomPartRemarks"].ToString();

                //if (ds.Tables[4].Rows[0]["AddtionalPart"].ToString() == "No")
                //    chkAddtionalPart.Checked = false;
                //else
                //    chkAddtionalPart.Checked = true;
                //ddlDrawingSequenceNumber.SelectedValue = ds.Tables[4].Rows[0]["DrawingSequenceNumber"].ToString();
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
                        txtInput.ID = ds.Tables[0].Rows[i]["Name"].ToString();

                        if (Mode == "edit")
                            txtInput.Text = ds.Tables[0].Rows[i]["MSMIDValue"].ToString();

                        if (ds.Tables[0].Rows[i]["Name"].ToString() == "QTY")
                        {
                            if (ds.Tables[4].Rows.Count > 0)
                                txtInput.Text = ds.Tables[4].Rows[0]["PartQty"].ToString();
                            else
                                txtInput.Text = "";
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
                    txtFormula.ID = "txtFormula" + "_" + ds.Tables[1].Rows[i]["Name"].ToString().Replace("SQUARE", "sqrt");
                    txtFormula.Text = ds.Tables[1].Rows[i]["Formula"].ToString();
                    txtFormula.Attributes.Add("style", "display:none;");
                    cell.Controls.Add(txtFormula);

                    Label lblFormulaName = new Label();
                    lblFormulaName.ID = "lblFormulaName" + "_" + ds.Tables[1].Rows[i]["MSMID"].ToString();
                    lblFormulaName.Text = ds.Tables[1].Rows[i]["Formula"].ToString();
                    lblFormulaName.CssClass = "";
                    lblFormulaName.Attributes.Add("style", "display:none;color:brown;");
                    cell.Controls.Add(lblFormulaName);

                    row.Cells.Add(cell);

                    cell = new HtmlTableCell();

                    Label lblFormulaValue = new Label();
                    lblFormulaValue.ID = ds.Tables[1].Rows[i]["Name"].ToString();

                    if (Mode == "edit")
                        lblFormulaValue.Text = ds.Tables[1].Rows[i]["MSMIDValue"].ToString();

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

    private void BindInterNalDevelopementDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.UserID = Convert.ToInt32(objSession.employeeid);
            ds = objP.GetInterNalBOMDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvInternalBOMDetails.DataSource = ds.Tables[0];
                gvInternalBOMDetails.DataBind();
            }
            else
            {
                gvInternalBOMDetails.DataSource = "";
                gvInternalBOMDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvInternalBOMDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {

                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
                //e.Row.Cells[7].Visible = false;

                //e.Row.Cells[10].Visible = false;
                ////e.Row.Cells[11].Visible = false;
                ////e.Row.Cells[12].Visible = false;
                //e.Row.Cells[13].Visible = false;
                //e.Row.Cells[14].Visible = false;
                //e.Row.Cells[19].Visible = false;
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

                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[6].Visible = false;
                //e.Row.Cells[7].Visible = false;

                //e.Row.Cells[10].Visible = false;           
                //e.Row.Cells[13].Visible = false;
                //e.Row.Cells[14].Visible = false;
                //e.Row.Cells[19].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvInternalBOMDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string PartName = "";
            string LayoutFileName = "";

            hdnBomID.Value = gvInternalBOMDetails.DataKeys[index].Values[2].ToString();
            hdnMID.Value = gvInternalBOMDetails.DataKeys[index].Values[1].ToString();
            hdnDDID.Value = gvInternalBOMDetails.DataKeys[index].Values[5].ToString();

            objP.BOMID = Convert.ToInt32(gvInternalBOMDetails.DataKeys[index].Values[2].ToString());
            objP.ETCID = Convert.ToInt32(gvInternalBOMDetails.DataKeys[index].Values[0].ToString());
            PartName = gvInternalBOMDetails.DataKeys[index].Values[3].ToString();
            objP.DDID = Convert.ToInt32(hdnDDID.Value);
            // lblPartName_Edit.Text = PartName.ToString();

            if (e.CommandName.ToString() == "EditBOM")
            {
                tblInputFields.Rows.Clear();
                tblFormulaFields.Rows.Clear();
                divFields.Visible = true;
                objP.Flag = "Edit";
                ds = objP.GetInterBomDetailsByBOMID();

                ddlMaterialName.SelectedValue = ds.Tables[4].Rows[0]["MID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[4].Rows[0]["ItemID"].ToString();

                // BindDrawingSequenceNumber("Edit");
                bindInputAndFormulaFieldsDetails(ds, "edit");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartLayoutDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    #endregion

    #region"Web Methods"
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string SaveBOMDetails(String[] strBOMDetails, String[] StrDetails, string BOMRemarks)
    {
        cSession objSession = new cSession();
        DataSet ds = new DataSet();
        string ItemID;
        string VersionNumber;
        string MID;
        string MSMID = "";
        string MSMIDVal = "";
        string ReturnMsg = "";
        string BOMID = "";
        string Remarks = "";
        string DrawingSequenceNumber = "";
        string AddtionalPart = "";

        objSession = (cSession)HttpContext.Current.Session["LoginDetails"];

        try
        {
            ItemID = StrDetails[0].Split('/')[0];
            VersionNumber = StrDetails[0].Split('/')[1];
            MID = StrDetails[0].Split('/')[2];
            BOMID = StrDetails[0].Split('/')[3];
            // Remarks = StrDetails[0].Split('/')[4];
            Remarks = BOMRemarks;
            DrawingSequenceNumber = StrDetails[0].Split('/')[5];
            AddtionalPart = StrDetails[0].Split('/')[6];

            if (Remarks != "" && ItemID != "0")
            {
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
                c.CommandText = "LS_SaveInterNalDevelopementBomDetails";

                c.Parameters.Add("@ItemID", ItemID);
                c.Parameters.Add("@MID", MID);
                c.Parameters.Add("@BOMID", BOMID);

                c.Parameters.Add("@MSMID", MSMID);
                c.Parameters.Add("@MSMIDVal", MSMIDVal);
                c.Parameters.Add("@Remarks", Remarks);
                c.Parameters.Add("@UserID", objSession.employeeid);

                DAL.GetDataset(c, ref ds);
                ReturnMsg = ds.Tables[0].Rows[0]["Message"].ToString();
            }
            else
                ReturnMsg = "select Item Name And Remarks";
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
        if (gvInternalBOMDetails.Rows.Count > 0)
            gvInternalBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


    #endregion
}