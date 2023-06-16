using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace BloodBankAPI.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMail(string emailAddress, string subject, string body, IConfiguration configuration)
        {
            try
            {

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(
                     configuration.GetSection("CompanyEmail:Email").Value ?? throw new ArgumentNullException()
                    ));
                email.To.Add(MailboxAddress.Parse(emailAddress));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "<p>" + body + "</p>"
                    //"<a href=\"https://www.google.com\">"+body+"</a>"
                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(
                     configuration.GetSection("CompanyEmail:Email").Value ?? throw new ArgumentNullException(),
                     configuration.GetSection("CompanyEmail:Password").Value ?? throw new ArgumentNullException()
                     );
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;

            }
            catch (Exception) //shuttleasy2spawn atıyo amk düzelt
            {
                return false;
            }


        }
    }
}
