
using BankSystem.Domain.Models;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using Xunit;


namespace BankSystem.Data.Tests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public void AddRangeAndCount5000Clients()
        {
            List<Employee> employees = TestDataGenerator.GenerateEmployeeList(5000);
            EmployeeStorage employeeStorage = new EmployeeStorage();

            employeeStorage.AddRange(employees);
            int count = employeeStorage.Count();

            Assert.Equal(5000, count);
        }


        [Fact]
        public void MostYoungEmployeeEqIvanov()
        {
            Employee[] employees =
            {
                new Employee {Name = "Мылинов", Birthday = new DateOnly(2003, 04, 05) },
                new Employee {Name = "Сидоров", Birthday = new DateOnly(1990, 04, 05) },
                new Employee {Name = "Зукобин", Birthday = new DateOnly(2010, 04, 05) },
                new Employee {Name = "Иванов",  Birthday = new DateOnly(2020, 04, 05) },
                new Employee {Name = "Пучкова", Birthday = new DateOnly(2000, 04, 05) }
            };
            EmployeeStorage employeeStorage = new EmployeeStorage();
            employeeStorage.AddRange(employees);

            Employee mostYouongEmployee = employeeStorage.MostYoungEmployee();

            Assert.Equal(employees[3], mostYouongEmployee);
        }


        [Fact]
        public void MostOldEmployeeEqSidorov()
        {
            Employee[] employees =
            {
                new Employee {Name = "Мылинов", Birthday = new DateOnly(2003, 04, 05) },
                new Employee {Name = "Сидоров", Birthday = new DateOnly(1990, 04, 05) },
                new Employee {Name = "Зукобин", Birthday = new DateOnly(2010, 04, 05) },
                new Employee {Name = "Иванов",  Birthday = new DateOnly(2020, 04, 05) },
                new Employee {Name = "Пучкова", Birthday = new DateOnly(2000, 04, 05) }
            };
            EmployeeStorage employeeStorage = new EmployeeStorage();
            employeeStorage.AddRange(employees);

            Employee mostOldEmployee = employeeStorage.MostOldEmployee();

            Assert.Equal(employees[1], mostOldEmployee);
        }


        [Fact]
        public void MiddleAge5EmployeesEq19()
        {
            Employee[] employees =
            {
                new Employee {Name = "Мылинов", Birthday = new DateOnly(2003, 04, 05) },
                new Employee {Name = "Сидоров", Birthday = new DateOnly(1990, 04, 05) },
                new Employee {Name = "Зукобин", Birthday = new DateOnly(2010, 04, 05) },
                new Employee {Name = "Иванов",  Birthday = new DateOnly(2020, 04, 05) },
                new Employee {Name = "Пучкова", Birthday = new DateOnly(2000, 04, 05) }
            };
            EmployeeStorage employeeStorage = new EmployeeStorage();
            employeeStorage.AddRange(employees);

            int middleAge = employeeStorage.MiddleAge();

            Assert.Equal(19, middleAge);
        }
    }
}
