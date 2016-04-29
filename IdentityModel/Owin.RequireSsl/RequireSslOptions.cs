/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System.IdentityModel.Selectors;
namespace Thinktecture.IdentityModel.Owin
{
    public class RequireSslOptions
    {
        public bool RequireClientCertificate { get; set; }
        public X509CertificateValidator ClientCertificateValidator { get; set; }

        public RequireSslOptions()
        {
            RequireClientCertificate = false;
            ClientCertificateValidator = X509CertificateValidator.None;
        }
    }
}