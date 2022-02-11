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
    public class Ownership
    {


        public string sOwnershipCode { get; set; }
        public string sOwnershipDesc { get; set; }

        public Ownership()
        {
            this.Clear();
        }

        public void Clear()
        {
            sOwnershipCode = string.Empty;
            sOwnershipDesc = string.Empty;
        }

        public Ownership(string sCode, string sDesc)
        {
            sOwnershipCode = sCode;
            sOwnershipDesc = sDesc;
        }
    }

    public class OwnershipList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Ownership> m_List;

        public List<Ownership> OwnershipLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public OwnershipList()
        {
            m_List = new List<Ownership>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from ownership_tbl order by ownership_code ";
                var epsrec = db.Database.SqlQuery<OWNERSHIP_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Ownership(items.OWNERSHIP_CODE, items.OWNERSHIP_DESC));
                }
            }
        }

        public string GetOwnershipCode(string sPermit)
        {
            string sPermitCode = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from ownership_tbl where ownership_desc = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<OWNERSHIP_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitCode = items.OWNERSHIP_CODE;
                }
            }

            return sPermitCode;
        }

        public string GetOwnershipDesc(string sPermit)
        {
            string sPermitDesc = string.Empty;
            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from ownership_tbl where ownership_code = '{sPermit}'";
                var epsrec = db.Database.SqlQuery<OWNERSHIP_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    sPermitDesc = items.OWNERSHIP_DESC;
                }
            }

            return sPermitDesc;
        }
    }
}
