using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GemBox.Spreadsheet;

public partial class Pages_ExcelIntoDataTable : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            //ExcelIntoDatatable("C:\\Users\\uppala\\Desktop\\MRNFormat Updated.xlsx");
            ConvertToDataTable("C:\\Users\\uppala\\Desktop\\MRNFormat Updated.xlsx");
            // vv("C:\\Users\\uppala\\Desktop\\MRNFormat Updated.xlsx");
            //  GenerateExcelData("C:\\Users\\uppala\\Desktop\\MRNFormat Updated.xlsx");
        }
    }

    private void ConvertToDataTable(string path)
    {
        // If using Professional version, put your serial key below.
        string Key = ConfigurationManager.AppSettings["GemBoxKey"].ToString();
        SpreadsheetInfo.SetLicense(Key);

        var workbook = ExcelFile.Load(path);

        // Select the first worksheet from the file.
        var worksheet = workbook.Worksheets[0];

        // Create DataTable from an Excel worksheet.
        var dataTable = worksheet.CreateDataTable(new CreateDataTableOptions()
        {
            ColumnHeaders = true,
            StartRow = 0,
            NumberOfColumns = worksheet.Columns.Count,
            NumberOfRows = worksheet.Rows.Count - 1,
            Resolution = ColumnTypeResolution.AutoPreferStringCurrentCulture
        });

        DataTable dt = new DataTable();

        dt = (DataTable)dataTable;

        // Write DataTable content
        var sb = new StringBuilder();
        sb.AppendLine("DataTable content:");
        foreach (DataRow row in dataTable.Rows)
        {
            sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}", row[0], row[1], row[2], row[3], row[4]);
            sb.AppendLine();
        }
        //Console.WriteLine(sb.ToString());
    }
}


//private DataTable ConvertToDataTable(string path)
//{
//    DataTable res = null;
//    try
//    {
//        //string path = System.IO.Path.GetFullPath(FileName);
//        OdbcConnection odbcConn = new OdbcConnection(@" Data Source='Excel Files';Dbq=" + path + ";ReadOnly=0;");
//        odbcConn.Open();

//        OdbcCommand cmd = new OdbcCommand();

//        OdbcDataAdapter oleda = new OdbcDataAdapter();

//        DataSet ds = new DataSet();

//        DataTable dt = odbcConn.GetSchema("Tables");

//        string sheetName = string.Empty;

//        if (dt != null)
//        {
//            sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
//        }

//        cmd.Connection = odbcConn;

//        cmd.CommandType = CommandType.Text;

//        cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

//        oleda = new OdbcDataAdapter(cmd);

//        oleda.Fill(ds, "excelData");

//        res = ds.Tables["excelData"];

//        odbcConn.Close();
//    }

//    catch (Exception ex)

//    {

//    }

//    finally

//    {



//    }

//    return res;

//}

//public void ExcelIntoDatatable(string FileName)
//{
//    try
//    {
//        DataTable dtResult = null;
//        int totalSheet = 0; //No of sheets on excel file  
//        using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + FileName + ";Extended Properties='Excel 16.0;HDR=YES;IMEX=1;';"))
//        {
//            objConn.Open();
//            OleDbCommand cmd = new OleDbCommand();
//            OleDbDataAdapter oleda = new OleDbDataAdapter();
//            DataSet ds = new DataSet();
//            DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
//            string sheetName = string.Empty;
//            if (dt != null)
//            {
//                var tempDataTable = (from dataRow in dt.AsEnumerable()
//                                     where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
//                                     select dataRow).CopyToDataTable();
//                dt = tempDataTable;
//                totalSheet = dt.Rows.Count;
//                sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
//            }
//            cmd.Connection = objConn;
//            cmd.CommandType = CommandType.Text;
//            cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
//            oleda = new OleDbDataAdapter(cmd);
//            oleda.Fill(ds, "excelData");
//            dtResult = ds.Tables["excelData"];
//            objConn.Close();
//        }
//    }
//    catch (Exception ex)
//    {
//        Log.Message(ex.ToString());
//    }
//}

////private void vv(string filePath)
////{
////    FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

////    //1. Reading from a binary Excel file ('97-2003 format; *.xls)
////    IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
////    //...       
////    //3. DataSet - The result of each spreadsheet will be created in the result.Tables
////    DataSet result = excelReader.AsDataSet();
////    //...
////    //4. DataSet - Create column names from first row
////    excelReader.IsFirstRowAsColumnNames = true;
////    // DataSet result = excelReader.AsDataSet();

////    //5. Data Reader methods
////    while (excelReader.Read())
////    {
////        //excelReader.GetInt32(0);
////    }

////    //6. Free resources (IExcelDataReader is IDisposable)
////    excelReader.Close();
////}

//private void GenerateExcelData(string read)
//{
//    try
//    {
//        //            string read = System.IO.Path.GetFullPath(Server.MapPath("~/empdetail.xlsx"));

//        if (Path.GetExtension(read) == ".xls")
//        {
//            x = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + read + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"");
//        }
//        else if (Path.GetExtension(read) == ".xlsx")
//        {
//            x = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + read + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
//        }
//        x.Open();
//        OleDbCommand y = new OleDbCommand();
//        OleDbDataAdapter z = new OleDbDataAdapter();
//        DataSet dset = new DataSet();
//        y.Connection = x;
//        y.CommandType = CommandType.Text;
//        y.CommandText = "SELECT distinct([Slno]) FROM [Sheet1$]";
//        z = new OleDbDataAdapter(y);
//        z.Fill(dset, "Slno");
//        //dropdown1.DataSource = dset.Tables["Slno"].DefaultView;
//        //if (!IsPostBack)
//        //{
//        //    dropdown1.DataTextField = "Slno";
//        //    dropdown1.DataValueField = "Slno";
//        //    dropdown1.DataBind();
//        //}
//        //if (!String.IsNullOrEmpty(SlnoAbbreviation) && SlnoAbbreviation != "Choose")
//        //{
//        //    y.CommandText = "SELECT [Slno], [EmpName], [Salaray], [Location]" +
//        //        "  FROM [Sheet1$] where [Slno]= @Slno_Abbreviation";
//        //    y.Parameters.AddWithValue("@Slno_Abbreviation", SlnoAbbreviation);
//        //}
//        //else
//        //{
//        //    y.CommandText = "SELECT [Slno],[EmpName],[Salary],[Location] FROM [Sheet1$]";
//        //}
//        //z = new OleDbDataAdapter(y);
//        //z.Fill(dset);

//        //Grid1.DataSource = dset.Tables[1].DefaultView;
//        //Grid1.DataBind();
//    }
//    catch (Exception ex)
//    {
//        //lbl1.Text = ex.ToString();
//    }
//    finally
//    {
//        x.Close();
//    }
//}


//public void exceldatatable()
//{
//    // If using Professional version, put your serial key below.
//    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

//    // Load Excel workbook from file's path.
//    ExcelFile workbook = ExcelFile.Load("SimpleTemplate.xlsx");

//    // Iterate through all worksheets in a workbook.
//    foreach (ExcelWorksheet worksheet in workbook.Worksheets)
//    {
//        // Display sheet's name.
//        Console.WriteLine("{1} {0} {1}\n", worksheet.Name, new string('#', 30));

//        // Iterate through all rows in a worksheet.
//        foreach (ExcelRow row in worksheet.Rows)
//        {
//            // Iterate through all allocated cells in a row.
//            foreach (ExcelCell cell in row.AllocatedCells)
//            {
//                // Read cell's data.
//                string value = cell.Value?.ToString() ?? "EMPTY";

//                // Display cell's value and type.
//                value = value.Length > 15 ? value.Remove(15) + "…" : value;
//                Console.Write($"{value} [{cell.ValueType}]".PadRight(30));
//            }

//            Console.WriteLine();
//        }
//    }
//}

