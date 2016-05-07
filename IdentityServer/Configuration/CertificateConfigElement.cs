using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Configuration
{
    /// <summary>
    /// Configuration element for certificate
    /// </summary>
    public class CertificateConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("Store", IsRequired = false, DefaultValue = "My")]
        public string Store
        {
            get { return (string)base["Store"]; }
            set { base["Store"] = value; }
        }

        [ConfigurationProperty("Location", IsRequired = false, DefaultValue = "LocalMachine")]
        public string Location
        {
            get { return (string)base["Location"]; }
            set { base["Location"] = value; }
        }

        [ConfigurationProperty("Subject", IsRequired = true, DefaultValue = "localhost")]
        public string Subject
        {
            get { return (string)base["Subject"]; }
            set { base["Subject"] = value; }
        }

        public X509Certificate2 GetCertificate()
        {
            var sloc = StoreLocation.LocalMachine;
            if (Location.Equals("CurrentUser", StringComparison.OrdinalIgnoreCase))
                sloc = StoreLocation.CurrentUser;
            var store = new X509Store(Store, sloc);
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, Subject, false);
            if (certs.Count < 1)
                throw new ConfigurationErrorsException("The certificate configured is not exist in local store!");
            return certs[0];
        }
    }
}
