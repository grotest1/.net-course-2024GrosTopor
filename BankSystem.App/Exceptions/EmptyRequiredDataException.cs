
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
