using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Models.Project;

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

        [HttpGet("GetAllProjects")]
        public List<ProjectModel> GetAllProjects()
        {
            return _projectRepository.Get();
        }
        [HttpGet("GetProjectById")]
        public ProjectModel GetProjectById(string id)
        {
            return _projectRepository.GetById(id);
        }

        [HttpPost("InsertProject")]
        public bool InsertProject(ProjectModel project)
        {
            return _projectRepository.Insert(project);
        }
    }
}
