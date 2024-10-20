using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorageEF : IClientStorage, IDisposable
    {
        private readonly BankSystemDbContext db = new BankSystemDbContext();

        public void Add(Client client)
        {
            db.Clients.Add(client);
            db.SaveChanges();
        }

        public void Update(Client client)
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
        }

        public void Delete(Client client)
        {
            db.Clients.Remove(client);
            db.SaveChanges();
        }

        public List<Client> Get(Func<Client, bool> filter)
        {
            return db.Clients.Where(filter).ToList();
        }

        public void AddAccount(Client client, Account account)
        {
            account.Client = client;
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            Account? findAccount = db.Accounts.FirstOrDefault(a => a.Id == account.Id);
            if (findAccount != null)
            {
                findAccount.Amount = account.Amount;
                db.SaveChanges();
            }
        }

        public void DeleteAccount(Account account)
        {
            db.Accounts.Remove(account);
        }

        public List<Account> GetAccount(Func<Account, bool> filter)
        {
            return db.Accounts.Where(filter).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
