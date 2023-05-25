using System;
using System.IO;
using System.Data;
using System.Text;
using eplus.core;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;



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

        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        Session["PurchaseDocsSavePath"] = ConfigurationManager.AppSettings["PurchaseDocsSavePath"].ToString();
        Session["PurchaseDocsHttpPath"] = ConfigurationManager.AppSettings["PurchaseDocsHttpPath"].ToString();
        Session["MPDocsSavePath"] = ConfigurationManager.AppSettings["MPDocsSavePath"].ToString();
        Session["MPDocsHttpPath"] = ConfigurationManager.AppSettings["MPHttpPath"].ToString();

        Session["WorkOrderIndentSavePath"] = ConfigurationManager.AppSettings["WorkOrderIndentSavePath"].ToString();
        Session["WorkOrderIndentHttpPath"] = ConfigurationManager.AppSettings["WorkOrderIndentHttpPath"].ToString();

        Session["ModuleID"] = hdnModuleID.Value;

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
        if (Request.UrlReferrer == null && Session["Currentpage"].ToString() != Path.GetFileName(Request.Path))
        {
            //Server.Transfer("~/ErrorPages/Accesdenied.html");
            //throw new HttpException(401, "Auth Failed");
            // Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        Session["Currentpage"] = Path.GetFileName(Request.Path);
        if (!IsPostBack)
        {
            cs = (cSession)Session["LoginDetails"];
            BindSchoolDetails();
            LoadSideMenu();
            getNotifications();
            getEvents();
        }

        if (target == "ChangPassword")
            Response.Redirect("changepassword.aspx", false);
        if (target == "PDF")
        {
            string Content = arg.ToString();
            PDFDownLoad(Content);
        }

        if (target == "Excel")
        {
            string JsonData = arg.ToString().Trim();

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(JsonData);
            ExcelDownLoad(dt);
        }
        if (target == "MsgOut")
            Response.Redirect("MessageOutBox.aspx", false);
        if (target == "MyInBox")
            Response.Redirect("MyInBox.aspx", false);

        //Response.Redirect();
        saveNavigationInfo();
    }

    public void saveNavigationInfo()
    {
        try
        {
            string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
            objcom = new cCommonMaster();
            objcom.PageName = pageName;
            objcom.UserID = cs.employeeid;
            objcom.saveNavigation(objcom);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void BindSchoolDetails()
    {
        SchoolName.InnerText = "LSI-MECH ENGINEERS PVT. LTD.";
        imgschoollogo.Src = "~\\assets\\images\\lonestar.png";

        spanUserName.InnerText = cs.name;
        spanUserType.InnerText = cs.Designation;

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
        objcom.Designationname = cs.Designation;
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
                        sb.Append("<li><a href=" + drpage["PageReference"].ToString() + " name='" + dr["MID"].ToString() + "' onclick='GetMIDValue();'>" + drpage["PageDisplayinMenu"].ToString() + "</a></li>");
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


    private void PDFDownLoad(string Content)
    {
        DataSet ds = new DataSet();
        cCommon _objc = new cCommon();
        try
        {
            string MAXPDFID = "";

            var sb = new StringBuilder();

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);

            string url = HttpContext.Current.Request.Url.AbsoluteUri;

            url = url.ToLower();
            string[] pagename = url.Split('/');
            string Main = url.Replace("pages/" + pagename[pagename.Length - 1] + "", "Assets/css/main.css");
            string epstyleurl = url.Replace("pages/" + pagename[pagename.Length - 1] + "", "Assets/css/ep-style.css");
            string style = url.Replace("pages/" + pagename[pagename.Length - 1] + "", "Assets/css/style.css");
            string Print = url.Replace("pages/" + pagename[pagename.Length - 1] + "", "Assets/css/print.css");
            string topstrip = url.Replace("pages/" + pagename[pagename.Length - 1] + "", "Assets/images/topstrrip.jpg");

            _objc.Main = Main;
            _objc.epstyleurl = epstyleurl;
            _objc.style = style;
            _objc.Print = Print;
            _objc.topstrip = topstrip;

            _objc.SaveHtmlFile(URL, "", "", Content);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile(pagename[pagename.Length - 1], pdffile, cs.employeeid);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ExcelDownLoad(DataTable dt)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

}
