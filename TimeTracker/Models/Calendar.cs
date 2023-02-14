using System.Globalization;


namespace TimeTracker.Models
{
    public class CalendarInput
    {
        public int Year;
        public int Month;
        public int WeekNumber;

        public int Day;

        public CalendarInput()
        {
            var date = DateTime.Today;
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
            WeekNumber = (int)new DateTime(Year, Month, 1).DayOfWeek;
        }

        public CalendarInput(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
            WeekNumber = (int)new DateTime(Year, Month, 1).DayOfWeek;
        }

        public DateOnly ToDate()
        {
            if (Day == 0)
            {
                Day= DateTime.Today.Day;
            }
            return new DateOnly(Year, Month, Day);

        }

    }
}
