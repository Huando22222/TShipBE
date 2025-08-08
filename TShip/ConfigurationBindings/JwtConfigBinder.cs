using TShip.Configurations;

namespace TShip.ConfigurationBindings
{
    public static class JwtConfigBinder
    {
        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
            return services;
        }
    }
}
