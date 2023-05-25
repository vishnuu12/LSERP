using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_StaffAssignment : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSess = new cSession();
    cCommonMaster objCommon;

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
            if (IsPostBack == false)
            {
                BindStaffAssignmentEnquiryDetails();
                AssignModuleName();
            }
            else
                return;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvStaffAssignmentDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtEmployeeListByDept;

        try
        {
            //Set the edit index.
            gvStaffAssignmentDetails.EditIndex = e.NewEditIndex;

            //Bind data to the GridView control.
            BindStaffAssignmentEnquiryDetails();

            DropDownList dl = new DropDownList();
            dl = (DropDownList)gvStaffAssignmentDetails.Rows[gvStaffAssignmentDetails.EditIndex].FindControl("ddlEmployeeName");

            dtEmployeeListByDept = (DataTable)ViewState["EmployeeListByDept"];
            dl.DataSource = dtEmployeeListByDept;
            dl.DataTextField = "EmployeeName";
            dl.DataValueField = "EmployeeID";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("-- Select Employee Name --", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvStaffAssignmentDetails.EditIndex = -1;
            BindStaffAssignmentEnquiryDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            if (((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName")).SelectedIndex > 0)
            {
                objCommon.SAID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[0].ToString());
                objCommon.EnquiryID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[1].ToString());
                objCommon.EmployeeID = Convert.ToInt32(((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName")).SelectedValue);
                objCommon.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                ds = objCommon.UpdateStaffAssignMentDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Updated Successfully');", true);

                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Update Failure');", true);

                gvStaffAssignmentDetails.EditIndex = -1;
                BindStaffAssignmentEnquiryDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[4].ToolTip = "Edit";
            if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (e.Row.Cells.GetCellIndex(cell) == 4)
                    {
                        ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[4].Controls[0])).ToolTip = "Update";
                        ((System.Web.UI.LiteralControl)(e.Row.Cells[4].Controls[1])).Text = "&nbsp;";
                        ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[4].Controls[2])).ToolTip = "Close";
                    }
                }
            }
        }
    }

    #endregion

    #region"Common Methods"

    private void BindStaffAssignmentEnquiryDetails()
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommon.EmployeeID = Convert.ToInt32(_objSess.employeeid);
            ds = objCommon.GetStaffAssignmentEnquiryDetails();
            ViewState["EmployeeListByDept"] = ds.Tables[1];
            ViewState["DepartmentID"] = ds.Tables[2].Rows[0]["DepartmentID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStaffAssignmentDetails.DataSource = ds.Tables[0];
                gvStaffAssignmentDetails.DataBind();
                gvStaffAssignmentDetails.UseAccessibleHeader = true;
                gvStaffAssignmentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
            }
            else
            {
                gvStaffAssignmentDetails.DataSource = "";
                gvStaffAssignmentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void AssignModuleName()
    {
        DataTable dt;
        try
        {
            dt = (DataTable)Session["UserModules"];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MID"].ToString() == "11")
                    lblModuleName.Text = "Sales";
                else if (dr["MID"].ToString() == "4")
                    lblModuleName.Text = "Design";
                else if (dr["MID"].ToString() == "6")
                    lblModuleName.Text = "Production";
                else if (dr["MID"].ToString() == "7")
                    lblModuleName.Text = "Procurement";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}