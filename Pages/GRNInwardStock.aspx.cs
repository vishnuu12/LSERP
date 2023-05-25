using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GRNInwardStock : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cPurchase objPc;
    cCommon objc;
    cQuality objQt;
    cStores objSt;

    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objPc = new cPurchase();
                objc = new cCommon();
                objc.GetPODetails(ddlPO, "0");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblMRNChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            BindAddedMaterials();
            objQt.mrnchangestatus = rblMRNChange.SelectedValue;

            objc.GetPODetails(ddlPO, objQt.mrnchangestatus);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlPO_OnSelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlPO.SelectedIndex > 0)
            BindAddedMaterials();
        else
        {
            gvaddeditems.DataSource = "";
            gvaddeditems.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
    }

    #endregion

    #region"Button Events"

    protected void btnShareStock_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        string SPODID = "";
        string MIDID = "";
        string MRNNumber = "";
        string MRNID = "";
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];


            foreach (GridViewRow row in gvaddeditems.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkQC");

                if (chkitems.Checked)
                {
                    if (SPODID == "")
                    {
                        SPODID = gvaddeditems.DataKeys[row.RowIndex].Values[0].ToString();
                        MIDID = gvaddeditems.DataKeys[row.RowIndex].Values[1].ToString();
                        MRNNumber = gvaddeditems.DataKeys[row.RowIndex].Values[3].ToString();
                        MRNID = gvaddeditems.DataKeys[row.RowIndex].Values[4].ToString();
                    }
                    else
                    {
                        SPODID = SPODID + ',' + gvaddeditems.DataKeys[row.RowIndex].Values[0].ToString();
                        MIDID = MIDID + ',' + gvaddeditems.DataKeys[row.RowIndex].Values[1].ToString();
                        MRNNumber = MRNNumber + ',' + gvaddeditems.DataKeys[row.RowIndex].Values[3].ToString();
                        MRNID = MRNID + ',' + gvaddeditems.DataKeys[row.RowIndex].Values[4].ToString();
                    }
                }
            }

            objQt.SPODIDS = SPODID;
            objQt.MIDIDS = MIDID;
            objQt.MRNNumbers = MRNNumber;
            objQt.MRNIDS = MRNID;
            objQt.employeeid = Convert.ToInt32(objSession.employeeid);
            ds = objQt.UpdateGeneralStockStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Inward Status Updated Successfully');", true);
                BindAddedMaterials();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindAddedMaterials()
    {
        objQt = new cQuality();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            objQt.mrnchangestatus = rblMRNChange.SelectedValue;
            objQt.SPODID = Convert.ToInt32(ddlPO.SelectedValue);

            ds = objQt.GetInwardedMaterialDetailsByLocationIDForgeneral();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MIQCDetails"] = ds.Tables[0];
                gvaddeditems.DataSource = ds.Tables[0];
                gvaddeditems.DataBind();
                if (objQt.mrnchangestatus == "0")
                {
                    gvaddeditems.Columns[0].Visible = true;
                    btnShareStock.Visible = true;
                }

                else
                {
                    gvaddeditems.Columns[0].Visible = false;
                    btnShareStock.Visible = false;
                }
            }
            else
            {
                gvaddeditems.DataSource = "";
                gvaddeditems.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string Main, string epstyleurl, string style, string Print, string topstrip, string div)
    {
        try
        {
            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("<style type='text/css'/>  .Qrcode{ float: right; } label{ color: black ! important;font-weight: bold;} p{ margin:0;padding:0; }  @page { size: landscape; } span { padding: 0px 0px; } </style>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;margin:0 auto'>");
            w.WriteLine("<div>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
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
        if (gvaddeditems.Rows.Count > 0)
            gvaddeditems.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}