using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EPSEntities;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using System.Windows.Forms;
using Common.StringUtilities;
using EPSEntities.Connection;

namespace Modules.Utilities
{
    public static class AuditTrail
    {
        public static ConnectionString dbConn = new ConnectionString();

        public static int InsertTrail(string strUserCode, string strModuleCode, string strTable, string strObject)
        {
            string strQuery = string.Empty;

            var db = new EPSConnection(dbConn);

            DateTime dtCurrent = AppSettingsManager.GetSystemDate();

            try { 
            strQuery = $"insert into trail values (:1,:2,to_date(:3,'MM/dd/yyyy'),:4,:5,:6)";
            db.Database.ExecuteSqlCommand(strQuery, 
                new OracleParameter(":1", strUserCode),
                new OracleParameter(":2", strModuleCode),
                new OracleParameter(":3", string.Format("{0:MM/dd/yyyy}", dtCurrent)),
                new OracleParameter(":4", string.Format("{0:HH:mm:ss}", dtCurrent)),
                new OracleParameter(":5", strTable),
                new OracleParameter(":6", StringUtilities.HandleApostrophe(strObject)));
            }
            catch (Exception ex)
            {
                ex.ToString(); 
                MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            return 1;
        }

        public static int InsertTrail(string strModuleCode, string strTable, string strObject)
        {
            string strQuery = string.Empty;

            var db = new EPSConnection(dbConn);

            DateTime dtCurrent = AppSettingsManager.GetSystemDate();

            try
            {
                strQuery = $"insert into trail values (:1,:2,to_date(:3,'MM/dd/yyyy'),:4,:5,:6)";
                db.Database.ExecuteSqlCommand(strQuery,
                    new OracleParameter(":1", AppSettingsManager.SystemUser.UserCode),
                    new OracleParameter(":2", strModuleCode),
                    new OracleParameter(":3", string.Format("{0:MM/dd/yyyy}", dtCurrent)),
                    new OracleParameter(":4", string.Format("{0:HH:mm:ss}", dtCurrent)),
                    new OracleParameter(":5", strTable),
                    new OracleParameter(":6", StringUtilities.HandleApostrophe(strObject)));
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }

            return 1;
        }

    }
}
