using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Configuration;

public partial class Pages_PurchaseIndentApproval : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;
    cPurchase objP;

    string PurchaseIndentSavePath = ConfigurationManager.AppSettings["PurchaseIndentSavePath"].ToString();
    string PurchaseIndentHttpPath = ConfigurationManager.AppSettings["PurchaseIndentHttpPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objP = new cPurchase();
                DataSet dsPINum = new DataSet();
                dsPINum = objP.GetPurchaseIndentNumber(ddlIndentNumber);
            }
            //        if (target == "UpdatePIStatus")
            //          UpdatePIStatus();
            if (target == "ViewIndentCopy")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvViewPurchaseIndent.DataKeys[index].Values[1].ToString();
                ViewState["PID"] = gvViewPurchaseIndent.DataKeys[index].Values[0].ToString();
                ViewDrawingFilename();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlIndentNumber_OnselectedIndexChanged(object sender, EventArgs e)
    {
        objP = new cPurchase();
        objc = new cCommon();
        try
        {
            if (ddlIndentNumber.SelectedIndex > 0)
            {
                objP.QHID = Convert.ToInt32(ddlIndentNumber.SelectedValue);
                BindGradeNameByPINumber();
                divOutput.Visible = true;
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cPurchase();
        string PID = "";
        string Msg = "";
        try
        {
            string Commandname = ((LinkButton)sender).CommandName;

            foreach (GridViewRow row in gvViewPurchaseIndent.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                if (chk.Checked)
                {
                    if (PID == "")
                        PID = gvViewPurchaseIndent.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PID = PID + "," + gvViewPurchaseIndent.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            objP.PIDs = PID;
            objP.QHID = Convert.ToInt32(ddlIndentNumber.SelectedValue);
            if (Commandname == "Approve")
            {
                objP.ApprovalStatus = 1;
                Msg = "Approved";
            }
            else if (Commandname == "Reject")
            {
                objP.ApprovalStatus = 9;
                Msg = "Rejected";
            }
            objP.CreatedBy = objSession.employeeid;
            objP.Remarks = txtRemarks.Text;
            ds = objP.UpdatePurchaseIndentApprovalStatus();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Indent " + Msg + " Successfully');", true);
                BindGradeNameByPINumber();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindGradeNameByPINumber()
    {
        objP = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            objP.QHID = Convert.ToInt32(ddlIndentNumber.SelectedValue);
            objP.PageNameFlag = "Approve";
            ds = objP.GetPurchaseIndentDetailsByIndentNumber();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvViewPurchaseIndent.DataSource = ds.Tables[0];
                gvViewPurchaseIndent.DataBind();

                lblIndentBy.Text = ds.Tables[0].Rows[0]["IndentBY"].ToString();
                lblIndentDate.Text = ds.Tables[0].Rows[0]["CreatedOn"].ToString();

                lblRFPNo.Text= ds.Tables[0].Rows[0]["RFPNo"].ToString();
            }
            else
            {
                gvViewPurchaseIndent.DataSource = "";
                gvViewPurchaseIndent.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewDrawingFilename()
    {
        try
        {
            objc = new cCommon();

            objc.ViewFileName(PurchaseIndentSavePath, PurchaseIndentHttpPath, ViewState["FileName"].ToString(), ViewState["PID"].ToString(), ifrm);

            //if (File.Exists(imgname))
            //{
            //    ViewState["ifrmsrc"] = httpPath + ViewState["FileName"].ToString();
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
            //    string s = "window.open('" + httpPath + EnquiryID + "/" + ViewState["FileName"].ToString() + "','_blank');ShowViewPopUP();";
            //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //}
            //else
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);                
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUP();", true);
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvViewPurchaseIndent.Rows.Count > 0)
            gvViewPurchaseIndent.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}