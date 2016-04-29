using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FluentAssertions;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;
using Thinktecture.IdentityModel.WebApi;
using Xunit;

namespace Owin.ResourceAuthorization.Tests
{
    [Trait("Resource Authorization Attribute", "Controller & Action")]
    public class ResourceAuthorizeAttributeControllerAndActionTests : WebApiTestBase
    {
        private ResourceAuthorizationContext _context;
        private readonly HttpResponseMessage _response;

        public class ResourceAuthorizeAttributeTestsController : ApiController
        {
            [HttpGet, Route("api/default")]
            [ResourceAuthorize("read", "protected")]
            public async Task<HttpResponseMessage> Protected()
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
        }

        public ResourceAuthorizeAttributeControllerAndActionTests()
        {
            _context = null;

            CheckAccessDelegate = c =>
            {
                _context = c;

                return Task.FromResult(true);
            };

            _response = Client.GetAsync("/api/default").Result;

        }

        [Fact(DisplayName = "Response should be success")]
        public void CheckResponse()
        {
            _response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact(DisplayName = "Context action method")]
        public void CheckAction()
        {
            _context.Action.ClaimValue("action").Should().Be("Protected");
        }

        [Fact(DisplayName = "Context contains controller")]
        public void CheckResource()
        {
            _context.Resource.ClaimValue("controller").Should().Be("ResourceAuthorizeAttributeTests");            
        }
    }
}
