using System;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;

namespace Owin.ResourceAuthorization.Tests
{
    public class DelegatedResourceAuthorizationManager : ResourceAuthorizationManager
    {
        private readonly Func<ResourceAuthorizationContext, Task<bool>> _checkAccessFunc;

        public DelegatedResourceAuthorizationManager(Func<ResourceAuthorizationContext, Task<bool>>  checkAccessFunc)
        {
            _checkAccessFunc = checkAccessFunc;
        }

        public override async Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            return await _checkAccessFunc(context);
        }
    }
}