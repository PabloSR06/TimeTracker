using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Time;

namespace timeTrakerApi.Data.Interfaces
{
    public interface ITimeRepository
    {
        List<DayHoursModel> GetDayHoursByUserId(string userId);
        List<DayHoursModel> GetDayHours(HourInputModel input, string userId);
        List<HoursProjectModel> GetProjectHours(HourInputModel input, string userId);
        bool InsertProjectHours(ProjectTimeInputModel input, string userId);
        bool InsertDayHours(DayInputModel input, string userId);


    }
}
