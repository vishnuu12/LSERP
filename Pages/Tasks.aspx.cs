using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Globalization;
public partial class Tasks : System.Web.UI.Page
{

    SqlConnection c = new SqlConnection();
    private SqlCommand cmd;
    private DataSet ds;
    private SqlDataAdapter adpt;
    private SqlDataReader dr;
    c_Finance objFinance;
    cSession _objSession = new cSession();

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetTasks(null);
        }
    }


    private void GetTasks(string Status)
    {
        objFinance = new c_Finance();
        if (Status == null)
            Status = "";
        DataSet ds = objFinance.getTasks(_objSession.employeeid.ToString(), Status);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvTask.DataSource = ds;
            lvTask.DataBind();
            divcheckall.Visible = true;
        }
        else
        {
            lvTask.DataSource = ds;
            lvTask.DataBind();
            divcheckall.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('Information!','No Tasks Found');", true);
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        cmd = new SqlCommand();
        c.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        cmd.Connection = c;
        c.Open();
        SqlTransaction sqlTransaction = c.BeginTransaction();
        cmd.Transaction = sqlTransaction;
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "LS_InsertTasks";
            cmd.Parameters.AddWithValue("@strTask", txtTask.Text.Trim());
            cmd.Parameters.AddWithValue("@UserID", _objSession.employeeid.ToString());
            if (txtCompletionDate.Text != "")
                cmd.Parameters.AddWithValue("@CompletionDate", DateTime.ParseExact(txtCompletionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            else
                cmd.Parameters.AddWithValue("@CompletionDate", DBNull.Value);
            cmd.ExecuteNonQuery();
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            Log.Message(ex.Message);
        }
        txtTask.Text = string.Empty;
        txtTask.Focus();
        txtCompletionDate.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success!','Task Added Successfully');", true);
        GetTasks(null);
    }


    protected void Content_Load(object sender, EventArgs e)
    {
        ImageButton lbdelete = sender as ImageButton;
        HiddenField myhiddenfield = lbdelete.NamingContainer.FindControl("hiddenID") as HiddenField;
        int myID = Convert.ToInt32(myhiddenfield.Value);

        cmd = new SqlCommand();
        c.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        cmd.Connection = c;
        c.Open();
        SqlTransaction sqlTransaction = c.BeginTransaction();
        cmd.Transaction = sqlTransaction;
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "LS_DeleteTasks";
            cmd.Parameters.AddWithValue("@strTaskID", myID);
            cmd.ExecuteNonQuery();
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            Log.Message(ex.Message);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success!','Task Deleted Successfully');", true);
        GetTasks(null);
    }


    protected void chkTaskall_OnCheckedChanged(object sender, EventArgs e)
    {
        int status = 0;
        if (chkTaskall.Checked == true)
            status = 1;

        cmd = new SqlCommand();
        c.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        cmd.Connection = c;
        c.Open();
        SqlTransaction sqlTransaction = c.BeginTransaction();
        cmd.Transaction = sqlTransaction;
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "LS_UpdateTasksall";
            cmd.Parameters.AddWithValue("@strStatus", status);
            cmd.ExecuteNonQuery();
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            Log.Message(ex.Message);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success!','Task status Updated Successfully');", true);
        GetTasks(null);
    }


    protected void chkTask_OnCheckedChanged(object sender, EventArgs e)
    {
        int status = 0;
        CheckBox cb = (CheckBox)sender;
        ListViewItem item = (ListViewItem)cb.NamingContainer;
        ListViewDataItem dataItem = (ListViewDataItem)item;
        int myID = Convert.ToInt32(lvTask.DataKeys[dataItem.DisplayIndex].Value.ToString());

        if (cb.ToolTip == "0")
            status = 1;

        cmd = new SqlCommand();
        c.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        cmd.Connection = c;
        c.Open();
        SqlTransaction sqlTransaction = c.BeginTransaction();
        cmd.Transaction = sqlTransaction;
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.CommandText = "LS_UpdateTasks";
            cmd.Parameters.AddWithValue("@strTaskID", myID);
            cmd.Parameters.AddWithValue("@strStatus", status);
            cmd.ExecuteNonQuery();
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            Log.Message(ex.Message);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success!','Task status Updated Successfully');", true);
        GetTasks(null);
    }


    protected void btnActive_Click(object sender, EventArgs e)
    {
        GetTasks("0");
    }
    protected void btnComplete_Click(object sender, EventArgs e)
    {
        GetTasks("1");
    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        GetTasks(null);
    }
}