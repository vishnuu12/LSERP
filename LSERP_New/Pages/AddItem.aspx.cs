using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
using System.IO;
using eplus.core;

public partial class Pages_AddItem : System.Web.UI.Page
{
    #region "Declaration"
    cSession objSession = new cSession();
    cCommon objcommon = new cCommon();
    cSales objSales = new cSales();
    cCommon objc;
    cDesign objDesign;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();
    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            objc = new cCommon();
            //  objc.GetLocationDetails(ddlLocation);
            objDesign = new cDesign();

            objDesign.GetPressureUnitsDetails(ddlPresureUnits);

            ddlenquiryload();
            //btngroup.Visible = false;
            //detailsdiv.Style.Add("display", "none");
            //specdiv.Style.Add("display", "block");
            ShowHideControls("add");
        }
        if (target == "ShareItem")
            UpdateItemSharedStatus();
    }

    #endregion

    #region"CheckBox Events"

    protected void chckStandarditem_OnCheckedChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            BindItemdetails(ddlEnquiryNumber.SelectedValue);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Dropdown Methods

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataView dv;
        DataTable dcustomr = new DataTable();
        DataTable denquiry = new DataTable();
        try
        {           
            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            if (ddlCustomerName.SelectedIndex > 0)
            {
                dv = new DataView(denquiry);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }

            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;

                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];

                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }

                BindItemdetails(ddlEnquiryNumber.SelectedValue);

                ShowHideControls("add,view,button");
                divsharebutton.Visible = true;
            }
            else
            {
                ShowHideControls("add");
                divsharebutton.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    protected void ddlitemname_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            if (chckStandarditem.Checked)
            {
                if (ddlitemname.SelectedIndex > 0)
                {
                    objDesign.ItemID = Convert.ToInt32(ddlitemname.SelectedValue);
                    objDesign.GetStandardItemSizeDetailsByItemID(ddlsize);
                }
                else
                {
                    objc = new cCommon();
                    objc.EmptyDropDownList(ddlitemname);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlsize_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            if (chckStandarditem.Checked)
            {
                if (ddlsize.SelectedIndex > 0)
                {
                    objDesign.SIEHID = Convert.ToInt32(ddlsize.SelectedValue.Split('/')[1].ToString());

                    ds = objDesign.getStandardItemDetailsBySIEHID();

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = ds.Tables[1].Rows[0]["Name"].ToString();
                        txt_tagno.Text = ds.Tables[1].Rows[0]["Value"].ToString();
                    }
                    else
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = "None";
                        txt_tagno.Text = "";
                    }

                    txt_pressure.Text = ds.Tables[0].Rows[0]["Presure"].ToString();
                    txt_movement.Text = ds.Tables[0].Rows[0]["Movement"].ToString();
                    txt_temperature.Text = ds.Tables[0].Rows[0]["Temprature"].ToString();
                    lblrevision.Text = ds.Tables[0].Rows[0]["RevisionNumber"].ToString();

                    ViewState["DrawingFilename"] = ds.Tables[0].Rows[0]["FileName"].ToString();
                }
                //else
                //{
                //    ddlitemname.SelectedIndex = 0;
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblEnquiryChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlenquiryload();
        ShowHideControls("add");
    }

    #endregion

    #region "GridView Events"

    protected void gvitemdetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objSales = new cSales();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int EDID = Convert.ToInt32(gvitemdetails.DataKeys[index].Values[0].ToString());
            int PUID = Convert.ToInt32(gvitemdetails.DataKeys[index].Values[1].ToString());
            ViewState["EDID"] = EDID;
            if (e.CommandName.ToString() == "EditItem")
            {
                ShowHideControls("input");
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "showdetails();", true);

                string SIEHID = gvitemdetails.DataKeys[index].Values[2].ToString();
                string ItemID = gvitemdetails.DataKeys[index].Values[3].ToString();
                string SID = gvitemdetails.DataKeys[index].Values[4].ToString();

                string TagNo = gvitemdetails.DataKeys[index].Values[5].ToString();

                string Pressure = gvitemdetails.DataKeys[index].Values[6].ToString();

                if (SIEHID != "0")
                {
                    chckStandarditem.Checked = true;
                    chckStandarditem.Enabled = false;
                    chckStandarditem_OnCheckedChanged(null, null);

                    ddlitemname.SelectedValue = ItemID;
                    ddlitemname_OnSelectIndexChanged(null, null);
                    ddlsize.SelectedValue = SID + '/' + SIEHID;
                }
                else
                {
                    chckStandarditem.Checked = false;
                    chckStandarditem.Enabled = true;

                    BindItemdetails(ddlEnquiryNumber.SelectedValue);

                    ddlitemname.ClearSelection();
                    ddlitemname.Items.FindByText(((Label)gvitemdetails.Rows[index].FindControl("lblitemname")).Text.ToString()).Selected = true;
                    ddlsize.ClearSelection();
                    ddlsize.Items.FindByText(((Label)gvitemdetails.Rows[index].FindControl("lblitemsize")).Text.ToString()).Selected = true;
                }
                //Label lblTagNo = (Label)gvitemdetails.Rows[index].FindControl("lbltagno");
                Label lblMovement = (Label)gvitemdetails.Rows[index].FindControl("lblmovement");
                //Label txtItemCode = (Label)gvitemdetails.Rows[index].FindControl("lblItemCode");
                //Label txtMaterialCode = (Label)gvitemdetails.Rows[index].FindControl("lblMaterialCode");

                ddlPresureUnits.SelectedValue = PUID.ToString();

                if (TagNo != "" && TagNo != "-")
                {
                    if (TagNo.EndsWith("TagNo"))
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = "TN";
                    }
                    else if (TagNo.EndsWith("MaterialCode"))
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = "MC";
                    }
                    else if (TagNo.EndsWith("ItemCode"))
                    {
                        rblTagNoItemCodeMatCode.SelectedValue = "IC";
                    }
                    else
                        rblTagNoItemCodeMatCode.SelectedValue = "None";
                   // txt_tagno.Text = TagNo;
                }
                else
                {
                    rblTagNoItemCodeMatCode.SelectedValue = "None";
                    txt_tagno.Text = "NA";
                }
				
				//Vishnu
				
				if (rblTagNoItemCodeMatCode.SelectedValue == "TN")
                {
                    txt_tagno.Text = TagNo.Replace("-TagNo", "");
                }
                else if (rblTagNoItemCodeMatCode.SelectedValue == "MC")
                {
                    txt_tagno.Text = TagNo.Replace("-MaterialCode", "");
                }
                else if (rblTagNoItemCodeMatCode.SelectedValue == "IC")
                {
                    txt_tagno.Text = TagNo.Replace("-ItemCode", "");
                }
                else if (rblTagNoItemCodeMatCode.SelectedValue == "None")
                {
                    txt_tagno.Text = "NA";
                }

                if (lblMovement.Text != "")
                {
                    //if (lblMovement.Text.Split('-')[1] == "AxizlExtansion")
                    //    rblItemMovement.SelectedValue = "AX";
                    //else if (lblMovement.Text.Split('-')[1] == "AxialCompression")
                    //    rblItemMovement.SelectedValue = "AC";
                    //else if (lblMovement.Text.Split('-')[1] == "LateralAngular")
                    //    rblItemMovement.SelectedValue = "LA";
                    txt_movement.Text = lblMovement.Text;
                }
                else
                {
                    // rblItemMovement.ClearSelection();
                    txt_movement.Text = "";
                }

                txt_description.Text = ((Label)gvitemdetails.Rows[index].FindControl("lbldescription")).Text.ToString();
                lblrevision.Text = ((Label)gvitemdetails.Rows[index].FindControl("lblrevisionno")).Text.ToString();
                txt_quantity.Text = ((Label)gvitemdetails.Rows[index].FindControl("lblqty")).Text.ToString();
                txt_pressure.Text = Pressure; //((Label)gvitemdetails.Rows[index].FindControl("lblpressure")).Text.ToString();
                txt_temperature.Text = ((Label)gvitemdetails.Rows[index].FindControl("lbltemp")).Text.ToString();
                txt_movement.Text = ((Label)gvitemdetails.Rows[index].FindControl("lblmovement")).Text.ToString();
                txt_matrlwarning.Text = ((Label)gvitemdetails.Rows[index].FindControl("lblmatrlwarning")).Text.ToString();

                txtLocation.Text = ((Label)gvitemdetails.Rows[index].FindControl("lblLocationName")).Text.ToString();
                txtEnquiryApplication.Text = ((Label)gvitemdetails.Rows[index].FindControl("lblEnquirApplication")).Text.ToString();
            }

            if (e.CommandName.ToString() == "DeleteItem")
            {
                objSales.deleteitemfromenquiry(ViewState["EDID"].ToString(), "LS_DeleteItemfromEnquiry");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "SuccessMessage('Success','Item Deleted successfully');", true);
                BindItemdetails(ViewState["EnquiryNumber"].ToString());
                ShowHideControls("add,view,button");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "ViewDocs")
            {
                string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
                string FileName = gvAttachments.DataKeys[index].Values[1].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName;
                if (File.Exists(imgname))
                {
                    //ViewState["ifrmsrc"] = imgname;
                    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message("CustomerEnquiryProcess" + " " + "gvAttachments_OnRowCommandex" + "" + " " + ex.ToString());
        }
    }

    protected void gvAttachments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            // string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + ViewState["EnquiryNumber"].ToString() + "/";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                //if (extension == "PDF")
                //{
                //    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/pdf.png"));
                //    string base64String = Convert.ToBase64String(imageBytes);
                //    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                //}
                //else if (extension == "DOC" || extension == "DOCX")
                //{
                //    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/word-ls.png"));
                //    string base64String = Convert.ToBase64String(imageBytes);
                //    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                //}
                //else if (extension == "XLS" || extension == "XLSX")
                //{
                //    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/excel-ls.png"));
                //    string base64String = Convert.ToBase64String(imageBytes);
                //    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                //}
                //else
                //{
                //byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                //string base64String = Convert.ToBase64String(imageBytes);
                //imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                //}
                imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                imgbtn.ToolTip = dr["FileName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvitemdetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btndelete = (LinkButton)e.Row.FindControl("lbtnDelete_itemdetails");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("lbtnEdit_itemdetails");

                if (dr["ItemSharedStatus"].ToString() == "1")
                {
                    btndelete.Visible = false;
                    if (objSession.type == 1)
                        btnEdit.Visible = true;
                    else
                        btnEdit.Visible = false;
                }
                else
                {
                    btndelete.Visible = true;
                    btnEdit.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"

    protected void btnshowdocs_Click(object sender, EventArgs e)
    {
        try
        {
            BindAttachements(ddlEnquiryNumber.SelectedValue);
        }
        catch (Exception ec)
        { }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());

        }
        catch (Exception ec)
        { }
    }

    protected void btnshowitems_Click(object sender, EventArgs e)
    {
        try
        {
            BindItemdetails(ddlEnquiryNumber.SelectedValue);
        }
        catch (Exception ec)
        { }
    }

    protected void btndetails_click(object sender, EventArgs e)
    {
        try
        {
            if (objcommon.Validate(divdetails))
            {
                objSales.UserID = Convert.ToInt32(objSession.employeeid);
                objSales.Createdon = DateTime.Now;
                objSales.Itemid = Convert.ToInt32(ddlitemname.SelectedValue);
                if (chckStandarditem.Checked)
                {
                    objSales.Sizeid = Convert.ToInt32(ddlsize.SelectedValue.Split('/')[0].ToString());
                    objSales.SIEHID = Convert.ToInt32(ddlsize.SelectedValue.Split('/')[1].ToString()); ;
                }
                else
                {
                    objSales.Sizeid = Convert.ToInt32(ddlsize.SelectedValue);
                    objSales.SIEHID = 0;
                }

                objSales.Revisionno = Convert.ToInt32(lblrevision.Text);
                objSales.Quantity = Convert.ToInt32(txt_quantity.Text);

                if (rblTagNoItemCodeMatCode.SelectedValue != "None")
                {
                    objSales.Tagno = txt_tagno.Text;
                    objSales.itemCodeType = rblTagNoItemCodeMatCode.SelectedValue;
                }
                else
                {
                    objSales.Tagno = null;
                    objSales.itemCodeType = null;
                }

                objSales.ItemDescription = txt_description.Text;
                objSales.ItemPressure = txt_pressure.Text;
                objSales.ItemTemperature = txt_temperature.Text;

                //if (rblItemMovement.SelectedIndex >= 0)
                //{
                //     objSales.ItemMovementType = rblItemMovement.SelectedValue;
                objSales.ItemMovement = txt_movement.Text;
                // }
                //else
                //  objSales.ItemMovementType = null;

                objSales.Matrlwarning = txt_matrlwarning.Text;
                objSales.LocationID = txtLocation.Text;
                objSales.EnquiryApplication = txtEnquiryApplication.Text;

                objSales.PUID = Convert.ToInt32(ddlPresureUnits.SelectedValue);

                if (ViewState["EDID"] == null)
                {
                    if (chckStandarditem.Checked)
                    {
                        if (ViewState["DrawingFilename"].ToString() != "")
                        {
                            string TargetPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
                            string SourcePath = DrawingDocumentSavePath + "StandardItemDocs" + "\\";

                            string fileName = ViewState["DrawingFilename"].ToString();

                            string sourceFile = SourcePath + fileName;
                            string destFile = TargetPath + fileName;

                            if (!Directory.Exists(TargetPath))
                                Directory.CreateDirectory(TargetPath);

                            if (!File.Exists(destFile))
                            {
                                File.Copy(sourceFile, destFile);
                            }
                        }
                    }
                    objSales.updateenquiryprocess(ViewState["EnquiryNumber"].ToString(), "LS_InertEnquiryprocess");
                }
                else
                {
                    objSales.updateenquiryprocess(ViewState["EDID"].ToString(), "LS_UpdateEnquiryprocess");
                    ViewState["EDID"] = null;
                }
                BindItemdetails(ViewState["EnquiryNumber"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Item Details Saved successfully');", true);
                ShowHideControls("add,view,button");
                //ClearValues();
            }
        }
        catch (Exception ec)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "showdetails();hideLoader();", true);
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("input");
        ViewState["EDID"] = null;
        chckStandarditem.Checked = false;
        chckStandarditem.Enabled = true;
        ClearFields();
    }

    protected void btnCancelAttachements_Click(object sender, EventArgs e)
    {
        ShowHideControls("add,view,button");
        ViewState["EDID"] = null;
        chckStandarditem.Enabled = true;
        chckStandarditem.Checked = false;
    }

    #endregion

    #region Common Methods

    private void ClearFields()
    {
        //ddlitemname.SelectedIndex = 0;
        //ddlsize.SelectedIndex = 0;
        txt_tagno.Text = "NA";
        //rblTagNoItemCodeMatCode.SelectedValue = "None";
        //ddlPresureUnits.SelectedIndex = 0;
        //txt_description.Text = "";
        //txt_quantity.Text = "";
        //txt_pressure.Text = "";
        //txt_temperature.Text = "";
        //txt_movement.Text = "";
        //txt_matrlwarning.Text = "";
        //txtLocation.Text = "";
        //txtEnquiryApplication.Text = "";
    }

    public void BindAttachements(string EnquiryID)
    {
        objSales = new cSales();
        DataSet dsGetAttachementsDetails = new DataSet();
        bool SIFlag = false;
        dsGetAttachementsDetails = objSales.GetEnquiryprocessDetails(EnquiryID, "LS_GetAttachementsDetails", SIFlag);
        ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];
        try
        {
            if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
            {
                gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
            dsGetAttachementsDetails = null;
            gvAttachments.Visible = true;
            gvitemdetails.Visible = false;
        }
    }
    private void ddlenquiryload()
    {
        objc = new cCommon();
        try
        {
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();

            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName,"LS_GetCustomerNameByAddItem", rblEnquiryChange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];
            dsEnquiryNumber = objc.GetEnquiryNumberByPendingList(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber,"LS_GetEnquiryIDByAddItem", rblEnquiryChange.SelectedValue);
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    public void BindItemdetails(string EnquiryID)
    {
        try
        {
            objSales = new cSales();
            DataSet dsGetItemDetails = new DataSet();
            bool SIFlag = chckStandarditem.Checked;
            dsGetItemDetails = objSales.GetEnquiryprocessDetails(EnquiryID, "LS_GetItemDetails", SIFlag);
            DataTable item = dsGetItemDetails.Tables[0];
            DataTable size = dsGetItemDetails.Tables[1];

            ddlitemname.DataSource = dsGetItemDetails.Tables[0];
            ddlitemname.DataTextField = "ItemName";
            ddlitemname.DataValueField = "ItemID";
            ddlitemname.DataBind();
            ddlitemname.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlsize.DataSource = dsGetItemDetails.Tables[1];
            ddlsize.DataTextField = "ItemSize";
            ddlsize.DataValueField = "SID";
            ddlsize.DataBind();
            ddlsize.Items.Insert(0, new ListItem("--Select--", "0"));

            lblrevision.Text = "0";

            if (dsGetItemDetails.Tables[3].Rows[0]["ButtonStatus"].ToString() == "hide" && objSession.type == 1)
                btnAddNew.Visible = true;
            else if (dsGetItemDetails.Tables[3].Rows[0]["ButtonStatus"].ToString() == "hide")
                btnAddNew.Visible = false;
            else
                btnAddNew.Visible = true;

            if (dsGetItemDetails.Tables[2].Rows.Count > 0)
            {
                gvitemdetails.DataSource = dsGetItemDetails.Tables[2];
                gvitemdetails.DataBind();
            }
            else
            {
                gvitemdetails.DataSource = "";
                gvitemdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
            // dsGetItemDetails = null;
            gvAttachments.Visible = false;
            gvitemdetails.Visible = true;
        }
    }

    public void UpdateItemSharedStatus()
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            if (gvitemdetails.Rows.Count > 0)
            {
                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                ds = objDesign.UpdateItemSharedStatus();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Hid", "SuccessMessage('Success','Item Shared Succesfully')", true);
                BindItemdetails(ddlEnquiryNumber.SelectedValue);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Hid", "ErrorMessage('Error','No Item Added')", true);
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divdetails.Visible = divbtngroup.Visible = divOutput.Visible = false;
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
                    case "button":
                        divbtngroup.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divdetails.Visible = true;
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

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvitemdetails.Rows.Count > 0)
            gvitemdetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvAttachments.Rows.Count > 0)
            gvAttachments.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}