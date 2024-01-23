using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.Project
{
    public class ProjectModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public DateTime? CreateOnDate { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedOnDate { get; set; }
    }
}
