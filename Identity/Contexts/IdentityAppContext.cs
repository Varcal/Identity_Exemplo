using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity.Contexts
{
    public class IdentityAppContext: IdentityDbContext<AppUser>
    {
        public IdentityAppContext()
            :base("Name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(256));
        }

        public static IdentityAppContext Create()
        {
            return new IdentityAppContext();
        }
    }
}
