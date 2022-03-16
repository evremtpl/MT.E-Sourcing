using Microsoft.EntityFrameworkCore;
using MT.E_Sourcing.Order.Domain.Repositories;
using MT.E_Sourcing.Order.Infrastructure.Data;
using MT.E_Sourcing.Order.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Domain.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersBySellerUserName(string userName)
        {
            var orderList = await _context.Orders
                .Where(o => o.SellerUserName == userName)
                .ToListAsync();

            return orderList;
        }
    }
}
