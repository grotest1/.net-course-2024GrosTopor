using System;

namespace BankSystem.App.Dto
{
    public class EmployeeDto : PersonDto
    {
        public string Contract { get; set; } = "";
        public int Salary { get; set; }

    }
}