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
    public class Structure
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string Code { get; set; }
        public string Desc { get; set; }

        public Structure()
        {
            this.Clear();
        }

        public void Clear()
        {
            Code = string.Empty;
            Desc = string.Empty;
        }

        public Structure(string sCode, string sDesc)
        {
            Code = sCode;
            Desc = sDesc;
        }

        public string GetStructureDesc(string sCode)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            Desc = string.Empty;

            strQuery = $"select * from struc_tbl where struc_code = '{sCode}'";
            var epsrec = db.Database.SqlQuery<STRUC_TBL>(strQuery);

            foreach (var items in epsrec)
            {
                Desc = items.STRUC_DESC;
            }

            return Desc;
        }
    }

    public class StructureList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Structure> m_List;

        public List<Structure> StructureLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public StructureList()
        {
            m_List = new List<Structure>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from struc_tbl order by struc_code ";
                var epsrec = db.Database.SqlQuery<STRUC_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Structure(items.STRUC_CODE, items.STRUC_DESC));
                }
            }
        }

        public string GetStructureCode(string sPermit)
        {
            string sPermitCode = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from struc_tbl where STRUC_DESC = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<STRUC_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitCode = items.STRUC_CODE;
                }
            }

            return sPermitCode;
        }
    }
}
