﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        //public Currency Currency { get; set; }
        public string CurrencyName { get; set; } = "";
        public int Amount { get; set; }
        public Guid ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
