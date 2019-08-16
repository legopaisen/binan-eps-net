using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.AppSettings;
using EPSEntities.Entity;
using Common.StringUtilities;
using Oracle.ManagedDataAccess.Client;
using System.Data.Entity;
using System.Data;
using EPSEntities.Connection;

namespace Modules.Utilities
{
    public class Engineers
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sOwnerCode;
        private string m_sLastName;
        private string m_sFirstName;
        private string m_sMI;
        private string m_sAddress;
        private string m_sEngrType;

        //additional information
        private string m_sHouseNo;
        private string m_sLotNo;
        private string m_sBlkNo;
        private string m_sBarangay;
        private string m_sDistrict;
        private string m_sCity;
        private string m_sProvince;
        private string m_sZip;
        private string m_sTIN;
        private string m_sPRC;
        private string m_sPTR;

        public Engineers()
        {
            this.Clear();
        }

        public void Clear()
        {
            m_sOwnerCode = string.Empty;
            m_sLastName = string.Empty;
            m_sFirstName = string.Empty;
            m_sMI = string.Empty;
            m_sAddress = string.Empty;
            m_sHouseNo = string.Empty;
            m_sLotNo = string.Empty;
            m_sBlkNo = string.Empty;
            m_sBarangay = string.Empty;
            m_sDistrict = string.Empty;
            m_sCity = string.Empty;
            m_sProvince = string.Empty;
            m_sZip = string.Empty;
        }

        /// <summary>
        /// This method sets owner information given its owner code.
        /// An invalid owner code returns an empty owner - owner code would also be empty. 
        /// So you may want to keep your broken owner code before passing it directly to this method.
        /// </summary>
        /// <param name="sOwnerCode">the owner code</param>
        public void GetOwner(string sOwnerCode)
        {
            this.Clear();
            string sQuery = string.Empty;

            var db = new EPSConnection(dbConn);

            sQuery = $"SELECT * FROM engineer_tbl WHERE engr_code = '{sOwnerCode}'";
            var trsrec = db.Database.SqlQuery<ENGINEER_TBL>(sQuery);

            foreach (var items in trsrec)
            {
                m_sOwnerCode = sOwnerCode;
                m_sLastName = items.ENGR_LN;
                m_sFirstName = items.ENGR_FN;
                m_sMI = items.ENGR_MI;
                m_sHouseNo = items.ENGR_HSE_NO;
                m_sLotNo = items.ENGR_LOT_NO;
                m_sBlkNo = items.ENGR_BLK_NO;
                m_sAddress = items.ENGR_ADDR;
                m_sBarangay = items.ENGR_BRGY;
                m_sCity = items.ENGR_CITY;
                m_sProvince = items.ENGR_PROV;
                m_sZip = items.ENGR_ZIP;
                m_sEngrType = items.ENGR_TYPE;
                m_sTIN = items.ENGR_TIN;
                m_sPRC = items.ENGR_PRC;
                m_sPTR = items.ENGR_PTR;
            }

        }

        public void GetOwner(string sLastName, string sFirstName, string sEngrType)
        {
            this.Clear();
            string sQuery = string.Empty;

            var db = new EPSConnection(dbConn);

            sQuery = $"SELECT * FROM engineer_tbl WHERE engr_ln = '{sLastName}'";
            sQuery += $" and engr_fn = '{sFirstName}'";
            if (!string.IsNullOrEmpty(sEngrType))
                sQuery += $" and engr_type = '{sEngrType}'";
            var epsrec = db.Database.SqlQuery<ENGINEER_TBL>(sQuery);

            foreach (var items in epsrec)
            {
                m_sOwnerCode = items.ENGR_CODE;
                m_sLastName = items.ENGR_LN;
                m_sFirstName = items.ENGR_FN;
                m_sMI = items.ENGR_MI;
                m_sHouseNo = items.ENGR_HSE_NO;
                m_sLotNo = items.ENGR_LOT_NO;
                m_sBlkNo = items.ENGR_BLK_NO;
                m_sAddress = items.ENGR_ADDR;
                m_sBarangay = items.ENGR_BRGY;
                m_sCity = items.ENGR_CITY;
                m_sProvince = items.ENGR_PROV;
                m_sZip = items.ENGR_ZIP;
                m_sEngrType = items.ENGR_TYPE;
                m_sTIN = items.ENGR_TIN;
                m_sPRC = items.ENGR_PRC;
                m_sPTR = items.ENGR_PTR;
            }

        }

        public string OwnerCode
        {
            get { return m_sOwnerCode; }
            set { m_sOwnerCode = value; }
        }

        public string LastName
        {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }

        public string FirstName
        {
            get { return m_sFirstName; }
            set { m_sFirstName = value; }
        }

        public string MiddleInitial
        {
            get { return m_sMI; }
            set { m_sMI = value; }
        }

        /// <summary>
        /// This property returns address of owner.
        /// </summary>
        public string Address
        {
            get { return m_sAddress; }
            set { m_sAddress = value; }
        }

        public string HouseNo
        {
            get { return m_sHouseNo; }
            set { m_sHouseNo = value; }
        }

        public string LotNo
        {
            get { return m_sLotNo; }
            set { m_sLotNo = value; }
        }

        public string BlkNo
        {
            get { return m_sBlkNo; }
            set { m_sBlkNo = value; }
        }

        public string TIN
        {
            get { return m_sTIN; }
            set { m_sTIN = value; }
        }

        public string PTR
        {
            get { return m_sPTR; }
            set { m_sPTR = value; }
        }

        public string PRC
        {
            get { return m_sPRC; }
            set { m_sPRC = value; }
        }

        public string EngrType
        {
            get { return m_sEngrType; }
            set { m_sEngrType = value; }
        }

        public string ZIP
        {
            get { return m_sZip; }
            set { m_sZip = value; }
        }

        /// <summary>
        /// This property returns address, barangay, municipality, and province of owner.
        /// </summary>
        public string CompleteAddress
        {
            get { return string.Format("{0} {1} {2} {3} {4} {5} {6} {7}", m_sHouseNo, m_sLotNo, m_sBlkNo, m_sAddress, m_sBarangay, m_sCity, m_sProvince, m_sZip); }
        }

        /// <summary>
        /// This property returns owner's name.
        /// Current format is Last Name, First Name, Middle Initial. 
        /// To specify a different format use ToPersonName function.
        /// </summary>
        public string OwnerName
        {
            get
            {
                return PersonName.ToPersonName(m_sLastName, m_sFirstName,
                    m_sMI, "L", "L, F", "L, F M.");

            }
        }

        public string Barangay
        {
            get { return m_sBarangay; }
        }

        public string District
        {
            get { return m_sDistrict; }
        }

        public string City
        {
            get { return m_sCity; }
        }

        public string Province
        {
            get { return m_sProvince; }
        }

        public string ToPersonName(string strPattern)
        {
            return PersonName.ToPersonName(strPattern, m_sLastName, m_sFirstName, m_sMI);
        }

        public Engineers(string sOwnCode, string sLastName, string sFirstName, string sMI,
            string sAddress, string sHouseNo, string sLotNo, string sBlkNo, string sBrgy,
            string sCity, string m_sProv, string sZip, string sEngrType, string sTIN,
            string sPRC, string sPTR)
        {
            m_sOwnerCode = sOwnCode;
            m_sLastName = sLastName;
            m_sFirstName = sFirstName;
            m_sMI = sMI;
            m_sAddress = sAddress;
            m_sHouseNo = sHouseNo;
            m_sLotNo = sLotNo;
            m_sBlkNo = sBlkNo;
            m_sBarangay = sBrgy;
            m_sCity = sCity;
            m_sProvince = m_sProv;
            m_sZip = sZip;
            m_sEngrType = sEngrType;
            m_sTIN = sTIN;
            m_sPRC = sPRC;
            m_sPTR = sPTR;
        }
    }

    public class EngineersList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Engineers> m_lstAcct;
        private string m_sOwnerCode;
        private string m_sLastName;
        private string m_sFirstName;
        private string m_sMI;
        private string m_sAddress;
        private string m_sEngrType;

        //additional information
        private string m_sHouseNo;
        private string m_sLotNo;
        private string m_sBlkNo;
        private string m_sBarangay;
        private string m_sDistrict;
        private string m_sCity;
        private string m_sProvince;
        private string m_sZip;
        private string m_sTIN;
        private string m_sPRC;
        private string m_sPTR;

        public void Clear()
        {
            m_sOwnerCode = string.Empty;
            m_sLastName = string.Empty;
            m_sFirstName = string.Empty;
            m_sMI = string.Empty;
            m_sAddress = string.Empty;
            m_sHouseNo = string.Empty;
            m_sLotNo = string.Empty;
            m_sBlkNo = string.Empty;
            m_sBarangay = string.Empty;
            m_sDistrict = string.Empty;
            m_sCity = string.Empty;
            m_sProvince = string.Empty;
            m_sZip = string.Empty;
            m_sEngrType = string.Empty;
            m_sTIN = string.Empty;
            m_sPRC = string.Empty;
            m_sPTR = string.Empty;
        }
    
        public EngineersList(string sAcctNo, string sLastName, string sFirstName, string sMI, string sEngrType)
        {
            m_lstAcct = new List<Engineers>();
            string sQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                if (string.IsNullOrEmpty(sAcctNo))
                    sAcctNo = "%";
                if (string.IsNullOrEmpty(sLastName))
                    sLastName = "%";
                if (string.IsNullOrEmpty(sFirstName))
                    sFirstName = "%";
                if (string.IsNullOrEmpty(sMI))
                    sMI = "%";
                if (string.IsNullOrEmpty(sEngrType))
                    sEngrType = "%";

                sQuery = $"select * from engineer_tbl where ";
                sQuery += $" engr_code like '{sAcctNo}%'";
                sQuery += $" and engr_ln like '{sLastName}%'";
                sQuery += $" and engr_fn like '{sFirstName}%'";
                sQuery += $" and engr_mi like '{sMI}%' ";
                sQuery += $" and engr_type like '{sEngrType}%' order by engr_ln";
                var epsrec = db.Database.SqlQuery<ENGINEER_TBL>(sQuery);

                foreach (var items in epsrec)
                {
                    Clear();
                    m_sOwnerCode = items.ENGR_CODE;
                    m_sLastName = items.ENGR_LN;
                    m_sFirstName = items.ENGR_FN;
                    m_sMI = items.ENGR_MI;
                    m_sAddress = items.ENGR_ADDR;
                    m_sHouseNo = items.ENGR_HSE_NO;
                    m_sLotNo = items.ENGR_LOT_NO;
                    m_sBlkNo = items.ENGR_BLK_NO;
                    m_sBarangay = items.ENGR_BRGY;
                    m_sCity = items.ENGR_CITY;
                    m_sProvince = items.ENGR_PROV;
                    m_sZip = items.ENGR_ZIP;
                    m_sEngrType = items.ENGR_TYPE;
                    m_sTIN = items.ENGR_TIN;
                    m_sPRC = items.ENGR_PRC;
                    m_sPTR = items.ENGR_PTR;

                    m_lstAcct.Add(new Engineers(m_sOwnerCode, m_sLastName, m_sFirstName,
                        m_sMI, m_sAddress, m_sHouseNo, m_sLotNo,
                        m_sBlkNo, m_sBarangay, m_sCity, m_sProvince, m_sZip,
                        m_sEngrType, m_sTIN, m_sPRC, m_sPTR));
                }
            }
        }

        public List<Engineers> AcctLst
        {
            get { return m_lstAcct; }
            set { m_lstAcct = value; }
        }
    }
}
