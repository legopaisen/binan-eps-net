using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EPSEntities.Connection;
using EPSEntities.Entity;

namespace Modules.Utilities
{
    public class Subcategories
    {
        public static ConnectionString dbConn = new ConnectionString();

        public DataTable GetSubcategory(string sPermitCode, string sArn, string sTable)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(" ", typeof(Boolean));
            dataTable.Columns.Add("Fees", typeof(String));
            dataTable.Columns.Add("Code", typeof(String));
            dataTable.Columns.Add("Means", typeof(String));
            dataTable.Columns.Add("Unit", typeof(String));
            dataTable.Columns.Add("Area", typeof(String));
            dataTable.Columns.Add("Cumulative", typeof(String));
            dataTable.Columns.Add("UnitValue", typeof(String));
            dataTable.Columns.Add("Amount", typeof(String));
            dataTable.Columns.Add("Category", typeof(String));
            dataTable.Columns.Add("Scope", typeof(String));
            dataTable.Columns.Add("OrigAmt", typeof(String));

            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            sQuery = $"select (ss.fees_desc || ', ' || S.fees_desc) as fees_desc ,S.fees_code, S.fees_means,";
            sQuery += " S.fees_unit,S.area_needed,S.cumulative, '0','0.00', S.category_code,S.scope_code ";
            sQuery += " from subcategories S left join subcategories ss on ss.fees_code = Substr(S.fees_code, 0, 4) ";
            sQuery += $" where S.fees_code in (select fees_code from permit_fees_tbl where permit_code = '{sPermitCode}') ";
            sQuery += $" and S.struc_code like (select distinct '%' || struc_code || '%' from {sTable} where arn = '{sArn}') ";
            sQuery += $" and S.scope_code like (select distinct '%' || scope_code || '%' from {sTable} where arn = '{sArn}') order by S.fees_code";
            var epsrec = db.Database.SqlQuery<SUBCATEGORIES>(sQuery);

            foreach (var items in epsrec)
            {
                dataTable.Rows.Add(false,items.FEES_DESC,items.FEES_CODE,items.FEES_MEANS,items.FEES_UNIT,items.AREA_NEEDED, items.CUMULATIVE,0,0.00,items.CATEGORY_CODE,items.SCOPE_CODE,0.00);
            }
            return dataTable;
        }

        public string GetFeesDesc(string sFeesCode)
        {
            string sFeesDesc = string.Empty;

            var db = new EPSConnection(dbConn);
            string sQuery = string.Empty;

            sQuery = $"select * from subcategories where fees_code = '{sFeesCode}'";
            var epsrec = db.Database.SqlQuery<SUBCATEGORIES>(sQuery);
            foreach (var items in epsrec)
            {
                sFeesDesc = items.FEES_DESC;
            }

            return sFeesDesc;
        }
    }
}
