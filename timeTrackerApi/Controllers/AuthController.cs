using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using timeTrackerApi.Data.Interfaces;
using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.User;
using timeTrackerApi.Services.Interfaces;

namespace timeTrackerApi.Controllers
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
            try
            {
                UserProfileModel userProfile = _userRepository.GetUserLogIn(credentials);

                if (userProfile == null)
                {
                    return Unauthorized();
                }

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(_tokenService.GenerateToken(userProfile)) });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Login: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        

        [HttpGet("password/{email}")]

        public IActionResult ForgotPassword(string email)
        {
            try
            {
                if (!IsValidEmail(email)) {
                    _logger.LogError("ForgotPassword: email is not valid");
                    return BadRequest("Email is not valid");
                }

                _userRepository.ForgotPassword(email);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdatePassword: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        


    }
}
