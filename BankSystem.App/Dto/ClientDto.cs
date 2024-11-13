using System;

namespace BankSystem.App.Dto
{
    public class ClientDto : PersonDto
    { 
        public EmployeeDto Manager {  get; set; }
        public string ManagerFio { get; set; }
    }
}