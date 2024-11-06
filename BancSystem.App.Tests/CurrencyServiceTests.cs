using BankSystem.App.Services;
using Xunit;

using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.Data.Storages;

namespace BancSystem.App.Tests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async Task CurrencyConversionAsyncPositiveTestAsync1()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            float result = await CurrencyService.CurrencyConversionAsync("USD", "USD", 100, token);

            Assert.Equal(100, result);
        }

        [Fact]
        public async Task CurrencyConversionAsyncPositiveTestAsync2()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            float result = await CurrencyService.CurrencyConversionAsync("USD", "MDL", 100, token);

            Assert.True(result > 1700);
        }

        [Fact]
        public async Task CurrencyConversionAsyncNegativeTestAsync()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            await Assert.ThrowsAsync<HttpRequestException>(() => CurrencyService.CurrencyConversionAsync("USDDDD", "MDLLLLL", 100, token));
        }

    }
}
