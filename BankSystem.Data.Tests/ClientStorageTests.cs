
using BankSystem.Domain.Models;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using Xunit;


namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        [Fact]
        public void AddAndCountClients()
        {
            ClientStorage clientStorage = new ClientStorage();

            int countBefore = clientStorage.Count();
            clientStorage.Add(new Client { Name = "Ионел" }, new List<Account> { new Account { Amount = 555, Currency = new Currency { Name = "руб"} } });
            int countAfter = clientStorage.Count();

            Assert.Equal(0, countBefore);
            Assert.Equal(1, countAfter);
        }

        [Fact]
        public void MostYoungClientEqIvanov()
        {
            Client[] clients =
            {
                new Client {Name = "Мылинов", Age = 15 },
                new Client {Name = "Сидоров", Age = 39 },
                new Client {Name = "Зукобин", Age = 19 },
                new Client {Name = "Иванов",  Age = 11 },
                new Client {Name = "Пучкова", Age = 22 }
            };
            Account defaultAccount = new Account() { Amount = 0, Currency = new Currency { Name = "руб" }};

            ClientStorage clientStorage = new ClientStorage();
            foreach (Client client in clients) 
                clientStorage.Add(client, [defaultAccount]);


            Client? mostYouongClient = clientStorage.FirstOrderBy(e => e.Age);

            Assert.Equal(clients[3], mostYouongClient);
        }

        [Fact]
        public void MostOldClientEqSidorov()
        {
            Client[] clients =
            {
                new Client {Name = "Мылинов", Age = 15 },
                new Client {Name = "Сидоров", Age = 39 },
                new Client {Name = "Зукобин", Age = 19 },
                new Client {Name = "Иванов",  Age = 11 },
                new Client {Name = "Пучкова", Age = 22 }
            };
            Account defaultAccount = new Account() { Amount = 0, Currency = new Currency { Name = "руб" } };

            ClientStorage clientStorage = new ClientStorage();
            foreach (Client client in clients)
                clientStorage.Add(client, [defaultAccount]);


            Client? mostYouongClient = clientStorage.FirstOrderBy(e => e.Age, true);

            Assert.Equal(clients[1], mostYouongClient);
        }


        [Fact]
        public void MiddleAge5ClientsEq21()
        {
            Client[] clients =
            {
                new Client {Name = "Мылинов", Age = 15 },
                new Client {Name = "Сидоров", Age = 39 },
                new Client {Name = "Зукобин", Age = 19 },
                new Client {Name = "Иванов",  Age = 11 },
                new Client {Name = "Пучкова", Age = 22 }
            };
            Account defaultAccount = new Account() { Amount = 0, Currency = new Currency { Name = "руб" } };

            ClientStorage clientStorage = new ClientStorage();
            foreach (Client client in clients)
                clientStorage.Add(client, [defaultAccount]);

            int middleAge = clientStorage.MiddleAge();

            Assert.Equal(21, middleAge);
        }
    }
}
