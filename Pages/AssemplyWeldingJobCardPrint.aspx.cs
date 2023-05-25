using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AssemplyWeldingJobCardPrint : System.Web.UI.Page
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','FW');", true);
    }

    private void JobCardPrintDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());
            ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetAssemplyWeldingPartToPartJobCardDetailsByJCHID_Print");

            lblProcessName_FW_P_H.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
            lblJobOrderID_FW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
            lblProcessname_FW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
            ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

            lblDate_FW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
            lblRFPNo_FW_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblContractorName_FW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
            lblContractorTeamname_FW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

            lblItemNameSize_FW_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblDrawingName_FW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
            lblPartname_FW_P.Text = "Sub Assemply Welding";

            // lblMaterialCategory_FW_P.Text = ""; //ds.Tables[1].Rows[0]["CategoryName"].ToString();
            // lblMaterialGrade_FW_P.Text = ""; //ds.Tables[1].Rows[0]["GradeName"].ToString();
            // lblThickness_FW_P.Text = ""; //ds.Tables[1].Rows[0]["THKValue"].ToString();

            //lblMRNNumber_FW_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
            lblJobOrderRemarks_FW_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

            lblRemarks_FW_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
            lblDeadLineDate_FW_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();

            lbljobcardStatus_FW_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

            //if (ds.Tables[6].Rows.Count > 0)
            //{
            //   gvPartSno_FW_P.DataSource = ds.Tables[6];
            //   gvPartSno_FW_P.DataBind();
            //}

            gvWPSDetails_FW_P.DataSource = ds.Tables[5];
            gvWPSDetails_FW_P.DataBind();

            if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                lblOfferQCTest_FW_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
            else
                lblOfferQCTest_FW_P.Text = "";

            lblTotalCost_FW_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
            lblPartQty_FW_P.Text = ds.Tables[3].Rows[0]["Itemqty"].ToString();
            // lblNOP_FW_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();
            ViewState["Address"] = ds.Tables[10];

            //if (ds.Tables[9].Rows.Count > 0)
            //{
            //    string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
            //    string result = "";

            //    lblFabricationType_FW_P.Text = ds.Tables[9].Rows[0]["FabricationTypeName"].ToString();

            //    foreach (DataRow dr in ds.Tables[9].Rows)
            //    {
            //        result = "";
            //        result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
            //        sb.Append(result);
            //    }

            //    divfabrication_FW_P.InnerHtml = sb.ToString();
            //}
            //else
            //    divfabrication_FW_P.InnerHtml = "";

            if (ds.Tables[9].Rows.Count > 0)
            {
                gvBoughtOutItemIssuedDetails_FW_P.DataSource = ds.Tables[9];
                gvBoughtOutItemIssuedDetails_FW_P.DataBind();
            }
            else
            {
                gvBoughtOutItemIssuedDetails_FW_P.DataSource = "";
                gvBoughtOutItemIssuedDetails_FW_P.DataBind();
            }

            if (ds.Tables[11].Rows.Count > 0)
            {
                gvBeforeWelding_FW_P.DataSource = ds.Tables[11];
                gvBeforeWelding_FW_P.DataBind();
            }
            else
            {
                gvBeforeWelding_FW_P.DataSource = "";
                gvBeforeWelding_FW_P.DataBind();
            }

            if (ds.Tables[12].Rows.Count > 0)
            {
                gvDuringWelding_FW_P.DataSource = ds.Tables[12];
                gvDuringWelding_FW_P.DataBind();
            }
            else
            {
                gvDuringWelding_FW_P.DataSource = "";
                gvDuringWelding_FW_P.DataBind();
            }

            if (ds.Tables[13].Rows.Count > 0)
            {
                gvFinalWelding_FW_P.DataSource = ds.Tables[13];
                gvFinalWelding_FW_P.DataBind();
            }
            else
            {
                gvFinalWelding_FW_P.DataSource = "";
                gvFinalWelding_FW_P.DataBind();
            }

            lblQCCompletionDate_FW_P.Text = ds.Tables[7].Rows[0]["QCDoneON"].ToString();

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

            //if (ds.Tables[15].Rows.Count > 0)
            //{
            //    gvTypeOfCheck_FW_P.DataSource = ds.Tables[15];
            //    gvTypeOfCheck_FW_P.DataBind();
            //}
            //else
            //{
            //    gvTypeOfCheck_FW_P.DataSource = "";
            //    gvTypeOfCheck_FW_P.DataBind();
            //}
			
			
            hdnCheyyarAddress.Value = ds.Tables[15].Rows[0]["CheyyarAddress"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}