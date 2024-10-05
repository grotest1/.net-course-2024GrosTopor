using System;
using System.Collections;
using System.Collections.Generic;
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

        public Client? FirstOrderBy<TKey>(Func<Client, TKey> keySelector, bool Descending = false)
        {
            if (Descending)
            {
                return _collection.Keys.OrderByDescending(keySelector).FirstOrDefault();
            }
            else
            {
                return _collection.Keys.OrderBy(keySelector).FirstOrDefault();
            }
        }


        public int MiddleAge()
        {
            int countClients = _collection.Count;
            return _collection.Keys.Sum(c => c.Age) / (countClients > 0 ? countClients : 0);
        }

        public int Count() => _collection.Count;
    }
}
