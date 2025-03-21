using Finan.Domain.DTOs;
using Finan.Domain.Entities;
using Finan.Domain.Parameters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IBankService : IBaseService<Bank>
    {
        Task<BankPaginationDTO?> GetAsync(int pageNumber, int pageSize);
    }
}
