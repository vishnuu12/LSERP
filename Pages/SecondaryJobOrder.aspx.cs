using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Configuration;
using SelectPdf;

public partial class Pages_SecondaryJobOrder : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cProduction objP;

    string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

    string JobOrderDocsSavePath = ConfigurationManager.AppSettings["JobOrderDocsSavePath"].ToString();
    string JobOrderDocsHttpPath = ConfigurationManager.AppSettings["JobOrderDocsHttpPath"].ToString();

    int Temp;

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
                //objc = new cCommon();
                //objP = new cProduction();
                //DataSet dsRFPHID = new DataSet();
                //DataSet dsCustomer = new DataSet();
                //dsCustomer = objP.getRFPCustomerNameByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                //dsRFPHID = objP.GetRFPDetailsByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                //ViewState["RFPDetails"] = dsRFPHID.Tables[0];
                ddlRFPNoAndCustomerLoad();
            }
            if (target == "GetMRN")
            {

            }
            if (target == "WorkInProgrss")
            {
                hdnMPID.Value = arg.ToString().Split('/')[0].ToString();
                hdnProcessOrder.Value = arg.ToString().Split('/')[1].ToString();
                ViewState["JCFlag"] = "WorkInProgrss";
                ViewState["JCHID"] = arg.ToString().Split('/')[2].ToString();
                BindProductionPartDetailsByMPIDAndProcessOrder();
            }
            if (target == "QCFailure")
            {
                hdnMPID.Value = arg.ToString().Split('/')[0].ToString();
                ViewState["JCDID"] = arg.ToString().Split('/')[1].ToString();
                ViewState["JCFlag"] = "QCFailure";
                BindProductionPartDetailsByMPIDAndProcessOrder();
            }

            if (target == "QCRework")
            {
                hdnMPID.Value = arg.ToString().Split('/')[0].ToString();
                ViewState["JCDID"] = arg.ToString().Split('/')[1].ToString();
                ViewState["JCFlag"] = "QCRework";
                ViewState["JCHID"] = "0";
                BindProductionPartDetailsByMPIDAndProcessOrder();
            }

            if (target == "deleteAJ")
            {
                DataSet ds = new DataSet();
                objP = new cProduction();
                objP.JCHID = Convert.ToInt32(arg.ToString());
                ds = objP.DeleteAssemplyJobCardDetailsByJCHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','Assemply Job Card Deleted Successfully');ShowAssemplyJobCardDetails();", true);

                BindAssemplyPlanningDetails();
                BindAssemplyPlanningJobCardDetails();
            }

            if (target == "deleteAssemplyMRNIssue")
            {
                DataSet ds = new DataSet();
                objP = new cProduction();
                objP.JCMRNID = Convert.ToInt32(arg.ToString());
                ds = objP.deleteAssemplyMRNIssueDetailsByJCHID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','Assemply Job Card MRN Deleted Successfully');ShowMRNBOIPopup();", true);
                BindAssemplyMRNIssuedDetails();
                BindBoughtOutMRNIssuedDetails();
            }

            if (target == "PrintAssemplyJobCard")
            {
                BindAssemplyJobcardPDFDetailsByJCHID(arg.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowAssemplyPlanningPopUp();", true);
            }

            if (target == "deleteJobCardDetailsByJCHID")
                DeleteJobCardDetailsByJCHID(arg.ToString());
			
			if (target == "PartToPartAssemplyJobCardPrint")
                PartToPartAssemplyJobCardDetails(arg.ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblPartOperation_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblPartOperation.SelectedValue == "PlateCutting")
            {
                divPlateCutting_J.Visible = true;
                divRings_J.Visible = false;
                divGimbal_J.Visible = false;
            }
            else if (rblPartOperation.SelectedValue == "Rings")
            {
                divPlateCutting_J.Visible = false;
                divRings_J.Visible = true;
                divGimbal_J.Visible = false;
            }
            else if (rblPartOperation.SelectedValue == "Gimbal")
            {
                divPlateCutting_J.Visible = false;
                divRings_J.Visible = false;
                divGimbal_J.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        divOutput.Visible = false;
        ddlRFPNoAndCustomerLoad();
    }

    #endregion

    #region"CheckBox Events"

    protected void chkitems_OnCheckedChanged(object sender, EventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        string PAPDIDs = "";
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            foreach (GridViewRow row in gvPartAssemplyPlanningDetails_AP.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    if (PAPDIDs == "")
                        PAPDIDs = gvPartAssemplyPlanningDetails_AP.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PAPDIDs = PAPDIDs + "," + gvPartAssemplyPlanningDetails_AP.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            ds = objP.GetBellosnoDetailsByRFPDID(PAPDIDs);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBellowSno_AP.DataSource = ds.Tables[0];
                gvBellowSno_AP.DataBind();
            }
            else
            {
                gvBellowSno_AP.DataSource = "";
                gvBellowSno_AP.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            divOutput.Visible = false;
            dt = (DataTable)ViewState["RFPDetails"];
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
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        cProduction objP = new cProduction();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;

                objP.status = rblRFPChange.SelectedValue;
                objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objP.GetProductionItemNameDetailsByRFPHID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvSecondaryJobOrderDetails.DataSource = ds.Tables[0];
                    gvSecondaryJobOrderDetails.DataBind();
                }
                else
                {
                    gvSecondaryJobOrderDetails.DataSource = "";
                    gvSecondaryJobOrderDetails.DataBind();
                }
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlWPSnumber_SW_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.WPSID = Convert.ToInt32(ddlWPSnumber_SW.SelectedValue);

            ds = objP.GetWPSDetailsByWPSID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWPSDetails_SW.DataSource = ds.Tables[0];
                gvWPSDetails_SW.DataBind();
            }
            else
            {
                gvWPSDetails_SW.DataSource = "";
                gvWPSDetails_SW.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlContractorName_J_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = sender as DropDownList;
            if (ddl.ID == "ddlContractorName_SW")
                BindContractorNameChanged(ddlContractorName_SW, ddlContractorTeamName_SW);
            else if (ddl.ID == "ddlContractorName_J")
                BindContractorNameChanged(ddlContractorName_J, ddlContractorTeamMemberName_J);
            else if (ddl.ID == "ddlContractorName_BTC")
                BindContractorNameChanged(ddlContractorName_BTC, ddlContractorTeamName_BTC);
            else if (ddl.ID == "ddlContractorName_AP")
                BindContractorNameChanged(ddlContractorName_AP, ddlContractorTeamMemberName_AP);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlContractorTeamMemberName_J_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        if (ddl.ID.ToString() == "ddlContractorTeamName_SW")
            BindContractorTeamnameChanged(ddlContractorName_SW, ddlContractorTeamName_SW);
        else if (ddl.ID.ToString() == "ddlContractorTeamMemberName_J")
            BindContractorTeamnameChanged(ddlContractorName_J, ddlContractorTeamMemberName_J);
        else if (ddl.ID.ToString() == "ddlContractorTeamName_BTC")
            BindContractorTeamnameChanged(ddlContractorName_BTC, ddlContractorTeamName_BTC);
        else if (ddl.ID.ToString() == "ddlContractorTeamMemberName_AP")
            BindContractorTeamnameChanged(ddlContractorName_AP, ddlContractorTeamMemberName_AP);
    }

    #endregion

    #region"Button Events"

    protected void btnSaveJobCard_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objP = new cProduction();
        string str = "";
        string AttachmentName = "";
        string IssuedLayout = "";
        string ReturnedLayout = "";
        try
        {
            objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
            objP.SecondaryJobOrderID = lblSecondaryJobOrderID_J.Text.ToString().Split('/')[1].ToString();
            objP.CMID = Convert.ToInt32(ddlContractorName_J.SelectedValue);
            objP.CTDID = Convert.ToInt32(ddlContractorTeamMemberName_J.SelectedValue);
            objP.IssueDate = DateTime.ParseExact(txtIssueDate_J.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.DeliveryDate = DateTime.ParseExact(txtDeliveryDate_J.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //  objP.PartQty = Convert.ToInt32(txtPartQuantity_J.Text);

            objP.NextProcess = Convert.ToInt32(hdnNextProcess.Value);
            objP.PDID = Convert.ToInt32(hdnPDID.Value);

            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);

            objP.joborderRemarks = txtJobOrderRemarks_J.Text;

            objP.MaterialissuedStatus = hdnMaterialIssuedStatus.Value;
            objP.ProcessName = ViewState["ProcessName"].ToString();

            if (ViewState["ProcessName"].ToString() == "Marking & Cutting")
            {
                if (rblPartOperation.SelectedValue == "PlateCutting")
                {
                    objP.FabricationType = "PlateCutting";
                    objP.FabricationTypeValues = txtDiameter_J.Text + "," + txtPlateLength_J.Text + "," + txtPlateWidth_J.Text;
                }
                else if (rblPartOperation.SelectedValue == "Rings")
                {
                    objP.FabricationType = "Rings";
                    objP.FabricationTypeValues = txtOD_J.Text + "," + txtID_J.Text + "," + txtNoOfSegments_J.Text;
                }
                else if (rblPartOperation.SelectedValue == "Gimbal")
                {
                    objP.FabricationType = "Gimbal";
                    objP.FabricationTypeValues = txtPlate1_J.Text + "," + txtPlate2_J.Text + "," + txtWidth_J.Text + "," + txtPlate1Qty_J.Text + "," + txtPlate2Qty_J.Text;
                }
                objP.TubeId = "";
                objP.TubeLength = "";
                objP.CuttingProcessID = Convert.ToInt32(ddlCuttingProcess_J.SelectedValue);
            }
            else
            {
                objP.FabricationType = "";
                objP.FabricationTypeValues = "";
                objP.TubeId = txtTubeId_J.Text;
                objP.TubeLength = txtTubeLength_J.Text;
                objP.CuttingProcessID = 0;
            }

            int count = 1;
            foreach (GridViewRow row in gvItemPartSNODetails.Rows)
            {
                CheckBox chkPartSno = (CheckBox)row.FindControl("chkPartSno");
                if (chkPartSno.Checked)
                {
                    string PRPDID = gvItemPartSNODetails.DataKeys[row.RowIndex].Values[0].ToString();
                    if (str == "")
                        str = PRPDID;
                    else
                        str = str + ',' + PRPDID;
                    objP.PartQty = Convert.ToInt32(count);
                    count++;
                }
            }

            objP.PRPDIDs = str;

            DataRow dr;

            dt.Columns.Add("MRNID");
            dt.Columns.Add("IssuedWeight");
            dt.Columns.Add("Length");
            dt.Columns.Add("Width");
            dt.Columns.Add("MaterialReturn");
            dt.Columns.Add("RtnWidth");
            dt.Columns.Add("RtnLength");
            dt.Columns.Add("ReturnedLayout");
            dt.Columns.Add("IssuedLayOut");
            dt.Columns.Add("MRNLocationID");

            if (fpAttchements.HasFile)
            {
                objc = new cCommon();
                cSales objSales = new cSales();

                string MaxAttachementId = objSales.GetMaximumAttachementID();

                AttachmentName = Path.GetFileName(fpAttchements.PostedFile.FileName);
                string[] extension = AttachmentName.Split('.');
                AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                objc.Foldername = JobOrderDocsSavePath;
                objc.FileName = AttachmentName;
                objc.PID = "SheetMarkingAndCutting";
                objc.AttachementControl = fpAttchements;
                objc.SaveFiles();
            }
            objP.AttachementName = AttachmentName;

            foreach (GridViewRow row in gvMRNDetails.Rows)
            {
                CheckBox chkMRN = (CheckBox)gvMRNDetails.Rows[row.RowIndex].FindControl("chkMRN");
                if (chkMRN.Checked)
                {
                    dr = dt.NewRow();
                    string MRNID = gvMRNDetails.DataKeys[row.RowIndex].Values[0].ToString();

                    TextBox txtIssuedWeight = (TextBox)gvMRNDetails.Rows[row.RowIndex].FindControl("txtIssuedWeight");
                    TextBox txtLength = (TextBox)gvMRNDetails.Rows[row.RowIndex].FindControl("txtLength");
                    TextBox txtWidth = (TextBox)gvMRNDetails.Rows[row.RowIndex].FindControl("txtWidth");
                    //  CheckBox chkMTLRtn = (CheckBox)gvMRNDetails.Rows[row.RowIndex].FindControl("chkMaterialReturn");
                    TextBox txtRtnLength = (TextBox)gvMRNDetails.Rows[row.RowIndex].FindControl("txtRtnLength");
                    TextBox txtRtnWidth = (TextBox)gvMRNDetails.Rows[row.RowIndex].FindControl("txtRtnWidth");
                    FileUpload fpCutLayout = (FileUpload)gvMRNDetails.Rows[row.RowIndex].FindControl("fpCutLayout");
                    FileUpload fpReturnedLayout = (FileUpload)gvMRNDetails.Rows[row.RowIndex].FindControl("fpReturnedLayout");
                    Label lblMRNNumber = (Label)gvMRNDetails.Rows[row.RowIndex].FindControl("lblMRNNumber");

                    string MRNLocationName = gvMRNDetails.DataKeys[row.RowIndex].Values[4].ToString();
                    string ReturnLayout = gvMRNDetails.DataKeys[row.RowIndex].Values[2].ToString();
                    string JobLayout = gvMRNDetails.DataKeys[row.RowIndex].Values[3].ToString();
                    string MRNLocationID = gvMRNDetails.DataKeys[row.RowIndex].Values[5].ToString();

                    // string MaterialRTN = chkMTLRtn.Checked == true ? "1" : "0";

                    if (fpCutLayout.HasFile)
                    {
                        cSales ojSales = new cSales();
                        cCommon objc = new cCommon();
                        objc.Foldername = StoresDocsSavePath;
                        IssuedLayout = Path.GetFileName(fpCutLayout.PostedFile.FileName);
                        string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                        string[] extension = IssuedLayout.Split('.');
                        IssuedLayout = MaximumAttacheID + lblMRNNumber.Text + extension[0].Trim().Replace("/", "") + '.' + extension[1];
                        objc.FileName = IssuedLayout;
                        objc.PID = "IssuedLayout" + "\\" + MRNLocationName + "\\";
                        objc.AttachementControl = fpCutLayout;
                        objc.SaveFiles();
                    }
                    else
                        IssuedLayout = JobLayout;

                    if (fpReturnedLayout.HasFile)
                    {
                        cSales ojSales = new cSales();
                        cCommon objc = new cCommon();
                        objc.Foldername = StoresDocsSavePath;
                        ReturnedLayout = Path.GetFileName(fpReturnedLayout.PostedFile.FileName);
                        string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                        string[] extension = ReturnedLayout.Split('.');
                        ReturnedLayout = MaximumAttacheID + lblMRNNumber.Text + extension[0].Trim().Replace("/", "") + '.' + extension[1];
                        objc.FileName = ReturnedLayout;
                        objc.PID = "MRNDocs" + "\\" + MRNLocationName + "\\";
                        objc.AttachementControl = fpReturnedLayout;
                        objc.SaveFiles();
                    }
                    else
                        ReturnedLayout = ReturnLayout;

                    dr["MRNID"] = MRNID;
                    dr["IssuedWeight"] = txtIssuedWeight.Text;
                    dr["Length"] = txtLength.Text;
                    dr["Width"] = txtWidth.Text;
                    dr["MaterialReturn"] = 0;
                    dr["RtnWidth"] = txtRtnWidth.Text;
                    dr["RtnLength"] = txtRtnLength.Text;
                    dr["ReturnedLayout"] = ReturnedLayout;
                    dr["IssuedLayOut"] = IssuedLayout;
                    dr["MRNLocationID"] = MRNLocationID;
                    dt.Rows.Add(dr);
                }
            }
            //   if (hdnMaterialIssuedStatus.Value == "0")
            objP.MRNdt = dt;
            //else
            //{
            //    dt.Rows.Clear();
            //    objP.MRNdt = dt;
            //}
            objP.UserID = Convert.ToInt32(objSession.employeeid);
            ds = objP.SaveJobCardDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "SuccessMessage('Success','Job Card Details Saved SuccessFully');HideJobCardPopUp();", true);
                BindPartDetailsByPRIDID();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            hdnEditJCHID.Value = "0";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "ErrorMessage('Error','Error Occured')", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveQCRequest_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objP = new cProduction();
        string PRPDIDs = "";
        //bool SelectAllPart = true;
        bool msg = false;
        try
        {
            if (ViewState["JCFlag"].ToString() == "WorkInProgrss")
            {
                foreach (GridViewRow row in gvQCRequest.Rows)
                {
                    CheckBox chkitems = (CheckBox)row.FindControl("chkQC");
                    if (chkitems.Checked)
                    {
                        msg = true;
                        if (PRPDIDs == "")
                            PRPDIDs = gvQCRequest.DataKeys[row.RowIndex].Values[0].ToString();
                        else
                            PRPDIDs = PRPDIDs + ',' + gvQCRequest.DataKeys[row.RowIndex].Values[0].ToString();
                    }
                }
                if (msg == true)
                {
                    objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
                    objP.MPID = Convert.ToInt32(hdnMPID.Value);
                    objP.procesOrder = Convert.ToInt32(hdnProcessOrder.Value);
                    ds = objP.SaveQcRequestDetails(PRPDIDs);
                }
            }
            else
            {
                objP.JCDID = Convert.ToInt32(ViewState["JCDID"].ToString());
                ds = objP.UpdateQCStatusByJCDID();
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                BindPartDetailsByPRIDID();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Request Saved Successfully');hidempeQCRequestPopUp();", true);
                SaveAlertDetails();
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "NotIssued")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Material still  Not Yet Issued Please Contact Store Department!');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }

    protected void btnPDFClick_Click(object sender, EventArgs e)
    {
        try
        {
            string arg = hdnProcessOrder_PDF.Value;
            ViewState["Flag"] = "PDF";
            hdnMPID.Value = arg.ToString().Split('/')[0].ToString();
            hdnJCHID.Value = arg.ToString().Split('/')[1].ToString();

            bindPDFPreviewDetails();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPartPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            if (ViewState["ProcessName"].ToString() == "Sheet Marking & Cutting")
            {
                objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetMarkingAndCuttingDetailsByJCHID_PRINT");

                lblProcessName_SMC_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                lblJobOrderID_SMC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblProcessname_SMC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblDate_SMC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                lblRFPNo_SMC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblContractorName_SMC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                lblContractorTeamname_SMC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                lblItemNameSize_SMC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblDrawingName_SMC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblPartname_SMC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
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
                lblPartQty_SMC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

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
                    //imgQrcode.Attributes.Add("style", "display:block;");
                    //imgQrcode.ImageUrl = QrCode;
                    ViewState["QrCode"] = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
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

                //gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                //gvQCObservationDetails_MC_P.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','SMC');ShowAddPartPopUp();", true);
            }

            else if (ViewState["ProcessName"].ToString() == "Marking & Cutting")
            {
                objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetMarkingAndCuttingDetailsByJCHID_PRINT");

                lblProcessNameHeader_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                lblJobOrderID_MC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblProcessName_MC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblDate_MC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                lblRFPNo_MC_P.Text = ddlRFPNo.SelectedItem.Text;
                lblContractorName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                lblContractorTeamMemberName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                lblItemNameSize_MC_P.Text = ViewState["ItemName"].ToString();
                lblDrawingname_MC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblPartName_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                lblMaterialCategory_MC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                lblmaterialGrade_MC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                lblThickness_MC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                lblMRNNumber_MC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                lblJobOrderRemarks_MC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

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

                lblTotalCost_MC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                lblPartQty_MC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

                ViewState["Address"] = ds.Tables[10];

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('information','Functionality Moved Into Another Page');", true);
                // Response.Redirect("JobCardExpensesSheet.aspx");
                Session["JCHID"] = hdnJCHID.Value;
                Session["RFPHID"] = ddlRFPNo.SelectedValue;
                //Response.Redirect("JobCardExpensesSheet.aspx", true);

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "JobCardExpensesSheet.aspx");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                //GeneratePDFFile("FabricationMarkingAndCutting");
            }

            else if (ViewState["ProcessName"].ToString() == "Sheet Welding")
            {
                objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetWeldingDetailsByJCHID_PRINT");

                lblProcessName_SW_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                lblJobOrderID_SW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblProcessname_SW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblDate_SW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                lblRFPNo_SW_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblContractorName_SW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                lblContractorTeamname_SW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                lblItemNameSize_SW_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblDrawingName_SW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblPartname_SW_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
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

                //if (ds.Tables[9].Rows.Count > 0)
                //{
                //    gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                //    gvMRNIssueDetails_SMC_P.DataBind();
                //}

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

                cQRcode objQr = new cQRcode();

                string QrNumber = objQr.generateQRNumber(9);
                string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                string QrCode = objQr.QRcodeGeneration(Code);
                if (QrCode != "")
                {
                    //imgQrcode.Attributes.Add("style", "display:block;");
                    //imgQrcode.ImageUrl = QrCode;
                    ViewState["QrCode"] = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
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

                //gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                //gvQCObservationDetails_MC_P.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','SW');ShowAddPartPopUp();", true);
            }

            else if (ViewState["ProcessName"].ToString() == "Fabrication & Welding")
            {
                objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetFabricationAndWeldingDetailsByJCHID_PRINT");

                //lblProcessName_FW_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                //lblJobOrderID_FW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                //lblProcessname_FW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                //ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                //lblDate_FW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                //lblRFPNo_FW_P.Text = ddlRFPNo.SelectedItem.Text;
                //lblContractorName_FW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                //lblContractorTeamname_FW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                //lblItemNameSize_FW_P.Text = ViewState["ItemName"].ToString();
                //lblDrawingName_FW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                //lblPartname_FW_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                ////  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                //lblMaterialCategory_FW_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                //lblMaterialGrade_FW_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                //lblThickness_FW_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                //lblMRNNumber_FW_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                //lblJobOrderRemarks_FW_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                //if (ds.Tables[6].Rows.Count > 0)
                //{
                //    gvPartSno_FW_P.DataSource = ds.Tables[6];
                //    gvPartSno_FW_P.DataBind();
                //}

                //gvWPSDetails_FW_P.DataSource = ds.Tables[5];
                //gvWPSDetails_FW_P.DataBind();

                ////if (ds.Tables[9].Rows.Count > 0)
                ////{
                ////    gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                ////    gvMRNIssueDetails_SMC_P.DataBind();
                ////}

                //if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                //    lblOfferQCTest_FW_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                //else
                //    lblOfferQCTest_FW_P.Text = "";

                //lblTotalCost_FW_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                //lblPartQty_FW_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
                //lblNOP_FW_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();
                //ViewState["Address"] = ds.Tables[10];

                //string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                //string result = "";

                //lblFabricationType_FW_P.Text = ds.Tables[9].Rows[0]["FabricationTypeName"].ToString();

                //foreach (DataRow dr in ds.Tables[9].Rows)
                //{
                //    result = "";
                //    result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                //    sb.Append(result);
                //}

                //divfabrication_FW_P.InnerHtml = sb.ToString();

                //  GeneratePDFFile("FabricationAndWelding");

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('information','Functionality Moved Into Another Page');", true);
                // Response.Redirect("JobCardExpensesSheet.aspx");
                Session["JCHID"] = hdnJCHID.Value;
                Session["RFPHID"] = ddlRFPNo.SelectedValue;

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "JobCardExpensesSheet.aspx");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                 //Response.Redirect("JobCardExpensesSheet.aspx", true);
            }

            else if (ViewState["ProcessName"].ToString() == "Bellow Forming & Tangent Cutting")
            {
                objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetBellowFormingAndTangentCuttingDetailsByJCHID_PRINT");

                lblProcessName_BFTC_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                lblJobOrderID_BFTC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblProcessname_BFTC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblDate_BFTC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                lblRFPNo_BFTC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblContractorName_BFTC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                lblContractorTeamname_BFTC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                lblItemNameSize_BFTC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblDrawingName_BFTC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblPartname_BFTC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                lblMaterialCategory_BFTC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                lblMaterialGrade_BFTC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                lblThickness_BFTC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                lblMRNNumber_BFTC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                lblJobOrderRemarks_BFTC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                lblremarks_BETC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                lbldeadlineDate_BFTC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();
                lbljobcardstatus_BFTC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvPartSno_BFTC_P.DataSource = ds.Tables[6];
                    gvPartSno_BFTC_P.DataBind();
                }

                lblBellowDetails_BFTC_P.Text = "Bellow Details" + " ( " + ds.Tables[5].Rows[0]["FormType"].ToString() + " )";

                if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    lblOfferQCTest_BFTC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                else
                    lblOfferQCTest_BFTC_P.Text = "";

                lblTotalCost_BFTC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                lblPartQty_BFTC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
                lblNOP_BFTC_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();

                ViewState["Address"] = ds.Tables[9];

                if (ds.Tables[5].Rows.Count > 0)
                {
                    string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:60%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                    string result = "";

                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        result = "";
                        result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["AsPerDrawing"].ToString());
                        sb.Append(result);
                    }

                    divBellowDetails_BFTC_P.InnerHtml = sb.ToString();
                }

                if (ds.Tables[5].Rows[0]["FormType"].ToString() == "Roll")
                {
                    divNumberOfStages_BFTC_P.Attributes.Add("style", "display:block;");
                    gvNumberOfStages_BFTC_P.DataSource = ds.Tables[10];
                    gvNumberOfStages_BFTC_P.DataBind();
                }
                else
                    divNumberOfStages_BFTC_P.Attributes.Add("style", "display:none;");

                gvStageActivity_BFTC_P.DataSource = ds.Tables[11];
                gvStageActivity_BFTC_P.DataBind();

                cQRcode objQr = new cQRcode();

                string QrNumber = objQr.generateQRNumber(9);
                string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                string QrCode = objQr.QRcodeGeneration(Code);
                if (QrCode != "")
                {
                    //imgQrcode.Attributes.Add("style", "display:block;");
                    //imgQrcode.ImageUrl = QrCode;
                    ViewState["QrCode"] = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
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

                //gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                //gvQCObservationDetails_MC_P.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','BFTC');ShowAddPartPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveSheetWelding_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        string str = "";
        string AttachmentName = "";
        try
        {
            objP.SecondaryJobOrderID = lblJobOrderID_SW.Text.ToString().Split('/')[1].ToString();
            objP.IssueDate = DateTime.ParseExact(txtIssuedDate_SW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.DeliveryDate = DateTime.ParseExact(txtDeliveryDate_SW.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.PDID = Convert.ToInt32(hdnPDID.Value);
            objP.NextProcess = Convert.ToInt32(hdnNextProcess.Value);
            objP.joborderRemarks = txtJobOrderRemarks_SW.Text;
            objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            // objP.PartQty = Convert.ToInt32(ViewState["PartQty"]);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.CMID = Convert.ToInt32(ddlContractorName_SW.SelectedValue);
            objP.CTDID = Convert.ToInt32(ddlContractorTeamName_SW.SelectedValue);

            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);

            int count = 1;
            foreach (GridViewRow row in gvPartSerielNumber_SW.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                if (chk.Checked)
                {
                    string PRPDID = gvPartSerielNumber_SW.DataKeys[row.RowIndex].Values[0].ToString();
                    if (str == "")
                        str = PRPDID;
                    else
                        str = str + ',' + PRPDID;
                    objP.PartQty = Convert.ToInt32(count);
                    count++;
                }
            }

            objP.PRPDIDs = str;

            foreach (GridViewRow row in gvWPSDetails_SW.Rows)
            {
                string WPSID = gvWPSDetails_SW.DataKeys[row.RowIndex].Values[0].ToString();

                objP.WPSID = Convert.ToInt32(WPSID);
            }

            if (fpUpload_SW.HasFile)
            {
                objc = new cCommon();
                cSales objSales = new cSales();

                string MaxAttachementId = objSales.GetMaximumAttachementID();
                AttachmentName = Path.GetFileName(fpUpload_SW.PostedFile.FileName);
                string[] extension = AttachmentName.Split('.');
                AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                objc.Foldername = JobOrderDocsSavePath;
                objc.FileName = AttachmentName;
                objc.PID = "SheetWelding";
                objc.AttachementControl = fpUpload_SW;
                objc.SaveFiles();
            }
            objP.AttachementName = AttachmentName;

            ds = objP.SaveJobCardSheetWeldingDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "SuccessMessage('Success','Job Card Sheet Welding Details Saved SuccessFully');HideSheetWeldingPopUp();", true);
                BindPartDetailsByPRIDID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnStages_Click(object sender, EventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.Stages = Convert.ToInt32(hdnStages.Value);
            ds = objP.GetStagesInBellowTangetCuttingRoll();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStages_BTC.DataSource = ds.Tables[0];
                gvStages_BTC.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnBellowTangetCutting_Click(object sender, EventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string str = "";
        string AttachmentName = "";
        try
        {
            objP.SecondaryJobOrderID = lblJobOrderID_BTC.Text.ToString().Split('/')[1].ToString();
            objP.IssueDate = DateTime.ParseExact(txtIssueDate_BTC.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.DeliveryDate = DateTime.ParseExact(txtDeliveryDate_BTC.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.PDID = Convert.ToInt32(hdnPDID.Value);
            objP.NextProcess = Convert.ToInt32(hdnNextProcess.Value);
            objP.joborderRemarks = txtJobOrderRemarks_BTC.Text;
            objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
            //  objP.PartQty = Convert.ToInt32(ViewState["PartQty"]);
            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.CMID = Convert.ToInt32(ddlContractorName_BTC.SelectedValue);
            objP.CTDID = Convert.ToInt32(ddlContractorTeamName_BTC.SelectedValue);

            DataRow dr;
            dt.Columns.Add("Stages");
            dt.Columns.Add("Inner");
            dt.Columns.Add("Outer");
            dt.Columns.Add("Gap");

            if (ViewState["FormingType"].ToString() == "Roll")
            {
                objP.RollFormingDevelopementPitch = txtRollFormingDevelopementPitch_BTC.Text;
                objP.RollFormingInitialDepth = txtRollFormingInitialDepth_BTC.Text;
                objP.Stages = Convert.ToInt32(txtNumberOfStages_BTC.Text);

                foreach (GridViewRow row in gvStages_BTC.Rows)
                {
                    dr = dt.NewRow();

                    Label txtStages = (Label)gvStages_BTC.Rows[row.RowIndex].FindControl("lblStages");
                    TextBox txtInner = (TextBox)gvStages_BTC.Rows[row.RowIndex].FindControl("txtInner");
                    TextBox txtOuter = (TextBox)gvStages_BTC.Rows[row.RowIndex].FindControl("txtOuter");
                    TextBox txtGap = (TextBox)gvStages_BTC.Rows[row.RowIndex].FindControl("txtGap");

                    dr["Stages"] = txtStages.Text;
                    dr["Inner"] = txtInner.Text;
                    dr["Outer"] = txtOuter.Text;
                    dr["Gap"] = txtGap.Text;

                    dt.Rows.Add(dr);
                }
            }
            else if (ViewState["FormingType"].ToString() == "Expandal")
            {
                objP.ExpandalDevelopementPitch = txtExpandalDevelopedPitch_BTC.Text;
                objP.ExpandalFinalOver = txtExpandalFinalOver_BTC.Text;

                dr = dt.NewRow();

                dr["Stages"] = 0;
                dr["Inner"] = 0;
                dr["Outer"] = 0;
                dr["Gap"] = 0;

                dt.Rows.Add(dr);

            }
            int count = 1;
            foreach (GridViewRow row in gvPartSNO_BTC.Rows)
            {
                string PRPDID = gvPartSNO_BTC.DataKeys[row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                if (chk.Checked)
                {
                    if (str == "")
                        str = PRPDID;
                    else
                        str = str + ',' + PRPDID;

                    objP.PartQty = Convert.ToInt32(count);
                    count++;
                }

            }

            if (fbBellowTC_BTC.HasFile)
            {
                objc = new cCommon();
                cSales objSales = new cSales();

                string MaxAttachementId = objSales.GetMaximumAttachementID();
                AttachmentName = Path.GetFileName(fbBellowTC_BTC.PostedFile.FileName);
                string[] extension = AttachmentName.Split('.');
                AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                objc.Foldername = JobOrderDocsSavePath;
                objc.FileName = AttachmentName;
                objc.PID = "BellowFormingAndTangentCutting";
                objc.AttachementControl = fbBellowTC_BTC;
                objc.SaveFiles();
            }

            objP.AttachementName = AttachmentName;
            objP.PRPDIDs = str;
            objP.dt = dt;

            objP.BellowFormType = ViewState["FormingType"].ToString();
            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);

            ds = objP.SaveBellowTangentCuttingDetails();

            //Message
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "SuccessMessage('Success','Job Card Bellow Tangent Cutting Details Saved SuccessFully');hideBellowsTangetCutPopUP();", true);
                BindPartDetailsByPRIDID();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveAssemplyjobCard_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        //cQuality objQt = new cQuality();
        cProduction objP = new cProduction();
        string PAPDID = "";
        string PRIDID = "";
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            // objP.PRIDID = Convert.ToInt32(hdnPRIDID.Value);

            objP.CMID = 0; //Convert.ToInt32(ddlContractorName_AP.SelectedValue);
            objP.CTDID = 0; //Convert.ToInt32(ddlContractorTeamMemberName_AP.SelectedValue);
            objP.IssueDate = DateTime.ParseExact(txtIssueDate_AP.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.DeliveryDate = DateTime.ParseExact(txtDeliveryDate_AP.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.JobOrderRemarks = txtJobOrderRemarks_AP.Text;

            foreach (GridViewRow row in gvPartAssemplyPlanningDetails_AP.Rows)
            {
                CheckBox chk = (CheckBox)gvPartAssemplyPlanningDetails_AP.Rows[row.RowIndex].FindControl("chkitems");
                if (chk.Checked)
                {
                    if (PAPDID == "")
                        PAPDID = gvPartAssemplyPlanningDetails_AP.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PAPDID = PAPDID + "," + gvPartAssemplyPlanningDetails_AP.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            foreach (GridViewRow row in gvBellowSno_AP.Rows)
            {
                CheckBox chk = (CheckBox)gvBellowSno_AP.Rows[row.RowIndex].FindControl("chkitems");
                if (chk.Checked)
                {
                    if (PRIDID == "")
                        PRIDID = gvBellowSno_AP.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PRIDID = PRIDID + "," + gvBellowSno_AP.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            objP.USERID = objSession.employeeid;

            //objQt.dt = dt;            
            ds = objP.SavePartAssemplyJobCardDetails(PAPDID, PRIDID);

            //if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            //{
            //    // objc = new cCommon();

            //    var sb = new StringBuilder();

            //    divSubAssemplyWeldingPDF.Attributes.Add("style", "display:block;");

            //    lblProcessName_AP_PDFHeader.Text = "Part  To Part Sub Assembly Welding";
            //    lblJobOrderID_AP_PDF.Text = ds.Tables[3].Rows[0]["AssemplyJobCardID"].ToString();
            //    lblDate_AP_PDF.Text = ds.Tables[3].Rows[0]["IssueDate"].ToString();
            //    lblRFPNo_AP_PDF.Text = ddlRFPNo.SelectedItem.Text;
            //    lblContractorName_AP_PDF.Text = ddlContractorName_AP.SelectedItem.Text;
            //    lblContractorTeamMemberName_AP_PDF.Text = ddlContractorTeamMemberName_AP.SelectedItem.Text;
            //    lblItemNameSize_AP_PDF.Text = lblItemNameSize_AP.Text;
            //    lblDrawingName_AP_PDF.Text = ds.Tables[1].Rows[0]["DrawingName"].ToString();
            //    lblPartName_AP_PDF.Text = "Sub Assembly";
            //    lblItemQty_AP_PDF.Text = ds.Tables[1].Rows.Count.ToString();
            //    lblProcessName_AP_PDF.Text = "Sub Assembly";

            //    gvPartSerielNumber_AP_PDF.DataSource = ds.Tables[1];
            //    gvPartSerielNumber_AP_PDF.DataBind();

            //    gvWPSDetails_AP_PDF.DataSource = ds.Tables[2];
            //    gvWPSDetails_AP_PDF.DataBind();

            //    lblJobOrderRemarks_AP_PDF.Text = ds.Tables[3].Rows[0]["JobOrderRemarks"].ToString();

            //    divSubAssemplyWeldingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            //    string div = sb.ToString();

            //    foreach (DataRow row in dt.Rows)
            //    {
            //        //  string path = PDFSavePath + row["PAPDID"] + "\\";
            //        if (!Directory.Exists(PDFSavePath))
            //            Directory.CreateDirectory(PDFSavePath);

            //        string htmlfile = "Assembly" + "_" + row["PAPDID"].ToString() + "_" + lblJobOrderId_AP.Text + ".html";
            //        string pdffile = "Assembly" + "_" + row["PAPDID"].ToString() + "_" + lblJobOrderId_AP.Text + ".pdf";

            //        string HtmlPath = PDFSavePath + htmlfile;
            //        string PDFPath = PDFSavePath + pdffile;

            //        SaveHtmlFile(HtmlPath, "Sub Assembly Welding", "", div);
            //        GenerateAndSavePDF(PDFSavePath, PDFPath, pdffile, HtmlPath);
            //    }

            //    divSubAssemplyWeldingPDF.Attributes.Add("style", "display:none;");

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Part Assembly Card Saved Successfully');ShowAssemplyPlanningPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAssemplyPlanningPopUp();", true);

            BindAssemplyPlanningJobCardDetails();
            BindAssemplyPlanningDetails();

            gvBellowSno_AP.DataSource = "";
            gvBellowSno_AP.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Errror Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        DataSet dsContractor = new DataSet();
        try
        {
            ViewState["ProcessName"] = hdnProcessName.Value;

            if (hdnProcessName.Value == "Marking & Cutting" || hdnProcessName.Value == "Fabrication & Welding")
            {
                divcontractornamedetails_J.Visible = false;
                divcontractornamedetails_SW.Visible = false;
            }
            else
            {
                divcontractornamedetails_J.Visible = true;
                divcontractornamedetails_SW.Visible = true;
            }

            if (hdnProcessName.Value == "Sheet Marking & Cutting")
            {
                dsContractor = objP.GetContractorDetails(ddlContractorName_J, ddlContractorTeamMemberName_J, ddlCuttingProcess_J);
                objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetMarkingAndCuttingByJCHID");
                EditSheetMarkingAndCuttingDetails(ds, "SMC");
            }
            else if (hdnProcessName.Value == "Marking & Cutting")
            {
                dsContractor = objP.GetContractorDetails(ddlContractorName_J, ddlContractorTeamMemberName_J, ddlCuttingProcess_J);
                objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetFabricationMarkingAndCuttingDetailsByJCHID");
                EditSheetMarkingAndCuttingDetails(ds, "MC");
            }
            else if (hdnProcessName.Value == "Sheet Welding" || hdnProcessName.Value == "Fabrication & Welding")
            {
                dsContractor = objP.GetContractorDetails(ddlContractorName_SW, ddlContractorTeamName_SW, ddlCuttingProcess_J);
                objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetSheetWeldingDetailsByJCHID");
                EditSheetWeldingDetails(ds, "SW");
            }
            else if (hdnProcessName.Value == "Bellow Forming & Tangent Cutti")
            {
                dsContractor = objP.GetContractorDetails(ddlContractorName_BTC, ddlContractorTeamName_BTC, ddlCuttingProcess_J);
                objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
                ds = objP.getEditJobCardPartDetailsByJCHID("LS_GetBellowFormingAndTangentDetailsByJCHID");
                EditBellowFormingAndTangentCuttingDetails(ds, "BFTC");
            }

            ViewState["ContractorName"] = dsContractor.Tables[0];
            ViewState["ContractorTeamName"] = dsContractor.Tables[1];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnIssueMRN_Click(object sender, EventArgs e)
    {
        // DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        objP = new cProduction();
        string MRN = "";
        string MRNID;
        string LocationID;
        string IssuedWeight;
        string MPID;
        bool msg = true;
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            // objP.PRIDID = Convert.ToInt32(hdnPRIDID.Value);

            foreach (GridViewRow row in gvMRNIssueBOI_AP.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                TextBox txtAvailWeight = (TextBox)row.FindControl("txtAvailWeight");

                if (chk.Checked)
                {
                    if (Convert.ToInt32(txtAvailWeight.Text) > 0)
                    {
                        MRNID = gvMRNIssueBOI_AP.DataKeys[row.RowIndex].Values[0].ToString();
                        LocationID = gvMRNIssueBOI_AP.DataKeys[row.RowIndex].Values[1].ToString();
                        IssuedWeight = txtAvailWeight.Text;
                        MPID = gvMRNIssueBOI_AP.DataKeys[row.RowIndex].Values[2].ToString();

                        if (MRN == "")
                            MRN = MRNID + "#" + LocationID + "#" + IssuedWeight + "#" + MPID;
                        else
                            MRN = MRN + "," + MRNID + "#" + LocationID + "#" + IssuedWeight + "#" + MPID;
                    }
                    else
                    {
                        msg = false;
                        break;
                    }
                }
            }
            objP.USERID = objSession.employeeid;

            if (msg)
            {
                ds = objP.SaveAssemplyMRNIssueDetails(MRN);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','MRN Issued Successfully');ShowMRNBOIPopup();", true);
                BindAssemplyMRNIssuedDetails();
                BindBoughtOutMRNIssuedDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Weight Should Be Greater Zero');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAJCompleted_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        bool msg = false;
        try
        {
            foreach (GridViewRow row in gvAJDetails_AJD.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    objP.PRIDID = Convert.ToInt32(gvAJDetails_AJD.DataKeys[row.RowIndex].Values[0].ToString());
                    objP.PAPDID = Convert.ToInt32(gvAJDetails_AJD.DataKeys[row.RowIndex].Values[1].ToString());
                    objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
                    ds = objP.UPdateAssemplyJobCardDetailsCompletedStatus();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() != "Updated")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                    if (ds.Tables[0].Rows[0]["alertqc"].ToString() == "yes")
                        msg = true;
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ViewPartAssemplyCardDetailsByJCHID();
                if (msg)
                    SaveAssemplyAlertDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','QC Request Saved Successfully');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnItemCompleted_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.PRIDID = Convert.ToInt32(hdnPRIDID.Value);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.UpdateItemCompletedStatusByPRIDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Item Completed Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnLoadSno_Click(object sender, EventArgs e)
    {
        chkitems_OnCheckedChanged(null, null);
    }

    #endregion

    #region"GridView Events"
	
    public int rfpdidold;
    public int rfpdidnew;
    Color grpcolor;

    protected void gvSecondaryJobOrderDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                rfpdidnew = Convert.ToInt32(dr["RFPDID"].ToString());
                if (rfpdidnew == rfpdidold)
                    e.Row.BackColor = grpcolor;
                else
                {
                    Color randomColor = randomcolorgenerate();
                    while (grpcolor == randomColor)
                    {
                        randomColor = randomcolorgenerate();
                    }
                    e.Row.BackColor = randomColor;
                    grpcolor = randomColor;

                }
                rfpdidold = rfpdidnew;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSecondaryJobOrderDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnPRIDID.Value = gvSecondaryJobOrderDetails.DataKeys[index].Values[0].ToString();
            hdnRFPDID.Value = gvSecondaryJobOrderDetails.DataKeys[index].Values[1].ToString();

            Label lblItemName = (Label)gvSecondaryJobOrderDetails.Rows[index].FindControl("lblItemName");
            Label lblItemSNO = (Label)gvSecondaryJobOrderDetails.Rows[index].FindControl("lblItemSNO");

            ViewState["ItemName"] = lblItemName.Text;

            if (e.CommandName == "Job")
            {
                BindPartDetailsByPRIDID();
                lblItemName_P.Text = "(" + lblItemSNO.Text + " - " + lblItemName.Text + ")";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPartPopUp();", true);
            }
            else if (e.CommandName == "Assemply")
            {
                DataSet dsContractor = new DataSet();
                objP = new cProduction();

                lblRFPNo_AP.Text = ddlRFPNo.SelectedItem.Text;
                lblItemNameSize_AP.Text = lblItemName.Text;

                BindAssemplyPlanningDetails();
                BindAssemplyPlanningJobCardDetails();
				BindAssemplyPlanningJobCardDetailsForPartToPart();

                gvBellowSno_AP.DataSource = "";
                gvBellowSno_AP.DataBind();

                lblItemName_AP.Text = "(" + lblItemSNO.Text + " - " + lblItemName.Text + ")";
                dsContractor = objP.GetContractorDetails(ddlContractorName_AP, ddlContractorTeamMemberName_AP, null);

                ViewState["ContractorName"] = dsContractor.Tables[0];
                ViewState["ContractorTeamName"] = dsContractor.Tables[1];
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        DataSet dsContractor = new DataSet();
        try
        {
            hdnEditJCHID.Value = "0";

            txtPartQuantity_J.Text = "";
            gvMRNDetails.DataSource = "";
            gvMRNDetails.DataBind();

            gvItemPartSNODetails.DataSource = "";
            gvItemPartSNODetails.DataBind();

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string JCHID = gvPartDetails.DataKeys[index].Values[4].ToString();

            hdnJCHID.Value = JCHID;

            hdnMPID.Value = gvPartDetails.DataKeys[index].Values[1].ToString();

            hdnPRPDID.Value = gvPartDetails.DataKeys[index].Values[0].ToString();
            hdnNextProcess.Value = gvPartDetails.DataKeys[index].Values[2].ToString();
            hdnMID.Value = gvPartDetails.DataKeys[index].Values[3].ToString();

            objP.PRPDID = Convert.ToInt32(hdnPRPDID.Value);
            objP.NextProcess = Convert.ToInt32(hdnNextProcess.Value);
            objP.MID = Convert.ToInt32(hdnMID.Value);
            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.JCHID = Convert.ToInt32(hdnJCHID.Value);

            if (JCHID == "0")
            {
                dsContractor = objP.GetContractorDetails(ddlContractorName_J, ddlContractorTeamMemberName_J, ddlCuttingProcess_J);
            }
            else
            {
                dsContractor = objP.GetContractorDetails(ddlContractorName_SW, ddlContractorTeamName_SW, ddlCuttingProcess_J);
                dsContractor = objP.GetContractorDetails(ddlContractorName_BTC, ddlContractorTeamName_BTC, ddlCuttingProcess_J);
            }
            ViewState["ContractorName"] = dsContractor.Tables[0];
            ViewState["ContractorTeamName"] = dsContractor.Tables[1];

            ds = objP.bindJobCardDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "")
            {
                hdnPDID.Value = ds.Tables[0].Rows[0]["PDID"].ToString();

                ViewState["ProcessName"] = ds.Tables[0].Rows[0]["ProcessName"].ToString();

                Label lblPartName = (Label)gvPartDetails.Rows[index].FindControl("lblPartName");
                Label lblItemName = (Label)gvPartDetails.Rows[index].FindControl("lblItemName");

                if (ViewState["ProcessName"].ToString() == "Marking & Cutting" || ViewState["ProcessName"].ToString() == "Fabrication & Welding")
                {
                    divcontractornamedetails_J.Visible = false;
                    divcontractornamedetails_SW.Visible = false;
                }
                else
                {
                    divcontractornamedetails_J.Visible = true;
                    divcontractornamedetails_SW.Visible = true;
                }

                if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Marking & Cutting" || ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Marking & Cutting")
                {
                    BindPartSnoAndMRNDetails();

                    lblParNameAndtProcessName_J.Text = lblPartName.Text + '/' + ds.Tables[0].Rows[0]["ProcessName"] + '/' + ds.Tables[0].Rows[0]["PrimaryJID"].ToString();

                    lblSecondaryJobOrderID_J.Text = ds.Tables[0].Rows[0]["PrimaryJID"].ToString() + "/" + ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                    lblRFPNo_J.Text = ddlRFPNo.SelectedItem.Text;
                    lblPartName_J.Text = lblPartName.Text;
                    lblItemSizeName_J.Text = ViewState["ItemName"].ToString();

                    if (ds.Tables[2].Rows.Count > 0)
                        lblDrawingName_J.Text = ds.Tables[2].Rows[0]["FileName"].ToString();

                    lblGradeName_J.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    lblCategoryName_J.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    lblThickness_J.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();


                    if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Marking & Cutting")
                    {
                        divMarkingAndCutting_J.Visible = true;
                        divSheetMarkingAndCutting_J.Visible = false;
                        divTubeIDAndTubeLength_J.Visible = false;

                        //lblDiameter_J.Text = ds.Tables[6].Rows[0]["MSMIDValue"].ToString();
                        //lblLength_J.Text = ds.Tables[3].Rows[0]["MSMIDValue"].ToString();
                        //lblWidth_J.Text = ds.Tables[4].Rows[0]["MSMIDValue"].ToString();
                        //lblWeight_J.Text = ds.Tables[5].Rows[0]["MSMIDValue"].ToString();
                        lblOfferQC_J.Text = ds.Tables[8].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                        lblPartQuantity_J.Text = " ( " + ds.Tables[7].Rows[0]["PartQuantity"].ToString() + " ) ";
                    }
                    else if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Marking & Cutting")
                    {
                        divTubeIDAndTubeLength_J.Visible = true;

                        lblPartQuantity_J.Text = " ( " + ds.Tables[3].Rows[0]["PartQuantity"].ToString() + " ) ";
                        divMarkingAndCutting_J.Visible = false;
                        divSheetMarkingAndCutting_J.Visible = true;
                        gvPLYDetails_J.DataSource = ds.Tables[5];
                        gvPLYDetails_J.DataBind();
                        lblOfferQC_J.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowJobCardPopUp();", true);
                }

                if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Welding" || ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Fabrication & Welding")
                {
                    //if (ds.Tables[0].Rows[0]["IndentStatus"].ToString() == "1")
                    //{
                    //ViewState["Flag"] = "NextJob";
                    lblProcessName_SW.Text = lblPartName.Text + '/' + ds.Tables[0].Rows[0]["ProcessName"];
                    lblJobOrderID_SW.Text = ds.Tables[0].Rows[0]["PrimaryJID"].ToString() + "/" + ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                    //    BindSheetWeldingDetails();

                    lblRFPNo_SW.Text = ddlRFPNo.SelectedItem.Text;


                    if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Welding")
                    {
                        //  lblContractorName_SW.Text = ds.Tables[6].Rows[0]["ContractorName"].ToString();
                        //   lblCTMName_SW.Text = ds.Tables[6].Rows[0]["ContractorTeamName"].ToString();
                        lblMRNNumber_SW.Text = ds.Tables[6].Rows[0]["MRNNumber"].ToString();
                        lblOfferQC_SW.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString();

                        //lblPartQty_SW.Text = ds.Tables[5].Rows[0]["PartQty"].ToString();
                        //ViewState["PartQty"] = ds.Tables[5].Rows[0]["PartQty"].ToString();

                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            gvPartSerielNumber_SW.DataSource = ds.Tables[5];
                            gvPartSerielNumber_SW.DataBind();
                        }
                        else
                        {
                            gvPartSerielNumber_SW.DataSource = "";
                            gvPartSerielNumber_SW.DataBind();
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Fabrication & Welding")
                    {
                        lblPartQty_SW.Text = ds.Tables[10].Rows[0]["PartQty"].ToString();
                        ViewState["PartQty"] = ds.Tables[10].Rows[0]["PartQty"].ToString();

                        //  lblContractorName_SW.Text = ds.Tables[10].Rows[0]["ContractorName"].ToString();
                        // lblCTMName_SW.Text = ds.Tables[10].Rows[0]["ContractorTeamName"].ToString();
                        lblMRNNumber_SW.Text = ds.Tables[10].Rows[0]["MRNNumber"].ToString();

                        //lblLength_SW.Text = ds.Tables[3].Rows[0]["MSMIDValue"].ToString();
                        //lblWidth_SW.Text = ds.Tables[4].Rows[0]["MSMIDValue"].ToString();
                        //lblWeight_SW.Text = ds.Tables[5].Rows[0]["MSMIDValue"].ToString();
                        //lblDiameter_SW.Text = ds.Tables[6].Rows[0]["MSMIDValue"].ToString();

                        lblOfferQC_SW.Text = ds.Tables[8].Rows[0]["OfferQC"].ToString();
                        if (ds.Tables[9].Rows.Count > 0)
                        {
                            gvPartSerielNumber_SW.DataSource = ds.Tables[9];
                            gvPartSerielNumber_SW.DataBind();
                        }
                        else
                        {
                            gvPartSerielNumber_SW.DataSource = "";
                            gvPartSerielNumber_SW.DataBind();
                        }
                    }

                    lblItemNameSize_SW.Text = ViewState["ItemName"].ToString();
                    lblDrawingName_SW.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    lblPartName_SW.Text = lblPartName.Text;
                    lblCategoryName_SW.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    lblGradeName_SW.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    lblThickness_SW.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();

                    //objP.GetWPSDetailsbyWPSNumber(ddlWPSnumber_SW);
                    GetWpsDetailsByWPSID();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowSheetWeldingPopUp();", true);
                    //}
                    //else
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Part is Under Workorder');", true);
                }

                if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Bellow Forming & Tangent Cutting")
                {
                    //if (ds.Tables[0].Rows[0]["IndentStatus"].ToString() == "1")
                    //{
                    lblProcessName_BTC.Text = lblPartName_BTC.Text + '/' + ds.Tables[0].Rows[0]["ProcessName"] + "(" + ds.Tables[7].Rows[0]["FormingType"].ToString() + ")";
                    lblJobOrderID_BTC.Text = ds.Tables[0].Rows[0]["PrimaryJID"].ToString() + "/" + ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                    lblRFPNo_BTC.Text = ddlRFPNo.SelectedItem.Text;

                    lblItemName_BTC.Text = ViewState["ItemName"].ToString();
                    lblDrawingName_BTC.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    lblPartName_BTC.Text = lblPartName.Text;
                    lblCategoryName_BTC.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    lblGradeName_BTC.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    lblThickness_BTC.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();

                    //lblContractorName_BTC.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                    //lblContractorTeamMemberName_BTC.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();
                    lblMRNNumber_BTC.Text = ds.Tables[7].Rows[0]["MRNNumber"].ToString();
                    lblOfferQC_BTC.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString();

                    //lblPartQty_BTC.Text = ds.Tables[6].Rows[0]["PartQty"].ToString();
                    // ViewState["PartQty"] = ds.Tables[6].Rows[0]["PartQty"].ToString();

                    lblID_BTC.Text = ds.Tables[5].Rows[0]["ID"].ToString();
                    lblOD_BTC.Text = ds.Tables[5].Rows[0]["OD"].ToString();
                    lblDepth_BTC.Text = ds.Tables[5].Rows[0]["DEP"].ToString();
                    lblPitch_BTC.Text = ds.Tables[5].Rows[0]["PIT"].ToString();
                    lblNoOfConvolutions_BTC.Text = ds.Tables[5].Rows[0]["NOC"].ToString();

                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        gvPartSNO_BTC.DataSource = ds.Tables[6];
                        gvPartSNO_BTC.DataBind();
                    }
                    else
                    {
                        gvPartSNO_BTC.DataSource = "";
                        gvPartSNO_BTC.DataBind();
                    }

                    ViewState["FormingType"] = ds.Tables[7].Rows[0]["FormingType"].ToString();

                    if (ds.Tables[7].Rows[0]["FormingType"].ToString() == "Expandal")
                    {
                        divTangetCuttRoll_BTC.Visible = false;
                        divTangetCuttingExpandal_BTC.Visible = true;
                    }
                    else if (ds.Tables[7].Rows[0]["FormingType"].ToString() == "Roll")
                    {
                        divTangetCuttRoll_BTC.Visible = true;
                        divTangetCuttingExpandal_BTC.Visible = false;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowBellowsTangetCutPopup();", true);
                    //}
                    //else
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Part is Under Workorder');", true);
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAddJobCard = (LinkButton)e.Row.FindControl("btnAddJobCard");

                if (dr["NextProcess"].ToString() == "9")
                {
                    // btnAddJobCard.CssClass = "aspNetDisabled";
                    btnAddJobCard.Visible = false;
                    btnAddJobCard.Attributes.Add("title", "All Process Completed");
                }
                else if (dr["ProcessCompleted"].ToString() != "0" && (dr["QCStatus"].ToString() != "1" && dr["QCStatus"].ToString() != "2"))
                {
                    // btnAddJobCard.CssClass = "aspNetDisabled";
                    btnAddJobCard.Visible = false;
                    btnAddJobCard.ToolTip = "Work In Progress";
                }
                else if (dr["MRNButtonSatus"].ToString() == "D")
                {
                    // btnAddJobCard.CssClass = "aspNetDisabled";
                    btnAddJobCard.Visible = false;
                    btnAddJobCard.ToolTip = "NO MRN";
                }
                else
                    btnAddJobCard.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyPlanningDetails_AP_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                TextBox txtJobCardQty = (TextBox)e.Row.FindControl("txtJobCardQty");
                HiddenField hdnAvailableQty = (HiddenField)e.Row.FindControl("hdnAvailableQty");

                int JobCardActualQty = Convert.ToInt32(dr["PartCompletedQty"].ToString()) - Convert.ToInt32(dr["AssemplyCompleted"].ToString());

                if (Convert.ToInt32(dr["ItemQty"].ToString()) == Convert.ToInt32(dr["AssemplyCompleted"].ToString()))
                    chk.Visible = false;

                txtJobCardQty.Text = JobCardActualQty.ToString();
                hdnAvailableQty.Value = JobCardActualQty.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void gvAssemplyPlanningDetails_AP_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument.ToString());
    //        hdnPAPDID.Value = gvAssemplyPlanningDetails_AP.DataKeys[index].Values[0].ToString();

    //        AssemplyPlanningPDF();
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void gvMRNDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkMaterialReturn");
                if (dr["MaterialReturn"].ToString() == "Yes")
                    chk.Checked = true;
                else
                    chk.Checked = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMRNDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string MRNLocationname = gvMRNDetails.DataKeys[index].Values[3].ToString();

            if (e.CommandName == "ViewCutLayout")
            {
                string CutLayout = gvMRNDetails.DataKeys[index].Values[3].ToString();
                string PID = "IssuedLayout" + "\\" + MRNLocationname;
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, CutLayout, PID, ifrm);
            }
            else if (e.CommandName == "ViewReturnedLayout")
            {
                string ReturnedLayout = gvMRNDetails.DataKeys[index].Values[2].ToString();
                string PID = "MRNDocs" + "\\" + MRNLocationname;
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, ReturnedLayout, PID, ifrm);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvItemPartSNODetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkPartSno");
                if (string.IsNullOrEmpty(dr["SMC_JCHID"].ToString()))
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartSerielNumber_SW_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (string.IsNullOrEmpty(dr["SW_JCHID"].ToString()))
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartSNO_BTC_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (string.IsNullOrEmpty(dr["BFBTC_JCHID"].ToString()))
                    chk.Checked = false;
                else
                    chk.Checked = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPartAssemplyPlanningDetails_AP_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (dr["CompletedStatus"].ToString() == "Completed" && dr["ItemQty"].ToString() != dr["ItemCompleted"].ToString())
                    chk.Visible = true;
                else
                    chk.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyJobCardHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string AssemplyJobJCHID = gvAssemplyJobCardHeader.DataKeys[index].Values[0].ToString();

            ViewState["AssemplyJobJCHID"] = AssemplyJobJCHID;

            if (e.CommandName == "EditAJD")
                EditAssemplyJobCardDetails(AssemplyJobJCHID);

            Label lblJoNo = (Label)gvAssemplyJobCardHeader.Rows[index].FindControl("lblJoNo");
            ViewState["AWJCNo"] = lblJoNo.Text;

            if (e.CommandName == "IssueMRN")
            {
                //if (gvAssemplyJobCardHeader.DataKeys[index].Values[1].ToString() == "1")
                //    btnIssueMRN.Visible = false;
                //else
                //    btnIssueMRN.Visible = true;                
                lblJobNo_BOI.Text = lblJoNo.Text + "/" + ddlRFPNo.SelectedItem.Text;
                BindBoughtOutMRNIssuedDetails();
                BindAssemplyMRNIssuedDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowMRNBOIPopup();", true);
            }

            if (e.CommandName == "viewAssemplyWeldingDetails")
            {
                ViewPartAssemplyCardDetailsByJCHID();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAssemplyJobCardDetails();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAJDetails_AJD_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (dr["CompletedStatus"].ToString() == "0" || dr["QCStatus"].ToString() == "7")
                    chk.Visible = true;
                else
                    chk.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyJobCardHeader_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //btnEdit,btnDelete
                //CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                // LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                if (dr["Status"].ToString() == "1")
                {
                    //btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
                else
                {
                    //btnEdit.Visible = true;
                    if (objSession.type == 1)
                        btnDelete.Visible = true;
                    else
                        btnDelete.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAssemplyMRNIssuedDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                if (dr["IssuedStatus"].ToString() == "1")
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMRNIssueBOI_AP_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (dr["MRNID"].ToString() == "0" || (Convert.ToDecimal(dr["AvailWeight"].ToString()) <= 0))
                    chk.Visible = false;
                else
                    chk.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"
	
	private void PartToPartAssemplyJobCardDetails(string PAPDID)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        ds = objP.GetJCHIDByPAPDID("LS_GetJCHIDByPAPDID", PAPDID);
        objP.JCHID = Convert.ToInt32(ds.Tables[0].Rows[0]["JCHID"].ToString());
        var page = HttpContext.Current.CurrentHandler as Page;
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        url = url.ToLower();

        string[] pagename = url.ToString().Split('/');
        string Replacevalue = pagename[pagename.Length - 1].ToString();

        string Page = url.Replace(Replacevalue, "PartToPartAssemplyJobCardPrint.aspx?JCHID=" + objP.JCHID + "&&PAPDID=" + PAPDID + "");

        string s = "window.open('" + Page + "','_blank');";
        this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
	
	private void BindAssemplyPlanningJobCardDetailsForPartToPart()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            //objP.PRIDID = Convert.ToInt32(hdnPRIDID.Value);
            //objP.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());

            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.GetAssemplyPlanningJobCardDetailsForPartToPart();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyJobCardHeaderPartToPartPrint.DataSource = ds.Tables[0];
                gvAssemplyJobCardHeaderPartToPartPrint.DataBind();
            }
            else
            {
                gvAssemplyJobCardHeaderPartToPartPrint.DataSource = "";
                gvAssemplyJobCardHeaderPartToPartPrint.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlRFPNoAndCustomerLoad()
    {
        DataSet ds = new DataSet();
        try
        {
            objc = new cCommon();
            objP = new cProduction();
            DataSet dsRFPHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objP.getRFPCustomerNameByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName, rblRFPChange.SelectedValue);
            dsRFPHID = objP.GetRFPDetailsByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo, rblRFPChange.SelectedValue);
            ViewState["RFPDetails"] = dsRFPHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteJobCardDetailsByJCHID(string JCHID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {

            // objSession = (cSession)HttpContext.Current.Session["LoginDetails"];

            objP.JCHID = Convert.ToInt32(JCHID);
            objP.UserID = Convert.ToInt32(objSession.employeeid);
            ds = objP.DeleteJobcardDetailsByJCHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "SuccessMessage('Success','Job Card Details Deleted SuccessFully');ShowAddPartPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MSG", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAddPartPopUp();", true);

            BindPartDetailsByPRIDID();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewPartAssemplyCardDetailsByJCHID()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            ds = objP.GetPartAssemplyPlanningJobCardDetailsByJCHID("LS_GetPartAssemplyPlanningJobCardDetailsByJCHID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAJDetails_AJD.DataSource = ds.Tables[0];
                gvAJDetails_AJD.DataBind();
            }
            else
            {
                gvAJDetails_AJD.DataSource = "";
                gvAJDetails_AJD.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyJobcardPDFDetailsByJCHID(string JCHID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            Session["JCHID"] = JCHID;
            Session["RFPHID"] = ddlRFPNo.SelectedValue;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "JobCardExpensesSheet.aspx");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


            // Response.Redirect("JobCardExpensesSheet.aspx", false);

            //objP.JCHID = Convert.ToInt32(JCHID);
            //ds = objP.GetAssemplyJobCardPDFDetailsByJCHID();

            //lblProcessName_AP_PDFHeader.Text = "Sub Assembly Welding";
            //lblJobOrderID_AP_PDF.Text = ds.Tables[0].Rows[0]["AssemplyJobCardID"].ToString();
            //lblDate_AP_PDF.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();
            //lblRFPNo_AP_PDF.Text = ddlRFPNo.SelectedItem.Text;
            //lblContractorName_AP_PDF.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();//ddlContractorName_AP.SelectedItem.Text;
            //lblContractorTeamMemberName_AP_PDF.Text = ds.Tables[0].Rows[0]["ContractorTeamName"].ToString();//ddlContractorTeamMemberName_AP.SelectedItem.Text;
            //lblItemNameSize_AP_PDF.Text = lblItemNameSize_AP.Text;
            //lblDrawingName_AP_PDF.Text = ds.Tables[1].Rows[0]["DrawingName"].ToString();
            //lblPartName_AP_PDF.Text = "Sub Assembly";
            //lblItemQty_AP_PDF.Text = "1";
            //lblProcessName_AP_PDF.Text = "Sub Assembly";

            ////gvPartSerielNumber_AP_PDF.DataSource = ds.Tables[1];
            ////gvPartSerielNumber_AP_PDF.DataBind();
            //gvWPSDetails_AP_PDF.DataSource = ds.Tables[2];
            //gvWPSDetails_AP_PDF.DataBind();

            //gvBoughtOutItemIssuedDetails_AP_PDF.DataSource = ds.Tables[3];
            //gvBoughtOutItemIssuedDetails_AP_PDF.DataBind();

            //lblJobOrderRemarks_AP_PDF.Text = ds.Tables[0].Rows[0]["JobOrderRemarks"].ToString();
            //lblTotalCost_AP_PDF.Text = ds.Tables[0].Rows[0]["TotalCost"].ToString();

            //cQRcode objQr = new cQRcode();

            //string QrNumber = objQr.generateQRNumber(9);
            //string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            //string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            //string QrCode = objQr.QRcodeGeneration(Code);
            //if (QrCode != "")
            //{
            //    // imgQrcode.Attributes.Add("style", "display:block;");
            //    //  imgQrcode.ImageUrl = QrCode;
            //    ViewState["QrCode"] = QrCode;
            //    objQr.QRNumber = displayQrnumber;
            //    objQr.fileName = "SubAssemplyWelding" + "/" + ds.Tables[0].Rows[0]["AssemplyJobCardID"].ToString();
            //    objQr.createdBy = objSession.employeeid;
            //    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
            //    objQr.Pagename = pageName;
            //    objQr.saveQRNumber();
            //}
            //else
            //    ViewState["QrCode"] = "";

            //string Address = ds.Tables[5].Rows[0]["Address"].ToString();
            //string PhoneAndFaxNo = ds.Tables[5].Rows[0]["PhoneAndFaxNo"].ToString();
            //string Email = ds.Tables[5].Rows[0]["Email"].ToString();
            //string WebSite = ds.Tables[5].Rows[0]["WebSite"].ToString();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintAssemplyPlanningPDF('" + QrCode + "','" + Address + "','" + PhoneAndFaxNo + "','" + Email + "','" + WebSite + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindBoughtOutMRNIssuedDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());

            ds = objP.GetBoughtOutMRNBlockedDetailsByRFPHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMRNIssueBOI_AP.DataSource = ds.Tables[0];
                gvMRNIssueBOI_AP.DataBind();
            }
            else
            {
                gvMRNIssueBOI_AP.DataSource = "";
                gvMRNIssueBOI_AP.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyMRNIssuedDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.JCHID = Convert.ToInt32(ViewState["AssemplyJobJCHID"].ToString());
            ds = objP.GetAssemplyMRNissuedDetailsByJCHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyMRNIssuedDetails.DataSource = ds.Tables[0];
                gvAssemplyMRNIssuedDetails.DataBind();
            }
            else
            {
                gvAssemplyMRNIssuedDetails.DataSource = "";
                gvAssemplyMRNIssuedDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void EditAssemplyJobCardDetails(string JCHID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.JCHID = Convert.ToInt32(JCHID);
            ds = objP.GetAssemplyJobCardDetailsByJCHID();

            txtJobOrderRemarks_AP.Text = ds.Tables[0].Rows[0]["JobOrderRemarks"].ToString();
            txtIssueDate_AP.Text = ds.Tables[0].Rows[0]["IssueDateEdit"].ToString();
            txtDeliveryDate_AP.Text = ds.Tables[0].Rows[0]["DeliveryDateEdit"].ToString();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public DataSet BindAssemplyPlanningDetails()
    {
        cQuality objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objQt.PRIDID = Convert.ToInt32(hdnPRIDID.Value);
            ds = objQt.GetAssemplyPlanningDetailsbyRFPDIDAndPRIDID();

            ViewState["AP"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartAssemplyPlanningDetails_AP.DataSource = ds.Tables[0];
                gvPartAssemplyPlanningDetails_AP.DataBind();
            }
            else
            {
                gvPartAssemplyPlanningDetails_AP.DataSource = "";
                gvPartAssemplyPlanningDetails_AP.DataBind();
            }

            // lblJobOrderId_AP.Text = ds.Tables[1].Rows[0]["AssemplyJobCardID"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopUP", "ShowAssemplyPlanningPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public Color randomcolorgenerate()
    {
        Random random = new Random();
        return Color.FromArgb(random.Next(200, 255), random.Next(150, 255), random.Next(150, 255));
    }

    private void BindPartDetailsByPRIDID()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.PRIDID = Convert.ToInt32(hdnPRIDID.Value);
            ds = objP.GetPartDetailsByPRIDID();

            ViewState["PRIDIDPartDetails"] = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartDetails.DataSource = ds.Tables[0];
                gvPartDetails.DataBind();
            }
            else
            {
                gvPartDetails.DataSource = "";
                gvPartDetails.DataBind();
            }
            /// ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "", true); PaymentstatusFortheJobCard();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSheetWeldingDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.JCHID = Convert.ToInt32(hdnJCHID.Value);
            objP.Flag = ViewState["Flag"].ToString();

            ds = objP.GetJobCardDetailsByMPIDANdProcessOrder();

            //   lblDate_SW.Text = ds.Tables[0].Rows[0]["IssuedDate"].ToString();
            //lblRFPNo_SW.Text = ddlRFPNo.SelectedItem.Text;
            //lblContractorName_SW.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
            //lblCTMName_SW.Text = ds.Tables[0].Rows[0]["ContractorTeamName"].ToString();

            //lblItemNameSize_SW.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
            //lblDrawingName_SW.Text = ds.Tables[2].Rows[0]["DrawingName"].ToString();
            //lblPartName_SW.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
            //lblPartQty_SW.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
            //lblCategoryName_SW.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
            //lblGradeName_SW.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
            //lblThickness_SW.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
            //lblMRNNumber_SW.Text = ds.Tables[5].Rows[0]["MRNNumber"].ToString();


            //lblLength_SW.Text = ds.Tables[5].Rows[0]["Length"].ToString();
            //lblWidth_SW.Text = ds.Tables[5].Rows[0]["Width"].ToString();
            //lblWeight_SW.Text = ds.Tables[5].Rows[0]["Weight"].ToString();
            //lblDiameter_SW.Text = ds.Tables[5].Rows[0]["Diameter"].ToString();
            //lblCuttingProcess_SW.Text = ds.Tables[0].Rows[0]["CuttingProcessName"].ToString();

            //if (ds.Tables[3].Rows.Count > 0)
            //{
            //    gvPartSerielNumber_SW.DataSource = ds.Tables[3];
            //    gvPartSerielNumber_SW.DataBind();
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindProductionPartDetailsByMPIDAndProcessOrder()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            divQCFailureStages.Visible = false;
            divQCrequest.Visible = false;
            //  divQCFailureSingleStage.Visible = false;

            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.procesOrder = Convert.ToInt32(hdnProcessOrder.Value);
            objP.JCDID = Convert.ToInt32(ViewState["JCDID"]);
            objP.Flag = ViewState["JCFlag"].ToString();
            objP.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());

            ds = objP.GetProductionPartDetailsByMPIDAndProcessOrder();

            if (ViewState["JCFlag"].ToString() == "WorkInProgrss")
            {
                divQCrequest.Visible = true;

                btnSaveQCRequest.Text = "Job Completed";
                btnSaveQCRequest.CssClass = "btn btn-cons btn-save AlignTop WorkInProgress";

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvQCRequest.DataSource = ds.Tables[0];
                    gvQCRequest.DataBind();
                }
                else
                {
                    gvQCRequest.DataSource = "";
                    gvQCRequest.DataBind();
                }
                lblPartName_Production.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
            }
            else
            {
                if (ViewState["JCFlag"].ToString() == "QCFailure")
                    btnSaveQCRequest.Visible = false;
                else
                {
                    btnSaveQCRequest.Visible = true;
                    btnSaveQCRequest.Text = "QC Request";
                    btnSaveQCRequest.CssClass = "btn btn-cons btn-save AlignTop QCRequest";
                }

                //if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Fabrication & Welding" || ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Welding")
                //{
                divQCFailureStages.Visible = true;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvQCFailureStages.DataSource = ds.Tables[0];
                    gvQCFailureStages.DataBind();
                }
                else
                {
                    gvQCFailureStages.DataSource = "";
                    gvQCFailureStages.DataBind();
                }

                lblPartName_Production.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString()
                    + " / " + ds.Tables[2].Rows[0]["PartSNO"].ToString();

                // }
                //else
                //{
                //    // divQCFailureSingleStage.Visible = true;
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        gvQCFailureStages.DataSource = ds.Tables[0];
                //        gvQCFailureStages.DataBind();
                //    }
                //    else
                //    {
                //        gvQCFailureStages.DataSource = "";
                //        gvQCFailureStages.DataBind();
                //    }
                //}
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowQCRequestPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindPDFPreviewDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            divMRNDetails_p.Attributes.Add("style", "display:none;");
            divWPSDetails_p.Attributes.Add("style", "display:none;");
            divSheetMarkingAndCutting_p.Attributes.Add("style", "display:none;");
            divMarkingAndCutting_p.Attributes.Add("style", "display:none;");
            divBellowTangetCutting_p.Attributes.Add("style", "display:none;");
            divBellowTangetCuttingRoll_p.Attributes.Add("style", "display:none;");
            divBellowTangentCuttingExpandal_p.Attributes.Add("style", "display:none;");

            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.JCHID = Convert.ToInt32(hdnJCHID.Value);

            ds = objP.GetJobCardDetailsByMPIDANdProcessOrder();

            //  lblProcessNameHeader_p.Text = ds.Tables[2].Rows[0]["PartName"].ToString() + " - " + ds.Tables[0].Rows[0]["ProcessName"].ToString();

            ViewState["ProcessName"] = ds.Tables[0].Rows[0]["ProcessName"].ToString();

            btndownloaddocs_Click(null, null);

            //lblProcessName_p.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
            //lblJobOrderID_p.Text = ds.Tables[0].Rows[0]["SecondaryJID"].ToString();
            //lblDate_p.Text = ds.Tables[0].Rows[0]["IssuedDate"].ToString();
            //lblRFPNo_p.Text = ddlRFPNo.SelectedItem.Text;
            //lblContractorName_p.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();
            //lblContractorTeamMemberName_p.Text = ds.Tables[0].Rows[0]["ContractorTeamName"].ToString();

            //lblItemNameSize_p.Text = ds.Tables[1].Rows[0]["ItemName"].ToString();
            //lblDrawingName_p.Text = ds.Tables[2].Rows[0]["DrawingName"].ToString();
            //lblPartName_p.Text = ds.Tables[2].Rows[0]["PartName"].ToString();
            ////  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
            //lblMaterialCategory_p.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
            //lblMaterialGrade_p.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
            //lblThickness_p.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
            //lblMrnNumber_p.Text = ds.Tables[5].Rows[0]["MRNNumber"].ToString();
            //lblJobOrderRemarks_p.Text = ds.Tables[0].Rows[0]["JobOrderRemarks"].ToString();

            //if (ds.Tables[3].Rows.Count > 0)
            //{
            //    gvParSNO_p.DataSource = ds.Tables[3];
            //    gvParSNO_p.DataBind();
            //}

            //if (ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Sheet Marking & Cutting" || ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Marking & Cutting")
            //{
            //    divMRNDetails_p.Attributes.Add("style", "display:block;");
            //    divWPSDetails_p.Attributes.Add("style", "display:none;");

            //    if (ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Marking & Cutting")
            //    {
            //        lblLength_p.Text = ds.Tables[5].Rows[0]["Length"].ToString();
            //        lblWidth_p.Text = ds.Tables[5].Rows[0]["Width"].ToString();
            //        lblWeight_p.Text = ds.Tables[5].Rows[0]["Weight"].ToString();
            //        lblDiameter_p.Text = ds.Tables[5].Rows[0]["Diameter"].ToString();
            //        lblCuttingProcess_p.Text = ds.Tables[0].Rows[0]["CuttingProcessName"].ToString();

            //        divSheetMarkingAndCutting_p.Attributes.Add("style", "display:none;");
            //        divMarkingAndCutting_p.Attributes.Add("style", "display:block;");
            //    }
            //    else if (ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Sheet Marking & Cutting")
            //    {
            //        // gvPLYDetails_p.DataSource = ds.Tables[5];
            //        // gvPLYDetails_p.DataBind();
            //        divSheetMarkingAndCutting_p.Attributes.Add("style", "display:block;");
            //        divMarkingAndCutting_p.Attributes.Add("style", "display:none;");
            //    }

            //    if (ds.Tables[4].Rows.Count > 0)
            //    {
            //        gvMRNDetails_p.DataSource = ds.Tables[4];
            //        gvMRNDetails_p.DataBind();
            //    }
            //}

            //if (ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Sheet Welding")
            //{
            //    divMRNDetails_p.Attributes.Add("style", "display:none;");
            //    divWPSDetails_p.Attributes.Add("style", "display:block;");
            //    if (ds.Tables[4].Rows.Count > 0)
            //    {
            //        gvWPSDetails_p.DataSource = ds.Tables[4];
            //        gvWPSDetails_p.DataBind();
            //    }
            //}

            //if (ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Bellow Forming & Tangent Cutting")
            //{
            //    divBellowTangetCutting_p.Attributes.Add("style", "display:block;");

            //    lblID_p.Text = ds.Tables[5].Rows[0]["ID"].ToString();
            //    lblOD_p.Text = ds.Tables[5].Rows[0]["OD"].ToString();
            //    lblDepth_p.Text = ds.Tables[5].Rows[0]["DEP"].ToString();
            //    lblPitch_p.Text = ds.Tables[5].Rows[0]["PIT"].ToString();
            //    lblNumberOfConvolutions_p.Text = ds.Tables[5].Rows[0]["NOC"].ToString();
            //    if (ds.Tables[9].Rows[0]["FormingType"].ToString() == "Roll")
            //    {
            //        divBellowTangetCuttingRoll_p.Attributes.Add("style", "display:block;");
            //        lblNumberOfStages_p.Text = ds.Tables[9].Rows[0]["NumberOfStages"].ToString();
            //        lblRollFormingDevelopedPitch_p.Text = ds.Tables[9].Rows[0]["RollFormingDevelopmentPitch"].ToString();
            //        lblRollFormingInitialDepth_p.Text = ds.Tables[9].Rows[0]["RollFormingInitialDepth"].ToString();

            //        if (ds.Tables[4].Rows.Count > 0)
            //        {
            //            gvStages_p.DataSource = ds.Tables[4];
            //            gvStages_p.DataBind();
            //        }
            //    }
            //    else if (ds.Tables[9].Rows[0]["FormingType"].ToString() == "Expandal")
            //    {
            //        divBellowTangentCuttingExpandal_p.Attributes.Add("style", "display:block;");
            //        lblExpandalDevelopedPitch_p.Text = ds.Tables[9].Rows[0]["ExpandalDevelopedPitch"].ToString();
            //        lblExpandalFinalRolller_p.Text = ds.Tables[9].Rows[0]["ExpandalFinalRollover"].ToString();
            //    }
            //}

            //if (!string.IsNullOrEmpty(ds.Tables[7].Rows[0]["OfferQC"].ToString()))
            //    lblOfferQC_p.Text = ds.Tables[7].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");

            //if (ds.Tables[6].Rows[0]["ProcessName"].ToString() == "Sheet Welding")
            //    lblProcessTotalCost_p.Text = "";
            //else
            //    lblProcessTotalCost_p.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
            //lblPartQty_p.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowJobCardPreview();", true);
    }

    private void GeneratePDFFile(string ProcessName)
    {
        objc = new cCommon();
        try
        {
            string MAXPDFID = "";
            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));           

            divLSERPLogo.Visible = false;

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                // imgQrcode.Attributes.Add("style", "display:block;");
                //  imgQrcode.ImageUrl = QrCode;
                ViewState["QrCode"] = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = ProcessName + "/" + ViewState["JobOrderID"].ToString();
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                ViewState["QrCode"] = "";

            if (ProcessName == "SheetMarkingAndCutting")
            {
                divSheetMarkingAndCuttingPDF.Visible = true;
                divSheetMarkingAndCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divSheetMarkingAndCuttingPDF.Visible = false;
            }
            else if (ProcessName == "FabricationMarkingAndCutting")
            {
                divMarkingAndCuttingPDF.Visible = true;
                divMarkingAndCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divMarkingAndCuttingPDF.Visible = false;
            }
            else if (ProcessName == "SheetWelding")
            {
                divSheetWeldingPDF.Visible = true;
                divSheetWeldingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divSheetWeldingPDF.Visible = false;
            }

            else if (ProcessName == "FabricationAndWelding")
            {
                divFabricationAndWeldingPDF.Visible = true;
                divFabricationAndWeldingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divFabricationAndWeldingPDF.Visible = false;
            }
            else if (ProcessName == "BellowFormingAndTangentCutting")
            {
                divBellowFormingAndTangentCuttingPDF.Visible = true;
                divBellowFormingAndTangentCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divBellowFormingAndTangentCuttingPDF.Visible = false;
            }

            string div = sb.ToString();

            MAXPDFID = objc.GetMaximumNumberPDF();
            string htmlfile = ProcessName + "_" + ViewState["JobOrderID"].ToString() + ".html";
            string pdffile = ProcessName + "_" + ViewState["JobOrderID"].ToString() + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            SaveHtmlFile(URL, "Secondary Job Order Details", "", div);
            GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            objc.ReadhtmlFile(htmlfile, hdnpdfContent);

            divLSERPLogo.Visible = true;

            objc.SavePDFFile("SecondaryJobOrderDetails.aspx", pdffile, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; }   .col-sm-12.contractorborder { padding-top:5px; border-left: 2px solid;border-right: 2px solid; border-bottom: 2px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");
            w.WriteLine("</head><body>");

            w.WriteLine("<div class='print-page'>");
            w.WriteLine("<table><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            w.WriteLine("<div class='header' style='border-bottom:1px solid;'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            //  winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-6 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR </h3>");
            w.WriteLine("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("<p style='font-weight:500;color:#000;width: 103%;'>" + Address + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");
            w.WriteLine("<div class='col-sm-12 padding0' style='padding-top:0px;'>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot class='footer-space'><tr><td>");
            //
            w.WriteLine("<div class='col-sm-12'>");
            // w.WriteLine("<div class='footer' style='border: 0px solid #000 ! important;'>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:50px;'>");
            w.WriteLine("<div class='col-sm-6 p-t-20'><label style='color:black; font-weight:bolder;float:left;'>Quality Incharge</label></div>");
            w.WriteLine("<div class='col-sm-6' style='padding-left:16%;'><label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + ViewState["QrCode"].ToString() + "' class='Qrcode' /></div>");
            //  w.WriteLine("</div>");
            w.WriteLine("</div></div>");
            w.WriteLine("</td></tr></tfoot></table>");
            w.WriteLine("</div>");

            //w.WriteLine("<div style='text-align:center;padding-top:10px;font-size:20px;color:#00BCD4;'>");
            //w.WriteLine("</div>");
            //w.WriteLine("<div class='col-sm-12' style='text-align:center;padding-top:10px;font-size:20px;font-weight:bold;'>");
            //w.WriteLine("");
            //w.WriteLine("<div>");
            //w.WriteLine("<div class='col-sm-12' id='divLSERPLogo' style='padding-top: 30px;' runat='server'>");
            //w.WriteLine("<img src='" + topstrip + "' alt='' height='140px;'>");
            //w.WriteLine("</div>");
            //w.WriteLine(div);
            //w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void AssemplyPlanningPDF()
    {
        cQuality objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {

            objQt.PAPDID = Convert.ToInt32(hdnPAPDID.Value);
            ds = objQt.GetJobCardNameByPAPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < 1; i++)  // ds.Tables[0].Rows.Count;
                {
                    string FileName = "Assembly" + "_" + ds.Tables[0].Rows[i]["PAPDID"].ToString() + "_" + ds.Tables[0].Rows[i]["AssemplyJobCardID"].ToString() + ".pdf";
                    cCommon.DownLoad(FileName, PDFSavePath + FileName);
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','No Job Card Available');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Errror Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    public void GenerateAndSavePDF(string LetterPath, string strFile, string pdffile, string URL)
    {
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = new PdfDocument();
        try
        {
            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);
            if (File.Exists(strFile))
                File.Delete(strFile);

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 0;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 0;
            converter.Options.MarginBottom = 0;
            converter.Options.WebPageWidth = 700;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;

            doc = converter.ConvertUrl(URL);
            doc.Save(strFile);

            var ms = new System.IO.MemoryStream();

            //HttpContext.Current.Response.ContentType = "Application/pdf";
            //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdffile);
            //HttpContext.Current.Response.TransmitFile(strFile);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();

            doc.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            converter = null;
            doc = null;
            LetterPath = null;
            strFile = null;
            URL = null;
        }
    }

    private void GetWpsDetailsByWPSID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            ds = objP.GetWPSDetailsByWPSID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWPSDetails_SW.DataSource = ds.Tables[0];
                gvWPSDetails_SW.DataBind();
            }
            else
            {
                gvWPSDetails_SW.DataSource = "";
                gvWPSDetails_SW.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void EditSheetMarkingAndCuttingDetails(DataSet ds, string ProcessName)
    {
        try
        {
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "MNI")
            {
                hdnMPID.Value = ds.Tables[0].Rows[0]["MPID"].ToString();

                lblParNameAndtProcessName_J.Text = ViewState["ItemName"].ToString() + '/' + ds.Tables[1].Rows[0]["PartName"] + '/' + ds.Tables[0].Rows[0]["ProcessName"] + '/'
                    + ds.Tables[0].Rows[0]["PrimaryJID"].ToString();

                lblSecondaryJobOrderID_J.Text = ds.Tables[0].Rows[0]["PrimaryJID"].ToString() + "/" + ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblRFPNo_J.Text = ddlRFPNo.SelectedItem.Text;
                lblPartName_J.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                lblItemSizeName_J.Text = ViewState["ItemName"].ToString();
                lblDrawingName_J.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblGradeName_J.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                lblCategoryName_J.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                lblThickness_J.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();

                lblPartQuantity_J.Text = " ( " + ds.Tables[3].Rows[0]["PartQuantity"].ToString() + " ) ";

                if (ProcessName == "SMC")
                {
                    divMarkingAndCutting_J.Visible = false;
                    divSheetMarkingAndCutting_J.Visible = true;
                    divTubeIDAndTubeLength_J.Visible = true;

                    gvPLYDetails_J.DataSource = ds.Tables[5];
                    gvPLYDetails_J.DataBind();
                }
                else if (ProcessName == "MC")
                {
                    divMarkingAndCutting_J.Visible = true;
                    divSheetMarkingAndCutting_J.Visible = false;
                    divTubeIDAndTubeLength_J.Visible = false;

                    //                    FabricationTypeName      
                    if (ds.Tables[5].Rows[0]["FabricationTypeName"].ToString() == "PlateCutting")
                    {
                        txtDiameter_J.Text = ds.Tables[5].Rows[0]["Diameter"].ToString();
                        txtPlateLength_J.Text = ds.Tables[5].Rows[0]["PlateLength"].ToString();
                        txtPlateWidth_J.Text = ds.Tables[5].Rows[0]["PlateWidth"].ToString();

                        divPlateCutting_J.Visible = true;
                        divRings_J.Visible = false;
                        divGimbal_J.Visible = false;
                    }
                    else if (ds.Tables[5].Rows[0]["FabricationTypeName"].ToString() == "Rings")
                    {
                        txtOD_J.Text = ds.Tables[5].Rows[0]["OD"].ToString();
                        txtID_J.Text = ds.Tables[5].Rows[0]["ID"].ToString();
                        txtNoOfSegments_J.Text = ds.Tables[5].Rows[0]["NoOfSegments"].ToString();

                        divPlateCutting_J.Visible = false;
                        divRings_J.Visible = true;
                        divGimbal_J.Visible = false;
                    }
                    else if (ds.Tables[5].Rows[0]["FabricationTypeName"].ToString() == "Gimbal")
                    {
                        txtPlate1_J.Text = ds.Tables[5].Rows[0]["Plate1"].ToString();
                        txtPlate2_J.Text = ds.Tables[5].Rows[0]["Plate2"].ToString();
                        txtWidth_J.Text = ds.Tables[5].Rows[0]["width"].ToString();
                        txtPlate1Qty_J.Text = ds.Tables[5].Rows[0]["Plate1Qty"].ToString();
                        txtPlate2Qty_J.Text = ds.Tables[5].Rows[0]["Plate2Qty"].ToString();

                        divPlateCutting_J.Visible = false;
                        divRings_J.Visible = false;
                        divGimbal_J.Visible = true;
                    }
                }
                lblOfferQC_J.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");

                ddlContractorName_J.SelectedValue = ds.Tables[7].Rows[0]["CMID"].ToString();
                ddlContractorTeamMemberName_J.SelectedValue = ds.Tables[7].Rows[0]["CTDID"].ToString();
                txtIssueDate_J.Text = ds.Tables[7].Rows[0]["ISSUEDDATE"].ToString();
                txtDeliveryDate_J.Text = ds.Tables[7].Rows[0]["DELIVERYDATE"].ToString();
                txtPartQuantity_J.Text = ds.Tables[3].Rows[0]["JobCardPartQty"].ToString();
                txtTubeId_J.Text = ds.Tables[7].Rows[0]["TubeID"].ToString();
                txtTubeLength_J.Text = ds.Tables[7].Rows[0]["TubeLength"].ToString();
                txtJobOrderRemarks_J.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                gvItemPartSNODetails.DataSource = ds.Tables[6];
                gvItemPartSNODetails.DataBind();

                gvMRNDetails.DataSource = ds.Tables[8];
                gvMRNDetails.DataBind();

                hdnPartUnitReqWeight.Value = ds.Tables[9].Rows[0]["UnitReqWeight"].ToString();
                hdnPQReqWeight.Value = ds.Tables[9].Rows[0]["PoReqWeight"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowJobCardPopUp();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Transaction is Going On')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error Occured')", true);
            Log.Message(ex.ToString());
        }
    }

    private void BindPartSnoAndMRNDetails()
    {
        try
        {
            DataSet ds = new DataSet();
            objP = new cProduction();
            // objP.PartQty = Convert.ToInt32(arg.ToString());
            objP.MPID = Convert.ToInt32(hdnMPID.Value);
            objP.NextProcess = Convert.ToInt32(hdnNextProcess.Value);
            objP.JCHID = Convert.ToInt32(hdnEditJCHID.Value);
            ds = objP.GetMRNDetailsAndPartSNoDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItemPartSNODetails.DataSource = ds.Tables[0];
                gvItemPartSNODetails.DataBind();
            }
            else
            {
                gvItemPartSNODetails.DataSource = "";
                gvItemPartSNODetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvMRNDetails.DataSource = ds.Tables[1];
                gvMRNDetails.DataBind();
            }
            else
            {
                gvMRNDetails.DataSource = "";
                gvMRNDetails.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
                hdnPartUnitReqWeight.Value = ds.Tables[2].Rows[0]["UnitReqWeight"].ToString();

            // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowJobCardPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void EditSheetWeldingDetails(DataSet ds, string ProcessName)
    {
        try
        {
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Incomplete")
            {
                if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Welding" || ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Fabrication & Welding")
                {
                    //ViewState["Flag"] = "NextJob";
                    lblProcessName_SW.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + '/' + ds.Tables[0].Rows[0]["ProcessName"];
                    lblJobOrderID_SW.Text = ds.Tables[0].Rows[0]["PrimaryJID"].ToString() + "/" + ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                    //    BindSheetWeldingDetails();

                    hdnMPID.Value = ds.Tables[0].Rows[0]["MPID"].ToString();

                    lblRFPNo_SW.Text = ddlRFPNo.SelectedItem.Text;

                    //if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Sheet Welding")
                    //{
                    ddlContractorName_SW.SelectedValue = ds.Tables[6].Rows[0]["CMID"].ToString();
                    ddlContractorTeamName_SW.SelectedValue = ds.Tables[6].Rows[0]["CTDID"].ToString();
                    //  lblMRNNumber_SW.Text = ds.Tables[6].Rows[0]["MRNNumber"].ToString();
                    lblOfferQC_SW.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString();

                    txtIssuedDate_SW.Text = ds.Tables[6].Rows[0]["ISSUEDDATE"].ToString();
                    txtDeliveryDate_SW.Text = ds.Tables[6].Rows[0]["DELIVERYDATE"].ToString();
                    txtJobOrderRemarks_SW.Text = ds.Tables[6].Rows[0]["JobOrderRemarks"].ToString();

                    //lblPartQty_SW.Text = ds.Tables[5].Rows[0]["PartQty"].ToString();
                    //ViewState["PartQty"] = ds.Tables[5].Rows[0]["PartQty"].ToString();

                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        gvPartSerielNumber_SW.DataSource = ds.Tables[5];
                        gvPartSerielNumber_SW.DataBind();
                    }
                    else
                    {
                        gvPartSerielNumber_SW.DataSource = "";
                        gvPartSerielNumber_SW.DataBind();
                    }
                    // }
                    //else if (ds.Tables[0].Rows[0]["ProcessName"].ToString() == "Fabrication & Welding")
                    //{
                    //    lblPartQty_SW.Text = ds.Tables[10].Rows[0]["PartQty"].ToString();
                    //    // ViewState["PartQty"] = ds.Tables[10].Rows[0]["PartQty"].ToString();

                    //    //  lblContractorName_SW.Text = ds.Tables[10].Rows[0]["ContractorName"].ToString();
                    //    // lblCTMName_SW.Text = ds.Tables[10].Rows[0]["ContractorTeamName"].ToString();
                    //    lblMRNNumber_SW.Text = ds.Tables[10].Rows[0]["MRNNumber"].ToString();

                    //    //lblLength_SW.Text = ds.Tables[3].Rows[0]["MSMIDValue"].ToString();
                    //    //lblWidth_SW.Text = ds.Tables[4].Rows[0]["MSMIDValue"].ToString();
                    //    //lblWeight_SW.Text = ds.Tables[5].Rows[0]["MSMIDValue"].ToString();
                    //    //lblDiameter_SW.Text = ds.Tables[6].Rows[0]["MSMIDValue"].ToString();

                    //    lblOfferQC_SW.Text = ds.Tables[8].Rows[0]["OfferQC"].ToString();
                    //    if (ds.Tables[9].Rows.Count > 0)
                    //    {
                    //        gvPartSerielNumber_SW.DataSource = ds.Tables[9];
                    //        gvPartSerielNumber_SW.DataBind();
                    //    }
                    //    else
                    //    {
                    //        gvPartSerielNumber_SW.DataSource = "";
                    //        gvPartSerielNumber_SW.DataBind();
                    //    }
                    //}

                    lblItemNameSize_SW.Text = ViewState["ItemName"].ToString();
                    lblDrawingName_SW.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                    lblPartName_SW.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                    lblCategoryName_SW.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                    lblGradeName_SW.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                    lblThickness_SW.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();

                    GetWpsDetailsByWPSID();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowSheetWeldingPopUp();", true);
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Transaction is going On');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error Occured')", true);
            Log.Message(ex.ToString());
        }
    }

    private void EditBellowFormingAndTangentCuttingDetails(DataSet ds, string ProcessName)
    {
        try
        {
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Incomplete")
            {
                lblProcessName_BTC.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + '/' + ds.Tables[0].Rows[0]["ProcessName"]
                    + "(" + ds.Tables[7].Rows[0]["FormingType"].ToString() + ")";
                lblJobOrderID_BTC.Text = ds.Tables[0].Rows[0]["PrimaryJID"].ToString() + "/" + ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblRFPNo_BTC.Text = ddlRFPNo.SelectedItem.Text;

                lblItemName_BTC.Text = ViewState["ItemName"].ToString();
                lblDrawingName_BTC.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                //// lblPartName_BTC.Text = lblPartName.Text;
                lblCategoryName_BTC.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                lblGradeName_BTC.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                lblThickness_BTC.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();

                //lblContractorName_BTC.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                //lblContractorTeamMemberName_BTC.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();
                ddlContractorName_BTC.SelectedValue = ds.Tables[7].Rows[0]["CMID"].ToString();
                ddlContractorTeamName_BTC.SelectedValue = ds.Tables[7].Rows[0]["CTDID"].ToString();
                txtIssueDate_BTC.Text = ds.Tables[7].Rows[0]["ISSUEDDATE"].ToString();
                txtDeliveryDate_BTC.Text = ds.Tables[7].Rows[0]["DELIVERYDATE"].ToString();
                txtJobOrderRemarks_BTC.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                //lblMRNNumber_BTC.Text = ds.Tables[7].Rows[0]["MRNNumber"].ToString();
                lblOfferQC_BTC.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString();

                //lblPartQty_BTC.Text = ds.Tables[6].Rows[0]["PartQty"].ToString();
                // ViewState["PartQty"] = ds.Tables[6].Rows[0]["PartQty"].ToString();

                lblID_BTC.Text = ds.Tables[5].Rows[0]["ID"].ToString();
                lblOD_BTC.Text = ds.Tables[5].Rows[0]["OD"].ToString();
                lblDepth_BTC.Text = ds.Tables[5].Rows[0]["DEP"].ToString();
                lblPitch_BTC.Text = ds.Tables[5].Rows[0]["PIT"].ToString();
                lblNoOfConvolutions_BTC.Text = ds.Tables[5].Rows[0]["NOC"].ToString();

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvPartSNO_BTC.DataSource = ds.Tables[6];
                    gvPartSNO_BTC.DataBind();
                }
                else
                {
                    gvPartSNO_BTC.DataSource = "";
                    gvPartSNO_BTC.DataBind();
                }

                ViewState["FormingType"] = ds.Tables[7].Rows[0]["FormingType"].ToString();

                if (ds.Tables[7].Rows[0]["FormingType"].ToString() == "Expandal")
                {
                    divTangetCuttRoll_BTC.Visible = false;
                    divTangetCuttingExpandal_BTC.Visible = true;

                    txtExpandalDevelopedPitch_BTC.Text = ds.Tables[7].Rows[0]["ExpandalDevelopedPitch"].ToString();
                    txtExpandalFinalOver_BTC.Text = ds.Tables[7].Rows[0]["ExpandalFinalRollover"].ToString();
                }
                else if (ds.Tables[7].Rows[0]["FormingType"].ToString() == "Roll")
                {
                    divTangetCuttRoll_BTC.Visible = true;
                    divTangetCuttingExpandal_BTC.Visible = false;

                    txtRollFormingDevelopementPitch_BTC.Text = ds.Tables[7].Rows[0]["RollFormingDevelopmentPitch"].ToString();
                    txtRollFormingInitialDepth_BTC.Text = ds.Tables[7].Rows[0]["RollFormingInitialDepth"].ToString();
                    txtNumberOfStages_BTC.Text = ds.Tables[7].Rows[0]["NumberOfStages"].ToString();

                    gvStages_BTC.DataSource = ds.Tables[8];
                    gvStages_BTC.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowBellowsTangetCutPopup();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','Transaction is going On');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorNameChanged(DropDownList ddlContractorName, DropDownList ddlContractorTeamMemberName)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["ContractorTeamName"];
            if (ddlContractorName.SelectedIndex > 0)
            {
                dt.DefaultView.RowFilter = "CMID='" + ddlContractorName.SelectedValue + "'";
                dt.DefaultView.ToTable();
            }

            ddlContractorTeamMemberName.DataSource = dt;
            ddlContractorTeamMemberName.DataTextField = "Name";
            ddlContractorTeamMemberName.DataValueField = "CTDID";
            ddlContractorTeamMemberName.DataBind();
            ddlContractorTeamMemberName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorTeamnameChanged(DropDownList ddlContractorName, DropDownList ddlContractorTeamMemberName)
    {
        try
        {
            DataTable dt;
            DataView dv;
            try
            {
                dt = (DataTable)ViewState["ContractorTeamName"];
                dv = new DataView(dt);


                if (ddlContractorTeamMemberName.SelectedIndex > 0)
                {
                    dv.RowFilter = "CTDID='" + ddlContractorTeamMemberName.SelectedValue + "'";
                    dt = dv.ToTable();
                }

                ddlContractorName.SelectedValue = dt.Rows[0]["CMID"].ToString();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyPlanningJobCardDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            //objP.PRIDID = Convert.ToInt32(hdnPRIDID.Value);
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.GetAssemplyPlanningJobCardDetailsByPRIDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyJobCardHeader.DataSource = ds.Tables[0];
                gvAssemplyJobCardHeader.DataBind();
            }
            else
            {
                gvAssemplyJobCardHeader.DataSource = "";
                gvAssemplyJobCardHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 10;
            objc.JCHID = Convert.ToInt32(ViewState["JCHID"].ToString());
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objc.GetStaffDetailsByRFPHIDAndUserType();

            string[] str = ds.Tables[0].Rows[0]["EmployeeIDS"].ToString().Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = str[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Alert For Job Card QC Clearance";
                objAlerts.Message = "Job Card QC Requested From RFP No" + " / " + lblPartName_Production.Text + " / " + ds.Tables[0].Rows[0]["RFPNo"].ToString() + "And Job card No" + ds.Tables[0].Rows[0]["JobcardNo"].ToString();
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAssemplyAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        DataSet dscheck = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 10;
            objc.JCHID = Convert.ToInt32(0);
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objc.GetStaffDetailsByRFPHIDAndUserType();

            string[] str = ds.Tables[0].Rows[0]["EmployeeIDS"].ToString().Split(',');

            for (int i = 0; i < str.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = str[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Alert For Assemply Welding Job Card QC Clearance";
                objAlerts.Message = "Assemply Welding Job Card QC Requested From RFP No" + " / " + ds.Tables[0].Rows[0]["RFPNo"].ToString() + "And Job card No " + ViewState["AWJCNo"].ToString();
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvSecondaryJobOrderDetails.Rows.Count > 0)
            gvSecondaryJobOrderDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvPartDetails.Rows.Count > 0)
            gvPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        //if (gvAssemplyPlanningDetails_AP.Rows.Count > 0)
        //    gvAssemplyPlanningDetails_AP.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}