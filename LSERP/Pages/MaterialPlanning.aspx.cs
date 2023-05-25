using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Configuration;
using System.Data;
using System.IO;


public partial class Pages_MaterialPlanning : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cStores objSt;
    cSales objSales;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    cProduction objP;

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
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getRFPCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsRFPHID = objc.GetRFPDetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                ViewState["RFPDetails"] = dsRFPHID.Tables[0];
                ShowHideControls("add");
            }

            if (target == "deletegvrow")
            {
                objSales = new cSales();
                objSales.AttachementID = Convert.ToInt32(arg);
                string Message = objSales.DeleteAttachement();

                if (Message == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Attachements Deleted successfully');ShowAddPopUp();", true);

                //  BindAttachements(ViewState["MPID"].ToString());
            }
            if (target == "deletegvrowMPMD")
            {
                objSt = new cStores();
                objSt.MPMD = Convert.ToInt32(arg);
                string Message = objSt.DeleteMaterialPlanningMRNDetails();

                if (Message == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','MRN Details Deleted successfully');ShowAddPopUp();OpenTap('MRNBlocking')", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('information','" + Message + "');ShowAddPopUp();OpenTap('MRNBlocking')", true);

                BindMRNDetailsByMPID();
                BindMaterialPlanningDetails();
            }
            if (target == "UpdateRFPStatus")
                UpdateRFPStatus();
            //  this.RegisterPostBackControl();
            if (target == "ViewDrawings")
                ViewDrawings(Convert.ToInt32(arg.ToString()));
            if (target == "ReviewMP")
                ReviewMaterialPlanning(Convert.ToInt32(arg.ToString()));
            if (target == "SharePP")
                ShareProcessPlanning();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    //protected void btnSaveAttchement_Click(object sender, EventArgs e)
    //{
    //    objSales = new cSales();
    //    objc = new cCommon();
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        objSales.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"]);
    //        objSales.AttachementTypeName = Convert.ToInt32(ddlTypeName.SelectedValue);
    //        objSales.Description = txtDescription.Text;
    //        //   objSales.AttachementID = Convert.ToInt32(hdnAttachementID.Value);
    //        objSales.MPID = Convert.ToInt32(hdnMPID.Value);

    //        string MaxAttachementId = objSales.GetMaximumAttachementID();
    //        string AttachmentName = "";
    //        string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
    //        if (fAttachment.HasFile)
    //        {
    //            AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
    //            string[] extension = AttachmentName.Split('.');
    //            AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

    //            objc.Foldername = Session["MPDocsSavePath"].ToString();
    //            objc.FileName = AttachmentName;
    //            objc.PID = ViewState["MPID"].ToString();
    //            objc.AttachementControl = fAttachment;
    //            objc.SaveFiles();
    //        }

    //        objSales.AttachementName = AttachmentName;

    //        ds = objSales.SaveMaterialPlanningAttachements();

    //        if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachement Details Saved successfully');", true);


    //        //string StrStaffDocumentPath = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\";

    //        //if (!Directory.Exists(StrStaffDocumentPath))
    //        //    Directory.CreateDirectory(StrStaffDocumentPath);

    //        //if (AttachmentName != "")
    //        //    fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);

    //        BindAttachements(ViewState["MPID"].ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
    //        Log.Message(ex.ToString());
    //    }
    //    finally
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowAddPopUp();OpenTab('Documents');", true);
    //        objSales = null;
    //        ds = null;
    //    }
    //}

    protected void btnRFPQC_Click(object sender, EventArgs e)
    {
        try
        {
            Session["RFPHID"] = ddlRFPNo.SelectedValue;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "RFPQualityPlanning.aspx");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnViewMP_Click(object sender, EventArgs e)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "ViewMaterialPlanning.aspx");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (objc.Validate(divInput))
            {
                objMat.MPID = Convert.ToInt32(hdnMPID.Value);
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                objMat.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
                objMat.BOMID = Convert.ToInt32(ViewState["BOMID"].ToString());
                objMat.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
                objMat.MGMIDinProduction = Convert.ToInt32(ddlMaterialNameProduction.SelectedValue);

                if (ViewState["PartName"].ToString() != "BELLOWS")
                {
                    objMat.joborderweight = Convert.ToDecimal(txtJoborderWeight.Text);
                    if (ddlMaterialCategory.SelectedValue == "1" || ddlMaterialCategory.SelectedValue == "8")
                        objMat.jobtype = "jobOrder";
                    else
                        objMat.jobtype = "BoughtOutItems";
                }
                else
                {
                    objMat.joborderweight = 0;
                    objMat.jobtype = "";
                }

                //objMat.THKValue = Convert.ToDecimal(txtThicknessValueInBOM.Text);

                objMat.THKId = Convert.ToInt32(ddlThickness.SelectedValue);
                objMat.RequiredWeight = Convert.ToDecimal(txtRequiredWeight.Text);
                objMat.CreatedBy = objSession.employeeid;

                ds = objMat.SaveMaterialPlanningDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Planning Details  Saved successFully');ClosePartItemPopUP();", true);
                    hdnMPID.Value = ds.Tables[1].Rows[0]["MPID"].ToString();
                }
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('information','Material Planning Details  Already Exists');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Planning Details  Updated successFully');ClosePartItemPopUP();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ClosePartItemPopUP();", true);


                // btnBlockingMRN_Click(null, null);
                BindMaterialPlanningDetails();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            //string[] showdiv = { "addnew", "add", "view" };
            //ShowHideControls(showdiv);
            //   hdnMPID.Value = "0";
            // ddlPartName.Enabled = ddlCustomerName.Enabled = ddlRFPNo.Enabled = ddlItemName.Enabled = true;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnMaterialPlanningStatus_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objMat.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objMat.UpdatematerialPlanningStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "InComplete")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('information','Please Complete Material Planning for All Parts');", true);
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Planning Details Status Completed');hidePartDetailpopup();", true);
                BindItemDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnShareMaterialPlanning_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        Label lblMaterialPlanningStatus;
        string RFPStatus = "";
        try
        {
            for (int i = 0; i < gvMPItemDetails.Rows.Count; i++)
            {
                lblMaterialPlanningStatus = (Label)gvMPItemDetails.Rows[i].FindControl("lblMaterialPlanned");
                if (lblMaterialPlanningStatus.Text == "Completed")
                    RFPStatus = "Completed";
                else
                {
                    RFPStatus = "Incomplete";
                    break;
                }
            }
            if (RFPStatus == "Incomplete")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','all item should have need to Completed Move Into Next Stage');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RFPStatus", "UpdateRFPStatus();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnBlockingMRN_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        objSales = new cSales();
        //    string AttachmentName = "";
        try
        {
            if (Convert.ToDecimal(txtBlockedWeight.Text) > 0)
            {
                objSt.MPID = Convert.ToInt32(hdnMPID.Value);
                if (ddlMRNNumber_A.SelectedValue != "0")
                {
                    objSt.MRNID = Convert.ToInt32(ddlMRNNumber_A.SelectedValue.Split('/')[0].ToString());
                    objSt.LocationID = Convert.ToInt32(ddlMRNNumber_A.SelectedValue.Split('/')[1].ToString());
                }
                else
                {
                    objSt.MRNID = 0;
                    objSt.LocationID = 0;
                }
                objSt.blockedweight = txtBlockedWeight.Text;
                objSt.UserID = Convert.ToInt32(objSession.employeeid);
                string MaxAttachementId = objSales.GetMaximumAttachementID();

                //if (fRequiredShape.HasFile)
                //{
                //    objc = new cCommon();
                //    AttachmentName = Path.GetFileName(fRequiredShape.PostedFile.FileName);
                //    string[] extension = AttachmentName.Split('.');
                //    AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                //    objc.Foldername = Session["MPDocsSavePath"].ToString();
                //    objc.FileName = AttachmentName;
                //    objc.PID = hdnMPID.Value;
                //    objc.AttachementControl = fRequiredShape;
                //    objc.SaveFiles();
                //}
                objSt.RequiredShape = "";
                ds = objSt.saveMaterialPlanningMRNDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material MRN Details Saved Successfully');CloseAddPopUp();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');CloseAddPopUp();", true);

                BindMRNDetailsByMPID();
                BindMaterialPlanningDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Blocked Qty Should be Greater Zero');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancelSPOItem_Click(object sender, EventArgs e)
    {
        try
        {
            showhidePopUpDiv("addnew,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddMRN_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlThickness.SelectedIndex != 0 && ddlMaterialNameProduction.SelectedIndex != 0)
            {
                objSt = new cStores();
                objSt.MGMID = Convert.ToInt32(ddlMaterialNameProduction.SelectedValue);
                objSt.THKValue = Convert.ToInt32(ddlThickness.SelectedValue);
                ViewState["requiredweight"] = txtRequiredWeight.Text;
                objSt.GetMRNNumberByMGMIDAndTHKID(ddlMRNNumber_A);
                ViewState["MPID"] = hdnMPID.Value;
                BindMRNDetailsByMPID();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddMRNPopUP();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Please Select Thickness And Material Grade Name');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSavePP_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        bool msg = true;
        string ProcessIDs = "";
        string BOMID = "";
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];

            foreach (GridViewRow row in gvProcessPlanningDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                CheckBoxList chkpp = (CheckBoxList)row.FindControl("chkPP");
                if (chk.Checked)
                {
                    if (chkpp.SelectedValue != "")
                    {
                        string ids = "";
                        foreach (ListItem li in chkpp.Items)
                        {
                            if (li.Selected)
                            {
                                if (ids == "")
                                    ids = li.Value;
                                else
                                    ids = ids + "," + li.Value;
                            }
                        }

                        BOMID = gvProcessPlanningDetails.DataKeys[row.RowIndex].Values[1].ToString();

                        if (ProcessIDs == "")
                            ProcessIDs = BOMID + "-" + ids;
                        else
                            ProcessIDs = ProcessIDs + "#" + BOMID + "-" + ids;
                    }
                    else
                    {
                        msg = false;
                        break;
                    }
                }
            }

            if (msg)
            {
                objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
                objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                objP.UserID = Convert.ToInt32(objSession.employeeid);
                ds = objP.SavePartProcessPlanningDetails(ProcessIDs);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Part Process Planning Details Saved SuccessFuly');", true);
                BindProcessPlanningByRFPDID();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ErrorMessage('Error','select Process Name');", true);
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
        objc = new cCommon();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                BindItemDetails();
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

    //protected void ddlThickness_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlThickness.SelectedIndex != 0 && ddlMaterialNameProduction.SelectedIndex != 0)
    //        {
    //            objSt = new cStores();
    //            objSt.MGMID = Convert.ToInt32(ddlMaterialNameProduction.SelectedValue);
    //            objSt.THKValue = Convert.ToInt32(ddlThickness.SelectedValue);
    //            ViewState["requiredweight"] = txtRequiredWeight.Text;
    //            objSt.GetMRNNumberByMGMIDAndTHKID(ddlMRNNumber_A);
    //            ViewState["MPID"] = hdnMPID.Value;
    //            //BindAttachements(hdnMPID.Value);
    //            BindMRNDetailsByMPID();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}
    //protected void ddlItemName_OnSelectIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objMat = new cMaterials();
    //    try
    //    {
    //        if (ddlItemName.SelectedIndex > 0)
    //        {
    //            BindMaterialPlanningDetails();
    //            objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
    //            ds = objMat.GetPartDetailsByEDID(ddlPartName);

    //            string[] showdiv = { "addnew", "add", "view" };
    //            ShowHideControls(showdiv);

    //        }
    //        else
    //        {
    //            string[] showdiv = { "add" };
    //            ShowHideControls(showdiv);
    //         //   lblitemqty.Text = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void ddlMaterialCategory_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
            objMat.GetMaterialGradeDetailsByCategoryID(ddlMaterialNameProduction);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMRNNumber_A_OnSelectIndexchanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            if (ddlMRNNumber_A.SelectedIndex > 0)
            {
                hdnstockqty.Value = "0";
                if (ddlMRNNumber_A.SelectedIndex > 1)
                {
                    objSt.MRNID = Convert.ToInt32(ddlMRNNumber_A.SelectedValue.Split('/')[0].ToString());
                    objSt.LocationID = Convert.ToInt32(ddlMRNNumber_A.SelectedValue.Split('/')[1].ToString());
                    divstockmonitor.Visible = true;
                    BindStockMonitorDetails();
                }
                else
                {
                    divstockmonitor.Visible = false;
                }
                txtBlockedWeight.Text = Convert.ToString(Convert.ToDecimal(ViewState["requiredweight"].ToString()) - Convert.ToDecimal(ViewState["blockedweight"].ToString()));
                hdnblockweight.Value = txtBlockedWeight.Text;
                if (hdnstockqty.Value != "0" && (Convert.ToDecimal(hdnblockweight.Value) > Convert.ToDecimal(hdnstockqty.Value)))
                    txtBlockedWeight.Text = hdnstockqty.Value;
                hdnblockweight.Value = txtBlockedWeight.Text;
                showhidePopUpDiv("addnew,input,view");
            }
            else
                showhidePopUpDiv("addnew,view");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();OpenTab('MRNBlocking');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvBomFiles_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string FileName = gvBomFiles.DataKeys[index].Values[0].ToString();
            string EnquiryNumber = gvBomFiles.DataKeys[index].Values[1].ToString();
            //LayoutName
            cCommon.DownLoad(FileName, CustomerEnquiryHttpPath + EnquiryNumber.ToString() + "\\" + FileName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMPItemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string[] RFPDID = gvMPItemDetails.DataKeys[index].Values[0].ToString().Split('/');
            hdnRFPDID.Value = RFPDID[0];
            hdnEDID.Value = RFPDID[1];

            Label lblItemName = (Label)gvMPItemDetails.Rows[index].FindControl("lblItemName");
            ViewState["Itemname"] = lblItemName.Text;

            if (e.CommandName == "ADD")
            {
                BindMaterialPlanningDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPartPopUp();", true);
            }
            else if (e.CommandName == "AddProcessPlanning")
            {
                BindProcessPlanningdetailsByRFPDIDAndEDID();
                BindProcessPlanningByRFPDID();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowProcessPlanningPopup();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStockMonitorDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string SIID = gvStockMonitorDetails.DataKeys[index].Values[0].ToString();

            string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
            string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

            objSt.SIID = Convert.ToInt32(SIID);
            if (e.CommandName == "ViewCuttingLayout")
            {
                objc = new cCommon();
                string BalanceLayoutName = gvStockMonitorDetails.DataKeys[index].Values[1].ToString();
                string Locationname = gvStockMonitorDetails.DataKeys[index].Values[2].ToString();
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, BalanceLayoutName, "MRNDocs" + "\\" + Locationname, ifrm);
            }
            else
            {
                ds = objSt.GetStockInwardCertificatesDetailsBySIID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCertificates.DataSource = ds.Tables[0];
                    gvCertificates.DataBind();
                }
                else
                {
                    gvCertificates.DataSource = "";
                    gvCertificates.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();ShowCertificates();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMaterialPlanningDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {

            //  chckworkorder.Checked = false;
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string MPID = gvMaterialPlanningDetails.DataKeys[index].Values[0].ToString();

            hdnMPID.Value = MPID;

            string BOMID = gvMaterialPlanningDetails.DataKeys[index].Values[1].ToString();

            ViewState["BOMID"] = BOMID;
            Label lblPartName = (Label)gvMaterialPlanningDetails.Rows[index].FindControl("lblPartName");

            ViewState["PartName"] = lblPartName.Text;

            if (e.CommandName == "addPMP")
            {


                ViewState["PartName"] = lblPartName.Text.Split('/')[0].ToString();

                if (ViewState["PartName"].ToString() != "BELLOWS")
                {
                    lblRequiredWeight.Attributes.Add("style", "display:none;");
                    divjoborderweight.Attributes.Add("style", "display:block;");
                }
                else
                {
                    lblRequiredWeight.Attributes.Add("style", "display:block;");
                    divjoborderweight.Attributes.Add("style", "display:none;");
                }

                lblPartName_API.Text = "( " + lblPartName.Text + " )";

                objMat = new cMaterials();
                objMat.BOMID = Convert.ToInt32(BOMID);
                objMat.EDID = Convert.ToInt32(hdnEDID.Value);
                objMat.MPID = Convert.ToInt32(hdnMPID.Value);

                ds = objMat.GetMaterialGradeNameUsedInDesignByBOMIDAndEDID();

                if (ds.Tables[7].Rows[0]["JobCardStatus"].ToString() == "InComplete")
                {
                    if (ds.Tables[6].Rows[0]["Message"].ToString() == "InComplete")
                    {
                        objMat.GetMaterialCategoryNameDetailsInMaterialPLanning(ddlMaterialCategory);
                        objMat.GetMaterialThicknessDetailsformaterialPlanning(ddlThickness);

                        clearFields();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblMaterialNameDesign.Text = "In BOM : " + ds.Tables[0].Rows[0]["GradeName"].ToString();
                            // ddlMaterialCategory.SelectedValue = ds.Tables[0].Rows[0]["CID"].ToString();
                        }

                        //if (ds.Tables[0].Rows.Count > 0)
                        //    ddlMaterialNameProduction.SelectedValue = ds.Tables[0].Rows[0]["MGMID"].ToString();

                        //   txtPartProduction.Text = ds.Tables[1].Rows[0]["Quantity"].ToString();
                        // txtRequiredWeight.Text = ds.Tables[2].Rows[0]["WT"].ToString();

                        if (ds.Tables[2].Rows.Count > 0)
                            lblRequiredWeight.Text = "In BOM:" + ds.Tables[2].Rows[0]["WT"].ToString();
                        else
                            lblRequiredWeight.Text = "In BOM:" + "0.00";

                        if (ds.Tables[3].Rows.Count > 0)
                            lblThkInBOM.Text = "In BOM:" + ds.Tables[3].Rows[0]["THK"].ToString();
                        else
                            lblThkInBOM.Text = "In BOM:" + "0.00";

                        if (ds.Tables[5].Rows.Count > 0)
                            lblAWT.Text = "In Bom:" + ds.Tables[5].Rows[0]["AWT"].ToString();
                        else
                            lblAWT.Text = "In Bom:" + "0.00";

                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            ddlMaterialCategory.SelectedValue = ds.Tables[4].Rows[0]["CID"].ToString();
                            //  ddlMaterialNameProduction.SelectedValue = ds.Tables[4].Rows[0]["MGMIDProduction"].ToString();
                            //  txtThicknessValueInBOM.Text = ds.Tables[4].Rows[0]["THKValue"].ToString();
                            ddlThickness.SelectedValue = ds.Tables[4].Rows[0]["THKValue"].ToString();
                            txtRequiredWeight.Text = ds.Tables[4].Rows[0]["UnitRequiredWeight"].ToString();

                            txtJoborderWeight.Text = ds.Tables[4].Rows[0]["UnitJobWeight"].ToString();
                        }

                        objMat.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
                        objMat.GetMaterialGradeDetailsByCategoryID(ddlMaterialNameProduction);

                        if (ds.Tables[4].Rows.Count > 0)
                            ddlMaterialNameProduction.SelectedValue = ds.Tables[4].Rows[0]["MGMIDProduction"].ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowAddPartItemPopUP();", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "InfoMessage('Information','" + ds.Tables[6].Rows[0]["Message"].ToString() + "')", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "InfoMessage('Information','" + ds.Tables[7].Rows[0]["JobCardStatus"].ToString() + "')", true);
            }

            if (e.CommandName == "AddAttachements")
            {
                dt = (DataTable)ViewState["MPDetails"];
                dt.DefaultView.RowFilter = "MPID='" + MPID + "'";

                lblPartGradeThkIN_A.Text = "(" + dt.DefaultView.ToTable().Rows[0]["PartName"].ToString() + "  " + "-" + dt.DefaultView.ToTable().Rows[0]["GradeNameProduction"].ToString() + "  " + " - " + dt.DefaultView.ToTable().Rows[0]["THKValue"].ToString() + ")";
                showhidePopUpDiv("addnew,view");
                objSt = new cStores();
                objSt.MGMID = Convert.ToInt32(dt.DefaultView.ToTable().Rows[0]["MGMIDProduction"].ToString());
                objSt.THKValue = Convert.ToDecimal(dt.DefaultView.ToTable().Rows[0]["THKValue"].ToString());
                ViewState["requiredweight"] = dt.DefaultView.ToTable().Rows[0]["TotalRequiredWeight"].ToString();
                objSt.GetMRNNumberByMGMIDAndTHKID(ddlMRNNumber_A);
                ViewState["MPID"] = MPID;
                BindMRNDetailsByMPID();

                // BindAttachements(MPID);
                hdnrequiredqty.Value = ViewState["requiredweight"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowAddPopUp();", true);
            }
            if (e.CommandName == "ViewCertificates")
                BinDCertificatesAndBomFilesByPartName("Certificates");

            if (e.CommandName == "ViewBomFiles")
                BinDCertificatesAndBomFilesByPartName("BomFiles");

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument.ToString());
    //        int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

    //        if (e.CommandName.ToString() == "ViewDocs")
    //        {
    //            objc = new cCommon();

    //            string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();

    //            //string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "/";                
    //            //ViewState["FileName"] = FileName;
    //            //ifrm.Attributes.Add("src", BasehttpPath + FileName);
    //            //string imgname = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName;
    //            //if (File.Exists(imgname))
    //            //{
    //            //    //ViewState["ifrmsrc"] = imgname;
    //            //    ViewState["ifrmsrc"] = BasehttpPath + FileName;
    //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
    //            //}
    //            //else
    //            //{
    //            //    ifrm.Attributes.Add("src", "");
    //            //    ViewState["ifrmsrc"] = "";
    //            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
    //            //}

    //            objc.ViewFileName(Session["MPDocsSavePath"].ToString(), Session["MPDocsHttpPath"].ToString(), FileName, hdnMPID.Value, ifrm);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void gvBlockingMRN_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string FileName = gvBlockingMRN.DataKeys[index].Values[0].ToString();
            objc.ViewFileName(Session["MPDocsSavePath"].ToString(), Session["MPDocsHttpPath"].ToString(), FileName, hdnMPID.Value, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMaterialPlanningDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAddAttach = (LinkButton)e.Row.FindControl("btnAddAttach");
                LinkButton btnAdd = (LinkButton)e.Row.FindControl("btnAdd");

                if (dr["MPID"].ToString() == "0")
                    btnAddAttach.CssClass = "aspNetDisabled";
                else
                    btnAddAttach.CssClass = "";

                // || dr["btnEnable"].ToString() == "1"

                if ((ViewState["MPStatus"].ToString() == "Completed") && dr["MPID"].ToString() != "0")
                    btnAdd.Visible = false;
                else
                    btnAdd.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStockMonitorDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnCertificates = (LinkButton)e.Row.FindControl("lbtnView");
                //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                //trigger.ControlID = btnCertificates.UniqueID;
                //trigger.EventName = "Click";
                //upView.Triggers.Add(trigger);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnCertificates);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMPItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnreviewMP = (LinkButton)e.Row.FindControl("btnReviewMP");

                if (objSession.type == 1)
                    btnreviewMP.Visible = true;
                else
                    btnreviewMP.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvBlockingMRN_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (ViewState["MPStatus"].ToString() == "Completed")
                    btnDelete.Visible = false;
                else
                    btnDelete.Visible = true;

                if (string.IsNullOrEmpty(dr["ApprovalStatus"].ToString()) || dr["ApprovalStatus"].ToString() == "Rejected")
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;

                if (objSession.type == 1)
                    btnDelete.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvProcessPlanningDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList chk = (CheckBoxList)e.Row.FindControl("chkPP");
                CheckBox chkQC = (CheckBox)e.Row.FindControl("chkQC");

                if (dr["PPDID"].ToString() == "0")
                {
                    chkQC.Visible = true;
                    DataTable dt = new DataTable();

                    dt = (DataTable)ViewState["PlanningName"];

                    chk.DataSource = dt;
                    chk.DataTextField = "PlanningName";
                    chk.DataValueField = "PPNID";
                    chk.DataBind();
                    //chk.Items.Insert(0, new ListItem("--Select--", "0")); 
                }
                else
                    chkQC.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvProcessPlanning_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objP.PPDID = Convert.ToInt32(gvProcessPlanning.DataKeys[index].Values[0].ToString());
            ds = objP.DeleteProcessPlanningByPPDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Process Planning Deleted SuccessFully');", true);
            BindProcessPlanningByRFPDID();
            BindProcessPlanningdetailsByRFPDIDAndEDID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvProcessPlanning_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                if (dr["PPStatus"].ToString() == "InComplete")
                    btnDelete.Visible = true;
                else
                    btnDelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ReviewMaterialPlanning(int index)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(gvMPItemDetails.DataKeys[index].Values[0].ToString().Split('/')[0].ToString());
            ds = objP.ReviewMaterialPlanningDetailsByItemName();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "SuccessMessage('Success','Process Successfully reviewed');", true);
                BindItemDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewDrawings(int index)
    {
        try
        {
            string EnquiryNumber = gvMPItemDetails.DataKeys[index].Values[2].ToString();
            string DrawingName = gvMPItemDetails.DataKeys[index].Values[1].ToString();

            string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
            string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
            // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
            string BasehttpPath = DrawingDocumentHttppath + EnquiryNumber + "/";
            string FileName = BasehttpPath + DrawingName;

            ifrm.Attributes.Add("src", FileName);
            if (File.Exists(DrawingDocumentSavePath + EnquiryNumber + '/' + DrawingName))
            {
                string s = "window.open('" + FileName + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMRNDetailsByMPID()
    {
        DataSet ds = new DataSet();
        try
        {
            objSt.MPID = Convert.ToInt32(hdnMPID.Value);
            ds = objSt.GetMaterialPlanningMRNDetailsByMPID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBlockingMRN.DataSource = ds.Tables[0];
                gvBlockingMRN.DataBind();
            }
            else
            {
                gvBlockingMRN.DataSource = "";
                gvBlockingMRN.DataBind();
            }
            ViewState["blockedweight"] = Convert.ToDecimal(ds.Tables[1].Rows[0]["Blockedweight"].ToString());
            hdnblockedqty.Value = ViewState["blockedweight"].ToString();
            //lblMRNBlockedWeight.Text = ViewState["blockedweight"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindStockMonitorDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = objSt.GetStockMonitorReportDetailsbyMRNID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Remove("Cost");

                gvStockMonitorDetails.DataSource = ds.Tables[0];
                gvStockMonitorDetails.DataBind();
                hdnstockqty.Value = ds.Tables[0].Rows[0]["ActualStock"].ToString();

            }
            else
            {
                gvStockMonitorDetails.DataSource = "";
                gvStockMonitorDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //public void BindAttachements(string MPID)
    //{
    //    objSales = new cSales();
    //    try
    //    {
    //        DataSet dsGetAttachementsDetails = new DataSet();
    //        objSales.MPID = Convert.ToInt32(MPID);

    //        dsGetAttachementsDetails = objSales.GetAttachementDetailsByMPID();

    //        if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
    //        {
    //            ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];
    //            gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
    //            gvAttachments.DataBind();
    //        }
    //        else
    //        {
    //            gvAttachments.DataSource = "";
    //            gvAttachments.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    private void BindItemDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objMat.GetItemDetailsByRFPHID(null, "LS_GetItemDetailsByRFPHIDInMaterialPlanning");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMPItemDetails.DataSource = ds.Tables[0];
                gvMPItemDetails.DataBind();
            }
            else
            {
                gvMPItemDetails.DataSource = "";
                gvMPItemDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMaterialPlanningDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objMat.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objMat.EDID = Convert.ToInt32(hdnEDID.Value);

            ds = objMat.GetMaterialPlanningDetailsByRFPHIDAndEDID();
            ViewState["MPDetails"] = ds.Tables[0];
            ViewState["EnquiryID"] = ds.Tables[1].Rows[0]["EnquiryNumber"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MPStatus"] = ds.Tables[2].Rows[0]["MPStatus"].ToString();

                gvMaterialPlanningDetails.DataSource = ds.Tables[0];
                gvMaterialPlanningDetails.DataBind();
                btnMaterialPlanningStatus.Visible = true;
            }
            else
            {
                gvMaterialPlanningDetails.DataSource = "";
                gvMaterialPlanningDetails.DataBind();
                btnMaterialPlanningStatus.Visible = false;
            }
            lblItemName_P.Text = "( " + ViewState["Itemname"].ToString() + " )" + " / " + "Item Qty : " + ds.Tables[2].Rows[0]["Itemqty"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void clearFields()
    {
        //ddlMaterialCategory.SelectedIndex = 0;
        //ddlMaterialNameProduction.SelectedIndex = 0;
        txtJoborderWeight.Text = "";
        txtRequiredWeight.Text = "";
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divAddNew.Visible = divOutput.Visible = false;

        string[] mode = divids.Split(',');

        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void showhidePopUpDiv(string divids)
    {
        divAdd_A.Visible = divInput_A.Visible = divOutput_A.Visible = false;
        try
        {
            string[] mode = divids.Split(',');

            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "addnew":
                        divAdd_A.Visible = true;
                        break;
                    case "view":
                        divOutput_A.Visible = true;
                        break;
                    case "input":
                        divInput_A.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateRFPStatus()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objMat.updateRFPStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Planning Status Completed SuccessFully')", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BinDCertificatesAndBomFilesByPartName(string Flagname)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            lblPartname.Text = ViewState["PartName"].ToString();

            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.BOMID = Convert.ToInt32(ViewState["BOMID"].ToString());
            objP.Flag = Flagname;
            ds = objP.GetCertificateAndBomFilesName();

            if (Flagname == "Certificates")
            {
                divMaterialInspectionCertificates.Visible = true;
                divBomFiles.Visible = false;

                gvMaterialInspectionCertificates.DataSource = ds.Tables[0];
                gvMaterialInspectionCertificates.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowCertificatesandBomFilePopUp()", true);
            }
            else
            {
                divMaterialInspectionCertificates.Visible = false;
                divBomFiles.Visible = true;

                gvBomFiles.DataSource = ds.Tables[0];
                gvBomFiles.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowCertificatesandBomFilePopUp()", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindProcessPlanningdetailsByRFPDIDAndEDID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objP.GetProcessPlanningDetailsByRFPDIDAndEDID();

            ViewState["PlanningName"] = ds.Tables[1];
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvProcessPlanningDetails.DataSource = ds.Tables[0];
                gvProcessPlanningDetails.DataBind();
            }
            else
            {
                gvProcessPlanningDetails.DataSource = "";
                gvProcessPlanningDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindProcessPlanningByRFPDID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.GetProcessPlanningByRFPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvProcessPlanning.DataSource = ds.Tables[0];
                gvProcessPlanning.DataBind();
            }
            else
            {
                gvProcessPlanning.DataSource = "";
                gvProcessPlanning.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShareProcessPlanning()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.UpdateProcessPlanningSharedStatusByRFPDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Process Planning Shared SuccessFully');", true);
            BindProcessPlanningByRFPDID();
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
        if (gvMaterialPlanningDetails.Rows.Count > 0)
            gvMaterialPlanningDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        //if (gvAttachments.Rows.Count > 0)
        //    gvAttachments.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvMPItemDetails.Rows.Count > 0)
            gvMPItemDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvProcessPlanningDetails.Rows.Count > 0)
            gvProcessPlanningDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion 
}