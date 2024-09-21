using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        private int _personalAccount = 0;
        public int PersonalAccount { get { return _personalAccount; } }


        public void AddAccount(int summ)
        {
            _personalAccount += summ;
        }

    }
}
