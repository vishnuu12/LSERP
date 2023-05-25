using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MarkingAndCuttingPrint : System.Web.UI.Page
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','MC');", true);
    }

    private void JobCardPrintDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            divQCDimension.Visible = false;

            objP.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());
            ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetMarkingAndCuttingDetailsByJCHID_PRINT");

            lblProcessNameHeader_MC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString() + " / " + ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
            lblJobOrderID_MC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
            lblProcessName_MC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
            ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

            lblDate_MC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
            lblRFPNo_MC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();

            lblContractorName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
            lblContractorTeamMemberName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

            hdnProductionBy.Value = ds.Tables[7].Rows[0]["ProductionBy"].ToString();
            hdnQCDoneBy.Value = ds.Tables[7].Rows[0]["QCDoneBY"].ToString();
            hdnQCDoneOn.Value = ds.Tables[7].Rows[0]["QCDoneOn"].ToString();
            hdnProductionDoneOn.Value = ds.Tables[7].Rows[0]["ProductionDoneOn"].ToString();

            lblItemNameSize_MC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblDrawingname_MC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
            lblPartName_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();

            lblMaterialCategory_MC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
            lblmaterialGrade_MC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
            lblThickness_MC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
            lblMRNNumber_MC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
            lblJobOrderRemarks_MC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

            lblOverallremarks_MC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
            lblDeadlineDate_MC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

            lbljobcardStatus_MC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

            if (ds.Tables[6].Rows.Count > 0)
            {
                gvPartSerialNo_MC_P.DataSource = ds.Tables[6];
                gvPartSerialNo_MC_P.DataBind();
            }

            string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
            string result = "";

            lblFabricationType_MC_P.Text = ds.Tables[5].Rows[0]["FabricationTypeName"].ToString();

            foreach (DataRow dr in ds.Tables[5].Rows)
            {
                result = "";
                result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                sb.Append(result);
            }

            divfabrication_MC_P.InnerHtml = sb.ToString();

            if (ds.Tables[9].Rows.Count > 0)
            {
                gvIssueDetails_MC_P.DataSource = ds.Tables[9];
                gvIssueDetails_MC_P.DataBind();
            }

            if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                lblOfferQC_MC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
            else
                lblOfferQC_MC_P.Text = "";

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

            lblTotalCost_MC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
            lblPartQty_MC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

            lblQCCompletionDate_MC_P.Text = ds.Tables[7].Rows[0]["QCDoneON"].ToString();

            ViewState["Address"] = ds.Tables[10];

            gvQCObservationDetails_MC_P.DataSource = ds.Tables[11];
            gvQCObservationDetails_MC_P.DataBind();

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

            //DNLID DocNo RevNo RevDate
            hdnDocNo.Value = ds.Tables[12].Rows[0]["DocNo"].ToString();
            hdnRevNo.Value = ds.Tables[12].Rows[0]["RevNo"].ToString();
            hdnRevDate.Value = ds.Tables[12].Rows[0]["RevDate"].ToString();

            if (ds.Tables[13].Rows.Count > 0)
            {
                gvTypeOfCheck_MC_P.DataSource = ds.Tables[13];
                gvTypeOfCheck_MC_P.DataBind();
            }
            else
            {
                gvTypeOfCheck_MC_P.DataSource = "";
                gvTypeOfCheck_MC_P.DataBind();
            }

            if (ds.Tables[14].Rows[0]["Name"].ToString() != "NOQC")
            {
                divQCDimension.Visible = true;

                gvJobCardQCDimensionDetails.DataSource = ds.Tables[14];
                gvJobCardQCDimensionDetails.DataBind();
            }

            hdnCheyyarAddress.Value = ds.Tables[15].Rows[0]["CheyyarAddress"].ToString();
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
        if (gvQCObservationDetails_MC_P.Rows.Count > 0)
            gvQCObservationDetails_MC_P.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion 
}