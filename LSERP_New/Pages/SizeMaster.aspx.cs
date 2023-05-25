using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_SizeMaster : System.Web.UI.Page
{
    #region"Declaration"

    cCommon objc;
    cMaterials objMat;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (IsPostBack == false)
            {
                bindSizeDetails();
                ShowHideControls("view");
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objMat = new cMaterials();

                    objMat.SID = Convert.ToInt32(arg);
                    DataSet ds = objMat.DeleteItemSizeDetailsBySID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Item Size Deleted successfully');", true);

                    bindSizeDetails();
                    ShowHideControls("view");
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("add");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("view");
        ClearValues();
        gvSizeMaster.UseAccessibleHeader = true;
        gvSizeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        objMat = new cMaterials();
        try
        {
            if (objc.Validate(divInput))
            {
                objMat.SID = Convert.ToInt32(hdnSID.Value);
                objMat.ItemSize = txtSize.Text;
                objMat.Description = txtDescription.Text;


                ds = objMat.SaveItemSizeDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Item Size Saved Successfully');hideLoader();", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Item Size Updated Successfully');hideLoader();", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Item Size Already Exists');hideLoader();", true);

                ShowHideControls("view");
                ClearValues();
                bindSizeDetails();
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "ErrorMessage('Error','Something Went Wrong');hideLoader();", true);
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"gridView Events"

    protected void gvSizeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnSID.Value = gvSizeMaster.DataKeys[index].Values[0].ToString();
            objMat.SID = Convert.ToInt32(hdnSID.Value);
            ds = objMat.GetItemSizeDetailsBySID();

            txtSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    private void bindSizeDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetItemSizeDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSizeMaster.DataSource = ds.Tables[0];
                gvSizeMaster.DataBind();
                gvSizeMaster.UseAccessibleHeader = true;
                gvSizeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvSizeMaster.DataSource = "";
                gvSizeMaster.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void ShowHideControls(string mode)
    {
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "add":
                    divInput.Visible = true;
                    txtSize.Focus();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txtSize.Focus();
                    break;
                case "view":
                    divAdd.Visible = divOutput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        hdnSID.Value = "0";
        txtSize.Text = "";
        txtDescription.Text = "";
    }

    #endregion
}