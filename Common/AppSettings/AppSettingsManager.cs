using System;
using System.Collections.Generic;
using System.Text;
using Common.DataConnector;
using System.IO;
using System.Windows.Forms;


namespace Common.AppSettings
{
    //public sealed class AppSettingsManager
    public static class AppSettingsManager
    {
        private static string sObject;
        public static SystemUser g_objSystemUser;
        private static int m_intYear;
        private static string m_strValidityInMonth;

        /*
        static readonly AppSettingsManager instance = new AppSettingsManager();

        public static AppSettingsManager Instance
        {
            get
            {
                return instance;
            }
        }
         */

        public static SystemUser SystemUser
        {
            get { return g_objSystemUser; }
            set { g_objSystemUser = value; }
        }


        //MCR 20141209 (s)
        private static string m_sSystemType = string.Empty;
        public static string GetSystemType
        {
            get { return m_sSystemType; }
            set { m_sSystemType = value; }
        }
        //MCR 20141209 (e)

        /// <summary>
        /// This static method returns current date/time of database server
        /// </summary>
        /// <returns>the current date/time</returns>
        public static DateTime GetSystemDate() // ALJ 20090902 change to public // pending cosder freeze date
        {

            DateTime dtSystemDate = DateTime.Now;
            OracleResultSet result = new OracleResultSet();
            result.Query = "SELECT SYSDATE FROM DUAL";
            if (result.Execute())
            {
                if (result.Read())
                {
                    dtSystemDate = result.GetDateTime(0);
                }
            }
            result.Close();
            return dtSystemDate;

            //return new DateTime(2009, 01, 13);
        }
        /// <summary>
        /// This static method sets current date/time of database server 
        /// </summary>
        /// <param name="dtCurrentDate">the date/time to be set</param>
        /// <returns>returns true if new date was successfully set otherwise false</returns>
        public static bool SetCurrentDate(DateTime dtCurrentDate)
        {
            OracleResultSet result = new OracleResultSet();

            result.Query = "SELECT COUNT(*) FROM datetime_setting WHERE description = :1";
            result.AddParameter(":1", "current_datetime");

            int intCount = 0;
            int.TryParse(result.ExecuteScalar().ToString(), out intCount);
            if (intCount == 0)
            {
                result.Query = "INSERT INTO datetime_setting VALUES (:1, :2)";
                result.AddParameter(":1", "current_datetime");
                result.AddParameter(":2", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dtCurrentDate));
                if (result.ExecuteNonQuery() == 0)
                {
                    result.Close();
                    return false;
                }
            }
            else
            {
                result.Query = "UPDATE datetime_setting SET datetime = :1 WHERE description = :2";
                result.AddParameter(":1", string.Format("{0:MM/dd/yyyy HH:mm:ss}", dtCurrentDate));
                result.AddParameter(":2", "current_datetime");
                if (result.ExecuteNonQuery() == 0)
                {
                    result.Close();
                    return false;
                }
            }
            result.Close();
            return true;
        }

        public static DateTime GetCurrentDate(bool blnHasUpdate)
        {
            DateTime dtSystemDate = GetSystemDate();
            /*
            if (blnHasUpdate && SetCurrentDate(dtSystemDate))
            {
            }
            else
            {
                OracleResultSet result = new OracleResultSet();
                result.Query = "SELECT TO_DATE(TRIM(datetime),'mm/dd/yyyy HH24:MI:SS') FROM datetime_setting";
                if (result.Execute())
                {
                    if (result.Read())
                    {
                        dtSystemDate =  result.GetDateTime(0);
                    }
                }
                result.Close();
            }
            */
            return dtSystemDate;
        }

        public static DateTime GetCurrentDate()
        {
            //return GetCurrentDate(true);
            return GetCurrentDate(false);
        }

        /*
        private static string GetValue(string strTable, string strConfigField, string strValueField, string strCode)
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = string.Format("SELECT {0} FROM {1} WHERE {2} = :1",
                strValueField, strTable, strConfigField);
            result.AddParameter(":1", strCode);
            if (result.Execute())
            {
                if (result.Read())
                {
                    return result.GetString(0).Trim();
                }
            }
            return string.Empty;
        }

        public static string GetConfigValue(string strConfigCode)
        {
            return AppSettingsManager.GetValue("config_table", "subj_code", "value_fld", strConfigCode);
        }

        public static string GetErrorValue(string strErrorCode)
        {
            return AppSettingsManager.GetValue("error_tbl", "error_code", "error_desc");
        }
         */

        /*
        public static string GetConfigValueByDescription(string strConfigDescription)
        {
            string strConfigValue = string.Empty;
            OracleResultSet result = new OracleResultSet();
            result.Query = "SELECT value_fld FROM config_table WHERE subj_desc = RPAD(:1, 255)";
            result.AddParameter(":1", strConfigDescription);
            if (result.Execute())
            {
                if (result.Read())
                {
                    strConfigValue = result.GetString(0).Trim();
                }
            }
            result.Close();
            return strConfigValue;
        }
        */

        public static string GetConfigValue(string strConfigCode)
        {
            return AppSettingsManager.GetConfigValue("VALUE_FLD", "config", "subj_code",
                ":1", strConfigCode);
        }

        public static string GetConfigValueByDescription(string strConfigDescription)
        {
            return AppSettingsManager.GetConfigValue("VALUE_FLD", "config", "subj_desc",
                "RPAD(:1, 8)", strConfigDescription);
        }

        public static string GetAuctionConfigValue(string strAuctionCode)
        {
            return AppSettingsManager.GetConfigValue("value_fld", "auction_config", "subj_code",
                "RPAD(:1, 2)", strAuctionCode);
        }


        public static string GetConfigObject(string sCode)
        {

            OracleResultSet xxx = new OracleResultSet();
            xxx.Query = "SELECT OBJECT FROM CONFIG WHERE TRIM(CODE) = :1";
            xxx.AddParameter(":1", sCode);
            if (xxx.Execute())
            {
                if (xxx.Read() == true)
                {
                    sObject = xxx.GetString("object").Trim();
                }
            }
            xxx.Close();
            return sObject;

        }

        public static string GetConfigDescription(string strConfigCode)
        {
            return AppSettingsManager.GetConfigValue("subj_desc", "config", "subj_code",
                "RPAD(:1, 2)", strConfigCode);
        }

        public static string GetConfigValue(string strValueField, string strConfigTable,
            string strSubjectField, string strFormatField, string strConfigCode)
        {
            string strConfigValue = string.Empty;
            OracleResultSet result = new OracleResultSet();
            result.Query = string.Format("SELECT {0} FROM {1} WHERE {2} = {3}", strValueField,
                strConfigTable, strSubjectField, strFormatField);
            //"SELECT value_fld FROM config_table WHERE subj_code = RPAD(:1, 3)";
            result.AddParameter(":1", strConfigCode);
            if (result.Execute())
            {
                if (result.Read())
                {
                    strConfigValue = result.GetString(0).Trim();
                }
            }
            result.Close();
            return strConfigValue;
        }

        public static bool HasRights(string strUserCode, string strModuleCode)
        {
            bool blnHasRights = false;
            OracleResultSet result = new OracleResultSet();
            result.Query = "SELECT COUNT(*) FROM auth WHERE trim(usr_code) = :1 AND mod_code = RPAD(:2, 10)";
            result.AddParameter(":1", strUserCode);
            result.AddParameter(":2", strModuleCode);
            int intCount = 0;
            int.TryParse(result.ExecuteScalar(), out intCount);
            if (intCount != 0)
                blnHasRights = true;
            result.Close();
            return blnHasRights;
        }

        public static string GetWorkstationName()
        {
            return Environment.MachineName.Trim();
        }

        //RDO 04302008 (s) get municipal code
        public static string GetMunicipalCode(string strMunicipalName)
        {
            string strMunicipalCode = string.Empty;
            string strVersion = string.Empty;
            strVersion = AppSettingsManager.GetConfigValue("60");
            OracleResultSet result = new OracleResultSet();
            if (strVersion == "PM")
            {
                result.Query = "SELECT dist_code FROM districts WHERE dist_nm = RPAD(:1, 20)";
                result.AddParameter(":1", strMunicipalName.Trim());
                strMunicipalCode = result.ExecuteScalar();
            }
            else if ((strVersion == "M" || strVersion == "C" || strVersion == "CC") && AppSettingsManager.GetConfigValue("10") == "Y")
            {
                result.Query = "SELECT dist_code FROM districts";
                strMunicipalCode = result.ExecuteScalar();
            }
            result.Close();

            return strMunicipalCode;
        }
        //RDO 04302008 (e) get municipal code


        //for compatibility with older version
        public static string GetDistrictCode()
        {
            string strDistrictCode = string.Empty;
            string strPath = string.Format("{0}/config_wan.ini", Environment.GetFolderPath(Environment.SpecialFolder.System));
            if (File.Exists(strPath))
            {
                //open and read contents
                FileInfo fileInf = new FileInfo(strPath);
                FileStream data = fileInf.OpenRead();

                Encoding enc = Encoding.UTF8;
                byte[] byteBuffer = new byte[fileInf.Length];
                data.Read(byteBuffer, 0, byteBuffer.Length);
                string strValue = enc.GetString(byteBuffer);
                data.Close();

                string strTray = string.Empty;

                strValue = strValue.Replace('\r', ' ');

                string[] strLines = strValue.Split('\n');
                for (int i = 0; i < strLines.Length; i++)
                {
                    string[] strColumns = strLines[i].Split('=');
                    strTray = strColumns[0].Trim().ToUpper();
                    if (strTray.Length >= 4 && strTray.Substring(0, 4) == "TRAY" && strColumns.Length > 1)
                    {
                        string[] strFields = strColumns[1].Split(',');
                        //need add codes if one district only
                        strDistrictCode = strFields[0];

                        break;
                    }
                }
            }

            return strDistrictCode;
        }

        public static int Year
        {
            get { return m_intYear; }
            set { m_intYear = value; }
        }

        public static string GetAcctName(string p_sReturnValue, string p_sAcctCode)
        {
            OracleResultSet pSet = new OracleResultSet();
            string sAcctName = string.Empty;
            string sAcctAdd = string.Empty;
            string sFName = string.Empty;
            string sMI = string.Empty;
            string sLName = string.Empty;
            string sHseNo = string.Empty;
            string sLotNo = string.Empty;
            string sBlkNo = string.Empty;
            string sStreet = string.Empty;
            string sBrgy = string.Empty;
            string sCity = string.Empty;
            string sTCT = string.Empty;

            pSet.Query = "select * from account where acct_code = '" + p_sAcctCode + "'";
            if (pSet.Execute())
            {
                if (pSet.Read())
                {
                    sFName = pSet.GetString("acct_fn").Trim();
                    sMI = pSet.GetString("acct_mi").Trim();
                    sLName = pSet.GetString("acct_ln").Trim();

                    if (sMI != "")
                        sMI = sMI + ".";
                    sAcctName = sFName + " " + sMI + " " + sLName;

                    sHseNo = pSet.GetString("acct_hse_no").Trim();
                    sLotNo = pSet.GetString("acct_lot_no").Trim();
                    sBlkNo = pSet.GetString("acct_blk_no").Trim();
                    sStreet = pSet.GetString("acct_addr").Trim();
                    sBrgy = pSet.GetString("acct_brgy").Trim();
                    sCity = pSet.GetString("acct_city").Trim();
                    sTCT = pSet.GetString("acct_tct").Trim();

                    if (sHseNo != "")
                        sHseNo = "#" + sHseNo;
                    if (sBlkNo != "")
                        sBlkNo = " Blk " + sBlkNo + ",";
                    if (sLotNo != "")
                        sLotNo = " Lot " + sLotNo + ",";
                    if (sStreet != "")
                        sStreet = " " + sStreet + ",";
                    if (sBrgy != "")
                        sBrgy = " " + sBrgy + ",";
                    if (sCity != "")
                        sCity = " " + sCity;

                    sAcctAdd = sHseNo + sBlkNo + sLotNo + sStreet + sBrgy + sCity;

                }
            }
            pSet.Close();

            if (p_sReturnValue == "AcctName")
            {
                return sAcctName;
            }
            else if (p_sReturnValue == "TCT")
            {
                return sTCT;
            }
            else
            {
                return sAcctAdd;
            }
        }

        public static DateTime ComputeExpiryDate(string p_sTRN, DateTime p_odtDateReg)
        {
            // RMC 20130214 added function in appsettingsmanager to avoid circular dependencies error when adding reference in projects

            DateTime odtDateToExpire;
            DateTime odtDateReg;

            int intTemp = 0;
            int intDays = 0;

            intDays = Convert.ToInt32(AppSettingsManager.GetConfigValue("17"));

            odtDateReg = p_odtDateReg.AddDays(-(intDays));

            intTemp = GetExpiryPeriod();

            //compute expiry date
            if (m_strValidityInMonth == "Y")  //in months
            {
                odtDateToExpire = odtDateReg.AddMonths(intTemp);
            }
            else
            {   //in years
                odtDateToExpire = odtDateReg.AddYears(intTemp);
            }

            return odtDateToExpire;

        }

        public static int GetExpiryPeriod()
        {
            // RMC 20130214 added function in appsettingsmanager to avoid circular dependencies error when adding reference in projects
            OracleResultSet pSet = new OracleResultSet();
            int intTemp = 0;
            string strTemp = string.Empty;

            pSet.Query = "select * from config where subj_desc = 'EXPIRY PERIOD (IN MONTHS)' or subj_desc = 'EXPIRY PERIOD (IN YEARS)'";
            if (pSet.Execute())
            {
                if (pSet.Read())
                {
                    strTemp = pSet.GetString("subj_desc");
                    if (strTemp == "EXPIRY PERIOD (IN MONTHS)")
                        m_strValidityInMonth = "Y";
                    else
                        m_strValidityInMonth = "N";
                    strTemp = pSet.GetString("value_fld");
                }
                else
                    strTemp = "0";

                int.TryParse(strTemp, out intTemp);
            }
            pSet.Close();

            return intTemp;
        }

        public static bool Granted(string strGrantedCode)
        {
            bool bGranted = false;
            OracleResultSet result = new OracleResultSet();
            result.Query = "select * from users_rights where user_code = '" + AppSettingsManager.SystemUser.UserCode + "' and right_code = '" + strGrantedCode.Trim() + "'";
            if (result.Execute())
            {
                if (result.Read())
                    bGranted = true;
                else
                {
                    MessageBox.Show("Access Denied!");
                    bGranted = false;
                }
            }
            result.Close();
            return bGranted;
        }

        public static string GetOneDistrictCode()
        {
            OracleResultSet result = new OracleResultSet();
            string strDistCode = string.Empty;

            result.Query = "select distinct dist_code from brgy order by dist_code";
            if (result.Execute())
            {
                if (result.Read())
                    strDistCode = result.GetString(0);

            }
            result.Close();

            return strDistCode;
        }

        public static string GetBlobImageConfig()
        {
            // RMC 20150226 adjustment in blob configuration
            string sConfig = string.Empty;
            string sTmp = string.Empty;
            OracleResultSet result = new OracleResultSet();
            result.CreateBlobConnection();

            //if (m_sSystemType == "A")
            //{
            //    sTmp = AppSettingsManager.GetConfigValue("62");
            //}
            //else
            //{
            //    sTmp = AppSettingsManager.GetConfigValue("63");
            //}

            //result.Query = "select * from sourcedoc_tbl where srcdoc_desc = '" + sTmp + "' and system_code = '" + m_sSystemType + "'";
            //if (result.Execute())
            //{
            //    if (result.Read())
            //        sConfig = result.GetString("srcdoc_code");
            //}
            //result.Close();

            //TEMPORARILY APPLIED FIXED EPS QUERY
            result.Query = "select * from sourcedoc_tbl where srcdoc_desc = 'ENGINEERING RECORDS' and system_code = 'E'";
            if (result.Execute())
            {
                if (result.Read())
                    sConfig = result.GetString("srcdoc_code");
            }
            result.Close();


            return sConfig;
        }

        public static string GetPermitAcro(string sPermit)
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select permit_desc_code from permit_tbl where permit_code = '" + sPermit + "'";
            if (result.Execute())
                if (result.Read())
                    sPermit = result.GetString("permit_desc_code");
            result.Close();
            return sPermit;
        }

        public static string GetPermitDesc(string sPermit)
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select permit_desc from permit_tbl where permit_code = '" + sPermit + "'";
            if (result.Execute())
                if (result.Read())
                    sPermit = result.GetString("permit_desc");
            result.Close();

            return sPermit;
        }

        public static string GetPermitCode(string sPermit)
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select permit_code from permit_tbl where permit_desc like '%" + sPermit + "%'";
            if (result.Execute())
                if (result.Read())
                    sPermit = result.GetString("permit_code");
            result.Close();

            return sPermit;
        }

        public static bool GetAreaNeedValue(string sFeesCode)
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select area_needed,fees_means from subcategories where fees_term <> 'SUBCATEGORIES' and fees_code = '" + sFeesCode + "'";
            if (result.Execute())
                if (result.Read())
                {
                    if (result.GetString("area_needed") == "Y" || result.GetString("fees_means") == "AR")
                        return true;
                    else
                        return false;
                }
            result.Close();
            return false;
        }

        public static bool GetPaidStatus(string sARN)
        {
            OracleResultSet result = new OracleResultSet();
            int cnt = 0;
            result.Query = $"select count(*) from payments where arn = '{sARN}'";
            int.TryParse(result.ExecuteScalar(), out cnt);
            if (cnt > 0)
                return true;
            else
                return false; 
        }
    }
}
