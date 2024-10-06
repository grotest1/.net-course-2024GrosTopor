using Bogus.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Exceptions
{
    public class EmptyRequiredDataException : Exception
    {
        private static string _field = "";
        public EmptyRequiredDataException(string field) : base($"Свойство {_field} обязательно к заполнению")
        {
            _field = field;
        }
    }
}
