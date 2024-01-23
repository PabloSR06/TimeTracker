namespace timeTrakerApi.Models.Project
{
    public class ProjectHoursModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int Minutes { get; set; }
        public DateTime? CreateOnDate { get; set; }
        public DateTime? LastModifiedOnDate { get; set; }
    }
}
