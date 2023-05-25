using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Globalization;

public partial class Pages_VisualExaminationReport : System.Web.UI.Page
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
                bindLPIReportDetails();
            }

            if (target == "deleteLPIRID")
            {
                objQt = new cQuality();
                objQt.LPIRID = arg.ToString();
                DataSet ds = objQt.DeleteLPIRDetailsByLPIRID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    bindVEReportHeader();
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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsBYRFPHIDInVEReportDeatils");

                //  lblTotalItemQty.Text = hdnTotalItemQty.Value = "Total Item Qty" + ds.Tables[0].Rows[0]["QTY"].ToString();

                bindVEReportHeader();
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
            //   ds = objQt.GetItemSpecificationDetailsByRFPDIDAndEDID();

            txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //txtProjectName.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBellowSNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemName.Text = ddlItemName.SelectedItem.Text;

            //objQt.StageOfTest = txtStageOfTest.Text = "Circumferential fillet welding of Duct to bellows andsupport";

            //objQt.MaterialSpecification = txtMaterialSpecification.Text = "A 240 TYP 321,IS 2062 Gr.B";
            //objQt.OtherReference = txtOtherReference.Text = "CHEM JOB NO:PEF-379";

            //objQt.PenetrantBatchNo = txtBatchNo.Text = "sadska";
            //objQt.Thickness = txtThickness.Text = "2 MM";

            //objQt.CleanRemoverBatchNo = txtCleanerBatch.Text = "17E01";
            //objQt.ProcedureAndRevNo = txtProceduerAndRevNo.Text = "sadskad";
            //objQt.DwellTime = txtDwellTime.Text = "10 mins";
            //objQt.SurfaceCondition = txtSurfaceCondition.Text = "oj";

            //objQt.DeveloperBatchNo = txtDeveloperBatchNo.Text = "ok";
            //objQt.SurfaceTemprature = txtSurfaceTemprature.Text = "10 deg";
            //objQt.DevelopementTime = txtDevelopementTime.Text = "110 mins";
            //objQt.PenetrateSystem = txtPenetrateSystem.Text = "lpi";
            //objQt.LightningEquipment = txtLightningEquipment.Text = "HandLamp";
            //objQt.Technique = txtTechnique.Text = "Solvent Removable";
            //objQt.LightIntensity = txtLightIntensity.Text = "140 LUX";
            //objQt.SheetOfIndications = txtSketchOfIndications.Text = "Ok";
            //objQt.InspectionQty = txtInspectionQty.Text = "1";
            //objQt.AcceptedQty = txtAcceptedQty.Text = "1";
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
            objQt.VERID = hdnVERID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Customername = txtCustomername.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.Material = txtMaterial.Text;
            objQt.Thickness = txtThickness.Text;
            objQt.ProcedureAndRevNo = txtProceduerAndRevNo.Text;
            objQt.AcceptanceCriteria = txtAcceptanceCriteria.Text;
            objQt.EquipmentUsed = txtEqupementUsed.Text;
            objQt.LightIntensity = txtLightIntensity.Text;
            objQt.Technique = txtTechnique.Text;
            objQt.QtmOfInspection = txtQtmOfInspection.Text;
            objQt.BSLNo = txtBSLNo.Text;
            objQt.PreparedName = txtPreparedName.Text;
            objQt.PreparedLevel = txtPrefaredLevel.Text;
            objQt.EvaluvatedName = txtEvaluvatedname.Text;
            objQt.EvaluvatedDesignation = txtEvaluvatedDesignation.Text;
            objQt.CustomerTPIAName = txtTPIAName.Text;
            objQt.CustomerTPIADesignation = txtTPIADesignation.Text;
            objQt.AIName = txtAIName.Text;
            objQt.AIDesignation = txtAIDesignation.Text;
            objQt.CreatedBy = objSession.employeeid;
            objQt.IfSpecifyItemName = txtItemName.Text;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("VERDID");
            dt.Columns.Add("VERID");
            dt.Columns.Add("JobIdentification");
            dt.Columns.Add("InterPretation");
            dt.Columns.Add("Result");

            foreach (GridViewRow row in gvVERDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");

                TextBox txtJointIdentification = (TextBox)row.FindControl("txtJointIdentification");
                TextBox txtIndicationType = (TextBox)row.FindControl("txtIndicationType");
                TextBox txtIndicationSize = (TextBox)row.FindControl("txtIndicationSize");
                TextBox txtInterPretation = (TextBox)row.FindControl("txtInterPretation");
                TextBox txtDisposition = (TextBox)row.FindControl("txtDisposition");
                if (chk.Checked)
                {
                    dr = dt.NewRow();
                    dr["VERDID"] = gvVERDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dr["VERID"] = gvVERDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dr["JobIdentification"] = txtJointIdentification.Text;
                    dr["InterPretation"] = txtIndicationType.Text;
                    dr["Result"] = txtIndicationSize.Text;
                    dt.Rows.Add(dr);
                }
            }
            objQt.dt = dt;
            ds = objQt.SaveVisualExaminationReport();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','LPI Report Saved successfully');", true);
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

    private void bindVEReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetVEReportHeaderDetailsByRFPHID");
            gvVEReportHeader.DataSource = ds.Tables[0];
            gvVEReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindLPIReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetVERDetails();
            gvVERDetails.DataSource = ds.Tables[0];
            gvVERDetails.DataBind();

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