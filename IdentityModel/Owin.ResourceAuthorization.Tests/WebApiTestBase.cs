using Microsoft.Owin.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Owin.ResourceAuthorization.Tests
{
    public class WebApiTestBase : IDisposable
    {
        private readonly TestServer _server;

        public WebApiTestBase()
        {
            var authorizationManager = new DelegatedResourceAuthorizationManager(async c =>
                {
                    if (CheckAccessDelegate != null)
                        return await CheckAccessDelegate(c);

                    return await Task.FromResult(true);
                });

            _server = TestServer.Create(app =>
                {
                    var config = new HttpConfiguration();
                    config.MapHttpAttributeRoutes();

                    app.UseResourceAuthorization(authorizationManager);
                    app.UseWebApi(config);
                });
            Client = new HttpClient(_server.Handler) { BaseAddress = new Uri("http://testserver/") };
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

        public HttpClient Client { get; private set; }
        public Func<ResourceAuthorizationContext, Task<bool>> CheckAccessDelegate { get; set; }
    }
}