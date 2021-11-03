﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using Modules.Utilities;
using System.Windows.Forms;
using EPSEntities.Entity;
using Oracle.ManagedDataAccess.Client;
using Common.AppSettings;
using ARCSEntities.Connection;
using Common.StringUtilities;
using Common.DataConnector;

namespace Modules.Billing
{
    public class BillCertificate : FormClass
    {

        public BillCertificate(frmBilling Form) : base(Form)
        { }

        public override void FormLoad()
        {
            //RecordFrm.dgvPermit.Enabled = false;
            RecordFrm.dgvPermit.Enabled = true;
            RecordFrm.grpAddFees.Visible = false;
        }

        public override void DisplayAssessmentData()
        {
            var db = new EPSConnection(dbConn);
            RecordFrm.dgvPermit.Rows.Clear();

            string sPermit = string.Empty;
            string strWhereCond = string.Empty;

            strWhereCond = $" where arn = '{RecordFrm.m_sAN}' and main_application = 1";

            var result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
                         select a;

            foreach (var item in result)
            {
                PermitList permit = new PermitList(null);
                sPermit = permit.GetPermitDesc(item.PERMIT_CODE);
                bool bAssessed = false;

                if (RecordFrm.PermitCode == item.PERMIT_CODE.Substring(0, 2))
                    bAssessed = true;
                else
                    bAssessed = ValidatePermitAssessed(item.PERMIT_CODE.Substring(0, 2));
                RecordFrm.dgvPermit.Rows.Add(bAssessed, sPermit, item.PERMIT_CODE);
            }

            m_sPermitCodeSelected = RecordFrm.PermitCode;
            LoadAssessmentGrid(RecordFrm.PermitCode);
        }

        public override void Save()
        {
            string sQuery = string.Empty;
            var db = new EPSConnection(dbConn);

            if (MessageBox.Show("Save Billing?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(RecordFrm.txtBillNo.Text.ToString()))
                {
                    sQuery = $"insert into billing_hist select * from billing where arn = '{RecordFrm.m_sAN}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from billing where arn = '{RecordFrm.m_sAN}'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"insert into taxdues_hist select * from taxdues where arn = '{RecordFrm.m_sAN}' and fees_code like '{RecordFrm.PermitCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from taxdues where arn = '{RecordFrm.m_sAN}' and fees_code like '{RecordFrm.PermitCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"insert into tax_details_hist select * from tax_details where arn = '{RecordFrm.m_sAN}' and fees_code like '{RecordFrm.PermitCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);

                    sQuery = $"delete from tax_details where arn = '{RecordFrm.m_sAN}' and fees_code like '{RecordFrm.PermitCode}%'";
                    db.Database.ExecuteSqlCommand(sQuery);
                }
                else
                {
                    RecordFrm.txtBillNo.Text = GenerateBillNo();
                }

                sQuery = "insert into billing values (:1,:2,:3,:4,:5,:6,:7)";
                db.Database.ExecuteSqlCommand(sQuery,
                        new OracleParameter(":1", RecordFrm.m_sAN),
                        new OracleParameter(":2", RecordFrm.txtStatus.Text.ToString()),
                        new OracleParameter(":3", RecordFrm.txtBillNo.Text.ToString()),
                        new OracleParameter(":4", AppSettingsManager.GetCurrentDate()),
                        new OracleParameter(":5", AppSettingsManager.SystemUser.UserCode),
                        new OracleParameter(":6", AppSettingsManager.GetCurrentDate()),
                        new OracleParameter(":7", ""));

                PermitList m_lstPermit = new PermitList(null);

                if (RecordFrm.PermitCode == m_lstPermit.GetPermitCode("EXCAVATION"))
                    OnSaveIntoExcavation();


                sQuery = "insert into tax_details ";
                sQuery += $"select arn,'{RecordFrm.txtBillNo.Text.ToString()}',fees_code,fees_unit,";
                sQuery += "fees_unit_value,fees_amt,orig_amt,permit_code from bill_tmp ";
                sQuery += $"where arn = '{RecordFrm.m_sAN}' and permit_code like '{RecordFrm.PermitCode}%'";
                db.Database.ExecuteSqlCommand(sQuery);

                sQuery = "insert into taxdues ";
                sQuery += $"select arn, '{RecordFrm.txtBillNo.Text.ToString()}', fees_code, fees_amt from bill_tmp ";
                sQuery += $"where arn = '{RecordFrm.m_sAN}' and permit_code like '{RecordFrm.PermitCode}%'";
                db.Database.ExecuteSqlCommand(sQuery);
                if (!UpdateReassessmentBilling())
                    return;

                MessageBox.Show("Billing Successfully Updated!", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //VALIDATE FIRST IF ALL PERMIT WAS BILLED
                
                    try
                    {
                        var dbarcs = new ARCSConnection(dbConnArcs);


                        Accounts account = new Accounts();
                        account.GetOwner(m_sProjOwner);
                        OracleResultSet result = new OracleResultSet();
                        result.CreateANGARCS();

                        string sFeesCode = string.Empty;
                        double dFeesAmt = 0;
                        string sInsert = string.Empty;

                        result.Query = $"delete from eps_billing where arn = '{RecordFrm.m_sAN}' and bill_no = '{RecordFrm.txtBillNo.Text.ToString()}'"; //AFM 20191010 delete previous arcs billing
                        result.ExecuteNonQuery();
                        result.Close();

                        sQuery = $"select fees_code, fees_amt from taxdues where arn = '{RecordFrm.m_sAN}' and bill_no = '{RecordFrm.txtBillNo.Text.ToString()}'";
                        var epsrec = db.Database.SqlQuery<TAXDUES>(sQuery);

                        foreach (var items in epsrec)
                        {
                            sFeesCode = "E" + items.FEES_CODE;
                            dFeesAmt = items.FEES_AMT;
                            result.Query = @"insert into eps_billing(arn,acct_code,mrs_acct_code,
                                    acct_ln,acct_fn,acct_mi,acct_house_no,acct_street,
                                    acct_brgy,acct_mun,acct_prov,acct_zip,bill_no,
                                    fees_code,fees_amt)
                                    values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15)";
                            result.AddParameter(":1", RecordFrm.m_sAN);
                            result.AddParameter(":2", m_sProjOwner);
                            result.AddParameter(":3", m_sProjOwner);
                            result.AddParameter(":4", StringUtilities.HandleApostrophe(account.LastName));
                            result.AddParameter(":5", StringUtilities.HandleApostrophe(account.FirstName));
                            result.AddParameter(":6", StringUtilities.HandleApostrophe(account.MiddleInitial));
                            result.AddParameter(":7", StringUtilities.HandleApostrophe(account.HouseNo));
                            result.AddParameter(":8", StringUtilities.HandleApostrophe(account.Address));
                            result.AddParameter(":9", StringUtilities.HandleApostrophe(account.Barangay));
                            result.AddParameter(":10", StringUtilities.HandleApostrophe(account.City));
                            result.AddParameter(":11", StringUtilities.HandleApostrophe(account.Province));
                            result.AddParameter(":12", account.ZIP);
                            result.AddParameter(":13", RecordFrm.txtBillNo.Text.ToString());
                            result.AddParameter(":14", sFeesCode);
                            result.AddParameter(":15", dFeesAmt);

                            result.ExecuteNonQuery();
                            result.Close();


                        //sInsert = @"insert into eps_billing(arn,acct_code,mrs_acct_code,
                        //    acct_ln,acct_fn,acct_mi,acct_house_no,acct_street,
                        //    acct_brgy,acct_mun,acct_prov,acct_zip,bill_no,
                        //    fees_code,fees_amt)
                        //    values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15)";
                        //dbarcs.Database.ExecuteSqlCommand(sInsert,
                        //        new OracleParameter(":1", RecordFrm.m_sAN),
                        //        new OracleParameter(":2", m_sProjOwner),
                        //        new OracleParameter(":3", m_sProjOwner),
                        //        new OracleParameter(":4", StringUtilities.HandleApostrophe(account.LastName)),
                        //        new OracleParameter(":5", StringUtilities.HandleApostrophe(account.FirstName)),
                        //        new OracleParameter(":6", StringUtilities.HandleApostrophe(account.MiddleInitial)),
                        //        new OracleParameter(":7", StringUtilities.HandleApostrophe(account.HouseNo)),
                        //        new OracleParameter(":8", StringUtilities.HandleApostrophe(account.Address)),
                        //        new OracleParameter(":9", StringUtilities.HandleApostrophe(account.Barangay)),
                        //        new OracleParameter(":10", StringUtilities.HandleApostrophe(account.City)),
                        //        new OracleParameter(":11", StringUtilities.HandleApostrophe(account.Province)),
                        //        new OracleParameter(":12", account.ZIP),
                        //        new OracleParameter(":13", RecordFrm.txtBillNo.Text.ToString()),
                        //        new OracleParameter(":14", sFeesCode),
                        //        new OracleParameter(":15", dFeesAmt));

                    }
                    }
                    catch { }
                

                

                if (Utilities.AuditTrail.InsertTrail(RecordFrm.ModuleCode, RecordFrm.m_sModule, "ARN: " + RecordFrm.m_sAN) == 0)
                {
                    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                RecordFrm.btnSave.Enabled = false;
                RecordFrm.btnPrint.Enabled = true;
                RecordFrm.btnCancel.Text = "Exit";

            }
        }
    }
}
