<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<script RunAt="server">   

    protected void Application_Start(object sender, EventArgs e)
    {
        //String WebsiteURL = Request.Url.ToString();
        RegisterRoutes();
       
    }

    private static void RegisterRoutes()
    {
        string myConnection = WebConfigurationManager.ConnectionStrings["con"].ConnectionString;
        SqlDataAdapter cmd1 = new SqlDataAdapter("select pagename, pagereference from LSE_PageDefinition", myConnection);

        DataSet ds = new DataSet();
        cmd1.Fill(ds, "MenuPages");
        DataTable table = ds.Tables["MenuPages"];
        System.Web.Routing.RouteTable.Routes.Ignore("{resource}.axd/{*pathInfo}");
        System.Web.Routing.RouteTable.Routes.Add(new System.Web.Routing.Route("{resource}.axd/{*pathInfo}", new System.Web.Routing.StopRoutingHandler()));
        foreach (DataRow dr in table.Rows)
        {
            System.Web.Routing.RouteTable.Routes.Add(dr["pagereference"].ToString(), new System.Web.Routing.Route("pages/" + dr["pagereference"].ToString(), new PageRouteHandler("~/pages/" + dr["pagename"].ToString())));
        }
    }
</script>
