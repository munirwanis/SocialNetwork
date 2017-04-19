using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SocialNetwork.Api.App_Start;
using SocialNetwork.Api.Models;
using SocialNetwork.Api.Providers;
using System;

namespace SocialNetwork.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        private void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                Provider = new ApplicationOAuthProvider(),
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}