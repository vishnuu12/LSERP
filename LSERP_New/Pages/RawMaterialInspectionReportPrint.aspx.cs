using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RawMaterialInspectionReportPrint : System.Web.UI.Page
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintRawMIRReport();", true);
    }

    #endregion

    #region "Common Methods"
    private void print()
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            string RIRHID = ViewState["RIRHID"].ToString();
            ds = objQCT.GetQualityTestReportDetailsForPrintByID("LS_GetRawMaterialInspectionReportDetailsPrint", RIRHID);

            lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDate"].ToString();
            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            lblcustomername_p.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();
            lblPONO_p.Text = ds.Tables[0].Rows[0]["PORefNo"].ToString();
            lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblSize_p.Text = ds.Tables[0].Rows[0]["Size"].ToString();
            lblDrawingNo_p.Text = ds.Tables[0].Rows[0]["DrawingName"].ToString();
            lblBellowSno_p.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
            lblQuantity_p.Text = ds.Tables[0].Rows[0]["QTY"].ToString();
            lblRemarks_p.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            lblQAPNo_p.Text = ds.Tables[0].Rows[0]["QAPRefNo"].ToString();
            lblprojectname_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();

            gvRawMIRDetails_p.DataSource = ds.Tables[1];
            gvRawMIRDetails_p.DataBind();

            hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}