using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Data.Storages
{
    public interface IStorage<T>
    {
        public void Add(T item);
        public void Update(T item);
        public void Delete(T item);
        public List<T> Get(Func<T, bool> filter);
    }
}
