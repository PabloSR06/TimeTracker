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

        [HttpGet("GetAllClients")]
        public List<ClientModel> GetAllClients()
        {
            return _clientRepository.Get();
        }

    }
}
