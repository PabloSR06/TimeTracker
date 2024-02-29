namespace timeTrackerApi.Models.Project
{
    public class HoursProjectModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int Minutes { get; set; }
        public DateTime Date { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
    }
}
