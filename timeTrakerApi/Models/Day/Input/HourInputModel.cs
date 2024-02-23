using System.Text.Json.Serialization;

namespace timeTrakerApi.Models.Day.Input
{
    public class HourInputModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
