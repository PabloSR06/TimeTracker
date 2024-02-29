using System.Text.Json.Serialization;

namespace timeTrackerApi.Models.Day.Input
{
    public class HourInputModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
