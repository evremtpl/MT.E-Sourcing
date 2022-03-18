using AutoMapper;
using MT.E_Sourcing.Common.Events.Concrete;
using MT.E_Sourcing.Order.Application.Commands.OrderCreate;


namespace MT.E_Sourcing.Order.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
