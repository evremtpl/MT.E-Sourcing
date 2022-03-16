using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MT.E_Sourcing.Order.Infrastructure.Data;

namespace MT.E_Sourcing.Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<OrderContext>(opt=>opt.UseInMemoryDatabase(databaseName:"InMemoryDb"),
                ServiceLifetime.Singleton,ServiceLifetime.Singleton);

            return services;
        }
    }
}
