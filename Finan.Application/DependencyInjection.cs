using Finan.Domain.Entities;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Repository;
using Finan.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Finan.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IBaseService<FinancialGroup>, BaseService<FinancialGroup>>();
            services.AddScoped<IBaseService<Currency>, BaseService<Currency>>();
            services.AddScoped<IBaseService<CostCenter>, BaseService<CostCenter>>();
            services.AddScoped<IBaseService<Payer>, BaseService<Payer>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFinancialClassificationService, FinancialClassificationService>();
            services.AddScoped<IReceivableService, ReceivableService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBaseService<Bank>, BaseService<Bank>>();
            services.AddScoped<IAccountService, AccountService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<FinancialGroup>, BaseRepository<FinancialGroup>>();
            services.AddScoped<IBaseRepository<Currency>, BaseRepository<Currency>>();
            services.AddScoped<IBaseRepository<CostCenter>, BaseRepository<CostCenter>>();
            services.AddScoped<IBaseRepository<Payer>, BaseRepository<Payer>>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReceivableRepository, ReceivableRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IFinancialClassificationRepository, FinancialClassificationRepository>();
            services.AddScoped<IBaseRepository<Bank>, BaseRepository<Bank>>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
