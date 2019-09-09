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
using Common.StringUtilities;

namespace Modules.Utilities
{
    public class Accounts
    {
        public static ConnectionString dbConn = new ConnectionString();
        private string m_sOwnerCode;
        private string m_sLastName;
        private string m_sFirstName;
        private string m_sMI;
        private string m_sAddress;
        private string m_sHouseNo;
        private string m_sLotNo;
        private string m_sBlkNo;
        private string m_sBarangay;
        private string m_sCity;
        private string m_sProvince;
        private string m_sZip;
        private string m_sTIN;
        private string m_sTCT;
        private string m_sCTC;
        private string m_sTelNo;

        public Accounts()
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
            m_sCity = string.Empty;
            m_sProvince = string.Empty;
            m_sZip = string.Empty;
            m_sTIN = string.Empty;
            m_sTCT = string.Empty;
            m_sCTC = string.Empty;
            m_sTelNo = string.Empty;
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

            sQuery = $"SELECT * FROM account WHERE acct_code = '{sOwnerCode}'";
            var trsrec = db.Database.SqlQuery<ACCOUNT>(sQuery);

            foreach (var items in trsrec)
            {
                m_sOwnerCode = sOwnerCode;
                m_sLastName = items.ACCT_LN;
                m_sFirstName = items.ACCT_FN;
                m_sMI = items.ACCT_MI;
                m_sHouseNo = items.ACCT_HSE_NO;
                m_sLotNo = items.ACCT_LOT_NO;
                m_sBlkNo = items.ACCT_BLK_NO;
                m_sAddress = items.ACCT_ADDR;
                m_sBarangay = items.ACCT_BRGY;
                m_sCity = items.ACCT_CITY;
                m_sProvince = items.ACCT_PROV;
                m_sZip = items.ACCT_ZIP;
                m_sTIN = items.ACCT_TIN;
                m_sTCT = items.ACCT_TCT;
                m_sCTC = items.ACCT_CTC;
                m_sTelNo = items.ACCT_TELNO;
            }
            
        }

        public void GetOwner(string sLastName, string sFirstName, string sMI)
        {
            this.Clear();
            string sQuery = string.Empty;

            var db = new EPSConnection(dbConn);

            sQuery = $"SELECT * FROM account WHERE acct_ln = '{sLastName}'";
            sQuery += $" and acct_fn = '{sFirstName}'";
            if(!string.IsNullOrEmpty(sMI))
                sQuery += $" and acct_mi = '{sMI}'";
            var epsrec = db.Database.SqlQuery<ACCOUNT>(sQuery);

            foreach (var items in epsrec)
            {
                m_sOwnerCode = items.ACCT_CODE;
                m_sLastName = items.ACCT_LN;
                m_sFirstName = items.ACCT_FN;
                m_sMI = items.ACCT_MI;
                m_sHouseNo = items.ACCT_HSE_NO;
                m_sLotNo = items.ACCT_LOT_NO;
                m_sBlkNo = items.ACCT_BLK_NO;
                m_sAddress = items.ACCT_ADDR;
                m_sBarangay = items.ACCT_BRGY;
                m_sCity = items.ACCT_CITY;
                m_sProvince = items.ACCT_PROV;
                m_sZip = items.ACCT_ZIP;
                m_sTIN = items.ACCT_TIN;
                m_sTCT = items.ACCT_TCT;
                m_sCTC = items.ACCT_CTC;
                m_sTelNo = items.ACCT_TELNO;
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

        public string ZIP
        {
            get { return m_sZip; }
            set { m_sZip = value; }
        }

        public string TIN
        {
            get { return m_sTIN; }
            set { m_sTIN = value; }
        }

        public string TCT
        {
            get { return m_sTCT; }
            set { m_sTCT = value; }
        }

        public string CTC
        {
            get { return m_sCTC; }
            set { m_sCTC = value; }
        }

        public string TelNo
        {
            get { return m_sTelNo; }
            set { m_sTelNo = value; }
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

        public Accounts(string sOwnCode, string sLastName, string sFirstName, string sMI,
            string sAddress, string sHouseNo, string sLotNo, string sBlkNo, string sBrgy,
            string sCity, string sProv, string sZip, string sTIN, string sTCT,
            string sCTC, string sTelNo)
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
            m_sProvince = sProv;
            m_sZip = sZip;
            m_sTIN = sTIN;
            m_sTCT = sTCT;
            m_sCTC = sCTC;
            m_sTelNo = sTelNo;
        }

        public void CreateAccount(string sLastName, string sFirstName, string sMI,
            string sAddress, string sHouseNo, string sLotNo, string sBlkNo, string sBrgy,
            string sCity, string sProv, string sZip, string sTIN, string sTCT,
            string sCTC, string sTelNo)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;

            GetOwner(sLastName, sFirstName, sMI);

            if (string.IsNullOrEmpty(m_sOwnerCode))
            {
                CreateAccountCode();

                if (!string.IsNullOrEmpty(m_sOwnerCode))
                {
                    strQuery = $"insert into account values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15,:16,:17)";
                    db.Database.ExecuteSqlCommand(strQuery,
                        new OracleParameter(":1", m_sOwnerCode),
                        new OracleParameter(":2", StringUtilities.HandleApostrophe(sLastName)),
                        new OracleParameter(":3", StringUtilities.HandleApostrophe(sFirstName)),
                        new OracleParameter(":4", StringUtilities.HandleApostrophe(sMI)),
                        new OracleParameter(":5", StringUtilities.HandleApostrophe(sHouseNo)),
                        new OracleParameter(":6", StringUtilities.HandleApostrophe(sLotNo)),
                        new OracleParameter(":7", StringUtilities.HandleApostrophe(sBlkNo)),
                        new OracleParameter(":8", StringUtilities.HandleApostrophe(sAddress)),
                        new OracleParameter(":9", StringUtilities.HandleApostrophe(sBrgy)),
                        new OracleParameter(":10", StringUtilities.HandleApostrophe(sCity)),
                        new OracleParameter(":11", StringUtilities.HandleApostrophe(sProv)),
                        new OracleParameter(":12", StringUtilities.HandleApostrophe(sZip)),
                        new OracleParameter(":13", StringUtilities.HandleApostrophe(sTCT)),
                        new OracleParameter(":14", StringUtilities.HandleApostrophe(sTIN)),
                        new OracleParameter(":15", StringUtilities.HandleApostrophe(sCTC)),
                        new OracleParameter(":16", StringUtilities.HandleApostrophe(sTelNo)),
                        new OracleParameter(":17", StringUtilities.HandleApostrophe(sLotNo)));
                }
            }
        }

        private void CreateAccountCode()
        {
            m_sOwnerCode = string.Empty;
            string strQuery = string.Empty;
            string strAcctCode = string.Empty;
            int iCode = 0;

            var db = new EPSConnection(dbConn);

            strQuery = "select max(acct_code) from account";
            strAcctCode = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();

            int.TryParse(strAcctCode, out iCode);
            iCode++;

            switch ((iCode).ToString().Length)
            {
                case 1:
                    {
                        strAcctCode = "00000" + (iCode).ToString();
                        break;
                    }
                case 2:
                    {
                        strAcctCode = "0000" + (iCode).ToString();
                        break;
                    }
                case 3:
                    {
                        strAcctCode = "000" + (iCode).ToString();
                        break;
                    }
                case 4:
                    {
                        strAcctCode = "00" + (iCode).ToString();
                        break;
                    }
                case 5:
                    {
                        strAcctCode = "0" + (iCode).ToString();
                        break;
                    }
                case 6:
                    {
                        strAcctCode = (iCode).ToString();
                        break;
                    }

            }

            m_sOwnerCode = strAcctCode;
        }
    }

    public class AccountsList
    {
        public static ConnectionString dbConn = new ConnectionString();
        private List<Accounts> m_lstAcct;
        private string m_sOwnerCode;
        private string m_sLastName;
        private string m_sFirstName;
        private string m_sMI;
        private string m_sAddress;
        private string m_sHouseNo;
        private string m_sLotNo;
        private string m_sBlkNo;
        private string m_sBarangay;
        private string m_sCity;
        private string m_sProvince;
        private string m_sZip;
        private string m_sTIN;
        private string m_sTCT;
        private string m_sCTC;
        private string m_sTelNo;

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
            m_sCity = string.Empty;
            m_sProvince = string.Empty;
            m_sZip = string.Empty;
            m_sTIN = string.Empty;
            m_sTCT = string.Empty;
            m_sCTC = string.Empty;
            m_sTelNo = string.Empty;
        }


        public AccountsList(string sAcctNo, string sLastName, string sFirstName, string sMI)
        {
            m_lstAcct = new List<Accounts>();
            string sQuery = string.Empty;

            using (var db = new EPSConnection(dbConn))
            {
                /*if (string.IsNullOrEmpty(sAcctNo))
                    sAcctNo = "%";
                if (string.IsNullOrEmpty(sLastName))
                    sLastName = "%";
                if (string.IsNullOrEmpty(sFirstName))
                    sFirstName = "%";
                if (string.IsNullOrEmpty(sMI))
                    sMI = "%";*/
                sAcctNo = sAcctNo.Trim();
                sLastName = sLastName.Trim();
                sFirstName = sFirstName.Trim();
                sMI = sMI.Trim();

                sQuery = $"select * from account where 1=1 ";
                if(!string.IsNullOrEmpty(sAcctNo))
                    sQuery += $" acct_code like '{sAcctNo}%'";
                if(!string.IsNullOrEmpty(sLastName))
                    sQuery += $" and acct_ln like '{sLastName}%'";
                if(!string.IsNullOrEmpty(sFirstName))
                    sQuery += $" and acct_fn like '{sFirstName}%'";
                if (!string.IsNullOrEmpty(sMI))
                    sQuery += $" and acct_mi like '{sMI}%' ";
                sQuery += " order by acct_ln";
                var epsrec = db.Database.SqlQuery<ACCOUNT>(sQuery);

                foreach (var items in epsrec)
                {
                    Clear();
                    m_sOwnerCode = items.ACCT_CODE;
                    m_sLastName = items.ACCT_LN;
                    m_sFirstName = items.ACCT_FN;
                    m_sMI = items.ACCT_MI;
                    m_sAddress = items.ACCT_ADDR;
                    m_sHouseNo = items.ACCT_HSE_NO;
                    m_sLotNo = items.ACCT_LOT_NO;
                    m_sBlkNo = items.ACCT_BLK_NO;
                    m_sBarangay = items.ACCT_BRGY;
                    m_sCity = items.ACCT_CITY;
                    m_sProvince = items.ACCT_PROV;
                    m_sZip = items.ACCT_ZIP;
                    m_sTIN = items.ACCT_TIN;
                    m_sTCT = items.ACCT_TCT;
                    m_sCTC = items.ACCT_CTC;
                    m_sTelNo = items.ACCT_TELNO;

                    m_lstAcct.Add(new Accounts(m_sOwnerCode, m_sLastName, m_sFirstName,
                        m_sMI, m_sAddress, m_sHouseNo, m_sLotNo,
                        m_sBlkNo, m_sBarangay, m_sCity, m_sProvince, m_sZip,
                        m_sTIN, m_sTCT, m_sCTC, m_sTelNo));
                }
            }
        }

        public List<Accounts> AcctLst
        {
            get { return m_lstAcct; }
            set { m_lstAcct = value; }
        }
    }
    
}
