namespace Finan.Domain.Entities
{
    public class Account : BaseEntity
    {
        public Bank? Bank { get; set; }
        public int BankId { get; set; }
        public string? Name { get; set; }
        public string? Agency { get; set; }
        public string? Number { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public List<Statement>? Statements { get; set; }
    }
}
