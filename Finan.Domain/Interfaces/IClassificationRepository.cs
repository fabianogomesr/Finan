using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IClassificationRepository : IBaseRepository<Classification>
    {
        Task<IEnumerable<Classification>> GetClassificationsByGroupIdAsync(int GroupId);
        Task<Classification> GetClassificationByIdAsync(int id);
        Task<IEnumerable<Classification>> GetClassificationsAsync();
        Task<PagedResult<ClassificationResponse>> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5);

    }
}
