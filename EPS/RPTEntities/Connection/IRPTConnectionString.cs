using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPTEntities.Connection
{
    public interface IRPTConnectionString
    {
        string GetRPTConnectionString();
        string GetRPTConnectionString(string model);
    }
}
