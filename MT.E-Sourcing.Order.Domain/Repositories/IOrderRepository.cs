using MT.E_Sourcing.Order.Domain.Repositories.Base;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MT.E_Sourcing.Order.Domain.Repositories
{
   public interface IOrderRepository :IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersBySellerUserName(string userName);
    }
}
