using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.Project
{
    public class HourInputModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
