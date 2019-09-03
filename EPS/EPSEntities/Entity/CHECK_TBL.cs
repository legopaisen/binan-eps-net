using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class CHECK_TBL
    {
        public string CATEGORY_CODE { get; set; }
        public string CHK_REF { get; set; }
        public DateTime CHK_DATE { get; set; }
        public string CHK_NO { get; set; }
        public string ACCT_NO { get; set; }
        public string ACCT_LN { get; set; }
        public string ACCT_FN { get; set; }
        public string ACCT_MI { get; set; }
        public float CHK_AMT { get; set; }
        public DateTime DT_CREATE { get; set; }
        public string BANK_CODE { get; set; }
        public float DEBIT_CREDIT { get; set; }
    }
}
