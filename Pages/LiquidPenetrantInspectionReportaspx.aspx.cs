using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Globalization;

public partial class Pages_LiquidPenetrantInspectionReportaspx : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cSales objSales;
    cMaterials objMat;
    cQCTestReports objQCTR;

    enum divName { add, input, view };

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
                DataSet ds = new DataSet();
                objc.GetCustomerNameDetailsByQCReport(ddlCustomerName);
                ds = objc.GetRFPNoDetailsByQCReport(ddlRFPNo);

                ViewState["RFP"] = ds.Tables[0];

                ShowHideControls("add");
                // bindLPIReportDetails();
            }

            if (target == "deleteVERID")
            {
                objQCTR = new cQCTestReports();
                DataSet ds = objQCTR.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteLPIReportDetailsByLPIRID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Deleted successfully');", true);
                    BindLPIReportDetailsByRFPHID();
                }
            }

            if (target == "Print")
            {
                string RIRHID = arg.ToString();
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "LPIReportPrint.aspx?RIRHID=" + RIRHID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"   

    protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["RFP"];
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

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            //ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            ddlCustomerName.SelectedIndex = 0;
            if (ddlRFPNo.SelectedIndex > 0)
            {
                cCommon objc = new cCommon();
                objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objc.GetCustomerNameDetailsByRFPHID();
                ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

                BindItemDetailsByRFPHID();
                BindLPIReportDetailsByRFPHID();
                ShowHideControls("add,view");
            }
            else
                ShowHideControls("add");
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
            if (ddlItemName.SelectedIndex > 0)
                ShowHideControls("add,view,input");
            else
                ShowHideControls("add,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlPenetrantBrand_SelectIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlCleanerBrand_SelectIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlDeveloperBrand_SelectIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion

    #region"Button Events"

    protected void btnSaveLPIR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQCTR = new cQCTestReports();
        try
        {
            objQCTR.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQCTR.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue);
            objQCTR.LPIRID = Convert.ToInt32(hdnLPIRID.Value);
            objQCTR.BellowSNo = txtBellowSNo.Text;
            objQCTR.StageOfTest = txtStageOfTest.Text;
            objQCTR.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQCTR.MaterialSpecification = txtMaterialSpecification.Text;
            objQCTR.PenetrantBrandName = txtPenetrantbrand.Text;
            objQCTR.PenetrantBatchNo = txtBatchNo.Text;
            objQCTR.Thickness = txtThickness.Text;
            objQCTR.CleanRemoverBrandName = txtCleanerRemoverBrand.Text;
            objQCTR.CleanRemoverBatchNo = txtCleanerBatch.Text;
            objQCTR.ProcedureAndRevNo = txtProceduerAndRevNo.Text;
            objQCTR.DwellTime = txtDwellTime.Text;
            objQCTR.SurfaceCondition = txtSurfaceCondition.Text;
            objQCTR.DeveloperBrandName = txtDeveloperBrand.Text;
            objQCTR.DeveloperBatchNo = txtDeveloperBatchNo.Text;
            objQCTR.SurfaceTemprature = txtSurfaceTemprature.Text;
            objQCTR.DevelopementTime = txtDevelopementTime.Text;
            objQCTR.PenetrateSystem = txtPenetrateSystem.Text;
            objQCTR.LightningEquipment = txtLightningEquipment.Text;
            objQCTR.Technique = txtTechnique.Text;
            objQCTR.LightIntensity = txtLightIntensity.Text;
            objQCTR.SheetOfIndications = txtSketchOfIndications.Text;
            objQCTR.InspectionQty = txtInspectionQty.Text;
            objQCTR.AcceptedQty = txtAcceptedQty.Text;
            objQCTR.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objQCTR.JointIdentification = txtJointidentification.Text;
            objQCTR.IndicationType = txtIdentificationType.Text;
            objQCTR.inditicationSize = txtindicationsize.Text;
            objQCTR.Interpretaion = txtinterpretaion.Text;
            objQCTR.Disposition = txtDisposition.Text;
            objQCTR.LSIInspectorName = txtLSIInspectorName.Text;
            objQCTR.LSIInspectorLevel = txtLSIInspectionLevel.Text;
            objQCTR.LSIInspectionDate = DateTime.ParseExact(txtLSIInspectionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQCTR.ThirdPartyInspectorname = txtthirdPartyInspectorName.Text;
            objQCTR.ThirdPartyInspectionLevel = txtThirdPartyInspectorLevelName.Text;
            objQCTR.ThirdPartyInspectionDate = DateTime.ParseExact(txtThirdPartyInspectorDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objQCTR.SaveLPIReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','LPI Report Saved successfully');", true);
                BindLPIReportDetailsByRFPHID();
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','LPI Report Updated successfully');", true);
                BindLPIReportDetailsByRFPHID();
            }
            btnCancel_Click(null, null);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Error Occcured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnLPIRID.Value = "0";
        txtTestDate.Text = "";
        ShowHideControls("add,view");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "clearFields();", true);
    }

    #endregion

    #region"GridView Events"

    protected void gvLPIHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string LPIRID = gvLPIHeader.DataKeys[index].Values[0].ToString();
            ViewState["LPIRID"] = LPIRID;
            hdnLPIRID.Value = LPIRID;

            if (e.CommandName.ToString() == "LPIPRINT")
            {
                BindLPIPrintdetails();
            }
            if (e.CommandName.ToString() == "EditLPI")
            {
                DataSet ds = new DataSet();
                objQCTR = new cQCTestReports();

                objQCTR.LPIRID = Convert.ToInt32(ViewState["LPIRID"].ToString());
                ds = objQCTR.GetLPIDetailsLPIRID();

                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString();
                hdnLPIRID.Value = ds.Tables[0].Rows[0]["LPIRID"].ToString();
                txtBellowSNo.Text = ds.Tables[0].Rows[0]["BellowSNo"].ToString();
                txtStageOfTest.Text = ds.Tables[0].Rows[0]["StageOfTest"].ToString();
                txtTechnique.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                txtMaterialSpecification.Text = ds.Tables[0].Rows[0]["MaterialSpecification"].ToString();
                txtPenetrantbrand.Text = ds.Tables[0].Rows[0]["PenetrantBrandName"].ToString();

                txtTestDate.Text = ds.Tables[0].Rows[0]["TestDateEdit"].ToString();

                txtBatchNo.Text = ds.Tables[0].Rows[0]["PenetrantBatchNo"].ToString();
                txtThickness.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();
                txtCleanerRemoverBrand.Text = ds.Tables[0].Rows[0]["CleanRemoverBrandName"].ToString();
                txtCleanerBatch.Text = ds.Tables[0].Rows[0]["CleanRemoverBatchNo"].ToString();
                txtProceduerAndRevNo.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
                txtDwellTime.Text = ds.Tables[0].Rows[0]["DwellTime"].ToString();
                txtSurfaceCondition.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();
                txtSurfaceTemprature.Text = ds.Tables[0].Rows[0]["SurfaceTemprature"].ToString();
                txtDeveloperBrand.Text = ds.Tables[0].Rows[0]["DeveloperBrandName"].ToString();
                txtDeveloperBatchNo.Text = ds.Tables[0].Rows[0]["DeveloperBatchNo"].ToString();
                txtDevelopementTime.Text = ds.Tables[0].Rows[0]["DevelopementTime"].ToString();
                txtPenetrateSystem.Text = ds.Tables[0].Rows[0]["PenetrateSystem"].ToString();
                txtLightningEquipment.Text = ds.Tables[0].Rows[0]["LightningEquipment"].ToString();

                txtTechnique.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
                txtLightIntensity.Text = ds.Tables[0].Rows[0]["LightIntensity"].ToString();
                txtSketchOfIndications.Text = ds.Tables[0].Rows[0]["SheetOfIndications"].ToString();
                txtInspectionQty.Text = ds.Tables[0].Rows[0]["InspectionQty"].ToString();
                txtAcceptedQty.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();

                txtJointidentification.Text = ds.Tables[0].Rows[0]["JointIdentification"].ToString();

                txtIdentificationType.Text = ds.Tables[0].Rows[0]["IndicationType"].ToString();
                txtindicationsize.Text = ds.Tables[0].Rows[0]["inditicationSize"].ToString();
                txtinterpretaion.Text = ds.Tables[0].Rows[0]["Interpretaion"].ToString();
                txtDisposition.Text = ds.Tables[0].Rows[0]["Disposition"].ToString();
                txtLSIInspectorName.Text = ds.Tables[0].Rows[0]["LSIInspectorName"].ToString();
                txtLSIInspectionLevel.Text = ds.Tables[0].Rows[0]["LSIInspectorLevel"].ToString();
                txtLSIInspectionDate.Text = ds.Tables[0].Rows[0]["LSIInspectionDateEdit"].ToString();
                txtthirdPartyInspectorName.Text = ds.Tables[0].Rows[0]["ThirdPartyInspectorname"].ToString();
                txtThirdPartyInspectorLevelName.Text = ds.Tables[0].Rows[0]["ThirdPartyInspectionLevel"].ToString();
                txtThirdPartyInspectorDate.Text = ds.Tables[0].Rows[0]["ThirdPartyInspectionDateEdit"].ToString();
                ShowHideControls("add,view,input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"
    private void BindItemDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objc.GetItemNameDetailsByRFPHIDQCReport(ddlItemName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindLPIPrintdetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.LPIRID = ViewState["LPIRID"].ToString();
            ds = objQt.GetLPIPrintdetailsByLPIRID();

            lblCustomerName_p.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
            lblReportNo_p.Text = "<label> Report No: </label>" + ds.Tables[0].Rows[0]["ReportNo"].ToString();
            lblReportdate_p.Text = "'<label> Report Date: </label>'" + ds.Tables[0].Rows[0]["Reportdateedit"].ToString();

            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblCustomerPONo_p.Text = ds.Tables[0].Rows[0]["CustomerPONo"].ToString();

            lblItemName_p.Text = ds.Tables[0].Rows[0]["IfSpecifyItemName"].ToString();
            lblJobsize_p.Text = ds.Tables[0].Rows[0]["JobSize"].ToString();
            lblDrawingNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            lblProjectName_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();

            lblStageoftest_p.Text = ds.Tables[0].Rows[0]["StageOfTest"].ToString();
            lblBSLNo_p.Text = ds.Tables[0].Rows[0]["BellowSNo"].ToString();
            lblTestDate_p.Text = ds.Tables[0].Rows[0]["TestDateView"].ToString();

            lblMaterialSpecification_p.Text = ds.Tables[0].Rows[0]["MaterialSpecification"].ToString();
            lblPenetrantbrand_p.Text = "<label> Brand: </label>" + ds.Tables[0].Rows[0]["PenetrantBrandNo"].ToString();
            lblPenetrantbatchno_p.Text = "<label> Batch No: </label>" + ds.Tables[0].Rows[0]["PenetrantBatchNo"].ToString();

            lblThickness_p.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();
            lblcleanerbrandno_p.Text = "<label> Brand: </label>" + ds.Tables[0].Rows[0]["CleanerBrandNo"].ToString();
            lblcleanerbatchno_p.Text = "<label> Batch No: </label>" + ds.Tables[0].Rows[0]["CleanRemoverBatchNo"].ToString();

            lblProcedureAndRevNo_p.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
            lblDwellTime_p.Text = ds.Tables[0].Rows[0]["DwellTime"].ToString();

            lblSurfacecondition_p.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();

            lbldeveloperbrand_p.Text = "<label> Brand: </label>" + ds.Tables[0].Rows[0]["DeveloperBrandNo"].ToString();
            lbldeveloperbatchno_p.Text = "<label> Batch No: </label>" + ds.Tables[0].Rows[0]["DeveloperBatchNo"].ToString();

            lblsurfacetemp_p.Text = ds.Tables[0].Rows[0]["SurfaceTemprature"].ToString();
            lbldevelopementtime_p.Text = ds.Tables[0].Rows[0]["DevelopementTime"].ToString();

            lblpenetrantsystem_p.Text = ds.Tables[0].Rows[0]["PenetrateSystem"].ToString();
            lbllightingequipment_p.Text = ds.Tables[0].Rows[0]["LightningEquipment"].ToString();

            lbltechnique_p.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
            lbllightintensity_p.Text = ds.Tables[0].Rows[0]["LightIntensity"].ToString();

            lblsketchofindications_p.Text = ds.Tables[0].Rows[0]["SheetOfIndications"].ToString();
            lblInspectionQty_p.Text = ds.Tables[0].Rows[0]["InspectionQty"].ToString();
            lblAcceptedQty_p.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvlpireport_p.DataSource = ds.Tables[1];
                gvlpireport_p.DataBind();
            }
            else
            {
                gvlpireport_p.DataSource = "";
                gvlpireport_p.DataBind();
            }

            hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintLiquidPenetrantInspectionReport();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindLPIReportHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetLPIReportHeaderByRFPHID");
            gvLPIHeader.DataSource = ds.Tables[0];
            gvLPIHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //private void bindLPIReportDetails()
    //{
    //    DataSet ds = new DataSet();
    //    objQt = new cQuality();
    //    try
    //    {
    //        ds = objQt.GetLPIReportDetailsByRFPHID();
    //        gvLPIDetails.DataSource = ds.Tables[0];
    //        gvLPIDetails.DataBind();

    //        //lblReportNo.Text = ds.Tables[1].Rows[0]["ReportNo"].ToString();
    //        //txtConvolutionOfRecords.Text = ds.Tables[1].Rows[0]["ControlID"].ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}   

    private void ShowHideControls(string divids)
    {
        divInput.Visible = divAdd.Visible = divOutPut.Visible = false;
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
                    case "add":
                        divAdd.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindLPIReportDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        objQCTR = new cQCTestReports();
        try
        {
            objQCTR.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQCTR.GetLPIreportDetailsByRFPHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvLPIHeader.DataSource = ds.Tables[0];
                gvLPIHeader.DataBind();
            }
            else
            {
                gvLPIHeader.DataSource = "";
                gvLPIHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}