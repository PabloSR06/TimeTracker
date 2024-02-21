using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Data.Repositories;
using timeTrakerApi.Models.Client;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Time;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChronosController : ControllerBase
    {
        private readonly ILogger<ChronosController> _logger;
        private readonly ITimeRepository _timeRepository;

        public ChronosController(ILogger<ChronosController> logger, ITimeRepository timeRepository)
        {
            _logger = logger;
            _timeRepository = timeRepository;
        }

        [HttpPost("/day/time")]
        [Authorize]
        public ActionResult<List<DayHoursModel>> GetDayHours(HourInputModel input)
        {
            try
            {
                _logger.LogInformation("GetDayHours");
                string? userId = User.FindFirst("userid")?.Value;
                if (userId != null)
                {
                    return _timeRepository.GetDayHours(input, userId);
                }
                return new List<DayHoursModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetDayHours: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("/day/time/track")]
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

        [HttpPost("/project/time")]
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
        
        [HttpPost("/project/time/track")]
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
