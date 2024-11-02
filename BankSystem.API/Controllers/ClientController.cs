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
        public IActionResult GetClients()
        {
            var response = _clientService.GetClientsDto();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetClient(Guid id)
        {
            var response = _clientService.GetClientDto(id);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientDto client)
        {
            Guid clientId = await _clientService.AddClientAsync(client);
            return Ok(clientId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDto client)
        {
            await _clientService.UpdateClientAsync(client);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient([FromQuery] Guid id)
        {
            await _clientService.DeleteClientAsync(id);
            return Ok();
        }


    }
}
