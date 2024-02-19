using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IClientRepository _clientRepository;

        public ClientsController(ILogger<ClientsController> logger, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
        }

        [HttpGet("GetClients")]
        [Authorize]
        public ActionResult<List<ClientModel>> GetClients()
        {
            List<ClientModel> clients = _clientRepository.Get();
            if (clients == null || clients.Count == 0)
            {
                _logger.LogError("GetClients: No clients found");
                return NotFound("No clients found");
            }
            _logger.LogTrace("GetClients: {0} clients found", clients.Count);
            return Ok(clients);
        }


    }
}
