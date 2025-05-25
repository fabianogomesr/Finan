using Finan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Domain.DTOs
{
    public class AccountDTO
    {
        public int? Id { get; set; }
        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? Name { get; set; }
        public string? Agency { get; set; }
        public string? Number { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal? Balance { get; set; }
    }
}
