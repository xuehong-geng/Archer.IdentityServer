using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Mvc;

namespace System.Web.Mvc
{
    public static class ControllerExtensions
    {
        public static HttpStatusCodeResult AccessDenied(this Controller controller)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            return new AccessDeniedResult(controller.Request);
        }
    }
}
