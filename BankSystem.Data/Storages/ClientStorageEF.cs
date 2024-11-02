using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorageEF : IClientStorage, IDisposable
    {
        private readonly BankSystemDbContext db = new BankSystemDbContext();

        public async Task AddAsync(Client client)
        {
            await Task.Run(() =>
            {
                db.Clients.Add(client);
                db.SaveChanges();
            });
        }

        public async Task UpdateAsync(Client client)
        {
            await Task.Run(() =>
            {
                Client? findClient = db.Clients.FirstOrDefault(c => c.Id == client.Id);
                if (findClient != null)
                {
                    findClient.Age = client.Age;
                    findClient.Name = client.Name;
                    findClient.Surname = client.Surname;
                    findClient.Passport = client.Passport;
                    findClient.PersonalPhoneNumber = client.PersonalPhoneNumber;

                    db.SaveChanges();
                }
            });
        }

        public async Task DeleteAsync(Client client)
        {
            await Task.Run(() =>
            {
                db.Clients.Remove(client);
                db.SaveChanges();
            });
        }

        public async Task<List<Client>> GetAsync(Func<Client, bool> filter)
        {
            return await Task.Run(() => db.Clients.Where(filter).ToList());
        }

        public async Task AddAccountAsync(Client client, Account account)
        {
            await Task.Run(() =>
            {
                account.Client = client;
                db.Accounts.Add(account);
                db.SaveChanges();
            });
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await Task.Run(() =>
            {
                Account? findAccount = db.Accounts.FirstOrDefault(a => a.Id == account.Id);
                if (findAccount != null)
                {
                    findAccount.Amount = account.Amount;
                    db.SaveChanges();
                }
            });
        }

        public async Task DeleteAccountAsync(Account account)
        {
            await Task.Run(() => db.Accounts.Remove(account));
        }

        public async Task<List<Account>> GetAccountAsync(Func<Account, bool> filter)
        {
            return await Task.Run(() => db.Accounts.Where(filter).ToList());
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
