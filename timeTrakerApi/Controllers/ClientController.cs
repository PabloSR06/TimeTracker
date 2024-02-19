using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(ILogger<ProjectController> logger, IProjectRepository projectRepository)
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
