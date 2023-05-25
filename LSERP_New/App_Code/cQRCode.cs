using System.Data;
using System;
using QRCodeEncoderDecoderLibrary;
using System.Drawing;
using System.Linq;
using eplus.core;
using System.Data.SqlClient;
using eplus.data;


/// <summary>
/// Summary description for cqrcode
/// </summary>
public class cQRcode
{

    #region "Declarations"

    public DataSet ds;
    SqlCommand c = new SqlCommand();
    public static Random random = new Random();
    cDataAccess DAL = new cDataAccess();

    #endregion

    #region "Properties"

    public string code { get; set; }
    public string pID { get; set; }
    public string fileName { get; set; }
    public string QRNumber { get; set; }
    public string createdBy { get; set; }
    public string collegeMasterID { get; set; }
    public string Pagename { get; set; }

    #endregion

    #region "Constructors"

    public cQRcode()
    {
    }

    #endregion

    #region "Common Methods"

    public string QRcodeGeneration(string code)
    {
        string qrImage = "";
        try
        {
            QREncoder QRCodeEncoder = new QREncoder();
            QRCodeEncoder.Encode(ErrorCorrection.M, code);
            // create bitmap image
            // each module will be 4 by 4 pixels
            // the quiet zone around the QR Code will be 8 pixels
            Bitmap QRCodeImage = QRCodeToBitmap.CreateBitmap(QRCodeEncoder, 4, 8);
            //converting bitmap to image
            Image img = (Image)QRCodeImage;
            //convert image to bytes
            ImageConverter converter = new ImageConverter();
            byte[] filedata = (byte[])converter.ConvertTo(img, typeof(byte[]));
            //convert bytes to base 64 string
            qrImage = "data:image/png;base64," + Convert.ToBase64String(filedata);
        }
        catch (Exception)
        {

        }
        return qrImage;
    }

    public string generateQRNumber(int length)
    {
        string QRNumber = "";
        try
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            QRNumber = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            cQRcode objqr = new cQRcode();
            objqr.QRNumber = QRNumber;
            if (checkQRNumber() == true)
                generateQRNumber(length);
        }
        catch (Exception)
        {

        }
        return QRNumber;
    }

    public bool checkQRNumber()
    {
        bool checkNumber = false;
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_CheckQRNumber";
            c.Parameters.AddWithValue("@QRNumber", QRNumber);
            DAL.GetScalar(c);

            if (DAL.GetScalar(c).ToString() == "EXISTS")
                checkNumber = true;
        }
        catch (Exception)
        {

        }
        return checkNumber;
    }

    public string saveQRNumber()
    {
        string FName = "";
        try
        {
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveQRCodeNumber";
            c.Parameters.AddWithValue("@QRNumber", QRNumber);

            c.Parameters.AddWithValue("@PID", DBNull.Value);
            c.Parameters.AddWithValue("@FileName", fileName);
            c.Parameters.AddWithValue("@CreatedBy", createdBy);
            c.Parameters.AddWithValue("@PageName", Pagename);


            FName = DAL.GetScalar(c);
        }
        catch (Exception)
        {

        }
        return FName;
    }

    public string getDisplayQRNumber(string QRNumber, int length, char comChar)
    {
        string disQRNumber = "";
        try
        {
            if (QRNumber.Length % length == 0)
            {
                for (int i = 0; i < length; i++)
                    disQRNumber = disQRNumber + QRNumber.Substring(length * i, length) + comChar;

                disQRNumber = disQRNumber.Trim(comChar);
            }
        }
        catch (Exception)
        {

        }
        return disQRNumber;
    }

    #endregion

}