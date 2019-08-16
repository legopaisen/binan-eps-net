using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class BILLING
    {
        public string ARN { get; set; }
        public string STATUS_CODE { get; set; }
        public string BILL_NO { get; set; }
        public Nullable<DateTime> BILL_DATE { get; set; }
        public string BILL_BY { get; set; }
        public Nullable<DateTime> RECEIVED_DATE { get; set; }
        public string RECEIVED_BY { get; set; }
    }
}
