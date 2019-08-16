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
    public class Scope
    {
        public string ScopeCode { get; set; }
        public string ScopeDesc { get; set; }

        public Scope()
        {
            this.Clear();
        }

        public void Clear()
        {
            ScopeCode = string.Empty;
            ScopeDesc = string.Empty;
        }

        public Scope(string sCode, string sDesc)
        {
            ScopeCode = sCode;
            ScopeDesc = sDesc;
        }
    }

    public class ScopeList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Scope> m_List;
        
        public List<Scope> ScopeLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public ScopeList()
        {
            m_List = new List<Scope>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from scope_tbl order by scope_code ";
                var epsrec = db.Database.SqlQuery<SCOPE_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Scope(items.SCOPE_CODE, items.SCOPE_DESC));
                }
            }
        }

        public string GetScopeCode(string sPermit)
        {
            string sPermitCode = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from scope_tbl where scope_desc = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<SCOPE_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitCode = items.SCOPE_CODE;
                }
            }

            return sPermitCode;
        }

        public string GetScopeDesc(string sPermit)
        {
            string sPermitDesc = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from scope_tbl where scope_code = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<SCOPE_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitDesc = items.SCOPE_DESC;
                }
            }

            return sPermitDesc;
        }
    }
}
