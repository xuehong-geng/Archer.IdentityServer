/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see LICENSE
 */

using System.Security.Claims;
using System.Web.Mvc;

namespace Thinktecture.IdentityModel.SystemWeb.Mvc
{
    public class ResourceActionAuthorizeAttribute : AuthorizeAttribute
    {
        private string _action;
        private string[] _resources;

        private const string _label = "Thinktecture.IdentityModel.Authorization.Mvc.ClaimsAuthorizeAttribute";

        public ResourceActionAuthorizeAttribute()
        { }

        public ResourceActionAuthorizeAttribute(string action, params string[] resources)
        {
            _action = action;
            _resources = resources;
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Items[_label] = filterContext;
            base.OnAuthorization(filterContext); 
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var principal = httpContext.User as ClaimsPrincipal;

            if (principal == null || principal.Identity == null)
            {
                principal = Principal.Anonymous;
            }

            if (!string.IsNullOrWhiteSpace(_action))
            {
                return ClaimsAuthorization.CheckAccess(principal, _action, _resources);
            }
            else
            {
                var filterContext = httpContext.Items[_label] as System.Web.Mvc.AuthorizationContext;
                return CheckAccess(principal, filterContext);
            }
        }

        protected virtual bool CheckAccess(ClaimsPrincipal principal, System.Web.Mvc.AuthorizationContext filterContext)
        {
            var action = filterContext.RouteData.Values["action"] as string;
            var controller = filterContext.RouteData.Values["controller"] as string;

            return ClaimsAuthorization.CheckAccess(principal, action, controller);
        }
    }
}
