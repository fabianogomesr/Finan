using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Service.Validators;

namespace Finan.Service.Services
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        private readonly ICurrencyRepository _baseRepository;

        public CurrencyService(ICurrencyRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<CurrencyDTO?> CreateAsync(CurrencyCommand currencyCommand)
        {
            if (!Validate(currencyCommand, new CurrencyValidator()))
                return null;

            var currency = new Currency
            {
                Name = currencyCommand.Name,
                Code = currencyCommand.Code,
                Symbol = currencyCommand.Symbol
            };

            await _baseRepository.Insert(currency);

            var result = await _baseRepository.Select(currency.Id);

            return new CurrencyDTO 
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
                Messages.Error("Moeda não encontrada.");
                return;
            }

            await _baseRepository.Delete(id);

        }

        public async Task<List<CurrencyDTO>?> GetAsync()
        {
            var result = await _baseRepository.Select();

            if (!result.Any())
                return null;

            return result.Select(x =>
                new CurrencyDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code
                }).ToList();
        }

        public async Task<PagedResult<CurrencyDTO>?> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _baseRepository.GetBanksAsync(pageNumber, pageSize);

            var items = result.Items.Select(x => new BankDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            });

            return result;
        }

        public async Task<CurrencyDTO?> GetByIdAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
                return null;

            return new CurrencyDTO
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code
            };
        }

        public async Task<CurrencyDTO?> UpdateAsync(CurrencyCommand currencyCommand)
        {
            if (!Validate(currencyCommand, new CurrencyValidator()))
                return null;

            var currency = await _baseRepository.Select(currencyCommand.Id);

            if (currency == null)
            {
                Messages.Error("Moeda não encontrada.");
                return null;
            }

            currency.Name = currencyCommand.Name;
            currency.Code = currencyCommand.Code;

            await _baseRepository.Update(currency);

            return new CurrencyDTO
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code
            };
        }
    }
}
