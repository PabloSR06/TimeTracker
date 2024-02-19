using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using timeTrakerApi.Data;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Controllers
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

        [HttpGet("GetUserByToken")]
        [Authorize]
        public ActionResult<BasicUserModel> GetUserByToken()
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

        [HttpGet("{userId}")]
        [Authorize]
        public ActionResult<BasicUserModel> GetUserById(int userId)
        {

            BasicUserModel? user = _userRepository.GetById(userId);
            if (user == null)
            {
                _logger.LogError("GetUserById: User Not Found for Id: {UserId}", userId);
                return NotFound("User Not Found");
            }

            _logger.LogTrace("GetUserById for Id: {UserId}", userId);
            return user;
        }


        [HttpPost("CreateUser")]
        [Authorize]
        public IActionResult CreateUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                _logger.LogError("CreateUser: User is empty");
                return BadRequest("User cannot be null");
            }
            bool result = _userRepository.Insert(user);
            return Ok(result);
        }
    }
}
