using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public interface IClientStorage : IStorage<Client>
    {
        public Task AddAccountAsync(Client client, Account account);
        public Task UpdateAccountAsync(Account account);
        public Task DeleteAccountAsync(Account account);
        public Task<List<Account>> GetAccountAsync(Func<Account, bool> filter);
    }
}
