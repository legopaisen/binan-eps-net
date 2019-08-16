using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class BUILDING
    {
        public int BLDG_NO { get; set; }
        public string BLDG_NM { get; set; }
        public string LAND_PIN { get; set; }
        public float BLDG_HEIGHT { get; set; }
        public float TOTAL_FLR_AREA { get; set; }
        public float ANCILLARY_AREA { get; set; }
        public int NO_UNITS { get; set; }
        public int NO_STOREYS { get; set; }
        public string MATERIAL_CODE { get; set; }
        public float EST_COST { get; set; }
        public float ACTUAL_COST { get; set; }
        public Nullable<DateTime> DATE_COMPLETED { get; set; }
        public float ASS_VAL { get; set; }

    }
}
