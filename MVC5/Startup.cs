using Identity;
using IoC;
using Owin;

namespace MVC5
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IocConfig.Initialize();
            StartupIdentity.Configuration(app);
        }
    }
}