using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralWorkOrderDC : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    GeneralWorkOrderIndentApproval gwoia;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            // Do Something

           
            generalWorkOrderDcPendingReport();
            ShowHideControls("view");
        }
        //if (target == "BindDCData")
        //{
        //    generalWorkOrderDcData(Convert.ToInt32(arg.ToString()));
        //    ShowHideControls("view");
        //}
        if (target == "PrintDC")
        { 
        } 
        if (target == "PrintPO")
        { 
        }

    }

    #endregion

    #region"Radio Events"

    protected void rblGWPONoChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            gwoia = new GeneralWorkOrderIndentApproval();
            if (rblGWPONoChange.SelectedIndex == 0)
            {
               
                ds = gwoia.GetgeneralWorkOrderDcPendingReport(rblGWPONoChange.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvGeneralWorkOrderDCPending.DataSource = ds.Tables[0];
                    gvGeneralWorkOrderDCPending.DataBind();
                    ShowHideControls("view");
                    //ShowHideControls("DCNot");
                }
                else
                {
                    gvGeneralWorkOrderDCPending.DataSource = "";
                    gvGeneralWorkOrderDCPending.DataBind();
                    ShowHideControls("view");
                    //ShowHideControls("DCNot");
                }
            }
            else if (rblGWPONoChange.SelectedIndex == 1)
            {
                ds = gwoia.GetgeneralWorkOrderDcPendingReport(rblGWPONoChange.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvGeneralWorkOrderDCPending.DataSource = ds.Tables[0];
                    gvGeneralWorkOrderDCPending.DataBind();
                    ShowHideControls("view");
                    //ShowHideControls("DCNot");
                }
                else
                {
                    gvGeneralWorkOrderDCPending.DataSource = "";
                    gvGeneralWorkOrderDCPending.DataBind();
                    ShowHideControls("view");
                    //ShowHideControls("DCNot");
                }
            }
            else if (rblGWPONoChange.SelectedIndex == 2)
            {
                ds = gwoia.GetgeneralWorkOrderDcPendingReport(rblGWPONoChange.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDcData.DataSource = ds.Tables[0];
                    gvDcData.DataBind();
                    ShowHideControls("dcview");
                }
                else
                {
                    gvDcData.DataSource = "";
                    gvDcData.DataBind();
                    ShowHideControls("dcview");
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


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("radio");
        ShowHideControls("view");
        hdnGWPOID.Value = "0";
        clearValues();
    }

    protected void btnSaveDC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;
            string GWOIDA = "";
            gwoia.GWOPOID = Convert.ToInt32(hdnGWPOID.Value);
            gwoia.DCDate = txtDCDate.Text;
            gwoia.FormJJNo = txtFormJJNNo.Text;
            gwoia.TariffClassification = Convert.ToInt32(txtTarrifClassification.Text);
            gwoia.Duration = Convert.ToInt32(txtDuration.Text);
            gwoia.GWPODC = Convert.ToInt32(txtGWOIDA.Text);
            gwoia.DutyDetailsDate = txtDutyDetailsDate.Text;
            

            foreach (GridViewRow row in gvGeneralWorkOrderDCPending.Rows)
            {
                 GWOIDA = gvGeneralWorkOrderDCPending.DataKeys[row.RowIndex].Values[1].ToString(); 
            }
            ds = gwoia.SaveGeneralWorkOrderDC();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Succeessfully');", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
            ShowHideControls("view");
            clearValues();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvGeneralWorkOrderDCPending_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        try
        {
            if (e.CommandName == "btnAddNew_Click")
            {

                DataSet ds = new DataSet();
                gwoia = new GeneralWorkOrderIndentApproval();
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                hdnGWPOID.Value = gvGeneralWorkOrderDCPending.DataKeys[index].Values[0].ToString();
                gwoia.GWPOID = Convert.ToInt32(hdnGWPOID.Value);
                Label lblLocation = (Label)gvGeneralWorkOrderDCPending.Rows[index].FindControl("LocationID");
                Label GWOIDA = (Label)gvGeneralWorkOrderDCPending.Rows[index].FindControl("lblGWPOID");
                txtGWOIDA.Text = GWOIDA.Text.ToString();
                int Location = Convert.ToInt32(lblLocation.Text.ToString());
                gwoia.GetDCLocationDetails(Location, ddlLocationName);
                ShowHideControls("radiohidden");
                ShowHideControls("input");
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    protected void gvDcData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void generalWorkOrderDcPendingReport()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            gwoia = new GeneralWorkOrderIndentApproval();
            string status = "0";
            ds = gwoia.GetgeneralWorkOrderDcPendingReport(status);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrderDCPending.DataSource = ds.Tables[0];
                gvGeneralWorkOrderDCPending.DataBind();
                ViewState["gvGeneralWorkOrderDCPending"] = ds.Tables[0];
            }
            else
            {
                gvGeneralWorkOrderDCPending.DataSource = "";
                gvGeneralWorkOrderDCPending.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

  
    private void ShowHideControls(string divids)
    {
        divDcData.Visible = divInput.Visible = divOutput.Visible = divAddNew.Visible = false;
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
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "dcview":
                        divDcData.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                    case "radio":
                        divRadio.Visible = true;
                        break;
                    case "radiohidden":
                        divRadio.Visible = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void clearValues()
    {
        txtDCDate.Text = "";
        txtFormJJNNo.Text = "";
        txtDutyDetailsDate.Text = "";
        txtDuration.Text = "";
        txtTarrifClassification.Text = "";
    }

    #endregion
}