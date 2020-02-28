using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPSEntities.Connection;
using System.Data;

namespace Modules.Reports
{
    public class FormReportClass
    {
        protected frmReport ReportForm = null;
        protected static ConnectionString dbConn = new ConnectionString();
        public string BillNo { get; set; }
        public string AN { get; set; }
        public DateTime dtTo { get; set; }
        public DateTime dtFrom { get; set; }

        
        public FormReportClass(frmReport Form)
        {
            this.ReportForm = Form;
        }

        public virtual void LoadForm()
        {

        }
    }
}
