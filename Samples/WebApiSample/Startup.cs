using System;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiSample.Startup))]

namespace WebApiSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:10367",
                ValidationMode = ValidationMode.ValidationEndpoint,

                RequiredScopes = new[] {"api1"}
            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            //config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);
        }
    }
}
