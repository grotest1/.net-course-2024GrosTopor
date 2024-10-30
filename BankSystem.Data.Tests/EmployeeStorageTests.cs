using BankSystem.Domain.Models;
using BankSystem.Data.Storages;
using Xunit;

namespace BankSystem.Data.Tests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public void AddRangeAndCount5000Clients()
        {
            EmployeeStorageEF employeeStorage = new EmployeeStorageEF();
                
            employeeStorage.AddAsync(new Employee() { Name = "Tom", Age = 30});

            int count = employeeStorage.GetAsync(e => true).Result.Count();

            Assert.Equal(1, count);
        }
    }
}
