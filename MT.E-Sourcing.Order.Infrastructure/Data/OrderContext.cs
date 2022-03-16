

using Microsoft.EntityFrameworkCore;

namespace MT.E_Sourcing.Order.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        protected OrderContext(DbContextOptions<OrderContext> options) :base(options)
        {

        }

        public DbSet<Domain.Entities.Order> Orders { get; set; }
    }
}
