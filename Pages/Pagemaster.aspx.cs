using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;

public partial class Pages_Pagemaster : System.Web.UI.Page
{

    #region Declaration
    cCommonMaster objcom = new cCommonMaster();
    cModuleMaster objMaster = new cModuleMaster();
    c_HR objHR;
    #endregion

    #region Page Methods

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
            return;
        else
        {
            DataSet ds = objMaster.getModuleMaster(objMaster);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlmenuname.DataSource = ds.Tables[0];
                ddlmenuname.DataValueField = "mid";
                ddlmenuname.DataTextField = "ModuleName";
                ddlmenuname.DataBind();
                ddlmenuname.Items.Insert(0, new ListItem("--Select--", "0"));
                ddladdmodule.DataSource = ds.Tables[0];
                ddladdmodule.DataValueField = "mid";
                ddladdmodule.DataTextField = "ModuleName";
                ddladdmodule.DataBind();
                ddladdmodule.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            ddlmenuname.Focus();
            // btnSave.Visible = false;
            gvModuleName.Visible = false;
            //addnew.Visible = false;
            //SaveNavigationInfo();
        }
    }

    #endregion

    #region Dropdown Methods

    protected void ddlmenuname_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmenuname.SelectedValue != "0")
            {
                BindGrid();
                //btnSave.Visible = true;
                gvModuleName.Visible = true;
            }
            else
            {
                // btnSave.Visible = false;
                gvModuleName.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Common Methods

    //public void SaveNavigationInfo()
    //{
    //    try
    //    {
    //        string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
    //        objcom = new cCommonMaster();
    //        objcom.PageName = pageName;
    //        objcom.UserID = Session["id"].ToString();
    //        objcom.PID = Convert.ToInt32(objcom.saveNavigation(objcom));
    //        Session["PID"] = objcom.PID;
    //        //if (objcom.getAssistInfocheck(Session["PID"].ToString()))
    //        //    lnkAssist.Visible = true;
    //        //else
    //        //    lnkAssist.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    public void BindGrid()
    {
        try
        {
            DataSet ds = new DataSet();
            objcom.PageMID = Convert.ToInt32(ddlmenuname.SelectedValue);
            ds = objcom.GetPagesWithModule(objcom);
            ViewState["CurrentTable"] = ds.Tables[0];
            ViewState["Designation"] = ds.Tables[1];
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

    protected void gvModuleName_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtDesignationList;
        try
        {
            //Set the edit index.
            gvModuleName.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            //Label lbl = (Label)gvModuleName.Rows[gvModuleName.EditIndex].FindControl("lblpageReference");   
            BindGrid();

            ListBox liDesignation = new ListBox();
            liDesignation = (ListBox)gvModuleName.Rows[gvModuleName.EditIndex].FindControl("liDesignation");
            dtDesignationList = (DataTable)ViewState["Designation"];
            liDesignation.DataSource = dtDesignationList;
            liDesignation.DataTextField = "UserType";
            liDesignation.DataValueField = "UserTypeID";
            liDesignation.DataBind();
            liDesignation.Items.Insert(0, new ListItem("-- Select User Name --", "0"));

            string lblDesignation = gvModuleName.DataKeys[gvModuleName.EditIndex].Values[3].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide", "multiselect('" + lblDesignation + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvModuleName_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvModuleName.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvModuleName_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DataSet ds = new DataSet();
        string DesignationIDs = "";
        try
        {
            //if (ValidateModule())
            //{
            ListBox liDesignation = ((ListBox)gvModuleName.Rows[e.RowIndex].FindControl("liDesignation"));
            TextBox txtpageReference_E = ((TextBox)gvModuleName.Rows[e.RowIndex].FindControl("txtpageReference_E"));
            TextBox txtDisplayName = ((TextBox)gvModuleName.Rows[e.RowIndex].FindControl("txtDisplayName"));
            CheckBox chkModules = (CheckBox)gvModuleName.Rows[e.RowIndex].FindControl("chkModules");
            TextBox txtDisplayOrder = (TextBox)gvModuleName.Rows[e.RowIndex].FindControl("txtDisplayOrder");
            Label lblPageName = (Label)gvModuleName.Rows[e.RowIndex].FindControl("lblpageName");

            if (txtDisplayOrder.Text != "0" && Convert.ToInt32(txtDisplayOrder.Text) <= gvModuleName.Rows.Count)
            {
                if (txtpageReference_E.Text != "" && liDesignation.SelectedIndex != 0 && txtDisplayName.Text != "" && txtDisplayOrder.Text != "")
                {
                    foreach (ListItem li in liDesignation.Items)
                    {
                        if (li.Selected)
                        {
                            if (DesignationIDs == "")
                                DesignationIDs = li.Value;
                            else
                                DesignationIDs = DesignationIDs + ',' + li.Value;
                        }
                    }

                    objMaster.pageDisplay = txtDisplayName.Text;
                    objMaster.pageReference = txtpageReference_E.Text;
                    objMaster.pageMID = Convert.ToInt32(ddlmenuname.SelectedValue);
                    objMaster.pageCheck = (chkModules.Checked) ? 1 : 0;

                    objMaster.pageSequence = (chkModules.Checked) ? Convert.ToInt32(txtDisplayOrder.Text) : 0;
                    objMaster.UserTypeIDs = DesignationIDs;
                    objMaster.PageName = lblPageName.Text;

                    objMaster.UpdatePage(objMaster);

                    gvModuleName.EditIndex = -1;
                    BindGrid();

                    if (objMaster.updateresult.Count > 0)
                    {
                        string updaterslt = string.Join(",", objMaster.updateresult);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success Message", "ErrorMessage('Error','" + updaterslt + " Pagereference name already exist. Please Rename it and try again. Remaining pages assigned successfully');", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success Message", "SuccessMessage('Success','Page assigned successfully');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "ErrorMessage('Error','Field Required')", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error Message", "ErrorMessage('Error','In display order Zero not allowed or display order less than module count');", true);
        }
        //else
        //  

        // }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Button Methods

    protected void lnkAssist_Click(object sender, EventArgs e)
    {
        //ucMessage.ShowAssist(Session["PID"].ToString());
    }
    protected void btnSaveadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (isvalid())
            {
                objMaster = new cModuleMaster();
                objMaster.PageName = txtPageName.Text;
                objMaster.pageDisplay = txtDisplayName.Text;
                objMaster.pageReference = txtPageReference.Text;
                objMaster.pageMID = Convert.ToInt32(ddladdmodule.SelectedValue);
                objMaster.pageSequence = Convert.ToInt32(txtDisplaySeq.Text);
                objMaster.savePage(objMaster);
                if (objMaster.returnvalue == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Page Reference Name Already Existed','success');", true);
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success!','Page Saved Successfully','success');", true);
                    ddlmenuname.Visible = true;
                    //addnew.Visible = false;
                    //  btnSave.Visible = false;
                    btnAddNew.Style.Remove("margin-left");
                }
            }
        }
        catch (Exception ec) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Some Technical Error occured','Error');", true); }
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //if (ValidateModule())
    //        //{
    //        //    foreach (GridViewRow row in gvModuleName.Rows)
    //        //    {
    //        //CheckBox chkModules = (CheckBox)row.FindControl("chkModules");
    //        //TextBox txtDisplayOrder = (TextBox)row.FindControl("txtDisplayOrder");
    //        //objMaster.pageMID = Convert.ToInt32(ddlmenuname.SelectedValue);
    //        //objMaster.pageCheck = (chkModules.Checked) ? 1 : 0;
    //        //objMaster.pageSequence = (chkModules.Checked) ? Convert.ToInt32(txtDisplayOrder.Text) : 0;
    //        //Label lblPageName = (Label)row.FindControl("lblpageName");
    //        //objMaster.PageName = lblPageName.Text;
    //        //Label lblpageDisplay = (Label)row.FindControl("lblpageDisplay");
    //        //objMaster.pageDisplay = lblpageDisplay.Text;
    //        //Label lblpageref = (Label)row.FindControl("lblpageReference");
    //        //objMaster.pageReference = lblpageref.Text;
    //        //TextBox txtpageReference = (TextBox)row.FindControl("txtpageReference");
    //        //if (txtpageReference.Visible == true)
    //        //{
    //        //    objMaster.pageCheck = 0;
    //        //    objMaster.UpdatePage(objMaster);
    //        //    objMaster.pageCheck = 1;
    //        //    objMaster.pageReference = txtpageReference.Text;
    //        //}
    //        //  objMaster.UpdatePage(objMaster);
    //        //}
    //        // BindGrid();
    //        //if (objMaster.updateresult.Count > 0)
    //        //{
    //        //    string updaterslt = string.Join(",", objMaster.updateresult);
    //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success Message", "ErrorMessage('Error','" + updaterslt + " Pagereference name already exist. Please Rename it and try again. Remaining pages assigned successfully');", true);
    //        //}
    //        //else ScriptManager.RegisterStartupScript(this, this.GetType(), "Success Message", "SuccessMessage('Success','Page assigned successfully');", true);
    //        // }
    //        // else
    //        //   ScriptManager.RegisterStartupScript(this, this.GetType(), "Error Message", "ErrorMessage('Error','In display order Zero not allowed or display order less than module count');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region Validations

    //public bool ValidateModule()
    //{
    //    bool isvalid = true;

    //    foreach (GridViewRow row in gvModuleName.Rows)
    //    {
    //        CheckBox chkModules = (CheckBox)row.FindControl("chkModules");
    //        TextBox txtDisplayOrder = (TextBox)row.FindControl("txtDisplayOrder");
    //        if (chkModules.Checked)
    //        {
    //            if ((txtDisplayOrder.Text == "0") || (txtDisplayOrder.Text == ""))
    //                isvalid = false;
    //            else if (Convert.ToInt32(txtDisplayOrder.Text) > gvModuleName.Rows.Count)
    //                isvalid = false;
    //        }
    //    }

    //    return isvalid;
    //}
    protected bool isvalid()
    {
        bool valid = true;
        //if (txtModuleName.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Error!','Please Enter Module Name','error');", true);
        //    valid = false;
        //}
        return valid;
    }
    #endregion

    #region CheckBox Methods

    //protected void chkModule_CheckChanged(object sender, EventArgs e)
    //{
    //    CheckBox chkModules = (CheckBox)sender;
    //    GridViewRow row = (GridViewRow)chkModules.NamingContainer;
    //    TextBox txtDisplayOrder = (TextBox)row.FindControl("txtDisplayOrder");
    //    Label lblpgref = (Label)row.FindControl("lblpageReference");
    //    TextBox txtpageReference = (TextBox)row.FindControl("txtpageReference");
    //    if (chkModules.Checked == true)
    //    {
    //        txtDisplayOrder.Visible = true;
    //        txtpageReference.Text = lblpgref.Text;
    //        txtpageReference.Visible = true;
    //        lblpgref.Visible = false;
    //    }
    //    else if (chkModules.Checked == false)
    //    {
    //        txtDisplayOrder.Visible = false;
    //        txtpageReference.Visible = false;
    //        lblpgref.Visible = true;
    //    }
    //}

    #endregion

    #region load_complete

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvModuleName.Rows.Count > 0)
            gvModuleName.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}