using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
using System.IO;
using eplus.core;

public partial class Pages_Email : System.Web.UI.Page
{
    #region "Declaration"
    cSession objSession = new cSession();
    EmailAndSmsAlerts objES;
    c_HR objH;
    cSession _objSession = new cSession();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region "Page Events"

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

    #endregion

    #region "GridView Events"

    #endregion

    #region "Button Events"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objES = new EmailAndSmsAlerts();
        try
        {
            AutomatedMessageMail();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Dropdown Methods

    protected void ddlEmployee_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objH = new c_HR();
        try
        {
            ds = objH.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(ddlEmployee.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
                txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('information','Email ID Is Not Available');", true);
                txtEmail.Text = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //private void loadBox()
    //{
    //    objAC.CollegeMasterID = Session["cmid"].ToString();
    //    ds = objAC.getDepartment(objAC);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        lstDepartments.DataSource = ds.Tables[0];
    //        lstDepartments.DataTextField = "DepartmentName";
    //        lstDepartments.DataValueField = "DepartmentId";
    //        lstDepartments.DataBind();
    //    }

    //    objAC.CollegeMasterID = Session["cmid"].ToString();
    //    objAC.collegeAcaID = Convert.ToInt64(Session["collegeAcaID"].ToString());
    //    ds = objAC.getCourseDetails(objAC);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        lstCourse.DataSource = ds.Tables[0];
    //        lstCourse.DataTextField = "coursename";
    //        lstCourse.DataValueField = "CourseId";
    //        lstCourse.DataBind();
    //    }

    //    objAC.CollegeMasterID = Session["cmid"].ToString();
    //    objAC.Status = 1;
    //    objAC.UserID = Session["id"].ToString();
    //    ds = objAC.getMyGroup(objAC);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        lstmygroup.DataSource = ds.Tables[0];
    //        lstmygroup.DataTextField = "GroupName";
    //        lstmygroup.DataValueField = "AGID";
    //        lstmygroup.DataBind();
    //    }
    //    objAC.GetDepartmentBatchWise(lstBatchs, objAC);
    //}

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

    #region Common Methods

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

    public void AutomatedMessageMail()
    {
        DataSet ds = new DataSet();
        DataSet dsDG = new DataSet();
        string returnvalue = "";
        string Flag = "";
        string DepartmentIds = "";
        string GroupNameIds = "";
        try
        {
            string strFile = "";
            //if (txtEmail.Text.Trim().ToString() != "" && ddlEmployee.SelectedIndex != 0)
            //{
            objES.dtSettings = objES.GetEmailSettings();

            objES.AlertType = "Mail";
            if (rbtnMode.SelectedValue == "1")
            {
                if (txtEmail.Text.Trim().ToString() != "" && ddlEmployee.SelectedIndex != 0)
                    Flag = "0";
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ErrorMessage('Error','Field Reqired');", true);
                objES.EntryMode = "Individual";
            }
            else
            {
                objH = new c_HR();
                objES.EntryMode = "Group";
                if (rbEmailgroup1.SelectedValue == "1")
                {
                    if (lstDepartments.SelectedValue != "0")
                    {
                        foreach (ListItem li in lstDepartments.Items)
                        {
                            if (li.Selected)
                            {
                                if (DepartmentIds == "")
                                    DepartmentIds = lstDepartments.SelectedValue;
                                else
                                    DepartmentIds = DepartmentIds + ',' + lstDepartments.SelectedValue;
                            }
                        }

                        objH.departmentids = DepartmentIds;
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
                                if (GroupNameIds == "")
                                    GroupNameIds = lstDepartments.SelectedValue;
                                else
                                    GroupNameIds = GroupNameIds + ',' + lstmygroup.SelectedValue;
                            }
                        }

                        objH.GroupNameIds = GroupNameIds;
                        dsDG = objH.GetEmployeeDetailsByGroupNameIDs();
                        Flag = "0";
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ErrorMessage('Error','Select Group Name');", true);
                }
            }

            if (fuAttach.HasFile)
            {
                cSales objSales = new cSales();
                cCommon objc = new cCommon();
                string CommunicationSavePath = ConfigurationManager.AppSettings["CommunicationSavePath"].ToString();
                string CommunicationHttpPath = ConfigurationManager.AppSettings["CommunicationHttpPath"].ToString();

                objc.Foldername = CommunicationSavePath;
                string Name = Path.GetFileName(fuAttach.PostedFile.FileName);

                string MaximumAttacheID = objSales.GetMaximumAttachementID();
                string[] extension = Name.Split('.');
                Name = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                objc.FileName = Name;
                objc.PID = ddlEmployee.SelectedValue;
                objc.AttachementControl = fuAttach;
                objc.SaveFiles();
                strFile = CommunicationSavePath + ddlEmployee.SelectedValue + "\\" + Name;
                objES.file = strFile;
            }
            else
                objES.file = "";

            if (Flag == "0")
            {
                if (rbtnMode.SelectedValue == "1")
                {
                    objES.reciverID = ddlEmployee.SelectedValue;
                    objES.reciverType = "Staff";
                    objES.EmailID = txtEmail.Text;
                    objES.Subject = txtSubject.Text;
                    objES.Message = txtMessage.Text;
                    objES.GroupID = 0;
                    objES.userID = _objSession.employeeid;
                    objES.ReceiverGroup = "";
                    objES.SendIndividualMail();
                    objES.Status = 1;
                    returnvalue = objES.SaveCommunicationEmailAlertDetails();
                }
                else
                {
                    for (int i = 0; i <= dsDG.Tables[0].Rows.Count; i++)
                    {
                        objES.reciverID = dsDG.Tables[0].Rows[i]["EmployeeID"].ToString();
                        objES.reciverType = "Staff";
                        objES.EmailID = dsDG.Tables[0].Rows[i]["Email"].ToString();//"karthikucev2014@gmail.com";//;
                        objES.Subject = txtSubject.Text;
                        objES.Message = txtMessage.Text;
                        objES.GroupID = Convert.ToInt32(rbEmailgroup1.SelectedValue.ToString());
                        objES.userID = _objSession.employeeid;

                        if (rbEmailgroup1.SelectedValue == "1")
                            objES.ReceiverGroup = DepartmentIds;
                        else
                            objES.ReceiverGroup = GroupNameIds;
                        objES.SendIndividualMail();
                        objES.Status = 1;
                        returnvalue = objES.SaveCommunicationEmailAlertDetails();
                    }
                }

                if (returnvalue == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Mail Dispatched Succssfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occurerd');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    #endregion
}