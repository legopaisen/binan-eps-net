using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.AppSettings;
using EPSEntities.Entity;
using EPSEntities.Connection;

namespace Modules.Records
{
    public class ApplicationTbl
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string ARN { get; set; }
        private string PROJ_DESC { get; set; }
        private string PERMIT_CODE { get; set; }
        private string PERMIT_NO { get; set; }
        private string STRUC_CODE { get; set; }
        private int BLDG_NO { get; set; }
        private string STATUS_CODE { get; set; }
        private string SCOPE_CODE { get; set; }
        private string CATEGORY_CODE { get; set; }
        private string OCCUPANCY_CODE { get; set; }
        private string BNS_CODE { get; set; }
        private string PROJ_HSE_NO { get; set; }
        private string PROJ_LOT_NO { get; set; }
        private string PROJ_BLK_NO { get; set; }
        private string PROJ_ADDR { get; set; }
        private string PROJ_BRGY { get; set; }
        private string PROJ_CITY { get; set; }
        private string PROJ_PROV { get; set; }
        private string PROJ_ZIP { get; set; }
        private string PROJ_OWNER { get; set; }
        private string PROJ_LOT_OWNER { get; set; }
        private string OWN_TYPE { get; set; }
        private string ENGR_CODE { get; set; }
        private string ARCHITECT { get; set; }
        private DateTime? PROP_START { get; set; }
        private DateTime? PROP_COMPLETE { get; set; }
        private DateTime? DATE_APPLIED { get; set; }
        private DateTime? DATE_ISSUED { get; set; }
        private string MEMO { get; set; }
        private int MAIN_APPLICATION { get; set; }

        public ApplicationTbl()
        {
            this.Clear();
        }

        public void Clear()
        {
            ARN = string.Empty;
            PROJ_DESC = string.Empty;
            PERMIT_CODE = string.Empty;
            PERMIT_NO = string.Empty;
            STRUC_CODE = string.Empty;
            BLDG_NO = 0;
            STATUS_CODE = string.Empty;
            SCOPE_CODE = string.Empty;
            CATEGORY_CODE = string.Empty;
            OCCUPANCY_CODE = string.Empty;
            BNS_CODE = string.Empty;
            PROJ_HSE_NO = string.Empty;
            PROJ_LOT_NO = string.Empty;
            PROJ_BLK_NO = string.Empty;
            PROJ_ADDR = string.Empty;
            PROJ_BRGY = string.Empty;
            PROJ_CITY = string.Empty;
            PROJ_PROV = string.Empty;
            PROJ_ZIP = string.Empty;
            PROJ_OWNER = string.Empty;
            PROJ_LOT_OWNER = string.Empty;
            OWN_TYPE = string.Empty;
            ENGR_CODE = string.Empty;
            ARCHITECT = string.Empty;
            PROP_START = AppSettingsManager.GetCurrentDate();
            PROP_COMPLETE = AppSettingsManager.GetCurrentDate();
            DATE_APPLIED = AppSettingsManager.GetCurrentDate();
            DATE_ISSUED = AppSettingsManager.GetCurrentDate();
            MEMO = string.Empty;
            MAIN_APPLICATION = 0;
        }
    

        public void GetRecord(string sARN)
        {
            this.Clear();
            string sQuery = string.Empty;

            var db = new EPSConnection(dbConn);
            
            sQuery = $"SELECT * FROM application WHERE arn = '{sARN}'";
            var trsrec = db.Database.SqlQuery<APPLICATION>(sQuery);

            foreach (var items in trsrec)
            {
                ARN = items.ARN;
                PROJ_DESC = items.PROJ_DESC;
                PERMIT_CODE = items.PERMIT_CODE;
                PERMIT_NO = items.PERMIT_NO;
                STRUC_CODE = items.STRUC_CODE;
                BLDG_NO = items.BLDG_NO;
                STATUS_CODE = items.STATUS_CODE;
                SCOPE_CODE = items.SCOPE_CODE;
                CATEGORY_CODE = items.CATEGORY_CODE;
                OCCUPANCY_CODE = items.OCCUPANCY_CODE;
                BNS_CODE = items.BNS_CODE;
                PROJ_HSE_NO = items.PROJ_HSE_NO;
                PROJ_LOT_NO = items.PROJ_LOT_NO;
                PROJ_BLK_NO = items.PROJ_BLK_NO;
                PROJ_ADDR = items.PROJ_ADDR;
                PROJ_BRGY = items.PROJ_BRGY;
                PROJ_CITY = items.PROJ_CITY;
                PROJ_PROV = items.PROJ_PROV;
                PROJ_ZIP = items.PROJ_ZIP;
                PROJ_OWNER = items.PROJ_OWNER;
                PROJ_LOT_OWNER = items.PROJ_LOT_OWNER;
                OWN_TYPE = items.OWN_TYPE;
                ENGR_CODE = items.ENGR_CODE;
                ARCHITECT = items.ARCHITECT;
                PROP_START = items.PROP_START;
                PROP_COMPLETE = items.PROP_COMPLETE;
                DATE_APPLIED = items.DATE_APPLIED;
                DATE_ISSUED = items.DATE_ISSUED;
                MEMO = items.MEMO;
                MAIN_APPLICATION = items.MAIN_APPLICATION;
            }
            
        }
        
        public ApplicationTbl(string sARN,string sPROJ_DESC,string sPERMIT_CODE,string sPERMIT_NO,
        string sSTRUC_CODE,int iBLDG_NO,string sSTATUS_CODE,string sSCOPE_CODE,string sCATEGORY_CODE,
        string sOCCUPANCY_CODE,string sBNS_CODE,string sPROJ_HSE_NO,string sPROJ_LOT_NO,
        string sPROJ_BLK_NO,string sPROJ_ADDR, string sPROJ_BRGY,string sPROJ_CITY,string sPROJ_PROV,
        string sPROJ_ZIP,string sPROJ_OWNER,string sPROJ_LOT_OWNER,string sOWN_TYPE,string sENGR_CODE,
        string sARCHITECT, DateTime dPROP_START,DateTime dPROP_COMPLETE, DateTime dDATE_APPLIED,
        Nullable<DateTime> dDATE_ISSUED,string sMEMO,int iMAIN_APPLICATION )
        {
            ARN = sARN;
            PROJ_DESC = sPROJ_DESC;
            PERMIT_CODE = sPERMIT_CODE;
            PERMIT_NO = sPERMIT_NO;
            STRUC_CODE = sSTRUC_CODE;
            BLDG_NO = iBLDG_NO;
            STATUS_CODE = sSTATUS_CODE;
            SCOPE_CODE = sSCOPE_CODE;
            CATEGORY_CODE =sCATEGORY_CODE;
            OCCUPANCY_CODE = sOCCUPANCY_CODE;
            BNS_CODE = sBNS_CODE;
            PROJ_HSE_NO = sPROJ_HSE_NO;
            PROJ_LOT_NO = sPROJ_LOT_NO;
            PROJ_BLK_NO = sPROJ_BLK_NO;
            PROJ_ADDR = sPROJ_ADDR;
            PROJ_BRGY = sPROJ_BRGY;
            PROJ_CITY = sPROJ_CITY;
            PROJ_PROV = sPROJ_PROV;
            PROJ_ZIP = sPROJ_ZIP;
            PROJ_OWNER = sPROJ_OWNER;
            PROJ_LOT_OWNER = sPROJ_LOT_OWNER;
            OWN_TYPE = sOWN_TYPE;
            ENGR_CODE = sENGR_CODE;
            ARCHITECT = sARCHITECT;
            PROP_START = dPROP_START;
            PROP_COMPLETE = dPROP_COMPLETE;
            DATE_APPLIED = dDATE_APPLIED;
            DATE_ISSUED = dDATE_ISSUED;
            MEMO = sMEMO;
            MAIN_APPLICATION = iMAIN_APPLICATION;
        }
    }

    public class ApplicationTblList
    {
        private List<ApplicationTbl> m_lstApp;
        public static ConnectionString dbConn = new ConnectionString();

        public ApplicationTblList()
        {
            m_lstApp = new List<ApplicationTbl>();
            string sQuery = string.Empty;

            //using (EPSEntities.EPSEntities db = new EPSEntities.EPSEntities())
            using (var db = new EPSConnection(dbConn))
            {
                sQuery = $"select * from application";
                var epsrec = db.Database.SqlQuery<APPLICATION>(sQuery);

                foreach (var items in epsrec)
                {
                    m_lstApp.Add(new ApplicationTbl(items.ARN, items.PROJ_DESC, items.PERMIT_CODE,
                        items.PERMIT_NO, items.STRUC_CODE, items.BLDG_NO, items.STATUS_CODE,
                        items.SCOPE_CODE, items.CATEGORY_CODE, items.OCCUPANCY_CODE, items.BNS_CODE, items.PROJ_HSE_NO,
                        items.PROJ_LOT_NO, items.PROJ_BLK_NO, items.PROJ_ADDR, items.PROJ_BRGY,
                        items.PROJ_CITY, items.PROJ_PROV, items.PROJ_ZIP, items.PROJ_OWNER,
                        items.PROJ_LOT_OWNER, items.OWN_TYPE, items.ENGR_CODE, items.ARCHITECT,
                        items.PROP_START, items.PROP_COMPLETE, items.DATE_APPLIED, null,
                        items.MEMO, items.MAIN_APPLICATION));
                }
            }
        }

        // this is another way of getting the list
        public static List<APPLICATION> GetRecord(string sWhereCond)
        {
            using (var db = new EPSConnection(dbConn))
            {
                string sQuery = string.Empty;
                sQuery = $"select * from application";
                if (!string.IsNullOrEmpty(sWhereCond))
                    sQuery += $" {sWhereCond}";
                return db.Database.SqlQuery<APPLICATION>(sQuery).ToList();
            }
        }

        public List<ApplicationTbl> AppLst
        {
            get { return m_lstApp; }
            set { m_lstApp = value; }
        }
    }
    
}
