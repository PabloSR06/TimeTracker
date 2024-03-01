using timeTrackerApi.Models.Day;
using timeTrackerApi.Models.Day.Input;
using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.Project.Input;

namespace timeTrackerApi.Data.Interfaces
{
    public interface IChronoRepository
    {
        List<DayHoursModel> GetDayHours(HourInputModel input, int userId);
        List<HoursProjectModel> GetProjectHours(HourInputModel input, int userId);
        bool InsertProjectHours(ProjectTimeInputModel input, int userId);
        bool InsertDayHours(DayInputModel input, int userId);


    }
}
