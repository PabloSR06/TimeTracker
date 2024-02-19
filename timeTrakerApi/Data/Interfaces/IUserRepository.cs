using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IUserRepository
    {
        BasicUserModel? GetById(int id);
        bool Insert(UserModel project);
        bool Delete(string id);
        bool ForgotPassword(string email);
        bool ResetPassword(UserCredentialsModel userCredential, string userId);
        UserProfileModel GetUserLogIn(UserCredentialsModel input);
    }
}
