using Microsoft.Owin.Security;
using Thinktecture.IdentityModel.Hawk.Core;
using Thinktecture.IdentityModel.Hawk.Core.Helpers;

namespace Thinktecture.IdentityModel.Hawk.Owin
{
    public class HawkAuthenticationOptions : AuthenticationOptions
    {
        public HawkAuthenticationOptions(Options hawkOptions)
            : base(HawkConstants.Scheme)
        {
            this.HawkOptions = hawkOptions;
        }

        public Options HawkOptions { get; set; }
    }
}
