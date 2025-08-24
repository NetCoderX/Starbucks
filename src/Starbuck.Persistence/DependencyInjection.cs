using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Starbuck.Persistence;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(op =>
        {
            op.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}
