using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using System.Data;
using Common.AppSettings;
using Microsoft.Reporting.WinForms;
using Modules.Records;
using Modules.Utilities;
using EPSEntities.Entity;
using Modules.Reports.Model;
using Common.DataConnector;
using System.Collections.ObjectModel;

namespace Modules.Reports.Model
{
    public class CertificateMODEL
    {
        public string BldgOwner { get; set; }
        public string BldgAdress { get; set; }
        public DateTime DtIssued { get; set; }
        public string EngrOfficial { get; set; }
        public Decimal AmtPaid { get; set; }
        public DateTime ORDate { get; set; }
    }
}
