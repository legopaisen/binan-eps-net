using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modules.Utilities;
using Oracle.ManagedDataAccess.Client;
using Common.StringUtilities;
using System.Data;
using EPSEntities.Connection;
using System.Windows.Forms;
using Common.AppSettings;
using Common.DataConnector;

namespace Modules.Transactions
{
    public class RecordForm
    {
        public static ConnectionString dbConn = new ConnectionString();
        protected frmRecords RecordFrm = null;
        protected PermitList m_lstPermit;
        protected string m_sArch = string.Empty;
        protected string m_sEngr = string.Empty;
        protected string m_sLotOwner = string.Empty;
        protected string m_sStrucOwner = string.Empty;
        protected int engrIDTmp = 0;
        protected string engrID = string.Empty;

        public RecordForm(frmRecords Form)
        {
            this.RecordFrm = Form;

        }

        public virtual void FormLoad()
        {
        }

        public virtual void PopulatePermit()
        {
        }

        public virtual void ButtonAddClick(string sender)
        {


        }
        public virtual void ButtonEditClick(string sender)
        {


        }
        public virtual void ButtonDeleteClick()
        {


        }

        public virtual void Save()
        {

        }

        public virtual void ClearControl()
        { }

        public virtual void EnableRecordEntry()
        {

        }

        public virtual void ButtonSearchClick()
        {


        }

        public virtual void InitEdit(bool blnEnable)
        {

        }

        private void SaveEngineer()
        {
            OracleResultSet result = new OracleResultSet();
            OracleResultSet result2 = new OracleResultSet();
            string EngrTmp = string.Empty;
            if (m_sArch == "")
                EngrTmp = m_sEngr;
            else
                EngrTmp = m_sArch;

            if (RecordFrm.SourceClass == "ENG_REC_EDIT") //AFM 20191022 saving of engineer for editing
                try
                {
                    for (int cnt = 0; cnt < RecordFrm.formEngr.dgvList.Rows.Count; cnt++)
                    {
                        result.Query = $"delete from engineer_tbl where engr_code = '{RecordFrm.formEngr.dgvList[0, cnt].Value.ToString()}'";
                        result.ExecuteNonQuery();
                        result.Close();

                        result2.Query = $"insert into engineer_tbl values('{RecordFrm.formEngr.dgvList[0, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[4, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[1, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[2, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[3, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[6, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[7, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[8, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[5, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[9, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[10, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[11, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[12, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[13, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[15, cnt].Value.ToString()}','{RecordFrm.formEngr.dgvList[14, cnt].Value.ToString()}')";
                        result2.ExecuteNonQuery();
                        result2.Close();
                    }
                }
                catch { }
            else //AFM 20191022 saving of engineer for new
            {
                result2.Query = $"insert into engineer_tbl values('{EngrTmp}','{RecordFrm.formEngr.cmbEngrType.Text}','{RecordFrm.formEngr.txtLastName.Text}','{RecordFrm.formEngr.txtFirstName.Text}','{RecordFrm.formEngr.txtMI.Text}','{RecordFrm.formEngr.txtHseNo.Text}','{RecordFrm.formEngr.txtLotNo.Text}','{RecordFrm.formEngr.txtBlkNo.Text}','{RecordFrm.formEngr.txtStreet.Text}','{RecordFrm.formEngr.cmbBrgy.Text}','{RecordFrm.formEngr.txtMun.Text}','{RecordFrm.formEngr.txtProv.Text}','{RecordFrm.formEngr.txtZIP.Text}','{RecordFrm.formEngr.txtTIN.Text}','{RecordFrm.formEngr.txtPRC.Text}','{RecordFrm.formEngr.txtPTR.Text}')";
                result2.ExecuteNonQuery();
                result2.Close();
            }

        }

        protected void GenerateEngrNo() //AFM 20191021 add new engineer
        {
            OracleResultSet result = new OracleResultSet();
            result.Query = "select max(engr_code) from engineer_tbl";
            if(result.Execute())
                if(result.Read())
                {
                    engrIDTmp = Convert.ToInt32(result.GetString(0)) + 1;
                    engrID = String.Format("{0:000000}", engrIDTmp);
                }
        }

        protected void GetArchEngr(string sPermit)
        {
            m_sArch = string.Empty;
            m_sEngr = string.Empty;

            string sTmp = string.Empty;

            for (int i = 0; i < RecordFrm.formEngr.dgvList.Rows.Count; i++)
            {
                try
                {
                    sTmp = RecordFrm.formEngr.dgvList[4, i].Value.ToString();
                    if (sTmp == "ARCHITECT")
                    {
                        if (RecordFrm.formEngr.dgvList[0, i].Value.ToString() == "")
                        {
                            GenerateEngrNo();
                            m_sArch = engrID;
                            SaveEngineer();
                        }
                        else
                            m_sArch = RecordFrm.formEngr.dgvList[0, i].Value.ToString();
                            SaveEngineer();
                    }
                    else
                    {
                        m_sEngr = "";
                        if (sTmp == "ELECTRICAL" && sPermit.Contains("ELECTRICAL"))
                        {
                            if (RecordFrm.formEngr.dgvList[0, i].Value.ToString() == "")
                            {
                                GenerateEngrNo();
                                m_sEngr = engrID;
                                SaveEngineer();
                            }
                            else
                            {
                                m_sEngr = RecordFrm.formEngr.dgvList[0, i].Value.ToString();
                                SaveEngineer();
                            }
                            break;
                        }
                        else if (sTmp == "MECHANICAL" && sPermit.Contains("MECHANICAL"))
                        {
                            if (RecordFrm.formEngr.dgvList[0, i].Value.ToString() == "")
                            {
                                GenerateEngrNo();
                                m_sEngr = engrID;
                                SaveEngineer();
                            }
                            else
                            {
                                m_sEngr = RecordFrm.formEngr.dgvList[0, i].Value.ToString();
                                SaveEngineer();
                            }
                            break;
                        }
                        else if (sTmp == "SANITARY" && sPermit.Contains("SANITARY"))
                        {
                            if (RecordFrm.formEngr.dgvList[0, i].Value.ToString() == "")
                            {
                                GenerateEngrNo();
                                m_sEngr = engrID;
                                SaveEngineer();
                            }
                            else
                            {
                                m_sEngr = RecordFrm.formEngr.dgvList[0, i].Value.ToString();
                                SaveEngineer();
                            }
                            break;
                        }
                        else if (sTmp == "CIVIL")
                        {
                            if (!sPermit.Contains("ELECTRICAL") && !sPermit.Contains("MECHANICAL") && !sPermit.Contains("SANITARY"))
                            {
                                if (RecordFrm.formEngr.dgvList[0, i].Value.ToString() == "")
                                {
                                    GenerateEngrNo();
                                    m_sEngr = engrID;
                                    SaveEngineer();
                                }
                                else
                                {
                                    m_sEngr = RecordFrm.formEngr.dgvList[0, i].Value.ToString();
                                    SaveEngineer();
                                }
                                break;
                            }
                        }
                    }

                }
                catch { }
            }
        }

        protected void SaveBuilding()
        {
            string strQuery = string.Empty;
            string strMaterial = string.Empty;
                
            var db = new EPSConnection(dbConn);

            if (!string.IsNullOrEmpty(RecordFrm.formBldgDate.txtBldgNo.Text.ToString()))
            {
                strQuery = $"delete from building where bldg_no = '{RecordFrm.formBldgDate.txtBldgNo.Text.ToString()}'";
                db.Database.ExecuteSqlCommand(strQuery);

                strMaterial = ((DataRowView)RecordFrm.formBldgDate.cmbMaterials.SelectedItem)["Code"].ToString();
                int iBldg = 0;
                string sTmpNull = "0";
                string sLandPin = "";
                sLandPin = RecordFrm.formBldgDate.txtPIN.Text.ToString();
                int.TryParse(RecordFrm.formBldgDate.txtBldgNo.Text.ToString(), out iBldg);
                try
                {
                    strQuery = $"insert into building values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13)";
                    db.Database.ExecuteSqlCommand(strQuery,
                        new OracleParameter(":1", iBldg),
                        new OracleParameter(":2", StringUtilities.HandleApostrophe(RecordFrm.formBldgDate.txtBldgName.Text.ToString())),
                        new OracleParameter(":3", sLandPin),
                            new OracleParameter(":4", StringUtilities.RemoveComma(RecordFrm.formBldgDate.txtHeight.Text.ToString())),
                            new OracleParameter(":5", StringUtilities.RemoveComma(RecordFrm.formBldgDate.txtArea.Text.ToString())),
                            new OracleParameter(":6", sTmpNull),
                            new OracleParameter(":7", RecordFrm.formBldgDate.txtUnits.Text),
                            new OracleParameter(":8", RecordFrm.formBldgDate.txtStoreys.Text),
                            new OracleParameter(":9", strMaterial),
                            new OracleParameter(":10", StringUtilities.RemoveComma(RecordFrm.formBldgDate.txtCost.Text.ToString())),
                            new OracleParameter(":11", sTmpNull),
                            new OracleParameter(":12", null),
                            new OracleParameter(":13", StringUtilities.RemoveComma(RecordFrm.formBldgDate.txtAssVal.Text.ToString())));

                    
                }
                catch (Exception ex) // catches any error
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                strQuery = $"update bldg_units set arn = '{RecordFrm.ARN}' where bldg_no = {iBldg}";
                db.Database.ExecuteSqlCommand(strQuery);
            }
        }

        public virtual void DisplayData()
        { }

        public virtual void DisplayBuilding(int iBldgNo)
        { }

        protected void DisplayOwners()
        {
            Accounts account = new Accounts();
            account.GetOwner(m_sStrucOwner);

            RecordFrm.formStrucOwn.ClearControls();
            RecordFrm.formStrucOwn.StrucAcctNo = account.OwnerCode;
            RecordFrm.formStrucOwn.txtLastName.Text = account.LastName;
            RecordFrm.formStrucOwn.txtFirstName.Text = account.FirstName;
            RecordFrm.formStrucOwn.txtMI.Text = account.MiddleInitial;
            RecordFrm.formStrucOwn.txtHseNo.Text = account.HouseNo;
            RecordFrm.formStrucOwn.txtLotNo.Text = account.LotNo;
            RecordFrm.formStrucOwn.txtBlkNo.Text = account.BlkNo;
            RecordFrm.formStrucOwn.txtStreet.Text = account.Address;
            RecordFrm.formStrucOwn.txtZIP.Text = account.ZIP;
            RecordFrm.formStrucOwn.txtTIN.Text = account.TIN;
            RecordFrm.formStrucOwn.txtTCT.Text = account.TCT;
            RecordFrm.formStrucOwn.txtCTC.Text = account.CTC;
            RecordFrm.formStrucOwn.txtTelNo.Text = account.TelNo;
            RecordFrm.formStrucOwn.cmbBrgy.Text = account.Barangay;
            RecordFrm.formStrucOwn.txtMun.Text = account.City;
            RecordFrm.formStrucOwn.txtProv.Text = account.Province;

            account = new Accounts();
            account.GetOwner(m_sLotOwner);

            RecordFrm.formLotOwn.ClearControls();
            RecordFrm.formLotOwn.LotAcctNo = account.OwnerCode;
            RecordFrm.formLotOwn.txtLastName.Text = account.LastName;
            RecordFrm.formLotOwn.txtFirstName.Text = account.FirstName;
            RecordFrm.formLotOwn.txtMI.Text = account.MiddleInitial;
            RecordFrm.formLotOwn.txtHseNo.Text = account.HouseNo;
            RecordFrm.formLotOwn.txtLotNo.Text = account.LotNo;
            RecordFrm.formLotOwn.txtBlkNo.Text = account.BlkNo;
            RecordFrm.formLotOwn.txtStreet.Text = account.Address;
            RecordFrm.formLotOwn.txtZIP.Text = account.ZIP;
            RecordFrm.formLotOwn.txtTIN.Text = account.TIN;
            RecordFrm.formLotOwn.txtTCT.Text = account.TCT;
            RecordFrm.formLotOwn.txtCTC.Text = account.CTC;
            RecordFrm.formLotOwn.txtTelNo.Text = account.TelNo;
            RecordFrm.formLotOwn.cmbBrgy.Text = account.Barangay;
            RecordFrm.formLotOwn.txtMun.Text = account.City;
            RecordFrm.formLotOwn.txtProv.Text = account.Province;
        }

        public virtual void DisplayEngrArch()
        { }

        public virtual bool ValidateData()
        {
            return true;
        }
    }
}
