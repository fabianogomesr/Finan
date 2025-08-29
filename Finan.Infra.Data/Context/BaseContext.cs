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
            //SeedEntities(modelBuilder);
        }
        private static void SeedEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUserSeed());
            modelBuilder.Entity<Group>().HasData(GetGroupSeed());
            modelBuilder.Entity<Classification>().HasData(GetClassificationSeed());
            modelBuilder.Entity<Currency>().HasData(GetCurrencySeed());
            modelBuilder.Entity<CostCenter>().HasData(GetCostCenterSeed());
            modelBuilder.Entity<Bank>().HasData(GetBankSeed());
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
        }

        private static User[] GetUserSeed() => new[]
        {
            new User { UserName = "Finan", Password = "Finan@1234", Email = "dev.fabianorocha@gmail.com", Role = "Maganer" }
        };
        private static Group[] GetGroupSeed() => new[]
        {
            new Group { Description = "Despesas Fixas", Nature = NatureGroupEnum.Debit },
            new Group { Description = "Despesas Variáveis", Nature = NatureGroupEnum.Debit },
            new Group { Description = "Receitas de Trabalho", Nature = NatureGroupEnum.Credit },
            new Group { Description = "Receitas de Investimentos", Nature = NatureGroupEnum.Credit },
            new Group { Description = "Receitas Eventuais ou Extraordinárias", Nature = NatureGroupEnum.Credit },
            new Group { Description = "Impostos", Nature = NatureGroupEnum.Both }
        };
        private static Classification[] GetClassificationSeed() => new[]
        {
            new Classification { Description = "Moradia", GroupId = 1 },
            new Classification { Description = "Alimentação", GroupId = 2},
            new Classification { Description = "Transporte", GroupId = 2 },
            new Classification { Description = "Lazer", GroupId = 2 },
            new Classification { Description = "Viagens", GroupId = 2 },
            new Classification { Description = "Salário", GroupId = 3 },
            new Classification { Description = "Gratificações e Bônus", GroupId = 3 },
            new Classification { Description = "Rendimentos de Poupança", GroupId = 4 },
            new Classification { Description = "Dividendos", GroupId = 4 },
            new Classification { Description = "Juros de Investimentos", GroupId = 4},
            new Classification { Description = "Aluguel de Imóveis", GroupId = 4 },
            new Classification { Description = "Presentes", GroupId = 5 },
            new Classification { Description = "Vendas de Bens", GroupId = 5},
            new Classification { Description = "Educação e Cursos", GroupId = 2 },
            new Classification { Description = "Cartão de Crédito", GroupId = 2},
            new Classification { Description = "Serviços de Internet, Gás e Energia", GroupId = 2 },
            new Classification { Description = "Pagamento e Restituição de Impostos", GroupId = 2 }
        };
        private static Currency[] GetCurrencySeed() => new[]
        {
            new Currency { Name = "Real", Code = "BRL", Symbol = "R$" },
            new Currency { Name = "Dólar", Code = "USD", Symbol = "$" }
        };
        private static CostCenter[] GetCostCenterSeed() => new[]
        {
                new CostCenter { Description = "Casa" },
                new CostCenter { Description = "Transporte" },
                new CostCenter { Description = "Saúde" },
                new CostCenter { Description = "Educação" },
                new CostCenter { Description = "Lazer" },
                new CostCenter { Description = "Investimentos" },
                new CostCenter { Description = "Alimentação" },
                new CostCenter { Description = "Cartão de Crédito" },
                new CostCenter { Description = "Doações e Presentes" },
                new CostCenter { Description = "Trabalho Formal" },
                new CostCenter { Description = "Trabalho Autônomo" },
                new CostCenter { Description = "Aluguéis" },
                new CostCenter { Description = "Outras Receitas" }
        };
        private static Bank[] GetBankSeed() => new[]
        {
            new Bank { Name = "Banco do Brasil S.A.", Code = "001"},
            new Bank { Name = "Banco Santander (Brasil) S.A.", Code = "033" },
            new Bank { Name = "Caixa Econômica Federal", Code = "104" },
            new Bank { Name = "Banco Bradesco S.A.", Code = "237" },
            new Bank { Name = "Itaú Unibanco S.A.", Code = "341" },
            new Bank { Name = "Nu Pagamentos S.A.", Code = "260" },
            new Bank { Name = "Banco C6 S.A.", Code = "336" },
            new Bank { Name = "Banco Inter S.A.", Code = "077" }
        };

    }
}
