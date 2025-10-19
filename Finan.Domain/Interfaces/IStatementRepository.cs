using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface IStatementRepository : IBaseRepository<Statement>
    {
        decimal GetBalanceByAccountId(int accountId);
    }
}
