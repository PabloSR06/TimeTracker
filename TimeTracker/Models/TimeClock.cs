using System;

namespace TimeTracker.Models
{
    public class TimeClock
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public TimeSpan? start_time { get; set; }
        public TimeSpan? finish_time { get; set; }
        public Boolean isFinish { get; set; }
        public DateTime date { get; set; }
    }

    public class ClockInModel
    {
        public int user_id { get; set; }
        public TimeSpan start_time { get; set; }
        public DateTime date { get; set; }

    }

    public class ClockOutModel
    {
        public int user_id { get; set; }
        public TimeSpan finish_time { get; set; }
        public DateTime date { get; set; }

    }

    public class CheckInTime
    {
        public TimeClock? time_table { get; set; }

        public List<ClockHistoryMin> clockHistories { get; set; }
        public Boolean isOpen { get; set; }
    }
}
