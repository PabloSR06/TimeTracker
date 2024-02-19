using System.Globalization;

namespace timeTrakerApi.Models.Time
{
    public class ProjectTimeInputModel
    {
        public string ProjectId { get; set; }
        public string Minutes { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}
