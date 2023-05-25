using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_AddSmsAndEmailGroup : System.Web.UI.Page
{

    #region "Declaration"

    c_HR objH;
    cSession objSession = new cSession();
    EmailAndSmsAlerts objAlerts;

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

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
                GetEmployeeList();
                GetCommunicationGroupType();
                bindSMSGroupAndEmailGroupDetails();
            }
            if (target == "DeleteCommunicationGroup")
                DeleteCommunicationGroupDetailsByCGNID(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    #endregion

    #region"Button Events"

    protected void btnSaveAndShare_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objAlerts = new EmailAndSmsAlerts();
        string EmployeeID;
        try
        {
            DataRow dr;
            dt.Columns.Add("EmployeeID");

            string GroupName = txtGroupName.Text;
            int i = 0;
            foreach (GridViewRow row in gvEmployeeList.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                if (chkitems.Checked)
                {
                    EmployeeID = gvEmployeeList.DataKeys[row.RowIndex].Values[0].ToString();

                    dr = dt.NewRow();
                    dr["EmployeeID"] = EmployeeID;
                    dt.Rows.Add(dr);
                }
            }
            objAlerts.CGNID = Convert.ToInt32(hdnCGNID.Value);
            objAlerts.dt = dt;
            objAlerts.GroupName = txtGroupName.Text;
            objAlerts.GroupType = ddlGroupType.SelectedValue;
            ds = objAlerts.SaveEmployeeEmailAndSMSGroup();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Group Saved Successfully');", true);
            bindSMSGroupAndEmailGroupDetails();
            hdnCGNID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblGroupType_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindSMSGroupAndEmailGroupDetails();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "OpenTab('VSG');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvGroupDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnCGNID.Value = gvGroupDetails.DataKeys[index].Values[0].ToString();

            Label lblGroupName = (Label)gvGroupDetails.Rows[index].FindControl("lblGroupName");

            if (e.CommandName == "ViewGroupDetails")
            {
                bindGroupMembersDetailsByCGNID();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
            }
            if (e.CommandName == "EditGroupDetails")
            {
                txtGroupName.Text = lblGroupName.Text;
                ddlGroupType.SelectedValue = rblGroupType.SelectedValue;
                GetEmployeeList();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvEmployeeList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkitems = (CheckBox)e.Row.FindControl("chkitems");

                if (dr["CGNID"].ToString() == "0")
                    chkitems.Checked = false;
                else
                    chkitems.Checked = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindGroupMembersDetailsByCGNID()
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        try
        {
            objAlerts.CGNID = Convert.ToInt32(hdnCGNID.Value);
            ds = objAlerts.GetGroupMembersDetailsByGroupID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGroupMemberDetails_p.DataSource = ds.Tables[0];
                gvGroupMemberDetails_p.DataBind();
            }
            else
            {
                gvGroupMemberDetails_p.DataSource = "";
                gvGroupMemberDetails_p.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetEmployeeList()
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        try
        {
            objAlerts.CGNID = Convert.ToInt32(hdnCGNID.Value);
            ds = objAlerts.GetEmployeeList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEmployeeList.DataSource = ds.Tables[0];
                gvEmployeeList.DataBind();
            }
            else
            {
                gvEmployeeList.DataSource = "";
                gvEmployeeList.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindSMSGroupAndEmailGroupDetails()
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        try
        {
            objAlerts.GroupType = rblGroupType.SelectedValue;
            ds = objAlerts.GetSMSGroupAndEmailGroupDetailsByGroupType();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGroupDetails.DataSource = ds.Tables[0];
                gvGroupDetails.DataBind();
            }
            else
            {
                gvGroupDetails.DataSource = "";
                gvGroupDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetCommunicationGroupType()
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        try
        {
            ds = objAlerts.GetCommunicationGroupType(ddlGroupType);

            rblGroupType.DataSource = ds.Tables[0];
            rblGroupType.DataTextField = "GroupTypeName";
            rblGroupType.DataValueField = "GroupTypeID";
            rblGroupType.DataBind();
            rblGroupType.SelectedValue = "1";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteCommunicationGroupDetailsByCGNID(int CGNID)
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        try
        {
            objAlerts.CGNID = CGNID;
            ds = objAlerts.DeleteCommunicationGroupDetailsByCGNID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Group Name Deleted Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            bindSMSGroupAndEmailGroupDetails();
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

        if (gvEmployeeList.Rows.Count > 0)
        {
            gvEmployeeList.UseAccessibleHeader = true;
            gvEmployeeList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvGroupDetails.Rows.Count > 0)
        {
            gvGroupDetails.UseAccessibleHeader = true;
            gvGroupDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvGroupMemberDetails_p.Rows.Count > 0)
        {
            gvGroupMemberDetails_p.UseAccessibleHeader = true;
            gvGroupMemberDetails_p.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}