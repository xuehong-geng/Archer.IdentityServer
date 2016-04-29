using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Thinktecture.IdentityModel.Mvc
{
    public class AccessDeniedResult : HttpStatusCodeResult
    {
        public AccessDeniedResult()
            : base(HttpContext.Current.Request.IsAuthenticated ? 403 : 401)
        {
        }

        public AccessDeniedResult(HttpRequestBase request)
            : base(request.IsAuthenticated ? 403 : 401)
        {
        }
    }
}
