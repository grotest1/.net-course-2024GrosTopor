using BankSystem.Domain.Models;
using BankSystem.Data.Storages;

namespace BankSystem.App.Services
{
    public class RateUpdater
    {
        private IClientStorage _clientStorage;

        public RateUpdater(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public async Task CalculateInterestRateAsync(CancellationToken token) 
        {
            while (true)
            {
                List<Account> accounts = _clientStorage.GetAccount(a => a.DateOpen.Day == DateTime.Now.Day);

                Parallel.ForEach<Account>(accounts, (account) => account.Amount += account.Amount * 2 / 100);
                
                await Task.Delay(24*60*60*1000, token);
            }
        }
        
    }
}
