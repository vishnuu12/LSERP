using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Pages_SMS : System.Web.UI.Page
{
    #region "Declaration"

    cSession objSession = new cSession();
    EmailAndSmsAlerts objAlert;
    c_HR objH;
    cSession _objSession = new cSession();

    #endregion

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
            //LoadAlert();
            //LoadDepartment();
            //  loadBox();
            BindEmployeeDetails();
        }
    }

    #region"DropDown Events"

    protected void ddlEmployee_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objH = new c_HR();
        try
        {
            ds = objH.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(ddlEmployee.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
                txtMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('information','Email ID Is Not Available');", true);
                txtMobile.Text = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region radio button Methods

    protected void rbtnMode_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnMode.SelectedValue == "1")
        {
            divcontact.Visible = true;
            divStaff.Visible = true;
            divDisplayto.Visible = false;
        }
        else
        {
            divStaff.Visible = false;
            divcontact.Visible = false;
            divDisplayto.Visible = true;
            rbEmailgroup1.SelectedIndex = 0;
            loadBox();
            DivDep.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "Dep();", true);
        }
    }

    protected void rbtnSendto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbEmailgroup1.SelectedValue == "1")
        {
            DivDep.Style.Add("display", "block");
            divmygroup.Style.Add("display", "none");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "Dep();", true);
        }
        else if (rbEmailgroup1.SelectedValue == "2")
        {
            DivDep.Style.Add("display", "none");
            divmygroup.Style.Add("display", "block");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "Dep();", true);
        }
        else
        {
            DivDep.Style.Add("display", "none");
            divmygroup.Style.Add("display", "none");
        }
    }

    #endregion


    #region"Button Events"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strDepart = string.Empty;
        string strID = string.Empty;
        string strRecType = string.Empty;
        string strRecID = string.Empty;
        string strRecName = string.Empty;
        string strRoute = string.Empty;
        string strMessage = string.Empty;
        string strReceiverGroup = string.Empty;
        strMessage = txtMessage.Text;
        bool valid = true;
        string Flag = "";
        DataSet dsDG = new DataSet();
        string DepartmentIds = "";
        string GroupNameIds = "";

        objAlert = new EmailAndSmsAlerts();

        if (rbtnMode.SelectedValue == "1")
        {
            if (txtMobile.Text != "" && txtMessage.Text != "" && ddlEmployee.SelectedIndex != 0)
            {
                if (txtMobile.Text.Length != 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Please enter Valid Mobile Number.');", true);
                    valid = false;
                    return;
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error!','Fill all fields .');", true);
                valid = false;
                return;
            }
        }

        objAlert.EntryMode = rbtnMode.SelectedItem.Text.ToString();

        objAlert.reciverID = ddlEmployee.SelectedValue;
        if (objAlert.EntryMode == "Individual")
        {
            if (valid == true)
            {
                objAlert.reciverType = "Individual";
                objAlert.GroupID = 0;
                objAlert.ReceiverGroup = "";

                objAlert.MobileNo = txtMobile.Text;
                objAlert.Message = txtMessage.Text;
                objAlert.userID = _objSession.employeeid;
                SqlCommand cmd = new SqlCommand();
                SqlConnection c = new SqlConnection();
                c.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                cmd.Connection = c;
                c.Open();
                SqlTransaction sqlTransaction = c.BeginTransaction();
                cmd.Transaction = sqlTransaction;
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    string AlertID = objAlert.SaveSmsAlertDetails();
                    //sqlTransaction.Commit();
                    DataTable dtMobileNo = new DataTable();
                    dtMobileNo.Columns.Add("MobileNo");
                    DataRow row = dtMobileNo.NewRow();
                    row[0] = txtMobile.Text;
                    dtMobileNo.Rows.Add(row);
                    DataTable dtSettings = new DataTable();
                    dtSettings = objAlert.GetSMSSettings();
                    objAlert.dtSettings = dtSettings;
                    if (rbtnMode.SelectedValue == "1")
                    {
                        string returnvalue = objAlert.SendSMS(dtMobileNo, dtSettings, strMessage);

                        if (returnvalue == "sent") ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success!', 'Message sent successfully');", true);
                        else ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Erromessage('Success!', 'Error occurred');", true);
                    }

                }
                catch (Exception ex)
                {
                    Log.Message(ex.Message);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Erromessage('Success!', 'Error occurred');", true);
                }
            }
        }

        else if (objAlert.EntryMode == "Group")
        {
            objAlert.reciverType = rbEmailgroup1.SelectedItem.Text.ToString();
            objAlert.GroupID = Convert.ToInt32(rbEmailgroup1.SelectedValue.ToString());
            objH = new c_HR();
            objAlert.EntryMode = "Group";
            if (rbEmailgroup1.SelectedValue == "1")
            {
                if (lstDepartments.SelectedValue != "0")
                {
                    foreach (ListItem li in lstDepartments.Items)
                    {
                        if (li.Selected)
                        {
                            if (strReceiverGroup == "")
                                strReceiverGroup = lstDepartments.SelectedValue;
                            else
                                strReceiverGroup = strReceiverGroup + ',' + lstDepartments.SelectedValue;
                        }
                    }

                    objH.departmentids = strReceiverGroup;
                    dsDG = objH.GetEmployeeDetailsByDepartmentIDs();
                    Flag = "0";
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ErrorMessage('Error','Select Department Name');", true);
            }
            else
            {
                if (lstmygroup.SelectedValue != "0")
                {
                    foreach (ListItem li in lstmygroup.Items)
                    {
                        if (li.Selected)
                        {
                            if (strReceiverGroup == "")
                                strReceiverGroup = lstDepartments.SelectedValue;
                            else
                                strReceiverGroup = strReceiverGroup + ',' + lstmygroup.SelectedValue;
                        }
                    }

                    objH.GroupNameIds = strReceiverGroup;
                    dsDG = objH.GetEmployeeDetailsByGroupNameIDs();
                    Flag = "0";
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ErrorMessage('Error','Select Group Name');", true);
            }

            DataTable dtMobileNo1 = new DataTable();
            DataTable dtSettings1 = new DataTable();
            dtMobileNo1.Columns.Add("MobileNo");
            DataRow dr;
            for (int i = 0; i < dsDG.Tables[0].Rows.Count; i++)
            {
                objAlert.reciverID = dsDG.Tables[0].Rows[i]["EmployeeID"].ToString();
                objAlert.ReceiverGroup = strReceiverGroup;
                objAlert.MobileNo = dsDG.Tables[0].Rows[i]["MobileNo"].ToString();
                objAlert.Message = txtMessage.Text;
                objAlert.userID = _objSession.employeeid;
                SqlCommand cmd1 = new SqlCommand();
                SqlConnection c1 = new SqlConnection();
                c1.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                cmd1.Connection = c1;
                c1.Open();
                SqlTransaction sqlTransaction1 = c1.BeginTransaction();
                cmd1.Transaction = sqlTransaction1;
                cmd1.CommandType = CommandType.StoredProcedure;

                string AlertID1 = objAlert.SaveSmsAlertDetails();

                dr = dtMobileNo1.NewRow();
                dr["MobileNo"] = "9384116378";//dsDG.Tables[0].Rows[i]["MobileNo"].ToString();
                dtMobileNo1.Rows.Add(dr);
            }
            dtSettings1 = objAlert.GetSMSSettings();
            objAlert.dtSettings = dtSettings1;
            string returnvalue1 = objAlert.SendSMS(dtMobileNo1, dtSettings1, strMessage);

            if (returnvalue1 == "sent") ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success!', 'Message sent successfully');", true);
            else ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Erromessage('Success!', 'Error occurred');", true);
        }
    }

    #endregion


    #region"Common Methods"

    private void loadBox()
    {
        objH = new c_HR();
        DataSet ds = new DataSet();
        try
        {
            ds = objH.getDepartment();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstDepartments.DataSource = ds.Tables[0];
                lstDepartments.DataTextField = "DepartmentName";
                lstDepartments.DataValueField = "DepartmentID";
                lstDepartments.DataBind();

                lstDepartments.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            objH.EmpID = Convert.ToInt32(_objSession.employeeid);
            ds = objH.GetMyGroupNameDetailsByEmployeeID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstmygroup.DataSource = ds.Tables[0];
                lstmygroup.DataTextField = "GroupName";
                lstmygroup.DataValueField = "CGNID";
                lstmygroup.DataBind();

                lstmygroup.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindEmployeeDetails()
    {
        DataSet ds = new DataSet();
        objH = new c_HR();
        try
        {
            objH.GetEmployeeList(ddlEmployee);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion
}