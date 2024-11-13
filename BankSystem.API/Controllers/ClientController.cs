using BankSystem.App.Dto;
using BankSystem.App.Services;
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
            //var response = _clientService.GetClientsDto();
            //return Ok(response);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/hello.txt");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "text/plain";
            string file_name = "hello2.txt";
            return File(mas, file_type, file_name);


        }

        [HttpGet]
        [Route("{id:Guid}")]
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
