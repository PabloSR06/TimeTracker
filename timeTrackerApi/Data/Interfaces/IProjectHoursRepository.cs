using timeTrackerApi.Models.Project;

namespace timeTrackerApi.Data.Interfaces
{
    public interface IProjectHoursRepository
    {
        List<ProjectHoursModel> GetProjectHours();
    }
}
