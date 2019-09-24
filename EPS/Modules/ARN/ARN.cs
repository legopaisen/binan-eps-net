using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.AppSettings;
using System.Data.Entity;
using EPSEntities.Entity;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using EPSEntities.Connection;

namespace Modules.ARN
{
    public partial class ARN : UserControl
    {
        public static ConnectionString dbConn = new ConnectionString();
        public string m_sLguCode, m_sDistCode, m_sTaxYear, m_sSeries, m_sARN;

        public ARN()
        {
            InitializeComponent();
            txtLGUCode.Enabled = true;
        }

        public string GetLGUCode
        {
            get { return txtLGUCode.Text; }
            set { txtLGUCode.Text = value; }
        }

        private void txtSeries_Leave(object sender, EventArgs e)
        {
            int iCount = 0;
            iCount = txtSeries.TextLength;

            FormatSeries(iCount);
        }

        private void txtTaxYear_TextChanged(object sender, EventArgs e)
        {
            if (txtTaxYear.Text.Trim().Length == 4)
                txtSeries.Focus();
        }

        private void txtDistCode_Leave(object sender, EventArgs e)
        {
            int intCount = 0;
            string strQuery = string.Empty;

            var db = new EPSConnection(dbConn);

            strQuery = $"select count(distinct dist_code) from brgy where dist_code = TRIM('{txtDistCode.Text}')";
            intCount = db.Database.SqlQuery<Int32>(strQuery).SingleOrDefault();

            if (intCount == 0)
            {
                MessageBox.Show("District code not found", "EPS", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDistCode.Focus();
                return;
            }
                 
        }

        private void txtTaxYear_Leave(object sender, EventArgs e)
        {
            int iCount = 0;
            iCount = txtTaxYear.TextLength;

            switch (iCount)
            {
                case 1:
                    {
                        txtTaxYear.Text = "000" + txtTaxYear.Text;
                        break;
                    }
                case 2:
                    {
                        txtTaxYear.Text = "00" + txtTaxYear.Text;
                        break;
                    }
                case 3:
                    {
                        txtTaxYear.Text = "0" + txtTaxYear.Text;
                        break;
                    }
                case 4:
                    {
                        txtTaxYear.Text = txtTaxYear.Text;
                        break;
                    }

            }
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

        public string GetDistCode
        {
            get { return txtDistCode.Text; }
            set { txtDistCode.Text = value; }
        }
        public string GetTaxYear
        {
            get { return txtTaxYear.Text; }
            set { txtTaxYear.Text = value; }
        }
        public string GetSeries
        {
            get { return txtSeries.Text; }
            set { txtSeries.Text = value; }
        }

        private void txtDistCode_TextChanged(object sender, EventArgs e)
        {
            if (txtDistCode.Text.Trim().Length == 2)
                txtTaxYear.Focus();
        }

        private void txtLGUCode_TextChanged(object sender, EventArgs e)
        {
            if (txtLGUCode.Text.Trim().Length == 3)
                txtDistCode.Focus();
        }

        public string GetArn()
        {
            m_sARN = txtLGUCode.Text + "-" + txtDistCode.Text + "-" + txtTaxYear.Text + "-" + txtSeries.Text;

            if (m_sARN.Length < 16)
                m_sARN = "";

            return m_sARN;
        }

        public void CreateARN(string sYear, string sBrgyCode)
        {
            txtTaxYear.Text = sYear;

            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sTmpArn = string.Empty;
            string sLguCodeConfig = "04";

            //strQuery = $"select distinct (dist_code) from brgy where brgy_code = '{sBrgyCode}'";
            //txtDistCode.Text = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
            txtDistCode.Text = sBrgyCode;
            txtLGUCode.Text = AppSettingsManager.GetConfigValue(sLguCodeConfig); //get lgucode

            strQuery = $"select arn from current_arn where arn like '%-{sYear}-%'";
            sTmpArn = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();

            if(string.IsNullOrEmpty(sTmpArn))
            {
                txtSeries.Text = "0001";
            }
            else
            {
                int iSeries = 0;
                int.TryParse(sTmpArn.Substring(12,4), out iSeries);  //021-01-2013-1840
                txtSeries.Text = (iSeries+1).ToString();
                FormatSeries(txtSeries.TextLength);
            }

            strQuery = $"delete from current_arn  where arn like '%-{sYear}-%'";
            db.Database.ExecuteSqlCommand(strQuery);

            strQuery = $"insert into current_arn values (:1)";
            db.Database.ExecuteSqlCommand(strQuery,
                new OracleParameter(":1", GetArn()));
        }

        public void SetArn(string sArn)
        {
            try
            {
                txtLGUCode.Text = sArn.Substring(0, 3);
                txtDistCode.Text = sArn.Substring(4, 2);
                txtTaxYear.Text = sArn.Substring(7, 4);
                txtSeries.Text = sArn.Substring(12, 4);
            }
            catch { }
        }

        public void Clear()
        {
            txtLGUCode.Text = "";
            txtTaxYear.Text = "";
            txtDistCode.Text = "";
            txtSeries.Text = "";
        }

    }
}
