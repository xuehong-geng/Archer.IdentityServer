using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Thinktecture.IdentityModel.WebApi
{
    public class AccessDeniedResult : System.Web.Http.Results.StatusCodeResult
    {
        public AccessDeniedResult(HttpRequestMessage request)
            : base(GetCode(request), request)
        {
        }

        public static HttpStatusCode GetCode(HttpRequestMessage request)
        {
            var ctx = request.GetRequestContext();
            if (ctx.Principal != null &&
               ctx.Principal.Identity != null &&
               ctx.Principal.Identity.IsAuthenticated)
            {
                return HttpStatusCode.Forbidden;
            }
            else
            {
                return HttpStatusCode.Unauthorized;
            }
        }
    }
}
