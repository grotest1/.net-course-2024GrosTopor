
using BankSystem.Domain.Models;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using Xunit;


namespace BankSystem.Data.Tests
{
    public class StorageTests
    {

        [Fact]
        public void ClientStorageTest()
        {
            ClientStorage clientStorage = new ClientStorage();

            List<Client> clients = TestDataGenerator.GenerateClientList(10);
            foreach (Client client in clients)
            {
                clientStorage.Add(client);
            }


            Client mostYoungClient = clientStorage.collection.OrderBy(c => c.Birthday).First();
            Client mostOldClient = clientStorage.collection.OrderBy(c => c.Birthday).Last();

            int allYears = 0;
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Client client in clientStorage.collection)
            {
                int years = today.Year - client.Birthday.Year;
                if (today.Month < client.Birthday.Month && today.Day < client.Birthday.Day)
                    years--;

                allYears += years;
            }

            int countClients = clientStorage.collection.Count();
            int middleAge = countClients > 0 ? countClients : 0;
        }
        
        [Fact]
        public void EmployeeStorageTest()
        {
            EmployeeStorage employeeStorage = new EmployeeStorage();

            List<Employee> employees = TestDataGenerator.GenerateEmployeeList(10);
            foreach (Employee employee in employees)
            {
                employeeStorage.Add(employee);
            }


            Employee mostYoungEmployee = employeeStorage.collection.OrderBy(c => c.Birthday).First();
            Employee mostOldEmployee   = employeeStorage.collection.OrderBy(c => c.Birthday).Last();

            int allYears = 0;
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Employee client in employeeStorage.collection)
            {
                int years = today.Year - client.Birthday.Year;
                if (today.Month < client.Birthday.Month && today.Day < client.Birthday.Day)
                    years--;

                allYears += years;
            }

            int countClients = employeeStorage.collection.Count();
            int middleAge = countClients > 0 ? countClients : 0;
        }
    }
}
