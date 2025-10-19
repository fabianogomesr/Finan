using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Extensions;
using Finan.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Finan.Infra.Data.Context
{
    public class BaseContext : DbContext 
    {
        private readonly IUserContext _userContext;
        public BaseContext(DbContextOptions<BaseContext> options, IUserContext userContext) : base(options)
        {
            _userContext = userContext;
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
            ApplyGlobalQueryFilters(modelBuilder);

        }

        private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            // Aplica o filtro global para todas as entidades que implementam ITenantEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(MultiTenantEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // Cria um filtro dinâmico (lambda) em tempo de execução
                    var method = typeof(BaseContext)
                        .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private void SetGlobalQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : MultiTenantEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(e => e.TenantId == _userContext.TenantId);
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

        public override int SaveChanges()
        {
            AddTenantIdToNewEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTenantIdToNewEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTenantIdToNewEntities()
        {
            var tenantId = _userContext.TenantId;

            // Se não há tenant no contexto (Guid.Empty), não faz nada
            if (tenantId == Guid.Empty)
                return;

            foreach (var entry in ChangeTracker.Entries<MultiTenantEntity>()
                .Where(e => e.State == EntityState.Added))
            {
                // Se o TenantId ainda não foi definido manualmente, preenche
                if (entry.Entity.TenantId == Guid.Empty)
                    entry.Entity.TenantId = tenantId;
            }
        }


    }
}
