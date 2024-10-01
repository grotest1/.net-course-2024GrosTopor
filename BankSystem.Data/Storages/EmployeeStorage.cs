using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage
    {
        public readonly List<Employee> collection = new List<Employee>();

        public void Add(Employee employee)
        {
            collection.Add(employee);
        }
    }
}
