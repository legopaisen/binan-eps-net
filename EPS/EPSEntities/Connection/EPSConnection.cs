using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPSEntities.Connection
{
    public class EPSConnection:EPSEntities
    {
        public EPSConnection(ConnectionString connectionString) : base(GetConnectionString(connectionString))
        {
        }

        public static string GetConnectionString(ConnectionString connectionString)
        {
            return connectionString.GetConnectionString();
        }
    }
}
