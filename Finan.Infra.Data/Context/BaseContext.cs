using Finan.Domain.Entities;
using Finan.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finan.Infra.Data.Context
{
    public class BaseContext : DbContext 
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public DbSet<AccountDeposit> AccountDeposit { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<FinancialClassification> FinancialClassification { get; set; }
        public DbSet<FinancialGroup> FinancialGroup { get; set; }
        public DbSet<Payer> Payer { get; set; }
        public DbSet<Payment> Payment { get; set; }

        public DbSet<Statement> Statement { get; set; }
        public DbSet<Receivable> Receivable { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<FinancialGroup>(new FinancialGroupMap().Configure);
            modelBuilder.Entity<FinancialClassification>(new FinancialClassificationMap().Configure);
            modelBuilder.Entity<Currency>(new CurrencyMap().Configure);
            modelBuilder.Entity<CostCenter>(new CostCenterMap().Configure);
            modelBuilder.Entity<Payer>(new PayerMap().Configure);
            modelBuilder.Entity<Bank>(new BankMap().Configure);
            modelBuilder.Entity<Account>(new AccountMap().Configure);
            modelBuilder.Entity<AccountDeposit>(new AccountDepositMap().Configure);
            modelBuilder.Entity<Receivable>(new ReceivableMap().Configure);
            modelBuilder.Entity<Payment>(new PaymentMap().Configure);
            modelBuilder.Entity<Statement>(new StatementMap().Configure);
        }
    }
}
