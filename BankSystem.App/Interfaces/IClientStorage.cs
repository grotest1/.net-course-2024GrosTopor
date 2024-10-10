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
        public void AddAccount(Client client, Account account);
        public void UpdateAccount(Client client, Account account);
        public void DeleteAccount(Client client, Account account);
        public List<Account> GetAccount(Client client, Func<Account, bool> filter);
    }
}
