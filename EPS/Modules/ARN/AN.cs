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
            txtMonth.Text = "";
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

        public string GetMonth
        {
            get { return txtMonth.Text; }
            set { txtMonth.Text = value; }
        }

        public string GetSeries
        {
            get { return txtSeries.Text; }
            set { txtSeries.Text = value; }
        }

        private void AN_Load(object sender, EventArgs e)
        {

        }

        public string GetAn()
        {
            if (!string.IsNullOrEmpty(txtCode.Text.ToString()))
            {
                if (string.IsNullOrEmpty(txtYear.Text.ToString()))
                    txtYear.Text = string.Format("{0:yyyy}", AppSettingsManager.GetCurrentDate());
                if (string.IsNullOrEmpty(txtMonth.Text.ToString()))
                    txtMonth.Text = string.Format("{0:MM}", AppSettingsManager.GetCurrentDate());
            }

            m_sAN = txtCode.Text + "-" + txtYear.Text + "-" + txtMonth.Text + "-" + txtSeries.Text;

            if (m_sAN.Length < 15)
                m_sAN = "";

            return m_sAN;
        }

        public void CreateAN(string sApplication)
        {
            var db = new EPSConnection(dbConn);
            string strQuery = string.Empty;
            string sTmpArn = string.Empty;

            PermitList permit = new PermitList(null);

            txtCode.Text = permit.GetPermitAppCode(sApplication);

            string sYear = string.Format("{0:yyyy}", AppSettingsManager.GetCurrentDate());

            strQuery = $"select arn from current_arn where arn like '{txtCode.Text}-{sYear}-%'";
            sTmpArn = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
            int iSeries = 0;
            if (string.IsNullOrEmpty(sTmpArn))
            {
                txtSeries.Text = "0001";
            }
            else
            {
                int.TryParse(sTmpArn.Substring(11, 4), out iSeries);  //BP-2019-01-0001
                txtSeries.Text = (iSeries + 1).ToString();
                FormatSeries(txtSeries.TextLength);
            }
            // validate if with duplicate
            string sUsedArn = string.Empty;
            int iUsedArn = 0;

            strQuery = $"select max(arn) from (select arn from application where arn like '{txtCode.Text}-{sYear}-%' union ";
            strQuery += $"select arn from application_que where arn like '{txtCode.Text}-{sYear}-%')";
            sUsedArn = db.Database.SqlQuery<string>(strQuery).SingleOrDefault();
            if (!string.IsNullOrEmpty(sUsedArn))
            {
                int.TryParse(sUsedArn.Substring(11, 4), out iUsedArn);
            }

            if(iUsedArn >= (iSeries + 1))
            {
                iUsedArn = iUsedArn + 1;
                txtSeries.Text = iUsedArn.ToString();
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
                txtCode.Text = sArn.Substring(0, 2);
                txtYear.Text = sArn.Substring(3, 4);
                txtMonth.Text = sArn.Substring(8, 2);
                txtSeries.Text = sArn.Substring(11, 4);
            }
            catch { }
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtYear.Text = "";
            txtMonth.Text = "";
            txtSeries.Text = "";
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text.ToString().Length == 2)
                txtYear.Focus();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            if (txtYear.Text.ToString().Length == 4)
                txtMonth.Focus();
        }

        private void txtMonth_TextChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text.ToString().Length == 2)
                txtSeries.Focus();
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
            int iCount = txtMonth.Text.ToString().Length;

            switch (iCount)
            {
                case 1:
                    {
                        txtMonth.Text = "0" + txtMonth.Text;
                        break;
                    }
                case 2:
                    {
                        txtMonth.Text = txtMonth.Text;
                        break;
                    }

            }
        }

        private void txtSeries_Leave(object sender, EventArgs e)
        {
            FormatSeries(txtSeries.Text.ToString().Length);
        }
    }
}
