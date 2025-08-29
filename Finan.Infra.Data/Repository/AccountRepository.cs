using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        protected readonly BaseContext _dbSet;

        public AccountRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<Account> GetAccountByIdAsync(int id) => await _dbSet.Set<Account>().Include(x => x.Bank).FirstAsync(x => x.Id == id);
        public async Task<IEnumerable<Account>> GetAccountsAsync() => await _dbSet.Set<Account>().Include(x => x.Bank).ToListAsync();
        public async Task<PagedResult<AccountDTO>> GetAccountsAsync(int pageNumber = 1, int pageSize = 5) 
        {
            return _dbSet.Set<Account>().Include(x => x.Bank).Select(x => new AccountDTO
            {
                Id = x.Id,
                BankId = x.BankId,
                BankName = x.Bank.Name,
                Name = x.Name,
                Agency = x.Agency,
                Number = x.Number,
                CreditLimit = x.CreditLimit,
                Balance = x.Balance
            }).ToPagedList(pageNumber, pageSize);
        } 

    }
}
