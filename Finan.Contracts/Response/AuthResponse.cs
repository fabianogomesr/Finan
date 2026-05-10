namespace Finan.Contracts.Response
{
    public class AuthResponse
    {
        public string? Token { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public int ContractId { get; set; }
    }
}
