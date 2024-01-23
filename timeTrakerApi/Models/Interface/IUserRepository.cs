

using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface IUserRepository
    {
        List<UserModel> Get();
        UserModel GetById(string id);
        bool Insert(UserModel project);
        bool Delete(string id);
    }
}
