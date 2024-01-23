namespace timeTrakerApi.Models.Project
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateOnDate { get; set; }
        public DateTime? LastModifiedOnDate { get; set; }
    }
}
