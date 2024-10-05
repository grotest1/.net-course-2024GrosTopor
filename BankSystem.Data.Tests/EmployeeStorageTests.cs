
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
                new Employee {Name = "Мылинов", Age = 15 },
                new Employee {Name = "Сидоров", Age = 39 },
                new Employee {Name = "Зукобин", Age = 19 },
                new Employee {Name = "Иванов",  Age = 11 },
                new Employee {Name = "Пучкова", Age = 22 }
            };
            EmployeeStorage employeeStorage = new EmployeeStorage();
            employeeStorage.AddRange(employees);

            Employee? mostYouongEmployee = employeeStorage.FirstOrderBy(e => e.Age);

            Assert.Equal(employees[3], mostYouongEmployee);
        }


        [Fact]
        public void MostOldEmployeeEqSidorov()
        {
            Employee[] employees =
            {
                new Employee {Name = "Мылинов", Age = 15 },
                new Employee {Name = "Сидоров", Age = 39 },
                new Employee {Name = "Зукобин", Age = 19 },
                new Employee {Name = "Иванов",  Age = 11 },
                new Employee {Name = "Пучкова", Age = 22 }
            };
            EmployeeStorage employeeStorage = new EmployeeStorage();
            employeeStorage.AddRange(employees);

            Employee? mostOldEmployee = employeeStorage.FirstOrderBy(e => e.Age, true);

            Assert.Equal(employees[1], mostOldEmployee);
        }


        [Fact]
        public void MiddleAge5EmployeesEq21()
        {
            Employee[] employees =
            {
                new Employee {Name = "Мылинов", Age = 15 },
                new Employee {Name = "Сидоров", Age = 39 },
                new Employee {Name = "Зукобин", Age = 19 },
                new Employee {Name = "Иванов",  Age = 11 },
                new Employee {Name = "Пучкова", Age = 22 }
            };
            EmployeeStorage employeeStorage = new EmployeeStorage();
            employeeStorage.AddRange(employees);

            int middleAge = employeeStorage.MiddleAge();

            Assert.Equal(21, middleAge);
        }
    }
}
