using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IFinancialClassificationService : IBaseService<FinancialClassification>
    {
        Task<FinancialClassificationDTO> AddFinancialClassification(FinancialClassificationCommand financialClassificationParameter);
        Task<FinancialClassificationDTO> GetFinancialClassificationByIdAsync(int id);
        Task<IEnumerable<FinancialClassificationDTO>> GetFinancialClassificationsAsync();
        Task<FinancialClassificationPaginationDTO> GetFinancialClassificationsAsync(int pageNumber = 1, int pageSize = 5);
        List<ClassificationTypeDTO>GetClassificationTypeList();
        Task<FinancialClassificationDTO> UpdateFinancialClassification(FinancialClassificationCommand financialClassificationParameter);
    }
}
