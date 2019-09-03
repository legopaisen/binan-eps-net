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

namespace Modules.Records
{
    public class ExcavationTblList
    {
        public static ConnectionString dbConn = new ConnectionString();

        public ExcavationTblList()
        {
        }

        // this is another way of getting the list
        public static List<EXCAVATION_TBL> GetRecord(string sWhereCond)
        {
            using (var db = new EPSConnection(dbConn))
            {
                string sQuery = string.Empty;
                sQuery = $"select * from excavation_tbl";
                if (!string.IsNullOrEmpty(sWhereCond))
                    sQuery += $" {sWhereCond}";
                return db.Database.SqlQuery<EXCAVATION_TBL>(sQuery).ToList();
            }
        }

        
    }
    
}
