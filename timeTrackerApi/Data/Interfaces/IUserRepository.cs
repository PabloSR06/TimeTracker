using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.User;

namespace timeTrackerApi.Data.Interfaces
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
