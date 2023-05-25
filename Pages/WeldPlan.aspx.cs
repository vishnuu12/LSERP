using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_WeldPlan : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cSales objSales;
    cMaterials objMat;

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
                DataSet dsPOHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                DataSet dsLocation = new DataSet();

                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO);

                ViewState["Customer"] = dsCustomer.Tables[0];
                ViewState["PO"] = dsPOHID.Tables[0];
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                objQt.VERID = arg.ToString();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeleteVisualreportdetailsByVERID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        cDesign objDesign = new cDesign();
        objSales = new cSales();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;

                objSales.POHID = ddlCustomerPO.SelectedValue;
                objSales.GetRFPDetailsByPOHID(ddlRFPNo);

                //ShowHideControls("input,view");
            }
            else
            {
                ddlCustomerName.SelectedIndex = 0;
                //ShowHideControls("input");
            }

            //objc = new cCommon();
            //objc.EmptyDropDownList(ddlItemName);
            //ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["PO"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlCustomerPO.DataSource = dt;
            ddlCustomerPO.DataTextField = "PORefNo";
            ddlCustomerPO.DataValueField = "POHID";
            ddlCustomerPO.DataBind();
            ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));

            //objc = new cCommon();
            //objc.EmptyDropDownList(ddlItemName);
            //ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                //objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                //                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsBYRFPHIDInWPReportDeatils");

                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();
                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string PageName = url.Replace(Replacevalue, "WeldPlanDetails.aspx");

                var page = HttpContext.Current.CurrentHandler as Page;

                string s = "window.open('" + PageName + "?RFPHID=" + ddlRFPNo.SelectedValue + "&UserID=" + objSession.employeeid + "','_blank');";
                //  page.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", s, true);
                //  lblTotalItemQty.Text = hdnTotalItemQty.Value = "Total Item Qty" + ds.Tables[0].Rows[0]["QTY"].ToString();

                //bindWeldPlanReportHeader();
                //ShowHideControls("input,view");
            }
            //else
            //{
            //    objc = new cCommon();
            //    objc.EmptyDropDownList(ddlItemName);
            //    ShowHideControls("input");
            //}

            //hdnWPRHID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


}