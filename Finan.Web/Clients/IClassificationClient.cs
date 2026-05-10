using Finan.Contracts.Request;
using Finan.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Web.Clients
{
    public interface IClassificationClient
    {
        Task<ApiResponse<List<ClassificationResponse>>> GetAllAsync();
        Task<ApiResponse<PagedResponse<ClassificationResponse>>> GetPageAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<ClassificationResponse>> GetAsync(int id);
        Task<ApiResponse<ClassificationResponse>> CreateAsync(ClassificationRequest Classification);
        Task<ApiResponse<ClassificationResponse>> UpdateAsync(ClassificationRequest Classification);
        Task DeleteAsync(int id);
        Task<ApiResponse<List<FinancialTypeResponse>>> GetFinancialTypeList();
        Task<ApiResponse<List<ClassificationResponse>>> GetClassificationsFromReceivableByGroupIdAsync(int GroupId);
        Task<ApiResponse<List<ClassificationResponse>>> GetClassificationsFromTransactionByGroupIdAsync(int GroupId);
    }
}
