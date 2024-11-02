using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Dto
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = "";
        public int Age { get; set; } = 0;
        public string Phone { get; set; } = "";
        public string Passport { get; set; } = "";
    }
}
