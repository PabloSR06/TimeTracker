using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using timeTrackerApi.Data;
using timeTrackerApi.Data.Interfaces;
using timeTrackerApi.Data.Repositories;
using timeTrackerApi.Models.Client;
using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.User;

namespace timeTrackerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserRepository _userRepository;

        public UsersController(ILogger<UsersController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("token")]
        [Authorize]
        public ActionResult<BasicUserModel> GetUserByToken()
        {
            try
            {
                string? userId = User.FindFirst("userid")?.Value;

                if (!int.TryParse(userId, out int userInt))
                {
                    _logger.LogError("GetUserByToken: UserId is empty");
                    return BadRequest("User Id cannot be empty");
                }


                BasicUserModel? user = _userRepository.GetById(userInt);
                if (user == null)
                {
                    _logger.LogError("GetUserByToken: User Not Found for Id: {UserId}", userId);
                    return NotFound("User Not Found");
                }
                _logger.LogTrace("GetUserByToken for Id: {UserId}", userId);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetUserByToken: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<BasicUserModel> GetUserById(int id)
        {
            try
            {
                BasicUserModel? user = _userRepository.GetById(id);
                if (user == null)
                {
                    _logger.LogError("GetUserById: User Not Found for Id: {UserId}", id);
                    return NotFound("User Not Found");
                }

                _logger.LogTrace("GetUserById for Id: {UserId}", id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetUserById: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="client">The user data to be created</param>
        /// <returns>An IActionResult indicating the result of the created operation.</returns>

        [HttpPost]
        [Authorize]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("CreateUser: User is empty");
                    return BadRequest("User cannot be null");
                }
                bool result = _userRepository.Insert(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateUser: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Delete a client.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("DeleteClient: clientId is 0");
                    return BadRequest("Client Id cannot be 0");
                }
                bool result = _userRepository.Delete(id);
                _logger.LogTrace("DeleteClient: {0}", result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteUser: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("password/token")]
        [Authorize]
        public IActionResult UpdatePassword([FromBody] ResetPasswordModel input)
        {
            try
            {
                string? userId = User.FindFirst("userid")?.Value;

                if (!int.TryParse(userId, out int userInt))
                {
                    _logger.LogError("UpdatePassword: UserId is empty");
                    return BadRequest("User Id cannot be empty");
                }
                
                bool result = _userRepository.UpdatePassword(input, userInt);
                _logger.LogTrace("UpdatePassword: {0}", result);
                return result ? Ok() : BadRequest("Client not created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdatePassword: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
