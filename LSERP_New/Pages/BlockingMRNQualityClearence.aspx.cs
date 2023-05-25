using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_BlockingMRNQualityClearence : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cQuality objQt;
    cProduction objP;
    string RFPQPStatus = "Completed";

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
        if (IsPostBack == false)
        {
            //objc = new cCommon();
            //DataSet dsRFPHID = new DataSet();
            //DataSet dsCustomer = new DataSet();
            //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
            //ViewState["dsCustomer"] = dsCustomer.Tables[0];
            //dsRFPHID = objc.GetQualityRFPDetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
            //ViewState["RFPDetails"] = dsRFPHID.Tables[0];
            ddlrfpnoAndCustomerNameLoad();
            ShowHideControls("add");
        }

        if (target == "deletegvrow")
        {
            objQt = new cQuality();
            objQt.PAPDID = Convert.ToInt32(arg);
            DataSet ds = objQt.deletePartAssemplyPlanningDetailsByPAPDID();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Assemply Planning Details Deleted successfully');", true);
        }
        if (target == "BMRNQCStatus")
            SaveQualityClearanceDetails();
    }

    #endregion

    #region"DropDown Events"

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                btnSaveAndShare.CssClass = "btn btn-cons btn-success aspNetDisabled";

                ShowHideControls("add,view");
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                ds = objMat.GetItemDetailsByRFPHID(null, "LS_GetItemDetailsByRFPHID");
                ViewState["RFPItemDetails"] = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                    gvItemDetails.DataSource = ds.Tables[0];

                else
                    gvItemDetails.DataSource = "";

                gvItemDetails.DataBind();
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
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

    #endregion

    #region"Button Events"

    protected void btnSaveAP_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.PAPDID = Convert.ToInt32(hdnPAPDID.Value);
            objQt.PRPDID1 = Convert.ToInt32(ddlPartName1_AP.SelectedValue.Split('/')[1].ToString());
            objQt.BOMID1 = Convert.ToInt32(ddlPartName1_AP.SelectedValue.Split('/')[0].ToString());
            objQt.BOMID2 = Convert.ToInt32(ddlPartName2_AP.SelectedValue.Split('/')[0].ToString());
            objQt.PRPDID2 = Convert.ToInt32(ddlPartName2_AP.SelectedValue.Split('/')[1].ToString());

            objQt.WPSID = Convert.ToInt32(ddlWPSnumber_AP.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objQt.Remarks = txtRemarks_AP.Text;
            ds = objQt.SavePartAssemplyPlanningDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','Assemply Planning Details Saved Successfully');", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','Assemply Planning Details Updated Successfully');", true);
            hdnPAPDID.Value = "0";
            BindAssemplyPlanningDetails();
            ddlRFPNo_SelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveAndShare_OnClick(object sender, EventArgs e)
    {
        DataTable dt;
        string Status = "";
        try
        {
            dt = (DataTable)ViewState["RFPItemDetails"];

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["QCStatus"].ToString() == "InComplete")
                {
                    Status = "IC";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "InfoMessage('information','Please Complete the QC Status For All item')", true);
                    break;
                }
                else
                    Status = "C";
            }
            if (Status == "C")
                UpdateQCStatus();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridviewEvents"

    protected void gvItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblstatus = (Label)e.Row.FindControl("lblstatus");

                if (string.IsNullOrEmpty(dr["BQCStatus"].ToString()))
                {
                    lblstatus.Text = "";
                }
                else
                {
                    lblstatus.Text = "QC Requested";
                }

                if (dr["QCStatus"].ToString() == "InComplete")
                    btnSaveAndShare.CssClass = "btn btn-cons btn-success";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvQualityClearence_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("rblApprove");



                //if (dr["QCStatus"].ToString() == "A")
                //    rbl.SelectedValue = "A";
                //else if (dr["QCStatus"].ToString() == "R")
                //    rbl.SelectedValue = "R";
                //else
                //    rbl.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int RFDID = Convert.ToInt32(gvItemDetails.DataKeys[index].Values[0].ToString().Split('/')[0]);
            int EDID = Convert.ToInt32(gvItemDetails.DataKeys[index].Values[0].ToString().Split('/')[1]);
            hdnRFPDID.Value = RFDID.ToString();
            hdnEDID.Value = EDID.ToString();
            Label lblItemName = (Label)gvItemDetails.Rows[index].FindControl("lblItemName");

            if (e.CommandName == "QCView")
            {
                lblItemName_QC.Text = lblItemName.Text + " / " + ddlRFPNo.SelectedItem.Text;
                bindQualityClearenceDetails();
            }
            else if (e.CommandName == "APView")
            {
                lblItemName_AP.Text = lblItemName.Text;
                BindAssemplyPlanningDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyPlanningDetails_AP_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnPAPDID.Value = gvAssemplyPlanningDetails_AP.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["AP"];
            dt.DefaultView.RowFilter = "PAPDID='" + hdnPAPDID.Value + "'";
            ddlPartName1_AP.SelectedValue = dt.DefaultView.ToTable().Rows[0]["BOMID1"].ToString() + '/' + dt.DefaultView.ToTable().Rows[0]["PRPDID1"].ToString();
            ddlPartName2_AP.SelectedValue = dt.DefaultView.ToTable().Rows[0]["BOMID2"].ToString() + '/' + dt.DefaultView.ToTable().Rows[0]["PRPDID2"].ToString();
            ddlWPSnumber_AP.SelectedValue = dt.DefaultView.ToTable().Rows[0]["WPSID"].ToString();
            txtRemarks_AP.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ddlrfpnoAndCustomerNameLoad()
    {
        DataSet ds = new DataSet();
        try
        {
            objQt = new cQuality();
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objQt.GetRFPCustomerNameByBlockingMRNQualityClearance(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
            //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
            ViewState["dsCustomer"] = dsCustomer.Tables[0];
            //dsRFPHID = objc.GetQualityRFPDetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
            dsRFPHID = objQt.GetRFPNoDetailsByBlockingMRNQualityClearance(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
            ViewState["RFPDetails"] = dsRFPHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void SaveQualityClearanceDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt = new DataTable();
        try
        {
            dt.Columns.Add("MPMD");
            dt.Columns.Add("QCStatus");
            dt.Columns.Add("Remarks");
            DataRow dr;
            foreach (GridViewRow row in gvQualityClearence.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                if (chk.Checked)
                {
                    RadioButtonList rbl = (RadioButtonList)row.FindControl("rblApprove");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    dr = dt.NewRow();
                    dr["MPMD"] = Convert.ToInt32(gvQualityClearence.DataKeys[row.RowIndex].Values[0].ToString());
                    dr["QCStatus"] = rbl.SelectedValue;
                    dr["Remarks"] = txtRemarks.Text;
                    dt.Rows.Add(dr);
                }
            }
            objQt.dt = dt;
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objQt.CreatedBy = objSession.employeeid;
            ds = objQt.SaveQualityClearence();

            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Quality Clearence Status Updated Succesfully');", true);
                bindQualityClearenceDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateQCStatus()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.UpdateMRNBlockingQualityClearenceStatusbyRFPHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','QC Status Completed Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindQualityClearenceDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objQt.GetBlockedMRNDetailsByRFPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQualityClearence.DataSource = ds.Tables[0];
                gvQualityClearence.DataBind();
            }
            else
            {
                gvQualityClearence.DataSource = "";
                gvQualityClearence.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowQCPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyPlanningDetails()
    {
        DataSet ds = new DataSet();
        // objMat = new cMaterials();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            //ds = objMat.GetPartDetailsByEDID(ddlPartName1_AP);
            ds = objQt.GetPartSeriwlNoDetailsByEDID(ddlPartName1_AP);
            ddlPartName2_AP.DataSource = ds.Tables[0];
            ddlPartName2_AP.DataTextField = "PartName";
            ddlPartName2_AP.DataValueField = "BOMID";
            ddlPartName2_AP.DataBind();
            ddlPartName2_AP.Items.Insert(0, new ListItem("--Select--", "0"));
            //  objP.GetWPSDetailsbyWPSNumber(ddlWPSnumber_AP);
            ds = new DataSet();
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objQt.GetPartAssemplyPlanningDetailsByRFPDID(ddlWPSnumber_AP);

            ViewState["AP"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyPlanningDetails_AP.DataSource = ds.Tables[0];
                gvAssemplyPlanningDetails_AP.DataBind();
            }
            else
            {
                gvAssemplyPlanningDetails_AP.DataSource = "";
                gvAssemplyPlanningDetails_AP.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowAssemplyPlanningPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divOutput.Visible = false;
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
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails(string QCStatus)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 6;
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

            if (QCStatus == "A")
                QCStatus = "Accepted";
            else if (QCStatus == "RW")
                QCStatus = "Reworked";
            else
                QCStatus = "Rejected";

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
                objAlerts.Subject = "Alert For Job Card QC Status";
                objAlerts.Message = "Job Card QC " + QCStatus + " From RFP No"
                    + ds.Tables[0].Rows[0]["RFPNo"].ToString() + "And Job card No";
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
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
        if (gvItemDetails.Rows.Count > 0)
            gvItemDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvQualityClearence.Rows.Count > 0)
            gvQualityClearence.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}