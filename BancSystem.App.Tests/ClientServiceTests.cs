using BankSystem.App.Services;
using Xunit;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;

namespace BancSystem.App.Tests
{
    public class ClientServiceTests
    {
        [Fact]
        public async Task AddClientPositivTestAsync()
        {
            
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };

            int countBefore = clientService.GetClientsAsync(c => c == client).Result.Count;
            await clientService.AddClientAsync(client);
            int countAfter = clientService.GetClientsAsync(c => c == client).Result.Count;

            Assert.True(countBefore + 1 == countAfter);
        }

        [Fact]
        public async Task AddClientNegativeTestByAge()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());

            await Assert.ThrowsAsync<UnderAgeException>(() => clientService.AddClientAsync(new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 15 }));
        }

        [Fact]
        public async Task AddClientNegativeTestByPassword()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());

            await Assert.ThrowsAsync<EmptyRequiredDataException>(() => clientService.AddClientAsync(new Client() { Name = "Ричард", Passport = "", Age = 35 }));
        }

        [Fact]
        public async Task AddAccountPositivTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            await clientService.AddClientAsync(client);
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики"},Client = client };

            await clientService.AddAccountAsync(client, account);
        }

        [Fact]
        public async Task AddAccountNegitivTestUncorrectCurrency()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "" }, Client = client };

            await Assert.ThrowsAsync<MissingDataException>(() => clientService.AddAccountAsync(client, account));
        }

        [Fact]
        public async Task UpdateAccountPositivTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            await clientService.AddClientAsync(client);
            
            Guid accountId = Guid.NewGuid();
            Account account     = new Account() { Id = accountId, Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };

            Account accountNew  = new Account() { Id = accountId, Amount = 500, Currency = new Currency() { Code = 501, Name = "фантики" } };

            await clientService.AddAccountAsync(client, account);

            await clientService.UpdateAccountAsync(accountNew);

            int? newAmount = clientService.GetClientAccountAsync(account.Id)?.Amount;
            Assert.Equal(500, newAmount);
        }


        [Fact]
        public async Task UpdateAccountNegativeTestAnotherAccount()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Account account = new Account() { Id = Guid.NewGuid(), Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };

            await Assert.ThrowsAsync<MissingDataException>(() => clientService.UpdateAccountAsync(account));
        }

        [Fact]
        public async Task GetClientByNamePositivTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            await clientService.AddClientAsync(client);

            Client? findClient = clientService.GetClientsAsync(c => c.Name == client.Name).FirstOrDefault();

            Assert.Equal(client, findClient);
        }

        [Fact]
        public async Task GetClientByPhonePositivTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            await clientService.AddClientAsync(client);

            Client? findClient = clientService.GetClientsAsync(c => c.PersonalPhoneNumber == client.PersonalPhoneNumber).FirstOrDefault();
            
            Assert.Equal(client, findClient);
        }

        [Fact]
        public async Task GetClientByPassportPositivTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            await clientService.AddClientAsync(client);

            Client? findClient = clientService.GetClientsAsync(c => c.Passport == client.Passport).FirstOrDefault();
            
            Assert.Equal(client, findClient);
        }

        [Fact]
        public async Task GetClientsByBirthdayRangePositivTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client1 = new Client() { Name = "Ричард1", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 22) };
            await clientService.AddClientAsync(client1);
            Client client2 = new Client() { Name = "Ричард2", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(2020, 01, 12) };
            await clientService.AddClientAsync(client2);
            Client client3 = new Client() { Name = "Ричард3", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1995, 05, 19) };
            await clientService.AddClientAsync(client3);

            List<Client> clients = clientService.GetClientsAsync(c => c.Birthday >= new DateOnly(2019, 1, 1) && c.Birthday <= new DateOnly(2022, 1, 1) ).Result;
            
            Assert.Equal(1, clients?.Count);
            Assert.Equal(client2, clients?[0]);
        }

        [Fact]
        public async Task CashOutPositiveTestAsync()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            await clientService.AddClientAsync(client);
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" }, Client = client };
            await clientService.AddAccountAsync(client, account);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            clientService.CashOut(account, 59, token);

            Assert.Equal(100, account.Amount);
        }
    }
}
