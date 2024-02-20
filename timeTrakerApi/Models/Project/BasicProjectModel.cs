using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.Project
{
    public class BasicProjectModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }

    }
}
