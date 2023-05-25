using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_UserLoginCreationMaster : System.Web.UI.Page
{

    #region"Declaration"

    cSession _objSess = new cSession();
    cCommonMaster objCommon;
    cCommon objc;

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
                BindEmployeeDetails();
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

    protected void gvEmployeeList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtEmployeeListByDept;
        try
        {
            //Set the edit index.
            gvEmployeeList.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindEmployeeDetails();

            DropDownList dl = new DropDownList();
            dl = (DropDownList)gvEmployeeList.Rows[gvEmployeeList.EditIndex].FindControl("ddlUserRole");

            dtEmployeeListByDept = (DataTable)ViewState["UserRole"];
            dl.DataSource = dtEmployeeListByDept;
            dl.DataTextField = "USERTYPE";
            dl.DataValueField = "USERTYPEID";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("-- Select User Role --", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvEmployeeList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvEmployeeList.EditIndex = -1;
            BindEmployeeDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvEmployeeList_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        objc = new cCommon();

        try
        {
            DropDownList ddl = ((DropDownList)gvEmployeeList.Rows[e.RowIndex].FindControl("ddlUserRole"));
            TextBox txtUsername = (TextBox)gvEmployeeList.Rows[e.RowIndex].FindControl("txtLoginname");
            TextBox txtPassword = (TextBox)gvEmployeeList.Rows[e.RowIndex].FindControl("txtPassword");

            if (ddl.SelectedIndex != 0 && txtUsername.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                objCommon.EmployeeID = Convert.ToInt32(gvEmployeeList.DataKeys[e.RowIndex].Values[0].ToString());
                objCommon.UserTypeID = Convert.ToInt32(ddl.SelectedValue);
                objCommon.UserName = txtUsername.Text;
                objCommon.Password = txtPassword.Text;

                ds = objCommon.UpdateUserLoginDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Updated Successfully');", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "InfoMessage('InforMation!','Already exists Login And password');", true);

                gvEmployeeList.EditIndex = -1;
                BindEmployeeDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Field Required!');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                e.Row.Cells[8].ToolTip = "Edit";
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (e.Row.Cells.GetCellIndex(cell) == 10)
                        {
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[10].Controls[0])).ToolTip = "Update";
                            ((System.Web.UI.LiteralControl)(e.Row.Cells[10].Controls[1])).Text = "&nbsp;";
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[10].Controls[2])).ToolTip = "Close";
                        }
                    }
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

    private void BindEmployeeDetails()
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommon.EmployeeID = Convert.ToInt32(_objSess.employeeid);
            ds = objCommon.getEmployeeLoginDetails();
            ViewState["UserRole"] = ds.Tables[1];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEmployeeList.DataSource = ds.Tables[0];
                gvEmployeeList.DataBind();
            }
            else
            {
                gvEmployeeList.DataSource = "";
                gvEmployeeList.DataBind();
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
        if (gvEmployeeList.Rows.Count > 0)
            gvEmployeeList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}