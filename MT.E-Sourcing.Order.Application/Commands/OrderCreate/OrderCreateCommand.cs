

using MediatR;
using MT.E_Sourcing.Order.Application.Responses;
using System;

namespace MT.E_Sourcing.Order.Application.Commands.OrderCreate
{
    public class OrderCreateCommand : IRequest<OrderResponse>
    {
        
        public string AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        public decimal UnitePrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
