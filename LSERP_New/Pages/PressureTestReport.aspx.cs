using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PressureTestReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cSales objSales;
    cMaterials objMat;
    cQCTestReports objQCT;

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
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                objc = new cCommon();
                objQCT = new cQCTestReports();
                DataSet ds = new DataSet();
                objc.GetCustomerNameDetailsByQCReport(ddlCustomerName);
                ds = objc.GetRFPNoDetailsByQCReport(ddlRFPNo);

                objQCT.GetTypeOfTest(ddlTypeOftest);
                objQCT.GetCalibrationDetails(ddlCalibrationNo1, ddlCalibrationNo2);

                ViewState["RFP"] = ds.Tables[0];
                ShowHideControls("add");
            }
            if (target == "deleteVERID")
            {
                objQCT = new cQCTestReports();
                DataSet ds = objQCT.DeleteQualityTestReportDetailsByPrimaryID("LS_DeletePressureTestReport", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Deleted successfully');", true);
                    BindPressureTestReportDetails();
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

                string Page = url.Replace(Replacevalue, "PressureTestReportPrint.aspx?PTRHID=" + RIRHID + "");

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
        cCommon objc = new cCommon();
        try
        {
            ddlCustomerName.SelectedIndex = 0;
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objc.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objc.GetCustomerNameDetailsByRFPHID();
                ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

                BindItemDetailsByRFPHID();
                BindPressureTestReportDetails();
                ShowHideControls("add,view");
            }
            else
            {
                ShowHideControls("add");
                objc.EmptyDropDownList(ddlItemName);
            }
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
                //BindPartDetailsByRFPDID();
                //BindRawMaterialInspectionReportDetails();
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

    #region "Button Events"

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        hdnPTRHID.Value = "0";
        txtTestDate.Text = "";
        txtBSLNo.Text = "";
        txtQTY.Text = "";
        ddlCalibrationNo1.SelectedIndex = 0;
        ddlCalibrationNo2.SelectedIndex = 0;
        ddlTypeOftest.SelectedIndex = 0;
        txtMedium.Text = "";
        txtHoldingTime.Text = "";
        txtApprovedTestProcedureNo.Text = "";
        txttestpressure.Text = "";
        txtResult.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "clearFields();", true);
    }

    protected void btnSavePTReport_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            objQCT.PTRHID = hdnPTRHID.Value;
            objQCT.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQCT.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.ToString());
            objQCT.TestDate = DateTime.ParseExact(txtTestDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQCT.BSLNo = txtBSLNo.Text;
            objQCT.QTY = txtQTY.Text;
            objQCT.TestProcedureNo = txtApprovedTestProcedureNo.Text;
            objQCT.TPTID = Convert.ToInt32(ddlTypeOftest.SelectedValue);
            objQCT.Medium = txtMedium.Text;
            objQCT.TestPressure = txttestpressure.Text;
            objQCT.HoldingTime = txtHoldingTime.Text;
            objQCT.TestTemprature = "";
            objQCT.Result = txtResult.Text;
            objQCT.CalibrationID1 = Convert.ToInt32(ddlCalibrationNo1.SelectedValue);
            objQCT.CalibrationID2 = Convert.ToInt32(ddlCalibrationNo2.SelectedValue);
            objQCT.CreatedBy = Convert.ToInt32(objSession.employeeid);

            ds = objQCT.SavePressureTestReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Pressure Test Report Saved successfully');", true);
                BindPressureTestReportDetails();
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Pressure Test Report Updated successfully');", true);
                BindPressureTestReportDetails();
            }

            btnCancel_Click(null, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPressureTestReport_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        objQCT = new cQCTestReports();
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PTRHID = gvPressureTestReport.DataKeys[index].Values[0].ToString();
            objQCT.PTRHID = PTRHID;

            ds = objQCT.GetPressureTestReportDetailsByPTRHID();

            if (e.CommandName == "EditPT")
            {
                hdnPTRHID.Value = ds.Tables[0].Rows[0]["PTRHID"].ToString();
                ddlRFPNo.SelectedValue = ds.Tables[0].Rows[0]["RFPHID"].ToString();
                ddlItemName.SelectedValue = ds.Tables[0].Rows[0]["RFPDID"].ToString();

                ddlTypeOftest.SelectedValue = ds.Tables[0].Rows[0]["TPTID"].ToString();
                txtMedium.Text = ds.Tables[0].Rows[0]["Medium"].ToString();
                txttestpressure.Text = ds.Tables[0].Rows[0]["TestPressure"].ToString();
                txtHoldingTime.Text = ds.Tables[0].Rows[0]["HoldingTime"].ToString();
                ddlCalibrationNo1.SelectedValue = ds.Tables[0].Rows[0]["CalibrationID1"].ToString();
                ddlCalibrationNo2.SelectedValue = ds.Tables[0].Rows[0]["CalibrationID2"].ToString();
                txtQTY.Text = ds.Tables[0].Rows[0]["QTY"].ToString();
                txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                txtApprovedTestProcedureNo.Text = ds.Tables[0].Rows[0]["TestProcedureNo"].ToString();
                txtTestDate.Text = ds.Tables[0].Rows[0]["TestDateEdit"].ToString();
                txtResult.Text = ds.Tables[0].Rows[0]["Result"].ToString();
            }

            if (e.CommandName.ToString() == "PdfPTR")
            {
                lblDate_p.Text = ds.Tables[0].Rows[0]["ReportDateView"].ToString();
                lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
                lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblProject_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                lblITPNo_p.Text = ds.Tables[0].Rows[0]["ITPNo"].ToString();
                lblcustomername_p.Text = ds.Tables[0].Rows[0]["Customername"].ToString();
                lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
                //lbl.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                //txtDRGRefNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
                //txtBSLNo.Text = ds.Tables[0].Rows[0]["BSLNo"].ToString();
                //txtQTY.Text = ds.Tables[0].Rows[0]["AcceptedQty"].ToString();
                lblApprovedTestProcedureNo_p.Text = ds.Tables[0].Rows[0]["ApprovedTestProcedureNo"].ToString();
                lblTypeOfTest_p.Text = lblTypeOfTestHeader_p.Text = ds.Tables[0].Rows[0]["TypeOfTest"].ToString();
                lblMedium_p.Text = ds.Tables[0].Rows[0]["Medium"].ToString();
                lblTestPressure_p.Text = ds.Tables[0].Rows[0]["TestPressure"].ToString();
                lblHoldingTime_p.Text = ds.Tables[0].Rows[0]["HoldingTime"].ToString();
                lblDesignPressure_p.Text = ds.Tables[0].Rows[0]["DesignPressure"].ToString();
                lblTestTemprature_p.Text = ds.Tables[0].Rows[0]["TestTemprature"].ToString();
                lblCodeno_p.Text = ds.Tables[0].Rows[0]["CodeNo"].ToString();
                lblRange_p.Text = ds.Tables[0].Rows[0]["Range"].ToString();
                lblCalibrationRef_p.Text = ds.Tables[0].Rows[0]["CalibrationRef"].ToString();
                lblCalibrationDoneOn_p.Text = ds.Tables[0].Rows[0]["CalibrationDoneOnView"].ToString();
                lblCalibrationDueOn_p.Text = ds.Tables[0].Rows[0]["CalibrationDueOnView"].ToString();
                lblResult_p.Text = ds.Tables[0].Rows[0]["Result"].ToString();

                gvItemDetails_p.DataSource = ds.Tables[1];
                gvItemDetails_p.DataBind();

                //hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
                //hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
                //hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
                //hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintPressureTestReport();", true);
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

    private void BindPressureTestReportDetails()
    {
        DataSet ds = new DataSet();
        objQCT = new cQCTestReports();
        try
        {
            objQCT.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQCT.GetPressureTestReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPressureTestReport.DataSource = ds.Tables[0];
                gvPressureTestReport.DataBind();
            }
            else
            {
                gvPressureTestReport.DataSource = "";
                gvPressureTestReport.DataBind();
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
    private void BindMovementTestReportPDFDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.MTRID = ViewState["MTRID"].ToString();
            ds = objQt.GetMovementTestReportdetailsByMTRID();

            //Address	PhoneAndFaxNo	Email	WebSite

            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblReportNo_p.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            lblDate_p.Text = ds.Tables[0].Rows[0]["Date"].ToString();

            lblItemName_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();

            lblPONo_p.Text = ds.Tables[0].Rows[0]["PONo"].ToString();
            lblProject_p.Text = ds.Tables[0].Rows[0]["JobDescription"].ToString();
            //   lblDRGNo_p.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //    lblQAPNo_p.Text = ds.Tables[0].Rows[0]["QAPNo"].ToString();

            lblcustomername_p.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();

            gvItemDetails_p.DataSource = ds.Tables[1];
            gvItemDetails_p.DataBind();

            //hdnAddress.Value = ds.Tables[2].Rows[0]["Address"].ToString();
            //hdnPhoneAndFaxNo.Value = ds.Tables[2].Rows[0]["PhoneAndFaxNo"].ToString();
            //hdnEmail.Value = ds.Tables[2].Rows[0]["Email"].ToString();
            //hdnWebsite.Value = ds.Tables[2].Rows[0]["WebSite"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintMovementTestReport();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}