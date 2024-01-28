using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;
        private readonly IDayHoursRepository _dayHoursRepository;

        public TimeController(ILogger<TimeController> logger, IDayHoursRepository dayHoursRepository)
        {
            _logger = logger;
            _dayHoursRepository = dayHoursRepository;
        }

        [HttpPost("GetDayHours")]
        public List<DayHoursModel> GetDayHours(HourInputModel input)
        {
            _logger.LogInformation("GetDayHours");
            return _dayHoursRepository.GetDayHours(input);
        }
    }
}
