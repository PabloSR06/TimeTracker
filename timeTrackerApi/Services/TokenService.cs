using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.User;
using timeTrackerApi.Services.Interfaces;

namespace timeTrackerApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateToken(UserProfileModel userProfile)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, userProfile.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userid", userProfile.Id.ToString())
            };

            claims.AddRange(userProfile.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: creds
            );

            return token;
        }

        public JwtSecurityToken GenerateGuestToken(BasicUserModel userProfile)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, userProfile.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Guest")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            return token;
        }


    }
}
