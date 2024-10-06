using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Services;
using Xunit;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;

namespace BancSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployeePositivTest()
        {
            EmployeeService employeeService = new EmployeeService();

            int countBefore = employeeService.GetEmployeesCount();
            employeeService.AddEmployee(new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25 });
            int countAfter = employeeService.GetEmployeesCount();

            Assert.Equal(0, countBefore);
            Assert.Equal(1, countAfter);
        }

        [Fact]
        public void AddEmployeeNegativeTestByAge()
        {
            EmployeeService employeeService = new EmployeeService();

            Assert.Throws<UnderAgeException>(() => employeeService.AddEmployee(new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 15 }));
        }

        [Fact]
        public void AddEmployeeNegativeTestByPassport()
        {
            EmployeeService employeeService = new EmployeeService();

            Assert.Throws<EmptyRequiredDataException>(() => employeeService.AddEmployee(new Employee() { Name = "Ричард", Passport = "", Age = 35 }));
        }

        [Fact]
        public void GetEmployeeByNamePositivTest()
        {
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            employeeService.AddEmployee(employee);

            Employee? findEmployee = employeeService.GetEmployee(c => c.Name == employee.Name);

            Assert.Equal(employee, findEmployee);
        }

        [Fact]
        public void GetEmployeeByPhonePositivTest()
        {
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            employeeService.AddEmployee(employee);

            Employee? findEmployee = employeeService.GetEmployee(c => c.PersonalPhoneNumber == employee.PersonalPhoneNumber);

            Assert.Equal(employee, findEmployee);
        }

        [Fact]
        public void GetEmployeeByPassportPositivTest()
        {
            EmployeeService employeeService = new EmployeeService();
            Employee employee = new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            employeeService.AddEmployee(employee);

            Employee? findEmployee = employeeService.GetEmployee(c => c.Passport == employee.Passport);

            Assert.Equal(employee, findEmployee);
        }

        [Fact]
        public void GetEmployeesByBirthdayRangePositivTest()
        {
            EmployeeService employeeService = new EmployeeService();
            Employee employee1 = new Employee() { Name = "Ричард1", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 22) };
            employeeService.AddEmployee(employee1);
            Employee employee2 = new Employee() { Name = "Ричард2", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(2020, 01, 12) };
            employeeService.AddEmployee(employee2);
            Employee employee3 = new Employee() { Name = "Ричард3", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1995, 05, 19) };
            employeeService.AddEmployee(employee3);

            List<Employee> employees = employeeService.GetEmployees(c => c.Birthday >= new DateOnly(2019, 1, 1) && c.Birthday <= new DateOnly(2022, 1, 1));
            
            Assert.Equal(1, employees?.Count);
            Assert.Equal(employee2, employees?[0]);
        }
    }
}
