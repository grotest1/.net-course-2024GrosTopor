using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void AddClientPositivTest()
        {
            
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };

            int countBefore = clientService.GetClients(c => c == client).Count;
            clientService.AddClient(client);
            int countAfter = clientService.GetClients(c => c == client).Count;

            Assert.True(countBefore + 1 == countAfter);
        }

        [Fact]
        public void AddClientNegativeTestByAge()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());

            Assert.Throws<UnderAgeException>(() => clientService.AddClient(new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 15 }));
        }

        [Fact]
        public void AddClientNegativeTestByPassword()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());

            Assert.Throws<EmptyRequiredDataException>(() => clientService.AddClient(new Client() { Name = "Ричард", Passport = "", Age = 35 }));
        }

        [Fact]
        public void AddAccountPositivTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики"},Client = client };

            clientService.AddAccount(account);
        }

        [Fact]
        public void AddAccountNegitivTestUncorrectCurrency()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "" }, Client = client };

            Assert.Throws<MissingDataException>(() => clientService.AddAccount(account));
        }

        [Fact]
        public void UpdateAccountPositivTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            
            Guid accountId = Guid.NewGuid();
            Account account     = new Account() { Id = accountId, Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };

            Account accountNew  = new Account() { Id = accountId, Amount = 500, Currency = new Currency() { Code = 501, Name = "фантики" } };
            
            clientService.AddAccount(account);

            clientService.UpdateAccount(accountNew);

            int? newAmount = clientService.GetClientAccount(account.Id)?.Amount;
            Assert.Equal(500, newAmount);
        }


        [Fact]
        public void UpdateAccountNegativeTestAnotherAccount()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Account account = new Account() { Id = Guid.NewGuid(), Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };

            Assert.Throws<MissingDataException>(() => clientService.UpdateAccount(account));
        }

        [Fact]
        public void GetClientByNamePositivTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            clientService.AddClient(client);

            Client? findClient = clientService.GetClients(c => c.Name == client.Name).FirstOrDefault();

            Assert.Equal(client, findClient);
        }

        [Fact]
        public void GetClientByPhonePositivTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            clientService.AddClient(client);

            Client? findClient = clientService.GetClients(c => c.PersonalPhoneNumber == client.PersonalPhoneNumber).FirstOrDefault();
            
            Assert.Equal(client, findClient);
        }

        [Fact]
        public void GetClientByPassportPositivTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            clientService.AddClient(client);

            Client? findClient = clientService.GetClients(c => c.Passport == client.Passport).FirstOrDefault();
            
            Assert.Equal(client, findClient);
        }

        [Fact]
        public void GetClientsByBirthdayRangePositivTest()
        {
            ClientService clientService = new ClientService(new ClientStorageEF());
            Client client1 = new Client() { Name = "Ричард1", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 22) };
            clientService.AddClient(client1);
            Client client2 = new Client() { Name = "Ричард2", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(2020, 01, 12) };
            clientService.AddClient(client2);
            Client client3 = new Client() { Name = "Ричард3", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1995, 05, 19) };
            clientService.AddClient(client3);

            List<Client> clients = clientService.GetClients(c => c.Birthday >= new DateOnly(2019, 1, 1) && c.Birthday <= new DateOnly(2022, 1, 1) );
            
            Assert.Equal(1, clients?.Count);
            Assert.Equal(client2, clients?[0]);
        }
    }
}
