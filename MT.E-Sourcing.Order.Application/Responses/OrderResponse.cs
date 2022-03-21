

using System;

namespace MT.E_Sourcing.Order.Application.Responses
{
   public class OrderResponse
    {
        public string Id  { get; set; }
        public string AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
