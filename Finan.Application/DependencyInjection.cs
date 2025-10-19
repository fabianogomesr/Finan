using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Repository;
using Finan.Service.Identity;
using Finan.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Finan.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Serviços específicos
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();

            // Serviços base genéricos
            services.AddScoped<IBaseService<Currency>, BaseService<Currency>>();
            services.AddScoped<IBaseService<CostCenter>, BaseService<CostCenter>>();
            services.AddScoped<IBaseService<Bank>, BaseService<Bank>>();

            // Contexto do usuário
            services.AddScoped<IUserContext, UserContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            // Repositórios específicos
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IStatementRepository, StatementRepository>();
            services.AddScoped<ISeedDataRepository, SeedDataRepository>();

            // Repositorios base genéricos
            services.AddScoped<IBaseRepository<Currency>, BaseRepository<Currency>>();
            services.AddScoped<IBaseRepository<CostCenter>, BaseRepository<CostCenter>>();
            services.AddScoped<IBaseRepository<Bank>, BaseRepository<Bank>>();
        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
