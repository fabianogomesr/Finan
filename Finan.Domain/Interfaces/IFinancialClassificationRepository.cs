using Finan.Domain.Entities;
using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IFinancialClassificationRepository : IBaseRepository<FinancialClassification>
    {
        Task<IEnumerable<FinancialClassification>> GetClassificationsFromReceivableByGroupIdAsync(int financialGroupId);
        Task<FinancialClassification> GetFinancialClassificationByIdAsync(int id);
        Task<IEnumerable<FinancialClassification>> GetFinancialClassificationsAsync();
        Task<EntityPagination<FinancialClassification>> GetFinancialClassificationsAsync(int pageNumber = 1, int pageSize = 5);

    }
}
