namespace Finan.Domain.Interfaces
{
    public interface IUserContext
    {
        Guid TenantId { get; }
        string UserName { get; }
    }
}
