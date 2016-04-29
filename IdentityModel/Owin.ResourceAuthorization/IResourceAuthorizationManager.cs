using System.Threading.Tasks;

namespace Thinktecture.IdentityModel.Owin.ResourceAuthorization
{
    public interface IResourceAuthorizationManager
    {
        Task<bool> CheckAccessAsync(ResourceAuthorizationContext context);
    }
}