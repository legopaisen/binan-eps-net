using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class APPLICATION
    {
        public string ARN { get; set; }
        public string PROJ_DESC { get; set; }
        public string PERMIT_CODE { get; set; }
        public string PERMIT_NO { get; set; }
        public string STRUC_CODE { get; set; }
        public int BLDG_NO { get; set; }
        public string STATUS_CODE { get; set; }
        public string SCOPE_CODE { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string OCCUPANCY_CODE { get; set; }
        public string BNS_CODE { get; set; }
        public string PROJ_HSE_NO { get; set; }
        public string PROJ_LOT_NO { get; set; }
        public string PROJ_BLK_NO { get; set; }
        public string PROJ_ADDR { get; set; }
        public string PROJ_BRGY { get; set; }
        public string PROJ_CITY { get; set; }
        public string PROJ_PROV { get; set; }
        public string PROJ_ZIP { get; set; }
        public string PROJ_OWNER { get; set; }
        public string PROJ_LOT_OWNER { get; set; }
        public string OWN_TYPE { get; set; }
        public string ENGR_CODE { get; set; }
        public string ARCHITECT { get; set; }
        public Nullable<DateTime> PROP_START { get; set; }
        public Nullable<DateTime> PROP_COMPLETE { get; set; }
        public Nullable<DateTime> DATE_APPLIED { get; set; }
        public Nullable<DateTime> DATE_ISSUED { get; set; }
        public string MEMO { get; set; }
        public int MAIN_APPLICATION { get; set; }
        public string PROJ_VILL { get; set; }
    }
}
