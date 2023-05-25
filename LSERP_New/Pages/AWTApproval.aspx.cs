using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AWTApproval : System.Web.UI.Page
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
                BindPrimaryJoborderAWTDeviationDetails();
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
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            foreach (GridViewRow row in gvAWTApproval.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    if (btn.CommandName == "Approval")
                    {
                        objProd.PRDID = Convert.ToInt32(gvAWTApproval.DataKeys[row.RowIndex].Values[0].ToString());
                        objProd.Flag = "A";
                    }
                    else
                    {
                        objProd.PRDID = Convert.ToInt32(gvAWTApproval.DataKeys[row.RowIndex].Values[0].ToString());
                        objProd.Flag = "R";
                    }
                    objProd.CreatedBy = Convert.ToInt32(objSession.employeeid);
                    objProd.Remarks = txtRemarks.Text;
                    ds = objProd.UpdatePrimaryJoborderAWTApprovalDetails();
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Approved")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Weight Approved Successfully');", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Rejected")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Weight Rejected Successfully');", true);
            BindPrimaryJoborderAWTDeviationDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindPrimaryJoborderAWTDeviationDetails()
    {
        DataSet ds = new DataSet();
        objProd = new cProduction();
        try
        {
            ds = objProd.GetPrimaryJoborderAWTDeviationDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAWTApproval.DataSource = ds.Tables[0];
                gvAWTApproval.DataBind();
            }
            else
            {
                gvAWTApproval.DataSource = "";
                gvAWTApproval.DataBind();
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
        if (gvAWTApproval.Rows.Count > 0)
            gvAWTApproval.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}