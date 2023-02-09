using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Models
{
    public class ClockHistory
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        public int TimeClock_id { get; set; }
        public int Minutes { get; set; }
        public string? Description { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class ClockHistoryMin
    {
        public int Id { get; set; }
        public string? Project_name { get; set; }
        public string? Collection_name { get; set; }
        public int Minutes { get; set; }
        public string? Description { get; set; }

    }

    public class ClockHistoryInput
    {

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You need to choose a collection")]
        public int collection_id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You need to choose a project")]
        public int Project_id { get; set; }
        public int TimeClock_id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The time should be more that 0")]
        public int Minutes { get; set; }
        public string? Description { get; set; }
    }
}
