
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
                new Client {Name = "Мылинов", Birthday = new DateOnly(2003, 04, 05) },
                new Client {Name = "Сидоров", Birthday = new DateOnly(1990, 04, 05) },
                new Client {Name = "Зукобин", Birthday = new DateOnly(2010, 04, 05) },
                new Client {Name = "Иванов",  Birthday = new DateOnly(2020, 04, 05) },
                new Client {Name = "Пучкова", Birthday = new DateOnly(2000, 04, 05) }
            };
            Account defaultAccount = new Account() { Amount = 0, Currency = new Currency { Name = "руб" }};

            ClientStorage clientStorage = new ClientStorage();
            foreach (Client client in clients) 
                clientStorage.Add(client, [defaultAccount]);


            Client? mostYouongClient = clientStorage.FirstOrderBy(e => e.Birthday, true);

            Assert.Equal(clients[3], mostYouongClient);
        }

        [Fact]
        public void MostOldClientEqSidorov()
        {
            Client[] clients =
            {
                new Client {Name = "Мылинов", Birthday = new DateOnly(2003, 04, 05) },
                new Client {Name = "Сидоров", Birthday = new DateOnly(1990, 04, 05) },
                new Client {Name = "Зукобин", Birthday = new DateOnly(2010, 04, 05) },
                new Client {Name = "Иванов",  Birthday = new DateOnly(2020, 04, 05) },
                new Client {Name = "Пучкова", Birthday = new DateOnly(2000, 04, 05) }
            };
            Account defaultAccount = new Account() { Amount = 0, Currency = new Currency { Name = "руб" } };

            ClientStorage clientStorage = new ClientStorage();
            foreach (Client client in clients)
                clientStorage.Add(client, [defaultAccount]);


            Client? mostYouongClient = clientStorage.FirstOrderBy(e => e.Birthday);

            Assert.Equal(clients[1], mostYouongClient);
        }


        [Fact]
        public void MiddleAge5ClientsEq19()
        {
            Client[] clients =
            {
                new Client {Name = "Мылинов", Birthday = new DateOnly(2003, 04, 05) }, //21
                new Client {Name = "Сидоров", Birthday = new DateOnly(1990, 04, 05) }, //34
                new Client {Name = "Зукобин", Birthday = new DateOnly(2010, 04, 05) }, //14
                new Client {Name = "Иванов",  Birthday = new DateOnly(2020, 04, 05) }, //4
                new Client {Name = "Пучкова", Birthday = new DateOnly(2000, 04, 05) }  //24
            };
            Account defaultAccount = new Account() { Amount = 0, Currency = new Currency { Name = "руб" } };

            ClientStorage clientStorage = new ClientStorage();
            foreach (Client client in clients)
                clientStorage.Add(client, [defaultAccount]);

            int middleAge = clientStorage.MiddleAge();

            Assert.Equal(19, middleAge);
        }
    }
}
