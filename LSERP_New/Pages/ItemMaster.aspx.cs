using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;


public partial class Pages_ItemMaster : System.Web.UI.Page
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
                bindItemDetails();
                ShowHideControls("view");
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objMat = new cMaterials();

                    objMat.ItemID = Convert.ToInt32(arg);
                    DataSet ds = objMat.DeleteItemDetailsByItemID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Item Name Deleted successfully');", true);

                    bindItemDetails();
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
        gvItemDetails.UseAccessibleHeader = true;
        gvItemDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                objMat.ItemID = Convert.ToInt32(hdnItemID.Value);
                objMat.HSNCode = txtHSNCode.Text;
                objMat.ItemName = txtItemName.Text;
                objMat.StandardItemFlag = rblStandardItem.SelectedValue;

                ds = objMat.SaveItemDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Item Name Saved Successfully');hideLoader();", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Item Name Updated Successfully');hideLoader();", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Item Name Already Exists');hideLoader();", true);

                ShowHideControls("view");
                ClearValues();
                bindItemDetails();
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

    protected void gvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnItemID.Value = gvItemDetails.DataKeys[index].Values[0].ToString();
            objMat.ItemID = Convert.ToInt32(hdnItemID.Value);
            ds = objMat.getItemDetailsByItemID();

            txtHSNCode.Text = ds.Tables[0].Rows[0]["HSNCode"].ToString();
            txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();

            rblStandardItem.SelectedValue = ds.Tables[0].Rows[0]["ItemFlag"].ToString() == "" ? "N" : ds.Tables[0].Rows[0]["ItemFlag"].ToString();

            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    private void bindItemDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetItemDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItemDetails.DataSource = ds.Tables[0];
                gvItemDetails.DataBind();
                gvItemDetails.UseAccessibleHeader = true;
                gvItemDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvItemDetails.DataSource = "";
                gvItemDetails.DataBind();
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
                    txtHSNCode.Focus();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txtHSNCode.Focus();
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
        hdnItemID.Value = "0";
        txtHSNCode.Text = "";
        txtItemName.Text = "";
    }

    #endregion
}