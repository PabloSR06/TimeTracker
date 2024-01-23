using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interface;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;
        private readonly IProjectRepository _projectRepository;

        public TimeController(ILogger<TimeController> logger, IProjectRepository projectRepository)
        {
            _logger = logger;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public void Get()
        {
            _projectRepository.Get();
        }
    }
}
