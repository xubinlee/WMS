using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Utility;
using System.Collections;

namespace Factory
{
    class ConfigInfo
    {

        static string license = string.Empty;

        public static string License
        {
            get { return license; }
            set { license = value; }
        }

        private static string m_SqlConStr;

        public static string SqlConStr
        {
            get
            {
                //Hashtable macAddress = new Hashtable();
                //macAddress.Add("wqb", "F32AE69569ECD888683C258486A5F50045912FFBDA0D16AF");
                license = ConfigurationManager.AppSettings["License"];
                if (license.Equals(Security.Encrypt(SystemManagement.GetMacAddressByNetworkInformation())))
                {
                    m_SqlConStr = Security.Decrypt(ConfigurationManager.ConnectionStrings["ERPToysConnectionString"].ConnectionString);
                }
                return m_SqlConStr;
            }
        }
    }
}
