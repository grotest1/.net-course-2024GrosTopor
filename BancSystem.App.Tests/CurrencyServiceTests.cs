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
            float result = await CurrencyService.CurrencyConversionAsync("USD", "USD", 100);

            Assert.Equal(100, result);
        }

        [Fact]
        public async Task CurrencyConversionAsyncPositiveTestAsync2()
        {
            float result = await CurrencyService.CurrencyConversionAsync("USD", "MDL", 100);

            Assert.True(result > 1700);
        }

        [Fact]
        public async Task CurrencyConversionAsyncNegativeTestAsync()
        {
            await Assert.ThrowsAsync<HttpRequestException>(() => CurrencyService.CurrencyConversionAsync("USDDDD", "MDLLLLL", 100));
        }

    }
}
