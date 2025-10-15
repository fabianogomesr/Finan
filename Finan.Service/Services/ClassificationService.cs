using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using Finan.Service.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class ClassificationService : BaseContractService<Classification>, IClassificationService
    {
        private readonly IClassificationRepository _baseRepository;

        public ClassificationService(IClassificationRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<ClassificationDTO> AddClassification<ClassificationValidator>(ClassificationCommand classificationParameter)
        {
            var classification = new Classification
            {
                Description = classificationParameter.Description,
                GroupId = classificationParameter.GroupId
            };

            await _baseRepository.Insert(classification);

            return new ClassificationDTO
            {
                Id = classification.Id,
                Description = classification.Description,
                GroupId = classification.GroupId
            };
        }

        public async Task<List<ClassificationDTO>> GetClassificationsByGroupIdAsync(int GroupId)
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

        public async Task<ClassificationDTO> GetClassificationByIdAsync(int id)
        {
            var result = await _baseRepository.GetClassificationByIdAsync(id);

            if (result == null)
                return null;

            return new ClassificationDTO
            {
                Id = result.Id,
                Description = result.Description,
                GroupId = result.Group.Id
            };
        } 

        public async Task<IEnumerable<ClassificationDTO>> GetClassificationsAsync() {

            var result = await _baseRepository.GetClassificationsAsync();

            if (!result.Any())
                return null;

            return result.Select(x => new ClassificationDTO
            {
                Id = x.Id,
                Description = x.Description,
                GroupId = x.Group.Id
            });
        }
  
        public async Task<PagedResult<ClassificationDTO>> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5) => await _baseRepository.GetClassificationsAsync(pageNumber, pageSize);

        public async Task<ClassificationDTO> UpdateClassification<ClassificationValidator>(ClassificationCommand classificationParameter)
        {
            var classification = _baseRepository.GetAll().Where(x => x.Id == classificationParameter.Id).FirstOrDefault();

            if (classification == null)
                throw new Exception("Classificação não encontrada!");

            classification.Description = classificationParameter.Description;
            classification.GroupId = classificationParameter.GroupId;

            await _baseRepository.Update(classification);

            return new ClassificationDTO
            {
                Id = classification.Id,
                Description = classification.Description,
                GroupId = classification.GroupId
            };

        }
    }
}
