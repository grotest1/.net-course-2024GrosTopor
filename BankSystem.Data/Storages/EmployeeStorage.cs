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
    public class EmployeeStorage
    {
        private readonly List<Employee> _collection = [];

        public void AddRange(IEnumerable<Employee> collection) => _collection.AddRange(collection);

        public Employee? FirstOrderBy<TKey>(Func<Employee, TKey> keySelector, bool Descending = false)
        {
            if (Descending)
            {
                return _collection.OrderByDescending(keySelector).FirstOrDefault();
            }
            else
            {
                return _collection.OrderBy(keySelector).FirstOrDefault();
            }
        }

        public int MiddleAge()
        {
            int countClients = _collection.Count;
            return _collection.Sum(c => c.Age) / (countClients > 0 ? countClients : 0);
        }

        public int Count() => _collection.Count;
    }
}
