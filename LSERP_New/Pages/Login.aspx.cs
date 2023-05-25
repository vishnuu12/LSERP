using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using eplus.core;
using System.Data;

public partial class Pages_Login : System.Web.UI.Page
{

    #region "Declaration"

    cSession _objSession = new cSession();
    cLogin _objLogin = new cLogin();
    DataSet dsLoginDetails = new DataSet();

    #endregion

    #region "PageLoad Event"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == true)
            return;
        else
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["LoginDetails"] = null;
            txtUsername.Focus();
        }
    }

    #endregion

    #region "Button Events"

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            jsonStaff jp = new jsonStaff();
            //jp.SaveEnquiryDetails("0", 1, "Some%20Description%20about%20project", 1, 1, 100, "2020-08-31", "2020-09-25", 1, 1, 1, 1, 1, "123456.jpg");
            // jp.SendEnquiryMail("1001", "Internal", "Muuthukumar", "2282", "Agraja Mahesh", "hariram@innovasphere.in", "100001", "Test3", "test3", "", "703A5e3A553A5d3Af83A232CRealme%2CRMX1851%2C29%2C1");
            //jp.GetEnquiryStatusReportDetails("1061");

            // jp.GetDashBoardDetails("1001");

            //  jp.SaveTaskDetails("Closure", "2020-09-25", "1001");
            //jp.ViewEnquiryDetails("100000");

            //jp.GetApprovalSumaryDetailsByTypeAndName("Approvals", "BOM");

            _objLogin = new cLogin();
            _objLogin.userID = txtUsername.Text;
            _objLogin.passWord = txtPassword.Text;
            dsLoginDetails = _objLogin.getLoginDetails();
            foreach (DataRow drrow in dsLoginDetails.Tables[0].Rows)
            {
                if (drrow["Message"].ToString() == "Success")
                {
                    _objSession.loginname = drrow["LoginName"].ToString();
                    _objSession.employeeid = drrow["EmployeeID"].ToString();
                    _objSession.type = Convert.ToInt32(drrow["UserTypeID"]);
                    _objSession.name = drrow["Name"].ToString();
                    _objSession.mail = drrow["Email"].ToString();
                    _objSession.ERPUsertype = Convert.ToInt32(drrow["ERPUserType"].ToString());
                    _objSession.mobno = drrow["MobileNo"].ToString();
                    _objSession.DepID = Convert.ToInt32(drrow["DepartmentID"].ToString());
                    _objSession.DepName = drrow["DepartmentName"].ToString();
                    _objSession.Designation = drrow["Designation"].ToString();
                    _objSession.UserPhoto = drrow["Photo"].ToString();
                    _objSession.lastLoginDateTime = drrow["LastLoginTime"].ToString();
                    _objSession.CompanyID = drrow["CompanyID"].ToString();
                    Session["LoginDetails"] = _objSession;
                    int EmployeeID = Convert.ToInt32(drrow["EmployeeID"].ToString());
                    saveUserLoginDetails(EmployeeID);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    Response.Redirect("AdminHome.aspx", false);
                }
                else if (drrow["Message"].ToString() == "AE")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "message('Error!','Your account is deactive. Please contact administrator', 'error');", true);
                }
                else if (drrow["Message"].ToString() == "WP")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "message('Error!','Wrong Password. Please try again with correct password', 'error');", true);
                }

                else

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "message('Error!','Invalid account or incorrect details. If persists, please contact your administrator!', 'error');", true);
            }
        }
        catch (Exception)
        {
        }
    }

    protected void btnForgotPassword_Click(object sender, EventArgs e)
    {
        try
        {
        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region "Common Method"

    private void saveUserLoginDetails(int EmployeeID)
    {
        try
        {
            string VisitorsIPAddr = string.Empty;
            string EmpID = EmployeeID.ToString();
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            _objLogin = new cLogin();
            _objLogin.userID = EmpID;
            _objLogin.ipAddress = VisitorsIPAddr;
            _objLogin.source = "Desktop";
            _objLogin.saveLoginUserDetails();
        }
        catch (Exception)
        {
        }
    }

    #endregion

}