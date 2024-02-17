using SendGrid.Helpers.Mail;
using System.IdentityModel.Tokens.Jwt;

namespace timeTrakerApi.Services.Interfaces
{
    public interface IMailService
    {
        void SendMail(string subject, EmailAddress to, JwtSecurityToken token);
        void SendForgotMail(string toMail, string toName, JwtSecurityToken token);
    }
}
