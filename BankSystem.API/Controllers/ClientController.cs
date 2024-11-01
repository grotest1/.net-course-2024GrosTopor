using BankSystem.App.Dto;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClient([FromQuery] Guid guid)
        {
            var response = _clientService.GetClientDto(guid);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDto client)
        {
            Guid clientId = await _clientService.AddClientAsync(client);
            return Ok(clientId);
        }


    }
}
