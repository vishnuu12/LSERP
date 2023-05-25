using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_SalesRevenueTargetEmployeeWise : System.Web.UI.Page
{
    #region "Declaration"

    cSales objSales;
    cSession objSesion = new cSession();

    #endregion

    #region"Page Init Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSesion = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindEmployeeNameAndFinanceDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.EmployeeID = Convert.ToInt32(ddlsalesemployeename.SelectedValue);
            objSales.TargetValue = txttargetvalue.Text;
            objSales.CreatedBy = objSesion.employeeid;

            ds = objSales.SaveSalesRevenueTargetValuesDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Employee Target Revenue Details Saved Succesfyully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            bindEmployeeNameAndFinanceDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvSalesrenuetargetdetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteSalesRevenue")
            {
                objSales = new cSales();
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataSet ds = new DataSet();
                objSales.SEFYTG = Convert.ToInt32(gvSalesrenuetargetdetails.DataKeys[index].Values[0].ToString());
                ds = objSales.DeleteSalesRevenueDetailsByFYID();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Revenue Details Deleted Succesfyully');", true);
                bindEmployeeNameAndFinanceDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"
    private void bindEmployeeNameAndFinanceDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.BindSalaesEmployeeName(ddlsalesemployeename);
            lblfinanceyear.Text = ds.Tables[1].Rows[0]["FinanceYear"].ToString();

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvSalesrenuetargetdetails.DataSource = ds.Tables[2];
                gvSalesrenuetargetdetails.DataBind();
            }
            else
            {
                gvSalesrenuetargetdetails.DataSource = "";
                gvSalesrenuetargetdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}