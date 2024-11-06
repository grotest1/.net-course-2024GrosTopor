using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.HttpData
{
    public class CurrencyConversionsResponce
    {
        public int Error { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; } = "";
        public float Amount { get; set; }
    }
}
