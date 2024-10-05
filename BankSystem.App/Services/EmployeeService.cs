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
                throw new PersonException("Не заполнено имя сотрудика");
            else if (employee.Age < 18)
                throw new PersonException("Детям тут не место");
            else if (employee.Passport == "")
                throw new PersonException("Требуются паспортные данные");

            _employeeStorage.Add(employee);
        }

        public Employee? GetEmployeeByName(string name)
        {
            return _employeeStorage.GetEmployee(c => c.Name == name);
        }

        public Employee? GetEmployeeByPhone(string phone)
        {
            return _employeeStorage.GetEmployee(c => c.PersonalPhoneNumber == phone);
        }

        public Employee? GetEmployeeByPassport(string passport)
        {
            return _employeeStorage.GetEmployee(c => c.Passport == passport);
        }

        public List<Employee> GetEmployeesByBirthdayRange(DateOnly startDate, DateOnly endDate)
        {
            return _employeeStorage.GetEmployees(c => c.Birthday >= startDate && c.Birthday <= endDate);
        }

        public int GetEmployeesCount()
        {
            return _employeeStorage.Count();
        }

    }
}
