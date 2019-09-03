using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class SCHEDULES
    {
        public string FEES_CODE { get; set; }
        public Nullable<float> QTY1 { get; set; }
        public Nullable<float> QTY2  { get; set; }
        public Nullable<float> RANGE1 { get; set; }
        public Nullable<float> RANGE2 { get; set; }
        public Nullable<float> RATE1  { get; set; }
        public Nullable<float> RATE2  { get; set; }
        public Nullable<float> AMOUNT1 { get; set; }
        public Nullable<float> AMOUNT2 { get; set; }
        public Nullable<float> RANGE_DESC { get; set; }
    }
}
