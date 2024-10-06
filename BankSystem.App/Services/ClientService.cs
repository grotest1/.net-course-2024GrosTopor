﻿using System;
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
        private ClientStorage _clientStorage;
        public ClientService()
        {
            _clientStorage = new ClientStorage();
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
            _clientStorage.Add(client, new List<Account> { defaultAccount });
        }

        public List<Client> GetClients(Func<Client, bool> predicate)
        {
            return _clientStorage.GetClients(predicate);
        }
        public Client? GetClient(Func<Client, bool> predicate)
        {
            return _clientStorage.GetClient(predicate);
        }

        public void AddAccount(Client client, Account account)
        {
            if (_clientStorage.GetClient(c => c == client) == null)
                throw new MissingDataException("Клиент не найден");
            else if (account.Currency.Code == 0)
                throw new EmptyRequiredDataException("Currency.Code");
            else if (account.Currency.Name == "")
                throw new EmptyRequiredDataException("Currency.Name");

            _clientStorage.AddClientAccount(client, account);
        }
        public void UpdateAccount(Client client, Account account, int newAccountSum)
        {
            if (_clientStorage.GetClient(c => c == client) == null)
                throw new MissingDataException("Клиент не найден");
            else if (_clientStorage.GetClientAccount(client, account.Id) == null)
                throw new MissingDataException("Лицевой счет не принадлежит клиенту");

            _clientStorage.UpdateClientAccount(client, account, newAccountSum);
        }

        public Account? GetClientAccount(Client client, int idAccount)
        {
            if (_clientStorage.GetClient(c => c == client) == null)
                throw new MissingDataException("Клиент не найден");

            return _clientStorage.GetClientAccount(client, idAccount);
        }


        public int GetClientsCount()
        {
            return _clientStorage.Count();
        }
    }
}
