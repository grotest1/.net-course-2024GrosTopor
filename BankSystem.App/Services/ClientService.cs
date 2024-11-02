using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;
using System.Threading;
using AutoMapper;
using BankSystem.App.Dto;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private IClientStorage _clientStorage;
        private readonly IMapper _mapper;

        public ClientService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }        
        
        public ClientService(IClientStorage clientStorage, IMapper mapper) : this(clientStorage)
        {
            _mapper = mapper;
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

            Task.Run(() => _clientStorage.AddAsync(client)).Wait();
            _clientStorage.AddAccountAsync(client, defaultAccount);
        }

        public async Task<Guid> AddClientAsync(ClientDto clientDto)
        {
            Client client = _mapper.Map<Client>(clientDto);
            await Task.Run(() => AddClient(client));
            return client.Id;
        }
        public async Task UpdateClientAsync(ClientDto clientDto)
        {
            Client client = _mapper.Map<Client>(clientDto);
            await Task.Run(() => UpdateClient(client));
        }
        public async Task DeleteClientAsync(Guid id)
        {
            await Task.Run(() =>
            {
                Client? client = GetClient(id);
                DeleteClient(client);
            });
        }

        public void UpdateClient(Client client)
        {
            if (GetClient(client.Id) == null)
                throw new MissingDataException("Клиент не найден");

            _clientStorage.UpdateAsync(client);
        }

        public void DeleteClient(Client client)
        {
            _clientStorage.DeleteAsync(client);
        }


        public Client? GetClient(Guid clientId)
        {
            return _clientStorage.GetAsync(c => c.Id == clientId).Result.FirstOrDefault();
        }

        public ClientDto? GetClientDto(Guid clientId)
        {
            Client? client = GetClient(clientId);
            return _mapper.Map<ClientDto>(client);
        }

        public List<ClientDto> GetClientsDto(int filterAge = 0)
        {
            List<Client> clients = GetClients(c => c.Age >= filterAge);
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public List<Client> GetClients(Func<Client, bool> predicate)
        {
            return _clientStorage.GetAsync(predicate).Result;
        }

        public void AddAccount(Client client, Account account)
        {
            if (account.Currency.Code == 0)
                throw new EmptyRequiredDataException("Currency.Code");
            else if (string.IsNullOrEmpty(account.Currency.Name))
                throw new EmptyRequiredDataException("Currency.Name");

            _clientStorage.AddAccountAsync(client, account);
        }
        public void UpdateAccount(Account account)
        {
            if (_clientStorage.GetAccountAsync(a => a.Id == account.Id).Result.Count == 0)
                throw new MissingDataException("Лицевой счет не найден");

            _clientStorage.UpdateAccountAsync(account);
        }

        public Account? GetClientAccount(Guid idAccount)
        {
            return _clientStorage.GetAccountAsync(a => a.Id == idAccount).Result.FirstOrDefault();
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
                _clientStorage.UpdateAccountAsync(account);
            }, token);
        }
    }
}
