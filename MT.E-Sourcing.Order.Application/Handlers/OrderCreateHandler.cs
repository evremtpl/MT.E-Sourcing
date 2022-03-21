using AutoMapper;
using MediatR;
using MT.E_Sourcing.Order.Application.Commands.OrderCreate;
using MT.E_Sourcing.Order.Application.Responses;
using MT.E_Sourcing.Order.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Order.Application.Handlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Domain.Entities.Order>(request);

            if (orderEntity == null)
                throw new ApplicationException("Entity could  not be mapped");

            var order = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return orderResponse;
        }
    }
}
