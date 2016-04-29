/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using Thinktecture.IdentityModel.Owin;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder RequireSsl(this IAppBuilder app, bool requireClientCertificate = false)
        {
            app.Use(typeof(RequireSslMiddleware), new RequireSslOptions { RequireClientCertificate = requireClientCertificate });
            return app;
        }

        public static IAppBuilder RequireSsl(this IAppBuilder app, RequireSslOptions options)
        {
            app.Use(typeof(RequireSslMiddleware), options);
            return app;
        }
    }
}
