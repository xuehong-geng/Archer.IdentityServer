using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;
using System.Web.Http;
using IdentityServer.Components;
using IdentityServer.Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityServer
{
    class IdentityServer
    {
        private static IdentityServer _default = new IdentityServer();

        public static IdentityServer Default
        {
            get { return _default; }
        }

        private IDisposable _app = null;

        /// <summary>
        /// Start certification publisher
        /// </summary>
        public void Start()
        {
            try
            {
                var baseUrl = ConfigurationManager.AppSettings["baseUrl"];
                if (String.IsNullOrEmpty(baseUrl))
                {
                    throw new ConfigurationErrorsException("AppSetting baseUrl is missing!");
                }
                _app = WebApp.Start<Startup>(url: baseUrl);
            }
            catch (Exception err)
            {
                Trace.TraceError("Exception thrown during start WebApp: {0}", err.Message);
                throw;
            }
            Trace.TraceInformation("Identity Server started.");
        }

        /// <summary>
        /// Stop certification publisher
        /// </summary>
        public void Stop()
        {
            if (_app != null)
            {
                _app.Dispose();
            }
            Trace.TraceInformation("Identity Server stopped.");
        }
    }

    /// <summary>
    /// WebApi application starter
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            appBuilder.UseWebApi(config);

            // Configure identity server
            var options = new IdentityServerOptions
            {
                SiteName = "Archer Identity Server",
                SigningCertificate = LoadCertificate(),

                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(IdentityServer3.Core.Models.StandardScopes.All)
                    .UseInMemoryUsers(Users.Get()),
                RequireSsl = false
            };
            appBuilder.UseIdentityServer(options);
        }

        X509Certificate2 LoadCertificate()
        {
            //return new X509Certificate2(
            //    string.Format(@"{0}\bin\identityServer\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
            var cfg = IdentityServerConfigSection.Default.SigninCertificate;
            return cfg.GetCertificate();
        }
    }
}
