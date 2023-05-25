using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_LPIReportPrint : System.Web.UI.Page
{
    #region "Declaration"

    cQCTestReports objQCT;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string RIRHID = Request.QueryString["RIRHID"].ToString();
            ViewState["RIRHID"] = RIRHID;
            print();
        }
    }

    #endregion

    #region"Button Events"

    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Print();", true);
    }

    #endregion

    #region "Common Methods"
    private void print()
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            string LPIRID = ViewState["RIRHID"].ToString();
            ds = objQCT.GetQualityTestReportDetailsForPrintByID("LS_GetLPIDetailsByLPIRID_Print", LPIRID);

            lblRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblSize.Text = ds.Tables[0].Rows[0]["Size"].ToString();
            lblDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingName"].ToString();
            lblprojectname.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
            lblQAPNo.Text = ds.Tables[0].Rows[0]["QAPRefNo"].ToString();
            lblcustomername.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();

            lblReportNo.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            lblreportDate.Text = ds.Tables[0].Rows[0]["ReportDateEdit"].ToString();

            lblBSLNo.Text = ds.Tables[0].Rows[0]["BellowSNo"].ToString();
            lblstageoftest.Text = ds.Tables[0].Rows[0]["StageOfTest"].ToString();
            lbltechnique.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
            lblmaterialspecification.Text = ds.Tables[0].Rows[0]["MaterialSpecification"].ToString();
            lblpenetrantbrandNo.Text = ds.Tables[0].Rows[0]["PenetrantBrandName"].ToString();
            lbltestdate.Text = ds.Tables[0].Rows[0]["TestDate"].ToString();
            lblpenetrantbatchno.Text = ds.Tables[0].Rows[0]["PenetrantBatchNo"].ToString();
            lblthickness.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();
            lblcleanerbrandNo.Text = ds.Tables[0].Rows[0]["CleanRemoverBrandName"].ToString();
            lblcleanerbatchNo.Text = ds.Tables[0].Rows[0]["CleanRemoverBatchNo"].ToString();
            lblprocedurerevNo.Text = ds.Tables[0].Rows[0]["ProcedureAndRevNo"].ToString();
            lblDwelltime.Text = ds.Tables[0].Rows[0]["DwellTime"].ToString();
            lblsurfacecondition.Text = ds.Tables[0].Rows[0]["SurfaceCondition"].ToString();
            lblsurfacetemp.Text = ds.Tables[0].Rows[0]["SurfaceTemprature"].ToString();
            lbldeveloperbrandNo.Text = ds.Tables[0].Rows[0]["DeveloperBrandName"].ToString();
            lbldeveloperbatchNo.Text = ds.Tables[0].Rows[0]["DeveloperBatchNo"].ToString();
            lbldevelopementtime.Text = ds.Tables[0].Rows[0]["DevelopementTime"].ToString();
            lblpenetrantsystem.Text = ds.Tables[0].Rows[0]["PenetrateSystem"].ToString();
            lbllightingequipement.Text = ds.Tables[0].Rows[0]["LightningEquipment"].ToString();
            lbltechnique.Text = ds.Tables[0].Rows[0]["Technique"].ToString();
            lbllightintensity.Text = ds.Tables[0].Rows[0]["LightIntensity"].ToString();

            lbljointidentification.Text = ds.Tables[0].Rows[0]["JointIdentification"].ToString();
            lblindicationtype.Text = ds.Tables[0].Rows[0]["IndicationType"].ToString();
            lblindicationsize.Text = ds.Tables[0].Rows[0]["inditicationSize"].ToString();
            lblinterpretation.Text = ds.Tables[0].Rows[0]["Interpretaion"].ToString();
            lblDisposition.Text = ds.Tables[0].Rows[0]["Disposition"].ToString();

            lblSketchofindications.Text = ds.Tables[0].Rows[0]["SheetOfIndications"].ToString();
            lblinspectionQTY.Text = ds.Tables[0].Rows[0]["InspectionQty"].ToString();
            lblacceptedQTY.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();

            lblLSIInspectorName.Text = ds.Tables[0].Rows[0]["LSIInspectorName"].ToString();
            lblLSIInspectionLevel.Text = ds.Tables[0].Rows[0]["LSIInspectorLevel"].ToString();
            lblLSIInspectionDate.Text = ds.Tables[0].Rows[0]["LSIInspectionDateEdit"].ToString();
            lblThirdPartyInspectorName.Text = ds.Tables[0].Rows[0]["ThirdPartyInspectorname"].ToString();
            lblThirdPartyInspectionLevel.Text = ds.Tables[0].Rows[0]["ThirdPartyInspectionLevel"].ToString();
            lblthirdPartyInspectionDate.Text = ds.Tables[0].Rows[0]["ThirdPartyInspectionDate"].ToString();

            hdnAddress.Value = ds.Tables[1].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[1].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[1].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[1].Rows[0]["WebSite"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}