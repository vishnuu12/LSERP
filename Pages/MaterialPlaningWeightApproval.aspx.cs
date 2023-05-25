using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MaterialPlaningWeightApproval : System.Web.UI.Page
{
    #region"Declaration"

    cProduction objProd;
    cSession objSession = new cSession();

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
                BinDMaterialPlanningDeviationDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objProd = new cProduction();
        try
        {
            LinkButton btn = sender as LinkButton;

            foreach (GridViewRow row in gvMPWeightApproval.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    if (btn.CommandName == "Approval")
                    {
                        objProd.MPID = Convert.ToInt32(gvMPWeightApproval.DataKeys[row.RowIndex].Values[0].ToString());
                        objProd.Flag = "A";
                    }
                    else
                    {
                        objProd.MPID = Convert.ToInt32(gvMPWeightApproval.DataKeys[row.RowIndex].Values[0].ToString());
                        objProd.Flag = "R";
                    }
                    objProd.CreatedBy = Convert.ToInt32(objSession.employeeid);
                    objProd.Remarks = txtRemarks.Text;
                    ds = objProd.UpdateMaterialPlanningWeightApprovalDetails();
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Approved")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Weight Approved Successfully');", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Rejected")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Weight Rejected Successfully');", true);
            BinDMaterialPlanningDeviationDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BinDMaterialPlanningDeviationDetails()
    {
        DataSet ds = new DataSet();
        objProd = new cProduction();
        try
        {
            ds = objProd.GetMaterialPlanningDeviationDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMPWeightApproval.DataSource = ds.Tables[0];
                gvMPWeightApproval.DataBind();
            }
            else
            {
                gvMPWeightApproval.DataSource = "";
                gvMPWeightApproval.DataBind();
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
        if (gvMPWeightApproval.Rows.Count > 0)
            gvMPWeightApproval.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}