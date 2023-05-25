using System;
using System.Data;
using eplus.data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.HtmlControls;
using QRCodeEncoderDecoderLibrary;
using System.Linq;
using System.Drawing;
using System.IO;
using SelectPdf;
using System.Data.OleDb;
using System.Configuration;
using GemBox.Spreadsheet;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;

public class cModuleMaster
{

    #region "Declaration"

    DataSet ds = new DataSet();
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();

    #endregion
    #region "Properties"
    public int MID { get; set; }
    public string moduleName { get; set; }
    public string moduleIcon { get; set; }
    public string returnvalue { get; set; }
    public string UserTypeName { get; set; }
    public Int32 UserTypeID { get; set; }

    public string PageName { get; set; }
    public string pageDisplay { get; set; }
    public string pageReference { get; set; }
    public int pageMID { get; set; }
    public int pageSequence { get; set; }
    public int pageCheck { get; set; }
    public List<string> updateresult = new List<string>();
    public int DesignationID { get; set; }

    public string DesignationIDs { get; set; }
    public string UserTypeIDs { get; set; }
    #endregion


    #region Methods
    public DataSet getModuleMaster(cModuleMaster objmaster)
    {
        DataSet ds = new DataSet();
        try
        {
            cDataAccess DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetModules";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public void saveModules(cModuleMaster objAcademicBO)
    {
        DAL = new cDataAccess();
        c = new SqlCommand();
        c.CommandType = CommandType.StoredProcedure;
        c.CommandText = "LS_SaveModules";
        c.Parameters.AddWithValue("@moduleName", objAcademicBO.moduleName);
        c.Parameters.AddWithValue("@moduleIcon", objAcademicBO.moduleIcon);
        objAcademicBO.returnvalue = DAL.GetScalar(c);
    }
    public void UpdateModules(cModuleMaster objAcademicBO)
    {
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_UpdateModules";
            c.Parameters.AddWithValue("@MID", objAcademicBO.MID);
            c.Parameters.AddWithValue("@moduleName", objAcademicBO.moduleName);
            c.Parameters.AddWithValue("@moduleIcon", objAcademicBO.moduleIcon);
            DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.Message);
        }
    }
    public void DleteModule(cModuleMaster objAcademicBO)
    {
        DAL = new cDataAccess();
        SqlCommand c = new SqlCommand();
        c.CommandType = CommandType.StoredProcedure;
        c.CommandText = "LS_DeleteModule";
        c.Parameters.AddWithValue("@MID", objAcademicBO.MID);
        DAL.GetScalar(c);
    }
    public void savePage(cModuleMaster objAcademicBO)
    {
        DAL = new cDataAccess();
        c = new SqlCommand();
        c.CommandType = CommandType.StoredProcedure;
        c.CommandText = "LS_SavePage";
        c.Parameters.AddWithValue("@pageName", objAcademicBO.PageName);
        c.Parameters.AddWithValue("@pageReference", objAcademicBO.pageReference);
        c.Parameters.AddWithValue("@pageDisplay", objAcademicBO.pageDisplay);
        c.Parameters.AddWithValue("@MID", objAcademicBO.pageMID);
        c.Parameters.AddWithValue("@displayOrder", objAcademicBO.pageSequence);
        objAcademicBO.returnvalue = DAL.GetScalar(c);
    }
    public void UpdatePage(cModuleMaster objAcademicBO)
    {
        DAL = new cDataAccess();
        c = new SqlCommand();
        c.CommandType = CommandType.StoredProcedure;
        c.CommandText = "LS_UpdatePage";
        c.Parameters.AddWithValue("@pageName", objAcademicBO.PageName);
        c.Parameters.AddWithValue("@pageReference", objAcademicBO.pageReference);
        c.Parameters.AddWithValue("@pageDisplay", objAcademicBO.pageDisplay);
        c.Parameters.AddWithValue("@MID", objAcademicBO.pageMID);
        c.Parameters.AddWithValue("@displayOrder", objAcademicBO.pageSequence);
        c.Parameters.AddWithValue("@Check", objAcademicBO.pageCheck);
        c.Parameters.AddWithValue("@UserTypeIDs", objAcademicBO.UserTypeIDs);

        objAcademicBO.returnvalue = DAL.GetScalar(c);
        if (objAcademicBO.returnvalue == "AE" && objAcademicBO.pageCheck == 1)
        {
            updateresult.Add(objAcademicBO.pageReference);
        }
    }
    #endregion
}
