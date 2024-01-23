using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface IProjectHoursRepository
    {
        List<ProjectHoursModel> GetProjectHours();
    }
}
