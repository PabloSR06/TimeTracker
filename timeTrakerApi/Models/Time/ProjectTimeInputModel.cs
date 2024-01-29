namespace timeTrakerApi.Models.Time
{
    public class ProjectTimeInputModel
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int Minutes { get; set; }
        public DateTime Date { get; set; }

    }
}
