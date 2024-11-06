using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BankSystem.App.HttpData;

namespace BankSystem.App.Services
{
    public static class CurrencyService
    {
        public static async Task<float> CurrencyConversionAsync(string currencyTitleFrom, string currencyTitleTo, float amount, CancellationToken token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                token.ThrowIfCancellationRequested();

                var parameters = new Dictionary<string, string>();
                parameters.Add("api_key", "YvQNW3U5Q6dtwjXpXPyJmYCpwxbBah");
                parameters.Add("from", currencyTitleFrom);
                parameters.Add("to", currencyTitleTo);
                if (amount != 1)
                    parameters.Add("amount", Convert.ToString(amount));
                
                string queryString = string.Join("&", parameters.Select(p => $"{p.Key}={p.Value}"));
                string fullUri = "https://www.amdoren.com/api/currency.php" + "?" + queryString;

                HttpResponseMessage response = await httpClient.GetAsync(fullUri, token);

                response.EnsureSuccessStatusCode();

                string message = await response.Content.ReadAsStringAsync();

                CurrencyConversionsResponce? currencyConversionsResponce = JsonConvert.DeserializeObject<CurrencyConversionsResponce>(message);

                if (currencyConversionsResponce != null)
                {
                    if (currencyConversionsResponce.Error == 0)
                        return currencyConversionsResponce.Amount;
                    else
                        throw new HttpRequestException(currencyConversionsResponce.ErrorMessage);
                }
                else
                    throw new JsonException("Ошибка конвертации ответа");

            }
        }

    }
}
