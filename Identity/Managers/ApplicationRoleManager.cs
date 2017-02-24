using Identity.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Identity.Managers
{
    public class ApplicationRoleManager: RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) 
            : base(store)
        {
        }       
    }
}
