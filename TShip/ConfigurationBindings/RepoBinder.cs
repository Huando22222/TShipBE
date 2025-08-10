// Tệp: ConfigurationBindings/AccountRepoBinder.cs

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TShip.Repositories;
using TShip.Repositories.Interfaces;

namespace TShip.ConfigurationBindings
{
    public static class RepoBinder
    {
        public static IServiceCollection AddRepoBindings(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepo, AccountRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            return services;
        }
    }
}
