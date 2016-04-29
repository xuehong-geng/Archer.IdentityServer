/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see LICENSE
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.IO;
using System.Security.Claims;
using System.Xml;

namespace Thinktecture.IdentityModel.Tokens
{
    public class IdentityModelJwtSecurityTokenHandler : JwtSecurityTokenHandler
    {
        TokenValidationParameters _validationParams;

        public IdentityModelJwtSecurityTokenHandler()
            : base()
        { }

        public IdentityModelJwtSecurityTokenHandler(TokenValidationParameters validationParams, Dictionary<string, string> inboundClaimTypeMap = null)
        {
            _validationParams = validationParams;

            if (inboundClaimTypeMap != null)
            {
                InboundClaimTypeMap = inboundClaimTypeMap;
            }
        }

        public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
        {
            if (_validationParams != null)
            {
                var jwt = token as JwtSecurityToken;
                var validated = ValidateToken(jwt.RawData, _validationParams, out token);
                var list = new List<ClaimsIdentity>(validated.Identities);
                return list.AsReadOnly();
            }
            else
            {
                return base.ValidateToken(token);
            }
        }

        public override SecurityToken ReadToken(string jwtEncodedString)
        {
            if (jwtEncodedString.StartsWith("<"))
            {
                return base.ReadToken(new XmlTextReader(new StringReader(jwtEncodedString)));
            }

            return base.ReadToken(jwtEncodedString);
        }
    }
}