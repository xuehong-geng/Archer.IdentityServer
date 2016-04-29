using Microsoft.Owin.Extensions;
using Owin;

namespace Thinktecture.IdentityModel.Hawk.Owin.Extensions
{
    public static class HawkAuthenticationExtension
    {
        public static IAppBuilder UseHawkAuthentication(this IAppBuilder app, HawkAuthenticationOptions options)
        {
            app.Use(typeof(HawkAuthenticationMiddleware), app, options);
            app.UseStageMarker(PipelineStage.Authenticate);
            return app;
        }
    }
}
