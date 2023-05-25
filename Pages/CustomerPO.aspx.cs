using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

public partial class Pages_CustomerPO : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cSales objSales;
    cMaterials objMat;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];

                ShowHideControls("add,view");

                BindCustomerPO();
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    objSales = new cSales();
                    objc = new cCommon();
                    objSales.PODID = Convert.ToInt32(arg.ToString());
                    ds = objSales.DeleteCustomerPODetailsByPODID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Customer PO Details Deleted Successfully');ShowAddPopUp();", true);

                    BindCustomerPODetails();
                }
                if (target == "ViewPOCopy")
                {
                    int index = Convert.ToInt32(arg.ToString());
                    ViewPOCopy(index);
                }
                if (target == "ViewPOCopyWithoutPrice")
                {
                    int index = Convert.ToInt32(arg.ToString());
                    ViewPOCopyWithoutPrice(index);
                }
                if (target == "SharePO")
                    updatePOSharedStatus(arg.ToString());

                if (target == "deleteCustomerPO")
                {
                    DataSet ds = new DataSet();
                    objSales = new cSales();

                    objSales.POHID = arg.ToString();

                    ds = objSales.DeleteCustomerPOHeaderByPOHID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Deleted Succesfuly');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('Info','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                    BindCustomerPO();
                }

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            divOutput.Visible = false;
            divPDF.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

                objSales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;
                objSales.GetDispatchedOfferDetailsByEnquiryNumber(ddlOfferNumber);

            }

            divOutput.Visible = false;
            divPDF.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlOfferNumber_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOfferNumber.SelectedIndex > 0)
            divPDF.Visible = true;
        else
            divPDF.Visible = false;
    }

    //protected void ddlitemname_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    objMat = new cMaterials();
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        objMat.EDID = Convert.ToInt32(ddlitemname.SelectedValue);
    //        ds = objMat.GetQuanityAndUnitPriceByEDID();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txt_quantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
    //            txtUnitPrice.Text = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"Button Events"

    protected void btndownloadPDF_Click(object sender, EventArgs e)
    {

        try
        {
            string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();

            string FileUrl = PDFSavePath + ddlOfferNumber.SelectedItem.Text + ".pdf";

            if (File.Exists(FileUrl))
                cCommon.DownLoad(ddlOfferNumber.SelectedItem.Text + ".pdf", FileUrl);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','File Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            hdnPOHID.Value = "0";
            ShowHideControls("input");
            ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = true;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        objc = new cCommon();
        string PoCopy = "";
        string PoCopyWithoutPrice = "";
        try
        {
            if (objc.Validate(divInput))
            {
                objSales.POHID = hdnPOHID.Value;
                objSales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;

                objSales.EODID = Convert.ToInt32(ddlOfferNumber.SelectedValue);

                if (txtPODate.Text != "")
                    objSales.PODate = DateTime.ParseExact(txtPODate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else
                    objSales.date = "";
                objSales.PORefgNo = txtRefNo.Text;

                string MaxAttachementId = objSales.GetMaximumAttachementID();
                if (fPoCopy.HasFile)
                {
                    string FileName = Path.GetFileName(fPoCopy.PostedFile.FileName);

                    string Attchname = "";

                    string[] extension = FileName.Split('.');
                    Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");

                    PoCopy = Attchname + '_' + Convert.ToInt32(MaxAttachementId) + '_' + ddlEnquiryNumber.SelectedValue + '.' + extension[extension.Length - 1];
                    objSales.POCopy = PoCopy;
                }
                else
                    objSales.POCopy = null;

                if (fPoCopyWithoutPrice.HasFile)
                {
                    string FileName = Path.GetFileName(fPoCopyWithoutPrice.PostedFile.FileName);

                    string Attchname = "";

                    string[] extension = FileName.Split('.');

                    Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");

                    PoCopyWithoutPrice = Attchname + '_' + Convert.ToInt32(MaxAttachementId) + 1 + '_' + ddlEnquiryNumber.SelectedValue + '.' + extension[extension.Length - 1];
                    objSales.POCopyWithoutPrice = PoCopyWithoutPrice;
                }
                else
                    objSales.POCopyWithoutPrice = null;

                objSales.CreatedBy = objSession.employeeid;

                ds = objSales.SaveCustomerPO();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Customer Po  Saved Successfully');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Customer Po  Already Exists');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Customer Po  Updated Successfully');", true);

                string path = CusstomerEnquirySavePath + ddlEnquiryNumber.SelectedValue + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!string.IsNullOrEmpty(PoCopy))
                    fPoCopy.SaveAs(path + PoCopy);

                if (!string.IsNullOrEmpty(PoCopyWithoutPrice))
                    fPoCopyWithoutPrice.SaveAs(path + PoCopyWithoutPrice);

                BindCustomerPO();
                ShowHideControls("add,view");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ShowHideControls("add,view");
            hdnPOHID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void btndetails_click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;
        DateTime dtime;
        try
        {
            dt.Columns.Add("PODID");
            dt.Columns.Add("POHID");
            dt.Columns.Add("EDID");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("DateOfDelivery");
            dt.Columns.Add("Checked");
            dt.Columns.Add("DDID");

            foreach (GridViewRow row in gvCustomerPOdetails.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                dr = dt.NewRow();
                if (chkditems.Checked)
                {
                    dr["PODID"] = Convert.ToInt32(gvCustomerPOdetails.DataKeys[row.RowIndex].Values[0]);
                    dr["POHID"] = Convert.ToInt32(hdnPOHID.Value);
                    dr["EDID"] = Convert.ToInt32(gvCustomerPOdetails.DataKeys[row.RowIndex].Values[1]);
                    dr["Quantity"] = Convert.ToInt32(((TextBox)gvCustomerPOdetails.Rows[row.RowIndex].FindControl("txtqty")).Text);
                    dr["UnitPrice"] = Convert.ToDecimal(((TextBox)gvCustomerPOdetails.Rows[row.RowIndex].FindControl("txtunitprice")).Text);
                    TextBox txdate = (TextBox)gvCustomerPOdetails.Rows[row.RowIndex].FindControl("txtDateOfDelivery");
                    dtime = DateTime.ParseExact(txdate.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dr["DateOfDelivery"] = dtime.ToString("MM/dd/yyyy").Replace("-", "/");
                    dr["Checked"] = Convert.ToInt32(1);
                    dr["DDID"] = Convert.ToInt32(gvCustomerPOdetails.DataKeys[row.RowIndex].Values[2]);
                }
                else
                {
                    dr["PODID"] = Convert.ToInt32(gvCustomerPOdetails.DataKeys[row.RowIndex].Values[0]);
                    dr["POHID"] = Convert.ToInt32(hdnPOHID.Value);
                    dr["EDID"] = Convert.ToInt32(0);
                    dr["Quantity"] = Convert.ToInt32(0);
                    dr["UnitPrice"] = Convert.ToDecimal(0);
                    dr["DateOfDelivery"] = null;
                    dr["Checked"] = Convert.ToInt32(0);
                    dr["DDID"] = Convert.ToInt32(gvCustomerPOdetails.DataKeys[row.RowIndex].Values[2]);
                }
                dt.Rows.Add(dr);
            }
            objSales.POTable = dt;
            //objSales.PODID = Convert.ToInt32(hdnPODID.Value);
            //objSales.POHID = hdnPOHID.Value;
            //objSales.EDID = Convert.ToInt32(ddlitemname.SelectedValue);
            //objSales.Quantity = Convert.ToInt32(txt_quantity.Text);
            //objSales.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            //objSales.dateOfDelivery = DateTime.ParseExact(txtDateOfDelivery.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objSales.SaveCustomerPODetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Customer Po Details Saved Successfully');HideCustomerPoPopUp();", true);
            BindCustomerPODetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');ShowAddPopUp();", true);
            BindCustomerPODetails();
        }
    }
    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());
    }

    #endregion

    #region"GridView Events"

    protected void gvCustomerPo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FileName;
        int POHID;
        DataTable dt;
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            int EnquiryNumber = Convert.ToInt32(gvCustomerPo.DataKeys[index].Values[1].ToString());
            ViewState["Enquirynumber"] = EnquiryNumber;
            string BaseHtttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";
            if (e.CommandName == "ViewPOCopy")
            {
                FileName = gvCustomerPo.DataKeys[index].Values[2].ToString();
                ViewState["FileName"] = FileName;
                //ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                //if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                //{
                //    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po Copy Not Found');", true);
                //}
                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
            }

            if (e.CommandName == "ViewPoCopyWithoutPrice")
            {
                FileName = gvCustomerPo.DataKeys[index].Values[3].ToString();
                ViewState["FileName"] = FileName;
                //ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                //if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                //{
                //    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po copy Without Price Not Found');", true);
                //}
                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
            }
            if (e.CommandName == "EditCustomerPO")
            {
                objc = new cCommon();
                objSales = new cSales();

                POHID = Convert.ToInt32(gvCustomerPo.DataKeys[index].Values[0].ToString());
                dt = (DataTable)ViewState["CustomerPO"];

                hdnPOHID.Value = POHID.ToString();

                dt.DefaultView.RowFilter = "POHID='" + POHID + "'";

                txtRefNo.Text = dt.DefaultView.ToTable().Rows[0]["PORefNo"].ToString();
                ddlCustomerName.SelectedValue = dt.DefaultView.ToTable().Rows[0]["ProspectID"].ToString();
                ddlEnquiryNumber.SelectedValue = dt.DefaultView.ToTable().Rows[0]["EnquiryNumber"].ToString();
                // ddlOfferNumber.SelectedValue = dt.DefaultView.ToTable().Rows[0]["EODID"].ToString();

                objSales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;
                objSales.GetDispatchedOfferDetailsByEnquiryNumber(ddlOfferNumber);

                ddlEnquiryNumber.Enabled = ddlCustomerName.Enabled = false;

                txtPODate.Text = dt.DefaultView.ToTable().Rows[0]["PODateWithTime"].ToString();

                ShowHideControls("input");
            }

            if (e.CommandName == "AddPODetails")
            {
                objMat = new cMaterials();

                string POSharedStatus = gvCustomerPo.DataKeys[index].Values[5].ToString();

                if (POSharedStatus == "1")
                    btndetails.Visible = false;
                else
                    btndetails.Visible = true;

                objMat.EnquiryNumber = EnquiryNumber;
                //objMat.GetItemDetailsByEnquiryNumber(ddlitemname);
                hdnEODID.Value = gvCustomerPo.DataKeys[index].Values[4].ToString();
                hdnPOHID.Value = gvCustomerPo.DataKeys[index].Values[0].ToString();
                BindCustomerPODetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();", true);
                popuplbl.InnerText = "Customer PO Details for " + ((Label)gvCustomerPo.Rows[index].FindControl("lblCustomerEnquiryNumber")).Text;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCustomerPOdetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PODID = gvCustomerPOdetails.DataKeys[index].Values[0].ToString();

            hdnPODID.Value = PODID;

            dt = (DataTable)ViewState["CustomerPODetails"];

            dt.DefaultView.RowFilter = "PODID='" + PODID + "'";

            //txt_quantity.Text = dt.DefaultView.ToTable().Rows[0]["Quantity"].ToString();
            //txtUnitPrice.Text = dt.DefaultView.ToTable().Rows[0]["UnitPrice"].ToString();
            //txtDateOfDelivery.Text = dt.DefaultView.ToTable().Rows[0]["DateOfDeliveryWithTime"].ToString();
            //ddlitemname.SelectedValue = dt.DefaultView.ToTable().Rows[0]["EDID"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowAddPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCustomerPo_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnedit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton btnDeletePO = (LinkButton)e.Row.FindControl("btnDeletePO");

                if (dr["POSharedStatus"].ToString() == "1")
                    btnedit.Visible = false;
                else
                    btnedit.Visible = true;

                if (objSession.type == 1)
                    btnDeletePO.Visible = true;
                else
                {
                    if (dr["POSharedStatus"].ToString() == "1")
                        btnDeletePO.Visible = false;
                    else
                        btnDeletePO.Visible = true;
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

    private void BindCustomerPO()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.EmpID = Convert.ToInt32(objSession.employeeid);
            ds = objSales.GetCustomerPO();
            ViewState["CustomerPO"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCustomerPo.DataSource = ds.Tables[0];
                gvCustomerPo.DataBind();
            }
            else
            {
                gvCustomerPo.DataSource = "";
                gvCustomerPo.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCustomerPODetails()
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            //objSales.POHID = hdnPOHID.Value;
            objSales.EODID = Convert.ToInt32(hdnEODID.Value);
            //objSales.POEnquirynumber = ViewState["Enquirynumber"].ToString();
            ds = objSales.GetCustomerPODetails();

            ViewState["CustomerPODetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCustomerPOdetails.DataSource = ds.Tables[0];
                gvCustomerPOdetails.DataBind();
            }
            else
            {
                gvCustomerPOdetails.DataSource = "";
                gvCustomerPOdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
        try
        {
            string[] mode = divids.Split(',');
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

    private void ViewPOCopy(int index)
    {
        string FileName;
        string EnquiryNumber;
        objc = new cCommon();
        try
        {
            FileName = gvCustomerPo.DataKeys[index].Values[2].ToString();
            EnquiryNumber = gvCustomerPo.DataKeys[index].Values[1].ToString();
            objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewPOCopyWithoutPrice(int index)
    {
        string FileName;
        string EnquiryNumber;
        objc = new cCommon();
        try
        {
            FileName = gvCustomerPo.DataKeys[index].Values[3].ToString();
            EnquiryNumber = gvCustomerPo.DataKeys[index].Values[1].ToString();
            ViewState["FileName"] = FileName;
            objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void updatePOSharedStatus(string POHID)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.POHID = POHID;
            ds = objSales.UpdatePOSharedStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PO Shared Successfully');", true);
                BindCustomerPO();
                SaveAlertDetails(ds.Tables[0].Rows[0]["EnquiryNo"].ToString());
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails(string EnquiryID)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.EnquiryID = EnquiryID;
            ds = objc.GetStaffNameDetailsByEnquiryID();

            objAlerts.EntryMode = "Individual";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = objSession.employeeid;
            objAlerts.reciverType = "Staff";
            objAlerts.file = "";
            objAlerts.reciverID = ds.Tables[0].Rows[0]["Design"].ToString();
            objAlerts.EmailID = "";
            objAlerts.GroupID = 0;
            objAlerts.Subject = "Alert For Design PO Review";
            objAlerts.Message = "Design PO Review Request From Enquiry Number" + EnquiryID;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
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
        if (gvCustomerPOdetails.Rows.Count > 0)
            gvCustomerPOdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvCustomerPo.Rows.Count > 0)
            gvCustomerPo.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}