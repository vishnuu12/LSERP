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

public partial class Pages_WorkOrderInward : System.Web.UI.Page
{
    #region"Declaration"

    cPurchase objPc;
    cCommon objc;
    cStores objSt;
    cSession objSession = new cSession();
    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Event"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (!IsPostBack)
            {
                //DataSet dsDCNo = new DataSet();
                //DataSet dsVendorName = new DataSet();
                //objPc = new cPurchase();
                //dsDCNo = objPc.GetVendorDCNoDetails(ddlDCNo);
                //objPc.PoType = "All";
                //dsVendorName = objPc.GetSupplierChainVendorNameDetails(ddlVendorName);
                ddlDCNoAndVendornameLoad();
                ShowHideControls("add");
            }

            if (target == "ShareInwardDC")
                UpdateShareInwardDCStatus();
            if (target == "DeleteWOIH")
                DeleteWorkOrderInwardHeaderByWOIHID(arg.ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblWPONoChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDCNoAndVendornameLoad();
            //   ddlWoNoAndVendorNameLoad();
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Dropdown Events"

    protected void ddlDCNo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objPc = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            if (ddlDCNo.SelectedIndex > 0)
            {
                objPc.VDCID = Convert.ToInt32(ddlDCNo.SelectedValue);
                ds = objPc.GetVendorNameByVDCID();
                ddlVendorName.SelectedValue = ds.Tables[0].Rows[0]["SCVMID"].ToString();

                BindWorkOrderInward();
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
                //objPc.SCVMID = Convert.ToInt32(ddlVendorName.SelectedValue);
                //ds = objPc.GetDCNumberByVendorName(ddlDCNo);
                dt = (DataTable)ViewState["DCNo"];
                if (ddlVendorName.SelectedIndex > 0)
                {
                    string SCVMID = ddlVendorName.SelectedValue;
                    dt.DefaultView.RowFilter = "SCVMID='" + SCVMID + "'";
                    dt.DefaultView.ToTable();
                }

                ddlDCNo.DataSource = dt;
                ddlDCNo.DataTextField = "DCNo";
                ddlDCNo.DataValueField = "VDCID";
                ddlDCNo.DataBind();
                ddlDCNo.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
                ddlDCNo.SelectedIndex = 0;
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveMI_Click(object sender, EventArgs e)
    {
        objSt = new cStores();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (objc.Validate(divInput))
            {
                objSt.VDCID = Convert.ToInt32(ddlDCNo.SelectedValue);
                objSt.SCVMID = Convert.ToInt32(ddlVendorName.SelectedValue);
                objSt.WorkOrderInwardHeaderID = Convert.ToInt32(hdnWOIHeaderID.Value);
                objSt.InvoiceNumber = txtInVoiceNumber.Text;
                objSt.InvoiceDate = DateTime.ParseExact(txtInvoiceDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objSt.DCNumber = txtDCNumber.Text;
                objSt.DCDate = DateTime.ParseExact(txtDCDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objSt.UserID = Convert.ToInt32(objSession.employeeid);

                objSt.EwayBillNo = txtEwayBillNo.Text;

                if (fpDCAttach.HasFile)
                {
                    cSales objSales = new cSales();
                    objc = new cCommon();
                    objc.Foldername = Session["StoresDocsSavePath"].ToString();
                    string Name = Path.GetFileName(fpDCAttach.PostedFile.FileName);
                    string MaximumAttacheID = objSales.GetMaximumAttachementID();
                    string[] extension = Name.Split('.');
                    Name = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                    objc.FileName = Name;
                    objc.PID = "WororderInwardDCFile";
                    objc.AttachementControl = fpDCAttach;
                    objc.SaveFiles();
                    objSt.DCCopy = Name;
                }
                else
                    objSt.DCCopy = "";

                ds = objSt.SaveWorkOrderInward();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Work Order Inward Saved Successfully');", true);
                    BindWorkOrderInward();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Work Order Updated Successfully');", true);

                ShowHideControls("addnew,add,view");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveWOIDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        objSt = new cStores();
        try
        {
            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("WOIHID");
            dt.Columns.Add("WOID");
            dt.Columns.Add("PartQty");

            foreach (GridViewRow row in gvVendorDCdetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                TextBox txtPartQty = (TextBox)row.FindControl("txtInwardQty");

                string WOIHID = gvVendorDCdetails.DataKeys[row.RowIndex].Values[0].ToString();

                if (chkitems.Checked)
                {
                    if (Convert.ToInt32(txtPartQty.Text) > 0)
                    {
                        dr = dt.NewRow();
                        dr["WOIHID"] = WOIHID;
                        dr["WOID"] = 0;
                        dr["PartQty"] = txtPartQty.Text;
                        dt.Rows.Add(dr);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Part Qty Should Not Zero');", true);
                }
            }

            objSt.dt = dt;
            objSt.WorkOrderInwardHeaderID = Convert.ToInt32(hdnWOIHeaderID.Value);
            objSt.VDCID = Convert.ToInt32(ddlDCNo.SelectedValue);
            //objSt.DCSubTotalQty = Convert.ToInt32(hdnDCSubTotalQty.Value);
            ds = objSt.SaveWorkOrderInwardDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Workorder Inward Details Saved Successfully');HideAddPopUp();", true);
            BindWorkOrderInward();
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
            hdnWOIHeaderID.Value = "0";
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnWOIHeaderID.Value = "0";
            ShowHideControls("add,addnew,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvWorkOrderInward_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string MIHID = gvWorkOrderInward.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["WI"];
            hdnWOIHeaderID.Value = MIHID.ToString();
            dt.DefaultView.RowFilter = "WOInwardHeaderID='" + MIHID + "'";

            if (e.CommandName == "EDitWI")
            {
                txtDCNumber.Text = dt.DefaultView.ToTable().Rows[0]["DCNumber"].ToString();

                txtDCDate.Text = dt.DefaultView.ToTable().Rows[0]["DCDateEdit"].ToString();

                txtInvoiceDate.Text = dt.DefaultView.ToTable().Rows[0]["InVoiceDateEdit"].ToString();
                txtInVoiceNumber.Text = dt.DefaultView.ToTable().Rows[0]["InVoiceNumber"].ToString();

                ShowHideControls("input");
            }
            if (e.CommandName == "Add")
            {
                lblheadername_p.Text = ddlDCNo.SelectedItem.Text + "/" + ddlVendorName.SelectedItem.Text;

                ViewState["ShareInwardDC"] = dt.DefaultView.ToTable().Rows[0]["ShareInwardDC"].ToString();//ShareInwardDC;

                if (ViewState["ShareInwardDC"].ToString() == "0")
                    btnSaveWOIDetails.Visible = true;
                else
                    btnSaveWOIDetails.Visible = false;

                GetVendorDCDetailsByVendorNumber();
                BindWorkOrderInwardedDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderInward_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAdd = (LinkButton)e.Row.FindControl("btnAdd");
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                //if (!string.IsNullOrEmpty(dr["WOIHIDStatus"].ToString()))
                //    btnAdd.Enabled = false;

                if (dr["ShareInwardDC"].ToString() == "1")
                    lbtnDelete.Visible = false;
                else
                    lbtnDelete.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderInwardedDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "DeleteInwardDetails")
            {
                hdnWOIDID.Value = gvWorkOrderInwardedDetails.DataKeys[index].Values[0].ToString();
                DeleteWorkOrderInwardDetailsByWOIDID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderInwardedDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (ViewState["ShareInwardDC"].ToString() == "1")
                    btnDelete.Visible = false;
                else
                    btnDelete.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvVendorDCdetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAllow = (LinkButton)e.Row.FindControl("btnAllow");

                if (objSession.type == 1)
                    btnAllow.Visible = true;
                else
                    btnAllow.Visible = false;

                if (dr["APDCQty"].ToString() == "0")
                    btnAllow.Text = "Allow";
                else
                    btnAllow.Text = "Un Allow";


            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvVendorDCdetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Allow")
            {
                objSt.UserID = Convert.ToInt32(objSession.employeeid);
                objSt.VDCDID = Convert.ToInt32(gvVendorDCdetails.DataKeys[index].Values[1].ToString());
                ds = objSt.UpdateAllowPermissionworkorderinwardQtyByVDCDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    if (ds.Tables[0].Rows[0]["Allow"].ToString() == "1")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Granted Succesfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Denied Succesfully');", true);
                }

                GetVendorDCDetailsByVendorNumber();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    //public void SaveAlertDetails()
    //{
    //    EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
    //    DataSet ds = new DataSet();
    //    cCommon objc = new cCommon();
    //    try
    //    {
    //        objc.UserTypeID = 10;
    //        objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
    //        ds = objc.GetStaffDetailsByRFPHIDAndUserType();

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
    //            objAlerts.Subject = "Work Order Indent Raised Alert";
    //            objAlerts.Message = "Work Order Indent Raised From RFP No" + ddlRFPNo.SelectedItem.Text + "Add Work Order QC Planning";
    //            objAlerts.Status = 0;
    //            objAlerts.SaveCommunicationEmailAlertDetails();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    private void DeleteWorkOrderInwardHeaderByWOIHID(string WOInwardHID)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.WorkOrderInwardHeaderID = Convert.ToInt32(WOInwardHID);
            ds = objSt.DeleteWorkorderinwardHeaderByWOIHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Work Order Inward Deleted Successfully');", true);
                BindWorkOrderInward();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlDCNoAndVendornameLoad()
    {
        try
        {
            DataSet dsDCNo = new DataSet();
            DataSet dsVendorName = new DataSet();
            objPc = new cPurchase();
            dsDCNo = objPc.GetVendorDCNoDetailsByWorkorderinward(ddlDCNo, rblWPONoChange.SelectedValue);
            ViewState["DCNo"] = dsDCNo.Tables[0];
            dsVendorName = objPc.GetVendorNameDetailsByWorkOrderInward(ddlVendorName, rblWPONoChange.SelectedValue);
            ViewState["VendorName"] = dsVendorName.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateShareInwardDCStatus()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.WorkOrderInwardHeaderID = Convert.ToInt32(hdnWOIHeaderID.Value);
            ds = objSt.UpdateShareWorkOrderInward();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Work Order Inward Shared Successfully'); HideAddPopUp();", true);
                BindWorkOrderInward();
                BindWorkOrderInwardedDetails();
                DataTable dt;
                dt = (DataTable)ViewState["WOInwardDetails"];

                var grouped = from table in dt.AsEnumerable()
                              group table by new { placeCol = table["RFPHID"] } into groupby
                              select new
                              {
                                  Value = groupby.Key,
                                  ColumnValues = groupby
                              };
                foreach (var key in grouped)
                {
                    foreach (var columnValue in key.ColumnValues)
                    {
                        SaveAlertDetails(columnValue["RFPHID"].ToString());
                    }
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "')", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails(string RFPHID)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.UserTypeID = 10;
            objc.RFPHID = Convert.ToInt32(RFPHID);
            objc.JCHID = 0;
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
                objAlerts.Subject = "Work Order Inward QC Alert";
                objAlerts.Message = "Work Order Inward Raised From RFP No " +
                    ds.Tables[0].Rows[0]["RFPNo"].ToString() + " Complete Your Work Order QC";
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetVendorDCDetailsByVendorNumber()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.VDCID = Convert.ToInt32(ddlDCNo.SelectedValue);
            ds = objSt.GetVendorDCDetailsByVendorByVendorNumber();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvVendorDCdetails.DataSource = ds.Tables[0];
                gvVendorDCdetails.DataBind();
            }
            else
            {
                gvVendorDCdetails.DataSource = "";
                gvVendorDCdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderInwardedDetails()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.VDCID = Convert.ToInt32(ddlDCNo.SelectedValue);
            objSt.WorkOrderInwardHeaderID = Convert.ToInt32(hdnWOIHeaderID.Value);
            ds = objSt.GetWorkOrderInwardedDetailsByVDCIDAndWOinwardHeaderID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderInwardedDetails.DataSource = ds.Tables[0];
                gvWorkOrderInwardedDetails.DataBind();

                ViewState["WOInwardDetails"] = ds.Tables[0];

            }
            else
            {
                gvWorkOrderInwardedDetails.DataSource = "";
                gvWorkOrderInwardedDetails.DataBind();
            }

            //hdnDCSubTotalQty.Value = ds.Tables[1].Rows[0]["SubTotalQty"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderInward()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.VDCID = Convert.ToInt32(ddlDCNo.SelectedValue);
            objSt.status = rblWPONoChange.SelectedValue;
            ds = objSt.GetWorkOrderInwardByVDCID();

            ViewState["WI"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderInward.DataSource = ds.Tables[0];
                gvWorkOrderInward.DataBind();
            }
            else
            {
                gvWorkOrderInward.DataSource = "";
                gvWorkOrderInward.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteWorkOrderInwardDetailsByWOIDID()
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.WOIDID = Convert.ToInt32(hdnWOIDID.Value);
            ds = objSt.DeleteWorkOrderInwardDetailsByWOIDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Workorder Inward Details Deleted Successfully');", true);
                BindWorkOrderInwardedDetails();
                GetVendorDCDetailsByVendorNumber();
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

    #endregion
}