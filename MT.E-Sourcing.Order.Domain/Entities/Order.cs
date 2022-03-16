

using MT.E_Sourcing.Order.Domain.Entities.Base;
using System;

namespace MT.E_Sourcing.Order.Domain.Entities
{
    public class Order : Entity
    {
        public string AuctionId  { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
