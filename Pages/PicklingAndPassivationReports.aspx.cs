using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;

public partial class Pages_PicklingAndPassivationReports : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cSales objSales;
    cMaterials objMat;

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
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet dsPOHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                DataSet dsLocation = new DataSet();

                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO);

                ViewState["Customer"] = dsCustomer.Tables[0];
                ViewState["PO"] = dsPOHID.Tables[0];
                ShowHideControls("input");
                //  bindRGSReportDetails();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeletePiclingAndPassivationReportDetailsByPPRHID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    ddlRFPNo_SelectIndexChanged(null, null);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        cDesign objDesign = new cDesign();
        objSales = new cSales();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;

                objSales.POHID = ddlCustomerPO.SelectedValue;
                objSales.GetRFPDetailsByPOHID(ddlRFPNo);

                ShowHideControls("input,view");
            }
            else
            {
                ddlCustomerName.SelectedIndex = 0;
                ShowHideControls("input");
            }

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("input");
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
            dt = (DataTable)ViewState["PO"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlCustomerPO.DataSource = dt;
            ddlCustomerPO.DataTextField = "PORefNo";
            ddlCustomerPO.DataValueField = "POHID";
            ddlCustomerPO.DataBind();
            ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByPicklingAndPassivationReportHeader");

                bindpicklingAndPassivationReportHeader();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }

            hdnPPRHID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            ds = objQt.GetItemSpecificationDetailsByRFPDIDAndEDID(null, null, null);

            //txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //txtJobDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBSLNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;
            txtCustomername.Text = ddlCustomerName.SelectedItem.Text;

            //   bindWallThinningItemDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSavePPR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.PPRHID = hdnPPRHID.Value;
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RFPNo = txtRFPNo.Text;
            objQt.ItemName = txtItemName.Text;
            objQt.ProjectName = txtProjectName.Text;
            objQt.ITPNo = txtITPNo.Text;
            objQt.Customername = txtCustomername.Text;
            objQt.PONo = txtPONo.Text;
            objQt.Size = txtSize.Text;
            objQt.DrawingNo = txtDRGRefNo.Text;
            objQt.BSLNo = txtBSLNo.Text;
            objQt.AcceptedQty = txtQTY.Text;
            objQt.WorkOrderNo = txtWorkorderNo.Text;
            objQt.Part = txtPart.Text;
            objQt.PECPLTDCNo = txtPECPLTDCNo.Text;

            objQt.DatePC = DateTime.ParseExact(txtDatePC.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.StartTimePC = txtStartTimePC.Text;
            objQt.CompletionTimePC = txtCompletionTimePC.Text;
            objQt.ProcedureAndResultsPC = txtProcedureAndResultsPC.Text;
            objQt.DateAPC = DateTime.ParseExact(txtDateAPC.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.StartTimeAPC = txtstarttimeAPC.Text;
            objQt.CompletionTimeAPC = txtCompletiontimeAPC.Text;
            objQt.MethodologyAPC = txtMethodologyAPC.Text;
            objQt.ProcedureAndResultsAPC = txtProcedureandresultsAPC.Text;
            objQt.DatePAC = DateTime.ParseExact(txtDatePAC.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.StartTimePAC = txtStarttimePAC.Text;
            objQt.CompletionTimePAC = txtCompletiontimePAC.Text;
            objQt.MethodologyPAC = txtMethodologyPAC.Text;
            objQt.ProcedureAndResultsPAC = txtProcedureAndResultsPAC.Text;
            objQt.Results = txtResults.Text;
            objQt.CreatedBy = objSession.employeeid;

            ds = objQt.SavePicklingAndPassivationReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Pickling And Passivation Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Pickling And Passivation Report Updated successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPAPRHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PPRHID = gvPAPRHeader.DataKeys[index].Values[0].ToString();
            objQt.PPRHID = PPRHID;
            ds = objQt.GetPicklingAndPassivationReportDetailsByPPRHID();
            if (e.CommandName == "EditPTR")
            {
                hdnPPRHID.Value = ds.Tables[0].Rows[0]["PPRHID"].ToString();
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                txtITPNo.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                txtCustomername.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtSize.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                txtDRGRefNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtQTY.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();
                txtWorkorderNo.Text = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                txtPart.Text = ds.Tables[0].Rows[0]["Part"].ToString();
                txtPECPLTDCNo.Text = ds.Tables[0].Rows[0]["PECPLTDCNo"].ToString();

                txtDatePC.Text = ds.Tables[1].Rows[0]["DatePCEdit"].ToString();
                txtStartTimePC.Text = ds.Tables[1].Rows[0]["StartTimePC"].ToString();
                txtCompletionTimePC.Text = ds.Tables[1].Rows[0]["CompletionTimePC"].ToString();
                txtProcedureAndResultsPC.Text = ds.Tables[1].Rows[0]["ProcedureAndResultsPC"].ToString();
                txtDateAPC.Text = ds.Tables[1].Rows[0]["DateAPCEdit"].ToString();
                txtstarttimeAPC.Text = ds.Tables[1].Rows[0]["StartTimeAPC"].ToString();
                txtCompletiontimeAPC.Text = ds.Tables[1].Rows[0]["CompletionTimeAPC"].ToString();
                txtMethodologyAPC.Text = ds.Tables[1].Rows[0]["MethodologyAPC"].ToString();
                txtProcedureandresultsAPC.Text = ds.Tables[1].Rows[0]["ProcedureAndResultsAPC"].ToString();
                txtDatePAC.Text = ds.Tables[1].Rows[0]["DatePACEdit"].ToString();
                txtStarttimePAC.Text = ds.Tables[1].Rows[0]["StartTimePAC"].ToString();
                txtCompletiontimePAC.Text = ds.Tables[1].Rows[0]["CompletionTimePAC"].ToString();
                txtMethodologyPAC.Text = ds.Tables[1].Rows[0]["MethodologyPAC"].ToString();
                txtProcedureAndResultsPAC.Text = ds.Tables[1].Rows[0]["ProcedureAndResultsPAC"].ToString();
                txtResults.Text = ds.Tables[1].Rows[0]["Results"].ToString();
            }
            if (e.CommandName.ToString() == "PdfPTR")
            {
                lblDatePC_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();

                lblQAPNo_p.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                lblcustomername_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();

                //lbl.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                //txtDRGRefNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                //txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                //txtQTY.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();
                //lblWorkOrderNo_p.Text = ds.Tables[0].Rows[0]["WorkOrderNo"].ToString();
                //lblpart.Text = ds.Tables[0].Rows[0]["Part"].ToString();

                lblDatePC_p.Text = ds.Tables[1].Rows[0]["DatePCView"].ToString();
                lblStarttimePC_p.Text = ds.Tables[1].Rows[0]["StartTimePC"].ToString();
                lblCompletionTimePC_p.Text = ds.Tables[1].Rows[0]["CompletionTimePC"].ToString();
                lblProcedureAndResultsPC_p.Text = ds.Tables[1].Rows[0]["ProcedureAndResultsPC"].ToString();
                lblDateAPC_p.Text = ds.Tables[1].Rows[0]["DateAPCView"].ToString();
                lblStarttimeAPC_p.Text = ds.Tables[1].Rows[0]["StartTimeAPC"].ToString();
                lblCompletionTimeAPC_p.Text = ds.Tables[1].Rows[0]["CompletionTimeAPC"].ToString();
                lblMethodologyAPC_p.Text = ds.Tables[1].Rows[0]["MethodologyAPC"].ToString();
                lblProcedureAndResultsAPC_p.Text = ds.Tables[1].Rows[0]["ProcedureAndResultsAPC"].ToString();
                lblDatePAC_p.Text = ds.Tables[1].Rows[0]["DatePACView"].ToString();
                lblStarttimePAC_p.Text = ds.Tables[1].Rows[0]["StartTimePAC"].ToString();
                lblCompletionTimePAC_p.Text = ds.Tables[1].Rows[0]["CompletionTimePAC"].ToString();
                lblMethodologyPAC_p.Text = ds.Tables[1].Rows[0]["MethodologyPAC"].ToString();
                lblProcedureAndResultsPAC_p.Text = ds.Tables[1].Rows[0]["ProcedureAndResultsPAC"].ToString();
                lblResult_p.Text = ds.Tables[1].Rows[0]["Results"].ToString();

                gvItemDetails_p.DataSource = ds.Tables[2];
                gvItemDetails_p.DataBind();

                hdnAddress.Value = ds.Tables[3].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[3].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[3].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[3].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintPicklingAndPassivationReport();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindpicklingAndPassivationReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetPicklingAndPassivationReportHeaderByRFPHID");
            gvPAPRHeader.DataSource = ds.Tables[0];
            gvPAPRHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divInput.Visible = divOutPut.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "view":
                        divOutPut.Visible = true;
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

    #endregion
}