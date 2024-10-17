using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public interface IClientStorage : IStorage<Client>
    {
        public void AddAccount(Client client, Account account);
        public void UpdateAccount(Account account);
        public void DeleteAccount(Account account);
        public List<Account> GetAccount(Func<Account, bool> filter);
    }
}
