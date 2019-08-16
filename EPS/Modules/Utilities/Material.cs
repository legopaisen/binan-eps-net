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
    public class Material
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string Code { get; set; }
        public string Desc { get; set; }

        public Material()
        {
            this.Clear();
        }

        public void Clear()
        {
            Code = string.Empty;
            Desc = string.Empty;
        }

        public Material(string sCode, string sDesc)
        {
            Code = sCode;
            Desc = sDesc;
        }

        public string GetMaterialDesc(string sCode)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            Desc = string.Empty;

            strQuery = $"select * from material_tbl where material_code = '{sCode}' ";
            var epsrec = db.Database.SqlQuery<MATERIAL_TBL>(strQuery);

            foreach (var items in epsrec)
            {
                Desc = items.MATERIAL_DESC;
            }

            return Desc;
        }
    }

    public class MaterialList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Material> m_List;
        
        public List<Material> MaterialLst
        {
            get { return m_List; }
            set { m_List = value; }
        }

        public MaterialList()
        {
            m_List = new List<Material>();

            string strQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                strQuery = $"select * from material_tbl order by material_code ";
                var epsrec = db.Database.SqlQuery<MATERIAL_TBL>(strQuery);

                foreach (var items in epsrec)
                {
                    m_List.Add(new Material(items.MATERIAL_CODE, items.MATERIAL_DESC));
                }
            }
        }

        public string GetMaterialCode(string sDesc)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            
            strQuery = $"select * from material_tbl where material_desc = '{sDesc}' ";
            var epsrec = db.Database.SqlQuery<MATERIAL_TBL>(strQuery);

            foreach (var items in epsrec)
            {
                sDesc = items.MATERIAL_CODE;
            }

            return sDesc;
        }
    }
}
