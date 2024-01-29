using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Time;

namespace timeTrakerApi.Data.Interface
{
    public interface ITimeRepository
    {
        List<DayHoursModel> GetDayHoursByUserId(string userId);
        List<DayHoursModel> GetDayHours(HourInputModel input);
        List<HoursProjectModel> GetProjectHours(HourInputModel input);
        bool InsertProjectHours(ProjectTimeInputModel input);
        bool InsertDayHours(DayInputModel input);


    }
}
