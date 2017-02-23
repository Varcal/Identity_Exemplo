using System.Web;
using Microsoft.Owin.Security;

namespace Identity.Factories
{
    public class AuthenticationManageFactory: IAuthenticationManagerFactory
    {
        public IAuthenticationManager Create()
        {
            return HttpContext.Current.GetOwinContext().Authentication;
        }
    }
}
