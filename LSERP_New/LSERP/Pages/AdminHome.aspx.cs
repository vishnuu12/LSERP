using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using eplus.data;

public partial class AdminHome : System.Web.UI.Page
{
    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            saveNavigationInfo();
            GetDashBoardColor();
        
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetDashBoardColor()
    {
        cCommonMaster _objCommon = new cCommonMaster();
        DataSet ds = _objCommon.getdashboardcolor();
        ViewState["DashBoard"] = ds;
    }


    public void saveNavigationInfo()
    {
        //string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
    }

}