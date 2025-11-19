using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Validators;

namespace Finan.Service.Services
{
    public class ClassificationService : BaseService, IClassificationService
    {
        private readonly IClassificationRepository _baseRepository;

        public ClassificationService(IClassificationRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ClassificationDTO?> AddClassification(ClassificationCommand classificationCommand)
        {
            if (!Validate(classificationCommand, new ClassificationValidator()))
                return null;

            var classification = new Classification
            {
                Description = classificationCommand.Description,
                GroupId = classificationCommand.GroupId
            };

            await _baseRepository.Insert(classification);

            var result = await _baseRepository.GetClassificationByIdAsync(classification.Id);

            return new ClassificationDTO
            {
                Id = result.Id,
                Description = result.Description,
                GroupId = result.GroupId
            };
        }

        public async Task<ClassificationDTO?> UpdateClassification(ClassificationCommand classificationCommand)
        {
            if (!Validate(classificationCommand, new ClassificationValidator()))
                return null;

            var classification = await _baseRepository.GetClassificationByIdAsync(classificationCommand.Id!.Value);

            if (classification == null)
            {
                Messages.Error("Conta não encontrada.");
                return null;
            }

            classification.Description = classificationCommand.Description;
            classification.GroupId = classificationCommand.GroupId;

            await _baseRepository.Update(classification);

            return new ClassificationDTO
            {
                Id = classification.Id,
                Description = classification.Description,
                GroupId = classification.GroupId
            };

        }

        public async Task<List<ClassificationDTO>?> GetClassificationsByGroupIdAsync(int GroupId)
        {
            var result = await _baseRepository.GetClassificationsByGroupIdAsync(GroupId);

            if (result == null)
                return null;

            return result.Select(x => new ClassificationDTO
            {
                Id = x.Id,
                Description = x.Description,
                GroupId = x.Group != null ? x.Group.Id : 0,
                GroupName = x.Group != null ? x.Group.Description : String.Empty
            }).ToList();
        }

        public async Task<ClassificationDTO?> GetClassificationByIdAsync(int id)
        {
            var result = await _baseRepository.GetClassificationByIdAsync(id);

            if (result == null)
                return null;

            return new ClassificationDTO
            {
                Id = result.Id,
                Description = result.Description,
                GroupId = result.GroupId,
                GroupName = result.Group?.Description
            };
        } 

        public async Task<List<ClassificationDTO>?> GetClassificationsAsync() {

            var result = await _baseRepository.GetClassificationsAsync();

            if (!result.Any())
                return null;

            return result.Select(x => new ClassificationDTO
            {
                Id = x.Id,
                Description = x.Description,
                GroupId = x.GroupId,
                GroupName = x.Group?.Description
            }).ToList();
        }
  
        public async Task<PagedResult<ClassificationDTO>?> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5) 
        {
            var result = await _baseRepository.GetClassificationsAsync(pageNumber, pageSize);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
            {
                Messages.Error("Classificação não encontrada.");
                return;
            };

            await _baseRepository.Delete(id);
        }
    }
}
