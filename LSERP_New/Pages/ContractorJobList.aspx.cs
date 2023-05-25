using eplus.core;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ContractorJobList : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cCommon _objc = new cCommon();
    cSales objSales = new cSales();
    cProduction objP = new cProduction();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                GetContractorListDetails();
                ShowHideControls("view");
            }
            if (target == "ViewJobComplete")
            {
                BindMaterialPlanningDetailsForContractor(arg.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPartPopUp();", true);
            }
            if (target == "ViewJobInComplete")
            {
                BindMaterialPlanningDetailsForIncomplete(arg.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPartPopUp();", true);
            }
            if (target == "JobOrder")
            {
                BindContractorJobcardPDFDetailsByRFPHID(arg.ToString());
                ShowHideControls("view");
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvContractorJobList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "ADD")
            {
            }
        }
        catch
        {

        }
    }

    #endregion

    #region"Common Methods"

    private void BindMaterialPlanningDetailsForContractor(string RFPDID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(RFPDID);

            ds = objP.GetMaterialPlanningDetailsByRFPHIDAndEDIDForContractor();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialPlanningDetails.DataSource = ds.Tables[0];
                gvMaterialPlanningDetails.DataBind();
                //btnMaterialPlanningStatus.Visible = true;
            }
            else
            {
                gvMaterialPlanningDetails.DataSource = "";
                gvMaterialPlanningDetails.DataBind();
               // btnMaterialPlanningStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindMaterialPlanningDetailsForIncomplete(string RFPDID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(RFPDID);

            ds = objP.GetMaterialPlanningDetailsByRFPHIDAndEDIDForIncomlete();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialPlanningDetails.DataSource = ds.Tables[0];
                gvMaterialPlanningDetails.DataBind();
                //btnMaterialPlanningStatus.Visible = true;
            }
            else
            {
                gvMaterialPlanningDetails.DataSource = "";
                gvMaterialPlanningDetails.DataBind();
                // btnMaterialPlanningStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetailsByPRIDID()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
          

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void GetContractorListDetails()
    {
        try
        {
            DataSet dsContractorListt = new DataSet();

            dsContractorListt = objP.GetContractorJobListDetails();

            if (dsContractorListt.Tables[0].Rows.Count > 0)
            {
                gvContractorJobList.DataSource = dsContractorListt.Tables[0];
                gvContractorJobList.DataBind();
                gvContractorJobList.UseAccessibleHeader = true;
                gvContractorJobList.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
            }
            else
            {
                gvContractorJobList.DataSource = "";
                gvContractorJobList.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAssemplyJobcardPDFDetailsByJCHID(string JCHID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            Session["JCHID"] = JCHID;
          //  Session["RFPHID"] = ddlRFPNo.SelectedValue;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "JobCardContractorExpenses.aspx");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void BindContractorJobcardPDFDetailsByRFPHID(string RFPDID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            Session["RFPDID"] = RFPDID;
            //  Session["RFPHID"] = ddlRFPNo.SelectedValue;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "ContractorJobAllList.aspx?RFPDID=" + RFPDID);

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string mode)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "edit":
                    divInput.Visible = true;
                    break;
                case "view":
                    divOutput.Visible = true;
                    break;
            }
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
        if (gvContractorJobList.Rows.Count > 0)
            gvContractorJobList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}