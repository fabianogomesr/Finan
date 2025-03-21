using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.Entities
{
    public class Account : BaseEntity
    {
        public Bank? Bank { get; set; }
        public string? Name { get; set; }
        public Receivable? Receivable { get; set; }
        public string? Agency { get; set; }
        public string? Number { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public List<Statement>? Statements { get; set; }
    }
}
