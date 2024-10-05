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
            int allYears = 0;
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Employee employee in _collection)
            {
                int years = today.Year - employee.Birthday.Year;
                if (today.Month < employee.Birthday.Month && today.Day < employee.Birthday.Day)
                    years--;

                allYears += years;
            }

            int countEmployees = _collection.Count;
            return allYears / (countEmployees > 0 ? countEmployees : 0);
        }

        public int Count() => _collection.Count;
    }
}
