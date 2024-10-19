
namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public IEnumerable<Account>? Accounts { get; set; }
    }
}
