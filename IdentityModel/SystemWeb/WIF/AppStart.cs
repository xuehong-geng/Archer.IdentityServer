using System.Web;
using Thinktecture.IdentityModel.SystemWeb;

[assembly:PreApplicationStartMethod(typeof(AppStart), "Start")]

namespace Thinktecture.IdentityModel.SystemWeb
{
    public class AppStart
    {
        public static void Start()
        {
            SessionConfiguration.Start();
        }
    }
}
