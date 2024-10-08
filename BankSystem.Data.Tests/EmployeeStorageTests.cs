
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
            EmployeeStorage employeeStorage = new EmployeeStorage();
                
            employeeStorage.Add(new Employee() { Name = "Tom", Age = 30});

            int count = employeeStorage.Get(e => true).Count();

            Assert.Equal(1, count);
        }
    }
}
