using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Person
    {
        public string Name { get; set; } = "";
        public DateOnly Birthday { get; set; }
        public string PersonalPhoneNumber { get; set; } = "";

        public override bool Equals(object? obj)
        {
            if (obj is Person person)
            {
                return Name == person.Name
                    && Birthday == person.Birthday
                    && PersonalPhoneNumber == person.PersonalPhoneNumber;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Birthday.GetHashCode() + PersonalPhoneNumber.GetHashCode();
        }
    }
}
