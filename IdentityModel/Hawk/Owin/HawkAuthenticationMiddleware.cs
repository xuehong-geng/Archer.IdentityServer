using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Thinktecture.IdentityModel.Hawk.Owin
{
    public class HawkAuthenticationMiddleware : AuthenticationMiddleware<HawkAuthenticationOptions>
    {
        public HawkAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, HawkAuthenticationOptions options)
            : base(next, options)
        { }

        protected override AuthenticationHandler<HawkAuthenticationOptions> CreateHandler()
        {
            return new HawkAuthenticationHandler();
        }
    }
}
