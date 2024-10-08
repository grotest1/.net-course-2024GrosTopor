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
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateOnly Birthday { get; set; }
        public int Age { get; set; }
        public string PersonalPhoneNumber { get; set; } = "";
        public string Passport { get; set; } = "";


        public override bool Equals(object? obj)
        {
            if (obj is Person person)
            {
                return Name == person.Name
                    && Age == person.Age
                    && PersonalPhoneNumber == person.PersonalPhoneNumber
                    && Passport == person.Passport;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Birthday.GetHashCode() + Age.GetHashCode() + PersonalPhoneNumber.GetHashCode() + Passport.GetHashCode();
        }
    }
}
