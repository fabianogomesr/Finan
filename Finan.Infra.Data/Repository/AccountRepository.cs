using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
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

        public async Task<EntityPagination<Account>> GetAccountsAsync(int pageNumber = 1, int pageSize = 5)
        {
            var entities = await _dbSet.Set<Account>().AsQueryable()
            .Include(x => x.Bank)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var totalItems = await _dbSet.Set<Account>().CountAsync();

            return new EntityPagination<Account>
            {
                Entities = entities,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
