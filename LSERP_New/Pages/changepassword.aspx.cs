using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_changepassword : System.Web.UI.Page
{
    #region Declaration

    cCommonMaster objCom = new cCommonMaster();
    cSession _objSession = new cSession();
    EDProcess ed = new EDProcess();

    #endregion

    #region Page Load

    protected void Page_Init(object sender, EventArgs e)
    {

        _objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        try
        {
            PasswordDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    public void PasswordDetails()
    {
        objCom.UserID = _objSession.employeeid;
        DataSet ds = objCom.getuserdetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtUserName.Text = ds.Tables[0].Rows[0]["Username"].ToString();
            lblpassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationPassword())
            {
                string oldpassword = txtOldPassword.Text;
                objCom.Password = oldpassword;
                objCom.newpassword = txtNewPassword.Text;
                objCom.confirmpassword = txtConfirmpassword.Text;
                objCom.UserName = txtUserName.Text;
                DataSet ds = objCom.changepassword();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["output"].ToString() == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Password Changed Successfully');", true);
                        txtOldPassword.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmpassword.Text = "";
                        PasswordDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "InfoMessage('Information !','No Data Found');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "InfoMessage('Information !','All Fields Are Mandatory');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected bool ValidationPassword()
    {
        string oldpassword = txtOldPassword.Text;
        bool valid = true;
        if (txtOldPassword.Text == "")
        {
            valid = false;
        }
        else if (oldpassword != lblpassword.Text)
        {
            valid = false;
        }
        if (txtNewPassword.Text == "")
        {
            valid = false;
        }
        if (txtConfirmpassword.Text == "")
        {
            valid = false;
        }
        else if (txtNewPassword.Text != txtConfirmpassword.Text)
        {
            valid = false;
        }
        return valid;
    }
}