using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;

public class CallibrationType
{
    public CallibrationType()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region "Declarations"

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();


    #endregion
    public int CIDHDN { get; set; }
    public int CID { get; set; }
    public int UserId { get; set; }
    public string txtCodeno { get; set; }
    public string txtRange { get; set; }
    public string txtcertificateno { get; set; }
    public string fbUpload { get; set; }
    public string txtCalibrationdon { get; set; }
    public string txtCalibrationdue { get; set; }

    public DataSet VCallibrationMaster()
    {
        DataSet dsw = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_VCallibrationMaster";
            c.Parameters.Add("@CID", CID);
            c.Parameters.Add("@txtCodeno", txtCodeno);
            c.Parameters.Add("@txtRange", txtRange);
            c.Parameters.Add("@txtcerfificateno", txtcertificateno);
            c.Parameters.Add("@txtCalibrationdon", txtCalibrationdon);
            c.Parameters.Add("@txtCalibrationdue", txtCalibrationdue);
            c.Parameters.Add("@UserId", UserId);
            c.Parameters.Add("@fbUpload", fbUpload);
            DAL.GetDataset(c, ref dsw);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return dsw;
    }

    public DataSet GetCallibrationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GETCallibrationmaster";
            c.Parameters.Add("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet DeleteCallibrationDetails()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_DeleteCallibrationDetails";
            c.Parameters.AddWithValue("@CID", CID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }


}