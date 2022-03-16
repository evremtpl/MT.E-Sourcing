

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Infrastructure.Data
{
   public static class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext)
        {
            if(!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreConfiguredOrders());
                await orderContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Domain.Entities.Order> GetPreConfiguredOrders()
        {
            return new List<Domain.Entities.Order>()
           {
               new Domain.Entities.Order()
               {
                   AuctionId=Guid.NewGuid().ToString(),
                   ProductId = Guid.NewGuid().ToString(),
                   SellerUserName="test@test.com",
                   UnitPrice=10,
                   TotalPrice=1000,
                   CreateDate=DateTime.UtcNow
               },
                 new Domain.Entities.Order()
               {
                   AuctionId=Guid.NewGuid().ToString(),
                   ProductId = Guid.NewGuid().ToString(),
                   SellerUserName="test2@test.com",
                   UnitPrice=10,
                   TotalPrice=1000,
                   CreateDate=DateTime.UtcNow
               },
                   new Domain.Entities.Order()
               {
                   AuctionId=Guid.NewGuid().ToString(),
                   ProductId = Guid.NewGuid().ToString(),
                   SellerUserName="test3@test.com",
                   UnitPrice=10,
                   TotalPrice=1000,
                   CreateDate=DateTime.UtcNow
               }

           };
        }
    }
}
