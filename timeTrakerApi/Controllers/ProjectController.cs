using Microsoft.AspNetCore.Mvc;
using timeTrakerApi.Data.Interface;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _clientRepository;

        public ClientController(ILogger<ClientController> logger, IClientRepository clientRepository)
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
