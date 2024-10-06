using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private EmployeeStorage _employeeStorage;
        public EmployeeService()
        {
            _employeeStorage = new EmployeeStorage();
        }

        public void AddEmployee(Employee employee)
        {
            if (employee.Name == "")
                throw new EmptyRequiredDataException("Name");
            else if (employee.Age < 18)
                throw new UnderAgeException(employee.Age);
            else if (employee.Passport == "")
                throw new EmptyRequiredDataException("Passport");

            _employeeStorage.Add(employee);
        }

        public Employee? GetEmployee(Func<Employee, bool> predicate)
        {
            return _employeeStorage.GetEmployee(predicate);
        }

        public List<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return _employeeStorage.GetEmployees(predicate);
        }

        public int GetEmployeesCount()
        {
            return _employeeStorage.Count();
        }

    }
}
