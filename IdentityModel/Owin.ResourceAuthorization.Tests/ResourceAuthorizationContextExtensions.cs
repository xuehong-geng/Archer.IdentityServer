using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Owin.ResourceAuthorization.Tests
{
    public static class ResourceAuthorizationContextExtensions
    {
        public static bool HasClaim(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.Any(c => c.Type == claimType);
        }

        public static string ClaimValue(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.Single(c => c.Type == claimType).Value;
        }

        public static  IEnumerable<string> ClaimValues(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.Where(c => c.Type == claimType).Select(c => c.Value);
        }

        public static IEnumerable<string> NameClaims(this IEnumerable<Claim> claims)
        {
            return claims.ClaimValues("name");
        }

        public static IEnumerable<string> ActionNames(this ResourceAuthorizationContext context)
        {
            return context.Action.NameClaims();
        }

        public static IEnumerable<string> ResourceNames(this ResourceAuthorizationContext context)
        {
            return context.Resource.NameClaims();
        }
    }
}