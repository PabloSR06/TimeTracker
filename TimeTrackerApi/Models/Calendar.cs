using System.Globalization;


namespace TimeTrackerApi.Models
{
    public class CalendarInput
    {
        public int Year;
        public int Month;
        public int WeekNumber;

        public int Day;

        public DateOnly Today;

        public CalendarInput()
        {
            var date = DateTime.Today;
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
            WeekNumber = (int)new DateTime(Year, Month, 1).DayOfWeek +1;
            Today = new DateOnly(Year, Month, Day);
        }

        public CalendarInput(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
            WeekNumber = (int)new DateTime(Year, Month, 1).DayOfWeek +1;
            Today = new DateOnly(Year, Month, Day);
        }

        public DateOnly ToDate()
        {
            if (Day == 0)
            {
                Day = DateTime.Today.Day;
            }
            return new DateOnly(Year, Month, Day);

        }
        public DateOnly ToDate(int day)
        {

            return new DateOnly(Year, Month, day);

        }

        public bool IsToday(int day)
        {
            return Today.Equals(ToDate(day));
        }
        
        public bool IsSameMonth()
        {           
            return Today.Month == Month;
        }

        public bool IsSameYear()
        {
            return Today.Year == Year;
        }
    }
}
