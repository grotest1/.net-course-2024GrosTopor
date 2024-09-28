using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Employee : Person
    {
        public string Contract {  get; set; } = "";
        public int Salary { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is Employee employee && base.Equals(employee))
            {
                return Contract == employee.Contract
                    && Salary == employee.Salary;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + Contract.GetHashCode() + Salary;
        }


    }
}
