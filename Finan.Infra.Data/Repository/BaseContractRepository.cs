using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Finan.Infra.Data.Repository
{
    public class BaseContractRepository<TEntity> : IBaseContractRepository<TEntity> where TEntity : BaseContractEntity
    {
        protected readonly BaseContext _dbSet;
        private readonly IUserContext _userContext;

        public BaseContractRepository(BaseContext mySqlContext, IUserContext userContext)
        {
            _userContext = userContext;
            _dbSet = mySqlContext;
        }

        public async Task Insert(TEntity obj)
        {
            obj.ContractId = _userContext.ContractId;
            _dbSet.Set<TEntity>().Add(obj);
            await _dbSet.SaveChangesAsync();
        }

        public async Task Insert(List<TEntity> obj)
        {
            foreach (var item in obj)
            {
                item.ContractId = _userContext.ContractId;
            }
            _dbSet.Set<TEntity>().AddRange(obj);
            await _dbSet.SaveChangesAsync();
        }

        public async Task Update(TEntity obj)
        {
            _dbSet.Entry(obj).State = EntityState.Modified;
            await _dbSet.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var obj = _dbSet.Set<TEntity>().Where(x => x.Id == id && (x.ContractId == _userContext.ContractId || x.ContractId == 0)).First();

            if (obj != null)
            {
                _dbSet.Set<TEntity>().Remove(obj);
                await _dbSet.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> Select() => await _dbSet.Set<TEntity>().Where(x => x.ContractId == _userContext.ContractId || x.ContractId == 0).ToListAsync();

        public IQueryable<TEntity> GetAll() => _dbSet.Set<TEntity>().Where(x => x.ContractId == _userContext.ContractId || x.ContractId == 0).AsQueryable();

        public async Task<PagedResult<TEntity>> Select(int pageNumber, int pageSize) => await _dbSet.Set<TEntity>().Where(x => x.ContractId == _userContext.ContractId || x.ContractId == 0).ToPagedListAsync(pageNumber, pageSize);

        public async Task<TEntity> Select(int id) => await _dbSet.Set<TEntity>().Where(x => (x.ContractId == _userContext.ContractId || x.ContractId == 0) && x.Id == id).FirstAsync();

    }
}
