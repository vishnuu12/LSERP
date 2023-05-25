using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RawMaterialInspectionReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cQCTestReports objQCT;

    #endregion

    #region"Page Init Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet ds = new DataSet();
                objc.GetCustomerNameDetailsByQCReport(ddlCustomerName);
                ds = objc.GetRFPNoDetailsByQCReport(ddlRFPNo);

                ViewState["RFP"] = ds.Tables[0];

                ShowHideControls("add");
                // bindLPIReportDetails();
            }
            if (target == "deleteVERID")
            {
                objQCT = new cQCTestReports();
                DataSet ds = objQCT.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteRawMaterialInspectionReportDetailsByRIRHID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Deleted successfully');", true);
                    BindRawMaterialInspectionReportDetails();
                }
            }
            if (target == "Print")
            {
                string RIRHID = arg.ToString();
                var page = HttpContext.Current.CurrentHandler as Page;
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "RawMaterialInspectionReportPrint.aspx?RIRHID=" + RIRHID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["RFP"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlRFPNo.DataSource = dt;

            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();

            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            ddlCustomerName.SelectedIndex = 0;
            if (ddlRFPNo.SelectedIndex > 0)
            {
                cCommon objc = new cCommon();
                objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objc.GetCustomerNameDetailsByRFPHID();
                ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

                BindItemDetailsByRFPHID();
            }
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                BindPartDetailsByRFPDID();
                BindRawMaterialInspectionReportDetails();
                ShowHideControls("add,view,input");
            }
            else
            {
                ShowHideControls("add,view");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveRawMIR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        DataTable dt;
        try
        {
            objQCT.RIRHID = hdnRIRHID.Value;
            objQCT.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQCT.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.ToString());
            objQCT.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQCT.BSLNo = txtBSLNo.Text;
            objQCT.QTY = txtQTY.Text;
            objQCT.Remarks = txtRemarks.Text;
            objQCT.CreatedBy = Convert.ToInt32(objSession.employeeid);

            DataRow dr;
            dt = new DataTable();

            dt.Columns.Add("MPID");
            dt.Columns.Add("MRNNo");
            dt.Columns.Add("MatTCNo");
            dt.Columns.Add("PlateNo");

            foreach (GridViewRow row in gvPartDetails.Rows)
            {
                TextBox txtMRNNo = (TextBox)row.FindControl("txtMRNNo");
                TextBox txtMatTCNo = (TextBox)row.FindControl("txtMatTCNo");
                TextBox txtPlateNo = (TextBox)row.FindControl("txtheatplateNo");

                dr = dt.NewRow();
                dr["MPID"] = gvPartDetails.DataKeys[row.RowIndex].Values[0].ToString();
                dr["MRNNo"] = txtMRNNo.Text;
                dr["MatTCNo"] = txtMatTCNo.Text;
                dr["PlateNo"] = txtPlateNo.Text;
                dt.Rows.Add(dr);
            }
            objQCT.dt = dt;

            ds = objQCT.SaveRawMaterialInspectionReport();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Raw Material Inspection Report Saved successfully');", true);
                BindRawMaterialInspectionReportDetails();
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RawMIR Report Updated successfully');", true);
                BindRawMaterialInspectionReportDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnRIRHID.Value = "0";
        txtTestDate.Text = "";
        txtBSLNo.Text = "";
        txtQTY.Text = "";
        txtRemarks.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "clearFields();", true);
    }

    #endregion

    #region"GridView Events"
    protected void gvRawMIRHeader_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds;
        objQCT = new cQCTestReports();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnRIRHID.Value = gvRawMIRHeader.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "EditRMI")
            {
                ds = new DataSet();

                objQCT.RIRHID = hdnRIRHID.Value;

                ds = objQCT.GetRawMaterialInspectionReportByRIRHID();

                hdnRIRHID.Value = ds.Tables[0].Rows[0]["RIRHID"].ToString();
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString();
                txtTestDate.Text = ds.Tables[0].Rows[0]["TestDate"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtQTY.Text = ds.Tables[0].Rows[0]["QTY"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

                gvPartDetails.DataSource = ds.Tables[1];
                gvPartDetails.DataBind();

            }

            else if (e.CommandName == "PDFRMI")
            {

            }

            ShowHideControls("add,view,input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindRawMaterialInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            objQCT.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQCT.GetRawMaterialInspectionReportDetailsByRFPHID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRawMIRHeader.DataSource = ds.Tables[0];
                gvRawMIRHeader.DataBind();
            }
            else
            {
                gvRawMIRHeader.DataSource = "";
                gvRawMIRHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            objQCT.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            ds = objQCT.GetPartDetailsByRFPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartDetails.DataSource = ds.Tables[0];
                gvPartDetails.DataBind();
            }
            else
            {
                gvPartDetails.DataSource = "";
                gvPartDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindItemDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objc.GetItemNameDetailsByRFPHIDQCReport(ddlItemName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divRawMaterialInspection.Visible = divAdd.Visible = divOutPut.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "view":
                        divOutPut.Visible = true;
                        break;
                    case "input":
                        divRawMaterialInspection.Visible = true;
                        break;
                    case "add":
                        divAdd.Visible = true;
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