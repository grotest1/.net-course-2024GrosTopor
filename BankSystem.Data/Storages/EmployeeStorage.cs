using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IStorage<Employee>
    {
        private readonly List<Employee> _collection = [];

        public void Add(Employee employee)
        {
            _collection.Add(employee);
        }

        public void Update(Employee employee)
        {
            Employee? findEmployee = _collection.FirstOrDefault(c => c.Id == employee.Id);
            if (findEmployee != null)
            {
                findEmployee.Age = employee.Age;
                findEmployee.Name = employee.Name;
                findEmployee.Passport = employee.Passport;
                findEmployee.PersonalPhoneNumber = employee.PersonalPhoneNumber;
            }
        }

        public void Delete(Employee employee)
        {
            Employee? findEmployee = _collection.FirstOrDefault(c => c.Id == employee.Id);
            if (findEmployee != null)
                _collection.Remove(findEmployee);
        }

        public List<Employee> Get(Func<Employee, bool> filter)
        {
            return _collection.Where(filter).ToList();
        }

    }
}
