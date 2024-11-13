using BankSystem.Domain.Models;
using BankSystem.Data.Storages;
using Xunit;

namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        [Fact]
        public void AddAndCountClients()
        {
            ClientStorageEF clientStorage = new ClientStorageEF();

            clientStorage.AddAsync(new Client { Name = "Ионел" });
            int count = clientStorage.GetAsync(e => true).Result.Count();

            Assert.Equal(1, count);
        }


        [Fact]
        public void GetHuet()
        {
            int a = 0;
            int b = 1 / a;

        }
    }
}
