using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SheetWeldingCardPrint : System.Web.UI.Page
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','SW');", true);
    }

    private void JobCardPrintDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());
            ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetWeldingDetailsByJCHID_PRINT");

            lblProcessName_SW_P_H.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString() + " / " + ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
            lblJobOrderID_SW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
            lblProcessname_SW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
            ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

            lblDate_SW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
            lblRFPNo_SW_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblContractorName_SW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
            lblContractorTeamname_SW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

            hdnProductionBy.Value = ds.Tables[7].Rows[0]["ProductionBy"].ToString();
            hdnQCDoneBy.Value = ds.Tables[7].Rows[0]["QCDoneBY"].ToString();
            hdnQCDoneOn.Value = ds.Tables[7].Rows[0]["QCDoneOn"].ToString();
            hdnProductionDoneOn.Value = ds.Tables[7].Rows[0]["ProductionDoneOn"].ToString();

            lblItemNameSize_SW_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblDrawingName_SW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
            lblPartname_SW_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
            lblMaterialCategory_SW_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
            lblMaterialGrade_SW_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
            lblThickness_SW_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
            lblMRNNumber_SW_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
            lblJobOrderRemarks_SW_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

            lblRemarks_SW_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
            lblDeadlineDate_SW_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

            lbljobcardstatus_SW_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

            if (ds.Tables[6].Rows.Count > 0)
            {
                gvPartSno_SW_P.DataSource = ds.Tables[6];
                gvPartSno_SW_P.DataBind();
            }

            gvWPSDetails_SW_P.DataSource = ds.Tables[5];
            gvWPSDetails_SW_P.DataBind();

            if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                lblOfferQCTest_SW_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
            else
                lblOfferQCTest_SW_P.Text = "";

            lblTotalCost_SW_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
            lblPartQty_SW_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
            lblNOP_SW_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();
            ViewState["Address"] = ds.Tables[10];

            if (ds.Tables[11].Rows.Count > 0)
            {
                gvbeforewelding_SW_P.DataSource = ds.Tables[11];
                gvbeforewelding_SW_P.DataBind();
            }
            else
            {
                gvbeforewelding_SW_P.DataSource = "";
                gvbeforewelding_SW_P.DataBind();
            }

            if (ds.Tables[12].Rows.Count > 0)
            {
                gvduringwelding_SW_P.DataSource = ds.Tables[12];
                gvduringwelding_SW_P.DataBind();
            }
            else
            {
                gvduringwelding_SW_P.DataSource = "";
                gvduringwelding_SW_P.DataBind();
            }

            if (ds.Tables[13].Rows.Count > 0)
            {
                gvfinalwelding_SW_P.DataSource = ds.Tables[13];
                gvfinalwelding_SW_P.DataBind();
            }
            else
            {
                gvfinalwelding_SW_P.DataSource = "";
                gvfinalwelding_SW_P.DataBind();
            }


            if (objSession.type == 6)
            {
                divtotalcost.Visible = true;
                divcontractor.Visible = true;
            }
            else
            {
                divtotalcost.Visible = false;
                divcontractor.Visible = false;
            }

            lblQCCompletionDate_SW_P.Text = ds.Tables[7].Rows[0]["QCDoneON"].ToString();

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

            hdnDocNo.Value = ds.Tables[14].Rows[0]["DocNo"].ToString();
            hdnRevNo.Value = ds.Tables[14].Rows[0]["RevNo"].ToString();
            hdnRevDate.Value = ds.Tables[14].Rows[0]["RevDate"].ToString();

            if (ds.Tables[15].Rows.Count > 0)
            {
                gvTypeOfCheck_SW_P.DataSource = ds.Tables[15];
                gvTypeOfCheck_SW_P.DataBind();
            }
            else
            {
                gvTypeOfCheck_SW_P.DataSource = "";
                gvTypeOfCheck_SW_P.DataBind();
            }
			
			hdnCheyyarAddress.Value = ds.Tables[16].Rows[0]["CheyyarAddress"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvbeforewelding_SW_P.Rows.Count > 0)
            gvbeforewelding_SW_P.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvduringwelding_SW_P.Rows.Count > 0)
            gvduringwelding_SW_P.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvfinalwelding_SW_P.Rows.Count > 0)
            gvfinalwelding_SW_P.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}