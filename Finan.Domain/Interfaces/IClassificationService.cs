using Finan.Contracts.Request;
using Finan.Contracts.Response;
using Finan.Domain.Entities;

namespace Finan.Domain.Interfaces
{
    public interface IClassificationService : IBaseService
    {
        Task<ClassificationResponse?> AddClassification(ClassificationRequest ClassificationParameter);
        Task<ClassificationResponse?> GetClassificationByIdAsync(int id);
        Task<List<ClassificationResponse>?> GetClassificationsAsync();
        Task<PagedResult<ClassificationResponse>?> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5);
        Task<List<ClassificationResponse>?> GetClassificationsByGroupIdAsync(int groupId);
        Task<ClassificationResponse?> UpdateClassification(ClassificationRequest ClassificationParameter);
        Task DeleteAsync(int id);
    }
}
