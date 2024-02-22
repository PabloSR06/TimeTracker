using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Time;

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
