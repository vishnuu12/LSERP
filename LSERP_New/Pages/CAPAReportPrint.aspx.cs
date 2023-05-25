using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_CAPAReportPrint : System.Web.UI.Page
{
    #region "Declaration"

    cReports objReport;

    #endregion

    #region "PageLoad Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["CAPAID"] = Request.QueryString["CAPAID"].ToString();
                BindCAPAPrintReportDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"
    protected void btnprint_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "PrintCAPAReport();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"
    private void BindCAPAPrintReportDetails()
    {
        DataSet ds = new DataSet();
        objReport = new cReports();
        try
        {
            objReport.CAPAID = ViewState["CAPAID"].ToString();
            ds = objReport.GetCAPAReportDetailsByCAPAIDForPrint();

            lblheading_p.Text = "NON CONFORMANCE REPORT";
            //ds.Tables[0].Rows[0]["RFPNo"].ToString() + " " +
            lblNCRNo_p.Text = ds.Tables[0].Rows[0]["NCRNo"].ToString();
            lblNCRdate_p.Text = ds.Tables[0].Rows[0]["NCRDate"].ToString();
            lblinspectionofweldlengthinmeterqty_p.Text = ds.Tables[0].Rows[0]["InspectionOfWelLengthInMeterPerQty"].ToString();
            lblinspectionfromdate_p.Text = ds.Tables[0].Rows[0]["InspectionFromDate"].ToString();
            lblinspectionToDate_p.Text = ds.Tables[0].Rows[0]["InspectionToDate"].ToString();
            lblrepeatedreworklocation_p.Text = ds.Tables[0].Rows[0]["RepeatedReworkLocation"].ToString();
            lblwelddefectsfoundinmeter_p.Text = ds.Tables[0].Rows[0]["WeldDefectsFoundInMeter"].ToString();
            lbldatafrom_p.Text = ds.Tables[0].Rows[0]["DataFrom"].ToString();
            lblrepeatedreworks_p.Text = ds.Tables[0].Rows[0]["RepeatedReworks"].ToString();

            //,DD.DrawingName,,,

            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblitemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString() + "/" + ds.Tables[0].Rows[0]["DrawingName"].ToString()+" / REV. "+ ds.Tables[0].Rows[0]["Version"].ToString();
            lblSize_p.Text = ds.Tables[0].Rows[0]["Size"].ToString();
            lblJobSno_p.Text = ds.Tables[0].Rows[0]["BellowSno"].ToString();
            lblNCSatge_p.Text = ds.Tables[0].Rows[0]["NCStage"].ToString();

            int i = 0;

            lbldescriptionofNonConformity_p.Text = "";
            lbldncdate_p.Text = "";
            lbldncqcengineer_p.Text = "";

            lblcorrection_p.Text = "";
            lblcorrectiondate_p.Text = "";
            lblcorrectionengineername_p.Text = "";

            lblrootcause_p.Text = "";
            lblrootcausedate_p.Text = "";
            lblrootcauseenginner_p.Text = "";

            lblcorrectiveaction_p.Text = "";
            lblcorrectiveactiondate_p.Text = "";
            lblcorrectiveactionengineername_p.Text = "";

            lbleffectivenessofaction_p.Text = "";
            lbleffectivenessofactiondate_p.Text = "";
            lbleffectivenessofactionengineername_p.Text = "";

            lblfinaldisposition_p.Text = "";
            lblfinaldispositiondate_p.Text = "";
            lblfinaldispositionengineername_p.Text = "";

            while (i < ds.Tables[1].Rows.Count)
            {
                if (i == 0)
                {
                    lbldescriptionofNonConformity_p.Text = ds.Tables[1].Rows[0]["Description"].ToString();
                    lbldncdate_p.Text = ds.Tables[1].Rows[0]["ActionDate"].ToString();
                    lbldncqcengineer_p.Text = ds.Tables[1].Rows[0]["InChargeName"].ToString();
                }
                else if (i == 1)
                {
                    lblcorrection_p.Text = ds.Tables[1].Rows[1]["Description"].ToString();
                    lblcorrectiondate_p.Text = ds.Tables[1].Rows[1]["ActionDate"].ToString();
                    lblcorrectionengineername_p.Text = ds.Tables[1].Rows[1]["InChargeName"].ToString();
                }
                else if (i == 2)
                {
                    lblrootcause_p.Text = ds.Tables[1].Rows[2]["Description"].ToString();
                    lblrootcausedate_p.Text = ds.Tables[1].Rows[2]["ActionDate"].ToString();
                    lblrootcauseenginner_p.Text = ds.Tables[1].Rows[2]["InChargeName"].ToString();
                }
                else if (i == 3)
                {
                    lblcorrectiveaction_p.Text = ds.Tables[1].Rows[3]["Description"].ToString();
                    lblcorrectiveactiondate_p.Text = ds.Tables[1].Rows[3]["ActionDate"].ToString();
                    lblcorrectiveactionengineername_p.Text = ds.Tables[1].Rows[3]["InChargeName"].ToString();
                }
                else if (i == 4)
                {
                    lbleffectivenessofaction_p.Text = ds.Tables[1].Rows[4]["Description"].ToString();
                    lbleffectivenessofactiondate_p.Text = ds.Tables[1].Rows[4]["ActionDate"].ToString();
                    lbleffectivenessofactionengineername_p.Text = ds.Tables[1].Rows[4]["InChargeName"].ToString();
                }
                else if (i == 5)
                {
                    lblfinaldisposition_p.Text = ds.Tables[1].Rows[5]["Description"].ToString();
                    lblfinaldispositiondate_p.Text = ds.Tables[1].Rows[5]["ActionDate"].ToString();
                    lblfinaldispositionengineername_p.Text = ds.Tables[1].Rows[5]["InChargeName"].ToString();
                }
                i++;
            }

            hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();
            hdnCompanyName.Value = dtAddress.Rows[0]["CompanyName"].ToString();

            hdnDocNo.Value = ds.Tables[3].Rows[0]["DocNo"].ToString();
            hdnRevNo.Value = ds.Tables[3].Rows[0]["RevNo"].ToString();
            hdnRevDate.Value = ds.Tables[3].Rows[0]["RevDate"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}