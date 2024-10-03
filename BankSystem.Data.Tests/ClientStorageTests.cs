
using BankSystem.Domain.Models;
using BankSystem.App.Services;
using BankSystem.Data.Storages;
using Xunit;


namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        [Fact]
        public void AddRangeAndCount5000Clients()
        {
            List<Client> clients = TestDataGenerator.GenerateClientList(5000);
            ClientStorage clientStorage = new ClientStorage();

            clientStorage.AddRange(clients);
            int count = clientStorage.Count();

            Assert.Equal(5000, count);
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
            ClientStorage clientStorage = new ClientStorage();
            clientStorage.AddRange(clients);

            Client mostYouongClient = clientStorage.MostYoungClient();

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
            ClientStorage clientStorage = new ClientStorage();
            clientStorage.AddRange(clients);

            Client mostOldClient = clientStorage.MostOldClient();

            Assert.Equal(clients[1], mostOldClient);
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
            ClientStorage clientStorage = new ClientStorage();
            clientStorage.AddRange(clients);

            int middleAge = clientStorage.MiddleAge();

            Assert.Equal(19, middleAge);
        }
    }
}
