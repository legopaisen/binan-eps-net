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
using EPSEntities.Connection;

namespace Modules.Utilities
{
    public class Occupancy
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string Code { get; set; }
        public string Desc { get; set; }
        //public string Category { get; set; }

        public Occupancy()
        {
            this.Clear();
        }

        public void Clear()
        {
            Code = string.Empty;
            Desc = string.Empty;
            //Category = string.Empty;
        }

        //public Occupancy(string sCode, string sDesc, string sCategory)
        public Occupancy(string sCode, string sDesc)
        {
            Code = sCode;
            Desc = sDesc;
            //Category = sCategory;
        }

        
    }

    public class OccupancyList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Occupancy> m_List;

        public List<Occupancy> OccupancyLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public OccupancyList(string sCategory, string sOccupancy)
        {
            m_List = new List<Occupancy>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from occupancy_tbl ";
                if(!string.IsNullOrEmpty(sCategory))
                    strQuery += $" where category_code = '{sCategory}' ";
                if(!string.IsNullOrEmpty(sOccupancy))
                    strQuery += $" and occ_code = '{sOccupancy}' "; 
                strQuery += $"order by occ_code ";
                var epsrec = db.Database.SqlQuery<OCCUPANCY_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Occupancy(items.OCC_CODE, items.OCC_DESC));
                }
            }
        }

        public string GetOccupancyCode(string sPermit)
        {
            string sPermitCode = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from occupancy_tbl where OCC_DESC = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<OCCUPANCY_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitCode = items.OCC_CODE;
                }
            }

            return sPermitCode;
        }
    }
}
