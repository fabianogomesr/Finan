using Finan.Domain.DTOs;
using Finan.Domain.Entities;
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
                Type = (Domain.Enums.FinancialType)financialClassificationParameter.Type,
                FinancialGroup = financialGroup
            };

            await _baseRepository.Insert(financialClassification);

            var result = await _baseRepository.GetFinancialClassificationByIdAsync(financialClassification.Id);

            return new FinancialClassificationDTO
            {
                Id = result.Id,
                Description = result.Description,
                Type = (byte)result.Type.GetHashCode(),
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
                Type = (byte)result.Type.GetHashCode(),
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
                Type = (byte)x.Type.GetHashCode(),
                FinancialGroupId = x.FinancialGroup.Id
            });
        }

        public async Task<FinancialClassificationDTO> UpdateFinancialClassification(FinancialClassificationCommand financialClassificationParameter)
        {
            var financialClassification = _baseRepository.GetAll().Where(x => x.Id == financialClassificationParameter.Id).FirstOrDefault();
            var financialGroup = _financialGroupRepository.GetAll().Where(x => x.Id == financialClassificationParameter.FinancialGroupId).FirstOrDefault();

            if (financialGroup == null)
                throw new Exception("Grupo financeiro não encontrado");

            financialClassification.Description = financialClassificationParameter.Description;
            financialClassification.Type = (Domain.Enums.FinancialType)financialClassificationParameter.Type;
            financialClassification.FinancialGroup = financialGroup;

            await _baseRepository.Update(financialClassification);

            return new FinancialClassificationDTO
            {
                Id = financialClassification.Id,
                Description = financialClassification.Description,
                Type = (byte)financialClassification.Type,
                FinancialGroupId = financialClassification.FinancialGroup.Id
            };

        }
    }
}
