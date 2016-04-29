/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

namespace Thinktecture.IdentityModel.WebApi.Authentication.Handler
{
    public enum ClientCertificateMode
    {
        ChainValidation,
        PeerValidation,
        ChainValidationWithIssuerSubjectName,
        ChainValidationWithIssuerThumbprint,
        IssuerThumbprint
    }
}
