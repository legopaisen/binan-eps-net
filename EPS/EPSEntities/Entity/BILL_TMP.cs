﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Entity
{
    public class BILL_TMP
    {
        public string ARN { get; set; }
        public string FEES_CODE { get; set; }
        public string FEES_UNIT { get; set; }
        public float FEES_UNIT_VALUE { get; set; }
        public float FEES_AMT { get; set; }
        public string PERMIT_CODE { get; set; }
        public string USER_CODE { get; set; }
        public float ORIG_AMT { get; set; }
        public string FEES_CATEGORY { get; set; }
    }
}
