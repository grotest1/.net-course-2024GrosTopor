using BankSystem.App.Services;
using Xunit;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;

namespace BancSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public async Task AddEmployeePositivTestAsync()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());

            int countBefore = employeeService.GetEmployeesAsync(e => true).Result.Count();
            await employeeService.AddEmployeeAsync(new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25 });
            int countAfter = employeeService.GetEmployeesAsync(e => true).Result.Count();

            Assert.True(countBefore + 1 == countAfter);
        }

        [Fact]
        public async Task AddEmployeeNegativeTestByAge()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());

            await Assert.ThrowsAsync<UnderAgeException>(() => employeeService.AddEmployeeAsync(new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 15 }));
        }

        [Fact]
        public async Task AddEmployeeNegativeTestByPassport()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());

            await Assert.ThrowsAsync<EmptyRequiredDataException>(() => employeeService.AddEmployeeAsync(new Employee() { Name = "Ричард", Passport = "", Age = 35 }));
        }

        [Fact]
        public async Task GetEmployeeByNamePositivTestAsync()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());
            Employee employee = new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            await employeeService.AddEmployeeAsync(employee);

            Employee? findEmployee = employeeService.GetEmployeesAsync(c => c.Name == employee.Name).Result.FirstOrDefault();

            Assert.Equal(employee, findEmployee);
        }

        [Fact]
        public async Task GetEmployeeByPhonePositivTestAsync()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());
            Employee employee = new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            await employeeService.AddEmployeeAsync(employee);

            Employee? findEmployee = employeeService.GetEmployeesAsync(c => c.PersonalPhoneNumber == employee.PersonalPhoneNumber).Result.FirstOrDefault();

            Assert.Equal(employee, findEmployee);
        }

        [Fact]
        public async Task GetEmployeeByPassportPositivTestAsync()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());
            Employee employee = new Employee() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            await employeeService.AddEmployeeAsync(employee);

            Employee? findEmployee = employeeService.GetEmployeesAsync(c => c.Passport == employee.Passport).Result.FirstOrDefault();

            Assert.Equal(employee, findEmployee);
        }

        [Fact]
        public async Task GetEmployeesByBirthdayRangePositivTestAsync()
        {
            EmployeeService employeeService = new EmployeeService(new EmployeeStorageEF());
            Employee employee1 = new Employee() { Name = "Ричард1", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 22) };
            await employeeService.AddEmployeeAsync(employee1);
            Employee employee2 = new Employee() { Name = "Ричард2", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(2020, 01, 12) };
            await employeeService.AddEmployeeAsync(employee2);
            Employee employee3 = new Employee() { Name = "Ричард3", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1995, 05, 19) };
            await employeeService.AddEmployeeAsync(employee3);

            List<Employee> employees = employeeService.GetEmployeesAsync(c => c.Birthday >= new DateOnly(2019, 1, 1) && c.Birthday <= new DateOnly(2022, 1, 1)).Result;
            
            Assert.Equal(employee2, employees?[0]);
        }
    }
}
