using Finan.CrossCutting.Encrypt;
using Finan.Domain.Interfaces;
using Finan.Infra.Data.Context;
using Finan.Infra.Data.Repository;
using Finan.Service.Identity;
using Finan.Service.Jwt;
using Finan.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Finan.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClassificationService, ClassificationService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICostCenterService, CostCenterService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IStatementRepository, StatementRepository>();
            services.AddScoped<ISeedDataRepository, SeedDataRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICostCenterRepository, CostCenterRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
        }

        public static void AddMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<BaseContext>();

            try
            {
                if (db.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("➡️ Aplicando migrations...");
                    db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Erro ao migrar banco: " + ex.Message);
            }

        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BaseContext>(options =>
                options.UseNpgsql(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information));
        }
    }
}
