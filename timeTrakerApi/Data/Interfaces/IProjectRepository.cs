using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IProjectRepository
    {
        List<BasicProjectModel> Get();
        BasicProjectModel GetById(int id);
        bool Insert(BasicProjectModel project);
        bool Delete(int id);
        bool Update(BasicProjectModel input);
    }
}
