using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using FluentValidation;

namespace Finan.Service.Services
{
    public class AccountService : BaseContractService<Account>, IAccountService
    {
        private readonly IAccountRepository _baseRepository;

        public AccountService(IAccountRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<AccountDTO> AddAccount<AccountValidator>(AccountCommand AccountParameter)
        {
            var Account = new Account
            {
                BankId = AccountParameter.BankId,
                Name = AccountParameter.Name,
                Agency = AccountParameter.Agency,
                Number = AccountParameter.Number,
                CreditLimit = AccountParameter.CreditLimit
            };

            await _baseRepository.Insert(Account);

            var result = await _baseRepository.GetAccountByIdAsync(Account.Id);

            return new AccountDTO
            {
                Id = result.Id,
                BankId = result.BankId,
                BankName = result.Bank?.Name,
                Name = result.Name,
                Agency = result.Agency,
                Number = result.Number,
                CreditLimit = result.CreditLimit,
                Balance = result.Balance
            };
        }

        public async Task<AccountDTO?> GetAccountByIdAsync(int id)
        {
            var result = await _baseRepository.GetAccountByIdAsync(id);

            if (result == null)
                return null;

            return new AccountDTO
            {
                Id = result.Id,
                BankId = result.BankId,
                BankName = result.Bank?.Name,
                Name = result.Name,
                Agency = result.Agency,
                Number = result.Number,
                CreditLimit = result.CreditLimit,
                Balance = result.Balance
            };
        }

        public async Task<IEnumerable<AccountDTO>?> GetAccountsAsync()
        {

            var result = await _baseRepository.GetAccountsAsync();

            if (!result.Any())
                return null;

            return result.Select(x => new AccountDTO
            {
                Id = x.Id,
                BankId = x.BankId,
                BankName = x.Bank?.Name,
                Name = x.Name,
                Agency = x.Agency,
                Number = x.Number,
                CreditLimit = x.CreditLimit,
                Balance = x.Balance
            });
        }

        public async Task<PagedResult<AccountDTO>> GetAccountsAsync(int pageNumber = 1, int pageSize = 5) => await _baseRepository.GetAccountsAsync(pageNumber, pageSize);


        public async Task<AccountDTO> UpdateAccount<AccountValidator>(AccountCommand AccountParameter)
        {
            var account = _baseRepository.GetAll().Where(x => x.Id == AccountParameter.Id).FirstOrDefault();

            account.BankId = AccountParameter.BankId;
            account.Name = AccountParameter.Name;
            account.Agency = AccountParameter.Agency;
            account.Number = AccountParameter.Number;
            account.CreditLimit = AccountParameter.CreditLimit;

            await _baseRepository.Update(account);

            return new AccountDTO
            {
                Id = account.Id,
                BankId = account.BankId,
                BankName = account.Bank?.Name,
                Name = account.Name,
                Agency = account.Agency,
                Number = account.Number,
                CreditLimit = account.CreditLimit,
                Balance = account.Balance
            };

        }
    }
}
