using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.Data.Storages;
using Xunit;
using BankSystem.App.Services;


namespace BankSystem.Data.Tests
{
    public class RateUpdaterTests
    {
        [Fact]
        public async void CalculateInterestRateAsyncTest()
        {
            RateUpdater rateUpdater = new RateUpdater(new ClientStorageEF());

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task = rateUpdater.CalculateInterestRateAsync(token);

            await Task.Delay(1000);

            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
        }
    }
}
