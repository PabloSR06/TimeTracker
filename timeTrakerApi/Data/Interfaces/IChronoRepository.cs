using timeTrakerApi.Models.Day;
using timeTrakerApi.Models.Day.Input;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Project.Input;

namespace timeTrakerApi.Data.Interfaces
{
    public interface IChronoRepository
    {
        List<DayHoursModel> GetDayHours(HourInputModel input, int userId);
        List<HoursProjectModel> GetProjectHours(HourInputModel input, int userId);
        bool InsertProjectHours(ProjectTimeInputModel input, int userId);
        bool InsertDayHours(DayInputModel input, int userId);


    }
}
