using System;
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
	public class Building : FormClass
	{
		
		public Building(frmBilling Form) : base(Form)
		{ }

		//private List<string> sPermitListSave;
		//public List<string> PermitListSave
		//{
			//get { return sPermitListSave; }
			//set { sPermitListSave = value; }
		//}
		private static List<string> sPermitListSave;

		public static List<string> PermitListSave
		{
			get { return sPermitListSave; }
			set { sPermitListSave = value; }
		}


		public override void FormLoad()
		{
			//RecordFrm.grpAddFees.Visible = true; --binan version
			OracleResultSet result = new OracleResultSet();
		}

	   

		public override void DisplayAssessmentData()
		{
			// Note: enable Other permits assessment override for Engr official

			var db = new EPSConnection(dbConn);
			RecordFrm.dgvPermit.Rows.Clear();

			string sPermit = string.Empty;
			string strWhereCond = string.Empty;

			strWhereCond = $" where arn = '{RecordFrm.m_sAN}' order by permit_code";

			if (AppSettingsManager.GetConfigValue("30") == "1")
			{
				var result = from a in Records.ApplicationTblList.GetRecord(strWhereCond)
							 select a;

				foreach (var item in result)
				{
					PermitList permit = new PermitList(null);
					sPermit = permit.GetPermitDesc(item.PERMIT_CODE);
					bool bAssessed = false;

					if (RecordFrm.PermitCode == item.PERMIT_CODE.Substring(0, 2))
					{
						//bAssessed = true;
					}

					else
					{
						//bAssessed = ValidatePermitAssessed(item.PERMIT_CODE.Substring(0, 2));
					}
					RecordFrm.dgvPermit.Rows.Add(bAssessed, sPermit, item.PERMIT_CODE);
				}

			}
			else
			{
				var result = from a in Records.ApplicationQueList.GetApplicationQue(strWhereCond)
							 select a;

				foreach (var item in result)
				{
					PermitList permit = new PermitList(null);
					sPermit = permit.GetPermitDesc(item.PERMIT_CODE);
					bool bAssessed = false;

					if (RecordFrm.PermitCode == item.PERMIT_CODE.Substring(0, 2))
					{
						//bAssessed = true;
					}

					else
					{
						//bAssessed = ValidatePermitAssessed(item.PERMIT_CODE.Substring(0, 2));
					}
					RecordFrm.dgvPermit.Rows.Add(bAssessed, sPermit, item.PERMIT_CODE);
				}
			}


		 

			m_sPermitCodeSelected = RecordFrm.PermitCode;
			LoadAssessmentGrid(RecordFrm.PermitCode);
		}

		private bool ValidateBillNo()
		{
			OracleResultSet result = new OracleResultSet();
			string sArn = string.Empty;
			if (AppSettingsManager.GetConfigValue("30") == "1")
			{
				result.Query = "select bill_no,arn from billing_hist where bill_no = '" + RecordFrm.txtBillNo.Text + "'";
				if (result.Execute())
					if (result.Read())
					{
						sArn = result.GetString("arn");
						if (RecordFrm.an1.ToString() != sArn)
							return true;
						else
							return false;
					}
				result.Query = "select bill_no,arn from billing where bill_no = '" + RecordFrm.txtBillNo.Text + "'";
				if (result.Execute())
					if (result.Read())
					{
						sArn = result.GetString("arn");
						if (RecordFrm.an1.GetAn().ToString() != sArn)
							return true;
						else
							return false;
					}
			}
			return false;
		}

		public override void Save()
		{
			//AFM 20191105 validations (s)
			if(ValidateBillNo())
			{
				MessageBox.Show("Bill No. Already Exists!");
				return;
			}
			if (AppSettingsManager.GetConfigValue("30") == "1" & RecordFrm.txtBillNo.Text == "")
			{
				MessageBox.Show("Bill No. is empty!");
				return;
			}
			//AFM 20191105 validations (e)


			string sQuery = string.Empty;
			var db = new EPSConnection(dbConn);

			try
			{
				if (MessageBox.Show("Save Billing?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (!string.IsNullOrEmpty(RecordFrm.txtBillNo.Text.ToString()))
					{
						sQuery = $"insert into billing_hist select * from billing where arn = '{RecordFrm.m_sAN}'";
						db.Database.ExecuteSqlCommand(sQuery);

						sQuery = $"delete from billing where arn = '{RecordFrm.m_sAN}'";
						db.Database.ExecuteSqlCommand(sQuery);

						sQuery = $"insert into taxdues_hist select * from taxdues where arn = '{RecordFrm.m_sAN}'";
						db.Database.ExecuteSqlCommand(sQuery);

						sQuery = $"delete from taxdues where arn = '{RecordFrm.m_sAN}'";
						db.Database.ExecuteSqlCommand(sQuery);

						sQuery = $"insert into tax_details_hist select * from tax_details where arn = '{RecordFrm.m_sAN}'";
						db.Database.ExecuteSqlCommand(sQuery);

						sQuery = $"delete from tax_details where arn = '{RecordFrm.m_sAN}'";
						db.Database.ExecuteSqlCommand(sQuery);
					}
					else
					{
						RecordFrm.txtBillNo.Text = GenerateBillNo();
					}

					sQuery = "insert into billing values (:1,:2,:3,:4,:5,:6,:7,:8)";
					db.Database.ExecuteSqlCommand(sQuery,
							new OracleParameter(":1", RecordFrm.m_sAN),
							new OracleParameter(":2", RecordFrm.txtStatus.Text.ToString()),
							new OracleParameter(":3", RecordFrm.txtBillNo.Text.ToString()),
							new OracleParameter(":4", AppSettingsManager.GetCurrentDate()),
							new OracleParameter(":5", AppSettingsManager.SystemUser.UserCode),
							new OracleParameter(":6", AppSettingsManager.GetCurrentDate()),
							new OracleParameter(":7", ""),
							new OracleParameter(":8", m_sProjOwner)); 

					PermitList m_lstPermit = new PermitList(null);

					if (RecordFrm.PermitCode == m_lstPermit.GetPermitCode("EXCAVATION"))
						OnSaveIntoExcavation();

					sQuery = "insert into tax_details ";
					sQuery += $"select arn,'{RecordFrm.txtBillNo.Text.ToString()}',fees_code,fees_unit,";
					sQuery += "fees_unit_value,fees_amt, orig_amt, permit_code, fees_category from bill_tmp ";
					sQuery += $"where arn = '{RecordFrm.m_sAN}'";
					db.Database.ExecuteSqlCommand(sQuery);

					sQuery = "insert into taxdues ";
					sQuery += $"select arn, '{RecordFrm.txtBillNo.Text.ToString()}', fees_code, fees_amt, fees_category, permit_code from bill_tmp ";
					sQuery += $"where arn = '{RecordFrm.m_sAN}'";
					db.Database.ExecuteSqlCommand(sQuery);


					if (!UpdateReassessmentBilling())
						return;
                    if (AppSettingsManager.GetConfigValue("30") != "1") 
                    {
                        if (CheckAdjustments()) //application with overriden values are subject for approval in engineer's module - requested by binan
					    {
						    MessageBox.Show($"AN: {RecordFrm.m_sAN} is subject for approval!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

						    if (Utilities.AuditTrail.InsertTrail(RecordFrm.ModuleCode, RecordFrm.m_sModule, "ARN: " + RecordFrm.m_sAN) == 0)
						    {
							    MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
							    return;
						    }
						    RecordFrm.EnableControls(false);
						    RecordFrm.grpAddFees.Enabled = true;
						    RecordFrm.btnSave.Enabled = false;
						    RecordFrm.btnCancel.Text = "Exit";
						    return;
					    }
                    }
                    MessageBox.Show("Billing Successfully Saved!\nARN: "+ RecordFrm.m_sAN +"\nBill No.: "+ RecordFrm.txtBillNo.Text.ToString() +" ", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);

					//VALIDATE FIRST IF ALL PERMIT WAS BILLED
					int iPermitCnt = 0;
					int iPermitBilled = 0;

					if (AppSettingsManager.GetConfigValue("30") == "1") //AFM 20191016 buildup mode
					{
						sQuery = $"select count(*) from application where arn = '{RecordFrm.m_sAN}'";
						iPermitCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
					}
					else
					{
						sQuery = $"select count(*) from application_que where arn = '{RecordFrm.m_sAN}'";
						iPermitCnt = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
					}

					//sQuery = $"select count(distinct substr(fees_code,0,2)) from taxdues where arn = '{RecordFrm.m_sAN}'";
					//iPermitBilled = db.Database.SqlQuery<Int32>(sQuery).SingleOrDefault();
					for (int cnt = 0; cnt < RecordFrm.dgvPermit.Rows.Count; cnt++) //AFM 20191018 count "permit"
					{
						if ((bool)RecordFrm.dgvPermit[0, cnt].Value == true)
						{
							iPermitBilled += 1;
						}
					}
					if (iPermitCnt != iPermitBilled)
					{
						MessageBox.Show("Other permits not yet billed, record is not yet ready for payment", "Billing", MessageBoxButtons.OK, MessageBoxIcon.Information);
						RecordFrm.btnPrint.Enabled = false;
					}
					else
					{
						if (AppSettingsManager.GetConfigValue("30") == "0")
						{
							try
							{
								var dbarcs = new ARCSConnection(dbConnArcs);
								OracleResultSet result = new OracleResultSet();
								result.CreateANGARCS(); //connect to arcs database

								Accounts account = new Accounts();
								account.GetOwner(m_sProjOwner);

								string sFeesCode = string.Empty;
								double dFeesAmt = 0;
								string sInsert = string.Empty;

								result.Query = $"delete from eps_billing where arn = '{RecordFrm.m_sAN}' and bill_no = '{RecordFrm.txtBillNo.Text.ToString()}'"; //AFM 20191010 delete previous arcs billing
								result.ExecuteNonQuery();
								result.Close();
								//sQuery = $"delete from eps_billing where arn = '{RecordFrm.m_sAN}' and bill_no = '{RecordFrm.txtBillNo.Text.ToString()}'";
								//dbarcs.Database.ExecuteSqlCommand(sQuery); //AFM 20191010 delete previous arcs billing

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
									//acct_ln,acct_fn,acct_mi,acct_house_no,acct_street,
									//acct_brgy,acct_mun,acct_prov,acct_zip,bill_no,
									//fees_code,fees_amt)
									//values (:1,:2,:3,:4,:5,:6,:7,:8,:9,:10,:11,:12,:13,:14,:15)";
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
						}
						if (AppSettingsManager.GetConfigValue("30") == "0")
							RecordFrm.btnPrint.Enabled = true;
						else
							RecordFrm.btnPrint.Enabled = false;
					}


					if (Utilities.AuditTrail.InsertTrail(RecordFrm.ModuleCode, RecordFrm.m_sModule, "ARN: " + RecordFrm.m_sAN) == 0)
					{
						MessageBox.Show("Failed to insert audit trail.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					RecordFrm.EnableControls(false);
					RecordFrm.grpAddFees.Enabled = true;
					RecordFrm.btnSave.Enabled = false;
					RecordFrm.btnCancel.Text = "Exit";

				}
			}
			catch (Exception ex)
			{
				ex.ToString();
				MessageBox.Show("Unable to save to the database", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}


		}

		private string GetFeesDesc(string sCode, string sCat)
		{
			OracleResultSet res = new OracleResultSet();
			if(sCat == "MAIN")
				res.Query = $"select * from subcategories where fees_code = '{sCode}' and fees_term <> 'SUBCATEGORY'";
			else if(sCat == "OTHERS")
				res.Query = $"select * from other_subcategories where fees_code = '{sCode}' and fees_term <> 'SUBCATEGORY'";
			else if (sCat == $"ADDITIONAL")
				res.Query = $"select * from addl_subcategories where fees_code = '{sCode}' and fees_term <> 'SUBCATEGORY'";

			string sDesc = string.Empty;
			if(res.Execute())
				if(res.Read())
				{
					sDesc = res.GetString("fees_desc");
				}
			res.Close();

			return sDesc;
		}

		private bool CheckAdjustments()
		{
			OracleResultSet res = new OracleResultSet();
			OracleResultSet res2 = new OracleResultSet();
            int iCnt = 0;
			string sFeesCode = string.Empty;
			string sFeesDesc = string.Empty;
			string sPermitCode = string.Empty;
			string sFeesCategory = string.Empty;
			double dAmt = 0;
			double dOrigAmt = 0;
			bool isForApproval = false;

            res.Query = $"select count(*) from application_approval where  ARN = '{RecordFrm.m_sAN.Trim()}' and status = 'APPROVED'";
            int.TryParse(res.ExecuteScalar(), out iCnt);
            if (iCnt > 0)
                return false;

            res.Query = $"DELETE FROM APPLICATION_APPROVAL WHERE ARN = '{RecordFrm.m_sAN.Trim()}'";
            if (res.ExecuteNonQuery() == 0)
            { }
            res.Close();

			res.Query = $"select * from bill_tmp where arn = '{RecordFrm.m_sAN.Trim()}' and fees_category <> 'MAIN'";
			if(res.Execute())
				while(res.Read())
				{
					sFeesCode = res.GetString("fees_code");
					sFeesCategory = res.GetString("fees_category");
					sPermitCode = res.GetString("permit_code");
					sFeesDesc = GetFeesDesc(res.GetString("fees_code"), sFeesCategory);
					dAmt = res.GetDouble("fees_amt");
					dOrigAmt = res.GetDouble("orig_amt");

					if (sFeesCategory == "ADDITIONAL" && dAmt != 0)
					{
						res2.Query = "INSERT INTO APPLICATION_APPROVAL VALUES(";
						res2.Query += $"'{RecordFrm.m_sAN.Trim()}', ";
						res2.Query += $"'{sFeesCode}', ";
						res2.Query += $"'{sFeesDesc}', ";
						res2.Query += $"'{sFeesCategory}', ";
						res2.Query += $"'{sPermitCode}', ";
						res2.Query += $"{dAmt}, ";
						res2.Query += $"{dOrigAmt}, ";
						res2.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
						res2.Query += $"'PENDING', ";
						res2.Query += $"null) ";
                        if (res2.ExecuteNonQuery() == 0)
						{ }
						res2.Close();
						isForApproval = true;
					}

					if(sFeesCategory == "OTHERS")
					{
						if (sFeesDesc.Contains("SURCHARGE") && dAmt < dOrigAmt)
						{
							res2.Query = "INSERT INTO APPLICATION_APPROVAL VALUES(";
							res2.Query += $"'{RecordFrm.m_sAN.Trim()}', ";
							res2.Query += $"'{sFeesCode}', ";
							res2.Query += $"'{sFeesDesc}', ";
							res2.Query += $"'{sFeesCategory}', ";
							res2.Query += $"'{sPermitCode}', ";
							res2.Query += $"{dAmt}, ";
							res2.Query += $"{dOrigAmt}, ";
							res2.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
							res2.Query += $"'PENDING', ";
                            res2.Query += $"null) ";
                            if (res2.ExecuteNonQuery() == 0)
							{ }
							res2.Close();
							isForApproval = true;
						}
						else if (dAmt != dOrigAmt)
						{
							res2.Query = "INSERT INTO APPLICATION_APPROVAL VALUES(";
							res2.Query += $"'{RecordFrm.m_sAN.Trim()}', ";
							res2.Query += $"'{sFeesCode}', ";
							res2.Query += $"'{sFeesDesc}', ";
							res2.Query += $"'{sFeesCategory}', ";
							res2.Query += $"'{sPermitCode}', ";
							res2.Query += $"{dAmt}, ";
							res2.Query += $"{dOrigAmt}, ";
							res2.Query += $"to_date('{AppSettingsManager.GetSystemDate().ToShortDateString()}','MM/dd/yyyy'), ";
							res2.Query += $"'PENDING', ";
                            res2.Query += $"null) ";
                            if (res2.ExecuteNonQuery() == 0)
							{ }
							res2.Close();
							isForApproval = true;
						}
					}
				}
			return isForApproval;
		}
	}
}
