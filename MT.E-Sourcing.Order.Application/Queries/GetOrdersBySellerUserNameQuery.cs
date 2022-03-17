

using MediatR;
using MT.E_Sourcing.Order.Application.Responses;
using System.Collections.Generic;

namespace MT.E_Sourcing.Order.Application.Queries
{
    public class GetOrdersBySellerUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {

        public string UserName { get; set; }

        public GetOrdersBySellerUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
