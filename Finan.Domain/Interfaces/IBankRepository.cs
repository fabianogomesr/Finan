using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IBankRepository : IBaseRepository<Bank>
    {
        Task<PagedResult<BankDTO>> GetBanksAsync(int pageNumber = 1, int pageSize = 5);
    }
}
