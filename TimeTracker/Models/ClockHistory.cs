namespace TimeTracker.Models
{
    public class ClockHistory
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int TimeClock_id { get; set; }
        public int Minutes { get; set; }
        public string? Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
