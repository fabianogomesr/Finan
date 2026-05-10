
using Finan.Contracts.Request;
using Finan.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Web.Clients
{
    public interface ICurrencyClient
    {
        Task<ApiResponse<List<CurrencyResponse>>> GetAllAsync();
        Task<ApiResponse<PagedResponse<CurrencyResponse>>> GetPageAsync(int pageNumber = 1, int pageSize = 10);
        Task<ApiResponse<CurrencyResponse>> GetAsync(int id);
        Task<ApiResponse<CurrencyResponse>> CreateAsync(CurrencyRequest Currency);
        Task<ApiResponse<CurrencyResponse>> UpdateAsync(CurrencyRequest Currency);
        Task DeleteAsync(int id);
    }
}
