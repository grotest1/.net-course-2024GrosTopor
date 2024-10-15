using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public interface IClientStorage : IStorage<Client>
    {
        public void AddAccount(Account account);
        public void UpdateAccount(Account account);
        public void DeleteAccount(Account account);
        public List<Account> GetAccount(Func<Account, bool> filter);
    }
}
