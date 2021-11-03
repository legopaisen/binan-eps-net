using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using EPSEntities.Entity;

namespace Modules.Utilities
{
    public class BankList
    {
        public static ConnectionString dbConn = new ConnectionString();

        public static List<BANK_TABLE> GetBankList(string sBankCode)
        {
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = "select * from bank_table ";
                if (!string.IsNullOrEmpty(sBankCode))
                    //strQuery += $" where bank_code = '{sBankCode}'";
                    strQuery += $" where bank_id = '{sBankCode}'";
                strQuery += "order by bank_nm ";
                return db.Database.SqlQuery<BANK_TABLE>(strQuery).ToList();
            }
        }
    }
}
