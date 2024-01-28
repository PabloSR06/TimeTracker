using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface ITimeRepository
    {
        List<DayHoursModel> GetDayHoursByUserId(string userId);
        List<DayHoursModel> GetDayHours(HourInputModel input);
        List<ProjectHoursModel> GetProjectHours(HourInputModel input);


    }
}
