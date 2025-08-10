using TShip.Configurations;

namespace TShip.ConfigurationBindings
{
    public static class ConfigBinder
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            return services;
        }
    }
}
