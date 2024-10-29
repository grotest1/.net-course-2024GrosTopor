
namespace BankSystem.App.Exceptions
{
    public class RemainerMoneyException : Exception
    {
        private static int _value;
        private static int _remainer;
        public RemainerMoneyException(int remainer, int value) : base($"Недостаточно средств. Остаток - {_remainer}, необходимо списать {_value}")
        {
            _value = value;
            _remainer = remainer;
        }
    }
}
