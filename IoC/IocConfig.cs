using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Identity.Contexts;
using Identity.Managers;
using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace IoC
{
    public class IocConfig
    {
        private static Container _container;

        public static void Initialize()
        {
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeRegisters(_container);
            _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

            _container.Verify();
        }

        private static void InitializeRegisters(Container container)
        {
            #region Identity
            container.Register<IdentityAppContext>(Lifestyle.Scoped);
            container.Register<IUserStore<AppUser>>(() => new UserStore<AppUser>(new IdentityAppContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(new IdentityAppContext()), Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register(() => container.IsVerifying() ? new OwinContext(new Dictionary<string, object>()).Authentication
                : HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            #endregion
        }
    }
}
