using Finan.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
        Task DeleteAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<EntityPagination<TEntity>> GetAsync(int pageNumber, int pageSize);
        Task<TEntity> UpdateAsync<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;
        IQueryable<TEntity> GetAll();
    }
}
