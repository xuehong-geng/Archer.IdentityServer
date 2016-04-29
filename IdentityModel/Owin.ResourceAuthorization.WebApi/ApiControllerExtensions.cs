using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.WebApi;

namespace System.Web.Http
{
    public static class ApiControllerExtensions
    {
        public static IHttpActionResult AccessDenied(this ApiController controller)
        {
            if (controller == null) throw new ArgumentNullException("controller");

            return new AccessDeniedResult(controller.Request);
        }
    }
}
