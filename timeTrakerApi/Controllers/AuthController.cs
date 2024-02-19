using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;
using timeTrakerApi.Services.Interfaces;

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
            UserProfileModel userProfile = _userRepository.GetUserLogIn(credentials);

            if (userProfile.Id == 0)
            {
                return Unauthorized();
            }

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(userProfile)) });
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

        [HttpGet("ForgotPassword")]

        public IActionResult ForgotPassword(string Email)
        {

            _userRepository.ForgotPassword(Email);

            return Ok();
        }


        [HttpPut("ResetPassword")]
        [Authorize(Roles = "Guest")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel input)
        {

            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                var claimValue = claim?.Value;
                var userId = User.FindFirst("userid")?.Value;

                UserCredentialsModel userCredential = new UserCredentialsModel
                {
                    Email = claimValue,
                    Password = input.Password
                };

                _userRepository.ResetPassword(userCredential, userId);

                return Ok();
            }

            return Unauthorized();
        }
    }
}
