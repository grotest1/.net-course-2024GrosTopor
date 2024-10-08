using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private IClientStorage _clientStorage;
        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClient(Client client)
        {
            if (client.Name == "")
                throw new EmptyRequiredDataException("Name");
            else if (client.Age < 18)
                throw new UnderAgeException(client.Age);
            else if (client.Passport == "")
                throw new EmptyRequiredDataException("Passport");

            Account defaultAccount = new Account() { Currency = new Currency() { Code = 840, Name = "USD" } };
            _clientStorage.Add(client);
            _clientStorage.AddAccount(client, defaultAccount);
        }

        public List<Client> GetClients(Func<Client, bool> predicate)
        {
            return _clientStorage.Get(predicate);
        }

        //public Client? GetClient(Func<Client, bool> predicate)
        //{
        //    return _clientStorage.GetClient(predicate);
        //}

        public void AddAccount(Client client, Account account)
        {
            if (_clientStorage.Get(c => c == client).Count == 0)
                throw new MissingDataException("Клиент не найден");
            else if (account.Currency.Code == 0)
                throw new EmptyRequiredDataException("Currency.Code");
            else if (account.Currency.Name == "")
                throw new EmptyRequiredDataException("Currency.Name");

            _clientStorage.AddAccount(client, account);
        }
        public void UpdateAccount(Client client, Account account)
        {
            if (_clientStorage.Get(c => c == client).Count == 0)
                throw new MissingDataException("Клиент не найден");
            else if (_clientStorage.GetAccount(client, a => a == account).Count == 0)
                throw new MissingDataException("Лицевой счет не принадлежит клиенту");

            _clientStorage.UpdateAccount(client, account);
        }

        public Account? GetClientAccount(Client client, int idAccount)
        {
            if (_clientStorage.Get(c => c == client).Count == 0)
                throw new MissingDataException("Клиент не найден");

            return _clientStorage.GetAccount(client, a => a.Id == idAccount).FirstOrDefault();
        }


        //public int GetClientsCount()
        //{
        //    return _clientStorage.Count();
        //}
    }
}
