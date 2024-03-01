using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using timeTrackerApi.Data.Interfaces;
using timeTrackerApi.Data.Repositories;
using timeTrackerApi.Models.Client;
using timeTrackerApi.Models.Project;
using timeTrackerApi.Models.User;

namespace timeTrackerApi.Controllers
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

        /// <summary>
        /// Obtains a list of all projects.
        /// </summary>
        /// <returns>Return a List of BasicProjectModel</returns>
        [HttpGet]
        [Authorize]
        public ActionResult<List<BasicProjectModel>> GetProjects()
        {
            try
            {
                List<BasicProjectModel> projects = _projectRepository.Get();
                if (projects == null || projects.Count == 0)
                {
                    _logger.LogError("GetProjects: No projects found");
                    return NotFound("No projects found");
                }
                _logger.LogTrace("GetProjects: {0} projects found", projects.Count);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProjects: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Obtains a single project.
        /// </summary>
        /// <param name="id">The project id</param>
        /// <returns>Return a Object GetProjectById</returns>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<BasicProjectModel> GetProjectById(int id)
        {

            try
            {
                BasicProjectModel? project = _projectRepository.GetById(id);
                if (project == null)
                {
                    _logger.LogError("GetProjectById: Project Not Found for Id: {projectId}", id);
                    return NotFound("User Not Found");
                }
                _logger.LogTrace("GetUserByToken for Id: {id}", id);
                return project;

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetProjectById: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Create a project.
        /// </summary>
        /// <param name="project">The client data to be created</param>
        /// <returns>An IActionResult indicating the result of the created operation.</returns>

        [HttpPost("CreateProject")]
        public IActionResult CreateProject([FromBody] BasicProjectModel project)
        {
            try
            {
                if (project == null)
                {
                    _logger.LogError("CreateProject: project is empty");
                    return BadRequest("project cannot be null");
                }
                bool result = _projectRepository.Insert(project);
                return result ? Created() : BadRequest("Client not created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateProject: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a project.
        /// </summary>
        /// <param name="project">The updated project information.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>

        [HttpPut]
        [Authorize]
        public IActionResult UpdateProject([FromBody] BasicProjectModel project)
        {
            try
            {
                if (project == null)
                {
                    _logger.LogError("UpdateProject: project is empty");
                    return BadRequest("Project cannot be null");
                }
                bool result = _projectRepository.Update(project);

                return result ? Ok() : BadRequest("Client not created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateProject: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Delete a project.
        /// </summary>
        /// <param name="id">The ID of the project to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteProject(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("DeleteProject: project id is 0");
                    return BadRequest("Project Id cannot be 0");
                }
                bool result = _projectRepository.Delete(id);
                _logger.LogTrace("DeleteProject: {0}", result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteProject: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}
