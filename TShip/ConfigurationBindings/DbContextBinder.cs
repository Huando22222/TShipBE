using Microsoft.EntityFrameworkCore;
using TShip.Data;

namespace TShip.ConfigurationBindings
{
    public static class DbContextBinder
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }   
    }
}
