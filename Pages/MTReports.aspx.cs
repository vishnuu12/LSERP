using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_MTReports : System.Web.UI.Page
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
                bindReportNo();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteMPERReportDetailsbyMPERID", arg.ToString());

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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsBYRFPHIDInMPERReportDetails");

                //  lblTotalItemQty.Text = hdnTotalItemQty.Value = "Total Item Qty" + ds.Tables[0].Rows[0]["QTY"].ToString();

                bindMPERHeader();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }
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
            ////txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBSLNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;
            txtCustomername.Text = ddlCustomerName.SelectedItem.Text;
            txtRFPNo.Text = ddlRFPNo.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveVER_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.MPERID = hdnMPERID.Value;
            objQt.ControlOfRecords = txtConvolutionOfRecords.Text;
            objQt.Customername = txtCustomername.Text;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.RFPNo = txtRFPNo.Text;
            objQt.PONo = txtPONo.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Material = txtMaterial.Text;
            objQt.MagneticParticle = txtMagneticParticle.Text;
            objQt.Thickness = txtThickness.Text;
            objQt.BrandNameAndColor = txtBrandNameAndColor.Text;
            objQt.ProcedureAndRevNo = txtProceduerAndRevNo.Text;
            objQt.TempratureOfThePart = txtTempratureOfThePart.Text;
            objQt.Equipment = txtEqupementUsed.Text;
            objQt.LightingEquipment = txtLightEquipment.Text;
            objQt.Technique = txtTechnique.Text;
            objQt.LightIntensity = txtLightIntensity.Text;
            objQt.LightIntensityMethod = txtLightIntensityMethod.Text;
            objQt.CurrentUsed = txtCurrentUsed.Text;
            objQt.TestSensitivity = txtTestSensitivity.Text;
            objQt.AcceptanceStandard = txtAcceptanceStandard.Text;
            objQt.RecordingOfINdication = txtRecordingOfIndicatiomn.Text;
            objQt.PerformedName = txtPreparedName.Text;
            objQt.PerformedLevel = txtPrefaredLevel.Text;
            objQt.EvaluvatedName = txtEvaluvatedname.Text;
            objQt.EvaluvatedDesignation = txtEvaluvatedDesignation.Text;
            objQt.InspectionAuthorityName = txtAIName.Text;
            objQt.InspectionAuthorityDesignation = txtAIDesignation.Text;
            objQt.CreatedBy = objSession.employeeid;
            objQt.IfSpecifyItemName = txtItemName.Text;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("MPERDID");
            dt.Columns.Add("MPERID");
            dt.Columns.Add("JobIdentification");
            dt.Columns.Add("InterPretation");
            dt.Columns.Add("Evaluvation");

            foreach (GridViewRow row in gvMTRdetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");

                TextBox txtJointIdentification = (TextBox)row.FindControl("txtJointIdentification");
                TextBox txtInterPretation = (TextBox)row.FindControl("txtInterPretation");
                TextBox txtEvaluvation = (TextBox)row.FindControl("txtEvaluvation");
                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["MPERDID"] = gvMTRdetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dr["MPERID"] = gvMTRdetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["JobIdentification"] = txtJointIdentification.Text;
                    dr["InterPretation"] = txtInterPretation.Text;
                    dr["Evaluvation"] = txtEvaluvation.Text;
                    dt.Rows.Add(dr);
                }
            }
            objQt.dt = dt;
            ds = objQt.SaveMTReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','MT Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Error Occcured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvLPIDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindMPERHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetMTReportHeaderByRFPHID");
            gvMTReportHeader.DataSource = ds.Tables[0];
            gvMTReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindReportNo()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetMTreportDetails");
            gvMTRdetails.DataSource = ds.Tables[0];
            gvMTRdetails.DataBind();

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

    #endregion
}