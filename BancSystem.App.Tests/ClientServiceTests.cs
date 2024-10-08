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
            
            ClientService clientService = new ClientService();

            int countBefore = clientService.GetClientsCount();
            clientService.AddClient(new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 });
            int countAfter = clientService.GetClientsCount();

            Assert.Equal(0, countBefore);
            Assert.Equal(1, countAfter);
        }

        [Fact]
        public void AddClientNegativeTestByAge()
        {
            ClientService clientService = new ClientService();

            Assert.Throws<UnderAgeException>(() => clientService.AddClient(new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 15 }));
        }

        [Fact]
        public void AddClientNegativeTestByPassword()
        {
            ClientService clientService = new ClientService();

            Assert.Throws<EmptyRequiredDataException>(() => clientService.AddClient(new Client() { Name = "Ричард", Passport = "", Age = 35 }));
        }

        [Fact]
        public void AddAccountPositivTest()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики"} };

            clientService.AddAccount(client, account);
        }

        [Fact]
        public void AddAccountNegitivTestAnotherClient()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };

            Client anotherClient = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            Account account = new Account() { Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };

            Assert.Throws<MissingDataException>(() => clientService.AddAccount(anotherClient, account));
        }

        [Fact]
        public void AddAccountNegitivTestUncorrectCurrency()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            Account account = new Account() { Amount = 159, Currency = new Currency()};

            Assert.Throws<EmptyRequiredDataException>(() => clientService.AddAccount(client, account));
        }

        [Fact]
        public void UpdateAccountPositivTest()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            Account account = new Account() { Id = 999, Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };
            clientService.AddAccount(client, account);

            clientService.UpdateAccount(client, account, 500);

            int? newAmount = clientService.GetClientAccount(client, account.Id)?.Amount;
            Assert.Equal(500, newAmount);
        }

        [Fact]
        public void UpdateAccountNegativeTestAnotherClient()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            Account account = new Account() { Id = 999, Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };
            clientService.AddAccount(client, account);

            Assert.Throws<MissingDataException>(() => clientService.UpdateAccount(new Client(), account, 500));
        }

        [Fact]
        public void UpdateAccountNegativeTestAnotherAccount()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25 };
            clientService.AddClient(client);
            Account account = new Account() { Id = 999, Amount = 159, Currency = new Currency() { Code = 501, Name = "фантики" } };

            Assert.Throws<MissingDataException>(() => clientService.UpdateAccount(client, account, 500));
        }

        [Fact]
        public void GetClientByNamePositivTest()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            clientService.AddClient(client);

            Client? findClient = clientService.GetClient(c => c.Name == client.Name);

            Assert.Equal(client, findClient);
        }

        [Fact]
        public void GetClientByPhonePositivTest()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            clientService.AddClient(client);

            Client? findClient = clientService.GetClient(c => c.PersonalPhoneNumber == client.PersonalPhoneNumber);
            
            Assert.Equal(client, findClient);
        }

        [Fact]
        public void GetClientByPassportPositivTest()
        {
            ClientService clientService = new ClientService();
            Client client = new Client() { Name = "Ричард", Passport = "EHGN 111", Age = 25, PersonalPhoneNumber = "77755544", Birthday = new DateOnly(1999, 12, 12) };
            clientService.AddClient(client);

            Client? findClient = clientService.GetClient(c => c.Passport == client.Passport);
            
            Assert.Equal(client, findClient);
        }

        [Fact]
        public void GetClientsByBirthdayRangePositivTest()
        {
            ClientService clientService = new ClientService();
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
