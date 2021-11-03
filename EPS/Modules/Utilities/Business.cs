using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EPSEntities;
using System.Data;
using System.Collections;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using System.Windows.Forms;
using Common.StringUtilities;
using EPSEntities.Connection;
using EPSEntities.Entity;

namespace Modules.Utilities
{
    public class Business
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string Code { get; set; }
        public string Desc { get; set; }
        public string FeeCode { get; set; }
        public string Means { get; set; }

        public Business()
        {
            this.Clear();
        }

        public void Clear()
        {
            Code = string.Empty;
            Desc = string.Empty;
            FeeCode = string.Empty;
            Means = string.Empty;
        }

        public Business(string sCode, string sDesc, string sFeeCode, string sMeans)
        {
            Code = sCode;
            Desc = sDesc;
            FeeCode = sFeeCode;
            Means = sMeans;
        }

        public string GetBusinessDesc(string sCode)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            Desc = string.Empty;

            strQuery = $"select * from bns_table where bns_code = '{sCode}' order by bns_code ";
            var epsrec = db.Database.SqlQuery<EPSEntities.BNS_TABLE>(strQuery); //test

            foreach (var items in epsrec)
            {
                Desc = items.BNS_DESC;
            }

            return Desc;
        }
    }

    public class BusinessList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Business> m_List;

        public List<Business> BusinessLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public BusinessList()
        {
            m_List = new List<Business>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from bns_table order by bns_code ";
                var epsrec = db.Database.SqlQuery<EPSEntities.BNS_TABLE>(strQuery); //test

                foreach (var items in epsrec)
                {
                    m_List.Add(new Business(items.BNS_CODE, items.BNS_DESC,items.FEES_CODE,items.MEANS));
                }
            }
        }
    }
}
