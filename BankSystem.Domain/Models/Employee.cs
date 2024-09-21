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

        private int _salary = 0;
        public int Salary { get { return _salary; } }


        public Employee(string name) : base(name) { }

        public void SetSalary(int summ)
        {
            //проверки напрашиваются
            _salary = summ;
        }





    }
}
