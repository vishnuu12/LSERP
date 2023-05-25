using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;


public partial class Pages_CompanyDetailsReport : System.Web.UI.Page
{
    #region"Declarartion"

    cReports objReport;

    #endregion

    #region"Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindEnquiryChartDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnViewDetails_Click(object sender, EventArgs e)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "CompanySummaryReport.aspx");

            string s = "window.open('" + Main + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            BindEnquiryChartDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Method"

    private void BindEnquiryChartDetails()
    {
        objReport = new cReports();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        try
        {
            ds = objReport.GetCompanyDetailsReport();

            dt = (DataTable)ds.Tables[0];

            string[] XPointMember = new string[ds.Tables[0].Rows.Count];
            int[] YPointMember = new int[ds.Tables[0].Rows.Count];

            //for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
            //{
            //    //storing Values for X axis    
            //    XPointMember[count] = ds.Tables[0].Rows[count]["Name"].ToString();
            //    //storing values for Y Axis    
            //    YPointMember[count] = Convert.ToInt32(ds.Tables[0].Rows[count]["value"].ToString());
            //}
            ////binding chart control            

            //Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            //// Chart1.Series[0].IsValueShownAsLabel = true;

            //Chart1.Series["Series1"].ToolTip = "Y = #VALY\nX = #VALX";

            //Chart1.Series["Series1"].Label = "Y = #VALY\nX = #VALX";

            ////Chart1.Series[0]

            ////Chart1.Series[0].IsXValueIndexed = true;

            ////Chart1.Series[0].Points.Add()

            ////setting Chart type     
            //// Chart1.Series[0].ChartType = SeriesChartType.Pyramid;
            ////Chart1.Series[0].ChartType = SeriesChartType.Funnel;
            //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            string JsonString = serializer.Serialize(rows);

            //JsonString = JsonString;

            hdndataString.Value = JsonString;


            gvCompanyDetailsReport.DataSource = ds.Tables[0];
            gvCompanyDetailsReport.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}