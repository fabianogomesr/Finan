using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Parameters;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IClassificationService : IBaseService<Classification>
    {
        Task<ClassificationDTO> AddClassification<ClassificationValidator>(ClassificationCommand ClassificationParameter);
        Task<ClassificationDTO> GetClassificationByIdAsync(int id);
        Task<IEnumerable<ClassificationDTO>> GetClassificationsAsync();
        Task<PagedResult<ClassificationDTO>> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5);
        Task<List<ClassificationDTO>> GetClassificationsByGroupIdAsync(int groupId);
        Task<ClassificationDTO> UpdateClassification<ClassificationValidator>(ClassificationCommand ClassificationParameter);
    }
}
