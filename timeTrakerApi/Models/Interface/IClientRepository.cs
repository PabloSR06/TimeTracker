

using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface IClientRepository
    {
        List<ClientModel> Get();
        ClientModel GetById(string id);
        bool Insert(ClientModel project);
        bool Delete(string id);
    }
}
