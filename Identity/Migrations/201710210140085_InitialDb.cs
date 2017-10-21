namespace Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AspNetIdentity.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 64, unicode: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AspNetIdentity.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 64, unicode: false),
                        RoleId = c.String(nullable: false, maxLength: 64, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("AspNetIdentity.Role", t => t.UserId)
                .ForeignKey("AspNetIdentity.User", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "AspNetIdentity.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 64, unicode: false),
                        Email = c.String(nullable: false, maxLength: 150, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(nullable: false, maxLength: 64, unicode: false),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 20),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(nullable: false, precision: 3, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AspNetIdentity.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 64, unicode: false),
                        ClaimType = c.String(maxLength: 50, unicode: false),
                        ClaimValue = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AspNetIdentity.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "AspNetIdentity.UserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 64, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProviderKey, t.LoginProvider })
                .ForeignKey("AspNetIdentity.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AspNetIdentity.UserRole", "RoleId", "AspNetIdentity.User");
            DropForeignKey("AspNetIdentity.UserLogin", "UserId", "AspNetIdentity.User");
            DropForeignKey("AspNetIdentity.UserClaim", "UserId", "AspNetIdentity.User");
            DropForeignKey("AspNetIdentity.UserRole", "UserId", "AspNetIdentity.Role");
            DropIndex("AspNetIdentity.UserLogin", new[] { "UserId" });
            DropIndex("AspNetIdentity.UserClaim", new[] { "UserId" });
            DropIndex("AspNetIdentity.UserRole", new[] { "RoleId" });
            DropIndex("AspNetIdentity.UserRole", new[] { "UserId" });
            DropTable("AspNetIdentity.UserLogin");
            DropTable("AspNetIdentity.UserClaim");
            DropTable("AspNetIdentity.User");
            DropTable("AspNetIdentity.UserRole");
            DropTable("AspNetIdentity.Role");
        }
    }
}
