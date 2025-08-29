using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static PagedResult<T> ToPagedList<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            if (page <= 0) page = 1;

            var totalCount = query.Count();
            var items = query
                .Skip((page - 1) * pageSize)
                .Take(10)
                .ToList();

            return new PagedResult<T>(items, totalCount, page, pageSize);
        }
    }
}
