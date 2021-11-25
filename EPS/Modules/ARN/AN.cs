using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using EPSEntities.Connection;
using Modules.Utilities;
using Common.AppSettings;
using Common.DataConnector;

namespace Modules.ARN
{
    public partial class AN : UserControl
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string m_sAN;
        private Permit m_Permit;

        public AN()
        {
            InitializeComponent();
            txtCode.Text = "";
            txtYear.Text = "";
            //txtMonth.Text = ""; // disabled for new arn of binan
            txtSeries.Text = "";
        }

        public string GetCode
        {
            get { return txtCode.Text; }
            set { txtCode.Text = value; }
        }

        public string GetTaxYear
        {
            get { return txtYear.Text; }
            set { txtYear.Text = value; }
        }

        public TextBox ArnCode
        {
            get { return txtCode; }
            set { txtCode = value; }
        }

        //public string GetMonth // disabled for new arn of binan
        //{
        //    get { return txtMonth.Text; }
        //    set { txtMonth.Text = value; }
        //}

        public string GetSeries
        {
            get { return txtSeries.Text; }
            set { txtSeries.Text = value; }
        }

        private void AN_Load(object sender, EventArgs e)
        {
            //txtCode.Text = "BP"; // AFM 20200629 hardcoded exclusive for Binan as per PM /
            txtCode.Text = "AN"; //AFM 20201027 for requested new arn for binan
            txtCode.MaxLength = 2; // AFM 20201027 for requested new arn for binan //AFM 20211104 reverted back to 2 as requested again by PM
            txtCode.Enabled = false; //AFM 20200811
            //txtYear.Text = AppSettingsManager.GetSystemDate().Year.ToString(); //AFM 20200629 fixed based on current year
            //txtMonth.Focus(); // disabled for new arn of binan
            txtSeries.Focus();
        }

        public string GetAn()
        {
            if (!string.IsNullOrEmpty(txtCode.Text.ToString()))
            {
                if (string.IsNullOrEmpty(txtYear.Text.ToString()))
                    txtYear.Text = string.Format("{0:yyyy}", AppSettingsManager.GetCurrentDate());
                //if (string.IsNullOrEmpty(txtMonth.Text.ToString()))
                //    txtMonth.Text = string.Format("{0:MM}", AppSettingsManager.GetCurrentDate()); // disabled for new arn of binan
            }

           // m_sAN = txtCode.Text + "-" + txtYear.Text + "-" + txtMonth.Text + "-" + txtSeries.Text;
            m_sAN = txtCode.Text + "-" + txtYear.Text + "-" + txtSeries.Text; //new arn of binan

            if (m_sAN.Length < 12)
                m_sAN = "";

            return m_sAN;
        }

        public string ANCodeGenerator(string sPermit)
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select permit_desc_code from permit_tbl where permit_code = '"+ sPermit +"'";
            if (result.Execute())
                if (result.Read())
                    sPermit = result.GetString("permit_desc_code");
            result.Close();
            return sPermit;
        }

        private void ResetSeriesSeq()
        {
            OracleResultSet res = new OracleResultSet();
            res.Query = "DROP SEQUENCE APPLICATION_SEQ";
            if(res.ExecuteNonQuery() == 0)
            { }
            res.Close();

            res.Query = @"CREATE SEQUENCE APPLICATION_SEQ
                        START WITH 1
                        MAXVALUE 9999
                        MINVALUE 1
                        NOCYCLE
                        NOCACHE
                        NOORDER";
            if (res.ExecuteNonQuery() == 0)
            { }
            res.Close();
        }

        public void CreateAN(string sApplication)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sTmpArn = string.Empty;
            string sAN = string.Empty;

            PermitList permit = new PermitList(null);

            //txtCode.Text = permit.GetPermitAppCode(sApplication); //AFM 202009

            //if (string.IsNullOrEmpty(txtCode.Text.ToString()))
            //    txtCode.Text = "BP";    // default for binan

            if (sApplication == "AN") //AFM 20201204 REQUESTED BY BINAN AS PER MITCH - FIXED "AN" FOR NEW APPLICATION
                txtCode.Text = "AN";
            else
                txtCode.Text = ANCodeGenerator(sApplication); // AFM 20201027 requested new arn format for binan

            string sYear = string.Format("{0:yyyy}", AppSettingsManager.GetCurrentDate());

            int iSeries = 0;
            if (sApplication != "AN")
            {
                strQuery = $"select arn from current_arn where arn like '{txtCode.Text}-{sYear}-%'"; 
                sTmpArn = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
                if (string.IsNullOrEmpty(sTmpArn))
                {
                    txtSeries.Text = "0001";
                }
                else
                {
                    int.TryParse(sTmpArn.Substring(8, 4), out iSeries);  //BP-2019-01-0001
                    txtSeries.Text = (iSeries + 1).ToString();
                    FormatSeries(txtSeries.TextLength);
                }
            }
           
            // validate if with duplicate
            string sUsedArn = string.Empty;
            int iUsedArn = 0;

            //AFM 20201221 for new application "AN"
            string sCurrSeries = string.Empty;
            string sTaxYear = string.Empty;
            int iCurrSeries = 0;
            if (sApplication == "AN")
            {
                strQuery = $"select max(arn) from (select arn from application where arn like '{txtCode.Text}-{sYear}-%' union ";
                strQuery += $"select arn from application_que where arn like '{txtCode.Text}-{sYear}-%')";
                sUsedArn = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
                try
                {
                    sTaxYear = sUsedArn.Substring(3, 4);
                }
                catch
                {
                    sTaxYear = AppSettingsManager.GetSystemDate().Year.ToString();
                    ResetSeriesSeq();
                }
                if (sTaxYear != AppSettingsManager.GetSystemDate().Year.ToString()) // reset yearly
                    ResetSeriesSeq();

                strQuery = "select application_seq.nextval as val from dual";
                iCurrSeries = db.Database.SqlQuery<int>(strQuery).SingleOrDefault();
            }
           

            if (sApplication != "AN")
            {
                strQuery = $"select max(arn) from (select arn from application where arn like '{txtCode.Text}-{sYear}-%' union ";
                strQuery += $"select arn from application_que where arn like '{txtCode.Text}-{sYear}-%')";
                sUsedArn = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
            
           
                if (!string.IsNullOrEmpty(sUsedArn))
                {
                    int.TryParse(sUsedArn.Substring(8, 4), out iUsedArn); //(11,4)
                }
            

                if (iUsedArn >= (iSeries + 1))
                {
                    iUsedArn = iUsedArn + 1;
                    txtSeries.Text = iUsedArn.ToString();
                    FormatSeries(txtSeries.TextLength);
                }
            }
            else
            {
                txtSeries.Text = iCurrSeries.ToString();
                FormatSeries(txtSeries.TextLength);
            }
            strQuery = $"delete from current_arn  where arn like '{txtCode.Text}-{sYear}-%'";
            db.Database.ExecuteSqlCommand(strQuery);

            strQuery = $"insert into current_arn values (:1)";
            db.Database.ExecuteSqlCommand(strQuery,
                new OracleParameter(":1", GetAn()));
        }

        private void FormatSeries(int iCount)
        {
            switch (iCount)
            {
                case 1:
                    {
                        txtSeries.Text = "000" + txtSeries.Text;
                        break;
                    }
                case 2:
                    {
                        txtSeries.Text = "00" + txtSeries.Text;
                        break;
                    }
                case 3:
                    {
                        txtSeries.Text = "0" + txtSeries.Text;
                        break;
                    }
                case 4:
                    {
                        txtSeries.Text = txtSeries.Text;
                        break;
                    }

            }
        }

        public void SetAn(string sArn)
        {
            try
            {
                if(sArn.Length == 12 || sArn.Length == 2)
                {
                    txtCode.Text = sArn.Substring(0, 2);
                    txtYear.Text = sArn.Substring(3, 4);
                    //txtMonth.Text = sArn.Substring(8, 2); // disabled for new arn of binan
                    //txtSeries.Text = sArn.Substring(11, 4);
                    txtSeries.Text = sArn.Substring(8, 4);
                }
                else if (sArn.Length == 13 || sArn.Length == 3) // requested new arn of binan
                {
                    txtCode.Text = sArn.Substring(0, 3);
                    txtYear.Text = sArn.Substring(4, 4);
                    //txtMonth.Text = sArn.Substring(8, 2); // disabled for new arn of binan
                    //txtSeries.Text = sArn.Substring(11, 4);
                    txtSeries.Text = sArn.Substring(9, 4);
                }
                
            }
            catch { }
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtYear.Text = "";
            //txtMonth.Text = ""; // disabled for new arn of binan
            txtSeries.Text = "";
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text.ToString().Length == 3) // changed from 2 to 3 - binan ver
                txtYear.Focus();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (txtYear.Text.ToString().Length == 4)
                //txtMonth.Focus(); // disabled for new arn of binan
                txtSeries.Focus();
        }

        private void txtMonth_TextChanged(object sender, EventArgs e)
        {
            //if (txtMonth.Text.ToString().Length == 2)// disabled for new arn of binan
            //    txtSeries.Focus();
        }

        private void txtYear_Leave(object sender, EventArgs e)
        {
            int iCount = txtYear.Text.ToString().Length;

            switch (iCount)
            {
                case 1:
                    {
                        txtYear.Text = "000" + txtYear.Text;
                        break;
                    }
                case 2:
                    {
                        txtYear.Text = "00" + txtYear.Text;
                        break;
                    }
                case 3:
                    {
                        txtYear.Text = "0" + txtYear.Text;
                        break;
                    }
                case 4:
                    {
                        txtYear.Text = txtYear.Text;
                        break;
                    }

            }
        }

        private void txtMonth_Leave(object sender, EventArgs e)
        {
            // disabled for new arn of binan

            //int iCount = txtMonth.Text.ToString().Length;

            //switch (iCount)
            //{
            //    case 1:
            //        {
            //            txtMonth.Text = "0" + txtMonth.Text;
            //            break;
            //        }
            //    case 2:
            //        {
            //            txtMonth.Text = txtMonth.Text;
            //            break;
            //        }

            //}
        }

        private void txtSeries_Leave(object sender, EventArgs e)
        {
            FormatSeries(txtSeries.Text.ToString().Length);
        }
    }
}
