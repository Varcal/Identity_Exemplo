using System.Data.Entity.ModelConfiguration;
using Identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Identity.Mappings
{
    public class ApplicationUserMap : EntityTypeConfiguration<AppUser>
    {
        public ApplicationUserMap()
        {
            ToTable("User", "AspNetIdentity");

            HasKey(x => x.Id);

            HasMany(x => x.Roles)
                .WithRequired()
                .HasForeignKey(x => x.RoleId);

            HasMany(x => x.Logins)
                .WithRequired()
                .HasForeignKey(x => x.UserId);

            HasMany(x => x.Claims)
                .WithRequired()
                .HasForeignKey(x => x.UserId);

            Property(x => x.Id)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();
            Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
            Property(x => x.EmailConfirmed)
                .HasColumnType("bit");

            Property(x => x.PasswordHash)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();
            Property(x => x.PhoneNumber)
                .HasMaxLength(20);
            Property(x => x.LockoutEndDateUtc)
                .HasColumnType("datetime2")
                .HasPrecision(3)
                .IsRequired();
            Property(x => x.UserName)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
        }
    }


    public class RoleMap : EntityTypeConfiguration<IdentityRole>
    {
        public RoleMap()
        {
            ToTable("Role", "AspNetIdentity");

            HasKey(x => x.Id);

            Property(p => p.Id)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();

            HasMany(x => x.Users)
                .WithRequired()
                .HasForeignKey(x => x.UserId);
        }
    }

    public class UserRoleMap : EntityTypeConfiguration<IdentityUserRole>
    {
        public UserRoleMap()
        {
            ToTable("UserRole", "AspNetIdentity");

            HasKey(x => new {x.UserId, x.RoleId});

            Property(p => p.RoleId)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();
            Property(p => p.UserId)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();
        }
    }

    public class UserClaimMap : EntityTypeConfiguration<IdentityUserClaim>
    {
        public UserClaimMap()
        {
            ToTable("UserClaim", "AspNetIdentity");

            HasKey(x => x.Id);

            Property(x=>x.UserId)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();

            Property(x => x.ClaimType)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            Property(x => x.ClaimValue)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

        }
    }

    public class UserLoginMap : EntityTypeConfiguration<IdentityUserLogin>
    {
        public UserLoginMap()
        {
            ToTable("UserLogin", "AspNetIdentity");

            HasKey(x => x.UserId);

            Property(x=>x.UserId)
                .HasColumnType("varchar")
                .HasMaxLength(64)
                .IsRequired();
            Property(x => x.LoginProvider)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
            Property(x => x.ProviderKey)
                .HasColumnType("varchar")
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}