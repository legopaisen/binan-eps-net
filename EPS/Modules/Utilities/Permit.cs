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
    public class Permit
    {
        public string PermitCode { get; set; }
        public string PermitDesc { get; set; }
        public string PermitAppCode { get; set; }

        public Permit()
        {
            this.Clear();
        }

        public void Clear()
        {
            PermitCode = string.Empty;
            PermitDesc = string.Empty;
            PermitAppCode = string.Empty;
        }

        public Permit(string sCode, string sDesc)
        {
            PermitCode = sCode;
            PermitDesc = sDesc;
            
        }
    }

    public class PermitList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Permit> m_List;

        public List<Permit> PermitLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public PermitList(string sWhereClause)
        {
            m_List = new List<Permit>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from permit_tbl ";
                if (!string.IsNullOrEmpty(sWhereClause))
                    strQuery += $" {sWhereClause} ";
                strQuery+= $"order by permit_code ";
                var epsrec = db.Database.SqlQuery<PERMIT_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Permit(items.PERMIT_CODE, items.PERMIT_DESC ));
                }
            }
        }

        public string GetPermitCode(string sPermit)
        {
            string sPermitCode = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from permit_tbl where permit_desc = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<PERMIT_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitCode = items.PERMIT_CODE;
                }
            }

            return sPermitCode;
        }

        public string GetPermitAppCode(string sPermit)
        {
            string sPermitCode = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from permit_tbl where permit_desc = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<PERMIT_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitCode = items.APP_CODE;
                }
            }

            return sPermitCode;
        }

        public string GetPermitDesc(string sPermit)
        {
            string sPermitDesc = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from permit_tbl where permit_code = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<PERMIT_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitDesc = items.PERMIT_DESC;
                }
            }

            return sPermitDesc;
        }
    }
}
