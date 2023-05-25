using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;
using System.IO;

public partial class Pages_MyInBox : System.Web.UI.Page
{

    #region "Declaration"

    EmailAndSmsAlerts objES;
    c_HR objH;
    cSession objSession = new cSession();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Evetns"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindAlertDetails();
        }
    }

    #endregion

    #region"Common Methods"

    private void BindAlertDetails()
    {
        DataSet ds = new DataSet();
        objES = new EmailAndSmsAlerts();
        try
        {
            objES.userID = objSession.employeeid;
            objES.type = "InBox";

            if (Session["DailyAlert"] == null)
                objES.DailyStatus = "All";
            else
                objES.DailyStatus = "DailyAlert";

            ds = objES.GetAlertDetailsByUserID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMyInbox.DataSource = ds.Tables[0];
                gvMyInbox.DataBind();
            }
            else
            {
                gvMyInbox.DataSource = "";
                gvMyInbox.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnViewAttachements(object sender, EventArgs e)
    {
        cCommon objc = new cCommon();
        try
        {
            string CommunicationSavePath = ConfigurationManager.AppSettings["CommunicationSavePath"].ToString();
            string CommunicationHttpPath = ConfigurationManager.AppSettings["CommunicationHttpPath"].ToString();

            //SenderID();
            //objc.ViewFileName(CommunicationSavePath, CommunicationHttpPath,);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMyInbox_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "ViewAttch")
            {
                string CommunicationSavePath = ConfigurationManager.AppSettings["CommunicationSavePath"].ToString();
                string CommunicationHttpPath = ConfigurationManager.AppSettings["CommunicationHttpPath"].ToString();

                cCommon objcommon = new cCommon();

                string FileName = gvMyInbox.DataKeys[index].Values[0].ToString();
                if (File.Exists(CommunicationSavePath + FileName))
                    cCommon.DownLoad(FileName, CommunicationSavePath + FileName);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','File Not Found');", true);
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
        if (gvMyInbox.Rows.Count > 0)
        {
            gvMyInbox.UseAccessibleHeader = true;
            gvMyInbox.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion

}