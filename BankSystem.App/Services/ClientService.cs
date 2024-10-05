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
        private ClientStorage _clientStorage;
        public ClientService() => _clientStorage = new ClientStorage();


        public void AddClient(Client client)
        {
            if (client.Name == "")
                throw new PersonException("Не заполнено имя клиента");
            else if (client.Age < 18)
                throw new PersonException("Детям тут не место");
            else if (client.Passport == "")
                throw new PersonException("Требуются паспортные данные");

            Account defaultAccount = new Account() { Currency = new Currency() { Code = 840, Name = "USD" } };
            _clientStorage.Add(client, new List<Account> { defaultAccount });
        }

        public void AddAccount(Client client, Account account)
        {
            
            if (_clientStorage.GetClient(c => c == client) == null)
                throw new PersonException("Клиент не найден");
            else if (account.Currency.Code == 0 || account.Currency.Name == "")
                throw new PersonException("Валюта заполнена некорректно");

            _clientStorage.AddClientAccount(client, account);
        }
        public void UpdateAccount(Client client, Account account, int newAccountSum)
        {
            if (_clientStorage.GetClient(c => c == client) == null)
                throw new PersonException("Клиент не найден");
            else if (_clientStorage.GetAccounts(client).Find(a => a == account) == null)
                throw new PersonException("Лицевой счет не принадлежит клиенту");

            _clientStorage.UpdateClientAccount(client, account, newAccountSum);
        }

        public Client? GetClientByName(string name)
        {
            return _clientStorage.GetClient(c => c.Name == name);
        }

        public Client? GetClientByPhone(string phone)
        {
            return _clientStorage.GetClient(c => c.PersonalPhoneNumber == phone);
        }

        public Client? GetClientByPassport(string passport)
        {
            return _clientStorage.GetClient(c => c.Passport == passport);
        }

        public List<Client> GetClientsByBirthdayRange(DateOnly startDate, DateOnly endDate)
        {
            return _clientStorage.GetClients(c => c.Birthday >= startDate && c.Birthday <= endDate);
        }

        public int GetClientsCount()
        {
            return _clientStorage.Count();
        }
    }
}
