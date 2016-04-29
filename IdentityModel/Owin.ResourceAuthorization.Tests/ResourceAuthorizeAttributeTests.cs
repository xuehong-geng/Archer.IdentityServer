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
    [Trait("Resource Authorization Attribute", "Single action, single resource")]
    public class ResourceAuthorizeAttributeTests : WebApiTestBase
    {
        private ResourceAuthorizationContext _context;
        private readonly HttpResponseMessage _response;

        public class ProtectedTestsController : ApiController
        {
            [HttpGet, Route("api/protected")]
            [ResourceAuthorize("read", "protected")]
            public async Task<HttpResponseMessage> Protected()
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
        }

        public ResourceAuthorizeAttributeTests()
        {
            _context = null;

            CheckAccessDelegate = c =>
            {
                _context = c;

                return Task.FromResult(true);
            };

            _response = Client.GetAsync("/api/protected").Result;

        }

        [Fact(DisplayName = "Response should be success")]
        public void CheckResponse()
        {
            _response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact(DisplayName = "Context contains Action")]
        public void CheckAction()
        {
            _context.ActionNames().Should().Contain("read");
        }

        [Fact(DisplayName = "Context contains Resource")]
        public void CheckResource()
        {
            _context.ResourceNames().Should().Contain("protected");
        }
    }
}