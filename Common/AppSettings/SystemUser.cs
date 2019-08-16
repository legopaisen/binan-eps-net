using System;
using System.Collections.Generic;
using System.Text;
using Common.DataConnector;
using Common.EncryptUtilities;

namespace Common.AppSettings
{
    public class SystemUser:User
    {
        private string m_strPosition;
        private string m_strMemo;
        private string m_strSystemCode;

        public SystemUser()
        {
            this.Clear();
        }

        /// <summary>
        /// ALJ 20100119 load sys user's details for BPLS
        /// </summary>
        /// <param name="strUserCode"></param>
        /// <returns></returns>
        public bool Load(string strUserCode)
        {
            this.Clear();
            OracleResultSet result = new OracleResultSet();
            result.Query = string.Format("SELECT user_ln, user_fn, user_mi, user_pos FROM users WHERE user_code = '{0}'",
                strUserCode);
            if (result.Execute())
            {
                if (result.Read())
                {
                    m_strUserCode = strUserCode;
                    m_strLastName = result.GetString("user_ln").Trim();
                    m_strFirstName = result.GetString("user_fn").Trim();
                    m_strMI = result.GetString("user_mi").Trim();
                    m_strPosition = result.GetString("user_pos").Trim();
                    return true;
                }
            }
            return false;
        }

        public bool Load(string strUserCode, string strSystemCode)
        {
            this.Clear();
            OracleResultSet result = new OracleResultSet();
            result.Query = string.Format("SELECT user_ln, user_fn, user_mi, user_pos, user_memo FROM users WHERE user_code = '{0}'",
                strUserCode);
            /*
            result.Query = "SELECT usr_ln, usr_fn, usr_mi, usr_pos, usr_memo FROM sys_users WHERE usr_code = RPAD(:1, 20) AND sys_code = :2";
            result.AddParameter(":1", strUserCode);
            result.AddParameter(":2", strSystemCode);
            */
            if (result.Execute())
            {
                if (result.Read())
                {
                    m_strUserCode = strUserCode;
                    m_strLastName = result.GetString("user_ln").Trim();
                    m_strFirstName = result.GetString("user_fn").Trim();
                    m_strMI = result.GetString("user_mi").Trim();
                    m_strPosition = result.GetString("user_pos").Trim();
                    m_strMemo = result.GetString("user_memo").Trim();
                    //m_strPassword = result.GetString("usr_pwd").Trim();
                    m_strSystemCode = strSystemCode;
                    return true;
                }
            }
            return false;
        }

        public string Position
        {
            get { return m_strPosition; }
        }

        public bool Authenticate(string strPassword)
        {
            //Common.EncryptUtilities.Encrypt enc = new Common.EncryptUtilities.Encrypt();            
            //string strEncrypted = enc.EncryptPassword(strPassword);
            Encryption enc = new Encryption();
            string strEncrypted = enc.EncryptString(strPassword);

            if (strPassword != string.Empty)
            {
                int intCount = 0;
                OracleResultSet result = new OracleResultSet();
                //use trim to capture ñ character //JVL mal
                result.Query = "SELECT COUNT(*) FROM users WHERE trim(user_code) = :1 AND trim(user_pwd) = :2";
                result.AddParameter(":1", m_strUserCode);               
                result.AddParameter(":2", strPassword);                              
                int.TryParse(result.ExecuteScalar().ToString(), out intCount);
                if (intCount != 0)
                {
                    return true;
                }

                result.Query = "SELECT COUNT(*) FROM users WHERE trim(user_code) = :1 AND trim(user_pwd) = :2";
                result.AddParameter(":1", m_strUserCode);
                result.AddParameter(":2", strEncrypted);                
                int.TryParse(result.ExecuteScalar().ToString(), out intCount);
                if (intCount != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Clear()
        {
            base.Clear();
            m_strPosition = string.Empty;
            m_strMemo = string.Empty;
            m_strSystemCode = string.Empty;
        }
    }
}
