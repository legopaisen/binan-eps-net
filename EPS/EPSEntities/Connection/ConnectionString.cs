using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.EncryptUtilities;
using System.Configuration;

namespace EPSEntities.Connection
{
    public class ConnectionString: IConnectionString
    {
        public ConnectionString()
        {
        }

        public string GetConnectionString()
        {
            return this.GetConnectionString("Model1"); // This is the .edmx Name
        }

        public string GetConnectionString(string model)
        {
            Encryption encrypt = new Encryption();
            string strHost = string.Empty; string strPassword = string.Empty;
            if (ConfigurationManager.AppSettings.Get("Host") != string.Empty)
                strHost = ConfigurationManager.AppSettings.Get("Host");
            string strService = string.Empty;
            if (ConfigurationManager.AppSettings.Get("ServiceName") != string.Empty)
                strService = ConfigurationManager.AppSettings.Get("ServiceName");
            string strUser = string.Empty;
            if (ConfigurationManager.AppSettings.Get("UserId") != string.Empty)
                strUser = ConfigurationManager.AppSettings.Get("UserId");
            if (ConfigurationManager.AppSettings.Get("Password") != string.Empty)
                strPassword = ConfigurationManager.AppSettings.Get("Password");
            string strDecryptedPassword = string.Empty;
            try
            {
                strDecryptedPassword = encrypt.DecryptString(strPassword);
            }
            catch { }

            return string.Format(@"metadata = res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=Oracle.ManagedDataAccess.Client;provider connection string=""DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={1})(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));USER ID={3};PASSWORD={4}""", model, strHost, strService, strUser, strDecryptedPassword);
        }
    }
}
