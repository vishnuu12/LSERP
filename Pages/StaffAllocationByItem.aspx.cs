using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_StaffAllocationByItem : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSess = new cSession();
    cCommonMaster objCommon;
    cCommon objc;
    cDesign objDesign;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSess = Master.csSession;
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
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(_objSess.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(_objSess.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
                ShowHideControls("add");
            }
            else
            {
                if (target == "updateOverAllItemStatus")
                    UpdateOverAllItemStatus(arg.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Dropdown Methods"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            ddlEnquiryNumber_SelectIndexChanged(this, EventArgs.Empty);
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        objc = new cCommon();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;
                objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

                BindProcessDetails();
                ShowHideControls("add,view,input");
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            foreach (GridViewRow row in gvItemnamelist.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                string EDID = gvItemnamelist.DataKeys[row.RowIndex].Values[0].ToString();
                string Checked = chk.Checked == true ? "1" : "0";
                string ProcessID = ViewState["ProcessID"].ToString();
                string EmployeeID = ViewState["EmployeeID"].ToString();
                DateTime DeadLineDate = Convert.ToDateTime(ViewState["DeadLineDate"]);

                ds = objDesign.UpdateItemStaffAssignment(EDID, Checked, ProcessID, EmployeeID, DeadLineDate);
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "SuccessMessage('Success','Item Assigned Succesfully');HideItemlistPopUp();", true);
                BindItemStaffdetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvProcessList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string ProcessID = gvProcessList.DataKeys[index].Values[0].ToString();
            DropDownList ddlemployee = (DropDownList)gvProcessList.Rows[index].FindControl("ddlEmployeeName");
            TextBox txtDeadLineDate = (TextBox)gvProcessList.Rows[index].FindControl("txtDeadLineDate");
            if (e.CommandName == "AssignItem")
            {
                if (ddlemployee.SelectedIndex > 0 && txtDeadLineDate.Text != "")
                {
                    ViewState["ProcessID"] = ProcessID;
                    ViewState["EmployeeID"] = ddlemployee.SelectedValue;
                    objDesign = new cDesign();
                    objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                    objDesign.ProcessID = Convert.ToInt32(ProcessID);
                    ViewState["DeadLineDate"] = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    ds = objDesign.getItemDetailsByEnquiryNumberAndProcessID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvItemnamelist.DataSource = ds.Tables[0];
                        gvItemnamelist.DataBind();
                    }
                    else
                    {
                        gvItemnamelist.DataSource = "";
                        gvItemnamelist.DataBind();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ShowItemlistPopUp();", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ErrorMessage('Error','Select Employee name And Date');", true);
            }
            if (e.CommandName == "UpdateProcess" && txtDeadLineDate.Text != "")
            {
                if (ddlemployee.SelectedIndex > 0)
                {
                    UpdateProcessStaffAssignment(ProcessID, ddlemployee.SelectedValue, DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ErrorMessage('Error','Select Employee name And Date');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvProcessList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnadd = (LinkButton)e.Row.FindControl("btnViewEnqAttch");
                LinkButton btnupdate = (LinkButton)e.Row.FindControl("btnUpdate");

                DropDownList ddlEmployee = (DropDownList)e.Row.FindControl("ddlEmployeeName");
                objDesign.GetEmployeeNameList(ddlEmployee);

                if (e.Row.RowIndex >= 4)
                {
                    btnadd.Visible = false;
                    btnupdate.Visible = true;
                }
                else
                {
                    btnadd.Visible = true;
                    btnupdate.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvItemnamelist_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");
                if (string.IsNullOrEmpty(dr["EmpID"].ToString()))
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

    protected void gvItemStaffDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnUpdate = (LinkButton)e.Row.FindControl("btnUpdate");

                btnUpdate.Visible = dr["OverAllSpecStatus"].ToString() == "0" ? true : false;

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindProcessDetails()
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            ds = objDesign.GetProcessDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvProcessList.DataSource = ds.Tables[0];
                gvProcessList.DataBind();
            }
            else
            {
                gvProcessList.DataSource = "";
                gvProcessList.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvItemStaffDetails.DataSource = ds.Tables[1];
                gvItemStaffDetails.DataBind();
            }
            else
            {
                gvItemStaffDetails.DataSource = "";
                gvItemStaffDetails.DataBind();
            }

            lblDrafterName.Text = ds.Tables[2].Rows[0]["Drafting"].ToString();
            lblFEAName.Text = ds.Tables[2].Rows[0]["FEA"].ToString();
            lblQualityname.Text = ds.Tables[2].Rows[0]["Quality"].ToString();
            lblrefractoryname.Text = ds.Tables[2].Rows[0]["Refractory"].ToString();
            lblheattreatmentname.Text = ds.Tables[2].Rows[0]["HeatTratement"].ToString();
            lblpaintingname.Text = ds.Tables[2].Rows[0]["Painting"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindItemStaffdetails()
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            ds = objDesign.GetProcessDetails();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvItemStaffDetails.DataSource = ds.Tables[1];
                gvItemStaffDetails.DataBind();
            }
            else
            {
                gvItemStaffDetails.DataSource = "";
                gvItemStaffDetails.DataBind();
            }

            lblDrafterName.Text = ds.Tables[2].Rows[0]["Drafting"].ToString();
            lblFEAName.Text = ds.Tables[2].Rows[0]["FEA"].ToString();
            lblQualityname.Text = ds.Tables[2].Rows[0]["Quality"].ToString();
            lblrefractoryname.Text = ds.Tables[2].Rows[0]["Refractory"].ToString();
            lblheattreatmentname.Text = ds.Tables[2].Rows[0]["HeatTratement"].ToString();
            lblpaintingname.Text = ds.Tables[2].Rows[0]["Painting"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateProcessStaffAssignment(string ProcessID, string EmployeeID, DateTime DeadLineDate)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objDesign.ProcessID = Convert.ToInt32(ProcessID);
            objDesign.UserID = Convert.ToInt32(EmployeeID);
            objDesign.DeadLineDate = DeadLineDate;
            ds = objDesign.UpdateEnquiryProcessStaffAssignmentByEnquiryNumberAndProcessName();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "SuccessMessage('Success','Staff Assigned Succesfully');", true);
            BindItemStaffdetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateOverAllItemStatus(string EDID)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.EDID = Convert.ToInt32(EDID);
            ds = objDesign.UpdateOverAllItemStatusByEDID();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "SuccessMessage('Success','Item Completed Succesfully');", true);
                BindItemStaffdetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
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

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvProcessList.Rows.Count > 0)
            gvProcessList.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvItemStaffDetails.Rows.Count > 0)
            gvItemStaffDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}