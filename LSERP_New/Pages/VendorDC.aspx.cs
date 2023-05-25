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
using System.Text;
using System.Configuration;

public partial class Pages_VendorDC : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cStores objSt;

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
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            ddlWoNoAndVendorNameLoad();
            ShowHideControls("add");
        }

        if (target == "ShareDC")
            ShareDC();
        if (target == "PrintVendorDC")
            PrintVendorDC(Convert.ToInt32(arg.ToString()));
        if (target == "PrintPO")
            PrintWorkOrderPODetails();
        if (target == "ViewIndentAttach")
        {
            objc = new cCommon();
            try
            {
                string FileName = gvWorkOrderIndent.DataKeys[Convert.ToInt32(arg.ToString())].Values[1].ToString();
                string WONo = gvWorkOrderIndent.DataKeys[Convert.ToInt32(arg.ToString())].Values[2].ToString();
                objc.ViewFileName(Session["WorkOrderIndentSavePath"].ToString(), Session["WorkOrderIndentHttpPath"].ToString(), FileName, WONo, ifrm);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblWPONoChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlWoNoAndVendorNameLoad();
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlWPONo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            if (ddlWPONo.SelectedIndex > 0)
            {
                objPc.WPOID = Convert.ToInt32(ddlWPONo.SelectedValue);
                ds = objPc.GetVendorNameByWPOID();
                ddlVendorName.SelectedValue = ds.Tables[0].Rows[0]["SCVMID"].ToString();

                BindVendorDCDetails();
                ShowHideControls("addnew,add,view");
            }
            else
            {
                ddlVendorName.SelectedIndex = 0;
                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlVendorName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        DataTable dt;
        try
        {
            if (ddlVendorName.SelectedIndex > 0)
            {
                dt = (DataTable)ViewState["WPONo"];
                if (ddlVendorName.SelectedIndex > 0)
                {
                    string SCVMID = ddlVendorName.SelectedValue;
                    dt.DefaultView.RowFilter = "SCVMID='" + SCVMID + "'";
                    dt.DefaultView.ToTable();
                }

                ddlWPONo.DataSource = dt;
                ddlWPONo.DataTextField = "WoNumber";
                ddlWPONo.DataValueField = "WPOID";
                ddlWPONo.DataBind();
                ddlWPONo.Items.Insert(0, new ListItem("--Select--", "0"));

                //ds = objPc.GetVendorNameByWPONumber(ddlWPONo);
            }
            else
                ddlWPONo.SelectedIndex = 0;
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void ddlSupplierChainVendor_OnSelectIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlSupplierChainVendor.SelectedIndex > 0)
    //        {
    //            BindWorkOrderPODetails();
    //            ShowHidePopUpControls("add,input,view");
    //        }
    //        else
    //            ShowHidePopUpControls("add,view");
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion"

    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            objPc = new cPurchase();
            objc = new cCommon();
            // ds = objPc.GetWPONumber(ddlWPONo, ddlSuplierName);
            objc.GetLocationDetails(ddlLocationName);
            //ViewState["WPODetails"] = ds.Tables[0];
            //ViewState["SupplierDetails"] = ds.Tables[1];

            ShowHideControls("input");
            hdnVDCID.Value = "0";
            clearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnVDCID.Value = "0";
        ShowHideControls("add,view,addnew");
        clearValues();
    }

    protected void btnSaveDC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.VDCID = Convert.ToInt32(hdnVDCID.Value);
            //  objPc.WPOID = Convert.ToInt32(ddlWPONo.SelectedValue);
            objPc.DCDate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objPc.FormJJNo = txtFormJJNNo.Text;
            objPc.TariffClassification = Convert.ToInt32(txtTarrifClassification.Text);
            objPc.Duration = Convert.ToInt32(txtDuration.Text);
            objPc.DutyDetailsDate = DateTime.ParseExact(txtDutyDetailsDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objPc.LocationID = Convert.ToInt32(ddlLocationName.SelectedValue);
            objPc.SCVMID = Convert.ToInt32(ddlVendorName.SelectedValue);
            objPc.WPOID = Convert.ToInt32(ddlWPONo.SelectedValue);

            ds = objPc.SaveVendorDCDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Vendor DC Saved Successfully');", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Vendor DC Updated Successfully');", true);
            BindVendorDCDetails();
            ShowHideControls("add,view");
            clearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveDCItem_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            foreach (GridViewRow row in gvWorkOrderIndent.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                TextBox txtDCQty = (TextBox)row.FindControl("txtDCQty");

                // TextBox txtRawMaterialQty = (TextBox)row.FindControl("txtRawMaterialQty");

                if (chk.Checked && Convert.ToInt32(txtDCQty.Text) > 0)
                {
                    TextBox txtItemDescription = (TextBox)row.FindControl("txtDescription");
                    // TextBox txtValue = (TextBox)row.FindControl("txtValue");

                    objPc.VDCID = Convert.ToInt32(hdnVDCID.Value);
                    objPc.WOIHID = Convert.ToInt32(gvWorkOrderIndent.DataKeys[row.RowIndex].Values[0].ToString());
                    objPc.WPOID = Convert.ToInt32(ddlWPONo.SelectedValue);
                    objPc.IssuedQty = Convert.ToInt32(txtDCQty.Text);
                    objPc.ItemDescription = txtItemDescription.Text;
                    objPc.Value = Convert.ToInt32(txtValue.Text);
                    objPc.txtRawMaterialQty = Convert.ToInt32(txtRawMaterialQty.Text);
                    ds = objPc.SaveVendorDCItemDetails();
                }
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Vendor DC Item Saved Successfully');", true);
                ShowHidePopUpControls("add,input,view");
                BindVendorDCItemDetails();
                BindWorkOrderIndentDetailsByWPOID();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridVioew Events"

    protected void gvVendorDC_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string VDCID = gvVendorDC.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["VendorDC"];
            hdnVDCID.Value = VDCID.ToString();
            dt.DefaultView.RowFilter = "VDCID='" + VDCID + "'";
            ViewState["DCSharedStatus"] = dt.DefaultView.ToTable().Rows[0]["DCSharedStatus"].ToString();

            if (e.CommandName == "EditVendorDC")
            {
                // ddlSuplierName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["SUPID"].ToString();

                objc = new cCommon();

                txtDCDate.Text = dt.DefaultView.ToTable().Rows[0]["DCDateEdit"].ToString();
                txtFormJJNNo.Text = dt.DefaultView.ToTable().Rows[0]["FormJJNo"].ToString();
                txtTarrifClassification.Text = dt.DefaultView.ToTable().Rows[0]["TariffClassification"].ToString();
                txtDuration.Text = dt.DefaultView.ToTable().Rows[0]["Duration"].ToString();
                txtDutyDetailsDate.Text = dt.DefaultView.ToTable().Rows[0]["DutyDetailsDateEdit"].ToString();

                objc.GetLocationDetails(ddlLocationName);

                ddlLocationName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["LocationID"].ToString();

                ShowHideControls("input");
            }

            if (e.CommandName == "AddDC")
            {
                objPc = new cPurchase();
                DataSet ds = new DataSet();

                objPc.PoType = "WPO";
                //objPc.GetSupplierChainVendorNameDetails(ddlSupplierChainVendor);
                ShowHidePopUpControls("input,view");

                if (ViewState["DCSharedStatus"].ToString() == "0")
                    btnSaveDCItem.Visible = true;

                BindVendorDCItemDetails();
                BindWorkOrderIndentDetailsByWPOID();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ShowDCPopUp();", true);
            }

            if (e.CommandName == "PDF")
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderPO_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            //string WPOID = gvWorkOrderPO.DataKeys[index].Values[0].ToString();
            //ViewState["WPOID"] = WPOID;
            Label lblDCNo = (Label)gvVendorDCitemDetails.Rows[index].FindControl("lblDCNo");

            ViewState["DCNo"] = lblDCNo.Text;

            BindWorkOrderIndentDetailsByWPOID();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ShowItemNameDetails();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvVendorDCitemDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "DeletePOItem")
            {
                objPc.VDCDID = gvVendorDCitemDetails.DataKeys[index].Values[0].ToString();
                ds = objPc.DeleteVendorDCItemDetailsByVDCDID();
                //Deleted               

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Vendor DC Item Details Deleted Successfully');", true);
                BindVendorDCItemDetails();
                BindWorkOrderIndentDetailsByWPOID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvVendorDCitemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (ViewState["DCSharedStatus"].ToString() == "1")
                {
                    btnDelete.Visible = false;
                    btnSaveDCItem.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                    btnSaveDCItem.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderIndent_onRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAllow = (LinkButton)e.Row.FindControl("btnAllow");

                if (dr["APDCQty"].ToString() == "1")
                    btnAllow.Text = "Un Allow";
                else
                    btnAllow.Text = "Allow";

                if (objSession.type == 1)
                    btnAllow.Visible = true;
                else
                    btnAllow.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderIndent_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Allow")
            {
                objSt.UserID = Convert.ToInt32(objSession.employeeid);
                objSt.WOIHID = Convert.ToInt32(gvWorkOrderIndent.DataKeys[index].Values[0].ToString());
                ds = objSt.UpdateAllowPermissionVendorDcQtyByWOIHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    if (ds.Tables[0].Rows[0]["Allow"].ToString() == "1")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Granted Succesfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Denied Succesfully');", true);
                }
                BindWorkOrderIndentDetailsByWPOID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common methods"

    private void PrintWorkOrderPODetails()
    {
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "WorkorderPoPrint.aspx?WPOID=" + ddlWPONo.SelectedValue + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlWoNoAndVendorNameLoad()
    {
        DataSet dsWPONo = new DataSet();
        DataSet dsSC = new DataSet();
        try
        {
            objPc = new cPurchase();
            dsWPONo = objPc.GetWPONumberDetailsByVendorDC(ddlWPONo, rblWPONoChange.SelectedValue);
            ViewState["WPONo"] = dsWPONo.Tables[0];
            dsSC = objPc.GetSupplierChainVendorNameDetailsByVendorDC(ddlVendorName, rblWPONoChange.SelectedValue);
            ViewState["Vendor"] = dsSC.Tables[0];
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
            divVendorDCPrint_p.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "VendorDC_" + hdnVDCID.Value + ".html";
            string pdffile = "VendorDC_" + hdnVDCID.Value + ".pdf";
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
        DataSet ds;
        try
        {

            ds = (DataSet)ViewState["Address"];
            DataTable dt1;
            DataTable dt2;

            dt1 = (DataTable)ds.Tables[0];
            dt2 = (DataTable)ds.Tables[2];

            string Address = dt1.Rows[0]["Address"].ToString();
            string TINNo = dt2.Rows[0]["TINNo"].ToString();
            string CodeNo = dt2.Rows[0]["CodeNo"].ToString();
            string CSTNo = dt2.Rows[0]["CSTNo"].ToString();
            string GSTNumber = dt2.Rows[0]["GSTNumber"].ToString();

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
            w.WriteLine("<table style='border-width:0px'><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            w.WriteLine("<div class='header' style='background:transparent;border:0px solid #000'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");

            w.WriteLine("<div class='col-sm-12 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;color:#000;font-family: Arial;display: contents;font-weight:700'>LONESTAR INDUSTRIES </h3>");
            w.WriteLine("<p style='font-weight:500;color:#000;'>" + Address + "</p>");
            w.WriteLine("<div style='display:flex;align-items:center;justify-content:center'>");
            w.WriteLine("<p style='font-weight:500;color:#000;display:inline-block'>Code No:" + CodeNo + " <span> | </span></p>");
            w.WriteLine("<p style='font-weight:500;color:#000;display:inline-block'>TIN No:" + TINNo + " <span> | </span></p>");
            w.WriteLine("<p style='font-weight:500;color:#000;display:inline-block'>CST No:" + CSTNo + " <span> | </span></p>");
            w.WriteLine("<p style='font-weight:500;color:#000;display:inline-block'>GST No:" + GSTNumber + " <span> | </span></p>");
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

    private void BindVendorDCDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.WPOID = Convert.ToInt32(ddlWPONo.SelectedValue);
            ds = objPc.GetVendorDCDetails();
            ViewState["VendorDC"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvVendorDC.DataSource = ds.Tables[0];
                gvVendorDC.DataBind();
            }
            else
            {
                gvVendorDC.DataSource = "";
                gvVendorDC.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //private void BindWorkOrderPODetails()
    //{
    //    DataSet ds = new DataSet();
    //    objPc = new cPurchase();
    //    try
    //    {
    //        //objPc.SCVMID = Convert.ToInt32(ddlSupplierChainVendor.SelectedValue);
    //        ds = objPc.GetWorkOrderApprovedPODetails();
    //        ViewState["WorkOrderPO"] = ds.Tables[0];

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvWorkOrderPO.DataSource = ds.Tables[0];
    //            gvWorkOrderPO.DataBind();
    //        }
    //        else
    //        {
    //            gvWorkOrderPO.DataSource = "";
    //            gvWorkOrderPO.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    private void BindVendorDCItemDetails()
    {
        DataSet ds = new DataSet();
        objPc = new cPurchase();
        try
        {
            objPc.VDCID = Convert.ToInt32(hdnVDCID.Value);
            ds = objPc.GetVendorDCItemDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvVendorDCitemDetails.DataSource = ds.Tables[0];
                gvVendorDCitemDetails.DataBind();
            }
            else
            {
                gvVendorDCitemDetails.DataSource = "";
                gvVendorDCitemDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderIndentDetailsByWPOID()
    {
        DataSet ds = new DataSet();
        cProduction objP = new cProduction();
        try
        {
            objP.WPOID = Convert.ToInt32(ddlWPONo.SelectedValue);
            ds = objP.GetWorkOrderIndentItemNameDetailsWPOID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderIndent.DataSource = ds.Tables[0];
                gvWorkOrderIndent.DataBind();
            }
            else
            {
                gvWorkOrderIndent.DataSource = "";
                gvWorkOrderIndent.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShareDC()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            if (gvVendorDCitemDetails.Rows.Count > 0)
            {
                objSt.VDCID = Convert.ToInt32(hdnVDCID.Value);
                ds = objSt.UpdateVendorDCStatusByVDCID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Vendor DC Shared Successfully');HideDCPopup();", true);
                    BindVendorDCDetails();
                    ////  SaveAlertDetails();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','DC Has No records');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void PrintVendorDC(int index)
    {
        try
        {
            DataSet ds = new DataSet();
            objPc = new cPurchase();
            objPc.VDCID = Convert.ToInt32(gvVendorDC.DataKeys[index].Values[0].ToString());
            ds = objPc.GetVendorDCDetailsByVDCIDForPDF();
            //VDCID	WPOID DCDateEdit TariffClassification DutyDetailsDate	DutyDetailsDateEdit	LocationID	Location	Address
            ViewState["Address"] = ds;

            DataTable dt2;
            dt2 = (DataTable)ds.Tables[2];

            hdnAddress.Value = ds.Tables[0].Rows[0]["Address"].ToString();

            hdnTINNo.Value = dt2.Rows[0]["TINNo"].ToString();
            hdnCodeNo.Value = dt2.Rows[0]["CodeNo"].ToString();
            hdnCSTNo.Value = dt2.Rows[0]["CSTNo"].ToString();
            hdnGSTNumber.Value = dt2.Rows[0]["GSTNumber"].ToString();
            hdnCompanyName.Value = dt2.Rows[0]["CompanyName"].ToString();

            lblWONo_p.Text = ds.Tables[0].Rows[0]["Wonumber"].ToString();
            lblFormJJno_p.Text = ds.Tables[0].Rows[0]["FormJJNo"].ToString();
            lblDCno_p.Text = ds.Tables[0].Rows[0]["DCNo"].ToString();
            lblDate_p.Text = ds.Tables[0].Rows[0]["DCDate"].ToString();
            lblSuppliername_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
            lblSupplierAddress_p.Text = ds.Tables[0].Rows[0]["SupplierAdddress"].ToString();
            lbltarrifClassification_p.Text = ds.Tables[0].Rows[0]["TariffClassification"].ToString();
            lblExpectedDurationofProcessing_p.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
			CompanyName_P.InnerText = hdnCompanyName.Value;

            gvWorkOrderPOItemDetails_p.DataSource = ds.Tables[1];
            gvWorkOrderPOItemDetails_p.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "PrintVendorDC();", true);
            // GeneratePDDF();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void clearValues()
    {
        hdnVDCID.Value = "0";
        txtDCDate.Text = "";
        ddlLocationName.SelectedIndex = 0;
        txtFormJJNNo.Text = "";
        txtDutyDetailsDate.Text = "";
        txtDuration.Text = "";
        txtTarrifClassification.Text = "";
    }

    //public void SaveAlertDetails()
    //{
    //    EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
    //    DataSet ds = new DataSet();
    //    cCommon objc = new cCommon();
    //    try
    //    {
    //        ds = objc.GetEmployeeIDDetailsByUserTypeIDSANDErpUserType("12", 1);

    //        string[] str = ds.Tables[0].Rows[0]["EmployeeIDS"].ToString().Split(',');

    //        for (int i = 0; i < str.Length; i++)
    //        {
    //            objAlerts.EntryMode = "Individual";
    //            objAlerts.AlertType = "Mail";
    //            objAlerts.userID = objSession.employeeid;
    //            objAlerts.reciverType = "Staff";
    //            objAlerts.file = "";
    //            objAlerts.reciverID = str[i];
    //            objAlerts.EmailID = "";
    //            objAlerts.GroupID = 0;
    //            objAlerts.Subject = "Work Order Inward Alert";
    //            objAlerts.Message = "" + ViewState["DCNo"].ToString();
    //            objAlerts.SaveCommunicationEmailAlertDetails();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = divAddNew.Visible = false;
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

    private void ShowHidePopUpControls(string divids)
    {
        divAdd_P.Visible = divInput_P.Visible = divOutput_P.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd_P.Visible = true;
                        break;
                    case "input":
                        divInput_P.Visible = true;
                        break;
                    case "view":
                        divOutput_P.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvVendorDC.Rows.Count > 0)
            gvVendorDC.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}