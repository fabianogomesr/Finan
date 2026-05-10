using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Application.Mappers;
using Finan.Application.Validators;
using Finan.Contracts.Response;
using Finan.Contracts.Request;

namespace Finan.Application.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountRepository _baseRepository;

        public AccountService(IAccountRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        #region Action
        public async Task<AccountResponse?> CreateAsync(AccountRequest accountCommand)
        {
            if (!Validate(accountCommand, new AccountValidator()))
                return null;

            var account = AccountMap.CommandToEntity(accountCommand);

            await _baseRepository.Insert(account);

            var result = await _baseRepository.GetAccountByIdAsync(account.Id);

            return AccountMap.EntityToDto(result);
        }

        public async Task<AccountResponse?> UpdateAsync(AccountRequest accountCommand)
        {
            if (!Validate(accountCommand, new AccountValidator()))
                return null;

            var account = await _baseRepository.GetAccountByIdAsync(accountCommand.Id);

            if (account == null)
            {
                Messages.Error("Conta não encontrada.");
                return null;
            }
                
            await _baseRepository.Update(AccountMap.CommandToEntity(accountCommand, account));

            return AccountMap.EntityToDto(account);
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


        #endregion

        #region Get

        public async Task<AccountResponse?> GetByIdAsync(int id)
        {
            var result = await _baseRepository.GetAccountByIdAsync(id);

            if (result == null)
                return null;

            return AccountMap.EntityToDto(result);
        }

        public async Task<List<AccountResponse>?> GetAsync()
        {
            var result = await _baseRepository.GetAccountsAsync();

            if (!result.Any())
                return null;

            return result.Select(AccountMap.EntityToDto).ToList();
        }

        public async Task<PagedResult<AccountResponse>?> GetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _baseRepository.GetAccountsAsync(pageNumber, pageSize);

            return result;
        }
        #endregion
    }
}
