using System;
using Identity.Contexts;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Identity.Managers
{
    public class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store) : base(store)
        {

        }

        public static ApplicationUserManager Create(IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<AppUser>(context.Get<IdentityAppContext>()));

            // Configurando validator para nome de usuario
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // Configuração de Lockout
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Providers de Two Factor Autentication
            manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<AppUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            manager.RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<AppUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            manager.EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            manager.SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("Varcal");
            var dataProtector = provider.Create("Identity_Exemplo");

            manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(dataProtector);

            return manager;
        }
    }
}
