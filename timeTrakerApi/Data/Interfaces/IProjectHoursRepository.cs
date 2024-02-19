using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IProjectHoursRepository
    {
        List<ProjectHoursModel> GetProjectHours();
    }
}
