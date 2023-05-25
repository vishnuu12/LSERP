using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AsynFileupload : System.Web.UI.Page
{

    #region"Declare"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void FileUploadComplete(object sender, EventArgs e)
    {
        try
        {
            string filename = System.IO.Path.GetFileName(asynF.FileName);
         //   asynF.SaveAs(Server.MapPath("~/Assets/images/") + filename);

            asynF.Style.Add("background-color", "none;");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
}

    #endregion