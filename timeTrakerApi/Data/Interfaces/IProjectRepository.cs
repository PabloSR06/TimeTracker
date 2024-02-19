using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IProjectRepository
    {
        List<ProjectModel> Get();
        ProjectModel GetById(int id);
        bool Insert(ProjectModel project);
        bool Delete(string id);
    }
}
