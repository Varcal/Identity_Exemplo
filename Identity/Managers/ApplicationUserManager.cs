using System;
using Identity.Contexts;
using Identity.Managers.Services;
using Identity.Models;
using Identity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Identity.Managers
{
    public sealed class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store) : base(store)
        {
            ConfigureUserManager();
        }

        private void ConfigureUserManager()
        {
            UserValidator = new UserValidator<AppUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<AppUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<AppUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            EmailService = new EmailService();
            SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("Identity_Exemplo");
            var dataProtector = provider.Create("UserToken");
            UserTokenProvider = new DataProtectorTokenProvider<AppUser, string>(dataProtector) as IUserTokenProvider<AppUser, string>;
        }
    }
}
