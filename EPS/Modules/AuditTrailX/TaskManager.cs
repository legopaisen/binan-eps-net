using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EPSEntities.Entity;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using System.Windows.Forms;
using Common.StringUtilities;

namespace Utilities
{
    public static class TaskManager
    {
        public static bool IsObjectLock(string strModuleCode, string strMode)
        {
            string strQuery = string.Empty;
            int intCnt = 0;
            string strUserCode = AppSettingsManager.SystemUser.UserCode;
            
            EPSEntities.EPSEntities db = new EPSEntities.EPSEntities();
          
            DateTime dtCurrent = AppSettingsManager.GetSystemDate();
            string strTime = string.Format("{0:MM/dd/yyyy hh:mm:ss}", dtCurrent);

            try {
                if (strMode == "DELETE")
                {
                    strQuery = $"delete from active_modules where module_code = '{strMode}' and user_code = '{strUserCode}'";
                    db.Database.ExecuteSqlCommand(strQuery);

                    return true;
                }
                else
                {
                    strQuery = $"select count(*) from active_modules where module_code = '{strMode}' and user_code = '{strUserCode}'";
                    intCnt = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

                    if (intCnt > 0)
                    {
                        MessageBox.Show(strUserCode + " already logged in this module", "EPS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strModuleCode))
                        {
                            strQuery = $"insert into active_modules values (:1,:2,:3)";
                            db.Database.ExecuteSqlCommand(strQuery,
                                new OracleParameter(":1", strModuleCode),
                                new OracleParameter(":2", strUserCode),
                                new OracleParameter(":3", strTime));
                            return false;
                        }
                    }
                }
                        
            }
            catch (Exception ex)
            {
                ex.ToString(); 
                MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }


    }
}
