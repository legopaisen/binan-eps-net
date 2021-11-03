﻿using System;
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
    public class OtherMajorFeesList
    {
        public static ConnectionString dbConn = new ConnectionString();

        public OtherMajorFeesList()
        {
        }

        public static List<OTHER_MAJOR_FEES> GetOtherMajorFeesListd()
        {
            using (var db = new EPSConnection(dbConn))
            {
                string sQuery = string.Empty;
                sQuery = $"select * from other_major_fees order by fees_code";
                return db.Database.SqlQuery<OTHER_MAJOR_FEES>(sQuery).ToList();
            }
        }
    }
}
