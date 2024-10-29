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

        public async Task AddClientAsync(Client client)
        {
            
            if (string.IsNullOrEmpty(client.Name))
                throw new EmptyRequiredDataException("Name");
            else if (client.Age < 18)
                throw new UnderAgeException(client.Age);
            else if (string.IsNullOrEmpty(client.Passport))
                throw new EmptyRequiredDataException("Passport");

            Account defaultAccount = new Account() { Currency = new Currency() { Code = 840, Name = "USD" }, Client = client };

            await Task.Run(() => { 
                _clientStorage.Add(client);
                _clientStorage.AddAccount(client, defaultAccount);
            });
        }

        public async Task UpdateClientAsync(Client client)
        {
            if (GetClientAsync(client.Id) == null)
                throw new MissingDataException("Клиент не найден");

            await Task.Run(() => _clientStorage.Update(client));
        }

        public async Task DeleteClientAsync(Client client)
        {
            await Task.Run(() => _clientStorage.Delete(client));
        }


        public async Task<Client?> GetClientAsync(Guid clientId)
        {
            return await Task.Run(() => _clientStorage.Get(c => c.Id == clientId).FirstOrDefault());
        }

        public async Task<List<Client>> GetClientsAsync(Func<Client, bool> predicate)
        {
            return await Task.Run(() => _clientStorage.Get(predicate));
        }

        public async Task AddAccountAsync(Client client, Account account)
        {
            if (account.Currency.Code == 0)
                throw new EmptyRequiredDataException("Currency.Code");
            else if (string.IsNullOrEmpty(account.Currency.Name))
                throw new EmptyRequiredDataException("Currency.Name");

            await Task.Run(() => _clientStorage.AddAccount(client, account));
        }
        public async Task UpdateAccountAsync(Account account)
        {
            if (_clientStorage.GetAccount(a => a.Id == account.Id).Count == 0)
                throw new MissingDataException("Лицевой счет не найден");

            await Task.Run(() => _clientStorage.UpdateAccount(account));
        }

        public async Task<Account?> GetClientAccountAsync(Guid idAccount)
        {
            return await Task.Run(() => _clientStorage.GetAccount(a => a.Id == idAccount).FirstOrDefault());
        }

        public void CashOut(Account account, int summ, CancellationToken token)
        {
            Task.Run(() =>
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                if (summ > account.Amount)
                    throw new RemainerMoneyException(account.Amount, summ);
                
                account.Amount -= summ;
                _clientStorage.UpdateAccount(account);
            }, token);
        }
    }
}
