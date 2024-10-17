
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
