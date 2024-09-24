using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Person
    {
        public string Name { get; set; } = "";
        public DateOnly Birthday { get; set; }
        public string PersonalPhoneNumber { get; set; }
    }
}
