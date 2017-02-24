using System;
using System.Threading.Tasks;
using Identity.Managers;
using Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using Microsoft.Owin.Security.Google;

namespace Identity
{
    public class StartupIdentity
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public static void Configuration(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            ConfigureAuth(app);
        }

        private static void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, AppUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            var goo = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = @"214177050910-a6n6876ffteg024lasuf85d1c584hjrf.apps.googleusercontent.com",
                ClientSecret = @"ksQGWc7AfmY8j2NSMGJqrJvF"
            };

            goo.Scope.Add("email");
            goo.Scope.Add("publish_actions");
            goo.Scope.Add("basic_info");

            goo.Provider = new GoogleOAuth2AuthenticationProvider()
            {

                OnAuthenticated = (context) =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:access_token", context.AccessToken, "goo", "Google"));
                    foreach (var x in context.User)
                    {
                        var claimType = $"urn:google:{x.Key}";
                        string claimValue = x.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "goo", "Google"));

                    }
                    return Task.FromResult(0);
                }
            };

            goo.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseGoogleAuthentication(goo);
        }
    }
}
