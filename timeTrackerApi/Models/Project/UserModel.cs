using System.Text.Json.Serialization;

namespace timeTrackerApi.Models.Project
{
    public class UserModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
