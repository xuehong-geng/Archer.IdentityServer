using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Thinktecture.IdentityModel.Mvc
{
    public class ResourceAuthorizeAttribute : AuthorizeAttribute
    {
        private string _action;
        private string[] _resources;

        public ResourceAuthorizeAttribute()
        { }

        public ResourceAuthorizeAttribute(string action, params string[] resources)
        {
            _action = action;
            _resources = resources;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!string.IsNullOrWhiteSpace(_action))
            {
                return CheckAccess(httpContext, _action, _resources);
            }
            else
            {
                var controller = httpContext.Request.RequestContext.RouteData.Values["controller"] as string;
                var action = httpContext.Request.RequestContext.RouteData.Values["action"] as string;
                
                return CheckAccess(httpContext, action, controller);
            }
        }

        protected virtual bool CheckAccess(HttpContextBase httpContext, string action, params string[] resources)
        {
            var task = httpContext.CheckAccessAsync(action, resources);

            if (task.Wait(5000))
            {
                return task.Result;
            }
            else
            {
                throw new TimeoutException();
            }
        }

        public new string Roles
        {
            get
            {
                throw new NotSupportedException();
            }
            set 
            {
                throw new NotSupportedException();
            }
        }

        public new string Users 
        { 
            get 
            {
                throw new NotSupportedException();
            } 
            set 
            {
                throw new NotSupportedException();
            } 
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new AccessDeniedResult(filterContext.HttpContext.Request);
        }
    }
}