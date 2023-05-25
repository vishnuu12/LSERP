using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_QualityPlanningHeader : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cQuality objQ;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (!IsPostBack)
            {
                objMat = new cMaterials();
                objMat.GetMaterialNameDetails(ddlMaterialName);
                ShowHideControls("add");
            }
            if (target == "deletegvrow")
            {
                objQ = new cQuality();
                objQ.QPHID = Convert.ToInt32(arg);
                DataSet ds = objQ.DeleteQPHeader();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);

                ddlMaterialName_SelectIndexChanged(null, null);
            }
            if (target == "deleteQPDID")
            {
                objQ = new cQuality();
                objQ.QPDID = Convert.ToInt32(arg);
                DataSet ds = objQ.DeleteQPDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');ShowAddPopUp();", true);

                BindQPDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMaterialName_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaterialName.SelectedIndex > 0)
            {
                BindQPHeaderDetails();
                ShowHideControls("add,addnew,view");
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void rblProcess_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblProcess.SelectedValue == "PA")
            {
                ShowHideControls("add");
                gvQualityPlanningHeader.Columns[3].Visible = true;
                gvQualityPlanningHeader.Columns[4].Visible = true;
            }
            else
            {
                ddlMaterialName.SelectedIndex = 0;
                ShowHideControls("view");
                gvQualityPlanningHeader.Columns[3].Visible = false;
                gvQualityPlanningHeader.Columns[4].Visible = false;
                BindQPHeaderDetails();
            }

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
        objQ = new cQuality();
        objc = new cCommon();
        try
        {
            if (objc.Validate(divInput) || objc.Validate(divQPDetails))
            {
                LinkButton btn = (LinkButton)sender;
                if (btn.ID == "btnSaveQPDetails")
                {
                    objQ.QPDID = Convert.ToInt32(hdnQPDID.Value);
                    objQ.QPHID = Convert.ToInt32(hdnQPHID.Value);
                    objQ.Stage = txtStage.Text;
                    objQ.TypeCheck = txtTypeCheck.Text;
                    ds = objQ.SaveQPDetails();
                    BindQPDetails();
                }
                else
                {
                    objQ.QPHID = Convert.ToInt32(hdnQPHID.Value);
                    objQ.Sequence = txtSequence.Text;
                    objQ.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                    ds = objQ.SaveQualityPlanningHeaderDetails();
                    ddlMaterialName_SelectIndexChanged(null, null);
                }
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Quality Planning Saved Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Quality Planning Updated Successfully');", true);
            }
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
            ShowHideControls("input");
            hdnQPHID.Value = "0";
            txtSequence.Text = "";
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
            LinkButton btn = (LinkButton)sender;

            if (btn.ID == "btnCancelQPDetails")
            {
                txtStage.Text = "";
                txtTypeCheck.Text = "";
            }
            else
                ShowHideControls("add,addnew,view");

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"GridView Events"

    protected void gvQualityPlanningHeader_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataView dv;
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string QPHID = gvQualityPlanningHeader.DataKeys[index].Values[0].ToString();

            Label lblSequence = (Label)gvQualityPlanningHeader.Rows[index].FindControl("lblSequence");

            if (e.CommandName == "EditQuality")
            {
                dt = (DataTable)ViewState["QPHeader"];
                hdnQPHID.Value = QPHID.ToString();

                dv = new DataView(dt);
                dt.DefaultView.RowFilter = "QPHID='" + QPHID + "'";
                dt = dv.ToTable();

                txtSequence.Text = dt.DefaultView.ToTable().Rows[0]["Sequence"].ToString();

                ShowHideControls("input");
            }
            if (e.CommandName == "Add")
            {
                lblProcessName.Text = lblSequence.Text;
                hdnQPHID.Value = QPHID;
                BindQPDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowAddPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvQPDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataView dv;
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string QPDID = gvQualityPlanningHeader.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "EditQPDetails")
            {
                dt = (DataTable)ViewState["QPDetails"];
                hdnQPDID.Value = QPDID.ToString();

                dv = new DataView(dt);
                dt.DefaultView.RowFilter = "QPDID='" + QPDID + "'";
                dt = dv.ToTable();

                txtStage.Text = dt.DefaultView.ToTable().Rows[0]["Stage"].ToString();
                txtTypeCheck.Text = dt.DefaultView.ToTable().Rows[0]["TypeCheck"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowAddPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"Common Methods"

    private void BindQPDetails()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.QPHID = Convert.ToInt32(hdnQPHID.Value);
            ds = objQ.GetQPDetails();

            if (ds.Tables[0].Rows.Count > 0)
                gvQPDetails.DataSource = ds.Tables[0];
            else
                gvQPDetails.DataSource = "";

            gvQPDetails.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindQPHeaderDetails()
    {
        objQ = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQ.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
            ds = objQ.GetQualityPlanningHeaderDetailsByMID();
            ViewState["QPHeader"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
                gvQualityPlanningHeader.DataSource = ds.Tables[0];
            else
                gvQualityPlanningHeader.DataSource = "";

            gvQualityPlanningHeader.DataBind();
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

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvQualityPlanningHeader.Rows.Count > 0)
            gvQualityPlanningHeader.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvQPDetails.Rows.Count > 0)
            gvQPDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}