using Finan.Contracts.Messages;

namespace Finan.Domain.Interfaces
{
    public interface IBaseService
    {
        MessageCollection Messages { get; }
    }
}
