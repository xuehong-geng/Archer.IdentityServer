using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Thinktecture.IdentityModel.Mvc
{
    public class HandleForbiddenAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        string viewName;
        public HandleForbiddenAttribute()
            : this("Forbidden")
        {
        }

        public HandleForbiddenAttribute(string viewName)
        {
            if (String.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentNullException("viewName");
            }
            
            this.viewName = viewName;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var statusCodeResult = filterContext.Result as HttpStatusCodeResult;
                if (statusCodeResult != null &&
                    statusCodeResult.StatusCode == 403)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = this.viewName
                    };
                }
            }
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var statusCodeResult = filterContext.Result as HttpStatusCodeResult;
                if (statusCodeResult != null &&
                    statusCodeResult.StatusCode == 403)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = this.viewName
                    };
                }
            }
        }
    }
}
