/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see LICENSE
 */

using System;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Thinktecture.IdentityModel.WebApi
{
    [Obsolete("We recommend using the version based on OWIN middleware instead")]
    public class ResourceActionAuthorizeAttribute : AuthorizeAttribute
    {
        private string _action;
        private string[] _resources;

        public ResourceActionAuthorizeAttribute()
        { }

        public ResourceActionAuthorizeAttribute(string action, params string[] resources)
        {
            _action = action; 
            _resources = resources;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var principal = actionContext.ControllerContext.RequestContext.Principal as ClaimsPrincipal;
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
                return CheckAccess(actionContext, principal);
            }
        }

        protected virtual bool CheckAccess(HttpActionContext actionContext, ClaimsPrincipal principal)
        {
            var action = actionContext.ActionDescriptor.ActionName;
            var resource = actionContext.ControllerContext.ControllerDescriptor.ControllerName;

            return ClaimsAuthorization.CheckAccess(
                principal,
                action,
                resource);
        }
    }
}
