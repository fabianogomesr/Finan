using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
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
        Task<PagedResult<ClassificationDTO>> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5);

    }
}
