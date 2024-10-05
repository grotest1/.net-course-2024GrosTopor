using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        public int Amount { get; set; }
    }
}
