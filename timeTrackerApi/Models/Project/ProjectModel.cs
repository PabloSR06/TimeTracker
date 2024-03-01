using System.Text.Json.Serialization;

namespace timeTrackerApi.Models.Project
{
    public class ProjectModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public DateTime? CreateOnDate { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedOnDate { get; set; }
    }
}
