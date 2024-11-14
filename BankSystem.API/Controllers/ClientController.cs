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
        private IConfiguration _configuration;

        public ClientController(ClientService clientService, IConfiguration configuration)
        {
            _clientService = clientService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            var response = _clientService.GetClientsDto();
            return Ok(response);
        }

        [HttpGet]
        [Route("files")]
        
        public IActionResult GetFiles()
        {
            string path = Path.Combine("C:", "1", "hello.txt");
            //byte[] mas = System.IO.File.ReadAllBytes(path);
            //string file_type = "text/plain";
            //string file_name = "hello2.txt";
            //return File(mas, file_type, file_name);

            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "text/plain";
            string file_name = "hello3.txt";
            return File(fs, file_type, file_name);


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
