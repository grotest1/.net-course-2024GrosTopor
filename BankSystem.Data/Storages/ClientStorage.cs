using BankSystem.Domain.Models;
using System;
using System.Linq;
using System.Security.Principal;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        //public Dictionary<Client, List<Account>> Data => throw new NotImplementedException();

        private readonly Dictionary<Client, List<Account>> _collection = [];

        public void Add(Client item)
        {
            _collection.Add(item, []);
        }

        public void Update(Client item)
        {
            Client? findClient = _collection.Keys.FirstOrDefault(c => c.Id == item.Id);
            if (findClient != null)
            {
                findClient.Age = item.Age;
                findClient.Name = item.Name;
                findClient.Passport = item.Passport;
                findClient.PersonalPhoneNumber = item.PersonalPhoneNumber;
            }
        }

        public void Delete(Client item)
        {
            Client? findClient = _collection.Keys.FirstOrDefault(c => c.Id == item.Id);
            if (findClient != null)
                _collection.Remove(findClient);
        }

        public List<Client> Get(Func<Client, bool> filter)
        {
            return _collection.Keys.Where(filter).ToList();
        }

        //----------------------------
        
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
            Account? findAccount = _collection[client].FirstOrDefault(a => a.Id == account.Id);
            if (findAccount != null)
                _collection[client].Remove(findAccount);

        }

        public List<Account> GetAccount(Client client, Func<Account, bool> filter)
        {
            return _collection[client].Where(filter).ToList();
        }


        //public Client Min<TKey>(Func<Client, TKey> keySelector)
        //{
        //    return _collection.Keys.OrderBy(keySelector).First();
        //}

        //public Client Max<TKey>(Func<Client, TKey> keySelector)
        //{
        //    return _collection.Keys.OrderByDescending(keySelector).First();
        //}


        //public Account? GetClientAccount(Client client, int idAccount)
        //{
        //    return _collection[client].Find(a => a.Id == idAccount);
        //}

        //public Client? GetClient(Func<Client, bool> predicate)
        //{
        //    return _collection.Keys.Where(predicate).FirstOrDefault();
        //}
        //public List<Client> GetClients(Func<Client, bool> predicate)
        //{
        //    return _collection.Keys.Where(predicate).ToList();
        //}

        //public int Sum(Func<Client, int> selector)
        //{
        //    return _collection.Keys.Sum(selector);
        //}

        //public int Count() => _collection.Count;




    }
}
