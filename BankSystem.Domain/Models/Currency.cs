
namespace BankSystem.Domain.Models
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public IEnumerable<Account>? Accounts { get; set; }
    }
}
