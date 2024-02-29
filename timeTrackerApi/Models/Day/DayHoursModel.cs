namespace timeTrackerApi.Models.Day
{
    public class DayHoursModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CreateOnDate { get; set; }
        public DateTime? LastModifiedOnDate { get; set; }
    }
}
