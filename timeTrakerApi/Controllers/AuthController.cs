using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly ITokenService _tokenService;


        public AuthController(ILogger<AuthController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserCredentialsModel credentials)
        {
            UserProfileModel userProfile = new UserProfileModel();
            userProfile.Email = credentials.Email;
            userProfile.Roles = new List<RoleModel> { new RoleModel { Name = "Admin" } };

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(userProfile)) });
            

            //return Unauthorized();
        }

        //[HttpPost("Register")]
        //public IActionResult Register([FromBody] UserProfileModel credentials)
        //{
        //    bool register = _userRepository.Register(credentials);

        //    if (register)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}
