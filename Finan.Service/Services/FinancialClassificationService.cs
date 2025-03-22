using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Domain.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class FinancialClassificationService : BaseService<FinancialClassification>, IFinancialClassificationService
    {
        private readonly IFinancialClassificationRepository _baseRepository;
        private readonly IBaseRepository<FinancialGroup> _financialGroupRepository;

        public FinancialClassificationService(IFinancialClassificationRepository baseRepository, IBaseRepository<FinancialGroup> financialGroupRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _financialGroupRepository = financialGroupRepository;   
        }

        public async Task<FinancialClassificationDTO> AddFinancialClassification(FinancialClassificationCommand financialClassificationParameter)
        {
            var financialGroup = _financialGroupRepository.GetAll().Where(x => x.Id == financialClassificationParameter.FinancialGroupId).FirstOrDefault();

            if (financialGroup == null)
                throw new Exception("Grupo financeiro não encontrado");

            var financialClassification = new FinancialClassification
            {
                Description = financialClassificationParameter.Description,
                Type = (Domain.Enums.FinancialType)financialClassificationParameter.TypeId,
                FinancialGroup = financialGroup
            };

            await _baseRepository.Insert(financialClassification);

            var result = await _baseRepository.GetFinancialClassificationByIdAsync(financialClassification.Id);

            return new FinancialClassificationDTO
            {
                Id = result.Id,
                Description = result.Description,
                TypeId = (byte)result.Type.GetHashCode(),
                FinancialGroupId = result.FinancialGroup.Id
            };
        }

        public async Task<FinancialClassificationDTO> GetFinancialClassificationByIdAsync(int id)
        {
            var result = await _baseRepository.GetFinancialClassificationByIdAsync(id);

            if (result == null)
                return null;

            return new FinancialClassificationDTO
            {
                Id = result.Id,
                Description = result.Description,
                TypeId = (byte)result.Type.GetHashCode(),
                FinancialGroupId = result.FinancialGroup.Id
            };
        } 

        public async Task<IEnumerable<FinancialClassificationDTO>> GetFinancialClassificationsAsync() {

            var result = await _baseRepository.GetFinancialClassificationsAsync();

            if (!result.Any())
                return null;

            return result.Select(x => new FinancialClassificationDTO
            {
                Id = x.Id,
                Description = x.Description,
                TypeId = (byte)x.Type.GetHashCode(),
                FinancialGroupId = x.FinancialGroup.Id
            });
        }
  
        public async Task<FinancialClassificationPaginationDTO> GetFinancialClassificationsAsync(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _baseRepository.Select(pageNumber, pageSize);

            return new FinancialClassificationPaginationDTO
            {
                FinancialClassifications = result.Entities.Select(x => new FinancialClassificationDTO { 
                    Id = x.Id, 
                    Description = x.Description, 
                    FinancialGroupId = x.FinancialGroup != null ? x.FinancialGroup.Id : 0, 
                    FinancialGroupName = x.FinancialGroup != null ? x.FinancialGroup.Description : String.Empty, 
                    TypeId = (byte)x.Type.GetHashCode(),
                    TypeName = x.Type.GetDescription()
                }).ToList(),
                PageSize = result.PageSize,
                CurrentPage = result.CurrentPage,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }

        public List<FinancialTypeDTO> GetFinancialTypeList()
        {
            var result = EnumExtensions.GetEnumList<FinancialType>();

            return result.Select(x => new FinancialTypeDTO
            {
                Id = x.Value,
                Description = x.Description
            }).ToList();
        }

        public async Task<FinancialClassificationDTO> UpdateFinancialClassification(FinancialClassificationCommand financialClassificationParameter)
        {
            var financialClassification = _baseRepository.GetAll().Where(x => x.Id == financialClassificationParameter.Id).FirstOrDefault();
            var financialGroup = _financialGroupRepository.GetAll().Where(x => x.Id == financialClassificationParameter.FinancialGroupId).FirstOrDefault();

            if (financialGroup == null)
                throw new Exception("Grupo financeiro não encontrado");

            financialClassification.Description = financialClassificationParameter.Description;
            financialClassification.Type = (Domain.Enums.FinancialType)financialClassificationParameter.TypeId;
            financialClassification.FinancialGroup = financialGroup;

            await _baseRepository.Update(financialClassification);

            return new FinancialClassificationDTO
            {
                Id = financialClassification.Id,
                Description = financialClassification.Description,
                TypeId = (byte)financialClassification.Type,
                FinancialGroupId = financialClassification.FinancialGroup.Id
            };

        }
    }
}
