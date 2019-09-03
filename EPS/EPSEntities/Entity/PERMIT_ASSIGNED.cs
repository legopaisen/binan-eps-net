using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class PERMIT_ASSIGNED
    {
        public string PERMIT_CODE { get; set; }
        public string FR_PERMIT_NO { get; set; }
        public string CU_PERMIT_NO { get; set; }
        public DateTime ASSIGNED_DATE { get; set; }
        public string ASSIGNED_BY { get; set; }
    }
}
