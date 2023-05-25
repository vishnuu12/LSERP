using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ContractorPaymentApproval : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
    cProduction objP;
    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                BindPrimaryJobOrderDetails();
                //bindContractorPaymentApprovalDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #region"Button Events"

    protected void Approval_Click(object sender, EventArgs e)
    {
        objP = new cProduction();
        string CPDID = "";
        DataSet ds = new DataSet();
        try
        {
            LinkButton btn = (LinkButton)sender;
            foreach (GridViewRow row in gvContractorPaymentDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkitems");
                TextBox txt = (TextBox)row.FindControl("txtRejectRemarks");
                if (chk.Checked)
                {
                    objP.CPDID = Convert.ToInt32(gvContractorPaymentDetails.DataKeys[row.RowIndex].Values[0].ToString());
                    if (btn.CommandName == "Approve")
                        objP.Flag = "Approve";
                    else if (btn.CommandName == "Reject")
                        objP.Flag = "Reject";
                    objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
                    objP.Remarks = txt.Text;
                    ds = objP.UpdateContractorPaymentApprovalStatusByPaymentID();
                }
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                if (btn.CommandName == "Approve")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Payment Approved Succesfully');HideJobCardDetailsPopUP();", true);
                else if (btn.CommandName == "Reject")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Payment Rejected Succesfully');", true);
                BindPrimaryJobOrderDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvPrimaryJobOrderDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string PJOID = gvPrimaryJobOrderDetails.DataKeys[index].Values[0].ToString();
            hdnPJOID.Value = PJOID;

            hdnRFPDID.Value = gvPrimaryJobOrderDetails.DataKeys[index].Values[1].ToString().Split('/')[0];
            hdnEDID.Value = gvPrimaryJobOrderDetails.DataKeys[index].Values[1].ToString().Split('/')[1];

            Label lblItemName = (Label)gvPrimaryJobOrderDetails.Rows[index].FindControl("lblItemName");
            ViewState["ItemName"] = lblItemName.Text;

            if (e.CommandName == "Payment")
            {
                LinkButton btn = (LinkButton)gvPrimaryJobOrderDetails.Rows[index].FindControl("lbtnJobNo");
                bindContractorPaymentApprovalDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowJobCardDetailsPopUP();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindContractorPaymentApprovalDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.RFPDID = Convert.ToInt32(hdnRFPDID.Value);
            ds = objP.GetContractorPaymentApprovalDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvContractorPaymentDetails.DataSource = ds.Tables[0];
                gvContractorPaymentDetails.DataBind();
            }
            else
            {
                gvContractorPaymentDetails.DataSource = "";
                gvContractorPaymentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPrimaryJobOrderDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            ds = objP.GetPrimaryJobOrderDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPrimaryJobOrderDetails.DataSource = ds.Tables[0];
                gvPrimaryJobOrderDetails.DataBind();
            }
            else
            {
                gvPrimaryJobOrderDetails.DataSource = "";
                gvPrimaryJobOrderDetails.DataBind();
            }
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
        if (gvPrimaryJobOrderDetails.Rows.Count > 0)
            gvPrimaryJobOrderDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}