using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.EncryptUtilities;
using System.Configuration;

namespace ARCSEntities.Connection
{
    public class ARCSConnectionString : IARCSConnectionString
    {
        public ARCSConnectionString()
        {
        }

        public string GetARCSConnectionString()
        {
            return this.GetARCSConnectionString("Model1"); // This is the .edmx Name
        }

        public string GetARCSConnectionString(string model)
        {
            Encryption encrypt = new Encryption();
            string strHost = string.Empty; string strPassword = string.Empty;
            if (ConfigurationManager.AppSettings.Get("ARCSHost") != string.Empty)
                strHost = ConfigurationManager.AppSettings.Get("ARCSHost");
            string strService = string.Empty;
            if (ConfigurationManager.AppSettings.Get("ARCSServiceName") != string.Empty)
                strService = ConfigurationManager.AppSettings.Get("ARCSServiceName");
            string strUser = string.Empty;
            if (ConfigurationManager.AppSettings.Get("ARCSUserId") != string.Empty)
                strUser = ConfigurationManager.AppSettings.Get("ARCSUserId");
            if (ConfigurationManager.AppSettings.Get("ARCSPassword") != string.Empty)
                strPassword = ConfigurationManager.AppSettings.Get("ARCSPassword");
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
