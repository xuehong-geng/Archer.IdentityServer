﻿using System.Web;

namespace Thinktecture.IdentityModel.SystemWeb
{
    public class SessionConfigurationModule : IHttpModule
    {
        static public bool CacheSessionsOnServer { get; set; }
        static public bool EnableSlidingSessionExpirations { get; set; }
        static public bool OverrideWSFedTokenLifetime { get; set; }
        static public bool SuppressLoginRedirectsForApiCalls { get; set; }
        static public bool SuppressSecurityTokenExceptions { get; set; }

        public void Init(HttpApplication application)
        {
            if (CacheSessionsOnServer)
            {
                PassiveModuleConfiguration.CacheSessionsOnServer();
            }

            if (EnableSlidingSessionExpirations)
            {
                PassiveModuleConfiguration.EnableSlidingSessionExpirations();
            }

            if (OverrideWSFedTokenLifetime)
            {
                PassiveModuleConfiguration.OverrideWSFedTokenLifetime();
            }

            if (SuppressLoginRedirectsForApiCalls)
            {
                PassiveModuleConfiguration.SuppressLoginRedirectsForApiCalls();
            }

            if (SuppressSecurityTokenExceptions)
            {
                PassiveModuleConfiguration.SuppressSecurityTokenExceptions();
            }
        }
        
        public void Dispose()
        {
        }
    }
}
