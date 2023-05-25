using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using eplus.core;

public partial class Pages_ProsPect : System.Web.UI.Page
{
    #region"Declaration"

    c_Finance objFinance = new c_Finance();
    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cCommon _objc = new cCommon();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
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

                GetGrid();

                // ViewState["CusName"] = "CustomerName Asc";

                _objCommon.getCountry(ddlCountry);
                _objCommon.GetRegion(ddlRegion);
                _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
                divAdd.Style.Add("display", "block");
                divInput.Style.Add("display", "none");
                divOutput.Style.Add("display", "block");

            }
            else
            {
                if (target == "deletegvrow")
                {
                    int CustomerId = Convert.ToInt32(arg);
                    DataSet ds = objFinance.DeleteCustomer(CustomerId);
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Customer Name Deleted successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
                    }

                    GetGrid();

                    divAdd.Style.Add("display", "block");
                    divInput.Style.Add("display", "none");
                    divOutput.Style.Add("display", "block");
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCountry_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlState.Items.Clear();
            _objCommon.CountryID = Convert.ToInt64(ddlCountry.SelectedValue);
            _objCommon.getState(ddlState);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();", true);

            divAdd.Style.Add("display", "none");
            divInput.Style.Add("display", "block");
            divOutput.Style.Add("display", "none");

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlState_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            _objCommon.StateID = Convert.ToInt64(ddlState.SelectedValue);
            _objCommon.getCity(ddlCity);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();", true);
            divAdd.Style.Add("display", "none");
            divInput.Style.Add("display", "block");
            divOutput.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button EVents"

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";

            DataTable Exdt = new DataTable();

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["ProsPect"];


            //Exdt.Columns.Add("CustomerName");
            //Exdt.Columns.Add("CustomerTypeName");
            //Exdt.Columns.Add("Address");
            //Exdt.Columns.Add("Phone");
            //Exdt.Columns.Add("Email");
            //Exdt.Columns.Add("ContactPerson");
            //Exdt.Columns.Add("CityName");
            //Exdt.Columns.Add("StateName");
            //Exdt.Columns.Add("CountryName");

            //foreach (DataRow row in dt.Rows)
            //{
            //    try
            //    {

            //    }
            //    catch (Exception Ex)
            //    {

            //    }
            //}

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = MAXEXID + ".xlsx";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
            {
                File.Delete(strFile);
            }

            _objc.exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "LoneStar", lbltitle.Text, 2, GemBoxKey);

            _objc.SaveExcelFile("ProsPect.aspx", LetterName, _objSession.employeeid);

            GetGrid();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnPDFDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXPDFID = "";


            gvProsPect.Columns[10].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvProsPect.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;

            gvProsPect.Columns[10].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            string HeaderTitle = "Prospect Details";

            _objc.SaveHtmlFile(URL, HeaderTitle, lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("ProsPect.aspx", pdffile, _objSession.employeeid);

            GetGrid();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnreset_Click(object sender, EventArgs e)
    {
        //Server.Transfer("Fa_Customers");
        ClearControls();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateControls())
            {
                objFinance = new c_Finance();
                objFinance.ProsPectID = Convert.ToInt32(hdnProsPectID.Value);
                objFinance.contactName = txtContactPerson.Text;

                objFinance.ProsPectName = txtProsPectName.Text;
                objFinance.address = txtAddress.Text;
                objFinance.phone = Convert.ToInt64(txtPhoneNo.Text);
                objFinance.email = txtEmail.Text;
                objFinance.Source = Convert.ToInt32(ddlSource.SelectedValue);
                objFinance.Region = Convert.ToInt32(ddlRegion.SelectedValue);
                //   objFinance.accountID = Convert.ToInt32(ddlLedger.SelectedValue.Split("/".ToCharArray())[0]);               
                objFinance.Country = Convert.ToInt32(ddlCountry.SelectedValue);

                objFinance.CreatedBy = Convert.ToInt32(_objSession.employeeid);
                objFinance.FaxNo = txtFaxNo.Text;

                if (ddlState.SelectedIndex > 0)
                    objFinance.StateName = "";
                else
                    objFinance.StateName = txtState.Text;
                if (ddlCity.SelectedIndex > 0)
                    objFinance.CityName = "";
                else
                    objFinance.CityName = txtCity.Text;

                objFinance.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                objFinance.state = Convert.ToInt32(ddlState.SelectedValue);

                string ReturnValue = objFinance.saveProsPect();

                if (ReturnValue == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Already Exists');", true);
                else
                {
                    if (Convert.ToInt32(hdnProsPectID.Value) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Customer Name Saved successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Customer Name Updated successfully');", true);
                    }
                }

                ClearControls();
                GetGrid();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvProsPect_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int ProsPectID = Convert.ToInt32(gvProsPect.DataKeys[index].Values[0].ToString());
            int Flag = Convert.ToInt32(gvProsPect.DataKeys[index].Values[1].ToString());

            //if (Flag == 0)
            //{
            if (e.CommandName.ToString() == "EditProsPect")
            {
                DataTable dt = (DataTable)ViewState["ProsPect"];

                dt.DefaultView.RowFilter = "ProsPectID='" + ProsPectID + "'";
                hdnProsPectID.Value = ProsPectID.ToString();
                txtAddress.Text = dt.DefaultView.ToTable().Rows[0]["Address"].ToString();
                txtProsPectName.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
                txtEmail.Text = dt.DefaultView.ToTable().Rows[0]["EmailID"].ToString();
                txtPhoneNo.Text = dt.DefaultView.ToTable().Rows[0]["PhoneNumber"].ToString();
                txtContactPerson.Text = dt.DefaultView.ToTable().Rows[0]["ContactPerson"].ToString();
                ddlCountry.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Country"].ToString();
                ddlSource.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Source"].ToString();
                ddlRegion.SelectedValue = dt.DefaultView.ToTable().Rows[0]["RegionId"].ToString();

                txtFaxNo.Text = dt.DefaultView.ToTable().Rows[0]["FaxNo"].ToString();

                if (ddlCountry.SelectedValue != "0")
                {
                    _objCommon.CountryID = Convert.ToInt64(ddlCountry.SelectedValue);
                    _objCommon.getState(ddlState);
                    ddlState.SelectedValue = dt.DefaultView.ToTable().Rows[0]["State"].ToString();
                }
                if (ddlState.SelectedValue != "0")
                {
                    _objCommon.StateID = Convert.ToInt64(ddlState.SelectedValue);
                    _objCommon.getCity(ddlCity);
                    ddlCity.SelectedValue = dt.DefaultView.ToTable().Rows[0]["City"].ToString();
                }

                divAdd.Style.Add("display", "none");
                divInput.Style.Add("display", "block");
                divOutput.Style.Add("display", "none");
            }
            //}
            else
            {
                gvProsPect.UseAccessibleHeader = true;
                gvProsPect.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','You Cannot Edit The Record');showDataTable();", true);

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Common Methods

    public void ClearControls()
    {
        try
        {
            txtProsPectName.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            txtContactPerson.Text = "";
            ddlCity.SelectedValue = "0";
            ddlState.SelectedValue = "0";
            ddlCountry.SelectedValue = "0";
            hdnProsPectID.Value = "0";
            ddlRegion.SelectedValue = "0";
            ddlSource.SelectedValue = "0";
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "block");
            divAdd.Style.Add("display", "block");

            gvProsPect.UseAccessibleHeader = true;
            gvProsPect.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetGrid()
    {
        try
        {
            objFinance = new c_Finance();
            DataSet ds = objFinance.GetProsPect();
            ViewState["ProsPect"] = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvProsPect.DataSource = ds;
                gvProsPect.DataBind();
                gvProsPect.UseAccessibleHeader = true;
                gvProsPect.HeaderRow.TableSection = TableRowSection.TableHeader;

                divDownload.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                gvProsPect.DataSource = "";
                gvProsPect.DataBind();
                divDownload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private bool ValidateControls()
    {
        bool isvalid;
        isvalid = true;
        string error = "";

        if (txtProsPectName.Text == "")
            error = txtProsPectName.ClientID + '/' + "Field Required";
        else if (txtContactPerson.Text == "")
            error = txtContactPerson.ClientID + '/' + "Field Required";
        else if (txtEmail.Text == "")
            error = txtEmail.ClientID + '/' + "Field Required";
        else if (!EmailAndSmsAlerts.ValidateEmail(txtEmail.Text))
            error = txtEmail.ClientID + '/' + "Please Enter Valid Email";
        else if (txtPhoneNo.Text == "")
            error = txtPhoneNo.ClientID + '/' + "Field Required";
        else if (txtAddress.Text == "")
            error = txtAddress.ClientID + '/' + "Field Required";
        else if (ddlCountry.SelectedIndex == 0)
            error = ddlCountry.ClientID + '/' + "Field Required";
        //else if (ddlState.SelectedIndex == 0)
        //    error = ddlState.ClientID + '/' + "Field Required";
        //else if (ddlCity.SelectedIndex == 0)
        //    error = ddlCity.ClientID + '/' + "Field Required";
        else if (ddlRegion.SelectedIndex == 0)
            error = ddlRegion.ClientID + '/' + "Field Required";
        else if (ddlSource.SelectedIndex == 0)
            error = ddlSource.ClientID + '/' + "Field Required";

        if (error != "")
        {
            isvalid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ServerSideValidation('" + error + "');", true);
            _objc.ShowInputSection(divAdd, divInput, divOutput);
        }
        return isvalid;
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvProsPect.Rows.Count > 0)
            gvProsPect.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}