using System;
using System.IO;
using System.Data;
using System.Text;
using eplus.core;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Master_CommonMaster : System.Web.UI.MasterPage
{
    DataSet ds = new DataSet();
    private cSession cs = null;

    public cSession csSession
    {
        get
        {
            return (cs == null) ? cs = (cSession)Session["LoginDetails"] : cs;
        }
    }

    cCommonMaster objcom = new cCommonMaster();
    string projecturl = ConfigurationManager.AppSettings["Url"].ToString();
    string[] dotArray = { "danger", "warning", "success", "primary", "info", "danger", "dark" };
    int rowcount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string s = Session["LoginDetails"] == null || Session["LoginDetails"].ToString() == "" ? "<script>window.open('Sessionexpired.aspx','bottom');</script>" : "";
        //s = "<script>window.open('Sessionexpired.aspx','bottom');</script>";
        if (Session["LoginDetails"] == null)
        {
            Response.Redirect("Login.aspx", true);
            //this.Response.Write(s);
            //this.Response.End();
        }
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now;
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";

        if (!IsPostBack)
        {
            cs = (cSession)Session["LoginDetails"];
            BindSchoolDetails();
            LoadSideMenu();
            getNotifications();
            getEvents();

        }
    }

    private void BindSchoolDetails()
    {
        SchoolName.InnerText = "Lonestar Industries";
        imgschoollogo.Src = "~\\assets\\images\\lonestar.png";

        spanUserName.InnerText = cs.name;
        spanUserType.InnerText = cs.Designation;

        imgheaduserphoto.Src = imguserphoto.Src = projecturl + "\\Images\\no-photo.png";
        if (!string.IsNullOrEmpty(cs.UserPhoto))
        {
            string userPhotopath = projecturl + "\\EmployeeDocs\\" + cs.employeeid + "\\" + cs.UserPhoto;
            if (objcom.URLExists(userPhotopath))
                imgheaduserphoto.Src = imguserphoto.Src = userPhotopath;

        }

        liLastLoginTime.InnerText = "Last Login on : " + cs.lastLoginDateTime;
    }

    private void LoadSideMenu()
    {

        objcom.userType = cs.type.ToString();
        ds = objcom.getMenuPagesByUserType();     
        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            Session["UserModules"] = ds.Tables[1];
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='vertical-nav-menu'><li class='app-sidebar__heading'>Menu</li>");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataTable dt = ds.Tables[1];
                dt.DefaultView.RowFilter = "MID=" + dr["MID"].ToString();
                if (dt.DefaultView.ToTable().Rows.Count > 0)
                {
                    sb.Append("<li class=''>");
                    //sb.Append("<a href='#'><i class='metismenu-icon' style='opacity: 10 !important;'><img src='../Assets/images/" + dr["ModuleImage"].ToString() + "' width='24' height='24' /></i>" + dr["ModuleName"].ToString() + "<i class='metismenu-state-icon pe-7s-angle-down caret-left'></i></a>");
                    sb.Append("<a href='#'><i class='metismenu-icon fas " + dr["ModuleIcon"].ToString() + "' style='opacity: 10 !important;'></i>" + dr["ModuleName"].ToString() + "<i class='metismenu-state-icon pe-7s-angle-down caret-left'></i></a>");
                    sb.Append("<ul>");
                    foreach (DataRow drpage in dt.DefaultView.ToTable().Rows)
                    {
                        sb.Append("<li><a href=" + drpage["PageReference"].ToString() + ">" + drpage["PageDisplayinMenu"].ToString() + "</a></li>");
                        //<i class='" + drpage["PageIcon"].ToString() + "'></i> 
                    }
                    sb.Append("</ul></li>");
                }
            }
            sb.Append("</ul>");
            divMenu.InnerHtml = sb.ToString();
        }
    }

    private void getNotifications()
    {
        StringBuilder sb = new StringBuilder();

        objcom.Type = "Head";
        //        ds = objcom.getNotifications();
        //        rowcount = 0;
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            sb.Append(@"<div class='vertical-timeline-item dot-" + dotArray[rowcount] + @" vertical-timeline-element'>
        //                              <div>
        //                                     <span class='vertical-timeline-element-icon bounce-in'></span>
        //                                     <div class='vertical-timeline-element-content bounce-in'>
        //                                        <h4 class='timeline-title'><a style='color:#6c757d;' href='" + projecturl + "/Notifications/" + row["FileName"].ToString() + @"'>
        //                                          " + row["AnnouncementTitle"].ToString() + @"</a></h4>
        //                                      <span class='vertical-timeline-element-date'></span>
        //                                      </div>
        //
        //                                 </div>
        //                         </div>");
        //            rowcount++;
        //        }
        //        divNotifications.InnerHtml = sb.ToString();
        divNotifications.InnerHtml = "";
    }

    private void getEvents()
    {
        StringBuilder sb = new StringBuilder();

        objcom.Type = "Head";
        //        ds = objcom.getEvents();
        //        rowcount = 0;
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            sb.Append(@"<div class='vertical-timeline-item vertical-timeline-element'>
        //                            <div>
        //                                <span class='vertical-timeline-element-icon bounce-in'><i class='badge badge-dot badge-dot-xl badge-" + dotArray[rowcount] + @"'>
        //                                  </i></span>
        //                                    <div class='vertical-timeline-element-content bounce-in'>
        //                                       <h4 class='timeline-title'>
        //                                           " + row["EventName"].ToString() + @"</h4>
        //                                             <p>
        //                                            " + row["Activity"].ToString() + @"</p>
        //                                     <span class='vertical-timeline-element-date'></span>
        //                                   </div>
        //                             </div>
        //                          </div>");
        //            rowcount++;
        //        }
        //        divEvents.InnerHtml = sb.ToString();
        divEvents.InnerHtml = "";
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}
