using Khawla.Data;
using Khawla.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Service
{
    public class KhawlaUserManager : UserManager<KhawlaUser>
    {
        public KhawlaUserManager(IUserStore<KhawlaUser> store)
            : base(store)
        {
        }

        public static KhawlaUserManager Create(IdentityFactoryOptions<KhawlaUserManager> options, IOwinContext context)
        {
            var manager = new KhawlaUserManager(new UserStore<KhawlaUser>(context.Get<KhawlaDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<KhawlaUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<KhawlaUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<KhawlaUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<KhawlaUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
