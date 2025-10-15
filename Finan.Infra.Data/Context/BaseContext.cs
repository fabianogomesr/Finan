using Finan.Domain.Entities;
using Finan.Domain.Enums;
using Finan.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

        public DbSet<BankTransaction> BankTransaction { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<CostCenter> CostCenter { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Classification> Classification { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Statement> Statement { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureEntityMappings(modelBuilder);
            SeedEntities(modelBuilder);
        }
        private static void SeedEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUserSeed());
            modelBuilder.Entity<Group>().HasData(GetGroupSeed());
            modelBuilder.Entity<Classification>().HasData(GetClassificationSeed());
            modelBuilder.Entity<Currency>().HasData(GetCurrencySeed());
            modelBuilder.Entity<CostCenter>().HasData(GetCostCenterSeed());
            modelBuilder.Entity<Bank>().HasData(GetBankSeed());
            modelBuilder.Entity<SubscriptionPlan>().HasData(GetSubscriptionPlanSeed());
            modelBuilder.Entity<Contract>().HasData(GetContractSeed());
        }

        private static void ConfigureEntityMappings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Group>(new GroupMap().Configure);
            modelBuilder.Entity<Classification>(new ClassificationMap().Configure);
            modelBuilder.Entity<Currency>(new CurrencyMap().Configure);
            modelBuilder.Entity<CostCenter>(new CostCenterMap().Configure);
            modelBuilder.Entity<Bank>(new BankMap().Configure);
            modelBuilder.Entity<Account>(new AccountMap().Configure);
            modelBuilder.Entity<BankTransaction>(new BankTransactionMap().Configure);
            modelBuilder.Entity<Transaction>(new TransactionMap().Configure);
            modelBuilder.Entity<Statement>(new StatementMap().Configure);
            modelBuilder.Entity<SubscriptionPlan>(new SubscriptionPlanMap().Configure);
            modelBuilder.Entity<Contract>(new ContractMap().Configure);
        }

        private static SubscriptionPlan[] GetSubscriptionPlanSeed() => new[]
{
            new SubscriptionPlan { Id = 1, Name = "Gratuito", UserQuantity = 1, Value = 0  },
            new SubscriptionPlan { Id = 2, Name = "Profissional", UserQuantity = 5, Value = 29.90M  },
            new SubscriptionPlan { Id = 3, Name = "Empresarial", UserQuantity = 20, Value = 99.90M  }
        };

        private static Contract[] GetContractSeed() => new[]
        {       
            new Contract { Id = 1, SubscriptionPlanId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(99), IsActive = true  },
        };

        private static User[] GetUserSeed() => new[]
        {
            new User { Id = 1, UserName = "Finan", Password = "Finan@1234", Email = "dev.fabianorocha@gmail.com", Role = "Manager", ContractId = 1 }
        };

        private static Group[] GetGroupSeed() => new[]
        {
            new Group { Id = 1, Description = "Despesas Fixas", Nature = NatureGroup.Debit, ContractId = 0 },
            new Group { Id = 2, Description = "Despesas Variáveis", Nature = NatureGroup.Debit, ContractId = 0  },
            new Group { Id = 3, Description = "Receitas de Trabalho", Nature = NatureGroup.Credit, ContractId = 0  },
            new Group { Id = 4, Description = "Receitas de Investimentos", Nature = NatureGroup.Credit, ContractId = 0  },
            new Group { Id = 5, Description = "Receitas Eventuais ou Extraordinárias", Nature = NatureGroup.Credit, ContractId = 0  },
        };

        private static Classification[] GetClassificationSeed() => new[]
        {
            new Classification { Id = 1, Description = "Moradia", GroupId = 1, ContractId = 0  },
            new Classification { Id = 2, Description = "Alimentação", GroupId = 2, ContractId = 0  },
            new Classification { Id = 3, Description = "Transporte", GroupId = 2, ContractId = 0  },
            new Classification { Id = 4, Description = "Lazer", GroupId = 2, ContractId = 0  },
            new Classification { Id = 5, Description = "Viagens", GroupId = 2, ContractId = 0  },
            new Classification { Id = 6, Description = "Salário", GroupId = 3, ContractId = 0  },
            new Classification { Id = 7, Description = "Gratificações e Bônus", GroupId = 3, ContractId = 0  },
            new Classification { Id = 8, Description = "Rendimentos de Poupança", GroupId = 4, ContractId = 0  },
            new Classification { Id = 9, Description = "Dividendos", GroupId = 4, ContractId = 0  },
            new Classification { Id = 10, Description = "Juros de Investimentos", GroupId = 4, ContractId = 0  },
            new Classification { Id = 11, Description = "Aluguel de Imóveis", GroupId = 4, ContractId = 0  },
            new Classification { Id = 12, Description = "Presentes", GroupId = 5, ContractId = 0  },
            new Classification { Id = 13, Description = "Vendas de Bens", GroupId = 5, ContractId = 0  },
            new Classification { Id = 14, Description = "Educação e Cursos", GroupId = 2, ContractId = 0  },
            new Classification { Id = 15, Description = "Cartão de Crédito", GroupId = 2, ContractId = 0  },
            new Classification { Id = 16, Description = "Serviços de Internet, Gás e Energia", GroupId = 2, ContractId = 0  },
            new Classification { Id = 17, Description = "Pagamento de Impostos", GroupId = 2, ContractId = 0  },
            new Classification { Id = 18, Description = "Restituição de Impostos", GroupId = 5, ContractId = 0  }
        };

        private static Currency[] GetCurrencySeed() => new[]
        {
            new Currency { Id = 1, Name = "Real", Code = "BRL", Symbol = "R$", ContractId = 0  },
            new Currency { Id = 2, Name = "Dólar", Code = "USD", Symbol = "$", ContractId = 0  }
        };

        private static CostCenter[] GetCostCenterSeed() => new[]
        {
            new CostCenter { Id = 1, Description = "Casa", ContractId = 0  },
            new CostCenter { Id = 2, Description = "Transporte", ContractId = 0  },
            new CostCenter { Id = 3, Description = "Saúde", ContractId = 0  },
            new CostCenter { Id = 4, Description = "Educação", ContractId = 0  },
            new CostCenter { Id = 5, Description = "Lazer", ContractId = 0  },
            new CostCenter { Id = 6, Description = "Investimentos", ContractId = 0  },
            new CostCenter { Id = 7, Description = "Alimentação", ContractId = 0  },
            new CostCenter { Id = 8, Description = "Cartão de Crédito", ContractId = 0  },
            new CostCenter { Id = 9, Description = "Doações e Presentes", ContractId = 0  },
            new CostCenter { Id = 10, Description = "Trabalho Formal", ContractId = 0  },
            new CostCenter { Id = 11, Description = "Trabalho Autônomo", ContractId = 0  },
            new CostCenter { Id = 12, Description = "Aluguéis", ContractId = 0  },
            new CostCenter { Id = 13, Description = "Outras Receitas", ContractId = 0  }
        };

        private static Bank[] GetBankSeed() => new[]
        {
            new Bank { Id = 1, Name = "Banco do Brasil S.A.", Code = "001", ContractId = 0  },
            new Bank { Id = 2, Name = "Banco Santander (Brasil) S.A.", Code = "033", ContractId = 0  },
            new Bank { Id = 3, Name = "Caixa Econômica Federal", Code = "104", ContractId = 0  },
            new Bank { Id = 4, Name = "Banco Bradesco S.A.", Code = "237", ContractId = 0  },
            new Bank { Id = 5, Name = "Itaú Unibanco S.A.", Code = "341", ContractId = 0  },
            new Bank { Id = 6, Name = "Nu Pagamentos S.A.", Code = "260", ContractId = 0  },
            new Bank { Id = 7, Name = "Banco C6 S.A.", Code = "336", ContractId = 0  },
            new Bank { Id = 8, Name = "Banco Inter S.A.", Code = "077", ContractId = 0  }
        };

    }
}
