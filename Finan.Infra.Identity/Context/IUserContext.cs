namespace Finan.Infra.Identity.Context
{
    public interface IUserContext
    {
        Guid TenantId { get; }
        string UserName { get; }
    }
}
