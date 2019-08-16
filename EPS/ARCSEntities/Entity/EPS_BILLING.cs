using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCSEntities.Entity
{
    public class EPS_BILLING
    {
        public string ARN { get; set; }
        public string ACCT_CODE { get; set; }
        public string MRS_ACCT_CODE { get; set; }
        public string ACCT_LN { get; set; }
        public string ACCT_FN { get; set; }
        public string ACCT_MI { get; set; }
        public string ACCT_HOUSE_NO { get; set; }
        public string ACCT_STREET { get; set; }
        public string ACCT_BRGY { get; set; }
        public string ACCT_MUN { get; set; }
        public string ACCT_PROV { get; set; }
        public string ACCT_ZIP { get; set; }
        public string BILL_NO { get; set; }
        public string FEES_CODE { get; set; }
        public float FEES_AMT { get; set; }
    }
}
