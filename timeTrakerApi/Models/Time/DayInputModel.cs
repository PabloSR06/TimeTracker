namespace timeTrakerApi.Models.Time
{
    public class DayInputModel
    {
        public int UserId { get; set; }
        public bool Type { get; set; }
        public DateTime Date { get; set; }
    }
}
