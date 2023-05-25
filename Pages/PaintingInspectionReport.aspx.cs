using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PaintingInspectionReport : System.Web.UI.Page
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
                bindPaintingObservationDetails();
                ShowHideControls("input");

                //  bindRGSReportDetails();
            }

            if (target == "deletePIRHID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeletePaintingInspectionReportHeaderByPIRHID", arg.ToString());

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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByPaintingInspectionReportHeader");

                bindPaintingInspectionReportDetails();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }

            hdnPIRHID.Value = "0";
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
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSavePIR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.PIRHID = hdnPIRHID.Value;
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
            objQt.TestProcedureNo = txtTestProcedureNo.Text;
            objQt.CustomerSpecification = txtCustomerSpecification.Text;
            objQt.MethodOfCleaning = txtMethodOfCleaning.Text;
            objQt.VisualInspection = txtVisualInspection.Text;
            objQt.Remarks = txtRemarks.Text;
            objQt.CreatedBy = objSession.employeeid;

            //TestProcedureNo
            //CustomerSpecification
            //MethodOfCleaning
            //VisualInspection
            //Remarks

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("PIRDID");
            dt.Columns.Add("PIRHID");
            dt.Columns.Add("PIRDDID");
            dt.Columns.Add("PrimerCoat");
            dt.Columns.Add("TopCoat");

            foreach (GridViewRow row in gvPaintingObservationDetails.Rows)
            {
                string PIRDDID = gvPaintingObservationDetails.DataKeys[row.RowIndex].Values[0].ToString();
                TextBox txtPrimerCoat = (TextBox)gvPaintingObservationDetails.Rows[row.RowIndex].FindControl("txtPrimerCoat");
                TextBox txtTopCoat = (TextBox)gvPaintingObservationDetails.Rows[row.RowIndex].FindControl("txtTopCoat");

                dr = dt.NewRow();
                dr["PIRDID"] = 0;
                dr["PIRHID"] = 0;
                dr["PIRDDID"] = PIRDDID;
                dr["PrimerCoat"] = txtPrimerCoat.Text;
                dr["TopCoat"] = txtTopCoat.Text;

                dt.Rows.Add(dr);
            }

            objQt.dt = dt;

            ds = objQt.SavePaintingInspectionReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Painting Inspection Test Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Painting Inspection  Test Report Updated successfully');", true);
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

    protected void gvPaintingInspectionReportHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PIRHID = gvPaintingInspectionReportHeader.DataKeys[index].Values[0].ToString();
            objQt.PIRHID = PIRHID;
            ds = objQt.GetPaintingInspectionReportDetailsByPIRHID();

            if (e.CommandName == "EditPIR")
            {
                hdnPIRHID.Value = ds.Tables[0].Rows[0]["PIRHID"].ToString();
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

                txtTestProcedureNo.Text = ds.Tables[0].Rows[0]["TestProcedureNo"].ToString();
                txtCustomerSpecification.Text = ds.Tables[0].Rows[0]["CustomerSpecification"].ToString();
                txtMethodOfCleaning.Text = ds.Tables[0].Rows[0]["MethodOfCleaning"].ToString();
                txtVisualInspection.Text = ds.Tables[0].Rows[0]["VisualInspection"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                gvPaintingObservationDetails.DataSource = ds.Tables[1];
                gvPaintingObservationDetails.DataBind();
            }
            if (e.CommandName.ToString() == "pdfPIR")
            {
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblProject_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                lblITPNo_p.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                lblcustomername_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();

                lblProcedureNo_p.Text = ds.Tables[0].Rows[0]["TestProcedureNo"].ToString();
                lblCustomerSpecification_p.Text = ds.Tables[0].Rows[0]["CustomerSpecification"].ToString();
                lblMethodofcleaning_p.Text = ds.Tables[0].Rows[0]["MethodOfCleaning"].ToString();
                lblvisualinspection_p.Text = ds.Tables[0].Rows[0]["VisualInspection"].ToString();
                lblremarks_p.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                gvpaintingObservationdetails_p.DataSource = ds.Tables[1];
                gvpaintingObservationdetails_p.DataBind();

                gvItemDetails_p.DataSource = ds.Tables[2];
                gvItemDetails_p.DataBind();

                hdnAddress.Value = ds.Tables[3].Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = ds.Tables[3].Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = ds.Tables[3].Rows[0]["Email"].ToString();
                hdnWebsite.Value = ds.Tables[3].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintPaintingInspectionReport();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindPaintingObservationDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetPaintingObservationDescriptionDetails();

            gvPaintingObservationDetails.DataSource = ds.Tables[0];
            gvPaintingObservationDetails.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindPaintingInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetPaintingInspectionReportHeaderByRFPHID");
            gvPaintingInspectionReportHeader.DataSource = ds.Tables[0];
            gvPaintingInspectionReportHeader.DataBind();
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
            //   lblDRGNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //    lblQAPNo_p.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();

            lblcustomername_p.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();

            gvItemDetails_p.DataSource = ds.Tables[1];
            gvItemDetails_p.DataBind();

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