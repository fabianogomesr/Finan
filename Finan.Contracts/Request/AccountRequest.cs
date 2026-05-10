namespace Finan.Contracts.Request
{
    public class AccountRequest
    {
        public int Id { get; set; }
        public int BankId { get; set; }
        public string? Name { get; set; }
        public string? Agency { get; set; }
        public string? Number { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
