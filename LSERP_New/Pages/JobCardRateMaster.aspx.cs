using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_JobCardRateMaster : System.Web.UI.Page
{
    #region "Declaration"

    cSession _objSession = new cSession();
    cProduction objP;

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            DataSet dsContractor = new DataSet();
            objP = new cProduction();

            dsContractor = objP.GetContractorDetails(ddlContractorName_AP, ddlContractorTeamMemberName_AP, null);

            ViewState["ContractorName"] = dsContractor.Tables[0];
            ViewState["ContractorTeamName"] = dsContractor.Tables[1];
            BindJobCardRateDetails();
            ShowHideControls("add,view");
        }
        else
        {
            if (target == "deletegvrow")
            {
                objP = new cProduction();
                DataSet ds = new DataSet();
                objP.RCMID = Convert.ToInt32(arg.ToString());

                ds = objP.DeleteJobCardRateDetailsbyRCMID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Rate Deleted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);

                BindJobCardRateDetails();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
            }
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlContractorName_J_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindContractorNameChanged(ddlContractorName_AP, ddlContractorTeamMemberName_AP);
    }

    protected void ddlContractorTeamMemberName_J_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindContractorTeamnameChanged(ddlContractorName_AP, ddlContractorTeamMemberName_AP);
    }

    #endregion

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RCMID = Convert.ToInt32(hdnRCMID.Value);
            objP.JCPNMID = Convert.ToInt32(ddlJobCardName.SelectedValue);
            objP.CTDID = Convert.ToInt32(ddlContractorTeamMemberName_AP.SelectedValue);
            objP.Rate = Convert.ToDecimal(txtrate.Text);
            objP.UserID = Convert.ToInt32(_objSession.employeeid);
            ds = objP.SaveContractorJobCardRateDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Rate Saved Succesfully');", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Rate Updated Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            BindJobCardRateDetails();
            ShowHideControls("view,add");
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        hdnRCMID.Value = "0";
    }

    protected void btnAddNew_OnClick(object sender, EventArgs e)
    {
        ShowHideControls("input");
        bindJobCardProcessNameDetails();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnRCMID.Value = "0";
        ShowHideControls("add,view");
        BindJobCardRateDetails();
    }

    #endregion

    #region"GridView Events"


    protected void gvJobCardRateDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditJobCardRate")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                hdnRCMID.Value = gvJobCardRateDetails.DataKeys[index].Values[0].ToString();

                bindJobCardProcessNameDetails();
                EditJobcardrateDetailsByRCMID();
                ShowHideControls("input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"

    private void EditJobcardrateDetailsByRCMID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RCMID = Convert.ToInt32(hdnRCMID.Value);
            ds = objP.GetContractorJobCardRateDetailsbyRCMID();

            ddlJobCardName.SelectedValue = ds.Tables[0].Rows[0]["JCPNMID"].ToString();
            ddlContractorTeamMemberName_AP.SelectedValue = ds.Tables[0].Rows[0]["CTDID"].ToString();
            BindContractorTeamnameChanged(ddlContractorName_AP, ddlContractorTeamMemberName_AP);
            txtrate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindJobCardProcessNameDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.GetJobCardProcessNameDetails(ddlJobCardName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindJobCardRateDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            ds = objP.GetContractorJobCardRateDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobCardRateDetails.DataSource = ds.Tables[0];
                gvJobCardRateDetails.DataBind();
            }
            else
            {
                gvJobCardRateDetails.DataSource = "";
                gvJobCardRateDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorNameChanged(DropDownList ddlContractorName, DropDownList ddlContractorTeamMemberName)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["ContractorTeamName"];
            if (ddlContractorName.SelectedIndex > 0)
            {
                dt.DefaultView.RowFilter = "CMID='" + ddlContractorName.SelectedValue + "'";
                dt.DefaultView.ToTable();
            }

            ddlContractorTeamMemberName.DataSource = dt;
            ddlContractorTeamMemberName.DataTextField = "Name";
            ddlContractorTeamMemberName.DataValueField = "CTDID";
            ddlContractorTeamMemberName.DataBind();
            ddlContractorTeamMemberName.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorTeamnameChanged(DropDownList ddlContractorName, DropDownList ddlContractorTeamMemberName)
    {
        try
        {
            DataTable dt;
            DataView dv;
            try
            {
                dt = (DataTable)ViewState["ContractorTeamName"];
                dv = new DataView(dt);


                if (ddlContractorTeamMemberName.SelectedIndex > 0)
                {
                    dv.RowFilter = "CTDID='" + ddlContractorTeamMemberName.SelectedValue + "'";
                    dt = dv.ToTable();
                }

                ddlContractorName.SelectedValue = dt.Rows[0]["CMID"].ToString();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        try
        {
            string[] mode = divids.Split(',');
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;

            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
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