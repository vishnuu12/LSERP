using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_MovementTestReport : System.Web.UI.Page
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
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteMovementTestReportDetailsByMTRID", arg.ToString());

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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRadioGraphicExaminationReportHeader");

                bindWallThiningReportDetails();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }

            hdnMTRID.Value = "0";
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

            txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            txtJobDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //     txtBSLNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;
            txtCustomername.Text = ddlCustomerName.SelectedItem.Text;

            bindWallThinningItemDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                dt.Columns.Add("MTRIDID");
                dt.Columns.Add("MTRID");
                dt.Columns.Add("BSLNo");
                dt.Columns.Add("Size");
                dt.Columns.Add("EquivalentAxialMovement");
                dt.Columns.Add("ActualMovement");
                dt.Columns.Add("Remarks");

                if (gvItemDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvItemDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvItemDetails.Rows)
                        {
                            string MTRDID = gvItemDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string MTRID = gvItemDetails.DataKeys[row.RowIndex].Values[1].ToString();
                            TextBox txtBSLNo = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtBSLNo");
                            TextBox txtSize = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtSize");
                            TextBox txtEquivalantAxialMovement = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtEquivalantAxialMovement");
                            TextBox txtActualMovement = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtActualMovement");
                            TextBox txtRemarks = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtRemarks");

                            CheckBox chkpart = (CheckBox)gvItemDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvItemDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr["MTRIDID"] = 0;
                                    dr["MTRID"] = 0;
                                    dr["BSLNo"] = txtBSLNo.Text;
                                    dr["Size"] = txtSize.Text;
                                    dr["EquivalentAxialMovement"] = txtEquivalantAxialMovement.Text;
                                    dr["ActualMovement"] = txtActualMovement.Text;
                                    dr["Remarks"] = txtRemarks.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();
                                    dr["MTRIDID"] = 0;
                                    dr["MTRID"] = 0;
                                    dr["BSLNo"] = txtBSLNo.Text;
                                    dr["Size"] = txtSize.Text;
                                    dr["EquivalentAxialMovement"] = txtEquivalantAxialMovement.Text;
                                    dr["ActualMovement"] = txtActualMovement.Text;
                                    dr["Remarks"] = txtRemarks.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvItemDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();
                            dr["MTRIDID"] = 0;
                            dr["MTRID"] = 0;
                            dr["BSLNo"] = "";
                            dr["Size"] = "";
                            dr["EquivalentAxialMovement"] = "";
                            dr["ActualMovement"] = "";
                            dr["Remarks"] = "";
                            dt.Rows.Add(dr);
                        }
                    }

                    gvItemDetails.DataSource = dt;
                    gvItemDetails.DataBind();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Given rows Rows Cant same of Grid view rows');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Please select the Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveMTR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());

            objQt.MTRID = hdnMTRID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RFPNo = txtRFPNo.Text;
            objQt.ItemName = txtItemName.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.Customername = txtCustomername.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.PONo = txtPONo.Text;
            objQt.QAPNo = txtQAPNo.Text;
            objQt.Remarks = txtRemarks.Text;
            objQt.CreatedBy = objSession.employeeid;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("MTRIDID");
            dt.Columns.Add("MTRID");
            dt.Columns.Add("BSLNo");
            dt.Columns.Add("Size");
            dt.Columns.Add("EquivalentAxialMovement");
            dt.Columns.Add("ActualMovement");
            dt.Columns.Add("Remarks");

            foreach (GridViewRow row in gvItemDetails.Rows)
            {
                string MTRDID = gvItemDetails.DataKeys[row.RowIndex].Values[0].ToString();
                string MTRID = gvItemDetails.DataKeys[row.RowIndex].Values[1].ToString();
                TextBox txtBSLNo = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtBSLNo");
                TextBox txtSize = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtSize");
                TextBox txtEquivalantAxialMovement = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtEquivalantAxialMovement");
                TextBox txtActualMovement = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtActualMovement");
                TextBox txtRemark = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtRemarks");

                CheckBox chkpart = (CheckBox)gvItemDetails.Rows[row.RowIndex].FindControl("chkitems");

                if (chkpart.Checked)
                {
                    dr = dt.NewRow();
                    dr["MTRIDID"] = MTRDID;
                    dr["MTRID"] = MTRID;
                    dr["BSLNo"] = txtBSLNo.Text;
                    dr["Size"] = txtSize.Text;
                    dr["EquivalentAxialMovement"] = txtEquivalantAxialMovement.Text;
                    dr["ActualMovement"] = txtActualMovement.Text;
                    dr["Remarks"] = txtRemark.Text;

                    dt.Rows.Add(dr);
                }
            }

            objQt.dt = dt;

            ds = objQt.SaveMovementTestReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Movement Test Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Movement Test Report Updated successfully');", true);
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

    protected void gvMovementTestReport_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string MTRID = gvMovementTestReport.DataKeys[index].Values[0].ToString();
            ViewState["MTRID"] = MTRID;
            if (e.CommandName.ToString() == "Pdf")
            {
                BindMovementTestReportPDFDetails();
            }
            if (e.CommandName.ToString() == "EditMT")
            {
                DataSet ds = new DataSet();
                objQt = new cQuality();

                objQt.MTRID = ViewState["MTRID"].ToString();
                ds = objQt.GetMovementTestReportdetailsByMTRID();

                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString() + "/" + ds.Tables[0].Rows[0]["EDID"].ToString();
                hdnMTRID.Value = ds.Tables[0].Rows[0]["MTRID"].ToString();
                txtReportDate.Text = ds.Tables[0].Rows[0]["EditDate"].ToString();
                txtRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();

                txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                txtCustomername.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                txtJobDescription.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
                txtPONo.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                txtQAPNo.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                gvItemDetails.DataSource = ds.Tables[1];
                gvItemDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindWallThiningReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetMovementtestReportHeaderByRFPHID");
            gvMovementTestReport.DataSource = ds.Tables[0];
            gvMovementTestReport.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindWallThinningItemDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetMovementTestReportDetails");

            gvItemDetails.DataSource = "";
            gvItemDetails.DataBind();

            lblReportNo.Text = ds.Tables[1].Rows[0]["ReportNo"].ToString();
            txtConvolutionOfRecords.Text = ds.Tables[1].Rows[0]["ControlID"].ToString();
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

    private void BindMovementTestReportPDFDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MTRID = ViewState["MTRID"].ToString();
            ds = objQt.GetMovementTestReportdetailsByMTRID();

            //Address	PhoneAndFaxNo	Email	WebSite

            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            lblDate_p.Text = ds.Tables[0].Rows[0]["Date"].ToString();

            lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();

            lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblProject_p.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
            lblDRGNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            lblQAPNo_p.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();

            lblcustomername_p.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();

            gvMovementTestReport_p.DataSource = ds.Tables[1];
            gvMovementTestReport_p.DataBind();

            hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintMovementTestReport();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}