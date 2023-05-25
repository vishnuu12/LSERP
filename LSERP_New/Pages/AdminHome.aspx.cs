using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using eplus.data;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Data.SqlClient;

public partial class AdminHome : System.Web.UI.Page
{
    #region "Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    public string JSONString = string.Empty;

    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        try
        {
            saveNavigationInfo();
            GetDashBoardColor();
            BindTaskdetails();
            //  BindDashBordDetails();            
            if (target == "Approvals")
            {
                if (arg.ToString() == "Staff")
                    Response.Redirect("StaffAssignment.aspx", false);
                else if (arg.ToString() == "Drawing")
                    Response.Redirect("DrawingHODApproval.aspx", false);
                else if (arg.ToString() == "BOM")
                    Response.Redirect("BOMApprovalSheet.aspx", false);
                else if (arg.ToString() == "RFP")
                    Response.Redirect("RFPApproval.aspx", false);
                else if (arg.ToString() == "SalesCost")
                    Response.Redirect("SalesCostApproval.aspx", false);
                else if (arg.ToString() == "POStaff")
                    Response.Redirect("POStaffAllocation.aspx", false);
                else if (arg.ToString() == "QuotationApproval")
                    Response.Redirect("QuotationApproval.aspx", false);
                else if (arg.ToString() == "PI")
                    Response.Redirect("PurchaseIndentApproval.aspx", false);
                else if (arg.ToString() == "SPO")
                    Response.Redirect("POApproval.aspx", false);
                else if (arg.ToString() == "RFP")
                    Response.Redirect("RFPApproval.aspx", false);
                else if (arg.ToString() == "WT")
                    Response.Redirect("MaterialPlaningWeightApproval.aspx", false);
                else if (arg.ToString() == "MRNQC")
                    Response.Redirect("BlockingMRNQualityClearence.aspx", false);
                else if (arg.ToString() == "JobCardQC")
                    Response.Redirect("SecondaryJobOrderQCClearence.aspx", false);
            }
            if (target == "Inbox")
                Response.Redirect("MyInBox.aspx", false);
            if (target == "Task")
                Response.Redirect("Tasks.aspx", false);
            if (target == "Alerts")
            {
                //if (arg.ToString() == "MaterialPlanning")
                //    Response.Redirect("MaterialPlanning.aspx", false);
                //else if (arg.ToString() == "RFPQualityPlanning")
                //    Response.Redirect("RFPQualityPlanning.aspx", false);
                //else if (arg.ToString() == "POStaffAllocation")
                //    Response.Redirect("POStaffAllocation.aspx", false);
                //else if (arg.ToString() == "BlockingMRNQualityClearence")
                //    Response.Redirect("BlockingMRNQualityClearence.aspx", false);
                //else if (arg.ToString() == "PurchaseIndent")
                //    Response.Redirect("PurchaseIndent.aspx", false);
                //else if (arg.ToString() == "SecondaryJobOrder")
                //    Response.Redirect("SecondaryJobOrder.aspx", false);

                if (arg.ToString() == "RFPJOBQC")
                    Response.Redirect("SecondaryJobOrderQCClearence.aspx", false);
                else if (arg.ToString() == "BQMRNQC")
                    Response.Redirect("BlockingMRNQualityClearence.aspx", false);
                else if (arg.ToString() == "RFPAWQC")
                    Response.Redirect("AssemplyWeldingQCClearance.aspx", false);
                else if (arg.ToString() == "WOIQCP")
                    Response.Redirect("WorkOrderQCPlanning.aspx", false);
                else if (arg.ToString() == "WOIQC")
                    Response.Redirect("WorkOrderInwardQCClearanceaspx.aspx", false);
                else if (arg.ToString() == "MIMRNQC")
                    Response.Redirect("MaterialInwardQCClearenceDetails.aspx", false);
                else if (arg.ToString() == "RFPQP")
                    Response.Redirect("RFPQualityPlanning.aspx", false);
                else if (arg.ToString() == "WPO")
                    Response.Redirect("WorkOrderPo.aspx", false);
                else if (arg.ToString() == "VDC")
                    Response.Redirect("VendorDC.aspx", false);
                else if (arg.ToString() == "WOIDC")
                    Response.Redirect("WorkOrderInward.aspx", false);
                else if (arg.ToString() == "SIMRN")
                    Response.Redirect("StockInward.aspx", false);
                else if (arg.ToString() == "RFPMP")
                    Response.Redirect("MaterialPlanning.aspx", false);
                else if (arg.ToString() == "RFPSecondaryJobCard")
                    Response.Redirect("SecondaryJobOrder.aspx", false);
                else if (arg.ToString() == "PJO")
                    Response.Redirect("PrimaryJobOrder.aspx", false);
                else if (arg.ToString() == "Issuematerial")
                    Response.Redirect("IssueMaterial.aspx", false);
                else if (arg.ToString() == "RFPStaffQuality")
                    Response.Redirect("RFPStaffAllocationQuality.aspx", false);
                else if (arg.ToString() == "RFPStaffProduction")
                    Response.Redirect("RFPStaffAllocationProduction.aspx", false);
                else if (arg.ToString() == "EnquiryReviewCheckList")
                    Response.Redirect("EnquiryReviewCheckList.aspx", false);
                else if (arg.ToString() == "AddItem")
                    Response.Redirect("AddItem.aspx", false);
                else if (arg.ToString() == "DrawingUpload")
                    Response.Redirect("Design.aspx", false);
                else if (arg.ToString() == "DesignStaffAssignment")
                    Response.Redirect("DesignStaffAssignment.aspx", false);
                else if (arg.ToString() == "BOMMaterialSpecSheet")
                    Response.Redirect("BOMMaterialSpecSheet.aspx", false);
                else if (arg.ToString() == "ViewCosting")
                    Response.Redirect("ViewCosting.aspx", false);
                else if (arg.ToString() == "NCRProduction")
                    Response.Redirect("CAPARequestPending.aspx", false);
                else if (arg.ToString() == "NCRQuality")
                    Response.Redirect("NCQCApproval.aspx", false);
                else if (arg.ToString() == "Inbox")
                {
                    Session["DailyAlert"] = "DailyAlert";
                    Response.Redirect("MyInBox.aspx", false);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion

    #region "GridView Events"

    protected void gvnavigation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[1].Visible = false; // hides the first column            
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }
    protected void gvnonavigation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            e.Row.Cells[0].Visible = false; // hides the first column                        
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }
    protected void gvChecklist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (_objSession.Designation == "Designer")
            {
                Response.Redirect("EnquiryReviewCheckList.aspx", false);
            }
            else if (_objSession.Designation == "Sales")
            {
                Response.Redirect("DesignApproval.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }
    protected void gvDrawingupload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (_objSession.Designation == "Designer")
            {
                Response.Redirect("Design.aspx", false);
            }
            else if (_objSession.Designation == "Sales")
            {
                Response.Redirect("SalesCost.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }
    protected void gvBomcost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (_objSession.Designation == "Designer")
            {
                Response.Redirect("DrawingBOM.aspx", false);
            }
            else if (_objSession.Designation == "Sales")
            {
                Response.Redirect("SalesCost.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    #endregion

    #region "Button Events"

    #endregion

    #region Dropdown Methods

    #endregion

    #region Common Methods

    public void GetDashBoardColor()
    {
        cCommonMaster _objCommon = new cCommonMaster();
        DataSet ds = _objCommon.getdashboardcolor();
        ViewState["DashBoard"] = ds;
    }

    public void saveNavigationInfo()
    {
        //string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
    }

    private void BindTaskdetails()
    {
        DataSet ds = new DataSet();
        StringBuilder sb = new StringBuilder();
        string content = "";
        try
        {
            //if (_objSession.type == 3)
            _objCommon.admin_Empid = Convert.ToInt32(_objSession.employeeid);

            ds = _objCommon.GetDesignertask("LS_GetDesignerTask");

            if (ds.Tables[0].Rows.Count < 1) sb.Append("<li><div class='descrptncontent' runat='server'><button type='button'  class='collapsible'><span>No Records</span></button></br></div></li>");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                content = "";
                content = "<li><div class='descrptncontent' runat='server'><button type='button'  id='" + ds.Tables[0].Rows[i]["Name"].ToString() + "' OnClick='Approvals(this);' class='collapsible'><span>" + ds.Tables[0].Rows[i]["Content"].ToString() + "</span><span class='count'  style='float: right;'>" + ds.Tables[0].Rows[i]["Count"].ToString() + "</span> </button></br></div></li>";
                sb.Append(content);
            }
            ulList.InnerHtml = sb.ToString();

            sb = new StringBuilder();
            if (ds.Tables[8].Rows.Count < 1) sb.Append("<li><div class='descrptncontent' runat='server'><button type='button'  class='collapsible'><span>No Records</span></button></br></div></li>");

            for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
            {
                content = "";
                content = "<li><div class='descrptncontent' runat='server'><button type='button'  id='" + ds.Tables[8].Rows[i]["Name"].ToString() + "' OnClick='Alerts(this);' class='collapsible'><span>" + ds.Tables[8].Rows[i]["Content"].ToString() + "</span><span class='count'  style='float: right;'>" + ds.Tables[8].Rows[i]["Count"].ToString() + "</span>  </button></br></div></li>";
                sb.Append(content);
            }
            ulist_Alerts.InnerHtml = sb.ToString();

            sb = new StringBuilder();
            if (ds.Tables[1].Rows.Count < 1) sb.Append("<li><div class='descrptncontent' runat='server'><button type='button'  class='collapsible'><span>No Records</span></button></br></div></li>");
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                content = "";
                //content = "<li><div class='descrptncontent' style='font-size:15px;' runat='server'>" + ds.Tables[1].Rows[i]["Message"].ToString() + "</li>";
                content = "<li><div class='descrptncontent' runat='server'><button type='button'  id='" + ds.Tables[1].Rows[i]["Message"].ToString() + "" + i + "' OnClick='Inbox(this);' class='collapsible'>" + ds.Tables[1].Rows[i]["Message"].ToString() +
                    "</button></br></div></li>";
                sb.Append(content);
            }
            ulList_Inbox.InnerHtml = sb.ToString();

            sb = new StringBuilder();
            if (ds.Tables[2].Rows.Count < 1) sb.Append("<li><div class='descrptncontent' runat='server'><button type='button'  class='collapsible'><span>No Records</span></button></br></div></li>");
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                content = "";
                content = "<li><div class='descrptncontent' runat='server'>" + ds.Tables[2].Rows[i]["Content"].ToString() + "</li>";
                //content = "<li><div class='descrptncontent' runat='server'><button type='button'  id='" + ds.Tables[2].Rows[i]["Name"].ToString() + "" + i + "' OnClick='Task(this);' class='collapsible'><span>" + ds.Tables[2].Rows[i]["Content"].ToString() + "</span><span class='count'  style='float: right;'>" + ds.Tables[2].Rows[i]["Count"].ToString() + "</span>  </button></br></div></li>";
                sb.Append(content);
            }
            ulList_Task.InnerHtml = sb.ToString();

            if (_objSession.type == 1)
            {
                divgraph.Visible = true;
                if (ds.Tables[3].Rows.Count > 0)
                {
                    JSONString = JsonConvert.SerializeObject(ds.Tables[3]);
                    JSONString = JSONString.Replace("]},{[", "] , [");
                    hdnjsonstring.Value = JSONString;
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    JSONString = JsonConvert.SerializeObject(ds.Tables[5]);
                    JSONString = JSONString.Replace("]},{[", "] , [");
                    hdnmonthjsonstring.Value = JSONString;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "areachart();piechart();", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "piechart();", true);
                if (ds.Tables[6].Rows.Count > 0)
                {
                    foreach (DataRow dtRow in ds.Tables[6].Rows)
                    {
                        if (dtRow["Name"].ToString() == "MonthlyPOOrders") lblMonthlyPOOrder.Text = dtRow["Count"].ToString();
                        if (dtRow["Name"].ToString() == "MonthyTotalEnquiry") lblMonthyEnquiries.Text = dtRow["Count"].ToString();
                        if (dtRow["Name"].ToString() == "PendingEnquiries") lblpendingenquiries.Text = dtRow["Count"].ToString();
                        if (dtRow["Name"].ToString() == "PendingOrders") lblpendingorders.Text = dtRow["Count"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    public void gridbind(DataTable dt, GridView gv, Label lbl, string lbltext)
    {
        lbl.Text = lbltext + dt.Rows.Count.ToString();
        if (dt.Rows.Count > 0)
        {
            gv.DataSource = dt;
            gv.DataBind();
        }
        else
        {
            gv.DataSource = "";
            gv.DataBind();
        }
        gv.Style.Add("display", "none");
    }

    #endregion







}