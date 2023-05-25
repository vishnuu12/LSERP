using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_PressureTestReportPrint : System.Web.UI.Page
{
    #region "Declaration"

    cQCTestReports objQCT;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string PTRHID = Request.QueryString["PTRHID"].ToString();
            ViewState["PTRHID"] = PTRHID;
            print();
        }
    }

    #endregion

    #region"Button Events"

    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintTestReport();", true);
    }

    #endregion

    #region "Common Methods"
    private void print()
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            string RIRHID = ViewState["PTRHID"].ToString();
            ds = objQCT.GetQualityTestReportDetailsForPrintByID("LS_GetPressureTestReportPrintDetails", RIRHID);

            lblPTReportHeaderName.Text = "PRESSURE TEST REPORT - " + ds.Tables[0].Rows[0]["TestName"].ToString();

            lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDate"].ToString();
            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            lblcustomername_p.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();
            lblPONo_p.Text = ds.Tables[0].Rows[0]["PORefNo"].ToString();
            lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblresult.Text = ds.Tables[0].Rows[0]["Result"].ToString();
            lblITPNo_p.Text = ds.Tables[0].Rows[0]["QAPRefNo"].ToString();
            lblProject_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();

            lblApprovedTestProcedureNo_p.Text = ds.Tables[0].Rows[0]["TestProcedureNo"].ToString();
            lblTypeOfTest_p.Text = ds.Tables[0].Rows[0]["TestName"].ToString();
            lblMedium_p.Text = ds.Tables[0].Rows[0]["Medium"].ToString();
            lblTestPressure_p.Text = ds.Tables[0].Rows[0]["TestPressure"].ToString();
            lblHoldingTime_p.Text = ds.Tables[0].Rows[0]["HoldingTime"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItemDetails_p.DataSource = ds.Tables[0];
                gvItemDetails_p.DataBind();
            }
            else
            {
                gvItemDetails_p.DataSource = "";
                gvItemDetails_p.DataBind();
            }

            lblcodeNo1.Text = ds.Tables[0].Rows[0]["CodeNo1"].ToString();
            lblrange1.Text = ds.Tables[0].Rows[0]["Range1"].ToString();
            lblcalibrationref1.Text = ds.Tables[0].Rows[0]["CertficateNo1"].ToString();
            lblcalibrationDoneOn1.Text = ds.Tables[0].Rows[0]["CalibrationDoneOn1"].ToString();
            lblcalibrationDueOn1.Text = ds.Tables[0].Rows[0]["CalibrationDue1"].ToString();

            lblcodeNo2.Text = ds.Tables[0].Rows[0]["CodeNo2"].ToString();
            lblrange2.Text = ds.Tables[0].Rows[0]["Range2"].ToString();
            lblcalibrationref2.Text = ds.Tables[0].Rows[0]["CertficateNo2"].ToString();
            lblcalibrationDoneOn2.Text = ds.Tables[0].Rows[0]["CalibrationDoneOn2"].ToString();
            lblCalibrationDueOn2.Text = ds.Tables[0].Rows[0]["CalibrationDue2"].ToString();

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