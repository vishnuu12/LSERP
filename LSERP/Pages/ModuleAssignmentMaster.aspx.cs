using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Pages_ModuleAssignmentMaster : System.Web.UI.Page
{

    #region Declaration
    cCommonMaster objcom = new cCommonMaster();

    #endregion

    #region Page Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        else
        {            
            DataSet ds = objcom.getuserRoleMaster();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUserType.DataSource = ds.Tables[0];
                ddlUserType.DataValueField = "UserTypeID";
                ddlUserType.DataTextField = "UserType";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            ddlUserType.Focus();
            btnSave.Visible = false;
            gvModuleName.Visible = false;
            //SaveNavigationInfo();
        }
    }

    #endregion

    #region Dropdown Methods
    protected void ddlUserType_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUserType.SelectedValue != "0")
            {
                BindGrid();
                btnSave.Visible = true;
                gvModuleName.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
                gvModuleName.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "HideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Common Methods

    public void SaveNavigationInfo()
    {
        try
        {
            string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
            objcom = new cCommonMaster();
            objcom.PageName = pageName;
            objcom.UserID = Session["id"].ToString();
            objcom.PID = Convert.ToInt32(objcom.saveNavigation(objcom));
            Session["PID"] = objcom.PID;
            //if (objcom.getAssistInfocheck(Session["PID"].ToString()))
            //    lnkAssist.Visible = true;
            //else
            //    lnkAssist.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void BindGrid()
    {
        try
        {
            DataSet ds = new DataSet();
            objcom.UserTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
            ds = objcom.GetUserModuleWithRoles(objcom);

            gvModuleName.DataSource = ds.Tables[0];
            gvModuleName.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Gridview Methods

    protected void gvModuleName_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Access = gvModuleName.DataKeys[e.Row.RowIndex].Values[1].ToString();
            CheckBox chkModules = (CheckBox)e.Row.FindControl("chkModules");
            TextBox txtDisplayOrder = (TextBox)e.Row.FindControl("txtDisplayOrder");

            if (Access == "1")
            {
                chkModules.Checked = true;
                txtDisplayOrder.Visible = true;
            }
            else if (Access == "0")
            {
                chkModules.Checked = false;
                txtDisplayOrder.Visible = false;
            }
        }
    }

    #endregion

    #region Button Methods

    protected void lnkAssist_Click(object sender, EventArgs e)
    {
        //ucMessage.ShowAssist(Session["PID"].ToString());
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateModule())
            {
                foreach (GridViewRow row in gvModuleName.Rows)
                {
                    CheckBox chkModules = (CheckBox)row.FindControl("chkModules");
                    TextBox txtDisplayOrder = (TextBox)row.FindControl("txtDisplayOrder");
                    objcom.UserTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
                    objcom.MID = Convert.ToInt32(gvModuleName.DataKeys[row.RowIndex].Values[0].ToString());
                    objcom.Check = (chkModules.Checked) ? 1 : 0;
                    objcom.Count = (chkModules.Checked) ? Convert.ToInt32(txtDisplayOrder.Text) : 0;
                    objcom.SaveUserModules(objcom);
                }
                BindGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success Message", "SuccessMessage('Success','Module assigned successfully');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error Message", "ErrorMessage('Error','In display order Zero not allowed or display order less than module count');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Validations

    public bool ValidateModule()
    {
        bool isvalid = true;

        foreach (GridViewRow row in gvModuleName.Rows)
        {
            CheckBox chkModules = (CheckBox)row.FindControl("chkModules");
            TextBox txtDisplayOrder = (TextBox)row.FindControl("txtDisplayOrder");
            if (chkModules.Checked)
            {
                if ((txtDisplayOrder.Text == "0") || (txtDisplayOrder.Text == ""))
                    isvalid = false;
                else if (Convert.ToInt32(txtDisplayOrder.Text) > gvModuleName.Rows.Count)
                    isvalid = false;
            }
        }

        return isvalid;
    }

    #endregion

    #region CheckBox Methods

    protected void chkModule_CheckChanged(object sender, EventArgs e)
    {
        CheckBox chkModules = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkModules.NamingContainer;
        TextBox txtDisplayOrder = (TextBox)row.FindControl("txtDisplayOrder");

        if (chkModules.Checked == true)
            txtDisplayOrder.Visible = true;
        else if (chkModules.Checked == false)
            txtDisplayOrder.Visible = false;
    }

    #endregion
}