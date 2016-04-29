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
    [Trait("Resource Authorization Attribute", "Action parameters")]
    public class ResourceAuthorizationAttributeActionParametersTests : WebApiTestBase
    {
        private ResourceAuthorizationContext _context;
        private readonly HttpResponseMessage _response;

        public class ResourceAuthorizationAttributeActionParametersTestsController : ApiController
        {
            [HttpGet, Route("api/protected/{param1}/{param2}")]
            [ResourceAuthorize("read", "protected")]
            public async Task<HttpResponseMessage> Protected(string param1, string param2)
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
        }

        public ResourceAuthorizationAttributeActionParametersTests()
        {
            _context = null;

            CheckAccessDelegate = c =>
            {
                _context = c;

                return Task.FromResult(true);
            };

            _response = Client.GetAsync("/api/protected/firstvalue/secondvalue").Result;

        }

        [Fact(DisplayName = "Response should be success")]
        public void CheckResponse()
        {
            _response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact(DisplayName = "Context contains action parameters as resources")]
        public void CheckAction()
        {
            _context.Resource.HasClaim("param1").Should().BeTrue();
            _context.Resource.HasClaim("param2").Should().BeTrue();
        }

        [Fact(DisplayName = "Resource claim has action parameter value")]
        public void CheckResource()
        {
            _context.Resource.ClaimValue("param1").Should().Be("firstvalue");
            _context.Resource.ClaimValue("param2").Should().Be("secondvalue");
        }
    }
}
