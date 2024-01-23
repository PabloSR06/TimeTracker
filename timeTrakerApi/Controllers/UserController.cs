using Microsoft.AspNetCore.Mvc;
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
        public List<UserModel> GetAllUsers()
        {
            return _userRepository.Get();
        }
        [HttpGet("GetUserById")]
        public UserModel GetUserById(string id)
        {
            return _userRepository.GetById(id);
        }
    }
}
