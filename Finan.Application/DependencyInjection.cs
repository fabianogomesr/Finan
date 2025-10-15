using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Repository;
using Finan.Service.Context;
using Finan.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Finan.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Serviços base genéricos de contrato
            services.AddScoped<IBaseContractService<Currency>, BaseContractService<Currency>>();
            services.AddScoped<IBaseContractService<CostCenter>, BaseContractService<CostCenter>>();
            services.AddScoped<IBaseContractService<Bank>, BaseContractService<Bank>>();

            // Serviços específicos
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();

            // Serviços base genéricos
            services.AddScoped<IBaseService<Contract>, BaseService<Contract>>();
            services.AddScoped<IBaseService<SubscriptionPlan>, BaseService<SubscriptionPlan>>();

            // Contexto do usuário
            services.AddScoped<IUserContext, UserContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            // Repositórios base genéricos de contrato
            services.AddScoped<IBaseContractRepository<Currency>, BaseContractRepository<Currency>>();
            services.AddScoped<IBaseContractRepository<CostCenter>, BaseContractRepository<CostCenter>>();
            services.AddScoped<IBaseContractRepository<Bank>, BaseContractRepository<Bank>>();

            // Repositórios específicos
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IStatementRepository, StatementRepository>();

            // Repositorios base genéricos
            services.AddScoped<IBaseRepository<Contract>, BaseRepository<Contract>>();
            services.AddScoped<IBaseRepository<SubscriptionPlan>, BaseRepository<SubscriptionPlan>>();
        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
