using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Time;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;
        private readonly ITimeRepository _timeRepository;

        public TimeController(ILogger<TimeController> logger, ITimeRepository timeRepository)
        {
            _logger = logger;
            _timeRepository = timeRepository;
        }

        [HttpPost("GetDayHours")]
        public List<DayHoursModel> GetDayHours(HourInputModel input)
        {
            _logger.LogInformation("GetDayHours");
            return _timeRepository.GetDayHours(input);
        }
        
        [HttpPost("GetProjectHours")]
        public List<HoursProjectModel> GetProjectHours(HourInputModel input)
        {
            _logger.LogInformation("GetProjectHours");
            return _timeRepository.GetProjectHours(input);
        }
        [HttpPost("InsertDayHours")]
        public bool InsertDayHours(DayInputModel input)
        {
            _logger.LogInformation("InsertDayHours");
            return _timeRepository.InsertDayHours(input);
        }
        [HttpPost("InsertProjectHours")]
        public bool InsertProjectHours(ProjectTimeInputModel input)
        {
            _logger.LogInformation("InsertDayHours");
            return _timeRepository.InsertProjectHours(input);
        }
    }
}
