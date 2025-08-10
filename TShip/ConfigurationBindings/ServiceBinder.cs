using TShip.Services;
using TShip.Services.Interfaces;

namespace TShip.ConfigurationBindings
{
    public static class ServiceBinder
    {
        public static IServiceCollection AddServiceBindings(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
