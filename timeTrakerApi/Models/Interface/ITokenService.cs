using System.IdentityModel.Tokens.Jwt;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Data.Interface
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(UserProfileModel userProfile);
        JwtSecurityToken GenerateGuestToken(UserProfileModel userProfile);
    }
}
