using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCSEntities.Connection
{
    public class ARCSConnection : ARCSEntities
    {
        public ARCSConnection(ARCSConnectionString connectionString) : base(GetARCSConnectionString(connectionString))
        {
        }

        public static string GetARCSConnectionString(ARCSConnectionString connectionString)
        {
            return connectionString.GetARCSConnectionString();
        }
    }
}
