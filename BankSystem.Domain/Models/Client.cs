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


        public Client(string name) : base(name) { }

        public void AddAccountBalance(int summ)
        {
            _personalAccount += summ;
        }

        public bool UseAccountBalance(int summ)
        {
            bool result = false;

            if (_personalAccount >= summ)
            {
                _personalAccount -= summ;
                result = true;
            }

            return result;

        }

    }
}
