using eplus.core;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_IndentToQuotationReports : System.Web.UI.Page
{
    #region"Declaration"

    cPurchase objPur;
    cSession objSession;
    cCommon objc;
    bool IsPageRefresh = false;
    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"pageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objPur = new cPurchase();
                DataSet dsPINum = new DataSet();
                dsPINum = objPur.GetQNNumberReports(ddlQNNumber, Convert.ToInt32(objSession.type));
                ShowHideControls("add");
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
            }
            else
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button events"
    protected void VieAttachment_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            string HttpPath = Session["PurchaseDocsHttpPath"].ToString();
            string SavePath = Session["PurchaseDocsSavePath"].ToString();
            objc.ViewFileName(SavePath, HttpPath, hdnFileName.Value, ddlQNNumber.SelectedValue, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlQNNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlQNNumber.SelectedIndex > 0)
            {
                bindPurchaseIndentDetailsByQNumber();
                ShowHideControls("add,input,view");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDataTable", "ShowDataTable();", true);
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Comon Methods"

    private void bindPurchaseIndentDetailsByQNumber()
    {
        objPur = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            objPur.QHID = Convert.ToInt32(ddlQNNumber.SelectedValue);
            objPur.VendorType = objSession.type;
            ds = objPur.GetPurchaseIndentDetailsByQNNumberReports();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQuateSubmission.DataSource = ds.Tables[0];
                gvQuateSubmission.DataBind();
            }
            else
            {
                gvQuateSubmission.DataSource = "";
                gvQuateSubmission.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                lblRFPNo.Text = ds.Tables[2].Rows[0]["RFPNo"].ToString();
                lblindentby.Text = ds.Tables[2].Rows[0]["IndentBy"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvQuateSubmission.Rows.Count > 0)
            gvQuateSubmission.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}