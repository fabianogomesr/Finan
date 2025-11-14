using Finan.Domain.Commands;
using Finan.Domain.DTOs;
using Finan.Domain.Entities;

namespace Finan.Service.Mappers
{
    public class AccountMap
    {
        public static AccountDTO EntityToDto(Account account)
        {
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

        public static Account CommandToEntity(AccountCommand accountCommand, Account? account = null)
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
