using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_RFPQualityPlanning : System.Web.UI.Page
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
            objQt = new cQuality();
            objQt.UserID = objSession.employeeid;
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            if (Session["RFPHID"] == null)
            {
                //dsCustomer = objQt.getRFPApprovedCustomerNameByUserID(ddlCustomerName);
                //dsRFPHID = objQt.GetRFPDetailsByUserIDAndRFPStatusCompleted(ddlRFPNo);
                //ViewState["RFPDetails"] = dsRFPHID.Tables[0];
                ddlCustomernameAndRFPNoLoad();
                ShowHideControls("add");
            }
            else
            {
                divradio.Visible = false;
                objMat = new cMaterials();
                ShowHideControls("add,view");
                DataSet ds = new DataSet();

                objMat.RFPHID = Convert.ToInt32(Session["RFPHID"].ToString());
                ds = objMat.GetItemDetailsByRFPHID(null, "LS_GetItemDetailsByRFPHID");

                if (ds.Tables[0].Rows.Count > 0)
                    gvItemDetails.DataSource = ds.Tables[0];
                else
                    gvItemDetails.DataSource = "";

                gvItemDetails.DataBind();

                btnSave.Visible = false;
                btnSaveItemQPStatus.Visible = false;
                btnSaveAndShare.Visible = false;
                btnSaveAP.Visible = false;
                btnSaveFinalAssembly_QC.Visible = false;
            }
        }
        if (target == "UpdateRFPQPStatus")
            UpdateRFPQPStatus();
        if (target == "deleteAssemplyPlanning")
        {
            objQt = new cQuality();
            objQt.PAPDID = Convert.ToInt32(arg);
            DataSet ds = objQt.deletePartAssemplyPlanningDetailsByPAPDID();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Assemply Planning Details Deleted successfully');ShowAssemplyPlanningPopUp();", true);
                BindAssemplyPlanningDetails();
            }
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlCustomernameAndRFPNoLoad();
        ShowHideControls("add");
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
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

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                ShowHideControls("add,view");
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                objMat.status = rblRFPChange.SelectedValue;
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                ds = objMat.GetItemDetailsByRFPQualityPlanning(null, "LS_GetItemDetailsByRFPHIDByRFPQualityPlanning");

                if (ds.Tables[0].Rows.Count > 0)
                    gvItemDetails.DataSource = ds.Tables[0];
                else
                    gvItemDetails.DataSource = "";

                gvItemDetails.DataBind();

                if (gvItemDetails.Rows.Count > 0)
                    btnSaveAndShare.Visible = true;
                else
                    btnSaveAndShare.Visible = false;

                if (ds.Tables[1].Rows[0]["QPStatus"].ToString() == "Completed")
                    btnSave.Visible = btnSaveAndShare.Visible = btnSaveItemQPStatus.Visible = false;
                else
                    btnSave.Visible = btnSaveAndShare.Visible = btnSaveItemQPStatus.Visible = true;

                ViewState["QPStatus"] = ds.Tables[1].Rows[0]["QPStatus"].ToString();
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveAndShare_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        Label lblPlanningStatus;
        string RFPQPStatus = "";
        try
        {
            //for (int i = 0; i < gvItemDetails.Rows.Count; i++)
            //{
            //    lblPlanningStatus = (Label)gvItemDetails.Rows[i].FindControl("lblPlanningStatus");
            //    if (lblPlanningStatus.Text == "Completed")
            //        RFPQPStatus = "Completed";
            //    else
            //    {
            //        RFPQPStatus = "Incomplete";
            //        break;
            //    }
            //}
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.CheckQualityPlanningAndAssemplyPlanningDetailsCompletedOrNot();

            if (ds.Tables[1].Rows[0]["QPMessage"].ToString() == "InComplete")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','all item should have need to QP Status Completed');", true);
            else
            {
                if (ds.Tables[0].Rows[0]["APMessage"].ToString() == "Completed")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "RFPQPStatus", "UpdateRFPQPStatus();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','all item should have need to Assemply planning Completed');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveItemQPStatus_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.EDID = Convert.ToInt32(hdnEDID.Value);
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);

            ds = objQt.UpdateQPStatusByRFPDIDAndEDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "InComplete")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "InfoMessage('Information','Please Complete All Parts of Item');ShowViewPopUp();", true);
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','QP Status Completed Successfully');ShowViewPopUp();", true);
                ddlRFPNo_SelectIndexChanged(null, null);
                SaveAlertDetails(ddlRFPNo.SelectedItem.Text);
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveRFPQPDetails(object sender, EventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        int WPSID = 0;
        try
        {
            foreach (GridViewRow row in gvRFPQualityPlanning.Rows)
            {
                WPSID = 0;

                objQt.RFPQPDID = Convert.ToInt32(gvRFPQualityPlanning.DataKeys[row.RowIndex].Values[1].ToString());

                objQt.BOMID = Convert.ToInt32(hdnBOMID.Value);
                objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
                objQt.QPDID = Convert.ToInt32(gvRFPQualityPlanning.DataKeys[row.RowIndex].Values[0].ToString());

                RadioButtonList rblLSIQCApplicable = (RadioButtonList)row.FindControl("rblListQCApplicable");
                RadioButtonList rblClientTPIAApplicable = (RadioButtonList)row.FindControl("rblClientTPIAApplicable");
                RadioButtonList rblDocumentMandatory = (RadioButtonList)row.FindControl("rblDocumentMandatory");
                Label lblTypeofCheck = (Label)row.FindControl("lblTypeOfCheck");

                if (rblLSIQCApplicable.SelectedValue == "1")
                {
                    if (lblTypeofCheck.Text == "WPS")
                    {
                        DropDownList ddlWPSNumber = (DropDownList)row.FindControl("ddlWPS");
                        if (ddlWPSNumber.SelectedIndex > 0)
                            WPSID = Convert.ToInt32(ddlWPSNumber.SelectedValue);
                    }
                }

                objQt.WPSID = WPSID;

                objQt.LSIQCApplicable = Convert.ToInt32(rblLSIQCApplicable.SelectedValue);
                objQt.ClientTPIAApplicable = Convert.ToInt32(rblClientTPIAApplicable.SelectedValue);
                objQt.Remarks = txtremarks.Text;
                objQt.Stage = ViewState["Stage"].ToString();
                objQt.CreatedBy = objSession.employeeid;

                ds = objQt.SaveRFPQualityPlanningDetails();
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','RFP QP Saved Successfully');hideAddPopUp();", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','RFP QP Updated Successfully');hideAddPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');hideAddPopUp();", true);

            BindPartDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveAP_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        string AWQPDID = "";
        try
        {
            if (ddlPartName1_AP.SelectedValue != ddlPartName2_AP.SelectedValue)
            {
                objQt.PAPDID = Convert.ToInt32(hdnPAPDID.Value);
                objQt.BOMID1 = Convert.ToInt32(ddlPartName1_AP.SelectedValue);
                objQt.BOMID2 = Convert.ToInt32(ddlPartName2_AP.SelectedValue);
                objQt.WPSID = Convert.ToInt32(ddlWPSnumber_AP.SelectedValue);
                objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
                objQt.Remarks = txtRemarks_AP.Text;

                foreach (ListItem li in liPartAssemblyQC.Items)
                {
                    if (li.Selected)
                    {
                        if (AWQPDID == "")
                            AWQPDID = li.Value;
                        else
                            AWQPDID = AWQPDID + ',' + li.Value;
                    }
                }
                objQt.AWQPDID = AWQPDID;

                objQt.CreatedBy = objSession.employeeid;

                ds = objQt.SavePartAssemplyPlanningDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','Assemply Planning Details Saved Successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','Assemply Planning Details Updated Successfully');", true);

                hdnPAPDID.Value = "0";

                BindAssemplyPlanningDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','Select Diffrent Part Sno');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveFinalAssembly_QC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        string FAQPDID = "";
        try
        {
            foreach (ListItem li in liFinalAssemblyQC.Items)
            {
                if (li.Selected)
                {
                    if (FAQPDID == "")
                        FAQPDID = li.Value;
                    else
                        FAQPDID = FAQPDID + ',' + li.Value;
                }
            }
            objQt.FAQPDID = FAQPDID;
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objQt.SaveFinalAssemblyQCListDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','Final Assembly QC Updated Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnShareAssemplyPlanning_Click(object sender, EventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objQt.UpdateShareAssemplyPlanningStatusByRFPDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','Assemply Planning Details Shared Successfully');HideAssemplyPlanningPopUp();", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
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
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAPReverse = (LinkButton)e.Row.FindControl("btnAPReverse");
                LinkButton btnupdateAPNeed = (LinkButton)e.Row.FindControl("btnupdateAPNeed");

                if (objSession.type == 1)
                {
                    btnupdateAPNeed.Visible = true;
                    btnAPReverse.Visible = true;
                }
                else
                {
                    btnupdateAPNeed.Visible = false;
                    btnAPReverse.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
	
    protected void gvRFPQualityPlanning_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                RadioButtonList rblListQCApplicable_H = (RadioButtonList)e.Row.FindControl("rblListQCApplicable_H");

                foreach (ListItem rbl in rblListQCApplicable_H.Items)
                {
                    if (ViewState["Stage"].ToString() == "stage1")
                    {
                        if (rbl.Value == "1")
                            rbl.Text = "Required";
                        else
                            rbl.Text = "Not Required";
                    }
                    else if (ViewState["Stage"].ToString() == "stage2")
                    {
                        if (rbl.Value == "1")
                            rbl.Text = "Witness";
                        else
                            rbl.Text = "Review";
                    }
                }
            }

            //update LSE_RFPdetail set APStatus='No Need' where RFPHID=20

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RadioButtonList rblLSIQCApplicable = (RadioButtonList)e.Row.FindControl("rblListQCApplicable");
                RadioButtonList rblListQCAccepted = (RadioButtonList)e.Row.FindControl("rblListQCAccepted");
                RadioButtonList rblClientTPIAApplicable = (RadioButtonList)e.Row.FindControl("rblClientTPIAApplicable");
                RadioButtonList rblClientTPIAAccepted = (RadioButtonList)e.Row.FindControl("rblClientTPIAAccepted");
                RadioButtonList rblDocumentMandatory = (RadioButtonList)e.Row.FindControl("rblDocumentMandatory");
                DropDownList ddlWPSNumber = (DropDownList)e.Row.FindControl("ddlWPS");

                RadioButtonList rbllqc = (RadioButtonList)e.Row.FindControl("rblListQCApplicable");

                if (dr["TypeOfCheck"].ToString() == "WPS")
                {
                    objP = new cProduction();
                    objP.GetWPSDetailsbyWPSNumber(ddlWPSNumber);

                    if (dr["LSIQCApplicable"].ToString() == "1")
                    {
                        DataTable dt;
                        ddlWPSNumber.Attributes.Add("style", "display:block;");
                        dt = (DataTable)ViewState["WpsNo"];
                        ddlWPSNumber.SelectedValue = dt.Rows[0]["WPSID"].ToString();
                    }
                    // ddlWPSNumber.Visible = false;                                            
                    else
                    {
                        ddlWPSNumber.Attributes.Add("style", "display:none;");
                    }
                }
                else
                    ddlWPSNumber.Visible = false;
                //ddlWPSNumber.Attributes.Add("style", "display:none;");

                rblLSIQCApplicable.SelectedValue = dr["LSIQCApplicable"].ToString() == "1" ? "1" : "0";
                rblClientTPIAApplicable.SelectedValue = dr["CLIENTQCAPPLICABLE"].ToString() == "1" ? "1" : "0"; ;
                rblDocumentMandatory.SelectedValue = dr["DOCUMENTMANDATORY"].ToString() == "1" ? "1" : "0"; ;

                foreach (ListItem rbl in rbllqc.Items)
                {
                    if (ViewState["Stage"].ToString() == "stage1")
                    {
                        if (rbl.Value == "1")
                            rbl.Text = "Required";
                        else
                            rbl.Text = "Not Required";
                    }
                    else if (ViewState["Stage"].ToString() == "stage2")
                    {
                        if (rbl.Value == "1")
                            rbl.Text = "Witness";
                        else
                            rbl.Text = "Review";
                    }
                }
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
            hdnPAPDID.Value = "0";

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int RFDID = Convert.ToInt32(gvItemDetails.DataKeys[index].Values[0].ToString().Split('/')[0]);
            int EDID = Convert.ToInt32(gvItemDetails.DataKeys[index].Values[0].ToString().Split('/')[1]);
            hdnRFPDID.Value = RFDID.ToString();
            hdnEDID.Value = EDID.ToString();
            Label lblItemName = (Label)gvItemDetails.Rows[index].FindControl("lblItemName");

            if (e.CommandName == "QPView")
            {
                lblItemName_QP.Text = lblItemName.Text + "/" + ddlRFPNo.SelectedItem.Text;
                BindPartDetails();
            }
            else if (e.CommandName == "APView")
            {
                lblItemName_AP.Text = lblItemName.Text + " / " + ddlRFPNo.SelectedItem.Text;
                BindAssemplyPlanningDetails();
            }
            else if (e.CommandName == "ReverseAP")
            {
                ReverseAssemplyPlanningStatusByRFPDID();
            }

            if (e.CommandName == "NoNeed")
            {
                cQuality objQ = new cQuality();
                LinkButton btnupdateAPNeed = (LinkButton)gvItemDetails.Rows[index].FindControl("btnupdateAPNeed");
                DataSet ds = new DataSet();

                objQ.RFPDID = Convert.ToInt32(hdnRFPDID.Value);

                ds = objQ.UpdateAssemplyPlanningStatusByRFPDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "SuccessMessage('Success','updated Succesfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "SuccessMessage('Success','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                ddlRFPNo_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartNameDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int BOMID = Convert.ToInt32(gvPartNameDetails.DataKeys[index].Values[0].ToString());
            hdnBOMID.Value = BOMID.ToString();
            Label lblPartName = (Label)gvPartNameDetails.Rows[index].FindControl("lblPartName");
            ViewState["Stage"] = e.CommandName.ToString();
            BindRFPQualityPlanningDetails(e.CommandName.ToString());
            lblPartName_QP.Text = lblPartName.Text + "/" + ddlRFPNo.SelectedItem.Text;
            // ViewState["Gradename"].ToString() + " / " +
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
            ddlPartName1_AP.SelectedValue = dt.DefaultView.ToTable().Rows[0]["BOMID1"].ToString();
            ddlPartName2_AP.SelectedValue = dt.DefaultView.ToTable().Rows[0]["BOMID2"].ToString();
            ddlWPSnumber_AP.SelectedValue = dt.DefaultView.ToTable().Rows[0]["WPSID"].ToString();
            txtRemarks_AP.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
            string QPDID = dt.DefaultView.ToTable().Rows[0]["EditQPDID"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselectPartAssemblyQC('" + QPDID + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void gvRFPQualityPlanning_OnDataBound(object sender, EventArgs e)
    {
        for (int i = gvRFPQualityPlanning.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = gvRFPQualityPlanning.Rows[i];
            GridViewRow previousRow = gvRFPQualityPlanning.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[2].Text == previousRow.Cells[2].Text)
                {
                    if (previousRow.Cells[2].RowSpan == 0)
                    {
                        //if (row.Cells[2].RowSpan == 0)
                        //    previousRow.Cells[2].RowSpan += 2;
                        //else
                        //    previousRow.Cells[2].RowSpan = row.Cells[2].RowSpan + 1;

                        //row.Cells[2].Visible = false;
                    }
                }
            }
        }
    }
    protected void gvAssemplyPlanningDetails_AP_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (Session["RFPHID"] == null)
                {
                    //ViewState["QPStatus"].ToString() == "Completed" || 
                    if (ViewState["APStatus"].ToString() == "Completed")
                    {
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        btnSaveAP.Visible = false;
                        btnSaveFinalAssembly_QC.Visible = false;
                    }
                    else
                    {
                        btnEdit.Visible = true;
                        btnDelete.Visible = true;
                        btnSaveAP.Visible = true;
                        btnSaveFinalAssembly_QC.Visible = true;
                    }
                }
                else
                {
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ddlCustomernameAndRFPNoLoad()
    {
        DataSet ds = new DataSet();
        try
        {
            objQt = new cQuality();
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objQt.GetRFPCustomerNameByRFPQualityPlanning(Convert.ToInt32(objSession.employeeid), ddlCustomerName, rblRFPChange.SelectedValue);
            dsRFPHID = objQt.GetRFPNoDetailsByRFPQualityPlanning(Convert.ToInt32(objSession.employeeid), ddlRFPNo, rblRFPChange.SelectedValue);
            ViewState["RFPDetails"] = dsRFPHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateRFPQPStatus()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

            ds = objQt.UpdateRFPQPStatusByRFPHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Success','RFP QP Completed Successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyPlanningDetails()
    {
        DataSet ds = new DataSet();
        DataSet dsQC = new DataSet();
        objMat = new cMaterials();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            dsQC = objQt.BindAssemplyWeldingAndFinalAssemplyQCDetails(liFinalAssemblyQC, liPartAssemblyQC);

            objMat.EDID = Convert.ToInt32(hdnEDID.Value);
            ds = objMat.GetPartDetailsByEDID(ddlPartName1_AP);

            //ddlPartName1_AP.DataSource = ds.Tables[0];
            //ddlPartName1_AP.DataTextField = "AssemplyPartName";
            //ddlPartName1_AP.DataValueField = "BOMID";
            //ddlPartName1_AP.DataBind();
            //ddlPartName1_AP.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlPartName2_AP.DataSource = ds.Tables[0];
            ddlPartName2_AP.DataTextField = "AssemplyPartName";
            ddlPartName2_AP.DataValueField = "BOMID";
            ddlPartName2_AP.DataBind();
            ddlPartName2_AP.Items.Insert(0, new ListItem("--Select--", "0"));
            //  objP.GetWPSDetailsbyWPSNumber(ddlWPSnumber_AP);
            ds = new DataSet();
            //////objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objQt.GetPartAssemplyPlanningDetailsByRFPDID(ddlWPSnumber_AP);

            ViewState["AP"] = ds.Tables[0];

            ViewState["APStatus"] = ds.Tables[1].Rows[0]["APStatus"].ToString();

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
            string QPDID = "";
            if (dsQC.Tables[2].Rows.Count > 0)
                QPDID = dsQC.Tables[2].Rows[0]["EditQPDID"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowAssemplyPlanningPopUp();multiselectFinalAssemblyQC('" + QPDID + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.EDID = Convert.ToInt32(hdnEDID.Value);
            objMat.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objMat.GetPartDetailsByEDID(null);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartNameDetails.DataSource = ds.Tables[0];
                gvPartNameDetails.DataBind();
            }
            else
            {
                gvPartNameDetails.DataSource = "";
                gvPartNameDetails.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowViewPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPQualityPlanningDetails(string CommandName)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.BOMID = Convert.ToInt32(hdnBOMID.Value);
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objQt.Stage = CommandName;
            ds = objQt.GetQualityPlanningDetailsByBOMID();

            ViewState["WpsNo"] = ds.Tables[1];
            ViewState["Gradename"] = ds.Tables[2].Rows[0]["GradeName"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPQualityPlanning.DataSource = ds.Tables[0];
                gvRFPQualityPlanning.DataBind();

                if (Session["RFPHID"] == null)
                    btnSave.Visible = true;
            }
            else
            {
                gvRFPQualityPlanning.DataSource = "";
                gvRFPQualityPlanning.DataBind();

                if (Session["RFPHID"] == null)
                    btnSave.Visible = false;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowAddPopUp();rblWPSLSI();", true);
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

    public void SaveAlertDetails(string RFPNo)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            //objc.EnquiryID = RFPNo;
            ds = objc.GetEmployeeIDDetailsByUserTypeIDSANDErpUserType("7", 3);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Alert For RFP Staff Assignment";
                objAlerts.Message = "RFP Relesed Allocate The Resources" + RFPNo;
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void ReverseAssemplyPlanningStatusByRFPDID()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objQt.ReverseAssemplyPlanningStatusByRFPDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Assemply Planning Details Reversed successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
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
        if (gvPartNameDetails.Rows.Count > 0)
            gvPartNameDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        //if (gvRFPQualityPlanning.Rows.Count > 0)
        //    gvRFPQualityPlanning.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}