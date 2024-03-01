using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using timeTrackerApi.Data.Interfaces;
using timeTrackerApi.Data.Repositories;
using timeTrackerApi.Models.Client;

namespace timeTrackerApi.Controllers
{
    /// <summary>
    /// API Controller for managing clients.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Controller Initialization
        /// </summary>
        /// <param name="clientRepository">The client repository</param>
        /// <param name="logger">The logger</param>
        public ClientsController(ILogger<ClientsController> logger, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Obtains a list of all clients.
        /// </summary>
        /// <returns>Return a List of BasicClientModel</returns>
        [HttpGet]
        [Authorize]
        public ActionResult<List<BasicClientModel>> GetClients()
        {
            try
            {
                List<BasicClientModel> clients = _clientRepository.Get();
                if (clients == null || clients.Count == 0)
                {
                    _logger.LogError("GetClients: No clients found");
                    return NotFound("No clients found");
                }
                _logger.LogTrace("GetClients: {0} clients found", clients.Count);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetClients: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        /// <summary>
        /// Obtains a single client.
        /// </summary>
        /// <param name="id">The client id</param>
        /// <returns>Return a Object BasicClientModel</returns>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<BasicClientModel> GetClient(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("DeleteClient: clientId is 0");
                    return BadRequest("Client Id cannot be 0");
                }

                BasicClientModel? client = _clientRepository.GetById(id);
                if (client == null)
                {
                    _logger.LogError("GetClient: Client Not Found for Id: {id}", id);
                    return NotFound("Client Not Found");
                }

                _logger.LogTrace("GetClient for Id: {clientId}", id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetClient: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Updates a client.
        /// </summary>
        /// <param name="client">The updated client information.</param>
        /// <returns>An IActionResult indicating the result of the update operation.</returns>

        [HttpPut]
        [Authorize]
        public IActionResult UpdateClient([FromBody] BasicClientModel client)
        {
            try
            {
                if (client == null)
                {
                    _logger.LogError("UpdateClient: client is empty");
                    return BadRequest("Client cannot be null");
                }
                bool result = _clientRepository.Update(client);

                return result ? Ok() : BadRequest("Client not created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateClient: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Delete a client.
        /// </summary>
        /// <param name="id">The ID of the client to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("DeleteClient: clientId is 0");
                    return BadRequest("Client Id cannot be 0");
                }
                bool result = _clientRepository.Delete(id);
                _logger.LogTrace("DeleteClient: {0}", result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteClient: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Create a client.
        /// </summary>
        /// <param name="client">The client data to be created</param>
        /// <returns>An IActionResult indicating the result of the created operation.</returns>

        [HttpPost]
        [Authorize]
        public IActionResult CreateClient([FromBody] BasicClientModel client)
        {
            try
            {
                if (client == null)
                {
                    _logger.LogError("CreateClient: User is empty");
                    return BadRequest("Client cannot be null");
                }
                bool result = _clientRepository.Insert(client);

                return result ? Created() : BadRequest("Client not created");
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateClient: An error occurred: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }

        }
    }
}
