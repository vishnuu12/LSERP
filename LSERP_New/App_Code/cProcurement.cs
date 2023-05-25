using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
 
public class cProcurement
{
    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();

    #endregion

    #region "Properties"
    #endregion

    #region "Common Methods"

    #endregion
}