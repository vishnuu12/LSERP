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
using eplus.core;

public partial class Pages_RFPPrint : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc = new cCommon();
    cDesign objDesign;
    cSales objSales;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                ViewState["RFPHID"] = Request.QueryString["RFPHID"].ToString();

                BindRFPPrintDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion    

    #region"GridView Events"

    protected void gvAnnexureBellowDetails_p_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvBellowDetails_p_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"   

    private void BindRFPPrintDetails()
    {
        DataTable dt;
        try
        {
            string RFPHID = ViewState["RFPHID"].ToString();

            objSales = new cSales();
            DataSet dsItem = new DataSet();
            dsItem = objSales.GetRFPItemdetails(RFPHID);

            DataSet dsrfpdetails = new DataSet();

            objSales.RFPHID = Convert.ToInt32(RFPHID);
            dsrfpdetails = objSales.GetRFPheaderDetailsByRFPHID();
            ViewState["RFPHeaderDetails"] = dsrfpdetails.Tables[0];

            dt = (DataTable)ViewState["RFPHeaderDetails"];
            hdnRFPHID.Value = RFPHID.ToString();
            dt.DefaultView.RowFilter = "RFPHID='" + RFPHID + "'";

            DataSet ds = new DataSet();
            DataTable dtItemDetails = new DataTable();

            objSales = new cSales();
            objSales.RFPHID = Convert.ToInt32(RFPHID);

            ds = objSales.getBellowsDetailsByRFPHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound" || dsItem.Tables[0].Rows.Count > 0)
            {
                lblRFPNo_P.Text = dt.DefaultView.ToTable().Rows[0]["RFPNo"].ToString() + " & " + ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();

                lblCustomerName_p.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
                lblCustomerOrderNo_p.Text = dt.DefaultView.ToTable().Rows[0]["PORefNo"].ToString();

                lblProject_p.Text = dt.DefaultView.ToTable().Rows[0]["ProjectName"].ToString();
                lblNumberOfItems_p.Text = dsItem.Tables[1].Rows[0]["TotalQuantity"].ToString();

                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    dtItemDetails = new DataTable();
                    dtItemDetails = (DataTable)dsItem.Tables[0];

                    gvItemDetails_p.DataSource = dtItemDetails;
                    gvItemDetails_p.DataBind();

                    gvAnnexureItemList_p.DataSource = dtItemDetails;
                    gvAnnexureItemList_p.DataBind();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                    {
                        ds.Tables[0].Columns.Remove("Message");
                        dtItemDetails = new DataTable();
                        dtItemDetails = (DataTable)ds.Tables[0];

                        if (dtItemDetails.Columns.Contains("Gradename"))
                            dtItemDetails.Columns["Gradename"].ColumnName = "Grade Name";
                        dtItemDetails.Columns["SNo"].ColumnName = "SL.No";

                        gvBellowDetails_p.DataSource = dtItemDetails;
                        gvBellowDetails_p.DataBind();

                        gvAnnexureBellowDetails_p.DataSource = dtItemDetails;
                        gvAnnexureBellowDetails_p.DataBind();
                    }
                    else
                    {
                        gvBellowDetails_p.DataSource = "";
                        gvBellowDetails_p.DataBind();

                        gvAnnexureBellowDetails_p.DataSource = "";
                        gvAnnexureBellowDetails_p.DataBind();
                    }
                }
                else
                {
                    gvItemDetails_p.DataSource = "";
                    gvItemDetails_p.DataBind();

                    gvBellowDetails_p.DataSource = "";
                    gvBellowDetails_p.DataBind();
                }

                lblQAPRefNo_p.Text = dt.DefaultView.ToTable().Rows[0]["QAPRefNo"].ToString();
                lblQAPApproval_p.Text = dt.DefaultView.ToTable().Rows[0]["QAPApprovalName"].ToString();
                lblDrawingApproval_p.Text = dt.DefaultView.ToTable().Rows[0]["DrawingApprovalName"].ToString();
                lblInspectionRequirtment_p.Text = dt.DefaultView.ToTable().Rows[0]["InspectionRequirtmentName"].ToString();
                lblLDClause_p.Text = dt.DefaultView.ToTable().Rows[0]["LDClauseName"].ToString();
                lblDespatchDetails_p.Text = dt.DefaultView.ToTable().Rows[0]["DespatchDetails"].ToString();
                lblMarketing_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                lblDesign_p.Text = ds.Tables[1].Rows[0]["DesignE"].ToString();
                // lblDataEntry_p.Text = ds.Tables[1].Rows[0]["PurchaseE"].ToString();
                lblProjectIncharge_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                lblApprovedBy_p.Text = ds.Tables[1].Rows[0]["RFPApprovedBy"].ToString();
                lblDueDateForDispatch_p.Text = dt.DefaultView.ToTable().Rows[0]["DeliveryDate"].ToString();//ds.Tables[1].Rows[0][""].ToString();
                lblNotesSummary_p.Text = dt.DefaultView.ToTable().Rows[0]["NotesSummary"].ToString();
                lblLocationName_p.Text = dt.DefaultView.ToTable().Rows[0]["LocationName"].ToString();
                GeneratePDF();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Add RFP item Details');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ErrorMessage('Error','Errror Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        string Flag;
        try
        {
            var sbPurchaseOrder = new StringBuilder();

            var sbGvItemAnnexure = new StringBuilder();
            var sbBellowAnnexure = new StringBuilder();

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                //imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "RFPRequest";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }

            // divPrint.Attributes.Add("style", "display:block;height:287mm;width:200mm;margin:15px;padding:15px;");
            //divPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbPurchaseOrder)));

            //if (gvAnnexureItemList_p.Rows.Count > 0)
            //{
            //    Flag = "multi";
            //    divAnnexureItemList_p.Attributes.Add("style", "display:block;height:287mm;width:200mm;float:left");
            //    divAnnexureBellowList_p.Attributes.Add("style", "display:block;height:287mm;width:200mm;float:left");

            //    divAnnexureItemList_p.RenderControl(new HtmlTextWriter(new StringWriter(sbGvItemAnnexure)));
            //    divAnnexureBellowList_p.RenderControl(new HtmlTextWriter(new StringWriter(sbGvItemAnnexure)));
            //}
            //else
            //{
            //    Flag = "single";
            //    divAnnexureItemList_p.Attributes.Add("style", "display:none;");
            //    divAnnexureBellowList_p.Attributes.Add("style", "display:none;");
            //}

            //string htmlfile = ViewState["RFPNo"].ToString().Replace('/', '-') + ".html";
            //string pdffile = ViewState["RFPNo"].ToString().Replace('/', '-') + ".pdf";
            //string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            //string pdfFileURL = LetterPath + pdffile;

            //string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "RFPPrint('" + epstyleurl + "','" + Main + "','" + QrCode + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


}