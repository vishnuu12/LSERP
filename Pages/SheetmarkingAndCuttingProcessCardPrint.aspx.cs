using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SheetmarkingAndCuttingProcessCardPrint : System.Web.UI.Page
{
    #region"Declaretion"

    cProduction objP;
    cSession objSession;

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
            if (!IsPostBack)
            {
                ViewState["JCHID"] = Request.QueryString["JCHID"].ToString();
                JobCardPrintDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','SMC');", true);
    }

    private void JobCardPrintDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());
            ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetMarkingAndCuttingDetailsByJCHID_PRINT");

            lblProcessName_SMC_P_H.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString() + " / " + ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
            lblJobOrderID_SMC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
            lblProcessname_SMC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
            ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

            lblDate_SMC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
            lblRFPNo_SMC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblContractorName_SMC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
            lblContractorTeamname_SMC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

            hdnProductionBy.Value = ds.Tables[7].Rows[0]["ProductionBy"].ToString();
            hdnQCDoneBy.Value = ds.Tables[7].Rows[0]["QCDoneBY"].ToString();
            hdnQCDoneOn.Value = ds.Tables[7].Rows[0]["QCDoneOn"].ToString();
            hdnProductionDoneOn.Value = ds.Tables[7].Rows[0]["ProductionDoneOn"].ToString();

            lblItemNameSize_SMC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblDrawingName_SMC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
            lblPartname_SMC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
            lblMaterialCategory_SMC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
            lblMaterialGrade_SMC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
            lblThickness_SMC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
            lblMRNNumber_SMC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
            lblJobOrderRemarks_SMC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();
            lblTubeID_SMC_P.Text = ds.Tables[7].Rows[0]["TubeID"].ToString();
            lblTubeLength_SMC_P.Text = ds.Tables[7].Rows[0]["TubeLength"].ToString();

            lblOverAllRemarks_SMC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
            lbldeadlineDate_SMC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();
            lbljobcardstatus_SMC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();
       

            if (ds.Tables[6].Rows.Count > 0)
            {
                gvPartSno_SMC_P.DataSource = ds.Tables[6];
                gvPartSno_SMC_P.DataBind();
            }

            gvPLYDetails_SMC_P.DataSource = ds.Tables[5];
            gvPLYDetails_SMC_P.DataBind();

            if (ds.Tables[9].Rows.Count > 0)
            {
                gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                gvMRNIssueDetails_SMC_P.DataBind();
            }

            if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                lblOfferQCTest_SMC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
            else
                lblOfferQCTest_SMC_P.Text = "";

            lblTotalCost_SMC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
            if (objSession.type == 6)
            {
                divcontractor.Visible = true;
                divtotalcost.Visible = true;
            }
            else
            {
                divcontractor.Visible = false;
                divtotalcost.Visible = false;
            }

            lblPartQty_SMC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

            lblQCCompletionDate_SMC_P.Text = ds.Tables[7].Rows[0]["QCDoneON"].ToString();

            ViewState["Address"] = ds.Tables[10];

            gvQCObservationdetails_SMC_P.DataSource = ds.Tables[11];
            gvQCObservationdetails_SMC_P.DataBind();

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                ViewState["QrCode"] = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = ds.Tables[0].Rows[0]["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                ViewState["QrCode"] = "";

            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
            hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

            hdnDocNo.Value = ds.Tables[12].Rows[0]["DocNo"].ToString();
            hdnRevNo.Value = ds.Tables[12].Rows[0]["RevNo"].ToString();
            hdnRevDate.Value = ds.Tables[12].Rows[0]["RevDate"].ToString();

            if (ds.Tables[13].Rows.Count > 0)
            {
                gvTypeOfCheck_SMC_P.DataSource = ds.Tables[13];
                gvTypeOfCheck_SMC_P.DataBind();
            }
            else
            {
                gvTypeOfCheck_SMC_P.DataSource = "";
                gvTypeOfCheck_SMC_P.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
		
            hdnCheyyarAddress.Value = ds.Tables[14].Rows[0]["CheyyarAddress"].ToString();
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvQCObservationdetails_SMC_P.Rows.Count > 0)
            gvQCObservationdetails_SMC_P.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion 
}