using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modules.Utilities
{
    public class FormOwnerClass
    {
        protected frmOwner RecordFrm = null;

        public FormOwnerClass(frmOwner Form)
        {
            this.RecordFrm = Form;

        }

        public virtual void FormLoad()
        {
        }

        public virtual bool SearchAccount()
        {
            return false;
        }

        public virtual bool ValidateData()
        {
            return false;
        }

        public virtual void Save()
        { }

        public virtual void Delete()
        { }

        public virtual void CellClick(object sender, DataGridViewCellEventArgs e)
        { }
    }
}
