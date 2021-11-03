using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.AppSettings;
using EPSEntities.Entity;
using EPSEntities.Connection;
using EPSEntities;

namespace Modules.Records
{
    public class OtherCertList
    {
        public static ConnectionString dbConn = new ConnectionString();

        public OtherCertList()
        {
        }

        // this is another way of getting the list
        public static List<OTHER_CERT> GetRecord(string sWhereCond)
        {
            using (var db = new EPSConnection(dbConn))
            {
                string sQuery = string.Empty;
                sQuery = $"select * from other_cert";
                if (!string.IsNullOrEmpty(sWhereCond))
                    sQuery += $" {sWhereCond}";
                return db.Database.SqlQuery<OTHER_CERT>(sQuery).ToList();
            }
        }

        
    }
    
}
