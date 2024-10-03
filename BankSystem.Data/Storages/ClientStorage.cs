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
        private readonly List<Client> _collection = [];

        public void AddRange(IEnumerable<Client> collection) => _collection.AddRange(collection);

        public Client MostYoungClient()
        {
            return _collection.OrderByDescending(c => c.Birthday).First();
        }

        public Client MostOldClient()
        {
            return _collection.OrderBy(c => c.Birthday).First();
        }

        public int MiddleAge()
        {
            int allYears = 0;
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Client client in _collection)
            {
                int years = today.Year - client.Birthday.Year;
                if (today.Month < client.Birthday.Month && today.Day < client.Birthday.Day)
                    years--;

                allYears += years;
            }

            int countClients = _collection.Count;
            return allYears / (countClients > 0 ? countClients : 0);
        }

        public int Count() => _collection.Count;
    }
}
