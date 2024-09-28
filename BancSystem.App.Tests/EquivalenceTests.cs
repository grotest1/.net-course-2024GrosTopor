using Xunit;
using BancSystem.App;
using BankSystem.Domain.Models;
using BankSystem.App.Services;


namespace BancSystem.App.Tests
{
    public class EquivalenceTests
    {

        [Fact]
        public void GetHashCodeNecessityPositivTest()
        {
            Dictionary<Client, Account> testData = TestDataGenerator.GenerateSomeData_Simple(10);
            KeyValuePair<Client, Account> firstElement = testData.FirstOrDefault();

            Client clientFromTestData = firstElement.Key;
            Client newClient = new Client
            {
                Name = clientFromTestData.Name,
                Birthday = clientFromTestData.Birthday,
                PersonalPhoneNumber = clientFromTestData.PersonalPhoneNumber
            };

            Account accountResult = testData[newClient];

            Assert.Equal(firstElement.Value, accountResult);
        }

        [Fact]
        public void GetHashCodeNecessityPositivTest_AccountArray()
        {
            Dictionary<Client, Account[]> testData = TestDataGenerator.GenerateSomeData_Array(10);
            KeyValuePair<Client, Account[]> firstElement = testData.FirstOrDefault();

            Client clientFromTestData = firstElement.Key;
            Client newClient = new Client
            {
                Name = clientFromTestData.Name,
                Birthday = clientFromTestData.Birthday,
                PersonalPhoneNumber = clientFromTestData.PersonalPhoneNumber
            };

            Account[] accountResult = testData[newClient];

            Assert.Equal(firstElement.Value, accountResult);
        }

        [Fact]
        public void GetHashCodeNecessityEmployeeList()
        {
            List<Employee> employees = TestDataGenerator.GenerateEmployeeList(10);
            Employee lastEmployee = employees.Last();
            Employee newClient = new Employee
            {
                Name = lastEmployee.Name,
                Birthday = lastEmployee.Birthday,
                PersonalPhoneNumber = lastEmployee.PersonalPhoneNumber,
                Contract = lastEmployee.Contract,
                Salary = lastEmployee.Salary
            };

            Employee employeeFind = employees.First(e => e == newClient);

            Assert.Equal(employeeFind, lastEmployee);
        }


    }
}
