using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Interfaces
{
    public interface ISeedDataRepository
    {
        public Task SeedDefaultDataAsync(Guid tenantId);
    }
}
