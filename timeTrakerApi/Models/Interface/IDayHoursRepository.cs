using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data.Interface
{
    public interface IDayHoursRepository
    {
        List<DayHoursModel> GetDayHours();
        List<DayHoursModel> GetDayHoursByUserId(string userId);
    }
}
