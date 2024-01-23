

using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface IProjectRepository
    {
        List<ProjectModel> Get();
        ProjectModel GetById(string id);
    }
}
