using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class TRAIL
    {
        public string USER_CODE { get; set; }
        public string MODULE_CODE { get; set; }
        public DateTime TRANS_DATE { get; set; }
        public string TRANS_TIME { get; set; }
        public string TABLE_FLD { get; set; }
        public string OBJECT_FLD { get; set; }
    }
}
