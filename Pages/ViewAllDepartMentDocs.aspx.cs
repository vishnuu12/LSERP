using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ViewAllDepartMentDocs : System.Web.UI.Page
{
    #region"PageInit Events"

    cReports objReports;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    cCommon objc;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["EnquiryID"] = Request.QueryString["EnquiryID"].ToString();
            BindAttachemntDetailsByDepartmentname("Sales");
            ShowHideControls("sales");
        }
    }

    #endregion

    #region"Button EVents"

    protected void btntab_Click(object sender, EventArgs e)
    {
        try
        {
            BindAttachemntDetailsByDepartmentname(hdnTabName.Value);

            ShowHideControls(hdnTabName.Value);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvEnquiryAttachemnts_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "ViewEnquiryDocs")
            {
                objc = new cCommon();
                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "\\";
                string FileName = gvEnquiryAttachemnts.DataKeys[index].Values[0].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName;

                // if (FileName.ToString().Split('.')[1].ToUpper() == "ZIP" || FileName.ToString().Split('.')[1].ToUpper() == "EML")
                cCommon.DownLoad(FileName, CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName);
                //else
                //    objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, ViewState["EnquiryID"].ToString(), ifrm);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvEnquiryOfferDocs_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pdffile = "";
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string lblOfferNo = gvEnquiryOfferDocs.DataKeys[index].Values[0].ToString();

            if (e.CommandName.ToString() == "ViewOfferDetails")
            {
                pdffile = lblOfferNo + ".html";
                ReadhtmlFile(pdffile);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCustomerPODocs_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FileName;
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            int EnquiryNumber = Convert.ToInt32(gvCustomerPODocs.DataKeys[index].Values[0].ToString());
            string BaseHtttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";
            if (e.CommandName == "ViewPOCopy")
            {
                FileName = gvCustomerPODocs.DataKeys[index].Values[1].ToString();
                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
            }
            if (e.CommandName == "ViewPoCopyWithoutPrice")
            {
                FileName = gvCustomerPODocs.DataKeys[index].Values[2].ToString();
                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvItemostingDetailsBOM_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "ViewCosting")
            {
                string DDID = gvItemostingDetailsBOM.DataKeys[index].Values[0].ToString();
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "ViewCostingSheet.aspx?DDID=" + DDID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            if (e.CommandName.ToString() == "ViewFileName")
            {
                string FileName = gvItemostingDetailsBOM.DataKeys[index].Values[1].ToString();

                ViewDrawingFilename(FileName);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvRFPSheets_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "ViewFileName")
            {
                objc = new cCommon();

                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "/";
                string FileName = gvRFPSheets.DataKeys[index].Values[1].ToString();

                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, ViewState["EnquiryID"].ToString(), ifrm);
            }
            if (e.CommandName.ToString() == "viewRFPPrint")
            {
                string RFPHID = gvRFPSheets.DataKeys[index].Values[0].ToString();
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.ToLower();

                string[] pagename = url.ToString().Split('/');
                string Replacevalue = pagename[pagename.Length - 1].ToString();

                string Page = url.Replace(Replacevalue, "RFPPrint.aspx?RFPHID=" + RFPHID + "");

                string s = "window.open('" + Page + "','_blank');";
                this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ShowHideControls(string divname)
    {
        Sales.Visible = Design.Visible = Production.Visible = Quality.Visible = false;
        try
        {
            switch (divname.ToLower())
            {
                case "sales":
                    Sales.Visible = true;
                    break;
                case "design":
                    Design.Visible = true;
                    break;
                case "production":
                    Production.Visible = true;
                    break;
                case "quality":
                    Quality.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAttachemntDetailsByDepartmentname(string tabname)
    {
        DataSet ds = new DataSet();
        try
        {
            if (tabname == "Sales")
                BindSalesAttachementDetails();
            if (tabname == "Design")
                BindDesignAttachemntDetails();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tabs", "ActiveTab('" + tabname + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tabs", "ActiveTab('" + tabname + "');", true);
        }
    }

    private void BindDesignAttachemntDetails()
    {
        DataSet ds = new DataSet();
        objReports = new cReports();
        try
        {
            objReports.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"].ToString());

            ds = objReports.GetDepartmentDocumentDetailsByEnquiryID("LS_GetDesignDepartmentDocumentDetailsByEnquiryID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItemostingDetailsBOM.DataSource = ds.Tables[0];
                gvItemostingDetailsBOM.DataBind();
            }
            else
            {
                gvItemostingDetailsBOM.DataSource = "";
                gvItemostingDetailsBOM.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvRFPSheets.DataSource = ds.Tables[1];
                gvRFPSheets.DataBind();
            }
            else
            {
                gvRFPSheets.DataSource = "";
                gvRFPSheets.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindSalesAttachementDetails()
    {
        DataSet ds = new DataSet();
        objReports = new cReports();
        try
        {
            objReports.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"].ToString());

            ds = objReports.GetDepartmentDocumentDetailsByEnquiryID("LS_GetSalesDepartmentDocumentDetailsByEnquiryID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEnquiryAttachemnts.DataSource = ds.Tables[0];
                gvEnquiryAttachemnts.DataBind();
            }
            else
            {
                gvEnquiryAttachemnts.DataSource = "";
                gvEnquiryAttachemnts.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvEnquiryOfferDocs.DataSource = ds.Tables[1];
                gvEnquiryOfferDocs.DataBind();
            }
            else
            {
                gvEnquiryOfferDocs.DataSource = "";
                gvEnquiryOfferDocs.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvCustomerPODocs.DataSource = ds.Tables[2];
                gvCustomerPODocs.DataBind();
            }
            else
            {
                gvCustomerPODocs.DataSource = "";
                gvCustomerPODocs.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ReadhtmlFile(string pdffile)
    {
        try
        {
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string pdfpath = LetterPath + pdffile;

            if (File.Exists(pdfpath))
            {
                string fileName = pdfpath;
                StreamReader reader = new StreamReader(fileName);

                string pathToHTMLFile = pdfpath;
                StringBuilder sb = new StringBuilder();

                StreamReader sr = new StreamReader(pdfpath);
                string htmlString = sr.ReadToEnd();
                htmlString = htmlString.Replace(Environment.NewLine, "");
                htmlString = htmlString.Replace("\n", String.Empty);
                htmlString = htmlString.Replace("\r", String.Empty);
                htmlString = htmlString.Replace("\t", String.Empty);

                sr.Close();
                sr.Dispose();

                hdnpdfContent.Value = htmlString;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintOffer();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "InfoMessage('Information','File Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewDrawingFilename(string FileName)
    {
        objc = new cCommon();
        try
        {
            string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString().Trim();
            string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

            string Savepath = DrawingDocumentSavePath;
            string httpPath = DrawingDocumentHttpPath;
            string EnquiryID = ViewState["EnquiryID"].ToString();

            objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttpPath, FileName, EnquiryID, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}