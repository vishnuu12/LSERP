using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Configuration;

public partial class Pages_GeneralWorkOrderDCJob : System.Web.UI.Page
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
            ShowHideControls("View");
            generalWorkOrderDcReport();
        }
        if (target == "ShareDC")
            ShareDC(); 
        if (target == "PrintPO")
            PrintGeneralWorkOrderPODetails(Convert.ToInt32(arg.ToString()));
        if (target == "PrintJobDC")
            PrintJobDC(Convert.ToInt32(arg.ToString()));
        //ShareDC();

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
        ShowHideControls("view");
    }

    protected void btnSaveDCformodal(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {

            foreach (GridViewRow row in gvWorkOrderDCJob.Rows)
            {
                gwoia.GWPOID = Convert.ToInt32(hdnGDCID.Value);
                gwoia.GWPODC = Convert.ToInt32(hdnGWPOID.Value);
                TextBox ItemDescription = (TextBox)row.FindControl("txtDescription");
                gwoia.ItemDescription = ItemDescription.Text;
                TextBox DCQty = (TextBox)row.FindControl("txtDCQty");
                gwoia.DCQty = Convert.ToInt32(DCQty.Text);
                ds = gwoia.SaveGeneralWorkOrderDCQty();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated") { 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
                    generalWorkOrderDcJob(gwoia.GDCID);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error');", true);

            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveDC_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.GWPOID = Convert.ToInt32(hdnGDCID.Value);
            gwoia.GWPODC = Convert.ToInt32(hdnGWPOID.Value);
            gwoia.FormJJNo = txtFormJJNNo.Text.ToString();
            gwoia.TariffClassification = Convert.ToInt32(txtTarrifClassification.Text.ToString());
            gwoia.Duration = Convert.ToInt32(txtDuration.Text.ToString());
            gwoia.DutyDetailsDate = txtDutyDetailsDate.Text.ToString();
            gwoia.DCDate = txtDCDate.Text.ToString();
            ds = gwoia.SaveGeneralWorkOrderDC();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Succeessfully');", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
            ShowHideControls("view");
            generalWorkOrderDcReport();
            clearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvGeneralDcData_RowDataCommand(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void gvGeneralDcData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        int index;

        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnGDCID.Value = gvGeneralDcData.DataKeys[index].Values[0].ToString();
            gwoia.GDCID = Convert.ToInt32(hdnGDCID.Value);
            if (e.CommandName == "EditDC")
            {

                DataTable dt = (DataTable)ViewState["gvGeneralWorkOrderDC"];
                dt.DefaultView.RowFilter = "GDCID='" + gwoia.GDCID + "'";
                hdnGDCID.Value = gwoia.GDCID.ToString();
                txtFormJJNNo.Text = dt.DefaultView.ToTable().Rows[0]["FormJJNo"].ToString();
                txtDCDate.Text = dt.DefaultView.ToTable().Rows[0]["DCDate"].ToString();
                txtTarrifClassification.Text = dt.DefaultView.ToTable().Rows[0]["TariffClassification"].ToString();
                txtDuration.Text = dt.DefaultView.ToTable().Rows[0]["Duration"].ToString();
                txtDutyDetailsDate.Text = dt.DefaultView.ToTable().Rows[0]["DutyDetailsDate"].ToString();
                ddlLocationName.DataTextField = dt.DefaultView.ToTable().Rows[0]["Location"].ToString();
                string Location = ddlLocationName.DataTextField.ToString();
                gwoia.GetDCLocation(Location, ddlLocationName);

                ShowHideControls("input");
            }
            if (e.CommandName == "AddDC")
            {
                generalWorkOrderDcJob(gwoia.GDCID);


                ShowHidePopUpControls("input");

               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "ShowDCPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderDCJob_onRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAllow = (LinkButton)e.Row.FindControl("btnAllow");

                if (dr["APDCQty"].ToString() == "1")
                    btnAllow.Text = "Un Allow";
                else
                    btnAllow.Text = "Allow";

                if (objSession.type == 1)
                    btnAllow.Visible = true;
                else
                    btnAllow.Visible = false;
                if (dr[11].ToString() == "0")
                    btnSave_DC.Visible = false;
                else
                    btnSave_DC.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWorkOrderDCJob_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Allow")
            {
                gwoia.UserID = Convert.ToInt32(objSession.employeeid);
                gwoia.GDCID = Convert.ToInt32(hdnGDCID.Value);
                ds = gwoia.UpdateAllowPermissionDcQtyByGDCID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    if (ds.Tables[0].Rows[0]["Allow"].ToString() == "1") {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Granted Succesfully');", true);
                        generalWorkOrderDcJob(gwoia.GDCID);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Permisson Denied Succesfully');", true);
                        generalWorkOrderDcJob(gwoia.GDCID);
                }
                generalWorkOrderDcReport();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }



    #endregion

    #region"Common Methods"

    private void PrintGeneralWorkOrderPODetails(int index)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            gwoia.GDCID = index;
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "GeneralWorkorderPoPrint.aspx?GDCID=" + gwoia.GDCID + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void generalWorkOrderDcReport()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            gwoia = new GeneralWorkOrderIndentApproval();
            ds = gwoia.GetgeneralWorkOrderDcReport();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralDcData.DataSource = ds.Tables[0];
                gvGeneralDcData.DataBind();
                ViewState["gvGeneralWorkOrderDC"] = ds.Tables[0];
            }
            else
            {
                gvGeneralDcData.DataSource = "";
                gvGeneralDcData.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void generalWorkOrderDcJob(int GDCID)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            int GWOIDA = GDCID;
            gwoia = new GeneralWorkOrderIndentApproval();
            ds = gwoia.GetgeneralWorkOrderDcJob(GWOIDA);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkOrderDCJob.DataSource = ds.Tables[0];
                gvWorkOrderDCJob.DataBind();
                ViewState["gvWorkOrderDCJob"] = ds.Tables[0];
            }
            else
            {
                gvWorkOrderDCJob.DataSource = "";
                gvWorkOrderDCJob.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShareDC()
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            if (gvWorkOrderDCJob.Rows.Count > 0)
            {
                gwoia.GDCID = Convert.ToInt32(hdnGDCID.Value);
                ds = gwoia.UpdateDCStatusByGDCID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','DC Shared Successfully');HideDCPopup();", true);
                    generalWorkOrderDcReport();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','DC Has No records');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void PrintJobDC(int index)
    {
        try
        {
            DataSet ds = new DataSet();
            gwoia = new GeneralWorkOrderIndentApproval();
            /* gwoia.GDCID = index;
            ds = gwoia.GetGeneralDCDetailsByGDCIDForPDF();
            //VDCID	WPOID DCDateEdit TariffClassification DutyDetailsDate	DutyDetailsDateEdit	LocationID	Location	Address
            ViewState["Address"] = ds;

            DataTable dt2;
            dt2 = (DataTable)ds.Tables[2];

            hdnAddress.Value = ds.Tables[0].Rows[0]["Address"].ToString();

            hdnTINNo.Value = dt2.Rows[0]["TINNo"].ToString();
            hdnCodeNo.Value = dt2.Rows[0]["CodeNo"].ToString();
            hdnCSTNo.Value = dt2.Rows[0]["CSTNo"].ToString();
            hdnGSTNumber.Value = dt2.Rows[0]["GSTNumber"].ToString();
            hdnCompanyName.Value = dt2.Rows[0]["CompanyName"].ToString();

            lblWONo_p.Text = ds.Tables[0].Rows[0]["Wonumber"].ToString();
            lblFormJJno_p.Text = ds.Tables[0].Rows[0]["FormJJNo"].ToString();
            lblDCno_p.Text = ds.Tables[0].Rows[0]["DCNo"].ToString();
            lblDate_p.Text = ds.Tables[0].Rows[0]["DCDate"].ToString();
            lblSuppliername_p.Text = ds.Tables[0].Rows[0]["VendorName"].ToString();
            lblSupplierAddress_p.Text = ds.Tables[0].Rows[0]["SupplierAdddress"].ToString();
            lbltarrifClassification_p.Text = ds.Tables[0].Rows[0]["TariffClassification"].ToString();
            lblExpectedDurationofProcessing_p.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
            CompanyName_P.InnerText = dt2.Rows[0]["CompanyName"].ToString();
            gvWorkOrderPOItemDetails_p.DataSource = ds.Tables[1];
            gvWorkOrderPOItemDetails_p.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "PrintJobDC();", true); */
			
			var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            gwoia.GDCID = index;
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "GeneralDCPrint.aspx?GDCID=" + gwoia.GDCID + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void ShowHideControls(string divids)
    {
        divInput.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                   
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

    private void ShowHidePopUpControls(string divids)
    {
        divInput_P.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "input":
                        divInput_P.Visible = true;
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