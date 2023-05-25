using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ProjectExpensesEntry : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cProduction objP;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (!IsPostBack)
            {
                BindRFPProjectExpensesDetails();
                BindProjectExpensesEntryDetails();
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    objP = new cProduction();
                    objP.PEEID = Convert.ToInt32(arg.ToString());

                    ds = objP.DeleteProjectExpensesEntryDetails();
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Part Name Deleted successfully');", true);
                    BindProjectExpensesEntryDetails();
                    BindRFPProjectExpensesDetails();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnProjectExpenses_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            foreach (GridViewRow row in gvRFPDetails.Rows)
            {
                CheckBox chkQC = (CheckBox)row.FindControl("chkQC");
                TextBox txtCMPPer = (TextBox)row.FindControl("txtCMPPer");
                TextBox txtOSF = (TextBox)row.FindControl("txtOSF");
                TextBox txtCurrentMonthDispatchValue = (TextBox)row.FindControl("txtCurrentMonthDispatchValue");
                TextBox txtOpenProjectExpenses = (TextBox)row.FindControl("txtOpenProjectExpenses");
                TextBox txtCPV = (TextBox)row.FindControl("txtCPV");
                Label lblRFPNo = (Label)row.FindControl("lblRFPNo");

                if (chkQC.Checked)
                {
                    objP.RFPHID = Convert.ToInt32(gvRFPDetails.DataKeys[row.RowIndex].Values[0].ToString());
                    objP.CMPPercentage = Convert.ToDecimal(txtCMPPer.Text);
                    objP.OSF = Convert.ToDecimal(txtOSF.Text);
                    objP.CurrentMonthDispatchValue = Convert.ToDecimal(txtCurrentMonthDispatchValue.Text);
                    objP.OpenProjectExpensesValue = Convert.ToDecimal(txtOpenProjectExpenses.Text);
                    objP.ClosingProjectValue = Convert.ToDecimal(txtCPV.Text);
                    objP.UserID = Convert.ToInt32(objSession.employeeid);

                    ds = objP.SaveProjectExpensesEntry();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() != "Added")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + lblRFPNo.Text + "');", true);
                        break;
                    }
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Expenses Saved SuccessFully');", true);
                BindProjectExpensesEntryDetails();
                BindRFPProjectExpensesDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindRFPProjectExpensesDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            ds = objP.GetRFPDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPDetails.DataSource = ds.Tables[0];
                gvRFPDetails.DataBind();
            }
            else
            {
                gvRFPDetails.DataSource = "";
                gvRFPDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindProjectExpensesEntryDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            ds = objP.BindProjectExpensesEntryDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvProjectExpensesDetails.DataSource = ds.Tables[0];
                gvProjectExpensesDetails.DataBind();
            }
            else
            {
                gvProjectExpensesDetails.DataSource = "";
                gvProjectExpensesDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvRFPDetails.Rows.Count > 0)
            gvRFPDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}