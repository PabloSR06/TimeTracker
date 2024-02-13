using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public List<DayHoursModel> GetDayHours(HourInputModel input)
        {
            _logger.LogInformation("GetDayHours");
            string? userId = User.FindFirst("userid")?.Value;
            if(userId != null)
            {
                return _timeRepository.GetDayHours(input, userId);
            }
            return new List<DayHoursModel>();
        }
        
        [HttpPost("GetProjectHours")]
        [Authorize]
        public List<HoursProjectModel> GetProjectHours(HourInputModel input)
        {
            _logger.LogInformation("GetProjectHours");
            string? userId = User.FindFirst("userid")?.Value;
            if (userId != null)
            {
                return _timeRepository.GetProjectHours(input, userId);
            }
            return new List<HoursProjectModel>();
            
        }
        [HttpPost("InsertDayHours")]
        [Authorize]
        public bool InsertDayHours(DayInputModel input)
        {
            _logger.LogInformation("InsertDayHours");
            
            string? userId = User.FindFirst("userid")?.Value;
            if (userId != null)
            {
                return _timeRepository.InsertDayHours(input, userId);
            }
            return false;
        }
        [HttpPost("InsertProjectHours")]
        [Authorize]
        public bool InsertProjectHours(ProjectTimeInputModel input)
        {
            _logger.LogInformation("InsertDayHours");
            string? userId = User.FindFirst("userid")?.Value;
            if (userId != null)
            {
                return _timeRepository.InsertProjectHours(input, userId);
            }
            return false;
        }
    }
}
