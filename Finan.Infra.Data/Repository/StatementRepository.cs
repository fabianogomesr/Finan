using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Repository
{
    public class StatementRepository : BaseContractRepository<Statement>, IStatementRepository
    {
        protected new readonly BaseContext _dbSet;
        public StatementRepository(BaseContext mySqlContext, IUserContext userContext) : base(mySqlContext, userContext)
        {
            _dbSet = mySqlContext;
        }

        public decimal GetBalanceByAccountId(int accountId) => GetAll().Where(s => s.AccountId == accountId && !s.Reversed && s.ReconciledDate != null).Sum(s => s.Value);

    }
}
