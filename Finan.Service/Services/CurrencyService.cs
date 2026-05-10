using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Application.Validators;

namespace Finan.Application.Services
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        private readonly ICurrencyRepository _baseRepository;

        public CurrencyService(ICurrencyRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<CurrencyResponse?> CreateAsync(CurrencyRequest currencyCommand)
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

            return new CurrencyResponse 
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                Symbol = result.Symbol
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

        public async Task<List<CurrencyResponse>?> GetAsync()
        {
            var result = await _baseRepository.Select();

            if (!result.Any())
                return null;

            return result.Select(x =>
                new CurrencyResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Symbol = x.Symbol
                }).ToList();
        }

        public async Task<PagedResult<CurrencyResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _baseRepository.GetBanksAsync(pageNumber, pageSize);

            var items = result.Items.Select(x => new CurrencyResponse
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Symbol = x.Symbol
            });

            return result;
        }

        public async Task<CurrencyResponse?> GetByIdAsync(int id)
        {
            var result = await _baseRepository.Select(id);

            if (result == null)
                return null;

            return new CurrencyResponse
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                Symbol = result.Symbol
            };
        }

        public async Task<CurrencyResponse?> UpdateAsync(CurrencyRequest currencyCommand)
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
            currency.Symbol = currencyCommand.Symbol;

            await _baseRepository.Update(currency);

            return new CurrencyResponse
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Symbol = currency.Symbol
            };
        }
    }
}
