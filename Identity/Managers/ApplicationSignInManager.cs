using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Identity.Managers
{
    public class ApplicationSignInManager: SignInManager<AppUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) 
            : base(userManager, authenticationManager)
        {

        }
    }
}
