using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPTEntities.Connection
{
    public class RPTConnection:Entities
    {
        public RPTConnection(RPTConnectionString connectionString) : base(GetRPTConnectionString(connectionString))
        {
        }

        public static string GetRPTConnectionString(RPTConnectionString connectionString)
        {
            return connectionString.GetRPTConnectionString();
        }
    }
}
