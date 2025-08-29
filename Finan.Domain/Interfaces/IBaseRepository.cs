using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity obj);
        Task Insert(List<TEntity> obj);
        Task Update(TEntity obj);
        Task Delete(int id);
        Task<IEnumerable<TEntity>> Select();
        Task<TEntity> Select(int id);
        Task<PagedResult<TEntity>> Select(int pageNumber, int pageSize);
        IQueryable<TEntity> GetAll();
    }
}
