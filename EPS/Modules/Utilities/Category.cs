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
    public class Category
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string Code { get; set; }
        public string Desc { get; set; }

        public Category()
        {
            this.Clear();
        }

        public void Clear()
        {
            Code = string.Empty;
            Desc = string.Empty;
        }

        public Category(string sCode, string sDesc)
        {
            Code = sCode;
            Desc = sDesc;
        }

        public string GetCategoryDesc(string sCode)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            Desc = string.Empty;

            strQuery = $"select * from category_tbl where category_code = '{sCode}'";
            var epsrec = db.Database.SqlQuery<CATEGORY_TBL>(strQuery);

            foreach (var items in epsrec)
            {
                Desc = items.CATEGORY_DESC;
            }

            return Desc;
        }

        public void CreateCategoryCode()
        {
            Code = string.Empty;
            string strQuery = string.Empty;
            string strCode = string.Empty;
            int iCode = 0;

            var db = new EPSConnection(dbConn);

            strQuery = "select max(category_code) from category_tbl";
            strCode = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();

            int.TryParse(strCode, out iCode);
            iCode++;

            switch ((iCode).ToString().Length)
            {
                case 1:
                    {
                        strCode = "0" + (iCode).ToString();
                        break;
                    }
                case 2:
                    {
                        strCode = (iCode).ToString();
                        break;
                    }


            }

            Code = strCode;
        }
    }

    public class CategoryList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Category> m_List;
        
        public List<Category> CategoryLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public CategoryList()
        {
            m_List = new List<Category>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from category_tbl order by category_code ";
                var epsrec = db.Database.SqlQuery<CATEGORY_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Category(items.CATEGORY_CODE, items.CATEGORY_DESC));
                }
            }
        }

        public CategoryList(string sCategoryDesc)
        {
            m_List = new List<Category>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from category_tbl where category_desc = '{StringUtilities.HandleApostrophe(sCategoryDesc)}' order by category_code ";
                var epsrec = db.Database.SqlQuery<CATEGORY_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Category(items.CATEGORY_CODE, items.CATEGORY_DESC));
                }
            }
        }

        public string GetCategoryCode(string sDesc)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sCode = string.Empty;

            strQuery = $"select * from category_tbl where CATEGORY_DESC = '{sDesc}' ";
            var epsrec = db.Database.SqlQuery<CATEGORY_TBL>(strQuery);

            foreach (var items in epsrec)
            {
                sCode = items.CATEGORY_CODE;
            }

            return sCode;
        }
    }
}
