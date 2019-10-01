using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Reports.Model
{
    public class BuildingInformation
    {

        public string OwnerName { get; set; }
        public string ProjDesc { get; set; }
        public string ProjLocation { get; set; }
        public string BldgMaterial { get; set; }
        public decimal BldgHeight{ get; set; }
        public decimal Area { get; set; }
        public decimal EstiCost { get; set; }
        public decimal ActualCost { get; set; }
        public string BldgPermitNo { get; set; }
        public DateTime DtIssued { get; set; }
        public string CertNo { get; set; }
        public DateTime? DtIssuedCert { get; set; }

    }
}
