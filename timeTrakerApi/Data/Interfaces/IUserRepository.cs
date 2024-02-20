using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IUserRepository
    {
        BasicUserModel? GetById(int id);
        bool Insert(UserModel project);
        bool Delete(int id);
        bool ForgotPassword(string email);
        bool UpdatePassword(ResetPasswordModel userCredential, int userId);
        UserProfileModel GetUserLogIn(UserCredentialsModel input);
    }
}
