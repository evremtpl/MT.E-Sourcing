using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MT.E_Sourcing.Order.Domain.Repositories;
using MT.E_Sourcing.Order.Domain.Repositories.Base;
using MT.E_Sourcing.Order.Infrastructure.Data;
using MT.E_Sourcing.Order.Infrastructure.Repositories;
using MT.E_Sourcing.Order.Infrastructure.Repositories.Base;

namespace MT.E_Sourcing.Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<OrderContext>(opt=>opt.UseInMemoryDatabase(databaseName:"InMemoryDb"),
            //    ServiceLifetime.Singleton,ServiceLifetime.Singleton);

            services.AddDbContext<OrderContext>(opt => 
            opt.UseSqlServer(configuration.GetConnectionString("OrderConnection"),
                b => b.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton );
            
            
           

            services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
            services.AddTransient<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
