using Finan.Domain.Entities;
using Finan.Contracts.Response;
using Finan.Contracts.Request;
using Finan.Contracts.Enums;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Repository
{
    public class ClassificationRepository : BaseRepository<Classification>, IClassificationRepository
    {
        protected readonly BaseContext _dbSet;

        public ClassificationRepository(BaseContext mySqlContext) : base(mySqlContext)
        {
            _dbSet = mySqlContext;
        }

        public async Task<IEnumerable<Classification>> GetClassificationsByGroupIdAsync(int GroupId) => await GetAll().Include(x => x.Group).Where(x => x.GroupId == GroupId).ToListAsync();
        public async Task<Classification> GetClassificationByIdAsync(int id) => await GetAll().Include(x => x.Group).FirstAsync(x => x.Id == id);
        public async Task<IEnumerable<Classification>> GetClassificationsAsync() => await GetAll().Include(x => x.Group).ToListAsync();
        public async Task<PagedResult<ClassificationResponse>> GetClassificationsAsync(int pageNumber = 1, int pageSize = 5) 
        {
            var result = GetAll().Select(x => new ClassificationResponse
            {
                Id = x.Id,
                Description = x.Description,
                GroupId = x.Group != null ? x.Group.Id : 0,
                GroupName = x.Group != null ? x.Group.Description : String.Empty
            });

            return await result.ToPagedListAsync(pageNumber, pageSize);
        }

    }
}
