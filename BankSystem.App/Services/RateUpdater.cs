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
            const int DAYMS = 24 * 60 * 60 * 1000;

            while (true)
            {
                try
                {
                    token.ThrowIfCancellationRequested();

                    List<Account> accounts = _clientStorage.GetAccountAsync(a => a.DateOpen.Day == DateTime.Now.Day).Result;

                    Parallel.ForEach<Account>(accounts, (account) => account.Amount += account.Amount * 2 / 100);

                    await Task.Delay(DAYMS, token);
                }
                catch (Exception ex)
                {
                    token.ThrowIfCancellationRequested();
                }
            }
        }
        
    }
}
