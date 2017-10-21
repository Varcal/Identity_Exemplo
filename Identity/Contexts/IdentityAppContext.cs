using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Identity.Mappings;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            

            

            IdentityConfig(modelBuilder);
        }

        private static void IdentityConfig(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new RoleMap());

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole", "AspNetIdentity");
            modelBuilder.Entity<IdentityUserRole>().HasKey(x => new {x.UserId, x.RoleId});

            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim", "AspNetIdentity");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserClaim>().Property(x => x.ClaimType)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            modelBuilder.Entity<IdentityUserClaim>().Property(x => x.ClaimValue)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin", "AspNetIdentity");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(x => new {x.UserId, x.ProviderKey, x.LoginProvider});
        }

        public static IdentityAppContext Create()
        {
            return new IdentityAppContext();
        }

    
    }
}
