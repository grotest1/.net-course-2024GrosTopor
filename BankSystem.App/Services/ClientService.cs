using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private IClientStorage _clientStorage;
        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public void AddClient(Client client)
        {
            
            if (string.IsNullOrEmpty(client.Name))
                throw new EmptyRequiredDataException("Name");
            else if (client.Age < 18)
                throw new UnderAgeException(client.Age);
            else if (string.IsNullOrEmpty(client.Passport))
                throw new EmptyRequiredDataException("Passport");

            Account defaultAccount = new Account() { Currency = new Currency() { Code = 840, Name = "USD" }, Client = client };
            
            _clientStorage.Add(client);
            _clientStorage.AddAccount(client, defaultAccount);
        }

        public void UpdateClient(Client client)
        {
            if (GetClient(client.Id) == null)
                throw new MissingDataException("Клиент не найден");

            _clientStorage.Update(client);
        }

        public void DeleteClient(Client client)
        {
            _clientStorage.Delete(client);
        }


        public Client? GetClient(Guid clientId)
        {
            return _clientStorage.Get(c => c.Id == clientId).FirstOrDefault();
        }

        public List<Client> GetClients(Func<Client, bool> predicate)
        {
            return _clientStorage.Get(predicate);
        }

        public void AddAccount(Client client, Account account)
        {
            if (account.Currency.Code == 0)
                throw new EmptyRequiredDataException("Currency.Code");
            else if (string.IsNullOrEmpty(account.Currency.Name))
                throw new EmptyRequiredDataException("Currency.Name");

            _clientStorage.AddAccount(client, account);
        }
        public void UpdateAccount(Account account)
        {
            if (_clientStorage.GetAccount(a => a.Id == account.Id).Count == 0)
                throw new MissingDataException("Лицевой счет не найден");

            _clientStorage.UpdateAccount(account);
        }

        public Account? GetClientAccount(Guid idAccount)
        {
            return _clientStorage.GetAccount(a => a.Id == idAccount).FirstOrDefault();
        }
    }
}
