using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCSEntities.Connection
{
    public interface IARCSConnectionString
    {
        string GetARCSConnectionString();
        string GetARCSConnectionString(string model);
    }
}
