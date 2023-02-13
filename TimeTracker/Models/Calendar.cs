using System.Globalization;


namespace TimeTracker.Models
{
    public class CalendarInput
    {
        public int Year;
        public int Month;
        public int WeekNumber;

        public int Day;

        public DateOnly ToDate()
        {
            if (Day == 0)
            {
                Day= 1;
            }
            return new DateOnly(Year, Month, Day);

        }
    }
}
