using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MaterialInwardQCClearenceDetails : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cQuality objQt;
    cStores objSt;

    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objPc = new cPurchase();
                objc = new cCommon();
                objc.GetLocationDetails(ddlLocationName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
            if (target == "deletegvrow")
            {
                objQt = new cQuality();
                int MIQCDID = Convert.ToInt32(arg);
                DataSet ds = objQt.DeleteMaterialInwardQCClearanceDetailsByMIDID(MIQCDID);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Quantity Deleted successfully');ShowAddQCPopup();", true);
                    BindDCAvailQtyDetails();
                    BindMaterialInwardQCClearanceQtyDetails();
                }
            }

            if (target == "ShareQC")
            {
                DataSet ds = new DataSet();
                objQt = new cQuality();
                try
                {
                    objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
                    objQt.LOcationID = Convert.ToInt32(ddlLocationName.SelectedValue);
                    objQt.CreatedBy = objSession.employeeid;

                    ds = objQt.UpdateMaterialInwardQCCompletedStatusByMIDID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','QC Shared Sussfulliy');HideAddQCPopup();", true);
                        BindAddedMaterials();
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');ShowAddQCPopup();", true);
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            if (target == "PrintMRN")
                bindMRNPrintDetails(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblMRNChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            BindAddedMaterials();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"


    protected void ddlLocationName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocationName.SelectedIndex > 0)
            BindAddedMaterials();
        else
        {
            gvaddeditems.DataSource = "";
            gvaddeditems.DataBind();
        }
    }

    #endregion

    #region"CheckBox Events"

    //protected void chkitems_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objQt = new cQuality();
    //    try
    //    {
    //        CheckBox chkModules = (CheckBox)sender;
    //        GridViewRow row = (GridViewRow)chkModules.NamingContainer;
    //        objQt.MIDID = Convert.ToInt32(gvaddeditems.DataKeys[row.RowIndex].Values[1].ToString());
    //        ds = objQt.UpdateCertificateMandatoryStatusByMIDID();
    //        if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Updated');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"Button Events"

    protected void btnSaveMIQC_Click(object sender, EventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
            objQt.MTR = rblMTR.SelectedValue;
            objQt.TPSNo = txtTPSNO.Text;
            objQt.Visual = rblVisual.SelectedValue;
            objQt.CheckTest = rblCheckTest.SelectedValue;
            objQt.AddtionalRequirtment = rblAddtionalRequirtment.SelectedValue;
            objQt.MeasuredDimension = txtMeasureddimension.Text;
            objQt.OriginalMarking = txtOriginalMarking.Text;
            objQt.CreatedBy = objSession.employeeid;
            objQt.LOcationID = Convert.ToInt32(ddlLocationName.SelectedValue);

            objQt.InstrumentRefNo = txtInstrumentRefNo.Text;
            objQt.CalreportNo = txtCalReportNo.Text;
            objQt.DoneOn = DateTime.ParseExact(txtDoneOn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.DueOn = DateTime.ParseExact(txtDueOn.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            objQt.InstrumentRefNo1 = txtInstrumentRefNo1.Text;
            objQt.CalreportNo1 = txtCalReportNo1.Text;
            objQt.DoneOn1 = DateTime.ParseExact(txtDoneOn1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.DueOn1 = DateTime.ParseExact(txtDueOn1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.PMI = txtPMI.Text;

            ds = objQt.SaveMaterialInwardQualityClearebnce();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','MI QC details Saved Sussfulliy');ShowAddQCPopup();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','MI QC details Updated Sussfulliy');ShowAddQCPopup();", true);

            BindAddedMaterials();
            BindMaterialInwardQCClearanceHeader();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveQCQtyDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            if (Convert.ToDecimal(txtQuantity.Text) > 0 && txtQuantity.Text != "")
            {
                objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
                objQt.Remarks = txtRemarks.Text;

                if (Convert.ToDecimal(txtQuantity.Text) > 0 && txtQuantity.Text != "")
                    objQt.Quantity = Convert.ToDecimal(txtQuantity.Text);
                else
                    objQt.Quantity = 0;
                objQt.MaterialQtyStatus = rblQtyStatus.SelectedValue;
                objQt.CreatedBy = objSession.employeeid;
                objQt.LOcationID = Convert.ToInt32(ddlLocationName.SelectedValue);

                ds = objQt.SaveMaterialInwardQCClearanceDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Quntity Saved Sussfulliy');ShowAddQCPopup();", true);
                    BindDCAvailQtyDetails();
                    BindMaterialInwardQCClearanceQtyDetails();
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','Quantity 0 is not Allowed');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveCertificate_Click(object sender, EventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        cSales objSales = new cSales();

        string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
        string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();
        try
        {
            foreach (GridViewRow row in gvCertficateDetails.Rows)
            {
                objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);

                TextBox txtCertificatesNo = (TextBox)gvCertficateDetails.Rows[row.RowIndex].FindControl("txtCertificatesNo");
                TextBox txtReceivedDate = (TextBox)gvCertficateDetails.Rows[row.RowIndex].FindControl("txtReceivedDate");
                FileUpload fAttachement = (FileUpload)gvCertficateDetails.Rows[row.RowIndex].FindControl("fAttachment");
                DropDownList ddlQCStatus = (DropDownList)gvCertficateDetails.Rows[row.RowIndex].FindControl("ddlQCStatus");
                TextBox txtRemarks = (TextBox)gvCertficateDetails.Rows[row.RowIndex].FindControl("txtRemarks");
                DropDownList ddlcertificateName = (DropDownList)gvCertficateDetails.Rows[row.RowIndex].FindControl("ddlCertificateName");

                string AttachmentName = "";
                if (fAttachement.HasFile)
                {
                    string MaxAttachementId = objSales.GetMaximumAttachementID();
                    string extn = Path.GetExtension(fAttachement.PostedFile.FileName).ToUpper();
                    AttachmentName = Path.GetFileName(fAttachement.PostedFile.FileName);
                    string[] extension = AttachmentName.Split('.');
                    AttachmentName = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "").ToString() + '_'
                        + MaxAttachementId + '.' + extension[extension.Length - 1];
                }
                //string CFID = gvCertficateDetails.DataKeys[row.RowIndex].Values[1].ToString();

                objQt.CFID = Convert.ToInt32(ddlcertificateName.SelectedValue);
                objQt.AttachementName = AttachmentName;
                objQt.CertficateNo = txtCertificatesNo.Text;
                objQt.ReceivedDate = DateTime.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objQt.CreatedBy = objSession.employeeid;
                objQt.CertificateQCStatus = ddlQCStatus.SelectedValue;
                objQt.Remarks = txtRemarks.Text;

                ds = objQt.SaveMaterialInwardQCCertficates();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MIQCCID"].ToString()))
                {
                    objc = new cCommon();
                    objc.Foldername = StoresDocsSavePath;
                    objc.FileName = AttachmentName;
                    objc.PID = "MaterialInwardQCCertificates";
                    objc.AttachementControl = fAttachement;
                    objc.SaveFiles();
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Certificate details Saved Sussfulliy');ShowAddPopUp();", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Certificate details Updated Sussfulliy');ShowAddPopUp();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            //BindCerficatesDetails();
            BindCertificateAddedDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnShareStockCertificates_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            if (gvcertificatesAddedDetails.Rows.Count > 0)
            {
                objPc.MIDID = hdnMIDID.Value;
                ds = objPc.UpdateStockCertificateSharedStatusByMIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Certificates Shared successfully');", true);
                BindCertificateAddedDetails();
                BindAddedMaterials();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "InfoMessage('Information','No Records Added');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void btnTCMandatory_OnClick(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objQt = new cQuality();
    //    try
    //    {
    //        foreach (GridViewRow row in gvaddeditems.Rows)
    //        {
    //            CheckBox chkCF = (CheckBox)row.FindControl("chkCFMandatory");
    //            if (chkCF.Checked)
    //            {
    //                objQt.MIDID = Convert.ToInt32(gvaddeditems.DataKeys[row.RowIndex].Values[1].ToString());
    //                ds = objQt.UpdateCertificateMandatoryStatusByMIDID();
    //            }
    //            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Updated');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"GridView Events"

    protected void gvaddeditems_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            DataTable dt = new DataTable();
            hdnMIDID.Value = gvaddeditems.DataKeys[index].Values[1].ToString();
            hdnSPODID.Value = gvaddeditems.DataKeys[index].Values[0].ToString();

            Label lblMRNNumber = (Label)gvaddeditems.Rows[index].FindControl("lblMRNNumber");
            Label lblCategoryName = (Label)gvaddeditems.Rows[index].FindControl("lblCategoryName");

            if (e.CommandName == "QC")
            {
                Label lblMIQCStatus = (Label)gvaddeditems.Rows[index].FindControl("lblMIQCStatus");

                int MIDID = Convert.ToInt32(gvaddeditems.DataKeys[index].Values[1].ToString());

                ViewState["MIQCStatus"] = gvaddeditems.DataKeys[index].Values[2].ToString();

                dt = (DataTable)ViewState["MIQCDetails"];
                dt.DefaultView.RowFilter = "MIDID='" + MIDID + "'";

                lblMRNInwardDate.Text = dt.DefaultView.ToTable().Rows[0]["MRNInwardDate"].ToString();

                hdnMIDID.Value = dt.DefaultView.ToTable().Rows[0]["MIDID"].ToString();
                //hdnMRNQuantity.Value = dt.DefaultView.ToTable().Rows[0]["DCAvailableQuantity"].ToString();

                lblSupplierName.Text = dt.DefaultView.ToTable().Rows[0]["SupplierName"].ToString();
                lblInVoiceNoAndDate.Text = dt.DefaultView.ToTable().Rows[0]["InVoiceNoAndDate"].ToString();
                lblPoNoAndDate.Text = dt.DefaultView.ToTable().Rows[0]["SPONumber"].ToString();

                lblMeasurment.Text = dt.DefaultView.ToTable().Rows[0]["Measurment"].ToString();

                // lblAvailableToQCClearanceQty.Text = "Available to DC Quantity  " + dt.DefaultView.ToTable().Rows[0]["DCAvailableQuantity"].ToString();
                //txtQuantity.Text = dt.DefaultView.ToTable().Rows[0]["DCAvailableQuantity"].ToString();

                //if (Convert.ToDecimal(dt.DefaultView.ToTable().Rows[0]["ReWorkedQuantity"].ToString()) > 0)
                //{
                //    hdnReworkedQty.Value = dt.DefaultView.ToTable().Rows[0]["ReWorkedQuantity"].ToString();
                //    //lblReworkedQty.Text = "Reworked Quantity  " + dt.DefaultView.ToTable().Rows[0]["ReWorkedQuantity"].ToString();
                //    //rblReworkedQtyStatus.ClearSelection();
                //    //divReworkedQuantity.Visible = true;
                //    //txtReworkedQuantity.Text = "";
                //}
                //else
                //divReworkedQuantity.Visible = false;



                Label lblGradeName_p = (Label)gvaddeditems.Rows[index].FindControl("lblGradeName");
                Label lblMaterialThickness_p = (Label)gvaddeditems.Rows[index].FindControl("lblMaterialThickness");
                Label lblTypeName_p = (Label)gvaddeditems.Rows[index].FindControl("lblTypeName");

                lblMrnNumber_h.Text = "MRN No:" + lblMRNNumber.Text;

                lblMaterialGrade.Text = lblGradeName_p.Text;
                lblMaterialthickness.Text = lblMaterialThickness_p.Text;
                lblmaterialType.Text = lblTypeName_p.Text;

                if (ViewState["MIQCStatus"].ToString() == "Completed")
                    btnQCCompleted.Visible = false;
                else
                    btnQCCompleted.Visible = true;

                BindMaterialInwardQCClearanceQtyDetails();
                BindDCAvailQtyDetails();
                BindMaterialInwardQCClearanceHeader();

                BindRFPNoAndIndentremarks(dt.DefaultView.ToTable().Rows[0]["SPODID"].ToString());


                DataSet ds = new DataSet();
                objQt = new cQuality();
                ds = objQt.CheckTCAddedOrNot(MIDID);
                if (ds.Tables[0].Rows[0]["CertificatesSharedStatus_MID"].ToString() == "1")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddQCPopup();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Please Add TC and Share it..','');", true);
            }
            if (e.CommandName == "Certificates")
            {
                lblcertificateheader_h.Text = lblCategoryName.Text + " / " + lblMRNNumber.Text;

                BindCerficatesDetails();
                BindCertificateAddedDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
            }

            if (e.CommandName.ToString() == "TC")
            {
                DataSet ds = new DataSet();
                objQt = new cQuality();
                objQt.MIDID = Convert.ToInt32(gvaddeditems.DataKeys[index].Values[1].ToString());
                ds = objQt.UpdateCertificateMandatoryStatusByMIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "SuccessMessage('Sucess','Updated');", true);
                    BindAddedMaterials();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCertficateDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //RadioButtonList rblApprove = (RadioButtonList)e.Row.FindControl("rblApprove");
                //TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                //DropDownList ddlQCStatus = (DropDownList)e.Row.FindControl("ddlQCStatus");
                DropDownList ddlQCStatus = (DropDownList)e.Row.FindControl("ddlCertificateName");

                DataTable dt;

                dt = (DataTable)ViewState["Certificates"];

                ddlQCStatus.DataSource = dt;
                ddlQCStatus.DataTextField = "CertificateName";
                ddlQCStatus.DataValueField = "CFID";
                ddlQCStatus.DataBind();
                ddlQCStatus.Items.Insert(0, new ListItem("--Select--", "0"));

                // if (!string.IsNullOrEmpty(dr["Status"].ToString()))
                //  rblApprove.SelectedValue = dr["Status"].ToString();
                //  btnSaveAndShare.CssClass = "btn btn-cons btn-success";
                ////if (string.IsNullOrEmpty(dr["QCStatus"].ToString()))
                ////    ddlQCStatus.SelectedValue = "A";
                ////else
                ////    ddlQCStatus.SelectedValue = dr["QCStatus"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvcertificatesAddedDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "ViewAttach")
            {
                string FileName = gvcertificatesAddedDetails.DataKeys[index].Values[0].ToString();
                if (e.CommandName.ToString() == "ViewAttach")
                {
                    string BasehttpPath = StoresDocsHttpPath + "MaterialInwardQCCertificates" + "/";
                    ViewState["FileName"] = FileName;
                    // objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, FileName, "MaterialInwardQCCertificates", ifrm);
                    cCommon.DownLoad(FileName, StoresDocsSavePath + "MaterialInwardQCCertificates" + "/" + FileName);
                }
            }
            if (e.CommandName.ToString() == "DeleteCertificates")
            {
                string MIQCCID = gvcertificatesAddedDetails.DataKeys[index].Values[2].ToString();
                DeleteStockCertificatesByMIQCCID(MIQCCID);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvQCQtyDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

                if (ViewState["MIQCStatus"].ToString() == "Completed")
                {
                    btnDelete.Visible = false;
                    btnSaveMIQC.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                    btnSaveMIQC.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvcertificatesAddedDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDeleteCF");

                if (dr["CertificatesSharedStatus_MID"].ToString() == "0")
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

    protected void gvaddeditems_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnTCM = (LinkButton)e.Row.FindControl("");
                LinkButton btnAddCF = (LinkButton)e.Row.FindControl("btnAdd");

                //if (dr["CFStatus"].ToString() == "Added")
                //btnTCM.Visible = false;
                //else
                //{
                //btnTCM.Visible = true;

                //if (dr["CFMandatoryStatus"].ToString() == "1")
                //btnAddCF.Visible = true;
                //else
                //btnAddCF.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void DeleteStockCertificatesByMIQCCID(string MIQCCID)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.MIQCCID = Convert.ToInt32(MIQCCID);
            ds = objPc.DeleteMaterialInwardStockCertificateDetailsByMIQCCID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Certificates Deleted successfully');ShowAddPopUp();", true);
            BindCertificateAddedDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPNoAndIndentremarks(string SPODID)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.SPODID = Convert.ToInt32(SPODID);
            ds = objPc.GetRFPNoAndIndentRemarksBySPODID();

            lblRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblIndentRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMaterialInwardQCClearanceHeader()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
            ds = objQt.GetMIQCHeaderDetailsByMIDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQcClearanceHeader.DataSource = ds.Tables[0];
                gvQcClearanceHeader.DataBind();
            }
            else
            {
                gvQcClearanceHeader.DataSource = "";
                gvQcClearanceHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDCAvailQtyDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
            ds = objQt.GetMaterialInwardQCAvailQtyDetails();
            hdnMRNQuantity.Value = ds.Tables[0].Rows[0]["DCAvailableQuantity"].ToString();
            lblAvailableToQCClearanceQty.Text = "Available to DC Quantity  " + ds.Tables[0].Rows[0]["DCAvailableQuantity"].ToString();
            txtQuantity.Text = ds.Tables[0].Rows[0]["DCAvailableQuantity"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAddedMaterials()
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.mrnchangestatus = rblMRNChange.SelectedValue;
            objQt.LOcationID = Convert.ToInt32(ddlLocationName.SelectedValue);
            ds = objQt.GetInwardedMaterialDetailsByLocationID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MIQCDetails"] = ds.Tables[0];
                gvaddeditems.DataSource = ds.Tables[0];
                gvaddeditems.DataBind();
                divOutput.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                gvaddeditems.DataSource = "";
                gvaddeditems.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }

            if (objSession.type == 1 || objSession.type == 11)
            {
                divAdmin.Visible = true;
                gvaddeditems.Columns[1].Visible = true;
            }
            else
            {
                divUser.Visible = true;
                gvaddeditems.Columns[1].Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCerficatesDetails()
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            //objQt.SPODID = Convert.ToInt32(hdnSPODID.Value);
            //objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);

            //ds = objQt.GetCertficateDetailsBySPODID();

            ds = objQt.GetCertficateDetailsByMRNQC();

            ViewState["Certificates"] = ds.Tables[1];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCertficateDetails.DataSource = ds.Tables[0];
                gvCertficateDetails.DataBind();
            }
            else
            {
                gvCertficateDetails.DataSource = "";
                gvCertficateDetails.DataBind();
            }


        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCertificateAddedDetails()
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.SPODID = Convert.ToInt32(hdnSPODID.Value);
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);

            ds = objQt.GetCertficateDetailsBySPODID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvcertificatesAddedDetails.DataSource = ds.Tables[0];
                gvcertificatesAddedDetails.DataBind();
            }
            else
            {
                gvcertificatesAddedDetails.DataSource = "";
                gvcertificatesAddedDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMaterialInwardQCClearanceQtyDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
            ds = objQt.GetMaterialInwardQCClearanceQuantityDetailsByMIDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQCQtyDetails.DataSource = ds.Tables[0];
                gvQCQtyDetails.DataBind();
            }
            else
            {
                gvQCQtyDetails.DataSource = "";
                gvQCQtyDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindMRNPrintDetails(int index)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            hdnMIDID.Value = gvaddeditems.DataKeys[index].Values[1].ToString();
            objQt.MIDID = Convert.ToInt32(hdnMIDID.Value);
            ds = objQt.GetMRNDetailsByMRNNumber();

            lblOfficeAddress_p.Text = ds.Tables[2].Rows[0]["Address"].ToString();
            lblOfficePhoneAndFaxNo_p.Text = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            lblOfficeEmail_p.Text = ds.Tables[2].Rows[0]["Email"].ToString();
            lblOfficeWebsite_p.Text = ds.Tables[2].Rows[0]["WebSite"].ToString();

            lblWorkAddress_p.Text = ds.Tables[3].Rows[0]["Address"].ToString();
            lblWorkPhoneAndFaxNo_p.Text = ds.Tables[3].Rows[0]["PhoneAndFaxNo"].ToString();
            lblWorkEmail_p.Text = ds.Tables[3].Rows[0]["Email"].ToString();
            lblWorkWebsite_p.Text = ds.Tables[3].Rows[0]["WebSite"].ToString();

            lblMRNNo_p.Text = ds.Tables[0].Rows[0]["MRNNumber"].ToString();
            lblDate_p.Text = ds.Tables[1].Rows[0]["InspectedOn"].ToString();

            lblSupplierName_p.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();
            lblInVoiceNo_p.Text = ds.Tables[0].Rows[0]["InVoiceNoAndDate"].ToString();
            lblPONoDate_p.Text = ds.Tables[0].Rows[0]["SPONumber"].ToString();
            lblMTR_p.Text = ds.Tables[1].Rows[0]["MTR"].ToString();
            lblTPSNo_p.Text = ds.Tables[1].Rows[0]["TPSNo"].ToString();
            lblVisual_p.Text = ds.Tables[1].Rows[0]["Visual"].ToString();
            lblCheckTest_p.Text = ds.Tables[1].Rows[0]["CheckTest"].ToString();
            lblAddtionalTestRequirtment_p.Text = ds.Tables[1].Rows[0]["AddtionalRequirtment"].ToString();
            lblMeasuredDimension_p.Text = ds.Tables[1].Rows[0]["MeasuredDimension"].ToString();
            lblOriginalMarking_p.Text = ds.Tables[1].Rows[0]["OriginalMarking"].ToString();

            //lblInstrumentRefNo_p.Text = ds.Tables[1].Rows[0]["InstrumentRefNo"].ToString();
            //lblCalReportNo_p.Text = ds.Tables[1].Rows[0]["CalreportNo"].ToString();
            //lblDoneOn_p.Text = ds.Tables[1].Rows[0]["DoneOn"].ToString();
            //lblDueOn_p.Text = ds.Tables[1].Rows[0]["DueOn"].ToString();

            lblReceivedBy_p.Text = ds.Tables[0].Rows[0]["ReceivedBy"].ToString();
            lblReceivedDate_p.Text = ds.Tables[0].Rows[0]["ReceivedDate"].ToString();

            lblInspectedBy_p.Text = ds.Tables[1].Rows[0]["InspectedBy"].ToString();
            lblInspectedDate_p.Text = ds.Tables[1].Rows[0]["InspectedOn"].ToString();

            lblCertificateNo_p.Text = ds.Tables[1].Rows[0]["CertificateNo"].ToString();
            lblPMI_p.Text = ds.Tables[1].Rows[0]["PMI"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQCClearedDetails.DataSource = ds.Tables[0];
                gvQCClearedDetails.DataBind();
            }
            else
            {
                gvQCClearedDetails.DataSource = "";
                gvQCClearedDetails.DataBind();
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                gvInstrumentDetails_p.DataSource = ds.Tables[4];
                gvInstrumentDetails_p.DataBind();
            }
            else
            {
                gvInstrumentDetails_p.DataSource = "";
                gvInstrumentDetails_p.DataBind();
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                lblDocNo_p.Text = ds.Tables[5].Rows[0]["DocNo"].ToString();
                lblRevNo_p.Text = ds.Tables[5].Rows[0]["RevNo"].ToString();
                lblISODate_p.Text = ds.Tables[5].Rows[0]["ISODate"].ToString();
            }

            GeneratePDDF();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GeneratePDDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            StringBuilder sbCosting = new StringBuilder();

            // divMRNPrint.Attributes.Add("style", "display:block;");

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                imgQrcode.Attributes.Add("style", "display:block;");
                imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "InternalDCPrint";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                imgQrcode.Attributes.Add("style", "display:none;");

            divMRNPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "MRNReport_" + lblMRNNo_p.Text + ".html";
            string pdffile = "MRNReport_" + lblMRNNo_p.Text + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string pdfFileURL = LetterPath + pdffile;

            string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            // SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());

            //objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);

            //  divMRNPrint.Attributes.Add("style", "display:none;");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "MRNPrint('" + epstyleurl + "','" + Main + "','" + QrCode + "','" + style + "','" + Print + "','" + topstrip + "');", true);

            // objc.ReadhtmlFile(htmlfile, hdnPdfContent);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string Main, string epstyleurl, string style, string Print, string topstrip, string div)
    {
        try
        {
            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("<style type='text/css'/>  .Qrcode{ float: right; } label{ color: black ! important;font-weight: bold;} p{ margin:0;padding:0; }  @page { size: landscape; } span { padding: 0px 0px; } </style>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;margin:0 auto'>");
            w.WriteLine("<div>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
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
        if (gvaddeditems.Rows.Count > 0)
        {
            gvaddeditems.UseAccessibleHeader = true;
            gvaddeditems.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}