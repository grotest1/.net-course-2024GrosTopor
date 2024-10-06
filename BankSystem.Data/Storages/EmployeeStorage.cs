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

        public void Add(Employee employee)
        {
            _collection.Add(employee);
        }
        public void AddRange(IEnumerable<Employee> collection)
        {
            _collection.AddRange(collection);
        }

        public Employee Min<TKey>(Func<Employee, TKey> keySelector)
        {
              return _collection.OrderBy(keySelector).First();
        }

        public Employee Max<TKey>(Func<Employee, TKey> keySelector)
        {
            return _collection.OrderByDescending(keySelector).First();
        }

        public Employee? GetEmployee(Func<Employee, bool> predicate)
        {
            return _collection.Where(predicate).FirstOrDefault();
        }
        public List<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return _collection.Where(predicate).ToList();
        }

        public int Sum(Func<Employee, int> selector)
        {
            return _collection.Sum(selector);
        }
        public int Count() => _collection.Count;
    }
}
