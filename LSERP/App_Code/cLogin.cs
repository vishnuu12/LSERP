using System;
using System.Data;
using eplus.data;

namespace eplus.core
{
    public class cLogin
    {
        #region "Properties"

        public string userID { get; set; }
        public string passWord { get; set; }
        public string ipAddress { get; set; }
        public string source { get; set; }
        public string smid { get; set; }


        #endregion

        #region "Methods"

        public DataSet getLoginDetails()
        {
            DataSet dsLoginDetails = new DataSet();
            try
            {
                cDataAccess DAL = new cDataAccess();
                string[] paramNames = { "@LoginName", "@Password" };
                object[] paramValue = new object[2];
                paramValue[0] = userID;
                paramValue[1] = passWord;
                DAL.GetDataset("LS_GetLoginDetails", paramValue, paramNames, ref dsLoginDetails);
            }
            catch (Exception e)
            {

            }
            return dsLoginDetails;
        }
        public void saveLoginUserDetails()
        {
            try
            {
                cDataAccess DAL = new cDataAccess();
                string[] paramNames = { "@UserID", "@IpAddress", "@Source" };
                object[] paramValue = new object[3];
                paramValue[0] = userID;
                paramValue[1] = ipAddress;
                paramValue[2] = source;
                DAL.GetScalar("LS_SaveUserLoginDetails", paramValue, paramNames);
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }

        #endregion
        }
    }
}
