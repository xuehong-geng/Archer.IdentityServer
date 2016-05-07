using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    public class IdentityServerConfigSection : ConfigurationSection
    {
        public static IdentityServerConfigSection Default
        {
            get
            {
                var cfg = ConfigurationManager.GetSection("IdentityServer") as IdentityServerConfigSection;
                if (cfg == null)
                    throw new ConfigurationErrorsException("Configuration section <IdentityServer> is missing!");
                return cfg;
            }
        }

        [ConfigurationProperty("SigninCertificate", IsRequired = true)]
        public CertificateConfigElement SigninCertificate
        {
            get { return (CertificateConfigElement) base["SigninCertificate"]; }
            set { base["SigninCertificate"] = value; }
        }
    }
}
