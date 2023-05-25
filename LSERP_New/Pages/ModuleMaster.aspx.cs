using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web.Services;

public partial class Pages_ModuleMaster : System.Web.UI.Page
{
    public cModuleMaster objMaster = new cModuleMaster();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        if (IsPostBack)
        {
            if (target == "deletemodule") deletemodule();
            else return;
        }
        else
        {
            BindgvModule();
        }
    }

    protected void BindgvModule()
    {
        try
        {
            objMaster = new cModuleMaster();
            DataSet ds = objMaster.getModuleMaster(objMaster);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvModuleMaster.DataSource = ds;
                gvModuleMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void deletemodule()
    {
        try
        {
            objMaster.MID = Convert.ToInt32(hdnMID.Value);
            objMaster.DleteModule(objMaster);
            BindgvModule();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Success!', 'Module Dleted Successfully','success');", true);            
        }
        catch (Exception ec) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Error!', 'Somer Teechnical Error occured','error');", true); }
    }
    protected void btnSaveApprover_Click(object sender, EventArgs e)
    {
        try
        {
            if (isvalid())
            {
                objMaster = new cModuleMaster();
                objMaster.moduleName = txtModuleName.Text;
                objMaster.moduleIcon = iconvalue.Value;
                if (hdnMID.Value == "")
                {
                    objMaster.saveModules(objMaster);
                    if (objMaster.returnvalue == "AE")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Info!','Module Name Already Existed','success');", true);
                }
                else
                {
                    objMaster.MID = Convert.ToInt32(hdnMID.Value);
                    objMaster.UpdateModules(objMaster);
                }
                BindgvModule();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Success!','Data Saved Successfully','success');", true);
            }
        }
        catch(Exception ec) { ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Error!','Some Technical Error occured','Error');", true); }
    }

    protected void gvModuleMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblModuleIcon = ((Label)e.Row.FindControl("lblModuleIcon"));
                Image imgModuleIcon = ((Image)e.Row.FindControl("imgModuleIcon"));
                if (lblModuleIcon.Text == "")
                {
                    lblModuleIcon.Visible = true;
                    lblModuleIcon.Text = "--";
                }
                else
                {
                    Label lblMID = ((Label)e.Row.FindControl("lblMID"));
                    string path = ConfigurationManager.AppSettings["ModuleIconPath"].ToString() + lblMID.Text + "\\" + lblModuleIcon.Text;
                    imgModuleIcon.ImageUrl = path;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvModuleMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditDetails" || e.CommandName == "DeleteDetails")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gridRow = gvModuleMaster.Rows[index];
            Label lblMID = ((Label)gridRow.FindControl("lblMID"));
            Label lblModuleName = ((Label)gridRow.FindControl("lblModuleName"));
            Label lblModuleIcon = ((Label)gridRow.FindControl("lblModuleIcon"));
            txtModuleName.Text = lblModuleName.Text;
            hdnMID.Value = lblMID.Text;
            if (e.CommandName == "DeleteDetails")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "deleteConfirm("+ hdnMID.Value + ");", true);
            }
            else
            { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowData(" + lblMID.Text + ");", true);
                iconview.InnerHtml = lblModuleIcon.Text;
                btnSaveApprover.Text = "Update";
            }
        }
    }
   
    protected bool isvalid()
    {
        bool valid = true;
        if (txtModuleName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "message('Error!','Please Enter Module Name','error');", true);
            valid = false;
        }
        return valid;
    }
}