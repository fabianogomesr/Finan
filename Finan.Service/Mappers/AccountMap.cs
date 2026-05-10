using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Entities;

namespace Finan.Application.Mappers
{
    public class AccountMap
    {
        public static AccountResponse EntityToDto(Account account)
        {
            return new AccountResponse
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

        public static Account CommandToEntity(AccountRequest accountCommand, Account? account = null)
        {
            if (account == null)
            {
                account = new Account
                {
                    BankId = accountCommand.BankId,
                    Name = accountCommand.Name,
                    Agency = accountCommand.Agency,
                    Number = accountCommand.Number,
                    CreditLimit = accountCommand.CreditLimit
                };
            }
            else
            {
                account.BankId = accountCommand.BankId;
                account.Name = accountCommand.Name;
                account.Agency = accountCommand.Agency;
                account.Number = accountCommand.Number;
                account.CreditLimit = accountCommand.CreditLimit;
            }

            return account;
        }

    }
}
