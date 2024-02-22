using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Data.Repositories;
using timeTrakerApi.Models.Client;
using timeTrakerApi.Models.Day;
using timeTrakerApi.Models.Day.Input;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.Project.Input;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChronosController : ControllerBase
    {
        private readonly ILogger<ChronosController> _logger;
        private readonly IChronoRepository _chronoRepository;

        public ChronosController(ILogger<ChronosController> logger, IChronoRepository chronoRepository)
        {
            _logger = logger;
            _chronoRepository = chronoRepository;
        }

        [HttpPost("/day/time")]
        [Authorize]
        public ActionResult<List<DayHoursModel>> GetDayHours(HourInputModel input)
        {
            try
            {
                _logger.LogInformation("GetDayHours");
                string? userId = User.FindFirst("userid")?.Value;
                if (!int.TryParse(userId, out int userInt))
                {
                    _logger.LogError("TrackDayTime: UserId is empty");
                    return BadRequest("User Id cannot be empty");
                }


                List<DayHoursModel> result = _chronoRepository.GetDayHours(input, userInt);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetDayHours: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("/day/time/track")]
        [Authorize]
        public IActionResult TrackDayTime(DayInputModel input)
        {
            try
            {
                _logger.LogInformation("TrackDayTime");

                string? userId = User.FindFirst("userid")?.Value;

                if (!int.TryParse(userId, out int userInt))
                {
                    _logger.LogError("TrackDayTime: UserId is empty");
                    return BadRequest("User Id cannot be empty");
                }

                bool result = _chronoRepository.InsertDayHours(input, userInt);

                return Ok(result);


            }
            catch (Exception ex)
            {
                _logger.LogError($"TrackDayTime: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("/project/time")]
        [Authorize]
        public ActionResult<List<HoursProjectModel>> GetProjectHours(HourInputModel input)
        {
            try
            {
                _logger.LogInformation("GetProjectHours");

                string? userId = User.FindFirst("userid")?.Value;

                if (!int.TryParse(userId, out int userInt))
                {
                    _logger.LogError("GetUserByToken: UserId is empty");
                    return BadRequest("User Id cannot be empty");
                }

                List<HoursProjectModel> result = _chronoRepository.GetProjectHours(input, userInt);


                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProjectHours: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }

        [HttpPost("/project/time/track")]
        [Authorize]
        public IActionResult TrackProjectTime(ProjectTimeInputModel input)
        {
            try
            {
                _logger.LogInformation("TrackProjectTime");

                string? userId = User.FindFirst("userid")?.Value;

                if (!int.TryParse(userId, out int userInt))
                {
                    _logger.LogError("TrackProjectTime: UserId is empty");
                    return BadRequest("User Id cannot be empty");
                }

                bool result = _chronoRepository.InsertProjectHours(input, userInt);

                return Ok(result);


            }
            catch (Exception ex)
            {
                _logger.LogError($"TrackProjectTime: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
