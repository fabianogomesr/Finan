namespace Finan.Contracts.Response
{
    public class AccountResponse
    {
        public int? Id { get; set; }
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? Name { get; set; }
        public string? Agency { get; set; }
        public string? Number { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal? Balance { get; set; }
    }
}
