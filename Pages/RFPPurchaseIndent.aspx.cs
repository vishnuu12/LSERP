using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPPurchaseIndent : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cProduction objProd;
    cMaterials objMat;
    cCommon objc;
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

            if (!IsPostBack)
            {
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                objProd = new cProduction();
                dsCustomer = objProd.GetRFPCustomerNameByMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsRFPHID = objProd.GetRFPDetailsByUserIDAndMPStatusCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                ShowHideControls("add");
                ViewState["RFPDetails"] = dsRFPHID.Tables[0];
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
                ShowHideControls("add,view,addnew");
            }

            if (target == "ViewIndentCopy")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvRFPIndentDetails.DataKeys[index].Values[1].ToString();
                ViewState["PID"] = gvRFPIndentDetails.DataKeys[index].Values[0].ToString();
                ViewDrawingFilename();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"  

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
                ShowHideControls("add,addnew,view");
                BindPurchaseindentDetails();
            }
            else
                ShowHideControls("add");
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

    protected void ddlMaterialType_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        //objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialType.SelectedIndex > 0)
                BindmaterialTypeFields("add", Convert.ToInt32(hdnPID.Value));

            else divMTFields.InnerHtml = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion    

    #region"Check Box Events"

    protected void chkitems_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            txtReqQty.Text = "0";
            DataSet ds = new DataSet();
            objProd = new cProduction();
            string MPID = "";

            CheckBox ch = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)ch.Parent.Parent;

            int Count = 0;

            string MGMID = "0";
            string THKID = "0";

            bool msg = true;

            foreach (GridViewRow row in gvMaterialplanningIndentDetails.Rows)
            {
                // TextBox txtReqqty = (TextBox)gvMaterialplanningIndentDetails.Rows[row.RowIndex].FindControl("txtReqWeight");
                decimal BlockedWeight = Convert.ToDecimal(gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[3].ToString());

                CheckBox chk = (CheckBox)gvMaterialplanningIndentDetails.Rows[row.RowIndex].FindControl("chkitems");

                string MGMIDNew = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[1].ToString();
                string THKIDNew = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[2].ToString();

                if (chk.Checked)
                {
                    if (Count == 0)
                    {
                        MGMID = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[1].ToString();
                        THKID = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[2].ToString();
                        msg = true;
                    }
                    else if (MGMID == MGMIDNew && THKID == THKIDNew)
                        msg = true;
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "ErrorMessage('ERROR','CLUBBING IS WRONG')", true);
                        msg = false;
                        chk.Checked = false;
                    }

                    if (msg)
                    {
                        txtReqQty.Text = Convert.ToString(Convert.ToDecimal(txtReqQty.Text) + (Convert.ToDecimal(BlockedWeight)));
                    }
                    else
                        break;

                    if (MPID == "")
                        MPID = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        MPID = MPID + ',' + gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[0].ToString();

                    Count++;
                }
            }
            if (msg)
            {
                ds = objProd.GetQCCertificatesByMPIDs(MPID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselect('" + ds.Tables[0].Rows[0]["CFIDs"].ToString() + "');", true);
            }

            if (ddlMaterialType.SelectedIndex == 0)
            {
                string PID = gvMaterialplanningIndentDetails.DataKeys[gr.RowIndex].Values[0].ToString();
                //  BindPurchaseIndentMaterialTypeFields(PID, "Edit");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnItemAddNew_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            ShowHideControls("input");
            hdnPID.Value = "0";

            objMat.getMaterialTypeName(ddlMaterialType);
            objMat.GetCertificateNameDetails(liCertificates);
            objMat.GetMaterialClassificationNomByPurchaseindent(ddlUOM, "RFP");

            bindMaterialplanningIndentdetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancelRFPtem_Click(object sender, EventArgs e)
    {
        hdnPID.Value = "0";
        ShowHideControls("view,add,addnew");
    }

    protected void btnSaveRFPIndent_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataSet ds = new DataSet();
        //objMat = new cMaterials();
        objProd = new cProduction();
        string MTFIDs = "";
        string MTFIDsValue = "";
        string certificates = "";
        string PurchaseCopy = "";
        string MaximumAttacheID;
        bool msg = true;
        string MPIDsAndReqQty = "";
        try
        {
            msg = objc.Validate(divInput);

            if (msg)
            {
                objProd.PID = Convert.ToInt32(hdnPID.Value);
                objProd.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

                objProd.IndentBy = Convert.ToInt32(objSession.employeeid);

                objProd.CMID = Convert.ToInt32(ddlUOM.SelectedValue);
                objProd.MTID = Convert.ToInt32(ddlMaterialType.SelectedValue);
                objProd.DeliveryDate = DateTime.ParseExact(txtDeliveryDate_I.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objProd.Remarks = txtRemarks.Text;
                objProd.Type = "RFP";

                // objMat.MPIDS = hdnmpids.Value;
                // objMat.RequiredWeight = Convert.ToDecimal(txtrequiredWeight.Text);

                foreach (GridViewRow row in gvMaterialplanningIndentDetails.Rows)
                {
                    CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                    string BlockedWeight = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[3].ToString();

                    if (chkitems.Checked)
                    {
                        if (MPIDsAndReqQty == "")
                            MPIDsAndReqQty = gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[0].ToString() + '-' + BlockedWeight;
                        else
                            MPIDsAndReqQty = MPIDsAndReqQty + "," + gvMaterialplanningIndentDetails.DataKeys[row.RowIndex].Values[0].ToString() + '-' + BlockedWeight;
                    }
                }

                objProd.MTFIDs = hdn_MTFIDS.Value;
                objProd.MTFIDsValue = hdn_MTFIDsValue.Value;

                objProd.DrawingName = txtDrawingname.Text;

                if (fPUpload.HasFile)
                {
                    cSales objSales = new cSales();
                    string FileName = Path.GetFileName(fPUpload.PostedFile.FileName);

                    string[] extension = FileName.Split('.');

                    MaximumAttacheID = objSales.GetMaximumAttachementID();

                    PurchaseCopy = MaximumAttacheID + FileName.Trim().Replace("/", "");
                    //   PoCopy = "PurcH" + '_' + ddlEnquiryNumber.SelectedValue + '.' + extension[1];
                    objProd.PurchaseCopy = PurchaseCopy;
                }
                else
                    objProd.PurchaseCopy = null;

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

                objProd.certficates = certificates;
                objProd.UOM = ddlUOM.SelectedValue;
                objProd.MPIDsAndReqQty = MPIDsAndReqQty;
                objProd.ReqQty = Convert.ToDecimal(txtReqQty.Text);

                ds = objProd.SaveRFPPurchaseIndentDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Purchase Indent Saved Successfully');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','purchase Indent Updated Successfully');", true);
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                    ShowHideControls("input");
                }

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added" || ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    BindPurchaseindentDetails();
                    string path = PurchaseIndentSavePath + ds.Tables[0].Rows[0]["PID"].ToString() + "\\";

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    if (!string.IsNullOrEmpty(PurchaseCopy))
                        fPUpload.SaveAs(path + PurchaseCopy);

                    ShowHideControls("view,addnew,add");
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "dynamicvalueretain();", true);
    }

    #endregion

    #region"Gridview Events"

    protected void gvMaterialplanningIndentDetails_OnRowDatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkitems = (CheckBox)e.Row.FindControl("chkitems");
                if (dr["PID"].ToString() == "0")
                    chkitems.Checked = false;
                else
                    chkitems.Checked = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvRFPIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objProd = new cProduction();
        string CFID;

        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PID = gvRFPIndentDetails.DataKeys[index].Values[0].ToString();

            hdnPID.Value = PID.ToString();
            if (e.CommandName == "EditPI")
            {
                objProd.PID = Convert.ToInt32(hdnPID.Value);
                objProd.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objProd.GetRFPIndentDetailsByPID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "")
                {
                    objMat = new cMaterials();

                    gvMaterialplanningIndentDetails.DataSource = ds.Tables[0];
                    gvMaterialplanningIndentDetails.DataBind();

                    objMat.GetMaterialClassificationNomByPurchaseindent(ddlUOM,"RFP");
                    objMat.getMaterialTypeName(ddlMaterialType);
                    objMat.GetCertificateNameDetails(liCertificates);

                    txtReqQty.Text = ds.Tables[1].Rows[0]["RequiredWeight"].ToString();

                    ddlUOM.SelectedValue = ds.Tables[1].Rows[0]["CMID"].ToString();
                    ddlMaterialType.SelectedValue = ds.Tables[1].Rows[0]["MaterialTypeID"].ToString();

                    txtDeliveryDate_I.Text = ds.Tables[1].Rows[0]["EditDeliveryDate"].ToString();

                    txtRemarks.Text = ds.Tables[1].Rows[0]["Remarks"].ToString();
                    txtDrawingname.Text = ds.Tables[1].Rows[0]["DrawingName"].ToString();

                    CFID = ds.Tables[1].Rows[0]["CFID"].ToString();

                    if (ds.Tables[1].Rows[0]["MaterialTypeID"].ToString() == "0")
                        divMTFields.InnerHtml = "";
                    else
                        BindmaterialTypeFields("Edit", Convert.ToInt32(hdnPID.Value));

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "multiselect('" + CFID + "');", true);

                    ShowHideControls("input");
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvRFPIndentDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
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

    private void BindPurchaseindentDetails()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objMat.Type = "RFP";
            objMat.UserID = Convert.ToInt32(objSession.employeeid);

            ds = objMat.GetPurchaseIndentDetailsByRFHID();
            ViewState["PurchaseIndentDetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPIndentDetails.DataSource = ds.Tables[0];
                gvRFPIndentDetails.DataBind();
                //btnPurchaseIndentStatus.Visible = true;
            }
            else
            {
                gvRFPIndentDetails.DataSource = "";
                gvRFPIndentDetails.DataBind();
                //btnPurchaseIndentStatus.Visible = false;
            }
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

    private void bindMaterialplanningIndentdetails()
    {
        DataSet ds = new DataSet();
        objProd = new cProduction();
        try
        {
            objProd.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objProd.GetMaterialplanningIndentDetailsByRFPHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialplanningIndentDetails.DataSource = ds.Tables[0];
                gvMaterialplanningIndentDetails.DataBind();
                //btnPurchaseIndentStatus.Visible = true;
            }
            else
            {
                gvMaterialplanningIndentDetails.DataSource = "";
                gvMaterialplanningIndentDetails.DataBind();
                //btnPurchaseIndentStatus.Visible = false;
            }
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
        if (gvRFPIndentDetails.Rows.Count > 0)
            gvRFPIndentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}