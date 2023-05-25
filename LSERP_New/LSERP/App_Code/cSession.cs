using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cSession
/// </summary>
public class cSession
{
    public string employeeid { get; set; }
    public string loginname { get; set; }
    public string name { get; set; }
    public int type { get; set; }
    public string mail { get; set; }
    public string mobno { get; set; }
    public int ERPUsertype { get; set; }
    public int DepID { get; set; }
    public string DepName { get; set; }
    public string Designation { get; set; }
    public string UserPhoto { get; set; }
    public string lastLoginDateTime { get; set; }
    public string CreatedBy { get; set; }
    public cSession()
    {

    }
}