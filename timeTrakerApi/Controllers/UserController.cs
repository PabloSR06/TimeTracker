using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using timeTrakerApi.Data;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpGet("GetAllUsers")]
        [Authorize]
        public List<UserModel> GetAllUsers()
        {
            return _userRepository.Get();
        }
        [HttpGet("GetUserById")]
        [Authorize]
        public UserModel GetUserById()
        {
            var userId = User.FindFirst("userid")?.Value;
            return _userRepository.GetById(userId);
        }
        [HttpPost("InsertProject")]
        [Authorize]
        public bool InsertProject(UserModel user)
        {
            return _userRepository.Insert(user);
        }
    }
}
