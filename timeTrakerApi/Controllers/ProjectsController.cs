using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Data.Repositories;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectRepository _projectRepository;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectRepository projectRepository)
        {
            _logger = logger;
            _projectRepository = projectRepository;
        }

        [HttpGet("GetProjects")]
        [Authorize]
        public ActionResult<List<ProjectModel>> GetProjects()
        {
            List<ProjectModel> projects = _projectRepository.Get();
            if (projects == null || projects.Count == 0)
            {
                _logger.LogError("GetProjects: No projects found");
                return NotFound("No projects found");
            }
            _logger.LogTrace("GetProjects: {0} projects found", projects.Count);
            return Ok(projects);
        }
        [HttpGet("{projectId}")]
        public ActionResult<ProjectModel> GetProjectById(int id)
        {

            ProjectModel? project = _projectRepository.GetById(id);
            if (project == null)
            {
                _logger.LogError("GetProjectById: Project Not Found for Id: {projectId}", id);
                return NotFound("User Not Found");
            }
            _logger.LogTrace("GetUserByToken for Id: {id}", id);
            return project;
        }

        [HttpPost("CreateProject")]
        public IActionResult CreateProject([FromBody] ProjectModel project)
        {
            if (project == null)
            {
                _logger.LogError("CreateProject: project is empty");
                return BadRequest("project cannot be null");
            }
            bool result = _projectRepository.Insert(project);
            return Ok(result);
        }
        
    }
}
