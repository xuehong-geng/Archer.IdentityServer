using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace Owin.ResourceAuthorization.Tests
{
    [Trait("WebApi & Owin", "Selfhosted testing")]
    public class WebApiPingTest : WebApiTestBase
    {
        public class WebApiUnderTestController : ApiController
        {
            [HttpGet, Route("api/ping")]
            public async Task<string> Ping()
            {
                return await Task.FromResult("pong");
            }
        }

        [Fact(DisplayName = "WebApi on Owin up and running")]
        public async Task WebApiOnOwinUpAndRuning()
        {
            var response = await Client.GetAsync("api/ping");
            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal("\"pong\"", result);
        }
    }
}