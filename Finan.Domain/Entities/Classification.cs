using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Classification : BaseContractEntity
    {
        public string? Description { get; set; }
        public Group? Group { get; set; }
        public int GroupId { get; set; }
        public List<BankTransaction>? BankTransactions { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
