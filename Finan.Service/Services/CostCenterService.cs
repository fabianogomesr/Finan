using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Service.Validators;

namespace Finan.Service.Services
{
    public class CostCenterService : BaseService, ICostCenterService
    {
        private readonly ICostCenterRepository _baseRepository;
        public CostCenterService(ICostCenterRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CostCenterDTO?> CreateAsync(CostCenterCommand costCenterCommand)
        {
            if (!Validate(costCenterCommand, new CostCenterValidator()))
                return null;

            var bank = new CostCenter
            {
                Description = costCenterCommand.Description
            };

            await _baseRepository.Insert(bank);

            var result = await _baseRepository.Select(bank.Id);

            return new CostCenterDTO
            {
                Id = result.Id,
                Description = result.Description
            };
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
            {
                Messages.Error("Conta não encontrada.");
                return;
            }

            await _baseRepository.Delete(id);
        }

        public async Task<List<CostCenterDTO>?> GetAsync()
        {
            var result = await _baseRepository.Select();

            if (!result.Any())
                return null;

            return result.Select(x =>
            new CostCenterDTO
            {
                Id = x.Id,
                Description = x.Description
            }).ToList();
        }

        public async Task<PagedResult<CostCenterDTO>?> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _baseRepository.GetBanksAsync(pageNumber, pageSize);

            return result;
        }

        public async Task<CostCenterDTO?> GetByIdAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
                return null;

            return new CostCenterDTO
            {
                Id = result.Id,
                Description = result.Description
            };
        }

        public async Task<CostCenterDTO?> UpdateAsync(CostCenterCommand costCenterCommand)
        {
            if (!Validate(costCenterCommand, new CostCenterValidator()))
                return null;

            var costCenter = await _baseRepository.Select(costCenterCommand.Id);

            if (costCenter == null)
            {
                Messages.Error("Centro de custo não encontrado.");
                return null;
            }

            costCenter.Description = costCenterCommand.Description;

            await _baseRepository.Update(costCenter);

            return new CostCenterDTO
            {
                Id = costCenter.Id,
                Description = costCenter.Description
            };
        }
    }
}
