using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Text;

public partial class Pages_PurchaseIndent : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cSales objSales;
    cProduction objProd;
    string PurchaseIndentSavePath = ConfigurationManager.AppSettings["PurchaseIndentSavePath"].ToString();
    string PurchaseIndentHttpPath = ConfigurationManager.AppSettings["PurchaseIndentHttpPath"].ToString();

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

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                // objc = new cCommon();
                objProd = new cProduction();
                objMat = new cMaterials();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                // dsCustomer = objc.getRFPCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                //  dsRFPHID = objc.GetRFPDetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                //objc.GetRFPDetailsByUserIDAndRFPtatusCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo, "LS_GetRFPDetailsByUserIDAndRFPStatusCompleted");
                //dsCustomer = objProd.GetRFPCustomerNameByMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                //dsRFPHID = objProd.GetRFPDetailsByUserIDAndMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                // ViewState["RFPDetails"] = dsRFPHID.Tables[0];
                // objMat.getmaterial
                rblpurchase_SelectIndexChanged(null, null);
                BindPurchaseindentDetails();
                ShowHideControls("addnew,view");
            }
            if (target == "ViewIndentCopy")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvPurchaseIndentDetails.DataKeys[index].Values[1].ToString();
                ViewState["PID"] = gvPurchaseIndentDetails.DataKeys[index].Values[0].ToString();
                ViewDrawingFilename();
            }

            if (target == "deletegvrowMPMD")
            {
                objProd = new cProduction();
                DataSet ds = new DataSet();
                objProd.PID = Convert.ToInt32(arg.ToString());

                ds = objProd.DeletePurchaseIndentDetailsByPID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Indent Details Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                BindPurchaseindentDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"RadioButtonEvents"

    protected void rblpurchase_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        try
        {
            if (rblpurchase.SelectedValue == "G")
            {
                //objMat.GetMaterialNameDetails(ddlPartName);
                objMat.GetMaterialCategoryNameDetails(ddlMaterialCategory);
                objMat.GetCertificateNameDetails(liCertificates);
                objMat.getMaterialTypeName(ddlMaterialType);
                //objMat.GetMaterialGradeNameDetails(ddlmaterialGrdae);
                ShowHideControls("addnew,view");
                // ddlMaterialCategory.Enabled = true;
                // ddlmaterialGrade.Enabled = true;
                //btnPurchaseIndentStatus.Visible = false;
                objMat.GetMaterialThicknessDetailsformaterialPlanning(ddlThickness);
                //objMat.getPurchaseEmployeeDetails(ddlIndentTo);
            }
            else
            {
                ShowHideControls("addnew,view,add");
                // ddlMaterialCategory.Enabled = false;
                // ddlmaterialGrade.Enabled = false;
                //btnPurchaseIndentStatus.Visible = true;
            }
            objMat.GetMaterialClassificationNom(ddlUOM);
            BindPurchaseindentDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMaterialCategory_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblpurchase.SelectedValue == "G")
            {
                DataSet ds = new DataSet();
                objMat = new cMaterials();
                if (ddlMaterialCategory.SelectedIndex > 0)
                {
                    objMat.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);
                    objMat.GetMaterialGradeDetailsByCategoryID(ddlmaterialGrdae);
                }
                else
                {
                    objMat.CID = 0;
                    objMat.GetMaterialGradeDetailsByCategoryID(ddlmaterialGrdae);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMaterialType_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        //objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialType.SelectedIndex > 0)
            {
                BindmaterialTypeFields("add", Convert.ToInt32(hdnPID.Value));
            }
            else divMTFields.InnerHtml = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlThickness_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        //objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlThickness.SelectedIndex > 0)
            {
                objMat.MPID = Convert.ToInt32(ddlmaterialGrdae.SelectedValue.Split('/')[0].ToString());
                objMat.MGMID = Convert.ToInt32(ddlmaterialGrdae.SelectedValue.Split('/')[1].ToString());

                ds = objMat.GetPartname(ddlRFPNo.SelectedValue, ddlThickness.SelectedValue);
                lblpartname.Text = ds.Tables[0].Rows[0]["Partname"].ToString();
                txtrequiredWeight.Text = ds.Tables[0].Rows[0]["Requiredweight"].ToString();
                lblreqweight.Text = ds.Tables[0].Rows[0]["Requiredweight"].ToString();
                hdnReqWeight.Value = ds.Tables[0].Rows[0]["Requiredweight"].ToString();
                hdnrblType.Value = rblpurchase.SelectedValue;
                hdnmpids.Value = ds.Tables[0].Rows[0]["MPID"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselect('" + ds.Tables[1].Rows[0]["CFIDs"].ToString() + "');", true);
            }
            else
            {
                lblpartname.Text = "";
                hdnmpids.Value = "";
                lblreqweight.Text = "";
                txtrequiredWeight.Text = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlmaterialGrdae_OnSelectIndexChanged(object sender, EventArgs e)
    {
        //objc = new cCommon();
        try
        {
            if (ddlmaterialGrdae.SelectedIndex > 0)
            {
                if (rblpurchase.SelectedValue == "R")
                {
                    BindMaterialPlanningIndentDetailsByMPID();
                }
            }
            else
            {
                ddlThickness.SelectedIndex = 0;
                ddlMaterialCategory.SelectedIndex = 0;
            }
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
                ////objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRFPHIDAndMRNNumber");
                // objMat.getItemDetailsByRFPHIDAndMRNNumber(ddlItemName);
                ShowHideControls("add,addnew,view");
                BindPurchaseindentDetails();
            }
            else
                ShowHideControls("add,addnew");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

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

    //protected void ddlItemName_OnSelectIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objMat = new cMaterials();
    //    try
    //    {
    //        if (ddlItemName.SelectedIndex > 0)
    //        {
    //      ////      objMat.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());

    //      ////      ds = objMat.GetMaterialGradeNameDetailsByEDID(ddlmaterialGrade);



    //            ViewState["EnquiryID"] = ds.Tables[1].Rows[0]["EnquiryID"].ToString();

    //            if (ds.Tables[2].Rows.Count > 0)
    //                lblDrawingName.Text = ds.Tables[2].Rows[0]["FileName"].ToString();

    //            BindPurchaseindentDetails();
    //            ShowHideControls("addnew,add,view");
    //        }
    //        else
    //            ShowHideControls("add");
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    //protected void ddlPartName_OnSelectIndexChanged(object sender, EventArgs e)
    //{
    //    objMat = new cMaterials();
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        if (ddlPartName.SelectedIndex > 0)
    //        {
    //            if (rblpurchase.SelectedValue == "R")
    //            {
    //                ////     ddlMaterialCategory.Enabled = ddlmaterialGrade.Enabled = false;
    //                objMat.BOMID = Convert.ToInt32(ddlPartName.SelectedValue);
    //                ds = objMat.GetMGMIDAndCIDByBOMIDInMaterialplanning();
    //                ddlMaterialCategory.SelectedValue = ds.Tables[0].Rows[0]["CID"].ToString();
    //                ////   ddlmaterialGrade.SelectedValue = ds.Tables[0].Rows[0]["MGMIDProduction"].ToString();
    //                txtpartqty.Text = ds.Tables[0].Rows[0]["PartQtyProduction"].ToString();
    //                if (chckworkorderindent.Checked) ddlmrnno.Items.Insert(0, new ListItem(ds.Tables[0].Rows[0]["MRNNumber"].ToString(), ds.Tables[0].Rows[0]["MRNNumber"].ToString()));
    //            }
    //            //// else
    //            ////    ddlMaterialCategory.Enabled = ddlmaterialGrade.Enabled = true;
    //        }
    //        ////else
    //        ////  ddlMaterialCategory.Enabled = ddlmaterialGrade.Enabled = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"Button Events"

    private void ViewDrawingFilename()
    {
        try
        {
            objc = new cCommon();
            objc.ViewFileName(PurchaseIndentSavePath, PurchaseIndentHttpPath, ViewState["FileName"].ToString(), ViewState["PID"].ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ChckedChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        ////   objMat.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
        Checkboxcondition();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            //txtDate.Text = DateTime.Today.ToString("d/m/yyyy");
            // BindDropDownDetails();
            if (rblpurchase.SelectedValue == "R")
            {
                //ShowHideControls("add,view,input");
                //  ddlMaterialCategory.Enabled = ddlmaterialGrade.Enabled = false;
                partname.Visible = true;
                BindDropDownDetails();

            }
            else if (rblpurchase.SelectedValue == "G")
            {
                partname.Visible = false;
                //ShowHideControls("input,view");
                objMat.GetMaterialGrade(ddlmaterialGrdae, "0");
            }
            //objMat.GetMaterialClassificationNom(ddlUOM);
            objMat.GetMaterialClassificationNomByPurchaseindent(ddlUOM,"G");
            ShowHideControls("input");
            hdnPID.Value = "0";
            ClearValues();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "rblpurchase('" + rblpurchase.SelectedValue + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        objSales = new cSales();
        string MTFIDs = "";
        string MTFIDsValue = "";
        string certificates = "";
        string PurchaseCopy = "";
        string MaximumAttacheID;
        bool msg = true;
        try
        {
            if (ddlmaterialGrdae.Enabled == true && ddlThickness.Enabled == true)
                msg = objc.Validate(divInput);
            if (msg)
            {
                objMat.PID = Convert.ToInt32(hdnPID.Value);
                objMat.RFPHID = rblpurchase.SelectedValue == "G" ? 0 : Convert.ToInt32(ddlRFPNo.SelectedValue);
                //objMat.RFPDID = rblpurchase.SelectedValue == "G" ? 0 : Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());

                //objMat.Date = DateTime.ParseExact(DateTime.Today.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //objMat.BOMID = Convert.ToInt32(ddlPartName.SelectedValue);
                ////    objMat.MPID = 1;
                //Convert.ToInt32(ddlPartName.SelectedValue);
                objMat.IndentBy = Convert.ToInt32(objSession.employeeid);
                objMat.IndentTo = Convert.ToInt32(ddlIndentTo.SelectedValue);
                objMat.CID = Convert.ToInt32(ddlMaterialCategory.SelectedValue);

                if (rblpurchase.SelectedValue == "G")
                    objMat.MGMID = Convert.ToInt32(ddlmaterialGrdae.SelectedValue);
                else
                    objMat.MGMID = Convert.ToInt32(ddlmaterialGrdae.SelectedValue.Split('/')[1].ToString());

                objMat.THKId = Convert.ToInt32(ddlThickness.SelectedValue);
                objMat.CMID = Convert.ToInt32(ddlUOM.SelectedValue);
                objMat.MTID = Convert.ToInt32(ddlMaterialType.SelectedValue);
                objMat.RequiredWeight = Convert.ToDecimal(txtrequiredWeight.Text);
                objMat.DeliveryDate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objMat.Remarks = txtRemarks.Text;
                objMat.Type = rblpurchase.SelectedValue == "G" ? "General" : "RFP";

                objMat.MPIDS = rblpurchase.SelectedValue == "G" ? "" : hdnmpids.Value;

                objMat.MTFIDs = hdn_MTFIDS.Value;
                objMat.MTFIDsValue = hdn_MTFIDsValue.Value;

                objMat.DrawingName = txtDrawingname.Text;
                //foreach (Control c in divMTFields.Controls)
                //{
                //    if (c is TextBox)
                //    {
                //        TextBox txt = (TextBox)c;
                //        if (MTFIDs == "")
                //            MTFIDs = txt.ID.Split('_')[1].ToString();
                //        else
                //            MTFIDs = MTFIDs + ',' + txt.ID.Split('_')[1].ToString();

                //        if (MTFIDsValue == "")
                //            MTFIDsValue = txt.Text;
                //        else
                //            MTFIDsValue = MTFIDsValue + ',' + txt.Text;
                //    }
                //}

                //objMat.MTFIDs = MTFIDs;
                //objMat.MTFIDsValue = MTFIDsValue;

                if (fPUpload.HasFile)
                {
                    string FileName = Path.GetFileName(fPUpload.PostedFile.FileName);

                    string[] extension = FileName.Split('.');

                    MaximumAttacheID = objSales.GetMaximumAttachementID();

                    PurchaseCopy = MaximumAttacheID + FileName.Trim().Replace("/", "");
                    //   PoCopy = "PurcH" + '_' + ddlEnquiryNumber.SelectedValue + '.' + extension[1];
                    objMat.PurchaseCopy = PurchaseCopy;
                }
                else
                    objMat.PurchaseCopy = null;

                objMat.RequiredWeight = Convert.ToDecimal(txtrequiredWeight.Text);

                foreach (ListItem li in liCertificates.Items)
                {
                    if (li.Selected)
                    {
                        if (certificates == "")
                            certificates = li.Value;
                        else if (certificates != "")
                            certificates = certificates + ',' + li.Value;
                    }
                }

                objMat.certficates = certificates;

                if (chckworkorderindent.Checked)
                {
                    objMat.PIndentMRNnumber = ddlmrnno.SelectedValue;
                    objMat.Jobdescription = txtJobdescription.Text;
                }
                else
                {
                    objMat.PIndentMRNnumber = "0";
                    objMat.Jobdescription = "";
                }

                ds = objMat.SavePurchaseIndentDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Purchase Indent Saved Successfully');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','purchase Indent Updated Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                string path = PurchaseIndentSavePath + ds.Tables[0].Rows[0]["PID"].ToString() + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!string.IsNullOrEmpty(PurchaseCopy))
                    fPUpload.SaveAs(path + PurchaseCopy);

                BindPurchaseindentDetails();
                ShowHideControls("view,addnew");
                // ddlmaterialGrdae_OnSelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "dynamicvalueretain();", true);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            /////         ddlCustomerName.Enabled = ddlRFPNo.Enabled = ddlPartName.Enabled = ddlItemName.Enabled = true;
            hdnPID.Value = "0";
            if (rblpurchase.SelectedValue == "R")
            {
                ShowHideControls("add,addnew,view");
                ddlmaterialGrdae.Enabled = ddlThickness.Enabled = true;
            }
            else
                ShowHideControls("addnew,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            cCommon.DownLoad(ViewState["FileName"].ToString() + '/' + ViewState["EnquiryID"].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnPurchaseIndentStatus_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ////     objMat.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            ds = objMat.SavePurchaseIndentStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Neet To Add Some Part For This Item')", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','purchase Indent Status Updated Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnIndent_OnClick(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objMat.GetPurchaseIndentDetailsByRFPHIDForPDF();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblIndentBy_p.Text = ds.Tables[0].Rows[0]["EmpIndentBy"].ToString();
                lblRFPNo_p.Text = ddlRFPNo.SelectedItem.Text;
                lblIndentDate_p.Text = ds.Tables[0].Rows[0]["IndentDate"].ToString();
                lblNote_p.Text = ds.Tables[0].Rows[0]["EmpIndentBy"].ToString() + ":" + ds.Tables[0].Rows[0]["IndentDate"].ToString();

                gvPurchseIndentDetails_p.DataSource = ds.Tables[0];
                gvPurchseIndentDetails_p.DataBind();

                ViewState["Address"] = ds.Tables[1];

                GeneratePDDF();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Please Add Indent');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddRFP_Click(object sender, EventArgs e)
    {
        string RFPNoList = "";
        DataSet ds = new DataSet();
        objProd = new cProduction();
        try
        {
            foreach (ListItem li in lstRFPList.Items)
            {
                if (li.Selected)
                {
                    if (RFPNoList == "")
                        RFPNoList = li.Value;
                    else if (RFPNoList != "")
                        RFPNoList = RFPNoList + ',' + li.Value;
                }
            }

            objProd.RFPNoList = RFPNoList;
            objProd.PID = Convert.ToInt32(hdnPID.Value);
            ds = objProd.UpdateRFPNOListByPID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RFP No Added Successfully');", true);
                BindPurchaseindentDetails();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPurchaseIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        objMat = new cMaterials();
        string CFID;

        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PID = gvPurchaseIndentDetails.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["PurchaseIndentDetails"];
            hdnPID.Value = PID.ToString();
            dt.DefaultView.RowFilter = "PID='" + PID + "'";

            if (e.CommandName == "EditPI")
            {
                DataSet ds = new DataSet();
                objMat.PID = Convert.ToInt32(hdnPID.Value);
                ds = objMat.CheckPOProcessignIndentByPID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    if (rblpurchase.SelectedValue == "R")
                    {
                        //BindDropDownDetails();
                        objMat.GetMaterialGrade(ddlmaterialGrdae, ddlRFPNo.SelectedValue);
                        ddlmaterialGrdae.Items.Insert(1, new ListItem(dt.DefaultView.ToTable().Rows[0]["GradeName"].ToString(), dt.DefaultView.ToTable().Rows[0]["MGMID"].ToString()));
                    }

                    if (rblpurchase.SelectedValue == "G")
                    {
                        objMat.GetMaterialCategoryNameDetails(ddlMaterialCategory);
                        ddlMaterialCategory.SelectedValue = dt.DefaultView.ToTable().Rows[0]["CID"].ToString();
                        ddlMaterialCategory_OnSelectIndexChanged(null, null);
                    }

                    ddlmaterialGrdae.SelectedValue = dt.DefaultView.ToTable().Rows[0]["MGMID"].ToString();

                    if (rblpurchase.SelectedValue == "R")
                    {
                        BindMaterialPlanningIndentDetailsByMPID();
                        ddlThickness.Items.Insert(1, new ListItem(dt.DefaultView.ToTable().Rows[0]["THKValue"].ToString(), dt.DefaultView.ToTable().Rows[0]["THKID"].ToString()));
                        ddlMaterialCategory.Items.Insert(1, new ListItem(dt.DefaultView.ToTable().Rows[0]["CategoryName"].ToString(), dt.DefaultView.ToTable().Rows[0]["CID"].ToString()));
                    }

                    objMat.GetMaterialClassificationNomByPurchaseindent(ddlUOM,"G");
                    objMat.getMaterialTypeName(ddlMaterialType);
                    objMat.GetCertificateNameDetails(liCertificates);

                    if (rblpurchase.SelectedValue == "R")
                        ddlmaterialGrdae.Enabled = false;

                    txtrequiredWeight.Text = dt.DefaultView.ToTable().Rows[0]["RequiredWeight"].ToString();
                    lblpartname.Text = dt.DefaultView.ToTable().Rows[0]["PartName"].ToString();

                    ddlThickness.SelectedValue = dt.DefaultView.ToTable().Rows[0]["THKID"].ToString();
                    hdnmpids.Value = dt.DefaultView.ToTable().Rows[0]["MPID"].ToString();
                    ddlIndentTo.SelectedValue = dt.DefaultView.ToTable().Rows[0]["indentTo"].ToString();
                    ddlMaterialCategory.SelectedValue = dt.DefaultView.ToTable().Rows[0]["CID"].ToString();

                    ddlUOM.SelectedValue = dt.DefaultView.ToTable().Rows[0]["CMID"].ToString();
                    ddlMaterialType.SelectedValue = dt.DefaultView.ToTable().Rows[0]["MaterialTypeID"].ToString();

                    txtDeliveryDate.Text = dt.DefaultView.ToTable().Rows[0]["EditDeliveryDate"].ToString();

                    txtRemarks.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
                    txtDrawingname.Text = dt.DefaultView.ToTable().Rows[0]["DrawingName"].ToString();

                    CFID = dt.DefaultView.ToTable().Rows[0]["CFID"].ToString();

                    if (dt.DefaultView.ToTable().Rows[0]["MRNNumber"].ToString() != "0")
                        txtJobdescription.Text = dt.DefaultView.ToTable().Rows[0]["JobDescription"].ToString();

                    Checkboxcondition();

                    if (dt.DefaultView.ToTable().Rows[0]["MaterialTypeID"].ToString() == "0")
                        divMTFields.InnerHtml = "";
                    else
                        BindmaterialTypeFields("Edit", Convert.ToInt32(hdnPID.Value));

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselect('" + CFID + "');", true);

                    ShowHideControls("input");
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }

            if (e.CommandName == "ViewPI")
            {
                objc = new cCommon();
                string FileName = "";
                FileName = gvPurchaseIndentDetails.DataKeys[index].Values[1].ToString();
                ViewState["FileName"] = FileName;
                objc.ViewFileName(PurchaseIndentSavePath, PurchaseIndentHttpPath, FileName, hdnPID.Value, ifrm);
            }
            if (e.CommandName == "AddRFP")
            {
                DataSet ds = new DataSet();
                objMat.PID = Convert.ToInt32(hdnPID.Value);
                ds = objMat.GetRFPNoList(lstRFPList);

                lblRFPList.Text = ds.Tables[1].Rows[0]["RFPNOList"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowRFPPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            ShowHideControls("input");
            Log.Message(ex.ToString());
        }
    }

    protected void gvPurchaseIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    if (rblpurchase.SelectedValue.ToString() == "G")
            //        e.Row.Cells[1].Attributes.Add("style", "display:none;");
            //    else
            //        e.Row.Cells[1].Attributes.Add("style", "display:block;");
            //}
            //else if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (rblpurchase.SelectedValue.ToString() == "G")
            //        e.Row.Cells[1].Attributes.Add("style", "display:none;");
            //    else
            //        e.Row.Cells[1].Attributes.Add("style", "display:block;");
            //}
			
			DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnedit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (objSession.type == 1)
                {
                    btnedit.Visible = true;
                    btnDelete.Visible = true;
                }



            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"Common Methods"

    private void BindMaterialPlanningIndentDetailsByMPID()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            ds = objMat.GetMaterialThickness(ddlRFPNo.SelectedValue, ddlmaterialGrdae.SelectedValue.Split('/')[0].ToString());

            ddlThickness.DataSource = ds.Tables[0];
            ddlThickness.DataTextField = "THKValue";
            ddlThickness.DataValueField = "THKId";
            ddlThickness.DataBind();
            ddlThickness.Items.Insert(0, new ListItem("--Select--", "0"));
            if (hdnPID.Value == "0")
                ddlThickness.SelectedValue = ds.Tables[0].Rows[0]["THKId"].ToString();

            ddlMaterialCategory.DataSource = ds.Tables[0];
            ddlMaterialCategory.DataTextField = "CategoryName";
            ddlMaterialCategory.DataValueField = "CID";
            ddlMaterialCategory.DataBind();
            ddlMaterialCategory.Items.Insert(0, new ListItem("--Select--", "0"));

            if (hdnPID.Value == "0")
                ddlMaterialCategory.SelectedValue = ds.Tables[0].Rows[0]["CID"].ToString();

            lblpartname.Text = ds.Tables[1].Rows[0]["Partname"].ToString();
            txtrequiredWeight.Text = ds.Tables[2].Rows[0]["Requiredweight"].ToString();
            lblreqweight.Text = ds.Tables[2].Rows[0]["Requiredweight"].ToString();
            hdnReqWeight.Value = ds.Tables[2].Rows[0]["Requiredweight"].ToString();
            hdnrblType.Value = rblpurchase.SelectedValue;
            hdnmpids.Value = ds.Tables[1].Rows[0]["MPID"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselect('" + ds.Tables[3].Rows[0]["CFIDs"].ToString() + "');", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindmaterialTypeFields(string Mode, int PID)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {

            ds = objMat.GetMaterialFields(ddlMaterialType.SelectedValue, Mode, PID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string dynamiccntrls = "<div class=\"col-sm-12 p-t-10\"><div class=\"col-sm-2\"></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label1replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt1replace\" type=\"text\" onkeypress=\"return validationDecimal(this)\" id=\"ContentPlaceHolder1_txt1replace\" Value='txtval1' class=\"form-control\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-4 text-left\"><div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" type=\"text\" onkeypress=\"return validationDecimal(this)\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div></div><div class=\"col-sm-2\"></div></div>";
                StringBuilder sbDivMTFields = new StringBuilder();
                int trowcnt = ds.Tables[0].Rows.Count;
                string partialdom = dynamiccntrls;
                string totoaldom = "";
                for (int trow = 0; trow < trowcnt; trow++)
                {
                    if (trow % 2 == 0)
                    {
                        partialdom = partialdom.Replace("label1replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt1replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval1", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval1", "");
                        if (trow == trowcnt - 1)
                        {
                            partialdom = partialdom.Replace("<div class=\"text-left\"><label class=\"form-label\">label2replace</label></div><div><input name=\"ctl00$ContentPlaceHolder1$txt2replace\" type=\"text\" onkeypress=\"return validationDecimal(this)\" id=\"ContentPlaceHolder1_txt2replace\" Value='txtval2' class=\"form-control mandatoryfield\" autocomplete=\"off\"></input></div>", "");
                            sbDivMTFields.Append(partialdom);
                        }
                    }
                    else
                    {
                        partialdom = partialdom.Replace("label2replace", ds.Tables[0].Rows[trow]["Name"].ToString());
                        partialdom = partialdom.Replace("txt2replace", "txt_" + ds.Tables[0].Rows[trow]["MTFID"].ToString());

                        if (Mode == "Edit")
                            partialdom = partialdom.Replace("txtval2", ds.Tables[0].Rows[trow]["MTFIDValues"].ToString());
                        else
                            partialdom = partialdom.Replace("txtval2", "");
                        sbDivMTFields.Append(partialdom);
                        partialdom = dynamiccntrls;
                    }
                }

                divMTFields.InnerHtml = sbDivMTFields.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDropDownDetails()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            ////  objMat.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objMat.EmpID = Convert.ToInt32(objSession.employeeid);
            ds = objMat.getPurchaseEmployeeDetails(ddlIndentTo);
            lblIndentby.Text = ds.Tables[1].Rows[0]["EmployeeName"].ToString();
            objMat.GetMaterialGrade(ddlmaterialGrdae, ddlRFPNo.SelectedValue);
            objMat.GetMaterialCategoryNameDetails(ddlMaterialCategory);
            objMat.GetCertificateNameDetails(liCertificates);
            //objMat.GetMaterialClassificationNom(ddlUOM);
            objMat.getMaterialTypeName(ddlMaterialType);
            //objMat.GetPartDetailsByRFPDID(ddlPartName, 0);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPurchaseindentDetails()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.RFPHID = rblpurchase.SelectedValue == "G" ? 0 : Convert.ToInt32(ddlRFPNo.SelectedValue);
            ////       objMat.RFPDID = rblpurchase.SelectedValue == "G" ? 0 : Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objMat.Type = rblpurchase.SelectedValue == "G" ? "General" : "RFP";
            objMat.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objMat.GetPurchaseIndentDetailsByRFHID();
            ViewState["PurchaseIndentDetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPurchaseIndentDetails.DataSource = ds.Tables[0];
                gvPurchaseIndentDetails.DataBind();
                //btnPurchaseIndentStatus.Visible = true;
            }
            else
            {
                gvPurchaseIndentDetails.DataSource = "";
                gvPurchaseIndentDetails.DataBind();
                //btnPurchaseIndentStatus.Visible = false;
            }
            if (objSession.type == 1)
                gvPurchaseIndentDetails.Columns[14].Visible = true;
            else
                gvPurchaseIndentDetails.Columns[14].Visible = false;

            if (rblpurchase.SelectedValue == "G")
                gvPurchaseIndentDetails.Columns[gvPurchaseIndentDetails.Columns.Count - 1].Visible = true;
            else
                gvPurchaseIndentDetails.Columns[gvPurchaseIndentDetails.Columns.Count - 1].Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
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
                    case "addnew":
                        divAddNew.Visible = true;
                        break;
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        divradio.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        divradio.Visible = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void Checkboxcondition()
    {
        try
        {
            //if (chckworkorderindent.Checked)
            //{
            //    objMat.GetPartDetailsByRFPDID(ddlPartName, 1);
            //    jobdescription.Visible = true;
            //    mrnno.Visible = true;
            //    matrltype.Visible = false;
            //}
            //else
            //{
            //    objMat.GetPartDetailsByRFPDID(ddlPartName, 0);
            //    jobdescription.Visible = false;
            //    mrnno.Visible = false;
            //    matrltype.Visible = true;
            //}
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
            divPurchaseIndentPrint_p.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "PurchaseIndent_" + ddlRFPNo.SelectedValue + ".html";
            string pdffile = "PurchaseIndent_" + ddlRFPNo.SelectedValue + ".pdf";
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

            SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());

            objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
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
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            StreamWriter w;
            w = File.CreateText(URL);
            //w.WriteLine("<html><head><title>");       
            w.WriteLine("<html><head><title>");
            w.WriteLine("Offer");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
            w.WriteLine("<div class='print-page'>");
            w.WriteLine("<table><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            w.WriteLine("<div class='header' style='border-bottom:1px solid;'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-8 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR INDUSTRIES</h3>");
            w.WriteLine("<div style='font-weight:500;color:#000;width: 100%;font-size:12px'>" + Address + "</div>");
            w.WriteLine("<div style='font-weight:500;color:#000;font-size:12px'>" + PhoneAndFaxNo + "</div>");
            w.WriteLine("<div style='font-weight:500;color:#000;font-size:12px'>" + Email + "</div>");
            w.WriteLine("<div style='font-weight:500;color:#000;font-size:12px'>" + WebSite + "</div>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-2'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");
            w.WriteLine("<div class='col-sm-12 padding:0' style='padding-top:0px;'>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot><tr><td>");
            w.WriteLine("</td></tr></tfoot></table>");
            w.WriteLine("</div>");
            w.WriteLine("</html>");

            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void ClearValues()
    {
        ddlmaterialGrdae.Enabled = true;
        ddlThickness.SelectedIndex = 0;
        ddlMaterialCategory.SelectedIndex = 0;
        lblpartname.Text = "";
        hdnmpids.Value = "";
        divMTFields.InnerHtml = "";
        ddlMaterialType.SelectedIndex = 0;
        txtrequiredWeight.Text = "";
        lblreqweight.Text = "";
        txtRemarks.Text = "";
        txtDrawingname.Text = "";
        txtDeliveryDate.Text = "";
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvPurchaseIndentDetails.Rows.Count > 0)
            gvPurchaseIndentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}