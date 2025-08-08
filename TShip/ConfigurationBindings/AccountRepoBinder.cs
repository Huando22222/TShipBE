// Tệp: ConfigurationBindings/AccountRepoBinder.cs

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TShip.Repositories;

namespace TShip.ConfigurationBindings
{
    public static class AccountRepoBinder
    {
        public static IServiceCollection AddAccountBindings(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            return services;
        }
    }
}
