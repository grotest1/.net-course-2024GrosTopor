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
            Dictionary<Client, Account> testData = TestDataGenerator.GenerateSomeDataSimple(10);
            KeyValuePair<Client, Account> firstElement = testData.FirstOrDefault();

            Client clientFromTestData = firstElement.Key;
            Client newClient = new Client
            {
                Name = clientFromTestData.Name,
                Age = clientFromTestData.Age,
                PersonalPhoneNumber = clientFromTestData.PersonalPhoneNumber
            };

            Account accountResult = testData[newClient];

            Assert.Equal(firstElement.Value, accountResult);
        }

        [Fact]
        public void GetHashCodeNecessityPositivTestAccountArray()
        {
            Dictionary<Client, Account[]> testData = TestDataGenerator.GenerateSomeDataArray(10);
            KeyValuePair<Client, Account[]> firstElement = testData.FirstOrDefault();

            Client clientFromTestData = firstElement.Key;
            Client newClient = new Client
            {
                Name = clientFromTestData.Name,
                Age = clientFromTestData.Age,
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
                Age = lastEmployee.Age,
                PersonalPhoneNumber = lastEmployee.PersonalPhoneNumber,
                Contract = lastEmployee.Contract,
                Salary = lastEmployee.Salary
            };

            Employee? employeeFind = employees.FirstOrDefault(e => e == newClient);

            Assert.False(lastEmployee.Equals(employeeFind));
        }


    }
}
