using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Application.Validators;

namespace Finan.Application.Services
{
    public class BankService : BaseService, IBankService
    {
        private readonly IBankRepository _baseRepository;

        public BankService(IBankRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<BankResponse?> CreateAsync(BankRequest bankCommand)
        {
            if (!Validate(bankCommand, new BankValidator()))
                return null;

            var bank = new Bank
            {
                Name = bankCommand.Name,
                Code = bankCommand.Code
            };

            await _baseRepository.Insert(bank);

            var result = await _baseRepository.Select(bank.Id);

            return new BankResponse
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code
            };
        }

        public async Task<BankResponse?> UpdateAsync(BankRequest bankCommand)
        {

            if (!Validate(bankCommand, new BankValidator()))
                return null;

            var result = await _baseRepository.Select(bankCommand.Id);

            if (result == null)
            {
                Messages.Error("Banco não encontrada.");
                return null;
            }

            result.Name = bankCommand.Name;
            result.Code = bankCommand.Code;

            await _baseRepository.Update(result);

            return new BankResponse
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code
            };
        }

        public async Task<BankResponse?> GetByIdAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
                return null;

            return new BankResponse
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code
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

        public async Task<List<BankResponse>?> GetAsync()
        {

            var result = await _baseRepository.Select();

            if (!result.Any())
                return null;

            return result.Select(x => 
                new BankResponse 
                { 
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                }
            ).ToList();
        }

        public async Task<PagedResult<BankResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _baseRepository.GetBanksAsync(pageNumber, pageSize);

            return result;
        }
    }
}

