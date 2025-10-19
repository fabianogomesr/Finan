using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Finan.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BaseContext _dbSet;

        public BaseRepository(BaseContext mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task Insert(TEntity obj)
        {
            _dbSet.Set<TEntity>().Add(obj);
            await _dbSet.SaveChangesAsync();
        }

        public async Task Insert(List<TEntity> obj)
        {
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
            var obj = _dbSet.Set<TEntity>().Find(id);

            if (obj != null)
            {
                _dbSet.Set<TEntity>().Remove(obj);
                await _dbSet.SaveChangesAsync();
            }  
        }

        public async Task<IEnumerable<TEntity>> Select() => await _dbSet.Set<TEntity>().ToListAsync();

        public IQueryable<TEntity> GetAll() => _dbSet.Set<TEntity>().AsQueryable<TEntity>();

        public async Task<PagedResult<TEntity>> Select(int pageNumber, int pageSize) => await _dbSet.Set<TEntity>().ToPagedListAsync(pageNumber, pageSize);

        public async Task<TEntity> Select(int id) => await _dbSet.Set<TEntity>().FindAsync(id);

        public IQueryable<TEntity> WithoutTenantFilter() => _dbSet.Set<TEntity>().IgnoreQueryFilters();


    }
}
