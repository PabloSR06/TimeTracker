using timeTrackerApi.Models.Project;

namespace timeTrackerApi.Data.Interfaces
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
