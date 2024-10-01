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

        public void Add(Client client) => _collection.Add(client);

        public IOrderedEnumerable<Client> OrderByBirthday() => _collection.OrderBy(c => c.Birthday);

        public int Count() => _collection.Count;

        public List<Client> ToList() => [.. _collection];
    }
}
