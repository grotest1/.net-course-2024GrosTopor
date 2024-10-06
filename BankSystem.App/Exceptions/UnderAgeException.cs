using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Exceptions
{
    public class UnderAgeException : Exception
    {
        private static int _value;
        public UnderAgeException(int value) : base($"Минимальный возраст - 18 лет. Текущий возраст - {_value}")
        {
            _value = value;
        }
    }
}
