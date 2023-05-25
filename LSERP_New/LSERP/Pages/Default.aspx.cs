using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

public partial class Pages_Default : System.Web.UI.Page
{

    #region "PageLoad Events"


    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
            return;
        else
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Address");
            dt.Columns.Add("City");
            dt.Rows.Add(1, "sivashankari", "b", "fghghh", "chennai");
            dt.Rows.Add(2, "cccccccccyyyyyyhhhhhy", "d", "rrr", "mumbai");
            dt.Rows.Add(3, "e", "f", "tyyyyy", "madurai");
            dt.Rows.Add(4, "g", "h", "gggg", "bangalore");
            dt.Rows.Add(5, "i", "j", "jjjjj", "chennai");
            dt.Rows.Add(6, "k", "l", "kkkkkk", "mumbai");
            dt.Rows.Add(7, "m", "n", "xxxxx", "chennai");
            dt.Rows.Add(8, "o", "p", "zzzzz", "Pune");
            dt.Rows.Add(9, "o", "p", "ggggg", "chennai");
            dt.Rows.Add(10, "fff", "gf", "nnnnnn", "Tanjore");
            dt.Rows.Add(11, "gggg", "yhy", "ttrtrt", "Trichy");
            dt.Rows.Add(12, "eeee", "jjjp", "rtgyh", "Delhi");
            dt.Rows.Add(13, "rrrr", "rrrr", "fghgjj", "Trichy");
            gvDefault.DataSource = dt;
            gvDefault.DataBind();
        }

    }

    #endregion
    #region "GridView Events"

    protected void gvDefault_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Attributes.Add("style", "width:30px");
            e.Row.Cells[1].Attributes.Add("style", "width:200px");
            e.Row.Cells[2].Attributes.Add("style", "width:50px");
            e.Row.Cells[3].Attributes.Add("style", "width:70px");
            e.Row.Cells[4].Attributes.Add("style", "width:100px");
        }
    }
    #endregion
}


