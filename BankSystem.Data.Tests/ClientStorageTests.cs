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

            clientStorage.Add(new Client { Name = "Ионел" });
            int count = clientStorage.Get(e => true).Count();

            Assert.Equal(1, count);
        }

    }
}
