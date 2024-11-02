using BankSystem.App.Dto;
using BankSystem.App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var response = _employeeService.GetEmployeesDto();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetEmployee(Guid id)
        {
            var response = _employeeService.GetEmployeeDto(id);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employee)
        {
            Guid employeeId = await _employeeService.AddEmployeeAsync(employee);
            return Ok(employeeId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employee)
        {
            await _employeeService.UpdateEmployeetAsync(employee);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid id)
        {
            await _employeeService.DeleteEmployeetAsync(id);
            return Ok();
        }


    }
}
