using MT.E_Sourcing.Common.Events.Abstract;
using System;

namespace MT.E_Sourcing.Common.Events.Concrete
{
    public  class OrderCreateEvent : IEvent
    {
        public string Id { get; set; }

        public string AuctionId { get; set; }

        public string ProductId { get; set; }

        public string SellerUserName { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }

        public int Quantity { get; set; }
    }
}
