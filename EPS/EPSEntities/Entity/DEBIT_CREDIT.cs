using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class DEBIT_CREDIT
    {
        public string PAYER_CODE { get; set; }
        public Int32 SEQID { get; set; }
        public string OR_NO { get; set; }
        public DateTime OR_DATE { get; set; }
        public float DEBIT { get; set; }
        public float CREDIT { get; set; }
        public float BALANCE { get; set; }
        public string MEMO { get; set; }
        public string CHK_NO { get; set; }
        public DateTime DATE_POSTED { get; set; }
        public string TIME_POSTED { get; set; }
        public string TELLER_CODE { get; set; }
        public string SERVED { get; set; }

    }
}
