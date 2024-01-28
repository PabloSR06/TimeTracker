using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface IDayHoursRepository
    {
        List<DayHoursModel> GetDayHoursByUserId(string userId);
        List<DayHoursModel> GetDayHours(HourInputModel input);
    }
}
