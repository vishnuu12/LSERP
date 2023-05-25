using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DailyActivitiesReportForSalesEnquiry : System.Web.UI.Page
{
    #region"Declarartion"

    cSession objSession = new cSession();
    cSales objsales;
    cCommon objcommon;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (!IsPostBack)
            {
                objsales = new cSales();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet ds = new DataSet();
                ds = objsales.GetDailyActivitiesSalesEnquiryDetailsByEmployeeID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, ddlCustomerName);
                ViewState["EnquiryDetails"] = ds.Tables[0];
                ViewState["CustomerDetails"] = ds.Tables[1];

                ShowHideControls("add");
            }

            if (target == "Delete")
            {
                objcommon = new cCommon();
                int PartId = Convert.ToInt32(arg);
                DataSet ds = objcommon.deleteDailyActivitiesReportDetailsDetailsByID(PartId, "LS_deleteDailyActivitiesReportDetailsByPrimaryID");
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Part Name Deleted successfully');", true);
                BindReportDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objsales = new cSales();
        DataTable dcustomr;
        DataTable denquiry;
        try
        {
            //objsales.designPendingEnquiryIDChange(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            DataView dv;
            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            try
            {
                if (ddlCustomerName.SelectedIndex > 0)
                {
                    dv = new DataView(dcustomr);
                    dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                    dcustomr = dv.ToTable();

                    ddlEnquiryNumber.DataSource = dcustomr;
                    ddlEnquiryNumber.DataTextField = "EnquiryName";
                    ddlEnquiryNumber.DataValueField = "EnquiryID";
                    ddlEnquiryNumber.DataBind();
                }
                else
                {
                    ddlEnquiryNumber.DataSource = denquiry;
                    ddlEnquiryNumber.DataTextField = "EnquiryName";
                    ddlEnquiryNumber.DataValueField = "EnquiryID";
                    ddlEnquiryNumber.DataBind();
                }

                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));
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

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objsales = new cSales();
        DataTable dcustomr;
        DataTable denquiry;
        try
        {
            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                DataView dv = new DataView(denquiry);
                dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                denquiry = dv.ToTable();
                ddlCustomerName.SelectedValue = denquiry.Rows[0]["ProspectID"].ToString();

                BindReportDetails();

                ShowHideControls("add,addnew,view,input");
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    //protected void btnAddNew_Click(object sender, EventArgs e)
    //{
    //    // ShowHideControls("input");
    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objsales = new cSales();
        DataSet ds = new DataSet();
        bool rsdate = true;
        try
        {
            objsales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;
            objsales.CurrentStatus = ddlCurentStatus.SelectedValue;
            objsales.ReScheduledSubmissiondate = rblReSchduleSubmissiondate.SelectedValue;

            if (rblReSchduleSubmissiondate.SelectedValue == "Yes")
            {
                objsales.ReScheduledDate = DateTime.ParseExact(txtreschduledate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                rsdate = true;
            }
            else
                rsdate = false;

            objsales.Remarks = txtRemarks.Text;
            objsales.UserID = Convert.ToInt32(objSession.employeeid);
            objsales.RescheduledateReason = "";
            ds = objsales.SaveDailyActivitiesSalesEnquiryDetails(rsdate);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Details Saved Succesfully');", true);
            ddlEnquiryNumber_SelectIndexChanged(null, null);
            // ShowHideControls("add,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // ShowHideControls("add,view,input");
    }

    #endregion

    #region"Common Methods"

    private void BindReportDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            objsales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;
            ds = objsales.GetSalesActivitiesEnquiryDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDailyActivitiesReport.DataSource = ds.Tables[0];
                gvDailyActivitiesReport.DataBind();
            }
            else
            {
                gvDailyActivitiesReport.DataSource = "";
                gvDailyActivitiesReport.DataBind();
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
            divAdd.Visible = divAddnew.Visible = divInput.Visible = divOutput.Visible = false;
            string[] mode = divids.Split(',');
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
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
}