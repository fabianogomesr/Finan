using Finan.Web.Clients;

namespace Finan.Web
{
    public static class DependencyInjection
    {
        public static void AddClients(this IServiceCollection services)
        {
            #region IOC Clients
            services.AddScoped<IBankClient, BankClient>();
            services.AddScoped<IUserClient, UserClient>();
            services.AddScoped<IGroupClient, GroupClient>();
            services.AddScoped<IClassificationClient, ClassificationClient>();
            services.AddScoped<ICostCenterClient, CostCenterClient>();
            services.AddScoped<ICurrencyClient, CurrencyClient>();
            services.AddScoped<ITransactionClient, TransactionClient>();
            services.AddScoped<IAccountClient, AccountClient>();
            #endregion

        }
    }
}
