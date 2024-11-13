
namespace BankSystem.Domain.Models
{
    public class Client : Person
    {
        public IEnumerable<Account>? Accounts { get; set; }
        public Guid ManagerId { get; set; }
        public Employee Manager { get; set; }
    }
}
