using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Pages_Customers : System.Web.UI.Page
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

                GetGrid(gvCustomer);

                objFinance.GetCustomerType(ddlCustomerType);

                _objCommon.GetRegion(ddlRegion);

                _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);

                ViewState["CusName"] = "CustomerName Asc";

                _objCommon.getCountry(ddlCountry);

                objFinance.GetProspectDetails(ddlProspect);

                txtOpeningBalance.Text = "0.00";

                _objc.ShowOutputSection(divAdd, divInput, divOutput);

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

                    GetGrid(gvCustomer);

                    _objc.ShowOutputSection(divAdd, divInput, divOutput);
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

            DataSet ds = new DataSet();
            ds = (DataSet)ViewState["Cutomers"];


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

            int rowcount = Convert.ToInt32(ds.Tables[0].Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Tables[0].Columns.Count);

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

            _objc.exportExcel(ds.Tables[0], rowcount, ColumnCount, strFile, LetterName, "LoneStar", lbltitle.Text, 2, GemBoxKey);

            _objc.SaveExcelFile("Customers.aspx", LetterName, _objSession.employeeid);

            GetGrid(gvCustomer);

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

            gvCustomer.Columns[3].Visible = false;
            gvCustomer.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvCustomer.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvCustomer.Columns[3].Visible = true;
            gvCustomer.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL, "Customer Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);


            _objc.SavePDFFile("Customers.aspx", pdffile, _objSession.employeeid);

            GetGrid(gvCustomer);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
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
            DataSet ds = new DataSet();

            objFinance = new c_Finance();
            if (ValidateControls())
            {
                objFinance.CustomerID = Convert.ToInt32(hdnCustomerID.Value);
                objFinance.contactName = txtContactPerson.Text;
                objFinance.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                objFinance.customerName = txtComp.Text;
                objFinance.address = txtAddress.Text;
                objFinance.phone = Convert.ToInt64(txtPhoneNo.Text);
                objFinance.email = txtEmail.Text;
                //   objFinance.accountID = Convert.ToInt32(ddlLedger.SelectedValue.Split("/".ToCharArray())[0]);       
                objFinance.CustomerPANNo = txtCustPanNo.Text;
                objFinance.CustomerGSTNo = txtCustGSTNo.Text;
                objFinance.CustomerTANNo = txtCustTanNo.Text;
                objFinance.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                objFinance.state = Convert.ToInt32(ddlState.SelectedValue);
                objFinance.bankname = txtBankName.Text;
                objFinance.bankAccount = txtBankAccNo.Text;
                objFinance.bank_IFSCDetails = txtBankIFSC.Text;
                objFinance.openingBalance = txtOpeningBalance.Text;
                objFinance.Region = Convert.ToInt32(ddlRegion.SelectedValue);
                objFinance.ProsPectID = Convert.ToInt32(ddlProspect.SelectedValue);
                objFinance.FaxNo = txtFaxNo.Text;
                objFinance.CustomerTypeID = Convert.ToInt32(ddlCustomerType.SelectedValue);

                objFinance.CreatedBy = Convert.ToInt32(_objSession.employeeid);
                ds = objFinance.saveCustomer();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Already Exists');", true);
                else
                {
                    if (Convert.ToInt32(hdnCustomerID.Value) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Customer Details Saved successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Customer Details  Updated successfully');", true);

                    }
                }

                ClearControls();
                GetGrid(gvCustomer);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            divAdd.Style.Add("display", "none");
            divInput.Style.Add("display", "block");
            divOutput.Style.Add("display", "none");

            if (e.CommandName.ToString() == "EditCustomer")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                int CustomerID = Convert.ToInt32(((Label)gvCustomer.Rows[index].FindControl("lblCustomerID")).Text);
                DataSet ds = (DataSet)ViewState["Cutomers"];
                DataTable dt = ds.Tables[0];
                dt.DefaultView.RowFilter = "CustomerID='" + CustomerID + "'";
                hdnCustomerID.Value = CustomerID.ToString();
                txtAddress.Text = dt.DefaultView.ToTable().Rows[0]["Address"].ToString();
                txtComp.Text = dt.DefaultView.ToTable().Rows[0]["CustomerName"].ToString();
                txtEmail.Text = dt.DefaultView.ToTable().Rows[0]["Email"].ToString();
                txtPhoneNo.Text = dt.DefaultView.ToTable().Rows[0]["Phone"].ToString();
                txtContactPerson.Text = dt.DefaultView.ToTable().Rows[0]["ContactPerson"].ToString();
                ddlCustomerType.SelectedValue = dt.DefaultView.ToTable().Rows[0]["CustomerTypeID"].ToString();
                txtCustPanNo.Text = dt.DefaultView.ToTable().Rows[0]["CustomerPAN"].ToString();
                txtCustTanNo.Text = dt.DefaultView.ToTable().Rows[0]["CustomerTAN"].ToString();
                txtCustGSTNo.Text = dt.DefaultView.ToTable().Rows[0]["CustomerGST"].ToString();
                txtBankName.Text = dt.DefaultView.ToTable().Rows[0]["BankName"].ToString();
                txtBankAccNo.Text = dt.DefaultView.ToTable().Rows[0]["BankAccountNo"].ToString();
                txtBankIFSC.Text = dt.DefaultView.ToTable().Rows[0]["BankIFSCDetails"].ToString();
                ddlCountry.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Country"].ToString();
                txtOpeningBalance.Text = dt.DefaultView.ToTable().Rows[0]["OpeningBalance"].ToString();
                txtFaxNo.Text = dt.DefaultView.ToTable().Rows[0]["FaxNo"].ToString();
                ddlRegion.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Region"].ToString();

                divProspect.Visible = false;

                if (txtOpeningBalance.Text == "")
                    txtOpeningBalance.Text = "0.00";
                if (ddlCountry.SelectedValue != "0")
                {
                    _objCommon.CountryID = Convert.ToInt64(ddlCountry.SelectedValue);
                    _objCommon.getState(ddlState);
                    ddlState.SelectedValue = dt.DefaultView.ToTable().Rows[0]["state"].ToString();
                }
                if (ddlState.SelectedValue != "0")
                {
                    _objCommon.StateID = Convert.ToInt64(ddlState.SelectedValue);
                    _objCommon.getCity(ddlCity);
                    ddlCity.SelectedValue = dt.DefaultView.ToTable().Rows[0]["City"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                //  e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                //  e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Common Methods

    private bool ValidateControls()  
    {
        bool isvalid;
        isvalid = true;
        string error = "";

        if (txtComp.Text == "")
            error = txtComp.ClientID + '/' + "Field Required";
        else if (ddlCustomerType.SelectedIndex == 0)
            error = ddlCustomerType.ClientID + '/' + "Field Required";
        else if (txtAddress.Text == "")
            error = txtAddress.ClientID + '/' + "Field Required";
        else if (ddlCountry.SelectedIndex == 0)
            error = ddlCountry.ClientID + '/' + "Field Required";
        else if (ddlState.SelectedIndex == 0)
            error = ddlState.ClientID + '/' + "Field Required";
        else if (ddlCity.SelectedIndex == 0)
            error = ddlCity.ClientID + '/' + "Field Required";
        else if (txtContactPerson.Text == "")
            error = txtContactPerson.ClientID + '/' + "Field Required";
        else if (txtEmail.Text == "")
            error = txtEmail.ClientID + '/' + "Field Required";
        else if (!EmailAndSmsAlerts.ValidateEmail(txtEmail.Text))
            error = txtEmail.ClientID + '/' + "Please Enter Valid Email";
        else if (txtPhoneNo.Text == "")
            error = txtPhoneNo.ClientID + '/' + "Field Required";
        else if (txtFaxNo.Text == "")
            error = txtFaxNo.ClientID + '/' + "Field Required";
        else if (txtCustGSTNo.Text != "")
        {
            if (txtCustGSTNo.Text.Length > 15 || txtCustGSTNo.Text.Length < 15)
                error = txtCustGSTNo.ClientID + '/' + "Please Enter Valid GST Number";
        }
        else if (txtCustPanNo.Text != "")
        {
            if (txtCustPanNo.Text.Length > 10 || txtCustPanNo.Text.Length < 10)
                error = txtCustPanNo.ClientID + '/' + "Please Enter Valid PAN Number";
        }
        else if (txtCustTanNo.Text != "")
        {
            if (txtCustTanNo.Text.Length > 10 || txtCustTanNo.Text.Length < 10)
                error = txtCustTanNo.ClientID + '/' + "Please Enter Valid TAN Number";
        }
        if (error != "")
        {
            isvalid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ServerSideValidation('" + error + "');", true);
            _objc.ShowInputSection(divAdd, divInput, divOutput);
        }

        return isvalid;
    }

    public void ClearControls()
    {
        try
        {
            txtComp.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            txtCustGSTNo.Text = "";
            ddlState.SelectedValue = "0";
            ddlCustomerType.SelectedValue = "0";
            txtCustPanNo.Text = "";
            txtCustTanNo.Text = "";
            txtCustGSTNo.Text = "";
            txtBankName.Text = "";
            txtBankAccNo.Text = "";
            txtBankIFSC.Text = "";
            ddlCountry.SelectedValue = "0";
            txtOpeningBalance.Text = "0.00";
            hdnCustomerID.Value = "0";
            divProspect.Visible = true;
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "block");
            divAdd.Style.Add("display", "block");
            gvCustomer.UseAccessibleHeader = true;
            gvCustomer.HeaderRow.TableSection = TableRowSection.TableHeader;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GetGrid(GridView dg)
    {
        try
        {
            objFinance = new c_Finance();
            DataSet ds = objFinance.getCustomers();
            ViewState["Cutomers"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                dg.DataSource = ds;
                dg.DataBind();
                dg.UseAccessibleHeader = true;
                dg.HeaderRow.TableSection = TableRowSection.TableHeader;
                divDownload.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                dg.DataSource = "";
                dg.DataBind();
                divDownload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}