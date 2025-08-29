using Finan.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string? Description { get; set; }
        public List<Classification>? Classifications { get; set; }
        public List<BankTransaction>? BankTransactions { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public NatureGroupEnum Nature { get; set; }
    }
}
