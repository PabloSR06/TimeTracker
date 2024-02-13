using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;


        public AuthController(ILogger<AuthController> logger, ITokenService tokenService, IUserRepository userRepository)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserCredentialsModel credentials)
        {
            UserProfileModel userProfile = new UserProfileModel();
            userProfile.Email = credentials.Email;
            userProfile.Roles = new List<RoleModel> { new RoleModel { Name = "Admin" } };
            userProfile.Id = 1;

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(userProfile)) });


            //return Unauthorized();
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserCredentialsModel credentials)
        {
            var userProfile = new UserModel();
            userProfile.Email = credentials.Email;
            userProfile.Password = credentials.Password;
            userProfile.Name = credentials.Email;

            bool register = _userRepository.Insert(userProfile);

            if (register)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
