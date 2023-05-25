using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_MessageOutBox : System.Web.UI.Page
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
            objES.type = "OutBox";
            ds = objES.GetAlertDetailsByUserID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMessageOutbox.DataSource = ds.Tables[0];
                gvMessageOutbox.DataBind();
            }
            else
            {
                gvMessageOutbox.DataSource = "";
                gvMessageOutbox.DataBind();
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
        if (gvMessageOutbox.Rows.Count > 0)
        {
            gvMessageOutbox.UseAccessibleHeader = true;
            gvMessageOutbox.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}