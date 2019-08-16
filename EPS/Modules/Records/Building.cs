using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using EPSEntities.Entity;

namespace Modules.Records
{
    public class Building
    {
        public static ConnectionString dbConn = new ConnectionString();

        public static List<BUILDING> GetBuildingRecord(int iBldgNo)
        {
            using (var db = new EPSConnection(dbConn))
            {
                string sQuery = string.Empty;
                sQuery = $"select * from building where bldg_no = {iBldgNo} ";
                return db.Database.SqlQuery<BUILDING>(sQuery).ToList();
            }
        }
    }
}
