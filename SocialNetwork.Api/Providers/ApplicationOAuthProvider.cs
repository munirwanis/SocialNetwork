using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SocialNetwork.Api.App_Start;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user != null)
            {
                var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
                AuthenticationProperties properties = CreateProperties(user);
                var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            }
            else
            {
                context.SetError("invalid_grant", "The UserName or Password is incorrect");
            }
        }

        private AuthenticationProperties CreateProperties(ApplicationUser user)
        {
            var data = new Dictionary<string, string>
            {
                {"UserName", user.UserName }
            };
            return new AuthenticationProperties(data);
        }
    }
}