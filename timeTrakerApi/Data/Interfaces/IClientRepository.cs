using timeTrakerApi.Models.Client;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IClientRepository
    {
        List<BasicClientModel> Get();
        BasicClientModel? GetById(int id);
        bool Insert(BasicClientModel project);
        bool Update(BasicClientModel input);
        bool Delete(int id);
    }
}
