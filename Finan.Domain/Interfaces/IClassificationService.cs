using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Parameters;

namespace Finan.Domain.Interfaces
{
    public interface IClassificationService : IBaseService
    {
        Task<ClassificationDTO?> AddClassification(ClassificationCommand ClassificationParameter);
        Task<ClassificationDTO?> GetClassificationByIdAsync(int id);
        Task<List<ClassificationDTO>?> GetClassificationsAsync();
        Task<PagedResult<ClassificationDTO>?> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5);
        Task<List<ClassificationDTO>?> GetClassificationsByGroupIdAsync(int groupId);
        Task<ClassificationDTO?> UpdateClassification(ClassificationCommand ClassificationParameter);
        Task DeleteAsync(int id);
    }
}
