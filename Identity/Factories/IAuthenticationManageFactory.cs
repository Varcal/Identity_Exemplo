using Microsoft.Owin.Security;

namespace Identity.Factories
{
    public interface IAuthenticationManagerFactory
    {
        IAuthenticationManager Create();
    }
}
