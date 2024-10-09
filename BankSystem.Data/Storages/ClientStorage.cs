using BankSystem.Domain.Models;
using System;
using System.Linq;
using System.Security.Principal;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private readonly Dictionary<Client, List<Account>> _collection = [];

        public void Add(Client client)
        {
            _collection.Add(client, []);
        }

        public void Update(Client client)
        {
            Client? findClient = _collection.Keys.FirstOrDefault(c => c.Id == client.Id);
            if (findClient != null)
            {
                findClient.Age = client.Age;
                findClient.Name = client.Name;
                findClient.Passport = client.Passport;
                findClient.PersonalPhoneNumber = client.PersonalPhoneNumber;
            }
        }

        public void Delete(Client client)
        {
            _collection.Remove(client);
        }

        public List<Client> Get(Func<Client, bool> filter)
        {
            return _collection.Keys.Where(filter).ToList();
        }

        public void AddAccount(Client client, Account account)
        {
            _collection[client].Add(account);
        }

        public void UpdateAccount(Client client, Account account)
        {
            Account? findAccount = _collection[client].FirstOrDefault(a => a.Id == account.Id);
            if (findAccount != null)
                findAccount.Amount = account.Amount;
        }

        public void DeleteAccount(Client client, Account account)
        {
            _collection[client].Remove(account);
        }

        public List<Account> GetAccount(Client client, Func<Account, bool> filter)
        {
            return _collection[client].Where(filter).ToList();
        }
    }
}
