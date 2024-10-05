using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        private readonly Dictionary<Client, List<Account>> _collection = [];

        public void Add(Client client, List<Account> accounts) => _collection.Add(client, accounts);

        public Client Min<TKey>(Func<Client, TKey> keySelector)
        {
            return _collection.Keys.OrderBy(keySelector).First();
        }

        public Client Max<TKey>(Func<Client, TKey> keySelector)
        {
            return _collection.Keys.OrderByDescending(keySelector).First();
        }

        public void AddClientAccount(Client client, Account account)
        {
            _collection[client].Add(account);
        }

        public void UpdateClientAccount(Client client, Account account, int newAccountSum)
        {
            Account? findAccount = _collection[client].Find(a => a == account);
            if (findAccount != null)
            {
                findAccount.Amount = newAccountSum;
            }
        }

        public List<Account> GetAccounts(Client client)
        {
            return _collection[client];
        }

        public Client? GetClient(Func<Client, bool> predicate)
        {
            return _collection.Keys.Where(predicate).FirstOrDefault();
        }
        public List<Client> GetClients(Func<Client, bool> predicate)
        {
            return _collection.Keys.Where(predicate).ToList();
        }

        public int Sum(Func<Client, int> selector)
        {
            return _collection.Keys.Sum(selector);
        }

        public int Count() => _collection.Count;
    }
}
