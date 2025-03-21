using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IFinancialClassificationRepository : IBaseRepository<FinancialClassification>
    {
        Task<FinancialClassification> GetFinancialClassificationByIdAsync(int id);
        Task<IEnumerable<FinancialClassification>> GetFinancialClassificationsAsync();
    }
}
