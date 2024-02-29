using System.IdentityModel.Tokens.Jwt;
using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.User;

namespace timeTrackerApi.Services.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateToken(UserProfileModel userProfile);
        JwtSecurityToken GenerateGuestToken(BasicUserModel userProfile);
    }
}
