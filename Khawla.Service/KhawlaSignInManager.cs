using Khawla.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Khawla.Service
{
    public class KhawlaSignInManager : SignInManager<KhawlaUser, string>
    {
        public KhawlaSignInManager(KhawlaUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(KhawlaUser user)
        {
            return user.GenerateUserIdentityAsync((KhawlaUserManager)UserManager);
        }

        public static KhawlaSignInManager Create(IdentityFactoryOptions<KhawlaSignInManager> options, IOwinContext context)
        {
            return new KhawlaSignInManager(context.GetUserManager<KhawlaUserManager>(), context.Authentication);
        }
    }
}
