/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System;

namespace Thinktecture.IdentityModel.WebApi.Authentication.Handler
{
    public class SessionTokenConfiguration
    {
        public TimeSpan DefaultTokenLifetime { get; set; }
        public string EndpointAddress { get; set; }
        public string HeaderName { get; set; }
        public string Scheme { get; set; }
        public string Audience { get; set; }
        public byte[] SigningKey { get; set; }
        public string IssuerName { get; set; }
        
        public SessionTokenConfiguration()
        {
            DefaultTokenLifetime = TimeSpan.FromHours(10);
            EndpointAddress = "/token";
            HeaderName = "Authorization";
            Scheme = "Session";
            Audience = "http://session.tt";
            IssuerName = "session issuer";
            SigningKey = CryptoRandom.CreateRandomKey(32);
        }
    }
}
