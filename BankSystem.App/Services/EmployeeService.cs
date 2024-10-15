using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using BankSystem.Data;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private IStorage<Employee> _employeeStorage;

        public EmployeeService(IStorage<Employee> employeeStorage)
        {
            _employeeStorage = employeeStorage;
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

        public void UpdateEmployee(Employee employee)
        {
            if (GetEmployee(employee.Id) == null)
                throw new MissingDataException("Сотрудник не найден");

            _employeeStorage.Update(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _employeeStorage.Delete(employee);
        }

        public Employee? GetEmployee(Guid employeeId)
        {
            return _employeeStorage.Get(c => c.Id == employeeId).FirstOrDefault();
        }

        public List<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return _employeeStorage.Get(predicate);
        }
    }
}
