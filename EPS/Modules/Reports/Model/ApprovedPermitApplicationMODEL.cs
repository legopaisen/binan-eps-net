using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Reports.Model
{
    public class ApprovedPermitApplicationMODEL
    {
        public string ProjOwner { get; set; }
        public string ProjDesc { get; set; }
        public string ProjAddr { get; set; }
        public string ORNo { get; set; }
        public decimal AmtPaid { get; set; }
        public decimal ProjCost { get; set; }
        public string BldgPermitNo { get; set; }
        public DateTime DtIssued { get; set; }
        public DateTime DtApplied { get; set; }
    }
}
