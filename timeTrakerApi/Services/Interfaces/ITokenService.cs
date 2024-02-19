using System.IdentityModel.Tokens.Jwt;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Services.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(UserProfileModel userProfile);
        JwtSecurityToken GenerateGuestToken(BasicUserModel userProfile);
    }
}
