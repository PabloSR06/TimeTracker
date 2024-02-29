using SendGrid;
using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;
using timeTrackerApi.Services.Interfaces;

namespace timeTrackerApi.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendForgotMail(string toMail, string toName, JwtSecurityToken token)
        {
            var apiKey = _configuration["SendGrid:APIkey"];
            SendGridClient client = new SendGridClient(apiKey);

            EmailAddress to = new EmailAddress(toMail, toName);

            //TODO: Add a localized string
            string subject = "Forgot Password";

            //TODO: Add content


            SendMail(subject, to, token);
        }

        public async void SendMail(string subject, EmailAddress to, JwtSecurityToken token)
        {
            var apiKey = _configuration["SendGrid:APIkey"];
            SendGridClient client = new SendGridClient(apiKey);
            EmailAddress from = new EmailAddress(_configuration["SendGrid:DefaultFrom"], _configuration["SendGrid:DefaultFromName"]);

            string plainTextContent = "http://localhost:5173/password/change/"+token;
            var htmlContent = "http://localhost:5173/password/change/"+ new JwtSecurityTokenHandler().WriteToken(token);

            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);
        }
    }
}
