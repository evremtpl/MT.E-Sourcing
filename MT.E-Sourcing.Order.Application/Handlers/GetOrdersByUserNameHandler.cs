using AutoMapper;
using MediatR;
using MT.E_Sourcing.Order.Application.Queries;
using MT.E_Sourcing.Order.Application.Responses;
using MT.E_Sourcing.Order.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Application.Handlers
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersBySellerUserNameQuery, IEnumerable<OrderResponse>>
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersByUserNameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersBySellerUserNameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersBySellerUserName(request.UserName);
            var response = _mapper.Map<IEnumerable<OrderResponse>>(orderList);

            return response;
        }
    }
}
