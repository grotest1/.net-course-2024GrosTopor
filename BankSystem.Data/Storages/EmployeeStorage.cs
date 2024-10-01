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

        public void Add(Employee employee) => _collection.Add(employee);

        public IOrderedEnumerable<Employee> OrderByBirthday() => _collection.OrderBy(c => c.Birthday);

        public int Count() => _collection.Count;

        public List<Employee> ToList() => [.. _collection];
    }
}
