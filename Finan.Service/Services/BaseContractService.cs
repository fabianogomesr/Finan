using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Service.Services
{
    public class BaseContractService<TEntity> : IBaseContractService<TEntity> where TEntity : BaseContractEntity
    {
        private readonly IBaseContractRepository<TEntity> _baseRepository;

        public BaseContractService(IBaseContractRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(obj);
            return obj;
        }

        public async Task DeleteAsync(int id) => await _baseRepository.Delete(id);

        public async Task<IEnumerable<TEntity>> GetAsync() => await _baseRepository.Select();

        public async Task<TEntity> GetByIdAsync(int id) => await _baseRepository.Select(id);

        public async Task<PagedResult<TEntity>> GetAsync(int pageNumber, int pageSize) => await _baseRepository.Select(pageNumber, pageSize);

        public IQueryable<TEntity> GetAll() => _baseRepository.GetAll();

        public async Task<TEntity> UpdateAsync<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(obj);
            return obj;
        }

        public void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
