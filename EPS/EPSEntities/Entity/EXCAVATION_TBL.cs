using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class EXCAVATION_TBL
    {
        public string ARN { get; set; }
        public string PROJ_DESC { get; set; }
        public string ACCT_CODE { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string HSE_NO { get; set; }
        public string LOT_NO { get; set; }
        public string BLK_NO { get; set; }
        public string ADDRESS { get; set; }
        public string BRGY { get; set; }
        public string CITY { get; set; }
        public string PROVINCE { get; set; }
        public DateTime DATE_APPLIED { get; set; }
        public string EXCAVATION_NO { get; set; }
        public DateTime DATE_ISSUED { get; set; }
    }
}
