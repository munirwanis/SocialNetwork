using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SocialNetwork.Api.Models;

namespace SocialNetwork.Api.App_Start
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var dbContext = context.Get<ApplicationDbContext>();
            var userStore = new UserStore<ApplicationUser>(dbContext);
            var manager = new ApplicationUserManager(userStore);

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {

            };

            manager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 6,
                RequireLowercase = true,
                RequireNonLetterOrDigit = true,
                RequireUppercase = true

            };

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}