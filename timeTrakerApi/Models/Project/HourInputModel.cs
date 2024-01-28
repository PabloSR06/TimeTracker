using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.Project
{
    public class HourInputModel
    {
        public int UserId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
