using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using Identity.Contexts;
using Identity.Models;
using Microsoft.AspNet.Identity;

namespace Identity.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            IdentityAppContext ctx = new IdentityAppContext();

            ctx.Users.Attach(new AppUser());
               


            // Habilitar o envio de e-mail
            if (true)
            {
                const string smtp = "mail.varcalsys.com.br";
                var email = ConfigurationManager.AppSettings["ContaDeEmail"];
                var senha = ConfigurationManager.AppSettings["SenhaEmail"];
                //var text = HttpUtility.HtmlEncode(message.Body);

                var msg = new MailMessage {From = new MailAddress("cleber.varcal@varcalsys.com.br", "VarçalSys")};

                msg.To.Add(new MailAddress(message.Destination));
                msg.Subject = message.Subject;
                msg.IsBodyHtml = true;
                msg.Body = message.Body;
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

                using (var smtpClient = new SmtpClient(smtp, Convert.ToInt32(587)))
                {
                    var credentials = new NetworkCredential(email, senha);
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(msg);
                }
            }

            return Task.FromResult(0);
        }
    }
}